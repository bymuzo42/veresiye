using System;
using System.Data.SQLite;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Veresiye2025
{
    public partial class Tahsilat : Form
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

        public Tahsilat(string cariUnvan)
        {
            InitializeComponent();

            // Form köşelerini yuvarla (20px)
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

            // Header panel için olay ekle (form taşıma için)
            pnlHeader.MouseDown += PnlHeader_MouseDown;
            lblFormTitle.MouseDown += PnlHeader_MouseDown;

            textBox1.Text = cariUnvan; // Cari unvanını textBox'a yerleştiriyoruz
            textBox1.ReadOnly = true;  // Cari unvanını düzenlemeyi engelliyoruz
            this.KeyPreview = true;    // Formun klavye girdilerini dinlemesini sağlar
            this.Load += Tahsilat_Load; // Load event handler'ı ekle
        }

        // Form taşıma için olay
        public void PnlHeader_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        public void Tahsilat_Load(object sender, EventArgs e)
        {
            textBox3.Text = 0.ToString("0.00 TL", CultureInfo.GetCultureInfo("tr-TR"));
            textBox3.TextAlign = HorizontalAlignment.Right;
            textBox3.ForeColor = Color.Gray; // Varsayılan gri yazı rengi
            textBox3.Font = new Font(textBox3.Font, FontStyle.Regular);
            textBox3.KeyDown += TextBox3_KeyDown; // KeyDown olayını bağla
            textBox3.KeyPress += TextBox3_KeyPress; // KeyPress olayını bağla
            textBox3.Leave += TextBox3_Leave; // TextBox odak dışı olduğunda çalışır
            textBox3.Enter += TextBox3_Enter; // TextBox odaklandığında çalışır
            this.KeyDown += Form_KeyDown; // Esc tuşu için
            // Ödemeyi yapan text box ayarları
            odemeyiyapantextbox.TextAlign = HorizontalAlignment.Left;
            // ComboBox ayarı - varsayılan seçim yapma
            tahsilatturusecme.DropDownStyle = ComboBoxStyle.DropDownList;
            // Tarih/saat ayarları - şu anki zaman
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
        }

        public void TextBox3_KeyDown(object sender, KeyEventArgs e)
        {
            // "Esc" tuşu ile formu kapat
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
                e.Handled = true;
                return;
            }
            // "Backspace" ve "Delete" için varsayılan davranışı koru
            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            {
                e.Handled = false; // Varsayılan davranışa izin ver
                return;
            }
        }

        public void Form_KeyDown(object sender, KeyEventArgs e)
        {
            // Eğer "Esc" tuşuna basılırsa formu kapat
            if (e.KeyCode == Keys.Escape)
            {
                this.Close(); // Formu kapat
                e.Handled = true; // İşlem tamamlandı
                return;
            }
            // Eğer "Backspace" veya "Delete" tuşuna basılırsa
            if ((e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete) && textBox3.Focused)
            {
                int selectionStart = textBox3.SelectionStart;
                // "Backspace" işlemi
                if (e.KeyCode == Keys.Back && selectionStart > 0)
                {
                    textBox3.Text = textBox3.Text.Remove(selectionStart - 1, 1); // Önceki karakteri sil
                    textBox3.SelectionStart = selectionStart - 1; // İmleci konumlandır
                    e.Handled = true;
                    return;
                }
                // "Delete" işlemi
                if (e.KeyCode == Keys.Delete && selectionStart < textBox3.Text.Length)
                {
                    textBox3.Text = textBox3.Text.Remove(selectionStart, 1); // Seçili karakteri sil
                    textBox3.SelectionStart = selectionStart; // İmleci konumlandır
                    e.Handled = true;
                    return;
                }
            }
        }

        public void TextBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Rakam, virgül ve kontrol karakterleri dışında girişleri engelle
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',')
            {
                e.Handled = true;
                return;
            }
            // Nokta girişini engelle ve uyarı ver
            if (e.KeyChar == '.')
            {
                e.Handled = true;
                MessageBox.Show("Nokta ile değer girişi yapamazsınız. Lütfen virgül ',' kullanın.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Birden fazla virgül girişini engelle
            if (e.KeyChar == ',' && textBox3.Text.Contains(","))
            {
                e.Handled = true;
                return;
            }
            // Virgülden sonra iki basamaktan fazla girişe izin verme
            if (textBox3.Text.Contains(","))
            {
                int decimalIndex = textBox3.Text.IndexOf(',');
                string afterDecimal = textBox3.Text.Substring(decimalIndex + 1);
                // İmleç, ondalık işaretten sonra ise kontrol et
                if (textBox3.SelectionStart > decimalIndex && afterDecimal.Length >= 2)
                {
                    e.Handled = true; // Ondalık kısmın 2 basamağı geçmesini engelle
                }
            }
        }

        public void TextBox3_Leave(object sender, EventArgs e)
        {
            string textWithoutTL = textBox3.Text.Replace(" TL", "").Replace(",", ".");
            if (decimal.TryParse(textWithoutTL, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal value))
            {
                textBox3.Text = value.ToString("0.00").Replace(".", ",") + " TL";
            }
            else
            {
                textBox3.Text = "0,00 TL"; // Varsayılan değer
            }
        }

        public void TextBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Text.EndsWith(" TL"))
            {
                textBox3.Text = textBox3.Text.Replace(" TL", ""); // "TL" kısmını kaldır
                textBox3.ForeColor = SystemColors.WindowText; // Varsayılan renk
            }
        }

        public void TextBox3_TextChanged(object sender, EventArgs e)
        {
            if (!textBox3.Text.EndsWith(" TL") && textBox3.Focused == false)
            {
                textBox3.Text += " TL";
                textBox3.SelectionStart = textBox3.Text.Length - 3; // İmleci doğru konuma yerleştir
            }
        }

        public void Vazgec_Click(object sender, EventArgs e)
        {
            this.Close(); // Formu kapatır
        }

        public void Alarm_Click(object sender, EventArgs e)
        {
            string _cariUnvan = textBox1.Text;
            // Alarmkur formunu aç ve cari unvanını ilet
            Alarmkur alarmKurFormu = new Alarmkur(_cariUnvan);
            alarmKurFormu.ShowDialog();
        }

        public string GetCariKoduFromUnvani(string unvani)
        {
            string cariKodu = string.Empty;
            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT CariKodu FROM Cari WHERE Unvani = @Unvani LIMIT 1";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Unvani", unvani);
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        cariKodu = result.ToString();
                    }
                }
            }
            return cariKodu;
        }

        public void kaydet_Click(object sender, EventArgs e)
        {
            // Ödemeyi yapan kontrolü - boş bırakılamaz
            if (string.IsNullOrWhiteSpace(odemeyiyapantextbox.Text))
            {
                MessageBox.Show("Ödemeyi yapan kişi bilgisi boş bırakılamaz.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                odemeyiyapantextbox.Focus();
                return;
            }
            // Tahsilat türü seçimi kontrolü
            if (tahsilatturusecme.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen bir tahsilat türü seçiniz.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tahsilatturusecme.Focus();
                return;
            }
            string cariUnvani = textBox1.Text;
            string cariKodu = GetCariKoduFromUnvani(cariUnvani);
            string aciklama = textBox2.Text;
            string odemeyiYapan = odemeyiyapantextbox.Text.Trim();
            string tahsilatTuru = tahsilatturusecme.SelectedItem.ToString();
            // Tutar alanını temizleyip ondalık sayıya çeviriyoruz
            string tutarText = textBox3.Text.Replace(" TL", "").Replace(",", ".");
            if (decimal.TryParse(tutarText, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal tahsilatTutar) && tahsilatTutar > 0)
            {
                try
                {
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
                    string formatliTarih = birlesikTarihSaat.ToString("yyyy-MM-dd HH:mm:ss");
                    string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
                    using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                    {
                        connection.Open();
                        // 1️⃣ Tahsilatı "cari_hareketleri" tablosuna ekle - YENİ ALANLARLA
                        string query = @"
                            INSERT INTO cari_hareketleri (cari_kodu, tarih, tur, aciklama, tahsilat, odemeyi_yapan, tahsilat_turu)
                            VALUES (@CariKodu, @Tarih, @Tur, @Aciklama, @Tahsilat, @OdemeyiYapan, @TahsilatTuru)";
                        using (SQLiteCommand command = new SQLiteCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@CariKodu", cariKodu);
                            command.Parameters.AddWithValue("@Tarih", formatliTarih);
                            command.Parameters.AddWithValue("@Tur", "Tahsilat Dekontu");
                            command.Parameters.AddWithValue("@Aciklama", aciklama);
                            command.Parameters.AddWithValue("@Tahsilat", tahsilatTutar);
                            command.Parameters.AddWithValue("@OdemeyiYapan", odemeyiYapan);
                            command.Parameters.AddWithValue("@TahsilatTuru", tahsilatTuru);
                            command.ExecuteNonQuery();
                        }
                        // 2️⃣ Cari tablosundaki Bakiye sütununu güncelle
                        string updateQuery = @"
                            UPDATE Cari
                            SET Bakiye = (Bakiye - @Tahsilat)
                            WHERE CariKodu = @CariKodu";
                        using (SQLiteCommand updateCommand = new SQLiteCommand(updateQuery, connection))
                        {
                            updateCommand.Parameters.AddWithValue("@Tahsilat", tahsilatTutar);
                            updateCommand.Parameters.AddWithValue("@CariKodu", cariKodu);
                            updateCommand.ExecuteNonQuery();
                        }
                    }
                    // 3️⃣ "Carihareketler" Formundaki DataGridView'i anında güncelle
                    if (Owner is Carihareketler cariHareketFormu)
                    {
                        cariHareketFormu.LoadCariHareketleri(cariKodu);
                        // En son eklenen satırı seçili hale getir
                        int lastRowIndex = cariHareketFormu.dataGridView1.Rows.Count - 1;
                        if (lastRowIndex >= 0)
                        {
                            cariHareketFormu.dataGridView1.FirstDisplayedScrollingRowIndex = lastRowIndex;
                            cariHareketFormu.dataGridView1.Rows[lastRowIndex].Selected = true;
                        }
                    }
                    // 4️⃣ Pasif Hesap kontrolü
                    FormCariEkle formCariEkle = new FormCariEkle();
                    formCariEkle.PasifHesapKontrol(cariKodu);
                    // 5️⃣ Form4'teki ana DataGridView'i güncelle
                    if (Application.OpenForms["Form4"] is Form4 form4)
                    {
                        form4.RefreshDataGridView();
                    }
                    MessageBox.Show("Tahsilat başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Veri eklenirken bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Lütfen geçerli bir tahsilat tutarı giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}