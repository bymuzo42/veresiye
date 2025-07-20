using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Veresiye2025
{
    public partial class FormFirstSetup : Form
    {
        // Form taşıma için gerekli API çağrıları
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        // Yuvarlatılmış köşeler için gerekli API çağrısı
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        public static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect,
            int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        private int currentStep = 1;
        private const int totalSteps = 3;
        private Panel userSetupPanel;
        private Panel companySetupPanel;
        private Panel completionPanel;

        // User setup controls
        private Guna.UI2.WinForms.Guna2TextBox txtAdSoyad;
        private Guna.UI2.WinForms.Guna2TextBox txtKullaniciAdi;
        private Guna.UI2.WinForms.Guna2TextBox txtSifre;
        private Guna.UI2.WinForms.Guna2TextBox txtEposta;
        private Label lblPasswordStrength;

        // Company setup (Form3'ten alınacak)
        private Form3 companyForm;

        public FormFirstSetup()
        {
            InitializeComponent();
            InitializeWizardPanels();
            ApplyRoundedCorners();
        }

        private void FormFirstSetup_Load(object sender, EventArgs e)
        {
            ShowStep(1);

            // ESC tuşu ile kapatma
            this.KeyPreview = true;
            this.KeyDown += (s, ev) => {
                if (ev.KeyCode == Keys.Escape)
                {
                    btnCancel_Click(null, null);
                }
            };
        }

        private void ApplyRoundedCorners()
        {
            // Form köşelerini yuvarla (15px radius)
            Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 15, 15));

            // Panel köşelerini yuvarla (10px radius)
            mainPanel.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, mainPanel.Width, mainPanel.Height, 10, 10));
        }

        private void InitializeWizardPanels()
        {
            // User Setup Panel (Step 2)
            InitializeUserSetupPanel();

            // Company Setup Panel (Step 3) - Form3'ü embed edeceğiz
            InitializeCompanySetupPanel();

            // Completion Panel (Step 4)
            InitializeCompletionPanel();
        }

        private void InitializeUserSetupPanel()
        {
            userSetupPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(40, 20, 40, 20),
                Visible = false
            };

            // Başlık
            Label titleLabel = new Label
            {
                Text = "👤 Yönetici Hesabı Oluşturun",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 123, 255),
                Dock = DockStyle.Top,
                Height = 50,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Form alanları
            Panel fieldsPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(60, 20, 60, 20)
            };

            // Ad Soyad
            Label lblAdSoyad = new Label
            {
                Text = "Ad Soyad:",
                Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold),
                Location = new Point(0, 20),
                Size = new Size(100, 25)
            };

            txtAdSoyad = new Guna.UI2.WinForms.Guna2TextBox
            {
                Location = new Point(120, 15),
                Size = new Size(280, 36),
                BorderRadius = 5,
                PlaceholderText = "Adınız ve soyadınız"
            };

            // Kullanıcı Adı
            Label lblKullaniciAdi = new Label
            {
                Text = "Kullanıcı Adı:",
                Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold),
                Location = new Point(0, 70),
                Size = new Size(100, 25)
            };

            txtKullaniciAdi = new Guna.UI2.WinForms.Guna2TextBox
            {
                Location = new Point(120, 65),
                Size = new Size(280, 36),
                BorderRadius = 5,
                PlaceholderText = "Kullanıcı adınız"
            };

            // Şifre
            Label lblSifre = new Label
            {
                Text = "Şifre:",
                Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold),
                Location = new Point(0, 120),
                Size = new Size(100, 25)
            };

            txtSifre = new Guna.UI2.WinForms.Guna2TextBox
            {
                Location = new Point(120, 115),
                Size = new Size(280, 36),
                BorderRadius = 5,
                PlaceholderText = "Güçlü bir şifre seçin",
                PasswordChar = '•'
            };

            // E-posta
            Label lblEposta = new Label
            {
                Text = "E-posta:",
                Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold),
                Location = new Point(0, 170),
                Size = new Size(100, 25)
            };

            txtEposta = new Guna.UI2.WinForms.Guna2TextBox
            {
                Location = new Point(120, 165),
                Size = new Size(280, 36),
                BorderRadius = 5,
                PlaceholderText = "email@ornek.com (opsiyonel)"
            };

            // Şifre güçlülük göstergesi
            lblPasswordStrength = new Label
            {
                Location = new Point(120, 210),
                Size = new Size(280, 25),
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.Gray,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Event handlers
            txtSifre.TextChanged += (s, e) => ShowPasswordStrength(txtSifre.Text);

            // Kontrolleri ekle
            fieldsPanel.Controls.AddRange(new Control[] {
                lblAdSoyad, txtAdSoyad,
                lblKullaniciAdi, txtKullaniciAdi,
                lblSifre, txtSifre,
                lblEposta, txtEposta,
                lblPasswordStrength
            });

            userSetupPanel.Controls.Add(titleLabel);
            userSetupPanel.Controls.Add(fieldsPanel);
            mainPanel.Controls.Add(userSetupPanel);
        }

        private void InitializeCompanySetupPanel()
        {
            companySetupPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Visible = false
            };

            // Form3'ü embed edeceğiz
            companyForm = new Form3();
            companyForm.TopLevel = false;
            companyForm.FormBorderStyle = FormBorderStyle.None;
            companyForm.Dock = DockStyle.Fill;

            companySetupPanel.Controls.Add(companyForm);
            mainPanel.Controls.Add(companySetupPanel);
        }

        private void InitializeCompletionPanel()
        {
            completionPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(40, 20, 40, 20),
                Visible = false
            };

            // Başarı ikonu
            Label successIcon = new Label
            {
                Text = "✅",
                Font = new Font("Segoe UI", 48F),
                Dock = DockStyle.Top,
                Height = 80,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Başlık
            Label titleLabel = new Label
            {
                Text = "Kurulum Tamamlandı!",
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 123, 255),
                Dock = DockStyle.Top,
                Height = 50,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Açıklama
            Label descLabel = new Label
            {
                Text = "🎉 Tebrikler! Veresiye2025 kurulumu başarıyla tamamlandı.\n\n" +
                       "Artık programı kullanmaya başlayabilirsiniz:\n" +
                       "• Cari hesapları yönetebilirsiniz\n" +
                       "• Borç ve alacak takibi yapabilirsiniz\n" +
                       "• Raporlar oluşturabilirsiniz\n" +
                       "• Emanet takibi yapabilirsiniz\n\n" +
                       "İyi kullanımlar!",
                Font = new Font("Segoe UI", 11F),
                ForeColor = Color.FromArgb(80, 80, 80),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };

            completionPanel.Controls.Add(successIcon);
            completionPanel.Controls.Add(titleLabel);
            completionPanel.Controls.Add(descLabel);
            mainPanel.Controls.Add(completionPanel);
        }

        private void ShowPasswordStrength(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                lblPasswordStrength.Text = "";
                return;
            }

            int strength = 0;
            if (password.Length >= 6) strength++;
            if (password.Any(char.IsDigit)) strength++;
            if (password.Any(char.IsUpper)) strength++;
            if (password.Any(char.IsLower)) strength++;
            if (password.Any(c => "!@#$%^&*()".Contains(c))) strength++;

            switch (strength)
            {
                case 0:
                case 1:
                    lblPasswordStrength.Text = "Şifre Gücü: Çok Zayıf";
                    lblPasswordStrength.ForeColor = Color.Red;
                    break;
                case 2:
                    lblPasswordStrength.Text = "Şifre Gücü: Zayıf";
                    lblPasswordStrength.ForeColor = Color.Orange;
                    break;
                case 3:
                    lblPasswordStrength.Text = "Şifre Gücü: Orta";
                    lblPasswordStrength.ForeColor = Color.Goldenrod;
                    break;
                case 4:
                    lblPasswordStrength.Text = "Şifre Gücü: Güçlü";
                    lblPasswordStrength.ForeColor = Color.LimeGreen;
                    break;
                case 5:
                    lblPasswordStrength.Text = "Şifre Gücü: Çok Güçlü";
                    lblPasswordStrength.ForeColor = Color.Green;
                    break;
            }
        }

        private void ShowStep(int step)
        {
            currentStep = step;
            progressBar.Value = step;

            // Tüm panelleri gizle
            welcomePanel.Visible = false;
            userSetupPanel.Visible = false;
            companySetupPanel.Visible = false;
            completionPanel.Visible = false;

            switch (step)
            {
                case 1:
                    stepLabel.Text = "Adım 1/3: Hoş Geldiniz";
                    welcomePanel.Visible = true;
                    btnBack.Enabled = false;
                    btnNext.Text = "Başla";
                    break;

                case 2:
                    stepLabel.Text = "Adım 2/3: Yönetici Hesabı";
                    userSetupPanel.Visible = true;
                    btnBack.Enabled = true;
                    btnNext.Text = "İleri";
                    txtAdSoyad.Focus();
                    break;

                case 3:
                    stepLabel.Text = "Adım 3/3: Firma Bilgileri";
                    companySetupPanel.Visible = true;
                    companyForm.Show();
                    btnBack.Enabled = true;
                    btnNext.Text = "Tamamla";
                    break;

                case 4:
                    stepLabel.Text = "Kurulum Tamamlandı";
                    completionPanel.Visible = true;
                    btnBack.Enabled = false;
                    btnNext.Text = "Programa Başla";
                    btnCancel.Visible = false;
                    break;
            }
        }

        private bool ValidateCurrentStep()
        {
            switch (currentStep)
            {
                case 1:
                    return true; // Hoş geldin ekranı, doğrulama gerektirmez

                case 2:
                    return ValidateUserSetup();

                case 3:
                    return ValidateCompanySetup();

                case 4:
                    return true; // Tamamlama ekranı
            }
            return false;
        }

        private bool ValidateUserSetup()
        {
            if (string.IsNullOrWhiteSpace(txtAdSoyad.Text))
            {
                MessageBox.Show("Ad Soyad alanı boş olamaz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAdSoyad.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtKullaniciAdi.Text) || txtKullaniciAdi.Text.Length < 3)
            {
                MessageBox.Show("Kullanıcı adı en az 3 karakter olmalıdır!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtKullaniciAdi.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtSifre.Text) || txtSifre.Text.Length < 6)
            {
                MessageBox.Show("Şifre en az 6 karakter olmalıdır!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSifre.Focus();
                return false;
            }

            if (!txtSifre.Text.Any(char.IsDigit))
            {
                MessageBox.Show("Şifre en az 1 sayı içermelidir!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSifre.Focus();
                return false;
            }

            return true;
        }

        private bool ValidateCompanySetup()
        {
            // Form3'ün validation metodunu kullanacağız
            // Form3'te public bir validation metodu olması gerekir
            return true; // Şimdilik true döndürüyoruz
        }

        private void SaveUserToDatabase()
        {
            try
            {
                string sifreHash = Helpers.HashPassword(txtSifre.Text);
                string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";

                using (var connection = new System.Data.SQLite.SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Yönetim tablosuna kaydet (Form1 ile uyumlu)
                    string query = @"INSERT INTO Yonetim (KullaniciAdi, Sifre) VALUES (@KullaniciAdi, @Sifre)";

                    using (var cmd = new System.Data.SQLite.SQLiteCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@KullaniciAdi", txtKullaniciAdi.Text.Trim());
                        cmd.Parameters.AddWithValue("@Sifre", sifreHash);
                        cmd.ExecuteNonQuery();
                    }

                    // Users tablosuna da kaydet (genişletilmiş bilgilerle)
                    string userQuery = @"INSERT INTO Users (KullaniciAdi, Sifre, AdSoyad, Eposta) 
                                        VALUES (@KullaniciAdi, @Sifre, @AdSoyad, @Eposta)";

                    using (var cmd = new System.Data.SQLite.SQLiteCommand(userQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@KullaniciAdi", txtKullaniciAdi.Text.Trim());
                        cmd.Parameters.AddWithValue("@Sifre", sifreHash);
                        cmd.Parameters.AddWithValue("@AdSoyad", txtAdSoyad.Text.Trim());
                        cmd.Parameters.AddWithValue("@Eposta", txtEposta.Text.Trim());
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kullanıcı kaydedilirken hata oluştu: {ex.Message}",
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (!ValidateCurrentStep())
                return;

            if (currentStep < totalSteps)
            {
                if (currentStep == 2)
                {
                    // Kullanıcı bilgilerini kaydet
                    try
                    {
                        SaveUserToDatabase();
                    }
                    catch
                    {
                        return; // Hata varsa ilerlemez
                    }
                }

                ShowStep(currentStep + 1);
            }
            else if (currentStep == totalSteps)
            {
                // Firma kaydetme (Form3'ten)
                try
                {
                    // companyForm.btnfirmakayit.PerformClick(); // Form3'ün kaydet butonunu tetikle
                    ShowStep(4); // Tamamlama ekranına geç
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Firma kaydedilirken hata oluştu: {ex.Message}",
                        "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Kurulum tamamlandı - programa başla
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (currentStep > 1)
            {
                ShowStep(currentStep - 1);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Kurulumu iptal etmek istediğinizden emin misiniz?\nProgram kapatılacaktır.",
                "Kurulum İptal", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            btnCancel_Click(sender, e);
        }

        // Form taşıma için başlık çubuğu olayları
        private void titleBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, 0xA1, 0x2, 0);
            }
        }

        private void setupTimer_Tick(object sender, EventArgs e)
        {
            // Animasyon efektleri için kullanılabilir
        }

        // Form yeniden boyutlandırıldığında köşeleri güncelle
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (this.Region != null)
            {
                this.Region.Dispose();
            }
            ApplyRoundedCorners();
        }
    }
}