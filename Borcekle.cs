using System;
using System.Data.SQLite;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Veresiye2025
{
    public partial class Borcekle : Form
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

        public Borcekle(string cariUnvan)
        {
            InitializeComponent();
            // Cari unvanını textBox1'e atar
            textBox1.Text = cariUnvan;
            textBox1.ReadOnly = true; // TextBox'ın düzenlenmesini engeller

            // Form köşelerini yuvarla ve form taşıma olayını ekle
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

            // Header panel için olay ekle (form taşıma için)
            pnlHeader.MouseDown += PnlHeader_MouseDown;
            lblFormTitle.MouseDown += PnlHeader_MouseDown;
        }

        // Form taşıma için olay
        public void PnlHeader_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        public void Borcekle_Load(object sender, EventArgs e)
        {
            // Para birimi formatını ayarla
            textBox3.Text = "0,00 TL";
            textBox3.TextAlign = HorizontalAlignment.Right;
            textBox3.ForeColor = Color.Black; // Sayısal kısım siyah
            textBox3.Font = new Font(textBox3.Font, FontStyle.Regular); // Sayısal kısım normal

            // Vade gün alanına sayısal kısıtlama
            vadegungir.KeyPress += VadeGungir_KeyPress; // Vade günü sayısal kontrol

            // Tarih ve saat ayarları
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;

            // Kaydedilen dili al
            string savedLanguage = LanguageManager.Instance.GetSavedLanguage();

            // Dil dosyasını yükle ve UI'yi güncelle
            LanguageManager.Instance.LoadLanguage(savedLanguage);
            LanguageManager.Instance.ApplyLanguage(this);

            // İlk alana odaklan
            textBox2.Focus(); // Açıklama alanına odaklan

            // ESC tuşu ile kapatma için
            this.KeyPreview = true;
            this.KeyDown += (s, ev) => {
                if (ev.KeyCode == Keys.Escape)
                    this.Close();
            };
        }

        // Vade gün alanına sadece sayı girilebilmesi için
        public void VadeGungir_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Sadece sayı ve kontrol karakterlerine izin ver
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Kullanıcı yalnızca sayı, nokta ve virgül girebilir; ondalık ayracın ardından iki hane sınırı
        public void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Rakam, nokta, virgül ve kontrol karakterleri dışındaki girişleri engeller
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != ',')
            {
                e.Handled = true;
            }

            // Virgül yerine nokta koymak için
            if (e.KeyChar == ',')
            {
                e.KeyChar = '.';  // Virgül yerine nokta kullan
            }

            // Ondalık ayracının tekrar girişini engelle
            if ((e.KeyChar == '.' || e.KeyChar == ',') && (textBox3.Text.Contains(".") || textBox3.Text.Contains(",")))
            {
                e.Handled = true;
            }

            // Nokta veya virgülden sonra en fazla iki haneye izin verir
            if (textBox3.Text.Contains(".") || textBox3.Text.Contains(","))
            {
                int decimalIndex = textBox3.Text.IndexOf('.') > -1 ? textBox3.Text.IndexOf('.') : textBox3.Text.IndexOf(',');
                string afterDecimal = textBox3.Text.Substring(decimalIndex + 1);

                // Ondalık işaretinden sonra iki hane varsa daha fazla giriş yapılmasına izin verilmez
                if (afterDecimal.Length >= 2 && textBox3.SelectionStart > decimalIndex)
                {
                    e.Handled = true;
                }
            }
        }

        // Kullanıcı kutudan ayrıldığında, değeri "0,00 TL" formatında göster
        public void textBox3_Leave(object sender, EventArgs e)
        {
            string textWithoutTL = textBox3.Text.Replace(" TL", "").Replace(",", "."); // Virgül yerine nokta
            if (decimal.TryParse(textWithoutTL, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal value))
            {
                textBox3.Text = value.ToString("0.00").Replace(".", ",") + " TL"; // Formatı düzenleyip " TL" ekliyoruz
                ApplyCurrencyFormat(); // Para birimi stilini uyguluyoruz
            }
            else
            {
                textBox3.Text = "0,00 TL"; // Eğer geçerli bir sayı yoksa, varsayılan değeri geri yazıyoruz
                ApplyCurrencyFormat();
            }
        }

        // Kullanıcı kutuya tıkladığında, "TL" ibaresini geçici olarak kaldırır
        public void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Text.EndsWith(" TL"))
            {
                textBox3.Text = textBox3.Text.Replace(" TL", "");  // TL kısmını kaldır
            }
            if (textBox3.Text == "0,00")
            {
                textBox3.Text = "";  // Varsayılan değeri temizliyoruz
            }
        }

        // TextChanged ile "TL" ekini korur ve stil uygular
        public void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (!textBox3.Text.EndsWith(" TL") && textBox3.Focused == false)
            {
                textBox3.Text += " TL";
                ApplyCurrencyFormat(); // Para birimi stilini uygula
                textBox3.SelectionStart = textBox3.Text.Length - 3;  // TL'nin sonuna gelirken, imleci doğru yere yerleştiriyoruz
            }
        }

        // Para birimi kısmını kırmızı ve kalın olarak ayarlayan metot
        public void ApplyCurrencyFormat()
        {
            textBox3.Font = new Font(textBox3.Font, FontStyle.Regular); // Sayısal kısmı normal
            int currencyIndex = textBox3.Text.IndexOf(" TL");
            if (currencyIndex > -1)
            {
                // Para birimi kısmını kırmızı ve kalın yapar
                textBox3.Select(currencyIndex, 3);
                textBox3.Select(textBox3.Text.Length, 0); // Metnin sonuna geri dön
            }
        }

        // KeyDown olayını textBox3'e ekleyin
        public void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            // Geri tuşuna basıldığında, TextBox'ta veri silinebilir
            if (e.KeyCode == Keys.Back)
            {
                // Eğer textBox3'te veri varsa, geri tuşu ile silme yapılabilir
                if (textBox3.SelectionStart > 0)
                {
                    textBox3.Text = textBox3.Text.Remove(textBox3.SelectionStart - 1, 1);
                    textBox3.SelectionStart = textBox3.Text.Length; // Sonraki karaktere odaklan
                    e.Handled = true;
                }
            }

            // Delete tuşuna basıldığında, TextBox'ta veri silinebilir
            if (e.KeyCode == Keys.Delete)
            {
                if (textBox3.SelectionStart < textBox3.Text.Length)
                {
                    textBox3.Text = textBox3.Text.Remove(textBox3.SelectionStart, 1);
                    textBox3.SelectionStart = textBox3.Text.Length; // Sonraki karaktere odaklan
                    e.Handled = true;
                }
            }
        }

        // "Vazgeç" butonuna tıklandığında formu kapatma işlemi
        public void vazgec_Click(object sender, EventArgs e)
        {
            this.Close(); // Formu kapat
        }

        // Alarm butonuna tıklandığında
        public void alarm_Click(object sender, EventArgs e)
        {
            string _cariUnvan = textBox1.Text;
            Alarmkur alarmKurFormu = new Alarmkur(_cariUnvan);
            alarmKurFormu.ShowDialog();
        }

        // Olay Tanımı
        public event Action<string, decimal> OnBakiyeGuncelle;

        public void kaydet_Click(object sender, EventArgs e)
        {
            // Borcu ekleyen kontrolü - boş bırakılamaz
            if (string.IsNullOrWhiteSpace(borcuekleyentextbox.Text))
            {
                MessageBox.Show("Borcu ekleyen kişi bilgisi boş bırakılamaz.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                borcuekleyentextbox.Focus();
                return;
            }

            // Vade gün kontrolü - boş bırakılamaz ve sayısal değer olmalı
            if (string.IsNullOrWhiteSpace(vadegungir.Text))
            {
                MessageBox.Show("Vade gün bilgisi boş bırakılamaz.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                vadegungir.Focus();
                return;
            }

            // Vade gün sayısal değer kontrolü
            if (!int.TryParse(vadegungir.Text, out int vadeGun))
            {
                MessageBox.Show("Vade gün bilgisi geçerli bir sayı olmalıdır.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                vadegungir.Focus();
                return;
            }

            string cariUnvani = textBox1.Text;
            string cariKodu = GetCariKoduFromUnvani(cariUnvani);
            string aciklama = textBox2.Text;
            string borcuEkleyen = borcuekleyentextbox.Text.Trim();
            string tutarText = textBox3.Text.Replace(" TL", "").Replace(",", ".");

            if (decimal.TryParse(tutarText, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal borcTutar) && borcTutar > 0)
            {
                try
                {
                    string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
                    using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                    {
                        connection.Open();

                        // ✅ Cari Limit ve Bakiye Değerlerini Al
                        decimal cariLimit = 0;
                        decimal mevcutBakiye = 0;
                        string limitQuery = "SELECT CariLimit, Bakiye FROM Cari WHERE CariKodu = @CariKodu";
                        using (SQLiteCommand limitCommand = new SQLiteCommand(limitQuery, connection))
                        {
                            limitCommand.Parameters.AddWithValue("@CariKodu", cariKodu);
                            using (SQLiteDataReader reader = limitCommand.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    cariLimit = reader["CariLimit"] != DBNull.Value ? Convert.ToDecimal(reader["CariLimit"]) : 0;
                                    mevcutBakiye = reader["Bakiye"] != DBNull.Value ? Convert.ToDecimal(reader["Bakiye"]) : 0;
                                }
                            }
                        }

                        // ✅ Kalan Borç Limiti
                        decimal kalanLimit = cariLimit - mevcutBakiye;

                        // ❌ Eğer eklenecek borç, kalan limiti aşarsa özel hata mesajını göster ve işlemi durdur
                        if (borcTutar > kalanLimit)
                        {
                            FormHataMesaji hataFormu = new FormHataMesaji(
                                "❌ Bu işlem yapılamaz!",
                                cariLimit,
                                mevcutBakiye,
                                borcTutar,
                                kalanLimit
                            );
                            hataFormu.ShowDialog();
                            return; // İşlemi durdur
                        }

                        // ✅ Borç kaydını ekleyelim - YENİ ALANLAR EKLENDİ
                        string query = @"
                            INSERT INTO cari_hareketleri (cari_kodu, tarih, tur, aciklama, borc, borcu_ekleyen, vade_gun)
                            VALUES (@CariKodu, @Tarih, @Tur, @Aciklama, @Borc, @BorcuEkleyen, @VadeGun)";

                        DateTime secilenTarih = dateTimePicker1.Value;
                        DateTime secilenSaat = dateTimePicker2.Value;

                        // Tarih ve saati birleştir
                        DateTime birlesikTarihSaat = new DateTime(
                            secilenTarih.Year,
                            secilenTarih.Month,
                            secilenTarih.Day,
                            secilenSaat.Hour,
                            secilenSaat.Minute,
                            secilenSaat.Second);

                        string formattedDate = birlesikTarihSaat.ToString("yyyy-MM-dd HH:mm:ss");
                        using (SQLiteCommand command = new SQLiteCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@CariKodu", cariKodu);
                            command.Parameters.AddWithValue("@Tarih", formattedDate);
                            command.Parameters.AddWithValue("@Tur", "Borç Dekontu");
                            command.Parameters.AddWithValue("@Aciklama", aciklama);
                            command.Parameters.AddWithValue("@Borc", borcTutar);
                            command.Parameters.AddWithValue("@BorcuEkleyen", borcuEkleyen);
                            command.Parameters.AddWithValue("@VadeGun", vadeGun);
                            command.ExecuteNonQuery();
                        }
                    }

                    // ✅ Pasif Hesap kontrolünü yap
                    FormCariEkle formCariEkle = new FormCariEkle();
                    formCariEkle.PasifHesapKontrol(cariKodu);
                    formCariEkle.GuncelleBakiye(cariKodu);

                    // ✅ Eğer Carihareketler formu açıksa güncelle
                    if (Application.OpenForms["Carihareketler"] is Carihareketler cariForm)
                    {
                        cariForm.UpdateFirmaAdi();
                    }

                    MessageBox.Show("✅ Borç başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"⚠ Veri eklenirken bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("⚠ Lütfen geçerli bir borç tutarı giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Cari unvanına göre cari kodunu döndüren metod
        public string GetCariKoduFromUnvani(string unvani)
        {
            string cariKodu = "";
            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT CariKodu FROM Cari WHERE Unvani = @CariUnvani";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CariUnvani", unvani);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            cariKodu = reader["CariKodu"].ToString();  // Cari kodunu al
                        }
                    }
                }
            }
            return cariKodu;
        }
    }
}