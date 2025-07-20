using System;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Linq;
using System.Threading.Tasks;
using System.IO;


namespace Veresiye2025
{
    public partial class Form2 : Form
    {
        // Form taşıma için gerekli DLL import
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        public extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        public extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        // Yuvarlatılmış köşeler için API
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        public static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );

        public bool isDarkMode = false;

        public Form2()
        {
            InitializeComponent();

            // Form köşelerini yuvarla (20px)
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

            // Header panel için olay ekle (form taşıma için)
            titleBar.MouseDown += titleBar_MouseDown;
            lblFormTitle.MouseDown += titleBar_MouseDown;
        }

        // Form yüklendiğinde çalışacak olan olay
        public void Form2_Load(object sender, EventArgs e)
        {
            // Tema ayarını al (Form1'den gelebilir)
            ApplyTheme(isDarkMode);

            // Validasyon ve klavye kısayolları
            ConfigureValidations();
            ConfigureKeyboardShortcuts();

            // Status mesajını ayarla
            statusLabel.Text = "Tüm bilgileri eksiksiz doldurun";
            lblPasswordStrength.Text = "";
        }

        private void ConfigureValidations()
        {
            // Gerçek zamanlı validasyon - kullanıcı yazarken
            txtAdSoyad.TextChanged += (s, e) => {
                if (string.IsNullOrWhiteSpace(txtAdSoyad.Text))
                {
                    txtAdSoyad.FillColor = Color.FromArgb(255, 236, 236);
                    statusLabel.Text = "Ad Soyad alanı gereklidir";
                    statusLabel.ForeColor = Color.Crimson;
                }
                else
                {
                    txtAdSoyad.FillColor = Color.FromArgb(248, 249, 250);
                    UpdateStatusMessage();
                }
            };

            txtKullaniciAdi.TextChanged += (s, e) => {
                if (txtKullaniciAdi.Text.Length < 3)
                {
                    txtKullaniciAdi.FillColor = Color.FromArgb(255, 236, 236);
                    statusLabel.Text = "Kullanıcı adı en az 3 karakter olmalıdır";
                    statusLabel.ForeColor = Color.Crimson;
                }
                else
                {
                    txtKullaniciAdi.FillColor = Color.FromArgb(248, 249, 250);
                    UpdateStatusMessage();
                }
            };

            txtSifre.TextChanged += (s, e) => {
                ShowPasswordStrength(txtSifre.Text);
                if (!IsPasswordStrong(txtSifre.Text))
                {
                    txtSifre.FillColor = Color.FromArgb(255, 236, 236);
                }
                else
                {
                    txtSifre.FillColor = Color.FromArgb(248, 249, 250);
                    UpdateStatusMessage();
                }
            };

            txtEposta.TextChanged += (s, e) => {
                if (!string.IsNullOrEmpty(txtEposta.Text) && !IsValidEmail(txtEposta.Text))
                {
                    txtEposta.FillColor = Color.FromArgb(255, 236, 236);
                    statusLabel.Text = "Geçerli bir e-posta adresi giriniz";
                    statusLabel.ForeColor = Color.Crimson;
                }
                else
                {
                    txtEposta.FillColor = Color.FromArgb(248, 249, 250);
                    UpdateStatusMessage();
                }
            };

            // Karakter kısıtlamaları
            txtAdSoyad.KeyPress += (s, evt) => {
                if (!char.IsControl(evt.KeyChar) &&
                    !char.IsLetter(evt.KeyChar) &&
                    evt.KeyChar != ' ')
                {
                    evt.Handled = true;
                }
            };

            txtKullaniciAdi.KeyPress += (s, evt) => {
                if (!char.IsControl(evt.KeyChar) &&
                    !char.IsLetterOrDigit(evt.KeyChar) &&
                    evt.KeyChar != '_')
                {
                    evt.Handled = true;
                }
            };
        }

        private void ConfigureKeyboardShortcuts()
        {
            this.KeyPreview = true;
            this.KeyDown += (s, e) => {
                if (e.KeyCode == Keys.Enter)
                {
                    // Hangi alanda olduğumuza göre ilerle
                    if (txtAdSoyad.Focused)
                        txtKullaniciAdi.Focus();
                    else if (txtKullaniciAdi.Focused)
                        txtSifre.Focus();
                    else if (txtSifre.Focused)
                        txtEposta.Focus();
                    else if (txtEposta.Focused)
                        kayitolkaydet.PerformClick();
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    this.Close();
                }
            };
        }

        private void UpdateStatusMessage()
        {
            if (ValidateAllFields())
            {
                statusLabel.Text = "Tüm alanlar geçerli - Kayıt yapabilirsiniz";
                statusLabel.ForeColor = Color.MediumSeaGreen;
            }
            else
            {
                statusLabel.Text = "Lütfen tüm alanları doğru doldurun";
                statusLabel.ForeColor = Color.FromArgb(100, 100, 100);
            }
        }

        private bool ValidateAllFields()
        {
            return !string.IsNullOrWhiteSpace(txtAdSoyad.Text) &&
                   txtKullaniciAdi.Text.Length >= 3 &&
                   IsPasswordStrong(txtSifre.Text) &&
                   (string.IsNullOrEmpty(txtEposta.Text) || IsValidEmail(txtEposta.Text));
        }

        // Şifre güçlülük göstergesi
        private void ShowPasswordStrength(string password)
        {
            int strength = 0;
            string strengthText = "";
            Color strengthColor = Color.Red;

            if (string.IsNullOrEmpty(password))
            {
                lblPasswordStrength.Text = "";
                return;
            }

            if (password.Length >= 6) strength++;
            if (password.Any(char.IsDigit)) strength++;
            if (password.Any(char.IsUpper)) strength++;
            if (password.Any(char.IsLower)) strength++;
            if (password.Any(c => "!@#$%^&*()".Contains(c))) strength++;

            switch (strength)
            {
                case 0:
                case 1:
                    strengthText = "Şifre: Çok Zayıf";
                    strengthColor = Color.Red;
                    break;
                case 2:
                    strengthText = "Şifre: Zayıf";
                    strengthColor = Color.Orange;
                    break;
                case 3:
                    strengthText = "Şifre: Orta";
                    strengthColor = Color.Goldenrod;
                    break;
                case 4:
                    strengthText = "Şifre: Güçlü";
                    strengthColor = Color.LimeGreen;
                    break;
                case 5:
                    strengthText = "Şifre: Çok Güçlü";
                    strengthColor = Color.Green;
                    break;
            }

            lblPasswordStrength.Text = strengthText;
            lblPasswordStrength.ForeColor = strengthColor;
        }

        // Tema değiştirme metodu
        public void ApplyTheme(bool darkMode)
        {
            if (darkMode)
            {
                // Ana form
                this.BackColor = Color.FromArgb(18, 18, 18);
                // Form paneli
                formPanel.BackColor = Color.FromArgb(30, 30, 30);
                // Başlık çubuğu
                titleBar.BackColor = Color.FromArgb(24, 24, 24);
                lblFormTitle.ForeColor = Color.White;
                // Status panel
                statusPanel.BackColor = Color.FromArgb(40, 40, 40);
                statusLabel.ForeColor = Color.LightGray;
                lblPasswordStrength.ForeColor = Color.LightGray;
                // TextBox'lar
                txtAdSoyad.FillColor = Color.FromArgb(45, 45, 45);
                txtAdSoyad.ForeColor = Color.White;
                txtKullaniciAdi.FillColor = Color.FromArgb(45, 45, 45);
                txtKullaniciAdi.ForeColor = Color.White;
                txtSifre.FillColor = Color.FromArgb(45, 45, 45);
                txtSifre.ForeColor = Color.White;
                txtEposta.FillColor = Color.FromArgb(45, 45, 45);
                txtEposta.ForeColor = Color.White;
                // Etiketler
                labelAdSoyad.ForeColor = Color.LightGray;
                labelKullaniciAdi.ForeColor = Color.LightGray;
                labelSifre.ForeColor = Color.LightGray;
                labelEposta.ForeColor = Color.LightGray;
                // Butonlar
                kayitolkaydet.FillColor = Color.FromArgb(75, 110, 175);
                vazgec.FillColor = Color.FromArgb(60, 65, 70);
            }
            else
            {
                // Ana form
                this.BackColor = Color.FromArgb(245, 247, 250);
                // Form paneli
                formPanel.BackColor = Color.White;
                // Başlık çubuğu
                titleBar.BackColor = Color.DodgerBlue;
                lblFormTitle.ForeColor = Color.White;
                // Status panel
                statusPanel.BackColor = Color.FromArgb(245, 247, 250);
                statusLabel.ForeColor = Color.FromArgb(100, 100, 100);
                // TextBox'lar
                txtAdSoyad.FillColor = Color.FromArgb(248, 249, 250);
                txtAdSoyad.ForeColor = Color.Black;
                txtKullaniciAdi.FillColor = Color.FromArgb(248, 249, 250);
                txtKullaniciAdi.ForeColor = Color.Black;
                txtSifre.FillColor = Color.FromArgb(248, 249, 250);
                txtSifre.ForeColor = Color.Black;
                txtEposta.FillColor = Color.FromArgb(248, 249, 250);
                txtEposta.ForeColor = Color.Black;
                // Etiketler
                labelAdSoyad.ForeColor = Color.FromArgb(60, 60, 60);
                labelKullaniciAdi.ForeColor = Color.FromArgb(60, 60, 60);
                labelSifre.ForeColor = Color.FromArgb(60, 60, 60);
                labelEposta.ForeColor = Color.FromArgb(60, 60, 60);
                // Butonlar
                kayitolkaydet.FillColor = Color.DodgerBlue;
                vazgec.FillColor = Color.FromArgb(108, 117, 125);
            }
        }

        // Async kayıt işlemi - iyileştirilmiş
        // Form2.cs - kayitolkaydet_Click metodunun güncellenmiş hali

        public async void kayitolkaydet_Click(object sender, EventArgs e)
        {
            // Validasyon
            if (!ValidateInputs())
                return;

            string kullaniciAdi = txtKullaniciAdi.Text.Trim();
            string eposta = txtEposta.Text.Trim();
            string adSoyad = txtAdSoyad.Text.Trim();

            // UI'ı güncelle - işlem başladı
            kayitolkaydet.Enabled = false;
            kayitolkaydet.Text = "Kaydediliyor...";
            statusLabel.Text = "Kayıt işlemi devam ediyor...";
            statusLabel.ForeColor = Color.RoyalBlue;

            try
            {
                // Duplicate kontrol (async)
                if (await CheckUserExistsAsync(kullaniciAdi, eposta))
                {
                    ShowValidationError("Bu kullanıcı adı veya e-posta zaten kayıtlı!");
                    return;
                }

                // Kayıt işlemi - ÇIFT TABLO
                string sifreHash = Helpers.HashPassword(txtSifre.Text);

                string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // 1. Users tablosuna kayıt (modern kullanıcı sistemi)
                            string userQuery = @"INSERT INTO Users (KullaniciAdi, Sifre, AdSoyad, Eposta)
                                        VALUES (@KullaniciAdi, @Sifre, @AdSoyad, @Eposta)";

                            using (SQLiteCommand userCmd = new SQLiteCommand(userQuery, connection, transaction))
                            {
                                userCmd.Parameters.AddWithValue("@KullaniciAdi", kullaniciAdi);
                                userCmd.Parameters.AddWithValue("@Sifre", sifreHash);
                                userCmd.Parameters.AddWithValue("@AdSoyad", adSoyad);
                                userCmd.Parameters.AddWithValue("@Eposta", eposta);

                                int userResult = userCmd.ExecuteNonQuery();

                                if (userResult <= 0)
                                {
                                    throw new Exception("Users tablosuna kayıt eklenemedi!");
                                }
                            }

                            // 2. Yonetim tablosuna da kayıt (eski sistem uyumluluğu için)
                            string yonetimQuery = @"INSERT INTO Yonetim (KullaniciAdi, Sifre)
                                           VALUES (@KullaniciAdi, @Sifre)";

                            using (SQLiteCommand yonetimCmd = new SQLiteCommand(yonetimQuery, connection, transaction))
                            {
                                yonetimCmd.Parameters.AddWithValue("@KullaniciAdi", kullaniciAdi);
                                yonetimCmd.Parameters.AddWithValue("@Sifre", sifreHash); // Hash'li şifre

                                int yonetimResult = yonetimCmd.ExecuteNonQuery();

                                if (yonetimResult <= 0)
                                {
                                    throw new Exception("Yonetim tablosuna kayıt eklenemedi!");
                                }
                            }

                            // 3. İşlemleri onayla
                            transaction.Commit();

                            // Başarı mesajı
                            statusLabel.Text = "Kayıt başarılı!";
                            statusLabel.ForeColor = Color.MediumSeaGreen;

                            // Başarılı kayıt sonrası işlemler
                            await Task.Delay(500); // Kullanıcıya mesajı göstermek için kısa bekleme

                            MessageBox.Show(
                                $"Kayıt başarılı!\n\n" +
                                $"Kullanıcı Adı: {kullaniciAdi}\n" +
                                $"Ad Soyad: {adSoyad}\n" +
                                $"E-posta: {eposta}\n\n" +
                                $"Şimdi giriş yapabilirsiniz.",
                                "Kayıt Tamamlandı",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                            );

                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        catch (Exception ex)
                        {
                            // Hata durumunda rollback
                            transaction.Rollback();
                            throw new Exception($"Kayıt işlemi sırasında hata: {ex.Message}");
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                string errorMessage = "Veritabanı hatası oluştu!";

                // SQLite hata kodlarına göre özel mesajlar
                if (ex.Message.Contains("UNIQUE"))
                {
                    errorMessage = "Bu kullanıcı adı zaten kullanılıyor!";
                }
                else if (ex.Message.Contains("constraint"))
                {
                    errorMessage = "Veri format hatası! Lütfen bilgileri kontrol edin.";
                }

                MessageBox.Show($"{errorMessage}\n\nDetay: {ex.Message}",
                    "Veritabanı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);

                statusLabel.Text = errorMessage;
                statusLabel.ForeColor = Color.Crimson;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Beklenmeyen hata oluştu!\n\n{ex.Message}",
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);

                statusLabel.Text = "Kayıt işlemi başarısız!";
                statusLabel.ForeColor = Color.Crimson;
            }
            finally
            {
                // UI'ı sıfırla
                kayitolkaydet.Enabled = true;
                kayitolkaydet.Text = "Kaydet";
            }
        }

        // Async duplicate kontrol metodunu güncelle
        private async Task<bool> CheckUserExistsAsync(string kullaniciAdi, string eposta)
        {
            return await Task.Run(() => {
                string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Hem Users hem de Yonetim tablosunda kontrol et
                    string checkQuery = @"
                SELECT COUNT(1) FROM (
                    SELECT KullaniciAdi FROM Users 
                    WHERE KullaniciAdi = @KullaniciAdi 
                    OR (Eposta = @Eposta AND @Eposta != '')
                    
                    UNION
                    
                    SELECT KullaniciAdi FROM Yonetim 
                    WHERE KullaniciAdi = @KullaniciAdi
                )";

                    using (SQLiteCommand checkCmd = new SQLiteCommand(checkQuery, connection))
                    {
                        checkCmd.Parameters.AddWithValue("@KullaniciAdi", kullaniciAdi);
                        checkCmd.Parameters.AddWithValue("@Eposta", eposta);

                        return Convert.ToInt32(checkCmd.ExecuteScalar()) > 0;
                    }
                }
            });
        }

        // Opsiyonel: Kayıt sonrası log tutma metodu
        private void LogUserRegistration(string kullaniciAdi, string adSoyad)
        {
            try
            {
                string logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Yeni kullanıcı kaydı: {kullaniciAdi} ({adSoyad})";

                // Log dosyasına yaz (opsiyonel)
                string logPath = Path.Combine(Application.StartupPath, "logs", "user_registrations.log");
                Directory.CreateDirectory(Path.GetDirectoryName(logPath));

                File.AppendAllText(logPath, logMessage + Environment.NewLine);
            }
            catch
            {
                // Log yazma hatası önemli değil, sessizce geç
            }
        }

        // Gelişmiş validasyon metodu
        private bool ValidateInputs()
        {
            // Boş alan kontrolü
            if (string.IsNullOrWhiteSpace(txtAdSoyad.Text))
            {
                ShowValidationError("Ad Soyad alanı boş olamaz!");
                txtAdSoyad.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtKullaniciAdi.Text))
            {
                ShowValidationError("Kullanıcı adı boş olamaz!");
                txtKullaniciAdi.Focus();
                return false;
            }

            // Kullanıcı adı uzunluk kontrolü
            if (txtKullaniciAdi.Text.Length < 3)
            {
                ShowValidationError("Kullanıcı adı en az 3 karakter olmalıdır!");
                txtKullaniciAdi.Focus();
                return false;
            }

            // Şifre güçlülük kontrolü
            if (!IsPasswordStrong(txtSifre.Text))
            {
                ShowValidationError("Şifre en az 6 karakter olmalı ve en az 1 sayı içermelidir!");
                txtSifre.Focus();
                return false;
            }

            // E-posta format kontrolü
            if (!string.IsNullOrWhiteSpace(txtEposta.Text) && !IsValidEmail(txtEposta.Text))
            {
                ShowValidationError("Geçerli bir e-posta adresi giriniz!");
                txtEposta.Focus();
                return false;
            }

            return true;
        }

        // Şifre güçlülük kontrolü
        private bool IsPasswordStrong(string password)
        {
            if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
                return false;

            // En az 1 sayı olmalı
            return password.Any(char.IsDigit);
        }

        // E-posta format kontrolü
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        // Hata mesajı gösterme
        private void ShowValidationError(string message)
        {
            MessageBox.Show(message, "Validation Hatası",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        // Vazgeç butonu olayı
        public void vazgec_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Kapat butonu olayı
        public void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Form taşıma için başlık çubuğu olayları
        public void titleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}