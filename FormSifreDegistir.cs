using System;
using System.Data.SQLite;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Timers;
using Guna.UI2.WinForms;

namespace Veresiye2025
{
    public partial class FormSifreDegistir : Form
    {
        public int hataliGirisSayisi;
        public const int maxHataliGiris = 3;
        public DateTime kilitlenmeZamani;
        public System.Timers.Timer kilitTimer;
        public const int maxSifreUzunlugu = 16;

        // Form köşelerini yuvarlatmak için
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        public static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );

        // Form taşıma için
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        public FormSifreDegistir()
        {
            InitializeComponent();

            // Form köşelerini yuvarlatma
            this.FormBorderStyle = FormBorderStyle.None;
            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 20, 20));

            // Form taşıma için olay ekle
            pnlHeader.MouseDown += PnlHeader_MouseDown;
            lblTitle.MouseDown += PnlHeader_MouseDown;

            // Form boyutu değiştiğinde köşeleri güncelle
            this.Resize += (s, e) => {
                this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 20, 20));
            };

            ApplyUIStyle();

            // Kayıtlı hatalı giriş sayısını ve kilit süresini oku
            hataliGirisSayisi = Properties.Settings.Default.HataliSifreGirisSayisi;
            kilitlenmeZamani = Properties.Settings.Default.SifreEngelZamani;
            KontrolSifreEngeli(); // Eğer kilitli ise formu açmayı engelle

            // Kaydedilen dili al
            try
            {
                string savedLanguage = LanguageManager.Instance.GetSavedLanguage();
                // Dil verilerini JSON dosyasından yükle ve UI'yi güncelle
                LanguageManager.Instance.LoadLanguage(savedLanguage);
                LanguageManager.Instance.ApplyLanguage(this);

                // Eğer FormGenelAyarlar açıksa, oradaki dilsec ComboBox'ına eriş ve dili değiştir
                FormGenelAyarlar genelAyarlarForm = Application.OpenForms["FormGenelAyarlar"] as FormGenelAyarlar;
                if (genelAyarlarForm != null)
                {
                    genelAyarlarForm.SetSelectedLanguage(savedLanguage);
                }
            }
            catch (Exception)
            {
                // Dil sistemi hatası olursa sessizce devam et
            }
        }

        // Form başlığı üzerinde mouse down olduğunda formu taşı
        public void PnlHeader_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        public void KontrolSifreEngeli()
        {
            if (hataliGirisSayisi >= maxHataliGiris && DateTime.Now < kilitlenmeZamani)
            {
                TimeSpan kalanSure = kilitlenmeZamani - DateTime.Now;
                MessageBox.Show($"Çok fazla yanlış giriş yaptınız! {kalanSure.Minutes} dakika {kalanSure.Seconds} saniye bekleyin.",
                                "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        public void ApplyUIStyle()
        {
            progressBarSifreGuclu.Visible = false;
            lblSifreDurumu.Visible = false;

            // Textbox Ayarları
            txtMevcutSifre.PasswordChar = '●';
            txtYeniSifre.PasswordChar = '●';
            txtYeniSifreTekrar.PasswordChar = '●';

            // Placeholder text ayarları
            txtMevcutSifre.PlaceholderText = "Mevcut şifrenizi girin";
            txtYeniSifre.PlaceholderText = "Yeni şifrenizi girin";
            txtYeniSifreTekrar.PlaceholderText = "Yeni şifrenizi tekrar girin";

            // "Şifre Göster" Checkbox
            chkShowPassword.CheckedChanged += (s, e) =>
            {
                bool showPassword = chkShowPassword.Checked;
                char passwordChar = showPassword ? '\0' : '●';
                txtMevcutSifre.PasswordChar = passwordChar;
                txtYeniSifre.PasswordChar = passwordChar;
                txtYeniSifreTekrar.PasswordChar = passwordChar;
            };

            // Şifre Gücü Ölçme
            txtYeniSifre.TextChanged += TxtYeniSifre_TextChanged;
            txtYeniSifre.KeyPress += SifreKarakterSiniri;
            txtYeniSifreTekrar.KeyPress += SifreKarakterSiniri;
        }

        public void btnKaydet_Click(object sender, EventArgs e)
        {
            // Null kontrolü: Eğer herhangi bir TextBox nesnesi null ise, işlem yapma
            if (txtMevcutSifre == null || txtYeniSifre == null || txtYeniSifreTekrar == null)
            {
                MessageBox.Show("Formdaki alanlar yüklenemedi.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (hataliGirisSayisi >= maxHataliGiris && DateTime.Now < kilitlenmeZamani)
            {
                TimeSpan kalanSure = kilitlenmeZamani - DateTime.Now;
                MessageBox.Show($"Çok fazla yanlış giriş yaptınız! {kalanSure.Minutes} dakika {kalanSure.Seconds} saniye bekleyin.",
                                "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string mevcutSifreHash = Helpers.HashPassword(txtMevcutSifre.Text);
            string yeniSifre = txtYeniSifre.Text;
            string yeniSifreTekrar = txtYeniSifreTekrar.Text;

            // Boş alan kontrolü
            if (string.IsNullOrWhiteSpace(txtMevcutSifre.Text) ||
                string.IsNullOrWhiteSpace(txtYeniSifre.Text) ||
                string.IsNullOrWhiteSpace(txtYeniSifreTekrar.Text))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Şifre uzunluğu kontrolü
            if (yeniSifre.Length < 4 || yeniSifre.Length > maxSifreUzunlugu)
            {
                MessageBox.Show($"Şifre en az 4, en fazla {maxSifreUzunlugu} karakter olmalıdır!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Yeni şifre eşleşme kontrolü
            if (yeniSifre != yeniSifreTekrar)
            {
                MessageBox.Show("Yeni şifreler eşleşmiyor!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // SQLite bağlantısı
            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT COUNT(1) FROM Yonetim WHERE KullaniciAdi = 'Admin' AND Sifre = @Sifre";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Sifre", mevcutSifreHash);
                        var result = cmd.ExecuteScalar();
                        if (Convert.ToInt32(result) == 0)
                        {
                            // Hatalı giriş
                            hataliGirisSayisi++;
                            Properties.Settings.Default.HataliSifreGirisSayisi = hataliGirisSayisi;
                            Properties.Settings.Default.Save();
                            if (hataliGirisSayisi >= maxHataliGiris)
                            {
                                kilitlenmeZamani = DateTime.Now.AddMinutes(10);
                                Properties.Settings.Default.SifreEngelZamani = kilitlenmeZamani;
                                Properties.Settings.Default.Save();
                                MessageBox.Show("Çok fazla yanlış giriş yaptınız! 10 dakika bekleyin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                MessageBox.Show($"Mevcut şifre yanlış! Kalan deneme hakkı: {maxHataliGiris - hataliGirisSayisi}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            return;
                        }
                    }

                    // Şifreyi Güncelleme
                    string updateQuery = "UPDATE Yonetim SET Sifre = @YeniSifre WHERE KullaniciAdi = 'Admin'";
                    using (SQLiteCommand updateCmd = new SQLiteCommand(updateQuery, connection))
                    {
                        string yeniSifreHash = Helpers.HashPassword(yeniSifre);
                        updateCmd.Parameters.AddWithValue("@YeniSifre", yeniSifreHash);
                        updateCmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Şifre başarıyla güncellendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Başarıyla değiştiyse hatalı giriş sayısını sıfırlama
                    Properties.Settings.Default.HataliSifreGirisSayisi = 0;
                    Properties.Settings.Default.Save();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void SifreKarakterSiniri(object sender, KeyPressEventArgs e)
        {
            Guna2TextBox txt = sender as Guna2TextBox;
            if (txt != null && txt.Text.Length >= maxSifreUzunlugu && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        public void TxtYeniSifre_TextChanged(object sender, EventArgs e)
        {
            string sifre = txtYeniSifre.Text;

            // Şifre boşsa ProgressBar'ı ve durumu gizle
            if (string.IsNullOrEmpty(sifre))
            {
                progressBarSifreGuclu.Visible = false;
                lblSifreDurumu.Visible = false;
                return;
            }

            // Şifre varsa ProgressBar'ı ve durumu göster
            progressBarSifreGuclu.Visible = true;
            lblSifreDurumu.Visible = true;

            int guc = 0;

            if (sifre.Length >= 4) guc += 30; // 4 karakter ve üstü
            if (sifre.Length >= 8) guc += 40; // 8 karakter ve üstü
            if (sifre.Any(char.IsDigit)) guc += 20; // Rakam içeriyorsa
            if (sifre.Any(ch => "!@#$%^&*()".Contains(ch))) guc += 10; // Özel karakter varsa ekstra puan

            // ProgressBar güncellemesi
            progressBarSifreGuclu.Value = Math.Min(guc, 100);

            // Renkleri belirleme
            Color renk;
            string mesaj;
            if (guc < 40)
            {
                renk = Color.Red;
                mesaj = "Zayıf Şifre";
            }
            else if (guc < 70)
            {
                renk = Color.Orange;
                mesaj = "Orta Seviye Şifre";
            }
            else
            {
                renk = Color.Green;
                mesaj = "Güçlü Şifre";
            }

            progressBarSifreGuclu.ProgressColor = renk;
            progressBarSifreGuclu.ProgressColor2 = renk;
            lblSifreDurumu.ForeColor = renk; // Label rengini ProgressBar ile aynı yap
            lblSifreDurumu.Text = mesaj;
        }

        public void FormSifreDegistir_Load(object sender, EventArgs e)
        {
            try
            {
                string savedLanguage = LanguageManager.Instance.GetSavedLanguage();
                LanguageManager.Instance.LoadLanguage(savedLanguage);
                LanguageManager.Instance.ApplyLanguage(this);

                // PlaceholderText değerlerini JSON'dan çekerek uygula
                txtMevcutSifre.PlaceholderText = LanguageManager.Instance.GetTranslation("FormSifreDegistir", "txtMevcutSifrePlaceholder");
                txtYeniSifre.PlaceholderText = LanguageManager.Instance.GetTranslation("FormSifreDegistir", "txtYeniSifrePlaceholder");
                txtYeniSifreTekrar.PlaceholderText = LanguageManager.Instance.GetTranslation("FormSifreDegistir", "txtYeniSifreTekrarPlaceholder");
                txtMevcutSifre.Refresh();
                txtYeniSifre.Refresh();
                txtYeniSifreTekrar.Refresh();
            }
            catch (Exception)
            {
                // Dil yapılandırması yüklenemezse varsayılan metinleri kullan
            }
        }

        public void btnVazgec_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}