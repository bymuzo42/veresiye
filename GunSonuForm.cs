using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;
using System.Drawing;
using System.Globalization;
using System.Threading.Tasks;
using System.Linq;

namespace Veresiye2025
{
    internal partial class GunSonuForm : Form
    {
        // Gün Sonu ID'si (dışarıdan erişebilmek için public olarak tanımlandı)
        public string GunSonuID { get; set; }
        // GunSonuForm içerisinde butonun Text özelliğini değiştirecek kod örneği:
        public Button GuncelleButton { get; set; }  // Button'a bir referans ekleyin

        // Form taşıma işlemleri için değişkenler
        public bool isDragging = false;
        public Point dragStartPoint;

        // Veritabanı işlemleri için servis
        public readonly DatabaseService _dbService;

        // İlerleme göstergesi
        public Guna.UI2.WinForms.Guna2ProgressBar progressBar;

        //edit mode
        public bool isEditMode = false;

        public GunSonuForm()
        {
            InitializeComponent();
            this.KeyDown += new KeyEventHandler(GunSonuForm_KeyDown); // KeyDown olayını bağlıyoruz

            // Tarih değiştiğinde sayaç ID'sini güncelle
            IslemTarihiPicker.ValueChanged += IslemTarihiPicker_ValueChanged;

            // Veritabanı servisini başlat
            _dbService = new DatabaseService();

            // İlerleme göstergesini oluştur
            InitializeProgressBar();

            // Form taşıma için olay işleyici ekleme
            panelTitleBar.MouseDown += PanelTitleBar_MouseDown;
            panelTitleBar.MouseMove += PanelTitleBar_MouseMove;
            panelTitleBar.MouseUp += PanelTitleBar_MouseUp;
            lblTitle.MouseDown += PanelTitleBar_MouseDown;
            lblTitle.MouseMove += PanelTitleBar_MouseMove;
            lblTitle.MouseUp += PanelTitleBar_MouseUp;

            // Buton hover efektleri
            btnClose.MouseEnter += btnClose_MouseEnter;
            btnClose.MouseLeave += btnClose_MouseLeave;

            // Tutar alanı için para birimi ve veri doğrulama
            SetupTutarTextBox();

            // Valör için genişletilmiş tooltip
            SetupTooltips();

            // DateTimePicker'ı güncel tarih ile ayarla
            IslemTarihiPicker.Value = DateTime.Now;
        }

        public void InitializeProgressBar()
        {
            progressBar = new Guna.UI2.WinForms.Guna2ProgressBar();
            progressBar.Location = new Point(10, 430);
            progressBar.Size = new Size(600, 10);
            progressBar.Visible = false;
            progressBar.BorderRadius = 5;
            progressBar.FillColor = Color.LightGray;
            progressBar.ProgressColor = Color.FromArgb(0, 123, 255);
            progressBar.ProgressColor2 = Color.FromArgb(0, 123, 255);
            this.Controls.Add(progressBar);
        }

        public void SetupTutarTextBox()
        {
            // Tutar alanına para birimi ekle ve sayısal girdi kontrolü
            TutarTextBox.PlaceholderText = "Tutar girin (₺)...";
            TutarTextBox.TextChanged += TutarTextBox_TextChanged;
            TutarTextBox.Leave += TutarTextBox_Leave;
            TutarTextBox.KeyPress += TutarTextBox_KeyPress;
        }

        public void TutarTextBox_TextChanged(object sender, EventArgs e)
        {
            // Nokta varsa virgüle çevir
            if (TutarTextBox.Text.Contains('.'))
            {
                int cursorPosition = TutarTextBox.SelectionStart;
                TutarTextBox.Text = TutarTextBox.Text.Replace('.', ',');

                // Cursor pozisyonunu koru
                if (cursorPosition <= TutarTextBox.Text.Length)
                {
                    TutarTextBox.SelectionStart = cursorPosition;
                }
            }
        }

        public void TutarTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Sadece rakam, virgül ve kontrol karakterlerine izin ver
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',')
            {
                // Nokta girilirse kullanıcıyı uyar
                if (e.KeyChar == '.')
                {
                    MessageBox.Show("Lütfen ondalık ayıracı olarak virgül (,) kullanınız.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                e.Handled = true; // Girişi engelle
                return;
            }

            // Virgül karakteri için sadece bir tane izin ver
            if (e.KeyChar == ',' && TutarTextBox.Text.Contains(','))
            {
                e.Handled = true; // İkinci bir virgül girilmesini önle
            }
        }

        public void TutarTextBox_Leave(object sender, EventArgs e)
        {
            // TextBox'tan çıkıldığında, sadece ondalık ayıracı kontrol et
            if (!string.IsNullOrEmpty(TutarTextBox.Text))
            {
                try
                {
                    // Türkiye kültürünü kullan (virgül ile ondalık ayırma)
                    decimal value;
                    if (decimal.TryParse(TutarTextBox.Text, NumberStyles.Any, new CultureInfo("tr-TR"), out value))
                    {
                        // "F2" formatı sadece ondalık kısmı biçimlendirir, binlik ayırıcı kullanmaz
                        // Örnek: 1565,25 şeklinde görünecek, 1.565,25 değil
                        TutarTextBox.Text = value.ToString("F2", new CultureInfo("tr-TR"));
                    }
                }
                catch
                {
                    // Geçersiz değer varsa işlem yapma
                }
            }
        }

        public async void IslemTarihiPicker_ValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SayaçTextBox.Text) || !SayaçTextBox.Text.StartsWith(GetMonthPrefix(DateTime.Now)))
            {
                // Sadece yeni ekleme sırasında veya mevcut ID seçilen ayda değilse güncelle
                string yeniSayaçID = await GenerateAyaSayaçIDAsync(IslemTarihiPicker.Value);
                SayaçTextBox.Text = yeniSayaçID;
            }
        }

        public void SetupTooltips()
        {
            toolTip.SetToolTip(SayaçTextBox, "Bu sayaç numarası otomatik olarak oluşturulur ve değiştirilemez.");
            toolTip.SetToolTip(ValörTextBox, "Valör, bankadan paranın hesaba geçiş süresidir. Bu süre bankaya göre otomatik olarak ayarlanır.");
            toolTip.SetToolTip(TutarTextBox, "POS cihazından çekilen tutarı TL cinsinden girin. Sadece sayı ve nokta kullanın.");
            toolTip.SetToolTip(IslemTarihiPicker, "İşlemin yapıldığı tarihi seçin. Bu tarih üzerine valör eklenerek ödeme tarihi hesaplanır.");
            toolTip.SetToolTip(btnkaydet, "Formu kaydetmek için tıklayın. Tüm alanların doldurulduğundan emin olun.");
            toolTip.SetToolTip(btntemizle, "Form alanlarını temizlemek için tıklayın. Bu işlem kaydedilmemiş bilgileri silecektir.");
        }

        // Form taşıma işlemleri
        public void PanelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            dragStartPoint = new Point(e.X, e.Y);
        }

        public void PanelTitleBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point p = PointToScreen(new Point(e.X, e.Y));
                this.Location = new Point(p.X - dragStartPoint.X, p.Y - dragStartPoint.Y);
            }
        }

        public void PanelTitleBar_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        // Kapatma butonu hover efekti
        public void btnClose_MouseEnter(object sender, EventArgs e)
        {
            btnClose.FillColor = Color.FromArgb(232, 17, 35);
        }

        public void btnClose_MouseLeave(object sender, EventArgs e)
        {
            btnClose.FillColor = Color.Transparent;
        }

        // Kapatma butonuna tıklama olayı
        public void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void SetSayaçID(string sayaçID)
        {
            SayaçTextBox.Text = sayaçID;  // Sayaç ID'sini TextBox'a yerleştir
        }

        public void GunSonuForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();  // ESC tuşuna basıldığında formu kapat
            }
        }

        public async void GunSonuForm_Load(object sender, EventArgs e)
        {
            try
            {
                // SayaçTextBox'ı sadece okunabilir yapıyoruz
                SayaçTextBox.ReadOnly = true;
                // Valör alanı değiştirilemesin
                ValörTextBox.ReadOnly = true;

                // Düzenleme modunda değilse devam et
                if (!isEditMode)
                {
                    // DateTimePicker'ı güncel tarihle ayarla
                    IslemTarihiPicker.Value = DateTime.Now;
                    // İlerleme çubuğunu göster
                    progressBar.Visible = true;
                    progressBar.Value = 10;

                    // Sayaç ID'sini mevcut tarihe göre oluştur (SayaçTextBox boşsa)
                    if (string.IsNullOrEmpty(SayaçTextBox.Text))
                    {
                        // IslemTarihiPicker.Value parametresini ekliyoruz
                        string sayaçID = await GenerateAyaSayaçIDAsync(IslemTarihiPicker.Value);
                        SayaçTextBox.Text = sayaçID;
                    }

                    // Veritabanından cihaz adlarını ve banka adlarını alıp ComboBox'lara ekleyelim
                    await LoadComboBoxDataAsync();
                    progressBar.Value = 50;

                    // ComboBox'lar boşsa, ilk öğeyi seçiyoruz.
                    if (CihazAdiComboBox.Items.Count > 0)
                    {
                        CihazAdiComboBox.SelectedIndex = 0;  // Varsayılan cihaz (ilk cihaz)
                    }
                    progressBar.Value = 80;
                    if (BankaAdiComboBox.Items.Count > 0)
                    {
                        BankaAdiComboBox.SelectedIndex = 0;  // Varsayılan banka (ilk banka)
                    }
                    progressBar.Value = 100;
                    await Task.Delay(500); // Gösterge için kısa bir gecikme
                    progressBar.Visible = false;
                }
                else
                {
                    // Düzenleme modunda ise sadece form gösterimini hazırla
                    Console.WriteLine("Form düzenleme modunda yükleniyor");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Form yüklenirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                progressBar.Visible = false;
            }
        }

        // Veritabanı işlemleri için asenkron metotlar
        public async Task LoadComboBoxDataAsync()
        {
            try
            {
                // Cihaz adlarını asenkron olarak yükle
                List<string> cihazAdlari = await _dbService.GetCihazAdlariAsync();
                CihazAdiComboBox.DataSource = cihazAdlari;

                // Banka adlarını asenkron olarak yükle
                List<string> bankaAdlari = await _dbService.GetBankaAdlariAsync();
                BankaAdiComboBox.DataSource = bankaAdlari;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Veriler yüklenirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // BankaAdiComboBox'ta seçim yapıldığında
        public async void BankaAdiComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Düzenleme modunda çalışmasına izin verme
            if (isEditMode)
            {
                Console.WriteLine("Düzenleme modunda olduğu için banka değişimi işlenmedi.");
                return;
            }

            if (BankaAdiComboBox.SelectedItem != null)
            {
                try
                {
                    progressBar.Visible = true;
                    progressBar.Value = 30;

                    // Seçilen bankanın adı
                    string bankaAdi = BankaAdiComboBox.SelectedItem.ToString();

                    // CihazAdiComboBox'ı güncelle
                    CihazAdiComboBox.DataSource = await _dbService.GetCihazAdiByBankaAsync(bankaAdi);
                    progressBar.Value = 70;

                    // İlk cihazı seç
                    if (CihazAdiComboBox.Items.Count > 0)
                    {
                        CihazAdiComboBox.SelectedIndex = 0;

                        // Düzenleme modunda değilse valör'ü güncelle
                        if (!isEditMode)
                        {
                            await UpdateValörAsync(CihazAdiComboBox.SelectedItem.ToString());
                        }
                    }
                    progressBar.Value = 100;
                    await Task.Delay(300);
                    progressBar.Visible = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Banka seçiminde hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    progressBar.Visible = false;
                }
            }
        }

        // Seçilen cihazın valör değerini asenkron olarak güncelle
        public async Task UpdateValörAsync(string cihazAdi)
        {
            try
            {
                // Düzenleme modunda bu metodu çalıştırma
                if (isEditMode)
                {
                    Console.WriteLine("Düzenleme modunda olduğu için valör güncellenmedi.");
                    return;
                }

                string valor = await _dbService.GetValorByCihazAdiAsync(cihazAdi);
                if (!string.IsNullOrEmpty(valor))
                {
                    ValörTextBox.Text = valor;  // Valör değerini TextBox'a aktar
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Valör bilgisi alınırken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void btntemizle_Click(object sender, EventArgs e)
        {
            try
            {
                // ComboBox'ları sıfırlıyoruz
                CihazAdiComboBox.SelectedIndex = -1;  // Seçili değeri sıfırlıyoruz
                BankaAdiComboBox.SelectedIndex = -1;  // Seçili değeri sıfırlıyoruz

                // TextBox'ları sıfırlıyoruz
                ValörTextBox.Clear();  // Valör sıfırlanacak
                TutarTextBox.Clear();  // Tutar sıfırlanacak

                // DateTimePicker'ı güncel tarihe ayarlıyoruz
                IslemTarihiPicker.Value = DateTime.Now;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Form temizlenirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        internal async void btnkaydet_Click(object sender, EventArgs e)
        {
            try
            {
                // İlerleme çubuğunu göster
                progressBar.Visible = true;
                progressBar.Value = 10;

                // Tüm alanların dolu olup olmadığını kontrol et
                if (string.IsNullOrEmpty(CihazAdiComboBox.SelectedItem?.ToString()) && string.IsNullOrEmpty(CihazAdiTextBox.Text) ||
                    string.IsNullOrEmpty(BankaAdiComboBox.SelectedItem?.ToString()) && string.IsNullOrEmpty(BankaAdiTextBox.Text) ||
                    string.IsNullOrEmpty(ValörTextBox.Text) ||
                    string.IsNullOrEmpty(TutarTextBox.Text))
                {
                    MessageBox.Show("Tüm alanları doldurun.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    progressBar.Visible = false;
                    return; // Hata varsa işlemi durdur
                }

                progressBar.Value = 30;

                // Sayaç ID'sini oluştur ve TextBox'a yaz - aya göre
                string sayaçID = String.IsNullOrEmpty(SayaçTextBox.Text)
                    ? await GenerateAyaSayaçIDAsync(IslemTarihiPicker.Value) // Doğru parametre ile çağrı
                    : SayaçTextBox.Text;
                SayaçTextBox.Text = sayaçID;

                progressBar.Value = 50;

                // Verileri alırken doğru türde olduklarından emin olalım
                string cihazAdi = !string.IsNullOrEmpty(CihazAdiTextBox.Text) ? CihazAdiTextBox.Text : CihazAdiComboBox.SelectedItem.ToString();
                string bankaAdi = !string.IsNullOrEmpty(BankaAdiTextBox.Text) ? BankaAdiTextBox.Text : BankaAdiComboBox.SelectedItem.ToString();

                // **Valör'ü integer'a dönüştürme**
                if (!int.TryParse(ValörTextBox.Text, out int valör))
                {
                    MessageBox.Show("Valör değeri geçerli bir sayı olmalıdır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    progressBar.Visible = false;
                    return; // Hata varsa işlemi durdur
                }

                progressBar.Value = 70;

                // **Tutar'ı decimal'a dönüştürme** - Türkçe kültür kullanarak (virgül ile)
                if (!decimal.TryParse(TutarTextBox.Text, NumberStyles.Any, new CultureInfo("tr-TR"), out decimal tutar))
                {
                    MessageBox.Show("Tutar değeri geçerli bir ondalıklı sayı olmalıdır.\nLütfen ondalık ayıracı olarak virgül (,) kullanınız.",
                        "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    progressBar.Visible = false;
                    return; // Hata varsa işlemi durdur
                }

                // **Tarihleri alıyoruz**
                DateTime islemTarihi = IslemTarihiPicker.Value;
                DateTime gececegiTarih = islemTarihi.AddDays(valör + 1);  // Valör + 1 gün

                progressBar.Value = 80;

                // Eğer formda mevcut bir "GunSonuID" varsa, güncelleme işlemi yap
                if (!string.IsNullOrEmpty(GunSonuID))
                {
                    // Verileri güncelliyoruz
                    await _dbService.UpdateGunSonuVerisiAsync(GunSonuID, cihazAdi, bankaAdi, valör, tutar, islemTarihi, gececegiTarih, sayaçID);
                }
                else
                {
                    // Yeni bir kayıt ekliyoruz
                    await _dbService.AddGunSonuVerisiAsync(cihazAdi, bankaAdi, valör, tutar, islemTarihi, gececegiTarih, sayaçID);
                }

                progressBar.Value = 90;

                // **postakip formundaki HesaplamalariYukle() metodunu çağırıyoruz**
                if (postakip.Instance != null)
                {
                    postakip.Instance.HesaplamalariYukle();
                    postakip.Instance.LoadData(1); // Verileri yeniden yükle
                    postakip.Instance.BankalariYukle(); // Bankaları yeniden yükle
                }

                progressBar.Value = 100;
                await Task.Delay(300); // Gösterge için kısa bir gecikme
                progressBar.Visible = false;

                // İşlem başarılı mesajını göster
                MessageBox.Show("İşlem başarıyla tamamlandı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Formu kapat
                this.Close();
            }
            catch (Exception ex)
            {
                // Hata mesajı
                MessageBox.Show("Kaydetme işlemi sırasında hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                progressBar.Visible = false;
            }
        }

        // Belirli bir tarih için ay kısaltmasını döndüren metot
        public string GetMonthPrefix(DateTime date)
        {
            string[] ayKisaltmalari = { "OCA", "SUB", "MAR", "NIS", "MAY", "HAZ", "TEM", "AGU", "EYL", "EKI", "KAS", "ARA" };
            int ay = date.Month - 1; // Dizi 0'dan başladığı için
            return ayKisaltmalari[ay];
        }

        // Belirli bir tarih için sayaç ID oluşturan metot
        public async Task<string> GenerateAyaSayaçIDAsync(DateTime date)
        {
            string ayKisaltmasi = GetMonthPrefix(date);
            int counter = await _dbService.GetMaxGunSonuIDForMonthAsync(ayKisaltmasi);
            return $"{ayKisaltmasi}{(counter + 1):D4}";
        }

        public void SetupForEditing(string gunSonuID, string bankaAdi, string cihazAdi, string valor, string tutar, DateTime islemTarihi)
        {
            // Düzenleme modunu aktif et
            isEditMode = true;

            // BankaAdiComboBox ve CihazAdiComboBox'ın olay işleyicilerini geçici olarak kaldır
            BankaAdiComboBox.SelectedIndexChanged -= BankaAdiComboBox_SelectedIndexChanged;

            // Diğer form temizleme ve başlangıç değerleri işlemleri engelle
            this.KeyDown -= GunSonuForm_KeyDown;
            IslemTarihiPicker.ValueChanged -= IslemTarihiPicker_ValueChanged;

            this.GunSonuID = gunSonuID;
            this.SayaçTextBox.Text = gunSonuID;

            // ComboBox'ları gizle, TextBox'ları göster
            this.BankaAdiComboBox.Visible = false;
            this.CihazAdiComboBox.Visible = false;
            this.BankaAdiTextBox.Visible = true;
            this.CihazAdiTextBox.Visible = true;

            // Değerleri atama
            this.BankaAdiTextBox.Text = bankaAdi;
            this.CihazAdiTextBox.Text = cihazAdi;

            // Valör değerini doğrudan düzenleme (ÖNEMLİ)
            this.ValörTextBox.Text = valor;
            Console.WriteLine($"SetupForEditing: Valör değeri ayarlandı: {valor}");

            this.TutarTextBox.Text = tutar;
            this.IslemTarihiPicker.Value = islemTarihi;

            // Salt okunur ayarları
            this.BankaAdiTextBox.ReadOnly = true;
            this.CihazAdiTextBox.ReadOnly = true;
            this.ValörTextBox.ReadOnly = true;

            // Form görünümünü ayarla
            this.lblTitle.Text = "Günsonu Düzenle";
            this.btnkaydet.Text = "Güncelle";

            // Form gösterildikten sonra olay işleyicileri yeniden ekle
            this.Shown += GunSonuForm_Shown;
        }

        // Eklenecek yeni metot
        public void GunSonuForm_Shown(object sender, EventArgs e)
        {
            // Form gösterildikten sonra key down olay işleyicisini tekrar ekle
            this.KeyDown += GunSonuForm_KeyDown;

            // Olayı bir kez çalıştırdıktan sonra kaldır
            this.Shown -= GunSonuForm_Shown;
        }

        public void SetValörDegeri(string valor)
        {
            try
            {
                // Doğrudan TextBox'a atama yaparak güvence altına alalım
                this.ValörTextBox.Text = valor;

                // Bu değerin değişmediğini garanti etmek için güncelleme olaylarını devre dışı bırakalım
                this.ValörTextBox.ReadOnly = true;

                // Düzenleme modunda olduğunu belirt
                isEditMode = true;

                Console.WriteLine($"SetValörDegeri metodunda atanan değer: {valor}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SetValörDegeri metodunda hata: {ex.Message}");
            }
        }

        /// <summary>
        /// Veritabanı işlemleri için yardımcı sınıf
        /// </summary>
        public class DatabaseService
    {
        public readonly string _connectionString = "Data Source=veresiye.db;Version=3;";

        // Cihaz adlarını veritabanından asenkron olarak almak
        public async Task<List<string>> GetCihazAdlariAsync()
        {
            List<string> cihazAdlari = new List<string>();

            await Task.Run(() =>
            {
                using (SQLiteConnection conn = new SQLiteConnection(_connectionString))
                {
                    conn.Open();
                    string query = "SELECT DISTINCT CihazAdi FROM Banka";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cihazAdlari.Add(reader["CihazAdi"].ToString());
                            }
                        }
                    }
                }
            });

            return cihazAdlari;
        }

        // Banka adlarını veritabanından asenkron olarak almak
        public async Task<List<string>> GetBankaAdlariAsync()
        {
            List<string> bankaAdlari = new List<string>();

            await Task.Run(() =>
            {
                using (SQLiteConnection conn = new SQLiteConnection(_connectionString))
                {
                    conn.Open();
                    string query = "SELECT DISTINCT BankaAdi FROM Banka";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                bankaAdlari.Add(reader["BankaAdi"].ToString());
                            }
                        }
                    }
                }
            });

            return bankaAdlari;
        }

        // Bankaya göre cihaz adlarını veritabanından asenkron olarak almak
        public async Task<List<string>> GetCihazAdiByBankaAsync(string bankaAdi)
        {
            List<string> cihazlar = new List<string>();

            await Task.Run(() =>
            {
                using (SQLiteConnection conn = new SQLiteConnection(_connectionString))
                {
                    conn.Open();
                    string query = "SELECT CihazAdi FROM Banka WHERE BankaAdi = @BankaAdi";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@BankaAdi", bankaAdi);
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cihazlar.Add(reader["CihazAdi"].ToString());
                            }
                        }
                    }
                }
            });

            return cihazlar;
        }

        // Cihaz adına göre valör bilgisini asenkron olarak almak
        public async Task<string> GetValorByCihazAdiAsync(string cihazAdi)
        {
            string valor = string.Empty;

            await Task.Run(() =>
            {
                using (SQLiteConnection conn = new SQLiteConnection(_connectionString))
                {
                    conn.Open();
                    string query = "SELECT Valor FROM Banka WHERE CihazAdi = @CihazAdi";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CihazAdi", cihazAdi);
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            valor = result.ToString();
                        }
                    }
                }
            });

            return valor;
        }

        // Belirli bir ay için maksimum GunSonuID'yi almak
        public async Task<int> GetMaxGunSonuIDForMonthAsync(string monthPrefix)
        {
            int counter = 0;

            await Task.Run(() =>
            {
                using (SQLiteConnection conn = new SQLiteConnection(_connectionString))
                {
                    conn.Open();
                    string query = "SELECT MAX(CAST(SUBSTR(GunsonuID, 4, 4) AS INTEGER)) FROM Gunsonu WHERE GunsonuID LIKE @Prefix";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Prefix", monthPrefix + "%");
                        object result = cmd.ExecuteScalar();
                        if (result != DBNull.Value && result != null)
                        {
                            counter = Convert.ToInt32(result);
                        }
                    }
                }
            });

            return counter;
        }

        // Günsonu verilerini eklemek
        public async Task AddGunSonuVerisiAsync(string cihazAdi, string bankaAdi, int valör, decimal tutar, DateTime islemTarihi, DateTime gececegiTarih, string sayaçID)
        {
            await Task.Run(() =>
            {
                using (SQLiteConnection conn = new SQLiteConnection(_connectionString))
                {
                    try
                    {
                        conn.Open();
                        string query = "INSERT INTO Gunsonu (GunsonuID, CihazAdi, BankaAdi, Valör, Tutar, IslemTarihi, GececegiTarih) " +
                                       "VALUES (@GunSonuID, @CihazAdi, @BankaAdi, @Valör, @Tutar, @IslemTarihi, @GececegiTarih)";
                        using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                        {
                            // Parametreleri doğru türde gönderiyoruz
                            cmd.Parameters.AddWithValue("@GunSonuID", sayaçID);
                            cmd.Parameters.AddWithValue("@CihazAdi", cihazAdi);
                            cmd.Parameters.AddWithValue("@BankaAdi", bankaAdi);
                            cmd.Parameters.AddWithValue("@Valör", valör);

                            // Önemli: decimal parametresini doğru şekilde gönderme
                            cmd.Parameters.Add(new SQLiteParameter("@Tutar", DbType.Double) { Value = (double)tutar });

                            cmd.Parameters.AddWithValue("@IslemTarihi", islemTarihi.ToString("yyyy-MM-dd HH:mm:ss"));
                            cmd.Parameters.AddWithValue("@GececegiTarih", gececegiTarih.ToString("yyyy-MM-dd HH:mm:ss"));

                            // Veriyi veritabanına ekliyoruz
                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Veri ekleme sırasında hata oluştu: " + ex.Message);
                    }
                }
            });
        }

        public async Task UpdateGunSonuVerisiAsync(string gunSonuID, string cihazAdi, string bankaAdi, int valör, decimal tutar, DateTime islemTarihi, DateTime gececegiTarih, string sayaçID)
        {
            await Task.Run(() =>
            {
                using (SQLiteConnection conn = new SQLiteConnection(_connectionString))
                {
                    try
                    {
                        conn.Open();
                        string query = "UPDATE Gunsonu SET CihazAdi=@CihazAdi, BankaAdi=@BankaAdi, Valör=@Valör, Tutar=@Tutar, IslemTarihi=@IslemTarihi, GececegiTarih=@GececegiTarih WHERE GunsonuID=@GunSonuID";
                        using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                        {
                            // Parametreleri doğru türde gönderiyoruz
                            cmd.Parameters.AddWithValue("@GunSonuID", gunSonuID);
                            cmd.Parameters.AddWithValue("@CihazAdi", cihazAdi);
                            cmd.Parameters.AddWithValue("@BankaAdi", bankaAdi);
                            cmd.Parameters.AddWithValue("@Valör", valör);

                            // Önemli: decimal parametresini doğru şekilde gönderme
                            cmd.Parameters.Add(new SQLiteParameter("@Tutar", DbType.Double) { Value = (double)tutar });

                            cmd.Parameters.AddWithValue("@IslemTarihi", islemTarihi.ToString("yyyy-MM-dd HH:mm:ss"));
                            cmd.Parameters.AddWithValue("@GececegiTarih", gececegiTarih.ToString("yyyy-MM-dd HH:mm:ss"));

                            // Veriyi veritabanına güncelliyoruz
                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Veri güncelleme sırasında hata oluştu: " + ex.Message);
                    }
                }
            });
        }

        // GunSonuForm içine eklenecek metot
        
    }
}
}