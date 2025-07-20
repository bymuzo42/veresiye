using System;
using System.Data.SQLite;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Drawing;
using System.Net;
using System.Net.Mail;
using Microsoft.VisualBasic;
using System.Linq;
using Guna.UI2.WinForms;

namespace Veresiye2025
{
    public partial class FormGenelAyarlar : Form
    {
        public FormGenelAyarlar()
        {
            InitializeComponent();

            EPostaGondermeAyarlarıPanel();
            LoadEmailSettings(); // Form açıldığında e-posta ayarlarını yükle
            this.Load += FormGenelAyarlar_Load;
            LoadCariListesi();  // **Cari listesini yükleyelim**
            this.KeyDown += new KeyEventHandler(FormGenelAyarlar_KeyDown); // KeyDown olayını bağlıyoruz

        }

        public void FormGenelAyarlar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();  // ESC tuşuna basıldığında formu kapat
            }
        }


        public void FormGenelAyarlar_Load(object sender, EventArgs e)
        {
            this.genelayarlar.SelectedTab = this.eposta; // E-Posta sekmesini aç
        }


        // **📌 Form açıldığında e-posta ayarlarını yükle**
        public void LoadEmailSettings()
        {
            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT EmailHost, EmailPort, EmailUser, EmailPassword FROM Settings LIMIT 1";

                using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtSmtpHost.Text = reader["EmailHost"].ToString();
                            txtSmtpPort.Text = reader["EmailPort"].ToString();
                            txtEmailUser.Text = reader["EmailUser"].ToString();
                            txtEmailPassword.Text = DecryptPassword(reader["EmailPassword"].ToString());
                        }
                    }
                }
            }
        }


        // **📌 E-Posta Ayarlarını Kaydet Butonu**
        public void epostakaydet_Click(object sender, EventArgs e)
        {
            string smtpHost = txtSmtpHost.Text;
            int smtpPort;
            string emailUser = txtEmailUser.Text;
            string emailPassword = txtEmailPassword.Text;

            // Port numarasını integer'a çevir
            if (!int.TryParse(txtSmtpPort.Text, out smtpPort))
            {
                MessageBox.Show("Geçerli bir port numarası girin!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(smtpHost) || string.IsNullOrWhiteSpace(emailUser) || string.IsNullOrWhiteSpace(emailPassword))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";

            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                INSERT INTO Settings (EmailHost, EmailPort, EmailUser, EmailPassword) 
                VALUES (@EmailHost, @EmailPort, @EmailUser, @EmailPassword)
                ON CONFLICT(ID) DO UPDATE SET 
                    EmailHost = excluded.EmailHost, 
                    EmailPort = excluded.EmailPort, 
                    EmailUser = excluded.EmailUser, 
                    EmailPassword = excluded.EmailPassword;";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@EmailHost", smtpHost);
                        cmd.Parameters.AddWithValue("@EmailPort", smtpPort);
                        cmd.Parameters.AddWithValue("@EmailUser", emailUser);
                        cmd.Parameters.AddWithValue("@EmailPassword", EncryptPassword(emailPassword)); // Şifreyi şifrele

                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("E-posta ayarları başarıyla kaydedildi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ayarlar kaydedilirken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void SendEmail(string recipientEmail, string subject, string body)
        {
            try
            {
                string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
                string smtpHost = "";
                int smtpPort = 0;
                string emailUser = "";
                string emailPassword = "";

                // **📌 Veritabanından SMTP ayarlarını yükle**
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT EmailHost, EmailPort, EmailUser, EmailPassword FROM Settings LIMIT 1";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                    {
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                smtpHost = reader["EmailHost"].ToString();
                                smtpPort = Convert.ToInt32(reader["EmailPort"]);
                                emailUser = reader["EmailUser"].ToString();
                                emailPassword = DecryptPassword(reader["EmailPassword"].ToString());
                            }
                        }
                    }
                }

                // **📌 SMTP Ayarlarını Kullanarak E-Posta Gönderme**
                using (SmtpClient smtpClient = new SmtpClient(smtpHost, smtpPort))
                {
                    smtpClient.Credentials = new NetworkCredential(emailUser, emailPassword);
                    smtpClient.EnableSsl = false; // Çoğu SMTP sunucusu için gereklidir

                    using (MailMessage mail = new MailMessage())
                    {
                        mail.From = new MailAddress(emailUser);
                        mail.To.Add(recipientEmail);
                        mail.Subject = subject;
                        mail.Body = body;
                        mail.IsBodyHtml = true; // HTML formatında e-posta göndermek için

                        smtpClient.Send(mail);
                    }
                }

                MessageBox.Show("E-Posta başarıyla gönderildi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("E-Posta gönderilirken hata oluştu:\n" + ex.ToString(), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        public void epostagonder_Click(object sender, EventArgs e)
        {
            using (InputBoxForm inputBox = new InputBoxForm("E-Posta Gönder", "Göndermek istediğiniz e-posta adresini girin:"))
            {
                if (inputBox.ShowDialog() == DialogResult.OK)
                {
                    string recipientEmail = inputBox.UserInput;
                    if (!string.IsNullOrWhiteSpace(recipientEmail))
                    {
                        SendEmail(recipientEmail, "Test E-Postası", "Bu bir test e-postasıdır.");
                    }
                }
            }
        }



        // **📌 Şifreleme fonksiyonu**
        public string EncryptPassword(string password)
        {
            byte[] data = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(data);
        }

        // **📌 Şifreyi çözme fonksiyonu**
        public string DecryptPassword(string encryptedPassword)
        {
            byte[] data = Convert.FromBase64String(encryptedPassword);
            return Encoding.UTF8.GetString(data);
        }

        // **📌 Vazgeç Butonu - Pencereyi Kapat**
        public void ayarlarvazgec_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void TextBox_Enter(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (txt != null && txt.ForeColor == Color.Gray)
            {
                txt.Text = "";
                txt.ForeColor = Color.Black;
            }
        }

        public void TextBox_Leave(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (txt != null && string.IsNullOrWhiteSpace(txt.Text))
            {
                if (txt == txtSmtpHost) txt.Text = "SMTP Sunucusu";
                else if (txt == txtSmtpPort) txt.Text = "Port";
                else if (txt == txtEmailUser) txt.Text = "E-Posta Adresi";
                else if (txt == txtEmailPassword) txt.Text = "Şifre";

                txt.ForeColor = Color.Gray;
            }
        }

        // Şifre kutusunu özel ele al (yıldızlı giriş)
        public void PasswordBox_Enter(object sender, EventArgs e)
        {
            if (txtEmailPassword.ForeColor == Color.Gray)
            {
                txtEmailPassword.Text = "";
                txtEmailPassword.ForeColor = Color.Black;
                txtEmailPassword.UseSystemPasswordChar = true;
            }
        }

        public void PasswordBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmailPassword.Text))
            {
                txtEmailPassword.Text = "Şifre";
                txtEmailPassword.ForeColor = Color.Gray;
                txtEmailPassword.UseSystemPasswordChar = false;
            }
        }

        public void lblBanner_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.subeleri.com.tr");
        }

        public void LoadCariLimitAsanlar()
        {
            lstCariLimitAsanlar.Items.Clear();
            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // **Cari Limit Aşanları Çekme Sorgusu**
                string query = @"
            SELECT Unvani, Telefon, Gsm, 
                   CAST(CariLimit AS REAL) AS CariLimitReal, 
                   CAST(Bakiye AS REAL) AS BakiyeReal 
            FROM Cari 
            WHERE BakiyeReal >= CariLimitReal";

                using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string unvani = reader["Unvani"].ToString();
                            string telefon = reader["Telefon"].ToString();
                            string gsm = reader["Gsm"].ToString();
                            string limit = reader["CariLimitReal"].ToString();
                            string bakiye = reader["BakiyeReal"].ToString();

                            // **Eğer Gsm varsa onu al, yoksa Telefon'u al**
                            string telefonNumarasi = !string.IsNullOrWhiteSpace(gsm) ? gsm : telefon;

                            if (!string.IsNullOrWhiteSpace(telefonNumarasi))
                            {
                                lstCariLimitAsanlar.Items.Add($"{unvani} - {telefonNumarasi} | Limit: {limit}, Bakiye: {bakiye}");
                            }
                        }
                    }
                }
            }

            MessageBox.Show("Cari limiti aşanlar listeye çekildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        


        public void btnListedenCikar_Click(object sender, EventArgs e)
        {
            while (lstCariLimitAsanlar.SelectedItems.Count > 0)
            {
                lstCariLimitAsanlar.Items.Remove(lstCariLimitAsanlar.SelectedItem);
            }
        }

        public void btnSmsGonder_Click(object sender, EventArgs e)
        {
            if (lstCariLimitAsanlar.Items.Count == 0)
            {
                MessageBox.Show("Gönderilecek müşteri bulunamadı!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int smsGonderilenMusteriSayisi = 0;

            foreach (var item in lstCariLimitAsanlar.Items)
            {
                string itemStr = item.ToString();
                if (itemStr.Contains("-") && itemStr.Contains("|"))
                {
                    string[] data = itemStr.Split('-');
                    if (data.Length > 1)
                    {
                        string phoneNumber = data[1].Split('|')[0].Trim();

                        if (!string.IsNullOrWhiteSpace(phoneNumber) && phoneNumber.All(char.IsDigit))
                        {
                            SendSms(phoneNumber, "Sayın müşterimiz, cari limitinizi aştınız. Ödeme yapmanız gerekmektedir.");
                            smsGonderilenMusteriSayisi++;
                        }
                    }
                }
            }

            MessageBox.Show($"📢 {smsGonderilenMusteriSayisi} müşteriye SMS gönderildi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }



        public void SendSms(string phoneNumber, string message) //api yapılması gerek şuan olarak boş..
        {
            //Buraya SMS APİ yazılması Gerekiyor
        }

        

        

        public void btnCariLimitKontrol_Click(object sender, EventArgs e)
        {
            LoadCariLimitAsanlar();
        }

        public void LoadCariListesi()
        {
            cmbCariSec.Items.Clear();
            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";

            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    // **Bakiyesi 0'dan büyük olan carileri getiriyoruz**
                    string query = "SELECT Unvani, Telefon, Gsm, Bakiye FROM Cari WHERE CAST(Bakiye AS REAL) > 0";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                    {
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            int cariSayisi = 0;

                            while (reader.Read())
                            {
                                string unvani = reader["Unvani"].ToString();
                                string telefon = reader["Telefon"].ToString();
                                string gsm = reader["Gsm"].ToString();
                                string bakiye = reader["Bakiye"].ToString();

                                // **Eğer GSM varsa onu al, yoksa Telefon'u al**
                                string telefonNumarasi = !string.IsNullOrWhiteSpace(gsm) ? gsm : telefon;

                                if (!string.IsNullOrWhiteSpace(unvani) && !string.IsNullOrWhiteSpace(telefonNumarasi))
                                {
                                    cmbCariSec.Items.Add($"{unvani} - {telefonNumarasi} | Bakiye: {bakiye}");
                                    cariSayisi++;
                                }
                            }

                            //MessageBox.Show($"📢 {cariSayisi} bakiyesi olan cari yüklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        public void btnCariEkle_Click(object sender, EventArgs e)
        {
            if (cmbCariSec.SelectedItem == null)
            {
                MessageBox.Show("Lütfen bir cari seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // **Seçili cariyi alalım ve ayrıştıralım**
            string secilenCari = cmbCariSec.SelectedItem.ToString().Trim();

            // **Son "-" işaretine göre bölme işlemi**
            int sonTireIndex = secilenCari.LastIndexOf('-');
            if (sonTireIndex == -1)
            {
                MessageBox.Show("Cari formatı hatalı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string unvani = secilenCari.Substring(0, sonTireIndex).Trim();
            string telefonNumarasi = secilenCari.Substring(sonTireIndex + 1).Split('|')[0].Trim();

            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // **Doğru SQL sorgusu**
                string query = "SELECT CariLimit, Bakiye FROM Cari WHERE TRIM(Unvani) = @Unvani AND (TRIM(Telefon) = @Telefon OR TRIM(Gsm) = @Telefon)";

                using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Unvani", unvani);
                    cmd.Parameters.AddWithValue("@Telefon", telefonNumarasi);

                    

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string limit = reader["CariLimit"].ToString();
                            string bakiye = reader["Bakiye"].ToString();

                            // **ListBox'a cari bilgilerini ekleyelim**
                            string cariKaydi = $"{unvani} | {telefonNumarasi} | Limit: {limit}, Bakiye: {bakiye}";
                            lstCariLimitAsanlar.Items.Add(cariKaydi);
                        }
                        else
                        {
                            MessageBox.Show($"❌ {unvani} - {telefonNumarasi} için cari bilgisi bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }




        public void temizle_Click(object sender, EventArgs e)
        {
            this.lstCariLimitAsanlar.Items.Clear(); // **Tüm listeyi temizler**
        }

        public void dilikaydet_Click(object sender, EventArgs e)
        {
            // FormGenelAyarlar formunun örneğini al
            FormGenelAyarlar genelAyarlarForm = (FormGenelAyarlar)Application.OpenForms["FormGenelAyarlar"];

            if (genelAyarlarForm != null)
            {
                // ComboBox'tan seçilen dili al
                string selectedLanguage = genelAyarlarForm.dilsec.SelectedItem.ToString();

                // Dil verilerini JSON dosyasından oku ve UI'yı güncelle
                LanguageManager.Instance.LoadLanguage(selectedLanguage);
                LanguageManager.Instance.ApplyLanguage(this); // Form1 veya FormGenelAyarlar

                // Dil tercihini kaydet
                LanguageManager.Instance.SaveLanguagePreference(selectedLanguage);

                // ComboBox'ta doğru dilin seçili olduğunu tutmak için:
                genelAyarlarForm.dilsec.SelectedItem = selectedLanguage;

                // Mesaj göster
                MessageBox.Show($"Dil {selectedLanguage} olarak değiştirildi.", "Dil Değişti", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Diğer tüm formları kapat
                foreach (Form form in Application.OpenForms)
                {
                    // Ana formu (Form1) hariç kapat
                    if (form.Name != "Form1")
                    {
                        form.Close();
                    }
                }

                // Ana formu (Form1) yeniden aç
                Form1 form1 = new Form1();
                form1.Show();
            }
            else
            {
                MessageBox.Show("FormGenelAyarlar formuna erişilemedi.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void SetSelectedLanguage(string language)
        {
            if (dilsec.Items.Contains(language))
            {
                dilsec.SelectedItem = language;
            }
        }

        public void guna2ImageButton1_Click(object sender, EventArgs e)
        {

        }

        public void EPostaGondermeAyarlarıPanel()
        {
            // Yeni Paneli Temizle
            epostagunapanel.Controls.Clear();

            // Panelin sınır ve dolgu ayarları
            epostagunapanel.BorderRadius = 15;
            epostagunapanel.BorderColor = Color.Gray;
            epostagunapanel.BorderThickness = 1;
            epostagunapanel.FillColor = Color.FromArgb(245, 245, 245); // Panelin arka plan rengi

            // Başlık Label
            var lblTitle = new Guna2HtmlLabel
            {
                Text = "E-Posta Gönderme Ayarları",  // Başlık metni
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(220, 53, 69), // Kırmızımsı başlık
                Location = new Point(10, 10),  // Başlık konumu
                AutoSize = true
            };

            // Açıklama metni (SMTP ayarları ve hatalar)
            var lblNote = new Guna2HtmlLabel
            {
                Text = "<b><font color='blue'>SMTP AYARLARI:<br></font></b>" +
                       "- SMTP Sunucu: mail.subeleri.com.tr<br>" +
                       "- Port: 465 (SSL) veya 587 (TLS)<br>" +
                       "- Kimlik Doğrulama: Gerekli (E-posta ve Şifre)<br>" +
                       "<b><font color='blue'>E-POSTA GÖNDERMEK İÇİN:<br></font></b>" +
                       "- SMTP bilgilerinizi doğru girdiğinizden emin olun.<br>" +
                       "- Şifreniz doğru mu? Eğer hata alıyorsanız, hosting firmanızdan SMTP şifrenizi kontrol edin.<br>" +
                       "- Farklı port kombinasyonlarını deneyin (465 veya 587).<br>" +
                       "<b><font color='blue'>HATALAR VE ÇÖZÜMLERİ:<br></font></b>" +
                       "- 'Uzak ad çözülemedi' hatası alıyorsanız, SMTP sunucu adresini kontrol edin.<br>" +
                       "- 'Kimlik doğrulama hatası' alıyorsanız, doğru e-posta ve şifreyi girdiğinizden emin olun.",
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                ForeColor = Color.Black,
                Location = new Point(10, 40),
                MaximumSize = new Size(epostagunapanel.Width - 20, 0), // Panelin genişliğine göre kayma
                AutoSize = true
            };

            // Panelin içine eklemeler
            epostagunapanel.Controls.Add(lblTitle);
            epostagunapanel.Controls.Add(lblNote);
        }


    }
}
