using System;
using System.Data.SQLite;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Veresiye2025
{
    public partial class Form1 : Form
    {
        public bool isDarkMode = false;

        public Form1()
        {
            InitializeComponent();

            // Modern form ayarları
            this.FormBorderStyle = FormBorderStyle.None;
            this.Region = CreateRoundedRectangle(this.Width, this.Height, 10);

            // Giriş bilgilerini yükle
            LoadSavedCredentials();

            // Dil ayarını yükle
            string savedLanguage = LanguageManager.Instance.GetSavedLanguage();
            LanguageManager.Instance.LoadLanguage(savedLanguage);
            LanguageManager.Instance.ApplyLanguage(this);

            // Şifre engel durumunu kontrol et
            CheckPasswordRestriction();

            // Çerçeve kenarlarını yuvarlatın
            this.Region = CreateRoundedRectangle(this.Width, this.Height, 10);
            loginPanel.Region = CreateRoundedRectangle(loginPanel.Width, loginPanel.Height, 8);
        }

        // Yuvarlak köşeli form için yardımcı metot
        public Region CreateRoundedRectangle(int width, int height, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            Rectangle rect = new Rectangle(0, 0, width, height);

            path.AddArc(rect.X, rect.Y, radius * 2, radius * 2, 180, 90);
            path.AddArc(rect.X + rect.Width - (radius * 2), rect.Y, radius * 2, radius * 2, 270, 90);
            path.AddArc(rect.X + rect.Width - (radius * 2), rect.Y + rect.Height - (radius * 2), radius * 2, radius * 2, 0, 90);
            path.AddArc(rect.X, rect.Y + rect.Height - (radius * 2), radius * 2, radius * 2, 90, 90);
            path.CloseFigure();

            return new Region(path);
        }

        // 🚀 YENİ: Kayıt Ol butonu click eventi
        public void btnKayitOl_Click(object sender, EventArgs e)
        {
            try
            {
                statusLabel.Text = "Kayıt formu açılıyor...";
                statusLabel.ForeColor = isDarkMode ? Color.LightSkyBlue : Color.RoyalBlue;

                this.Hide(); // Ana formu gizle
                Form2 kayitFormu = new Form2();

                if (kayitFormu.ShowDialog() == DialogResult.OK)
                {
                    // Kayıt başarılı - kullanıcıya bilgi ver
                    statusLabel.Text = "Kayıt başarılı! Şimdi giriş yapabilirsiniz.";
                    statusLabel.ForeColor = Color.MediumSeaGreen;

                    // Form alanlarını temizle
                    txtSifre.Clear();
                    txtSifre.Focus();
                }
                else
                {
                    // Kayıt iptal edildi veya hata
                    statusLabel.Text = LanguageManager.Instance.GetMessage("loginInfo");
                    statusLabel.ForeColor = isDarkMode ? Color.Silver : Color.DimGray;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kayıt formu açılırken hata oluştu: {ex.Message}",
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "Kayıt formunda hata oluştu";
                statusLabel.ForeColor = Color.Crimson;
            }
            finally
            {
                this.Show(); // Ana formu tekrar göster
            }
        }

        public void btnGirisYap_Click(object sender, EventArgs e)
        {
            // 🚀 YENİ: Gelişmiş boş alan kontrolü
            if (string.IsNullOrWhiteSpace(txtKullaniciAdi.Text))
            {
                statusLabel.Text = "Kullanıcı adı boş olamaz!";
                statusLabel.ForeColor = Color.Crimson;
                txtKullaniciAdi.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtSifre.Text))
            {
                statusLabel.Text = "Şifre boş olamaz!";
                statusLabel.ForeColor = Color.Crimson;
                txtSifre.Focus();
                return;
            }

            // Giriş işlemi başladığında UI güncellemesi
            btnGirisYap.Enabled = false;
            btnKayitOl.Enabled = false; // 🚀 YENİ: Kayıt ol butonunu da deaktif et
            loginProgressBar.Visible = true;
            statusLabel.Text = LanguageManager.Instance.GetMessage("loginInProgress");
            statusLabel.ForeColor = isDarkMode ? Color.LightSkyBlue : Color.RoyalBlue;
            Application.DoEvents();

            try
            {
                // Giriş işlemi için 0.8 saniye beklet (kullanıcı deneyimi için)
                System.Threading.Thread.Sleep(800);

                string kullaniciAdi = txtKullaniciAdi.Text.Trim();
                string sifreHash = Helpers.HashPassword(txtSifre.Text);
                string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";

                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // 🚀 YENİ: Hem Users hem Yonetim tablosundan kontrol et
                    string query = @"SELECT COUNT(1) FROM 
                            (SELECT KullaniciAdi, Sifre FROM Users 
                             WHERE KullaniciAdi = @KullaniciAdi AND Sifre = @Sifre
                             UNION 
                             SELECT KullaniciAdi, Sifre FROM Yonetim 
                             WHERE KullaniciAdi = @KullaniciAdi AND Sifre = @Sifre)";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@KullaniciAdi", kullaniciAdi);
                        cmd.Parameters.AddWithValue("@Sifre", sifreHash);
                        var result = cmd.ExecuteScalar();

                        if (Convert.ToInt32(result) > 0)
                        {
                            // Giriş başarılı
                            loginProgressBar.Value = 100;
                            statusLabel.Text = LanguageManager.Instance.GetMessage("loginSuccess");
                            statusLabel.ForeColor = Color.MediumSeaGreen;
                            Application.DoEvents();

                            // Animasyon tamamlanması için 0.5 saniye bekle
                            System.Threading.Thread.Sleep(500);

                            // Vadesi geçen carileri kontrol et
                            KontrolVadeGecenCariler();

                            // Giriş bilgilerini kaydet
                            SaveCredentials();

                            // Firma kontrolü
                            if (FirmaKaydiVarMi(out string firmaAdi))
                            {
                                // Ana ekranı aç
                                Form4 anaEkran = new Form4(firmaAdi);
                                this.Hide();
                                anaEkran.ShowDialog();
                                this.Show();

                                // Ana ekrandan dönüldüğünde UI sıfırla
                                ResetLoginUI();
                            }
                            else
                            {
                                // Firma kaydı yoksa kayıt formunu aç
                                Form3 firmaKayitFormu = new Form3();
                                firmaKayitFormu.ShowDialog();
                                ResetLoginUI();
                            }
                        }
                        else
                        {
                            // Giriş başarısız
                            loginProgressBar.Value = 0;
                            statusLabel.Text = LanguageManager.Instance.GetMessage("loginError");
                            statusLabel.ForeColor = Color.Crimson;
                            txtSifre.Clear();
                            txtSifre.Focus();

                            // Hata efekti (hafif titreşim)
                            ShakeControl(loginPanel);
                            ResetLoginButtons(); // 🚀 YENİ: Butonları tekrar aktif et
                            loginProgressBar.Visible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                statusLabel.Text = "Veritabanı hatası: " + ex.Message;
                statusLabel.ForeColor = Color.Crimson;
                ResetLoginButtons(); // 🚀 YENİ: Hata durumunda butonları aktif et
                loginProgressBar.Visible = false;
            }
        }

        // 🚀 YENİ: Login butonlarını sıfırlama metodu
        public void ResetLoginButtons()
        {
            btnGirisYap.Enabled = true;
            btnKayitOl.Enabled = true;
        }

        public void ResetLoginUI()
        {
            statusLabel.Text = LanguageManager.Instance.GetMessage("loginInfo");
            statusLabel.ForeColor = isDarkMode ? Color.Silver : Color.DimGray;
            ResetLoginButtons(); // 🚀 YENİ: Butonları sıfırla
            loginProgressBar.Visible = false;
            loginProgressBar.Value = 0;
            txtSifre.Clear();
        }

        public void ShakeControl(Control control)
        {
            var originalLocation = control.Location;
            var rnd = new Random();

            for (int i = 0; i < 10; i++)
            {
                control.Location = new Point(originalLocation.X + rnd.Next(-5, 6), originalLocation.Y);
                Application.DoEvents();
                System.Threading.Thread.Sleep(20);
            }

            control.Location = originalLocation;
        }

        public void KontrolVadeGecenCariler()
        {
            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string updateQuery = "UPDATE Cari SET Hesap = 'Pasif' WHERE VadeTarihi < @Today AND Hesap = 'Aktif'";
                using (SQLiteCommand updateCommand = new SQLiteCommand(updateQuery, connection))
                {
                    updateCommand.Parameters.AddWithValue("@Today", DateTime.Today);
                    updateCommand.ExecuteNonQuery();
                }
            }
        }

        public bool FirmaKaydiVarMi(out string firmaAdi)
        {
            firmaAdi = string.Empty;
            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT FirmaAdi FROM Firmalar LIMIT 1";
                using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                {
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        firmaAdi = result.ToString();
                        return true;
                    }
                }
            }
            return false;
        }

        public void SaveCredentials()
        {
            if (hatirla.Checked)
            {
                Properties.Settings.Default.RememberMe = true;
                Properties.Settings.Default.SavedUsername = txtKullaniciAdi.Text;
                Properties.Settings.Default.SavedPassword = txtSifre.Text;
            }
            else
            {
                Properties.Settings.Default.RememberMe = false;
                Properties.Settings.Default.SavedUsername = string.Empty;
                Properties.Settings.Default.SavedPassword = string.Empty;
            }
            Properties.Settings.Default.Save();
        }

        public void LoadSavedCredentials()
        {
            if (Properties.Settings.Default.RememberMe)
            {
                txtKullaniciAdi.Text = Properties.Settings.Default.SavedUsername;
                txtSifre.Text = Properties.Settings.Default.SavedPassword;
                hatirla.Checked = true;
            }
            else
            {
                // 🚀 YENİ: Artık placeholder otomatik - boş bırakıyoruz
                txtKullaniciAdi.Text = string.Empty;
                txtSifre.Text = string.Empty;
                hatirla.Checked = false;
            }
        }

        public void goster_CheckedChanged(object sender, EventArgs e)
        {
            // 🚀 YENİ: Guna2TextBox için password char değiştirme
            txtSifre.PasswordChar = goster.Checked ? '\0' : '•';
        }

        public void btnSifreDegistir_Click(object sender, EventArgs e)
        {
            DateTime engelZamani = Properties.Settings.Default.SifreEngelZamani;
            if (engelZamani > DateTime.Now)
            {
                TimeSpan kalanSure = engelZamani - DateTime.Now;

                MessageBox.Show(
                    $"{LanguageManager.Instance.GetMessage("passwordLocked")} {kalanSure.Minutes}:{kalanSure.Seconds.ToString("00")}",
                    LanguageManager.Instance.GetMessage("accessDenied"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            this.Hide();
            FormSifreDegistir sifreDegistirFormu = new FormSifreDegistir();
            sifreDegistirFormu.ShowDialog();
            this.Show();
            CheckPasswordRestriction();
        }

        public void SifreDegistirmeEngelle()
        {
            btnSifreDegistir.Enabled = false;
            btnSifreDegistir.Cursor = Cursors.No;
            Properties.Settings.Default.SifreEngelZamani = DateTime.Now.AddMinutes(10);
            Properties.Settings.Default.Save();
            CheckPasswordRestriction();
        }

        public void CheckPasswordRestriction()
        {
            Properties.Settings.Default.Reload();
            DateTime engelZamani = Properties.Settings.Default.SifreEngelZamani;

            if (engelZamani > DateTime.Now)
            {
                btnSifreDegistir.Enabled = false;
                btnSifreDegistir.Cursor = Cursors.No;
                btnSifreDegistir.BackColor = isDarkMode ? Color.FromArgb(60, 65, 70) : Color.Silver;

                btnSifreDegistir.Click -= btnSifreDegistir_Click;
                btnSifreDegistir.Click -= BtnSifreEngelli_Click;
                btnSifreDegistir.Click += BtnSifreEngelli_Click;

                if (!timerEngel.Enabled)
                {
                    timerEngel.Start();
                }
            }
            else
            {
                btnSifreDegistir.Enabled = true;
                btnSifreDegistir.Cursor = Cursors.Hand;
                btnSifreDegistir.BackColor = isDarkMode ? Color.FromArgb(75, 110, 175) : Color.FromArgb(108, 117, 125);

                btnSifreDegistir.Click -= btnSifreDegistir_Click;
                btnSifreDegistir.Click -= BtnSifreEngelli_Click;
                btnSifreDegistir.Click += btnSifreDegistir_Click;
                timerEngel.Stop();
            }
        }

        public void BtnSifreEngelli_Click(object sender, EventArgs e)
        {
            DateTime engelZamani = Properties.Settings.Default.SifreEngelZamani;
            TimeSpan kalanSure = engelZamani - DateTime.Now;

            MessageBox.Show(
                $"{LanguageManager.Instance.GetMessage("passwordLocked")} {kalanSure.Minutes}:{kalanSure.Seconds.ToString("00")}",
                LanguageManager.Instance.GetMessage("accessDenied"),
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );
        }

        public void timerEngel_Tick(object sender, EventArgs e)
        {
            CheckPasswordRestriction();
        }

        public void btnTheme_Click(object sender, EventArgs e)
        {
            isDarkMode = !isDarkMode;
            ApplyTheme(isDarkMode);
        }

        public void ApplyTheme(bool darkMode)
        {
            if (darkMode)
            {
                // Ana form
                this.BackColor = Color.FromArgb(18, 18, 18);

                // Login panel
                loginPanel.BackColor = Color.FromArgb(30, 30, 30);
                loginPanel.ForeColor = Color.White;

                // Başlık çubuğu
                titleBar.BackColor = Color.FromArgb(24, 24, 24);
                lblFormTitle.ForeColor = Color.White;

                // 🚀 YENİ: Guna2TextBox'lar için tema
                txtKullaniciAdi.FillColor = Color.FromArgb(45, 45, 45);
                txtKullaniciAdi.ForeColor = Color.White;
                txtKullaniciAdi.BorderColor = Color.FromArgb(60, 60, 60);
                txtKullaniciAdi.PlaceholderForeColor = Color.Gray;

                txtSifre.FillColor = Color.FromArgb(45, 45, 45);
                txtSifre.ForeColor = Color.White;
                txtSifre.BorderColor = Color.FromArgb(60, 60, 60);
                txtSifre.PlaceholderForeColor = Color.Gray;

                // Butonlar
                btnGirisYap.BackColor = Color.FromArgb(75, 110, 175);
                btnGirisYap.ForeColor = Color.White;

                // 🚀 YENİ: Kayıt Ol butonu - dark mode
                btnKayitOl.BackColor = Color.FromArgb(30, 30, 30);
                btnKayitOl.ForeColor = Color.FromArgb(75, 110, 175);
                btnKayitOl.FlatAppearance.BorderColor = Color.FromArgb(75, 110, 175);
                btnKayitOl.FlatAppearance.MouseOverBackColor = Color.FromArgb(45, 45, 45);

                btnSifreDegistir.BackColor = Color.FromArgb(75, 110, 175);
                btnSifreDegistir.ForeColor = Color.White;

                btnTheme.BackColor = Color.FromArgb(45, 45, 45);
                btnTheme.ForeColor = Color.White;
                btnTheme.Text = "☀️";
                toolTip.SetToolTip(btnTheme, "Açık Temaya Geç");

                closeButton.BackColor = Color.FromArgb(24, 24, 24);
                closeButton.ForeColor = Color.White;

                minimizeButton.BackColor = Color.FromArgb(24, 24, 24);
                minimizeButton.ForeColor = Color.White;

                // Checkbox ve linkler
                hatirla.ForeColor = Color.LightGray;
                goster.ForeColor = Color.LightGray;
                forgotPasswordLink.LinkColor = Color.DeepSkyBlue;
                forgotPasswordLink.ActiveLinkColor = Color.White;

                // Diğer kontrolller
                statusLabel.ForeColor = Color.Silver;
                loginProgressBar.BackColor = Color.FromArgb(40, 40, 40);

                // Dil butonu
                languageButton.BackColor = Color.FromArgb(45, 45, 45);
                languageButton.ForeColor = Color.White;
            }
            else
            {
                // Ana form
                this.BackColor = Color.FromArgb(245, 247, 250);

                // Login panel
                loginPanel.BackColor = Color.White;
                loginPanel.ForeColor = Color.Black;

                // Başlık çubuğu
                titleBar.BackColor = Color.FromArgb(0, 123, 255);
                lblFormTitle.ForeColor = Color.White;

                // 🚀 YENİ: Guna2TextBox'lar için tema
                txtKullaniciAdi.FillColor = Color.FromArgb(248, 249, 250);
                txtKullaniciAdi.ForeColor = Color.Black;
                txtKullaniciAdi.BorderColor = Color.FromArgb(213, 218, 223);
                txtKullaniciAdi.PlaceholderForeColor = Color.Gray;

                txtSifre.FillColor = Color.FromArgb(248, 249, 250);
                txtSifre.ForeColor = Color.Black;
                txtSifre.BorderColor = Color.FromArgb(213, 218, 223);
                txtSifre.PlaceholderForeColor = Color.Gray;

                // Butonlar
                btnGirisYap.BackColor = Color.FromArgb(0, 123, 255);
                btnGirisYap.ForeColor = Color.White;

                // 🚀 YENİ: Kayıt Ol butonu - light mode
                btnKayitOl.BackColor = Color.Transparent;
                btnKayitOl.ForeColor = Color.FromArgb(0, 123, 255);
                btnKayitOl.FlatAppearance.BorderColor = Color.FromArgb(0, 123, 255);
                btnKayitOl.FlatAppearance.MouseOverBackColor = Color.FromArgb(230, 240, 255);

                btnSifreDegistir.BackColor = Color.FromArgb(108, 117, 125);
                btnSifreDegistir.ForeColor = Color.White;

                btnTheme.BackColor = Color.FromArgb(248, 249, 250);
                btnTheme.ForeColor = Color.Black;
                btnTheme.Text = "🌙";
                toolTip.SetToolTip(btnTheme, "Koyu Temaya Geç");

                closeButton.BackColor = Color.FromArgb(0, 123, 255);
                closeButton.ForeColor = Color.White;

                minimizeButton.BackColor = Color.FromArgb(0, 123, 255);
                minimizeButton.ForeColor = Color.White;

                // Checkbox ve linkler
                hatirla.ForeColor = Color.FromArgb(80, 80, 80);
                goster.ForeColor = Color.FromArgb(80, 80, 80);
                forgotPasswordLink.LinkColor = Color.FromArgb(0, 123, 255);
                forgotPasswordLink.ActiveLinkColor = Color.FromArgb(0, 86, 179);

                // Diğer kontrolller
                statusLabel.ForeColor = Color.FromArgb(108, 117, 125);
                loginProgressBar.BackColor = Color.FromArgb(240, 240, 240);

                // Dil butonu
                languageButton.BackColor = Color.FromArgb(248, 249, 250);
                languageButton.ForeColor = Color.Black;
            }

            // Logonun temalara göre değişmesi
            try
            {
                if (Properties.Resources.ResourceManager.GetObject("logo_dark") != null &&
                    Properties.Resources.ResourceManager.GetObject("logo_light") != null)
                {
                    logoPictureBox.Image = darkMode
                        ? (Image)Properties.Resources.ResourceManager.GetObject("logo_dark")
                        : (Image)Properties.Resources.ResourceManager.GetObject("logo_light");
                }
            }
            catch { /* Logo kaynakları olmayabilir */ }
        }

        public void languageButton_Click(object sender, EventArgs e)
        {
            contextMenuLanguage.Show(languageButton, new Point(0, languageButton.Height));
        }

        public void menuItemTurkce_Click(object sender, EventArgs e)
        {
            LanguageManager.Instance.LoadLanguage("tr-TR");
            LanguageManager.Instance.ApplyLanguage(this);
            languageButton.Text = "🇹🇷";
            statusLabel.Text = LanguageManager.Instance.GetMessage("loginInfo");
        }

        public void menuItemEnglish_Click(object sender, EventArgs e)
        {
            LanguageManager.Instance.LoadLanguage("en-US");
            LanguageManager.Instance.ApplyLanguage(this);
            languageButton.Text = "🇺🇸";
            statusLabel.Text = LanguageManager.Instance.GetMessage("loginInfo");
        }

        public void forgotPasswordLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show(
                LanguageManager.Instance.GetMessage("contactAdminMessage"),
                LanguageManager.Instance.GetMessage("forgotPasswordTitle"),
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            // Başlangıç mesajını ayarla
            statusLabel.Text = LanguageManager.Instance.GetMessage("loginInfo");

            // Dil ayarına göre dil butonunu güncelle
            string savedLanguage = LanguageManager.Instance.GetSavedLanguage();
            if (savedLanguage == "tr-TR")
                languageButton.Text = "🇹🇷";
            else if (savedLanguage == "en-US")
                languageButton.Text = "🇺🇸";
            else
                languageButton.Text = "🌐";
        }

        // Form taşıma için başlık çubuğu kodları
        public bool isDragging = false;
        public Point dragStartPoint;

        public void titleBar_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            dragStartPoint = new Point(e.X, e.Y);
        }

        public void titleBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point diff = new Point(e.X - dragStartPoint.X, e.Y - dragStartPoint.Y);
                this.Location = new Point(this.Location.X + diff.X, this.Location.Y + diff.Y);
            }
        }

        public void titleBar_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        public void closeButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void minimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}