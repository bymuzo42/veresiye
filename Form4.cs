using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;
using System.Drawing.Printing;
using System.Threading.Tasks;
using System.Reflection;
using System.Diagnostics;
using System.Net;
using Guna.UI2.WinForms;
using System.Drawing.Drawing2D;
using System.Security.Claims;
using Veresiye2025.Controls;
using System.IO;

namespace Veresiye2025
{
    public partial class Form4 : Form
    {
        public string _firmaAdi;
        public List<string> cariKoduListesi = new List<string>();
        public string currentCariKodu;
        internal List<Alarm> Alarmlar = new List<Alarm>();
        public PrintDocument printDocument = new PrintDocument();
        public string[,] raporVerileri; // Yazdırılacak veriler için bir dizi
        public Font font = new Font("Arial", 10, FontStyle.Regular); // Yazı tipi
        public int currentRow = 0; // Yazdırma sırasında satır takibi
        public delegate void BakiyeGuncelleEventHandler(string cariKodu, decimal bakiye);
        public event BakiyeGuncelleEventHandler OnBakiyeGuncelle;
        public bool dragging = false;
        public Point dragCursorPoint;
        public Point dragFormPoint;
        // Form4 sınıfına ait alarm işlemleri için eklenmesi gereken değişkenler
        public System.Windows.Forms.NotifyIcon notifyIcon;
        public System.Windows.Forms.Timer alarmKontrolTimer;
        public bool bildirimGosterildi = false;
        public Label alarmSayacLabel = null;
        public PictureBox alarmSayacPictureBox = null;
        // Sağ panel kontrolü için değişkenler
        public bool rightPanelVisible = true;
        public const int NORMAL_WIDTH = 893;
        //public Button btnShowRightPanel = null;

        // Form sınıfının dışında, namespace içinde tanımlayın:
        public class CircularLabel : Control
        {
            public int _value;
            public int Value
            {
                get { return _value; }
                set
                {
                    _value = value;
                    this.Text = value > 9 ? "9+" : value.ToString();
                    this.Invalidate();
                }
            }
            public CircularLabel()
            {
                this.SetStyle(ControlStyles.SupportsTransparentBackColor |
                             ControlStyles.OptimizedDoubleBuffer |
                             ControlStyles.AllPaintingInWmPaint |
                             ControlStyles.UserPaint, true);
                this.BackColor = Color.Transparent;
                this.Size = new Size(22, 22);
                this.Font = new Font("Segoe UI", 8, FontStyle.Bold);
            }
            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                // Daire çiz
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(239, 68, 93)))
                {
                    e.Graphics.FillEllipse(brush, 0, 0, this.Width - 1, this.Height - 1);
                }
                // Metin çiz
                using (StringFormat sf = new StringFormat())
                {
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;
                    using (SolidBrush brush = new SolidBrush(Color.White))
                    {
                        e.Graphics.DrawString(
                            this.Text,
                            this.Font,
                            brush,
                            new RectangleF(0, 0, this.Width, this.Height),
                            sf
                        );
                    }
                }
            }
        }

        public bool isDarkMode = false;

        public Form4(string firmaAdi)
        {
            InitializeComponent();
            this.BackColor = SystemColors.Control;
            panelContent.BackColor = SystemColors.Control;
            pnlYaklasanAlarmlar.BackColor = Color.MidnightBlue; // Bu satırı ekleyin
            // DataGridView navigation ve scrollbar ayarları
            dataGridView1.StandardTab = true;
            dataGridView1.TabStop = true;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ScrollBars = ScrollBars.Both; // SCROLLBAR'I ZORLA AKTIF ET

            KeyPreview = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            _firmaAdi = firmaAdi;

            // Event bağlama
            dataGridView1.DataError += DataGridView1_DataError; // DataError olayını ekle
            cmbSorgu.SelectedIndexChanged += cmbSorgu_SelectedIndexChanged;
            txtUnvan.TextChanged += txtUnvan_TextChanged;
            txtUnvan.KeyDown += txtUnvan_KeyDown;
            dataGridView1.KeyDown += DataGridView1_KeyDown;
            cmbHesap.Items.Clear();
            cmbHesap.Items.Add("Tümü");
            cmbHesap.Items.Add("Aktif Hesaplar");
            cmbHesap.Items.Add("Pasif Hesaplar");
            cmbHesap.SelectedIndex = 0;
            this.Load += Form4_Load_1;
            this.FormClosed += Form4_FormClosed;
        }

        public string FirmaAdi
        {
            get
            {
                // Başlık formatı: "Ana Ekran - Gencer Avm"
                if (this.Text.Contains("-"))
                {
                    return this.Text.Split('-')[1].Trim(); // Başlıktan firma adını ayırır
                }
                return "Firma Adı Belirtilmemiş";
            }
        }

        public void RefreshDataGridView()
        {
            dataGridView1.Refresh();  // DataGridView'i yeniler
            LoadCariList();  // Cari listesi verilerini yeniden yükler
            // DataGridView'deki her bir satır için renklendirme işlemi
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["Hesap"].Value?.ToString() == "Pasif Hesap")
                {
                    // Eğer "Pasif Hesap" ise kırmızı renkte göster
                    row.DefaultCellStyle.ForeColor = Color.Red;
                    row.DefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
                }
                else if (row.Cells["Hesap"].Value?.ToString() == "Aktif Hesap")
                {
                    // Eğer "Aktif Hesap" ise siyah renkte göster
                    row.DefaultCellStyle.ForeColor = Color.Black;
                    row.DefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
                }
            }
        }

        public void UpdateColumn5()
        {
            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                // Recursive CTE ile her cari_kodu için bakiye hesaplama
                string query = @"
WITH RECURSIVE Calculated AS (
    SELECT
        cari_kodu,
        id,
        borc,
        tahsilat,
        borc - tahsilat AS bakiye
    FROM cari_hareketleri
    WHERE id = (SELECT MIN(id) FROM cari_hareketleri WHERE cari_kodu = cari_hareketleri.cari_kodu)
    UNION ALL
    SELECT
        ch.cari_kodu,
        ch.id,
        ch.borc,
        ch.tahsilat,
        c.bakiye + ch.borc - ch.tahsilat AS bakiye
    FROM cari_hareketleri ch
    JOIN Calculated c
      ON ch.cari_kodu = c.cari_kodu
     AND ch.id > c.id
)
SELECT
    cari_kodu,
    bakiye AS bakiye
FROM Calculated
WHERE id = (SELECT MAX(id) FROM cari_hareketleri WHERE cari_kodu = Calculated.cari_kodu)
ORDER BY cari_kodu;";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string cariKodu = reader["cari_kodu"].ToString();
                            decimal bakiye = reader["bakiye"] != DBNull.Value ? Convert.ToDecimal(reader["bakiye"]) : 0;
                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                if (row.Cells["CariKodu"].Value?.ToString() == cariKodu)
                                {
                                    // Bakiye sütununu formatla
                                    dataGridView1.Columns["Bakiye"].DefaultCellStyle.Format = "C2";
                                    dataGridView1.Columns["Bakiye"].ValueType = typeof(double);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        public void PanelTop_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                dragging = true;
                dragCursorPoint = Cursor.Position;
                dragFormPoint = this.Location;
            }
        }

        public void PanelTop_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        public void PanelTop_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        public void LoadCariList()
        {
            // cariKoduListesi'ni temizle
            cariKoduListesi.Clear();
            dataGridView1.Columns.Clear();

            // Sütun eklemeleri ve diğer DataGridView ayarları
            dataGridView1.Columns.Add("Hesap", "Hesap");
            dataGridView1.Columns.Add("CariKodu", "Cari Kodu");
            dataGridView1.Columns.Add("Unvani", "Unvanı");
            dataGridView1.Columns.Add("Il", "İl");
            dataGridView1.Columns.Add("SonIslemTarihi", "Son İşlem Tarihi");
            dataGridView1.Columns.Add("Bakiye", "Bakiye");

            // SCROLLBAR AYARLARINI EKLE
            dataGridView1.ScrollBars = ScrollBars.Both;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AllowUserToResizeColumns = false;

            // Görünüm ayarları
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Sütun genişlikleri
            dataGridView1.Columns["Hesap"].Width = 85;
            dataGridView1.Columns["CariKodu"].Width = 90;
            dataGridView1.Columns["Il"].Width = 60;
            dataGridView1.Columns["SonIslemTarihi"].Width = 80;
            dataGridView1.Columns["Bakiye"].Width = 140;
            dataGridView1.Columns["Unvani"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            // Hücre stil ayarları
            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Regular);
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;

            // Verileri yükle
            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                // Ana sorgu - Borçları yüksekten düşüğe doğru sırala
                string query = @"
SELECT
    Cari.CariKodu,
    Cari.Unvani,
    Cari.Il,
    Cari.Hesap,
    IFNULL(Cari.bakiye, 0) AS Bakiye,
    DATE('now') AS SonIslemTarihi
FROM Cari
ORDER BY Cari.bakiye DESC"; // Borcu en yüksek olanlar en üstte
                // Önce DataGridView'i doldur
                List<string> tempCariKodlari = new List<string>(); // Geçici liste
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Cari kodunu geçici listeye ekle (sıralamayı korumak için)
                            string cariKodu = reader["CariKodu"].ToString();
                            tempCariKodlari.Add(cariKodu);
                            decimal bakiye = Convert.ToDecimal(reader["Bakiye"]);
                            int rowIndex = dataGridView1.Rows.Add(
                                reader["Hesap"].ToString(),
                                cariKodu,
                                reader["Unvani"].ToString(),
                                reader["Il"].ToString(),
                                Convert.ToDateTime(reader["SonIslemTarihi"]).ToString("dd.MM.yyyy"),
                                bakiye.ToString("C2")
                            );
                            // Stil ayarları
                            DataGridViewRow row = dataGridView1.Rows[rowIndex];
                            if (reader["Hesap"].ToString() == "Pasif Hesap")
                            {
                                row.DefaultCellStyle.ForeColor = Color.Red;
                                row.DefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
                            }
                            else if (reader["Hesap"].ToString() == "Aktif Hesap")
                            {
                                row.DefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
                                if (bakiye < 0)
                                {
                                    row.Cells["Bakiye"].Style.ForeColor = Color.Blue;
                                }
                                else
                                {
                                    row.DefaultCellStyle.ForeColor = Color.Black;
                                }
                            }
                        }
                    }
                }
                // Şimdi cariKoduListesi'ni DataGridView ile aynı sırada doldur
                cariKoduListesi.AddRange(tempCariKodlari);
                Console.WriteLine($"Yüklenen cari kodu sayısı: {cariKoduListesi.Count}");
            }

            // SCROLLBAR'I ZORLA KORUYUN
            dataGridView1.ScrollBars = ScrollBars.Both;
        }

        public async void Form4_Load_1(object sender, EventArgs e)
        {
                       
            rightPanelVisible = true;

            // Form başlığını ayarla
            this.Text = $"Ana Ekran - {_firmaAdi}";
            // DataGridView hata olayını bağla
            dataGridView1.DataError += DataGridView1_DataError;

            // Cari listesini yükle ve sütunları güncelle
            LoadCariList();
            UpdateColumn5();

            // Sağ panel varsayılan ayarları
            rightPanelVisible = true;

            // ComboBox varsayılan seçimler
            if (cmbSorgu.Items.Count > 0)
                cmbSorgu.SelectedIndex = 0;
            if (cmbHesap.Items.Count > 0)
                cmbHesap.SelectedIndex = 0;
            if (cmbFiltre.Items.Count > 0)
                cmbFiltre.SelectedIndex = 0;
            if (cmbAra.Items.Count > 0)
                cmbAra.SelectedIndex = 0;

            // Form yüklendikten sonra DataGridView'e odaklan
            this.BeginInvoke(new Action(() => {
                dataGridView1.ScrollBars = ScrollBars.Both; // Zorla aktif et
                dataGridView1.Focus();
                if (dataGridView1.Rows.Count > 0)
                {
                    dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells[0];
                    dataGridView1.Rows[0].Selected = true;
                }
            }));

            // Kaydedilen dili al
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

            // Alarm sistemi kurulumu ve ilk kontrol
            AlarmTablosunuKontrolEt();
            InitializeAlarmSystem();
            // Alarm panelini başlangıçta güncelle
            GuncelleAlarmPanelIcerigi();
            // Uygulamanın biraz bekleme süresinden sonra bildirim kontrolü yapmasını sağla
            // async/await kullanarak 5 saniye bekle
            await Task.Delay(5000);
            // 5 saniye sonra doğrudan alarm kontrolünü yap (Invoke gerekmez)
            Console.WriteLine("İlk alarm kontrolü gerçekleştiriliyor...");
            KontrolEtVeGosterYaklasanAlarmlari();

            // alarmbildir butonuna sağ tıklama menüsü ekle
            ContextMenuStrip alarmMenu = new ContextMenuStrip();
            // Yeni Alarm Ekle menü öğesi
            alarmMenu.Items.Add("Yeni Alarm Ekle", null, (s, e2) =>
            {
                Alarmkur alarmForm = new Alarmkur();
                alarmForm.ShowDialog();
                KontrolEtAlarmlari();
            });
            // Alarm Listesini Göster menü öğesi
            alarmMenu.Items.Add("Alarm Listesini Göster", null, (s, e2) =>
            {
                GosterAlarmListesi();
            });
            // Menüyü butona bağla
            alarmbildir.ContextMenuStrip = alarmMenu;
        }
               

        public void DataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            // Sadece Enter tuşu için özel işlem yap
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true; // Enter tuşunun varsayılan davranışını engelle
                OpenCariHareketler();
            }
            // Diğer tüm tuşlar (ok tuşları dahil) için DataGridView'in varsayılan davranışını koru
        }

        public void OpenCariHareketler()
        {
            if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Index >= 0)
            {
                string selectedCariKodu = dataGridView1.CurrentRow.Cells["CariKodu"].Value?.ToString();
                string selectedCariAdi = dataGridView1.CurrentRow.Cells["Unvani"].Value?.ToString();
                if (!string.IsNullOrEmpty(selectedCariKodu) && !string.IsNullOrEmpty(selectedCariAdi))
                {
                    // Cari hareket formunu aç
                    Carihareketler cariHareketForm = new Carihareketler(selectedCariKodu, cariKoduListesi, selectedCariAdi);
                    cariHareketForm.OnBakiyeGuncelle += UpdateColumn5FromCarihareketler;
                    this.Hide();
                    cariHareketForm.ShowDialog();
                    this.Show();
                    RefreshDataGridView();
                    UpdateColumn5();
                    cariHareketForm.UpdateButton13();
                }
                else
                {
                    MessageBox.Show("Seçili satırda gerekli bilgiler eksik.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Lütfen bir satır seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Diğer metodlar aynı kalıyor - sadece ana sorunlu metodları düzelttim
        // Geri kalan tüm metodlarınız aynen kalacak...

        public decimal GetVadesiGecenHesapToplam()
        {
            decimal vadesiGecenToplam = 0;
            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT SUM(Bakiye) FROM Cari WHERE VadeTarihi < @Today AND Hesap = 'Pasif'";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Today", DateTime.Today);
                    object result = command.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        vadesiGecenToplam = Convert.ToDecimal(result);
                    }
                }
            }
            return vadesiGecenToplam;
        }

        public void txtUnvan_TextChanged(object sender, EventArgs e)
        {
            // txtUnvan her değiştiğinde ApplySearch metodunu çağır
            ApplySearch();
        }

        public void InitializeSearchSystem()
        {
            // cmbSorgu combobox seçim değişikliğini dinleme
            cmbSorgu.SelectedIndexChanged += cmbSorgu_SelectedIndexChanged;
            // txtUnvan yazıldıkça arama yapma
            txtUnvan.TextChanged += txtUnvan_TextChanged;
        }

        public void cmbSorgu_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Seçilen sorgu yöntemine göre lblUnvan'ı ayarla
            string selectedSearch = cmbSorgu.SelectedItem.ToString();
            if (selectedSearch == "Unvan İle Sorgu")
            {
                lblUnvan.Text = "Unvan :";
            }
            else if (selectedSearch == "Kod İle Sorgu")
            {
                lblUnvan.Text = "Cari Kod :";
            }
            else if (selectedSearch == "İl İle Sorgu")
            {
                lblUnvan.Text = "İl İle :";
            }
            // Yeni sorgu yöntemi seçildiğinde mevcut aramayı uygula
            ApplySearch();
        }

        public void ApplySearch()
        {
            string searchValue = txtUnvan.Text.ToLower(); // Arama değeri
            string selectedSearch = cmbSorgu.SelectedItem?.ToString() ?? "Unvan İle Sorgu";
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue; // Yeni satırları atla
                // Hangi sütunda arama yapılacağı
                string cellValue = "";
                if (selectedSearch == "Unvan İle Sorgu")
                {
                    cellValue = row.Cells["Unvani"].Value?.ToString() ?? "";
                }
                else if (selectedSearch == "Kod İle Sorgu")
                {
                    cellValue = row.Cells["CariKodu"].Value?.ToString() ?? "";
                }
                else if (selectedSearch == "İl İle Sorgu")
                {
                    cellValue = row.Cells["Il"].Value?.ToString() ?? "";
                }
                // Satır görünürlüğünü ayarla
                row.Visible = cellValue.ToLower().Contains(searchValue);
            }
        }

        public void TxtUnvan_KeyPressForDigitsOnly(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        public void txtUnvan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bool anyRowVisible = false;
                // Satırların görünürlüğünü kontrol et
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Visible)
                    {
                        anyRowVisible = true;
                        break;
                    }
                }
                // Eğer hiçbir satır görünmüyorsa, yeni cari ekleme öner
                if (!anyRowVisible)
                {
                    DialogResult result = MessageBox.Show(
                        "Yeni Cari Eklemek İstiyor Musunuz?",
                        "Cari Bulunamadı",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );
                    if (result == DialogResult.Yes)
                    {
                        // txtUnvan'daki değeri aktararak FormCariEkle aç
                        string unvan = txtUnvan.Text;
                        FormCariEkle cariEkleFormu = new FormCariEkle("", unvan);
                        cariEkleFormu.ShowDialog();
                        // Cari listesini yenile
                        LoadCariList();
                    }
                    // txtUnvan alanını temizle
                    txtUnvan.Text = string.Empty;
                }
                e.Handled = true; // Enter tuşunun başka bir işlem yapmasını engelle
            }
        }

        public void cmbHesap_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedFilter = cmbHesap.SelectedItem?.ToString(); // Combobox'tan seçili değeri al
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue; // Yeni satır kontrolü (ek satır varsa göz ardı et)
                string hesapDurumu = row.Cells["Hesap"].Value?.ToString();
                // Filtreleme işlemi
                if (selectedFilter == "Tümü")
                {
                    row.Visible = true; // Tüm hesaplar gösterilir
                }
                else if (selectedFilter == "Aktif Hesaplar")
                {
                    row.Visible = hesapDurumu == "Aktif Hesap"; // Sadece aktif hesaplar
                }
                else if (selectedFilter == "Pasif Hesaplar")
                {
                    row.Visible = hesapDurumu == "Pasif Hesap"; // Sadece pasif hesaplar
                }
            }
        }

        public void Form4_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        public decimal GetVadesiGecenToplam()
        {
            decimal toplamBorc = 0;
            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = @"
                    SELECT SUM(Borc) AS ToplamBorc
                    FROM cari_hareketleri
                    WHERE Cari_Kodu IN (
                        SELECT Cari_Kodu
                        FROM cari_hareketleri
                        WHERE Tur = 'Borç Dekontu'
                        AND Tarih <= DATE('now', '-30 days')
                        AND Cari_Kodu NOT IN (
                            SELECT DISTINCT Cari_Kodu
                            FROM cari_hareketleri
                            WHERE Tur = 'Tahsilat Dekontu'
                            AND Tarih > DATE('now', '-30 days')
                        )
                    )";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        toplamBorc = Convert.ToDecimal(result);
                    }
                }
            }
            return toplamBorc;
        }

        // Alarm Ekleme Metodu
        public void AlarmEkle(DateTime tarih, string odemeTuru, string aciklama, string cariUnvan)
        {
            MessageBox.Show("Alarm başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void BtnCarininHareketleriniAc_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Index >= 0)
            {
                string selectedCariKodu = dataGridView1.CurrentRow.Cells["CariKodu"].Value?.ToString();
                string selectedCariAdi = dataGridView1.CurrentRow.Cells["Unvani"].Value?.ToString();
                if (!string.IsNullOrEmpty(selectedCariKodu) && !string.IsNullOrEmpty(selectedCariAdi))
                {
                    // Cari hareket formunu aç
                    Carihareketler cariHareketForm = new Carihareketler(selectedCariKodu, cariKoduListesi, selectedCariAdi);
                    cariHareketForm.OnBakiyeGuncelle += UpdateColumn5FromCarihareketler;
                    this.Hide();
                    cariHareketForm.ShowDialog();
                    this.Show();
                    RefreshDataGridView();
                    UpdateColumn5();
                    // UpdateButton13 metodunu çağırarak button13'ü güncelle
                    cariHareketForm.UpdateButton13();
                }
                else
                {
                    MessageBox.Show("Seçili satırda gerekli bilgiler eksik.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Lütfen bir satır seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void UpdateColumn5FromCarihareketler(string cariKodu, decimal tutar)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["CariKodu"].Value?.ToString() == cariKodu)
                {
                    // Mevcut bakiyeyi al
                    string bakiyeText = row.Cells["Bakiye"].Value?.ToString();
                    decimal mevcutBakiye = decimal.TryParse(bakiyeText, out var result) ? result : 0;
                    // Yeni bakiyeyi hesapla
                    decimal yeniBakiye = mevcutBakiye + tutar;
                    // Doğru sütunu güncelle (Bakiye sütunu)
                    row.Cells["Bakiye"].Value = yeniBakiye.ToString("C2");
                    break;
                }
            }
        }

        public void UpdateDataGridView()
        {
            try
            {
                // Var olan bağlantıyı yenile
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();
                // Yeniden veritabanını yükle
                LoadCariList(); // Verileri yeniden doldurur
                // Kullanıcıya başarı mesajı
                MessageBox.Show("DataGridView başarıyla güncellendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Hata durumunda kullanıcıyı bilgilendir
                MessageBox.Show($"DataGridView güncellenirken bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // BtnDegistir Click Olayı
        public void BtnDegistir_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Index != -1)
            {
                // Seçilen cariye ait CariKodu'nu al
                string cariKodu = dataGridView1.CurrentRow.Cells["CariKodu"].Value.ToString();
                // FormCariEkle'yi düzenleme modu ile aç
                FormCariEkle cariEkleFormu = new FormCariEkle(cariKodu); // CariKodu ile açılıyor
                cariEkleFormu.ShowDialog();
                // Cari listesini yenile
                LoadCariList();
            }
            else
            {
                MessageBox.Show("Lütfen değiştirmek istediğiniz cariyi seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // BtnEkle Click Olayı
        public void BtnEkle_Click(object sender, EventArgs e)
        {
            FormCariEkle cariEkleFormu = new FormCariEkle();
            cariEkleFormu.ShowDialog();
            LoadCariList();
        }

        public void BtnSil_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Index != -1)
            {
                string cariKodu = dataGridView1.CurrentRow.Cells["CariKodu"].Value.ToString();
                string firmaAdi = dataGridView1.CurrentRow.Cells["Unvani"].Value.ToString();
                DialogResult result = MessageBox.Show(
                    $"Bu kaydı ve bu cariye ait tüm hareketleri silmek istediğinize emin misiniz?\n\nCari Kod: {cariKodu}\nFirma Adı: {firmaAdi}",
                    "Silme Onayı",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
                    using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                    {
                        connection.Open();
                        // Transaction başlatılır
                        using (SQLiteTransaction transaction = connection.BeginTransaction())
                        {
                            try
                            {
                                // `cari_hareketleri` tablosundan ilişkili kayıtları sil
                                string deleteHareketQuery = "DELETE FROM cari_hareketleri WHERE cari_kodu = @CariKodu";
                                using (SQLiteCommand hareketCommand = new SQLiteCommand(deleteHareketQuery, connection, transaction))
                                {
                                    hareketCommand.Parameters.AddWithValue("@CariKodu", cariKodu);
                                    hareketCommand.ExecuteNonQuery();
                                }
                                // `cari` tablosundan ilgili cariyi sil
                                string deleteCariQuery = "DELETE FROM Cari WHERE CariKodu = @CariKodu";
                                using (SQLiteCommand cariCommand = new SQLiteCommand(deleteCariQuery, connection, transaction))
                                {
                                    cariCommand.Parameters.AddWithValue("@CariKodu", cariKodu);
                                    int rowsAffected = cariCommand.ExecuteNonQuery();
                                    if (rowsAffected > 0)
                                    {
                                        MessageBox.Show("Cari ve ilişkili hareketleri başarıyla silindi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Cari bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                                // İşlemleri onayla
                                transaction.Commit();
                                // Cari listesini yeniden yükle
                                LoadCariList();
                            }
                            catch (Exception ex)
                            {
                                // Hata durumunda işlemleri geri al
                                transaction.Rollback();
                                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Silmek istediğiniz cariyi seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void Form4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                BtnSil_Click(sender, e);
            }
            if (e.KeyCode == Keys.F3)
            {
                Hareketler hareketlerForm = new Hareketler();
                hareketlerForm.StartPosition = FormStartPosition.CenterScreen;
                hareketlerForm.ShowDialog(); // Hareketler formunu modally açar
            }
            if (e.KeyCode == Keys.F4)
            {
                OpenBackupRestoreForm();
            }
            // Form4'te F11 tuşuna basıldığında
            if (e.KeyCode == Keys.F11)
            {
                // Postakip formunu açıyoruz
                postakip postakipForm = new postakip();  // Postakip formunun örneğini oluşturuyoruz
                postakipForm.ShowDialog();  // Formu açıyoruz
                // BankalariYukle metodunu çağırıyoruz
                postakipForm.BankalariYukle();  // Verileri yükle
            }
            if (e.KeyCode == Keys.F12)
            {
                HesapMakinesi hesapMakinesi = new HesapMakinesi();
                hesapMakinesi.ShowDialog();
                e.Handled = true;
            }
        }

        public void yedekİşlemleriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenBackupRestoreForm();
        }

        internal void OpenBackupRestoreForm()
        {
            string firmaAdi = FirmaAdi; // Başlıkta belirtilen firma adını al
            BackupRestoreForm backupRestoreForm = new BackupRestoreForm(firmaAdi);
            // Restore tamamlandığında DataGridView'i güncellemek için event'e abone ol
            backupRestoreForm.OnRestoreCompleted += () =>
            {
                RefreshDataGridView();
                MessageBox.Show("Tüm Cariler Güncellendi.!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };
            backupRestoreForm.ShowDialog(); // Modal olarak göster
        }

        public void DataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // Hataları yoksay ve kullanıcıya uyarı gösterme
            e.ThrowException = false;
            Console.WriteLine($"Hata: {e.Exception.Message} - Sütun: {e.ColumnIndex}, Satır: {e.RowIndex}");
        }

        // DataGridView1 CellContentClick Olayı
        public void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // İçerik hücre tıklama işlemleri buraya
        }

        // DataGridView1 SelectionChanged Olayı
        public void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null && dataGridView1.Columns.Contains("CariKodu"))
            {
                currentCariKodu = dataGridView1.CurrentRow.Cells["CariKodu"].Value?.ToString();
            }
        }

        // FirmaHesaplarıToolStripMenuItem Click Olayı
        public void FirmaHesaplarıToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Firma hesapları formunu aç
            FormFirmaHesaplari firmaHesaplari = new FormFirmaHesaplari();
            firmaHesaplari.ShowDialog();
        }

        public void cmbFiltre_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedFilter = cmbFiltre.SelectedItem?.ToString(); // Combobox'tan seçili değeri al
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue; // Yeni satır kontrolü (ek satır varsa göz ardı et)
                // Bakiye sütunundaki veriyi al ve temizle
                string bakiyeStr = row.Cells["Bakiye"].Value?.ToString()?.Replace("₺", "").Trim();
                // Bakiye geçerli bir sayı mı kontrol et
                if (!decimal.TryParse(bakiyeStr, out decimal bakiye))
                {
                    row.Visible = false; // Geçersiz veriler gizlenir
                    continue;
                }
                // Filtreleme işlemi
                if (selectedFilter == "Tümü..")
                {
                    row.Visible = true; // Borçlular ve alacaklılar // Borçlu ve alacaklı tüm hesaplar gösterilir
                }
                else if (selectedFilter == "Sadece Borçlular")
                {
                    row.Visible = bakiye > 0; // Sadece pozitif bakiyeli hesaplar
                }
                else if (selectedFilter == "Sadece Alacaklılar")
                {
                    row.Visible = bakiye < 0; // Sadece negatif bakiyeli hesaplar
                }
            }
        }

        public void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            float x = e.MarginBounds.Left; // Başlangıç X konumu
            float y = e.MarginBounds.Top; // Başlangıç Y konumu
            float lineHeight = font.GetHeight(e.Graphics); // Satır yüksekliği
            // Sütun genişliklerini tanımlayın
            float[] columnWidths = { 100, 100, 150, 100, 150, 100 }; // Sütun genişlikleri (piksel)
            // Başlıkları yazdır
            if (currentRow == 0)
            {
                for (int col = 0; col < raporVerileri.GetLength(1); col++)
                {
                    e.Graphics.DrawString(
                        raporVerileri[0, col],
                        font,
                        Brushes.Black,
                        x,
                        y
                    );
                    x += columnWidths[col]; // X pozisyonunu bir sonraki sütuna kaydır
                }
                y += lineHeight; // Başlık sonrası bir satır alta in
                x = e.MarginBounds.Left; // X konumunu sıfırla
                currentRow++; // Bir sonraki satıra geç
            }
            // İçeriği yazdır
            while (currentRow < raporVerileri.GetLength(0))
            {
                for (int col = 0; col < raporVerileri.GetLength(1); col++)
                {
                    e.Graphics.DrawString(
                        raporVerileri[currentRow, col],
                        font,
                        Brushes.Black,
                        x,
                        y
                    );
                    x += columnWidths[col]; // X pozisyonunu bir sonraki sütuna kaydır
                }
                y += lineHeight; // Bir sonraki satıra geç
                x = e.MarginBounds.Left; // X konumunu sıfırla
                currentRow++;
                // Sayfa sınırını kontrol et
                if (y + lineHeight > e.MarginBounds.Bottom)
                {
                    e.HasMorePages = true; // Daha fazla sayfa varsa
                    return;
                }
            }
            // Tüm satırlar yazdırıldı
            e.HasMorePages = false;
            currentRow = 0; // Yeni yazdırma için sıfırla
        }

        public void HazirlaRaporVerileri()
        {
            DataGridView dgv = dataGridView1; // Tablonuzun adı
            // DataGridView'deki verileri diziye aktar
            int rows = dgv.Rows.Count;
            int cols = dgv.Columns.Count;
            raporVerileri = new string[rows + 1, cols];
            // Başlıkları ekle
            for (int c = 0; c < cols; c++)
            {
                raporVerileri[0, c] = dgv.Columns[c].HeaderText;
            }
            // Satırları ekle
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    raporVerileri[r + 1, c] = dgv.Rows[r].Cells[c].Value?.ToString() ?? "";
                }
            }
        }

        public void hareketlerF3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Hareketler formunu oluştur
            Hareketler hareketlerForm = new Hareketler();
            // Formun ekranın ortasında açılması için ayarla
            hareketlerForm.StartPosition = FormStartPosition.CenterScreen;
            // Formu göster
            hareketlerForm.ShowDialog(); // Modally açar
        }

        public void yedekAlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenBackupRestoreForm();
        }

        public List<BorcKaydi> GetOdemeYapmayanCariler()
        {
            List<BorcKaydi> borclular = new List<BorcKaydi>();
            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = @"
                    SELECT C.CariKodu, C.Unvani,
                           C.Bakiye AS Bakiye
                    FROM Cari C
                    WHERE C.CariKodu NOT IN (
                        SELECT DISTINCT Cari_Kodu
                        FROM cari_hareketleri
                        WHERE Tur = 'Tahsilat Dekontu'
                        AND Tarih >= DATE('now', '-30 days')
                    )
                    AND C.Bakiye > 0
                    ORDER BY C.Bakiye DESC";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            borclular.Add(new BorcKaydi
                            {
                                CariKodu = reader["CariKodu"].ToString(),
                                Unvan = reader["Unvani"].ToString(),
                                Bakiye = reader["Bakiye"] != DBNull.Value ? Convert.ToDecimal(reader["Bakiye"]) : 0
                            });
                        }
                    }
                }
            }
            return borclular;
        }

        public void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void hakkımızdaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Programın mevcut sürümünü al
            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            // Bilgi mesajı
            string mesaj = $"Bu program Muzaffer Erdoğan tarafından geliştirilmiştir.\n\n" +
                           $"📌 Güncel Sürüm: {version}\n" +
                           $"📅 Son Güncelleme: {DateTime.Now.Year}\n\n" +
                           $"© {DateTime.Now.Year} Tüm Hakları Saklıdır.";
            MessageBox.Show(mesaj, "Hakkımızda", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        internal void DownloadAndInstallUpdate(string downloadUrl)
        {
            try
            {
                // 📂 Masaüstü konumunu al
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string fileName = "Veresiye2025_Update.exe"; // Güncelleme dosyasının adı
                string filePath = System.IO.Path.Combine(desktopPath, fileName); // Masaüstüne kaydet
                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(downloadUrl, filePath); // Güncellemeyi indir
                }
                // ✅ Kullanıcıya uyarı mesajı ver
                MessageBox.Show(
                    $"✅ Güncelleme başarıyla indirildi!\n\n📂 **Dosya Konumu:** {filePath}\n\n⚠️ **Güncellemeyi yüklemeden önce yedek aldığınızdan emin olun!**\n\n🔄 Güncellemek için masaüstündeki **Veresiye2025_Update.exe** dosyasını çalıştırın.",
                    "Güncelleme İndirildi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Güncelleme indirilemedi: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void güncellemeDenetleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string currentVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString(); // Mevcut sürüm
            string updateUrl = "http://localhost/veresiye/versiyon.txt"; // 🔥 Yeni yol
            try
            {
                using (WebClient client = new WebClient())
                {
                    string rawData = client.DownloadString(updateUrl).Trim(); // `versiyon.txt` oku
                    // Eğer boş veya eksik veri geldiyse
                    if (string.IsNullOrWhiteSpace(rawData))
                    {
                        MessageBox.Show("Güncelleme bilgisi alınamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    string[] versionInfo = rawData.Split('\n'); // Satırları ayır
                    string latestVersion = versionInfo[0].Trim(); // İlk satır: Yeni sürüm
                    string downloadLink = versionInfo.Length > 1 ? versionInfo[1].Trim() : ""; // İkinci satır: Güncelleme linki
                    // Eğer yeni sürüm varsa güncellemeyi teklif et
                    if (new Version(latestVersion) > new Version(currentVersion))
                    {
                        DialogResult result = MessageBox.Show(
                            $"Yeni bir güncelleme mevcut! ({latestVersion})\nŞu anki sürüm: {currentVersion}\n\nGüncellemeyi indirmek ister misiniz?",
                            "Güncelleme Mevcut",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Information
                        );
                        // Kullanıcı "Evet" derse güncellemeyi indir ve uygula
                        if (result == DialogResult.Yes && !string.IsNullOrEmpty(downloadLink))
                        {
                            DownloadAndInstallUpdate(downloadLink);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Uygulamanız güncel.", "Güncelleme Kontrolü", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (WebException ex)
            {
                MessageBox.Show($"Güncelleme kontrol edilirken hata oluştu:\n{ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void şifreDeğiştirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateTime engelZamani = Properties.Settings.Default.SifreEngelZamani;
            if (DateTime.Now < engelZamani)
            {
                // 📌 Kalan süreyi hesapla
                TimeSpan kalanSure = engelZamani - DateTime.Now;
                MessageBox.Show($"Şifre değiştirme kilitli! {kalanSure.Minutes} dakika {kalanSure.Seconds} saniye sonra tekrar deneyin.",
                                "Erişim Engellendi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                FormSifreDegistir sifreDegistirForm = new FormSifreDegistir();
                sifreDegistirForm.ShowDialog();
            }
        }

        public void genelAyarlarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormGenelAyarlar formGenelAyarlar = new FormGenelAyarlar();
            formGenelAyarlar.ShowDialog(); // Moda olarak aç
        }

        public void posTakipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // posTakip formunu oluştur
            postakip posForm = new postakip();
            // Eski verileri sil
            posForm.SilGececegiTarihiGecmisVeriler(); // Eski verileri silen metodu çağır
            // Formu açmadan önce DataGridView'i güncelle
            posForm.BankalariYukle(); // Verileri yükle
            // Filtreleme koşullarını sıfırla
            posForm.ResetFilters(); // Yeni metodu çağır
            // Formu modal olarak aç
            posForm.ShowDialog(); // Yeni formu aç
        }

        public void kktakip_Click(object sender, EventArgs e)
        {
            // Karttakip formunun bir örneğini oluştur
            Karttakip karttakipForm = new Karttakip();
            // Formu göstermeden önce verileri yükle (opsiyonel, zaten Load'da yükleniyor)
            karttakipForm.KartlariYukle();
            // Karttakip formunu ShowDialog() ile göster
            karttakipForm.ShowDialog();
        }

        public void btnEkle_Click(object sender, EventArgs e)
        {
            FormCariEkle cariEkleFormu = new FormCariEkle();
            cariEkleFormu.ShowDialog();
            LoadCariList();
        }

        public void btnDegistir_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Index != -1)
            {
                // Seçilen cariye ait CariKodu'nu al
                string cariKodu = dataGridView1.CurrentRow.Cells["CariKodu"].Value.ToString();
                // FormCariEkle'yi düzenleme modu ile aç
                FormCariEkle cariEkleFormu = new FormCariEkle(cariKodu); // CariKodu ile açılıyor
                cariEkleFormu.ShowDialog();
                // Cari listesini yenile
                LoadCariList();
            }
            else
            {
                MessageBox.Show("Lütfen değiştirmek istediğiniz cariyi seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void btnSil_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Index != -1)
            {
                string cariKodu = dataGridView1.CurrentRow.Cells["CariKodu"].Value.ToString();
                string firmaAdi = dataGridView1.CurrentRow.Cells["Unvani"].Value.ToString();
                DialogResult result = MessageBox.Show(
                    $"Bu kaydı ve bu cariye ait tüm hareketleri silmek istediğinize emin misiniz?\n\nCari Kod: {cariKodu}\nFirma Adı: {firmaAdi}",
                    "Silme Onayı",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
                    using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                    {
                        connection.Open();
                        // Transaction başlatılır
                        using (SQLiteTransaction transaction = connection.BeginTransaction())
                        {
                            try
                            {
                                // `cari_hareketleri` tablosundan ilişkili kayıtları sil
                                string deleteHareketQuery = "DELETE FROM cari_hareketleri WHERE cari_kodu = @CariKodu";
                                using (SQLiteCommand hareketCommand = new SQLiteCommand(deleteHareketQuery, connection, transaction))
                                {
                                    hareketCommand.Parameters.AddWithValue("@CariKodu", cariKodu);
                                    hareketCommand.ExecuteNonQuery();
                                }
                                // `cari` tablosundan ilgili cariyi sil
                                string deleteCariQuery = "DELETE FROM Cari WHERE CariKodu = @CariKodu";
                                using (SQLiteCommand cariCommand = new SQLiteCommand(deleteCariQuery, connection, transaction))
                                {
                                    cariCommand.Parameters.AddWithValue("@CariKodu", cariKodu);
                                    int rowsAffected = cariCommand.ExecuteNonQuery();
                                    if (rowsAffected > 0)
                                    {
                                        MessageBox.Show("Cari ve ilişkili hareketleri başarıyla silindi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Cari bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                                // İşlemleri onayla
                                transaction.Commit();
                                // Cari listesini yeniden yükle
                                LoadCariList();
                            }
                            catch (Exception ex)
                            {
                                // Hata durumunda işlemleri geri al
                                transaction.Rollback();
                                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Silmek istediğiniz cariyi seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void BtnCarininHareketleriniAc_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Index >= 0)
            {
                string selectedCariKodu = dataGridView1.CurrentRow.Cells["CariKodu"].Value?.ToString();
                string selectedCariAdi = dataGridView1.CurrentRow.Cells["Unvani"].Value?.ToString();
                if (!string.IsNullOrEmpty(selectedCariKodu) && !string.IsNullOrEmpty(selectedCariAdi))
                {
                    // Cari hareket formunu aç
                    Carihareketler cariHareketForm = new Carihareketler(selectedCariKodu, cariKoduListesi, selectedCariAdi);
                    cariHareketForm.OnBakiyeGuncelle += UpdateColumn5FromCarihareketler;
                    this.Hide();
                    cariHareketForm.ShowDialog();
                    this.Show();
                    RefreshDataGridView();
                    UpdateColumn5();
                    // UpdateButton13 metodunu çağırarak button13'ü güncelle
                    cariHareketForm.UpdateButton13();
                }
                else
                {
                    MessageBox.Show("Seçili satırda gerekli bilgiler eksik.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Lütfen bir satır seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void alarmbildir_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Index >= 0)
            {
                string cariKodu = dataGridView1.CurrentRow.Cells["CariKodu"].Value?.ToString();
                string cariUnvani = dataGridView1.CurrentRow.Cells["Unvani"].Value?.ToString();
                Alarmkur alarmForm = new Alarmkur(cariUnvani, cariKodu);
                if (alarmForm.ShowDialog() == DialogResult.OK)
                {
                    KontrolEtAlarmlari();
                }
            }
            else
            {
                // Cari seçilmemiş, boş alarm formu aç
                Alarmkur alarmForm = new Alarmkur();
                if (alarmForm.ShowDialog() == DialogResult.OK)
                {
                    KontrolEtAlarmlari();
                }
            }
        }

        public void btnTopla_Click(object sender, EventArgs e)
        {
            decimal aktifBorclu = 0;
            decimal aktifAlacakli = 0;
            decimal pasifBorclu = 0;
            decimal pasifAlacakli = 0;
            decimal vadesiGecenToplam = GetVadesiGecenHesapToplam();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;
                string hesapDurumu = row.Cells["Hesap"].Value?.ToString() ?? "";
                string bakiyeStr = row.Cells["Bakiye"].Value?.ToString();
                decimal bakiye = 0;
                // Bakiye değerini parse etme
                if (decimal.TryParse(bakiyeStr, System.Globalization.NumberStyles.Currency, null, out decimal parsedBakiye))
                {
                    bakiye = parsedBakiye;
                }
                else
                {
                    continue;
                }
                // Hesap durumuna göre toplama
                if (hesapDurumu == "Aktif Hesap")
                {
                    if (bakiye < 0)
                        aktifAlacakli += bakiye;
                    else
                        aktifBorclu += bakiye;
                }
                else if (hesapDurumu == "Pasif Hesap")
                {
                    if (bakiye < 0)
                        pasifAlacakli += bakiye;
                    else
                        pasifBorclu += bakiye;
                }
            }
            // ✅ Vadesi Geçen Borçları Hesapla
            vadesiGecenToplam = GetVadesiGecenToplam(); // ✅ Eğer zaten tanımlıysa, sadece değer ata
            // ✅ FormToplam formunu aç ve hesaplamaları gönder
            FormToplam toplamForm = new FormToplam(aktifBorclu, aktifAlacakli, pasifBorclu, pasifAlacakli, vadesiGecenToplam);
            toplamForm.ShowDialog();
        }

        public void yazdir_Click(object sender, EventArgs e)
        {
            // Rapor verilerini hazırla
            HazirlaRaporVerileri();
            // PrintDocument olayını bağla
            printDocument.PrintPage += new PrintPageEventHandler(printDocument_PrintPage);
            // Önizleme penceresini göster
            PrintPreviewDialog previewDialog = new PrintPreviewDialog
            {
                Document = printDocument,
                Width = 800,
                Height = 600
            };
            if (previewDialog.ShowDialog() == DialogResult.OK)
            {
                // Yazdırma işlemini başlat
                printDocument.Print();
            }
        }

        

        #region Alarm Sistemi Metodları
        public void InitializeAlarmSystem()
        {
            // Alarm sistemi kurulumu
            AlarmTablosunuKontrolEt();
            // Timer başlat - sadece alt paneli güncellemek için
            alarmKontrolTimer = new System.Windows.Forms.Timer();
            alarmKontrolTimer.Interval = 30000; // 30 saniyede bir kontrol et
            alarmKontrolTimer.Tick += (s, e) => {
                KontrolEtAlarmlari(); // Sadece alt paneli güncelle
            };
            alarmKontrolTimer.Start();
            // İlk güncelleme
            KontrolEtAlarmlari();
        }

        public void AlarmKontrolTimer_Tick(object sender, EventArgs e)
        {
            KontrolEtAlarmlari();
            KontrolEtVeGosterYaklasanAlarmlari();
        }

        public void AlarmTablosunuKontrolEt()
        {
            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = @"CREATE TABLE IF NOT EXISTS Alarmlar (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    cari_kodu TEXT NOT NULL,
                    cari_unvan TEXT NOT NULL,
                    alarm_tarihi DATETIME NOT NULL,
                    mesaj TEXT NOT NULL,
                    odeme_turu TEXT NOT NULL,
                    onem_derecesi TEXT DEFAULT 'Normal',
                    bildirildi INTEGER DEFAULT 0,
                    durum TEXT DEFAULT 'Bekliyor',
                    olusturulma_tarihi DATETIME DEFAULT CURRENT_TIMESTAMP
                )";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        // Sınıf seviyesinde (Form4 sınıfının içinde, metodların dışında) tanımla
        // Sınıf seviyesinde tanımlanacak değişken
        public Label alarmBildirimLabel = null;

        public void KontrolEtAlarmlari()
        {
            // Sadece alt panel alarm yazısını güncelle
            GuncelleAlarmPanelIcerigi();
        }

        public void GosterAlarmDetay(int alarmId)
        {
            // Veritabanından alarm bilgilerini al
            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
            string cariUnvan = "", cariKodu = "", mesaj = "", odemeTuru = "", onemDerecesi = "";
            DateTime alarmTarihi = DateTime.Now;
            DateTime olusturulmaTarihi = DateTime.Now;
            DateTime? sonErtelemeTarihi = null;
            int ertelemeSayisi = 0;
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                // Önce tabloyu gerekli sütunlar ile güncelle
                try
                {
                    // Eğer sütunlar yoksa ekle
                    string alterTable1 = "ALTER TABLE Alarmlar ADD COLUMN olusturulma_tarihi DATETIME DEFAULT CURRENT_TIMESTAMP";
                    string alterTable2 = "ALTER TABLE Alarmlar ADD COLUMN erteleme_sayisi INTEGER DEFAULT 0";
                    string alterTable3 = "ALTER TABLE Alarmlar ADD COLUMN son_erteleme_tarihi DATETIME";
                    using (SQLiteCommand cmd1 = new SQLiteCommand(alterTable1, connection))
                    {
                        try { cmd1.ExecuteNonQuery(); } catch { /* Sütun zaten var olabilir */ }
                    }
                    using (SQLiteCommand cmd2 = new SQLiteCommand(alterTable2, connection))
                    {
                        try { cmd2.ExecuteNonQuery(); } catch { /* Sütun zaten var olabilir */ }
                    }
                    using (SQLiteCommand cmd3 = new SQLiteCommand(alterTable3, connection))
                    {
                        try { cmd3.ExecuteNonQuery(); } catch { /* Sütun zaten var olabilir */ }
                    }
                }
                catch { /* Hata olsa bile devam et */ }
                // Alarm verilerini çek
                string query = "SELECT * FROM Alarmlar WHERE id = @id";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", alarmId);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            cariUnvan = reader["cari_unvan"].ToString();
                            cariKodu = reader["cari_kodu"].ToString();
                            mesaj = reader["mesaj"].ToString();
                            odemeTuru = reader["odeme_turu"].ToString();
                            onemDerecesi = reader["onem_derecesi"].ToString();
                            alarmTarihi = Convert.ToDateTime(reader["alarm_tarihi"]);
                            // Yeni alanları oku
                            if (reader["olusturulma_tarihi"] != DBNull.Value)
                                olusturulmaTarihi = Convert.ToDateTime(reader["olusturulma_tarihi"]);
                            if (reader["erteleme_sayisi"] != DBNull.Value)
                                ertelemeSayisi = Convert.ToInt32(reader["erteleme_sayisi"]);
                            if (reader["son_erteleme_tarihi"] != DBNull.Value)
                                sonErtelemeTarihi = Convert.ToDateTime(reader["son_erteleme_tarihi"]);
                        }
                        else
                        {
                            MessageBox.Show("Alarm bilgileri bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
            }
            // Form boyutunu ayarla
            int formWidth = 450;
            int formHeight = 470; // 430'dan 450'ye çıkardık
            // Ana form
            Form detayForm = new Form
            {
                Text = "Alarm Detayı",
                Size = new Size(formWidth, formHeight),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                BackColor = Color.FromArgb(50, 50, 50),
                ForeColor = Color.White
            };
            // HEADER PANEL - Üst mavi panel
            Panel headerPanel = new Panel
            {
                Size = new Size(formWidth, 80),
                Location = new Point(0, 0),
                BackColor = Color.FromArgb(0, 120, 215)
            };
            // CONTENT PANEL - Orta gri içerik paneli
            Panel contentPanel = new Panel
            {
                Size = new Size(formWidth, 290), // 270'den 290'a çıkardık
                Location = new Point(0, 80),
                BackColor = Color.FromArgb(40, 40, 40)
            };
            // BUTTON PANEL - Alt siyah buton paneli
            Panel buttonPanel = new Panel
            {
                Size = new Size(formWidth, 60),
                Location = new Point(0, 370), // 330'dan 370'e çıkardık
                BackColor = Color.FromArgb(30, 30, 30)
            };
            // HEADER İÇERİĞİ
            Label lblCariUnvan = new Label
            {
                Text = cariUnvan,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.White,
                Size = new Size(400, 40),
                Location = new Point(25, 20),
                AutoEllipsis = true
            };
            Label lblTarih = new Label
            {
                Text = alarmTarihi.ToString("dd MMMM yyyy HH:mm"),
                Font = new Font("Segoe UI", 11),
                ForeColor = Color.WhiteSmoke,
                Size = new Size(400, 25),
                Location = new Point(25, 60)
            };
            // İÇERİK ALANI ETİKETLERİ
            // 1. Açıklama
            Label lblAciklamaTitle = new Label
            {
                Text = "Açıklama",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.Gray,
                Location = new Point(25, 20),
                AutoSize = true
            };
            Label lblAciklamaValue = new Label
            {
                Text = mesaj,
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.White,
                Size = new Size(400, 25),
                Location = new Point(25, 45),
                AutoEllipsis = true
            };
            // 2. Ödeme Türü
            Label lblOdemeTuruTitle = new Label
            {
                Text = "Ödeme Türü",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.Gray,
                Location = new Point(25, 85),
                AutoSize = true
            };
            Label lblOdemeTuruValue = new Label
            {
                Text = odemeTuru,
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.White,
                Location = new Point(25, 110),
                AutoSize = true
            };
            // 3. Önem Derecesi
            Label lblOnemDerecesiTitle = new Label
            {
                Text = "Önem Derecesi",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.Gray,
                Location = new Point(250, 85),
                AutoSize = true
            };
            Label lblOnemDerecesiValue = new Label
            {
                Text = onemDerecesi,
                Font = new Font("Segoe UI", 12),
                ForeColor = onemDerecesi == "Kritik" ? Color.Red : (onemDerecesi == "Yüksek" ? Color.Orange : Color.DeepSkyBlue),
                Location = new Point(250, 110),
                AutoSize = true
            };
            // 4. Oluşturulma Tarihi
            Label lblOlusturmaTarihiTitle = new Label
            {
                Text = "Oluşturulma Tarihi",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.Gray,
                Location = new Point(25, 150),
                AutoSize = true
            };
            Label lblOlusturmaTarihiValue = new Label
            {
                Text = olusturulmaTarihi.ToString("dd.MM.yyyy HH:mm"),
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.White,
                Location = new Point(25, 175),
                AutoSize = true
            };
            // 5. Erteleme Bilgileri
            Label lblErteleTitle = new Label
            {
                Text = "Erteleme Bilgisi",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.Gray,
                Location = new Point(250, 150),
                AutoSize = true
            };
            Label lblErteleValue = new Label
            {
                Text = ertelemeSayisi > 0 ?
                    $"{ertelemeSayisi} kez ertelendi" :
                    "Ertelenmedi",
                Font = new Font("Segoe UI", 12),
                ForeColor = ertelemeSayisi > 0 ? Color.Orange : Color.White,
                Location = new Point(250, 175),
                AutoSize = true
            };
            // 6. Son Erteleme Tarihi
            Label lblSonErtelemeTitle = new Label
            {
                Text = "Son Erteleme",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.Gray,
                Location = new Point(25, 215),
                AutoSize = true,
                Visible = sonErtelemeTarihi.HasValue
            };
            Label lblSonErtelemeValue = new Label
            {
                Text = sonErtelemeTarihi.HasValue ?
                    sonErtelemeTarihi.Value.ToString("dd.MM.yyyy HH:mm") :
                    "",
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.White,
                Location = new Point(25, 240),
                AutoSize = true,
                Visible = sonErtelemeTarihi.HasValue
            };
            // 7. Ertelenen Vakit Bilgisi
            Label lblErtelenenVakitTitle = new Label
            {
                Text = "Ertelenen Vakit",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.Gray,
                Location = new Point(250, 215),
                AutoSize = true,
                Visible = sonErtelemeTarihi.HasValue && ertelemeSayisi > 0
            };
            // Erteleme süresini hesapla ve göster
            string ertelemeMetni = "";
            if (sonErtelemeTarihi.HasValue && ertelemeSayisi > 0)
            {
                // Son erteleme tarihinden alarm tarihine kadar olan süreyi hesapla
                TimeSpan ertelemeSuresi = alarmTarihi - sonErtelemeTarihi.Value;
                // Erteleme süresine göre metin belirleme (15 dk, 30 dk, 1 saat, 2 saat, 4 saat, vb.)
                if (Math.Abs(ertelemeSuresi.TotalMinutes - 15) < 1)
                {
                    ertelemeMetni = "15 dakika sonra";
                }
                else if (Math.Abs(ertelemeSuresi.TotalMinutes - 30) < 1)
                {
                    ertelemeMetni = "30 dakika sonra";
                }
                else if (Math.Abs(ertelemeSuresi.TotalHours - 1) < 0.1)
                {
                    ertelemeMetni = "1 saat sonra";
                }
                else if (Math.Abs(ertelemeSuresi.TotalHours - 2) < 0.1)
                {
                    ertelemeMetni = "2 saat sonra";
                }
                else if (Math.Abs(ertelemeSuresi.TotalHours - 4) < 0.1)
                {
                    ertelemeMetni = "4 saat sonra";
                }
                else if (Math.Abs(ertelemeSuresi.TotalDays - 1) < 0.1)
                {
                    ertelemeMetni = "Yarın aynı saatte";
                }
                else
                {
                    // Standart sürelere uymuyorsa, kesin süreyi göster
                    if (ertelemeSuresi.TotalDays >= 1)
                    {
                        ertelemeMetni = $"{(int)ertelemeSuresi.TotalDays} gün sonra";
                    }
                    else if (ertelemeSuresi.TotalHours >= 1)
                    {
                        ertelemeMetni = $"{(int)ertelemeSuresi.TotalHours} saat sonra";
                    }
                    else
                    {
                        ertelemeMetni = $"{(int)ertelemeSuresi.TotalMinutes} dakika sonra";
                    }
                }
            }
            Label lblErtelenenVakitValue = new Label
            {
                Text = alarmTarihi.ToString("dd MMMM yyyy HH:mm"),
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.Yellow,
                Location = new Point(250, 240),
                AutoSize = true,
                Visible = sonErtelemeTarihi.HasValue && ertelemeSayisi > 0
            };
            // BUTONLAR - Eşit aralıklı
            int buttonWidth = 120;
            int buttonSpacing = 25;
            int totalButtons = 3;
            int totalButtonWidth = (buttonWidth * totalButtons) + (buttonSpacing * (totalButtons - 1));
            int startX = (formWidth - totalButtonWidth) / 2;
            // Ertele Butonu - Mavi
            Button btnErtele = new Button
            {
                Text = "Ertele",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Size = new Size(buttonWidth, 40),
                Location = new Point(startX, 15),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnErtele.FlatAppearance.BorderSize = 0;
            // Tamamlandı Butonu - Yeşil
            Button btnTamamlandi = new Button
            {
                Text = "Tamamlandı",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Size = new Size(buttonWidth, 40),
                Location = new Point(startX + buttonWidth + buttonSpacing, 15),
                BackColor = Color.FromArgb(46, 184, 46),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnTamamlandi.FlatAppearance.BorderSize = 0;
            // Sil Butonu - Kırmızı
            Button btnSil = new Button
            {
                Text = "Sil",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Size = new Size(buttonWidth, 40),
                Location = new Point(startX + (buttonWidth + buttonSpacing) * 2, 15),
                BackColor = Color.FromArgb(232, 17, 35),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnSil.FlatAppearance.BorderSize = 0;
            // OLAYLAR
            btnTamamlandi.Click += (s, e) =>
            {
                if (GuncelleAlarmDurumu(alarmId, "Tamamlandı"))
                {
                    MessageBox.Show("Alarm tamamlandı olarak işaretlendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GuncelleAlarmPanelIcerigi();
                    KontrolEtAlarmlari();
                    detayForm.Close();
                }
            };
            btnErtele.Click += (s, e) =>
            {
                ErtelemeSureSecimFormuGoster(alarmId, detayForm, ertelemeSayisi);
            };
            btnSil.Click += (s, e) =>
            {
                DialogResult result = MessageBox.Show("Bu alarmı silmek istediğinizden emin misiniz?",
                                                     "Alarm Sil",
                                                     MessageBoxButtons.YesNo,
                                                     MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (SilAlarm(alarmId))
                    {
                        MessageBox.Show("Alarm başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        GuncelleAlarmPanelIcerigi();
                        KontrolEtAlarmlari();
                        detayForm.Close();
                    }
                }
            };
            // KONTROLLERİ FORMA EKLE
            // Header panel içeriği
            headerPanel.Controls.Add(lblCariUnvan);
            headerPanel.Controls.Add(lblTarih);
            // Content panel içeriği
            contentPanel.Controls.Add(lblAciklamaTitle);
            contentPanel.Controls.Add(lblAciklamaValue);
            contentPanel.Controls.Add(lblOdemeTuruTitle);
            contentPanel.Controls.Add(lblOdemeTuruValue);
            contentPanel.Controls.Add(lblOnemDerecesiTitle);
            contentPanel.Controls.Add(lblOnemDerecesiValue);
            contentPanel.Controls.Add(lblOlusturmaTarihiTitle);
            contentPanel.Controls.Add(lblOlusturmaTarihiValue);
            contentPanel.Controls.Add(lblErteleTitle);
            contentPanel.Controls.Add(lblErteleValue);
            // Son erteleme tarihi varsa göster
            if (sonErtelemeTarihi.HasValue)
            {
                contentPanel.Controls.Add(lblSonErtelemeTitle);
                contentPanel.Controls.Add(lblSonErtelemeValue);
                // Ertelenen vakit bilgisi
                if (ertelemeSayisi > 0)
                {
                    contentPanel.Controls.Add(lblErtelenenVakitTitle);
                    contentPanel.Controls.Add(lblErtelenenVakitValue);
                }
            }
            // Button panel içeriği
            buttonPanel.Controls.Add(btnErtele);
            buttonPanel.Controls.Add(btnTamamlandi);
            buttonPanel.Controls.Add(btnSil);
            // Panelleri forma ekle
            detayForm.Controls.Add(headerPanel);
            detayForm.Controls.Add(contentPanel);
            detayForm.Controls.Add(buttonPanel);
            // Formu göster
            detayForm.ShowDialog();
        }

        public void ErtelemeSureSecimFormuGoster(int alarmId, Form parentForm, int mevcutErtelemeSayisi = 0)
        {
            // Form boyutları
            int formWidth = 300;
            int formHeight = 200;
            Form ertelemeForm = new Form
            {
                Text = "Alarmı Ertele",
                Size = new Size(formWidth, formHeight),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                BackColor = Color.FromArgb(45, 45, 48),
                ForeColor = Color.White
            };
            Label lblAciklama = new Label
            {
                Text = "Erteleme süresini seçin:",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 20)
            };
            ComboBox cmbSure = new ComboBox
            {
                Size = new Size(260, 30),
                Location = new Point(20, 50),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10),
                BackColor = Color.FromArgb(60, 60, 63),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            cmbSure.Items.Add("15 dakika");
            cmbSure.Items.Add("30 dakika");
            cmbSure.Items.Add("1 saat");
            cmbSure.Items.Add("2 saat");
            cmbSure.Items.Add("4 saat");
            cmbSure.Items.Add("Yarın aynı saatte");
            cmbSure.SelectedIndex = 0; // Varsayılan 15 dakika
            Button btnTamam = new Button
            {
                Text = "Tamam",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Size = new Size(100, 35),
                Location = new Point(20, 100),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnTamam.FlatAppearance.BorderSize = 0;
            Button btnVazgec = new Button
            {
                Text = "Vazgeç",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Size = new Size(100, 35),
                Location = new Point(180, 100),
                BackColor = Color.FromArgb(100, 100, 100),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnVazgec.FlatAppearance.BorderSize = 0;
            btnTamam.Click += (s, e) =>
            {
                int selectedIndex = cmbSure.SelectedIndex;
                DateTime yeniTarih = DateTime.Now;
                switch (selectedIndex)
                {
                    case 0: // 15 dakika
                        yeniTarih = DateTime.Now.AddMinutes(15);
                        break;
                    case 1: // 30 dakika
                        yeniTarih = DateTime.Now.AddMinutes(30);
                        break;
                    case 2: // 1 saat
                        yeniTarih = DateTime.Now.AddHours(1);
                        break;
                    case 3: // 2 saat
                        yeniTarih = DateTime.Now.AddHours(2);
                        break;
                    case 4: // 4 saat
                        yeniTarih = DateTime.Now.AddHours(4);
                        break;
                    case 5: // Yarın aynı saatte
                        yeniTarih = DateTime.Now.AddDays(1);
                        break;
                    default:
                        yeniTarih = DateTime.Now.AddMinutes(15);
                        break;
                }
                if (ErteleAlarm(alarmId, yeniTarih, mevcutErtelemeSayisi + 1))
                {
                    GuncelleAlarmPanelIcerigi();
                    KontrolEtAlarmlari();
                    ertelemeForm.Close();
                    parentForm.Close();
                }
            };
            btnVazgec.Click += (s, e) =>
            {
                ertelemeForm.Close();
            };
            ertelemeForm.Controls.Add(lblAciklama);
            ertelemeForm.Controls.Add(cmbSure);
            ertelemeForm.Controls.Add(btnTamam);
            ertelemeForm.Controls.Add(btnVazgec);
            ertelemeForm.ShowDialog(parentForm);
        }

        public bool ErteleAlarm(int alarmId, DateTime yeniTarih, int ertelemeSayisi)
        {
            try
            {
                string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = @"UPDATE Alarmlar
                           SET alarm_tarihi = @yeniTarih,
                               bildirildi = 0,
                               erteleme_sayisi = @ertelemeSayisi,
                               son_erteleme_tarihi = @sonErtelemeTarihi
                           WHERE id = @id";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@yeniTarih", yeniTarih.ToString("yyyy-MM-dd HH:mm:ss"));
                        command.Parameters.AddWithValue("@ertelemeSayisi", ertelemeSayisi);
                        command.Parameters.AddWithValue("@sonErtelemeTarihi", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        command.Parameters.AddWithValue("@id", alarmId);
                        int result = command.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show($"Alarm {yeniTarih.ToString("dd.MM.yyyy HH:mm")} tarihine ertelendi.",
                                           "Alarm Ertelendi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return true;
                        }
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Alarm ertelenirken bir hata oluştu: {ex.Message}", "Hata",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public int GetAktifAlarmSayisi()
        {
            int sayac = 0;
            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = @"SELECT COUNT(*) FROM Alarmlar
                               WHERE datetime(alarm_tarihi) <= datetime('now', '+3 days')
                               AND durum = 'Bekliyor'";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        sayac = Convert.ToInt32(result);
                    }
                }
            }
            return sayac;
        }

        public void KontrolEtVeGosterYaklasanAlarmlari()
        {
            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                // Bugün olan ve bildirilmemiş alarmları kontrol et
                string query = @"SELECT id, cari_kodu, cari_unvan, alarm_tarihi, mesaj, odeme_turu, onem_derecesi
                       FROM Alarmlar
                       WHERE date(alarm_tarihi) = date('now')
                       AND bildirildi = 0
                       AND durum = 'Bekliyor'";
                List<int> bildirimYapilanIds = new List<int>();
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["id"]);
                            string cariUnvan = reader["cari_unvan"].ToString();
                            string cariKodu = reader["cari_kodu"].ToString();
                            string mesaj = reader["mesaj"].ToString();
                            string odemeTuru = reader["odeme_turu"].ToString();
                            string onemDerecesi = reader["onem_derecesi"].ToString();
                            DateTime alarmTarihi = Convert.ToDateTime(reader["alarm_tarihi"]);
                            // Ses çal
                            CalSesliUyari(onemDerecesi);
                            // Gelişmiş bildirim göster
                            GosterInteraktifBildirim(id, cariUnvan, cariKodu, mesaj, odemeTuru, alarmTarihi);
                            bildirimYapilanIds.Add(id);
                            // Aynı anda çok fazla bildirimi önlemek için kısa bir bekleme ekle
                            Task.Delay(1500).Wait();
                        }
                    }
                }
                // Bildirilen alarmları güncelle
                if (bildirimYapilanIds.Count > 0)
                {
                    string updateQuery = $"UPDATE Alarmlar SET bildirildi = 1 WHERE id IN ({string.Join(",", bildirimYapilanIds)})";
                    using (SQLiteCommand updateCmd = new SQLiteCommand(updateQuery, connection))
                    {
                        updateCmd.ExecuteNonQuery();
                    }
                }
                // Yarın olan alarmları da kontrol et
                string queryYarin = @"SELECT COUNT(*)
                          FROM Alarmlar
                          WHERE date(alarm_tarihi) = date('now', '+1 day')
                          AND durum = 'Bekliyor'";
                using (SQLiteCommand cmd = new SQLiteCommand(queryYarin, connection))
                {
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        int yarinAlarmSayisi = Convert.ToInt32(result);
                        if (yarinAlarmSayisi > 0)
                        {
                            // Bildirim göster
                            GosterBildirim("YARIN İÇİN ÖDEME HATIRLATICI",
                                           $"Yarın için {yarinAlarmSayisi} adet ödeme hatırlatıcısı bulunuyor.",
                                           ToolTipIcon.Info);
                        }
                    }
                }
            }
        }

        // Önem derecesine göre farklı sesler çal
        public void CalSesliUyari(string onemDerecesi)
        {
            try
            {
                // Ses dosyalarını uygulama klasörünüzde bulundurun
                string sesDosyasi = "";
                switch (onemDerecesi)
                {
                    case "Kritik":
                        sesDosyasi = Application.StartupPath + "\\sounds\\kritik_alarm.wav";
                        break;
                    case "Yüksek":
                        sesDosyasi = Application.StartupPath + "\\sounds\\yuksek_alarm.wav";
                        break;
                    default: // Normal
                        sesDosyasi = Application.StartupPath + "\\sounds\\normal_alarm.wav";
                        break;
                }
                // Eğer ses dosyası mevcutsa çal
                if (System.IO.File.Exists(sesDosyasi))
                {
                    System.Media.SoundPlayer player = new System.Media.SoundPlayer(sesDosyasi);
                    player.Play();
                }
                else
                {
                    // Varsayılan sistem sesi
                    System.Media.SystemSounds.Question.Play();
                }
            }
            catch
            {
                // Ses çalma başarısız olursa varsayılan sistem sesini çal
                System.Media.SystemSounds.Exclamation.Play();
            }
        }

        // İnteraktif bildirim göster
        public void GosterInteraktifBildirim(int alarmId, string cariUnvan, string cariKodu, string mesaj, string odemeTuru, DateTime alarmTarihi)
        {
            // Özel bildirim formu oluştur
            Form bildirimForm = new Form
            {
                Text = "Veresiye2025 Alarm",
                Size = new Size(400, 250),
                StartPosition = FormStartPosition.Manual,
                FormBorderStyle = FormBorderStyle.FixedSingle,
                MaximizeBox = false,
                MinimizeBox = false,
                ControlBox = true,
                BackColor = Color.FromArgb(45, 45, 48),
                ForeColor = Color.White,
                TopMost = true
            };
            // Ekranın sağ alt köşesinde göster
            Rectangle workingArea = Screen.GetWorkingArea(this);
            bildirimForm.Location = new Point(workingArea.Right - bildirimForm.Width - 10,
                                             workingArea.Bottom - bildirimForm.Height - 10);
            // Form içeriği
            PictureBox pictureBox = new PictureBox
            {
                BackColor = Color.Transparent,
                Size = new Size(60, 60),
                Location = new Point(20, 15),
                Image = Veresiye2025.Properties.Resources.clock,
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            // PictureBox'ı yuvarlak yapmak için
            pictureBox.Paint += (s, e) =>
            {
                GraphicsPath gp = new GraphicsPath();
                gp.AddEllipse(0, 0, pictureBox.Width - 1, pictureBox.Height - 1);
                Region region = new Region(gp);
                pictureBox.Region = region;
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.DrawEllipse(new Pen(Color.DodgerBlue, 2), 0, 0, pictureBox.Width - 1, pictureBox.Height - 1);
            };
            Label lblBaslik = new Label
            {
                Text = "Alarm Hatırlatıcı",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(100, 15)
            };
            Label lblCari = new Label
            {
                Text = cariUnvan,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 174, 239),
                AutoSize = true,
                Location = new Point(100, 45)
            };
            Label lblMesaj = new Label
            {
                Text = mesaj,
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                ForeColor = Color.White,
                Size = new Size(270, 40),
                Location = new Point(100, 70),
                AutoEllipsis = true
            };
            Label lblTarih = new Label
            {
                Text = $"Tarih: {alarmTarihi.ToString("dd.MM.yyyy HH:mm")}",
                Font = new Font("Segoe UI", 8, FontStyle.Italic),
                ForeColor = Color.LightGray,
                AutoSize = true,
                Location = new Point(100, 115)
            };
            Label lblOdemeTuru = new Label
            {
                Text = $"Ödeme Türü: {odemeTuru}",
                Font = new Font("Segoe UI", 8, FontStyle.Italic),
                ForeColor = Color.LightGray,
                AutoSize = true,
                Location = new Point(100, 135)
            };
            // Butonlar
            Guna.UI2.WinForms.Guna2Button btnTamamlandi = new Guna.UI2.WinForms.Guna2Button
            {
                Text = "Tamamlandı",
                Size = new Size(110, 35),
                Location = new Point(20, 170),
                BorderRadius = 8,
                FillColor = Color.FromArgb(0, 150, 0)
            };
            Guna.UI2.WinForms.Guna2Button btnErtele = new Guna.UI2.WinForms.Guna2Button
            {
                Text = "Ertele",
                Size = new Size(110, 35),
                Location = new Point(140, 170),
                BorderRadius = 8,
                FillColor = Color.FromArgb(0, 120, 215)
            };
            Guna.UI2.WinForms.Guna2Button btnSil = new Guna.UI2.WinForms.Guna2Button
            {
                Text = "Sil",
                Size = new Size(110, 35),
                Location = new Point(260, 170),
                BorderRadius = 8,
                FillColor = Color.FromArgb(200, 0, 0)
            };
            // Buton olayları
            btnTamamlandi.Click += (s, e) =>
            {
                GuncelleAlarmDurumu(alarmId, "Tamamlandı");
                KontrolEtAlarmlari();
                bildirimForm.Close();
            };
            btnErtele.Click += (s, e) =>
            {
                ErtelemeSureSecimFormuGoster(alarmId, bildirimForm);
            };
            btnSil.Click += (s, e) =>
            {
                if (MessageBox.Show("Bu alarmı silmek istediğinize emin misiniz?", "Alarm Sil",
                                 MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SilAlarm(alarmId);
                    KontrolEtAlarmlari();
                    bildirimForm.Close();
                }
            };
            // Kontrolleri forma ekle
            bildirimForm.Controls.Add(pictureBox);
            bildirimForm.Controls.Add(lblBaslik);
            bildirimForm.Controls.Add(lblCari);
            bildirimForm.Controls.Add(lblMesaj);
            bildirimForm.Controls.Add(lblTarih);
            bildirimForm.Controls.Add(lblOdemeTuru);
            bildirimForm.Controls.Add(btnTamamlandi);
            bildirimForm.Controls.Add(btnErtele);
            bildirimForm.Controls.Add(btnSil);
            // Otomatik kapanma için timer
            Timer kapanmaTimer = new Timer { Interval = 30000 }; // 30 saniye
            kapanmaTimer.Tick += (s, e) =>
            {
                bildirimForm.Close();
                kapanmaTimer.Stop();
                kapanmaTimer.Dispose();
            };
            kapanmaTimer.Start();
            // Form kapatıldığında timer'ı temizle
            bildirimForm.FormClosed += (s, e) =>
            {
                kapanmaTimer.Stop();
                kapanmaTimer.Dispose();
            };
            // Formu göster
            bildirimForm.Show();
        }

        // Erteleme için süre seçim formunu göster
        public void ErtelemeSureSecimFormuGoster(int alarmId, Form parentForm)
        {
            Form ertelemeForm = new Form
            {
                Text = "Alarmı Ertele",
                Size = new Size(300, 200),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                BackColor = Color.FromArgb(45, 45, 48),
                ForeColor = Color.White
            };
            Label lblAciklama = new Label
            {
                Text = "Erteleme süresini seçin:",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 20)
            };
            Guna.UI2.WinForms.Guna2ComboBox cmbSure = new Guna.UI2.WinForms.Guna2ComboBox
            {
                Size = new Size(250, 35),
                Location = new Point(20, 50),
                BorderRadius = 8,
                Font = new Font("Segoe UI", 10)
            };
            cmbSure.Items.Add("15 dakika");
            cmbSure.Items.Add("30 dakika");
            cmbSure.Items.Add("1 saat");
            cmbSure.Items.Add("2 saat");
            cmbSure.Items.Add("4 saat");
            cmbSure.Items.Add("Yarın aynı saatte");
            cmbSure.SelectedIndex = 0; // Varsayılan 15 dakika
            Guna.UI2.WinForms.Guna2Button btnTamam = new Guna.UI2.WinForms.Guna2Button
            {
                Text = "Tamam",
                Size = new Size(100, 35),
                Location = new Point(20, 100),
                BorderRadius = 8,
                FillColor = Color.FromArgb(0, 120, 215)
            };
            Guna.UI2.WinForms.Guna2Button btnVazgec = new Guna.UI2.WinForms.Guna2Button
            {
                Text = "Vazgeç",
                Size = new Size(100, 35),
                Location = new Point(170, 100),
                BorderRadius = 8,
                FillColor = Color.FromArgb(100, 100, 100)
            };
            btnTamam.Click += (s, e) =>
            {
                int selectedIndex = cmbSure.SelectedIndex;
                DateTime yeniTarih = DateTime.Now;
                switch (selectedIndex)
                {
                    case 0: // 15 dakika
                        yeniTarih = DateTime.Now.AddMinutes(15);
                        break;
                    case 1: // 30 dakika
                        yeniTarih = DateTime.Now.AddMinutes(30);
                        break;
                    case 2: // 1 saat
                        yeniTarih = DateTime.Now.AddHours(1);
                        break;
                    case 3: // 2 saat
                        yeniTarih = DateTime.Now.AddHours(2);
                        break;
                    case 4: // 4 saat
                        yeniTarih = DateTime.Now.AddHours(4);
                        break;
                    case 5: // Yarın aynı saatte
                        yeniTarih = DateTime.Now.AddDays(1);
                        break;
                    default:
                        yeniTarih = DateTime.Now.AddMinutes(15);
                        break;
                }
                ErteleAlarm(alarmId, yeniTarih);
                KontrolEtAlarmlari();
                ertelemeForm.Close();
                parentForm.Close();
            };
            btnVazgec.Click += (s, e) =>
            {
                ertelemeForm.Close();
            };
            ertelemeForm.Controls.Add(lblAciklama);
            ertelemeForm.Controls.Add(cmbSure);
            ertelemeForm.Controls.Add(btnTamam);
            ertelemeForm.Controls.Add(btnVazgec);
            ertelemeForm.ShowDialog(parentForm);
        }

        // Alarmı belirtilen tarihe ertele
        public bool ErteleAlarm(int alarmId, DateTime yeniTarih)
        {
            try
            {
                string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE Alarmlar SET alarm_tarihi = @yeniTarih, bildirildi = 0 WHERE id = @id";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@yeniTarih", yeniTarih.ToString("yyyy-MM-dd HH:mm:ss"));
                        command.Parameters.AddWithValue("@id", alarmId);
                        int result = command.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show($"Alarm {yeniTarih.ToString("dd.MM.yyyy HH:mm")} tarihine ertelendi.",
                                           "Alarm Ertelendi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return true;
                        }
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Alarm ertelenirken bir hata oluştu: {ex.Message}", "Hata",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Form kapanışında timer ve notifyIcon temizleme
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            // Timer ve NotifyIcon'ı temizle
            if (alarmKontrolTimer != null)
            {
                alarmKontrolTimer.Stop();
                alarmKontrolTimer.Dispose();
            }
            if (notifyIcon != null)
            {
                notifyIcon.Visible = false;
                notifyIcon.Dispose();
            }
        }

        // Form4 sınıfına eklenecek statik değişken
        public static Form _alarmListeForm = null;

        // Form4.cs içine
        public bool GosterAlarmListesi()
        {
            try
            {
                // Form4 içinde uygun bir AlarmListesiniGoster metodu çağrısı
                // Örneğin:
                Alarmkur alarm = new Alarmkur();
                alarm.AlarmListesiniGoster();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Form4.GosterAlarmListesi hatası: {ex.Message}");
                return false;
            }
        }

        // Form4 sınıfında güncellenecek GosterBildirim metodu
        public void GosterBildirim(string baslik, string mesaj, ToolTipIcon ikonTipi = ToolTipIcon.Info)
        {
            NotifyIcon notifyIcon = new NotifyIcon
            {
                Icon = SystemIcons.Information,
                Visible = true,
                BalloonTipTitle = baslik,
                BalloonTipText = mesaj,
                BalloonTipIcon = ikonTipi
            };
            // Bildirime tıklandığında alarm listesini göster
            notifyIcon.BalloonTipClicked += (sender, e) =>
            {
                this.Invoke(new Action(() =>
                {
                    // Form4'ten doğrudan GosterAlarmListesi metodunu çağır
                    GosterAlarmListesi();
                }));
            };
            notifyIcon.ShowBalloonTip(3000);
            // 5 saniye sonra gizle
            Task.Delay(5000).ContinueWith(t =>
            {
                notifyIcon.Visible = false;
                notifyIcon.Dispose();
            });
        }

        public void YukleAlarmVerileri(Guna.UI2.WinForms.Guna2DataGridView dgvAlarmlar)
        {
            dgvAlarmlar.Rows.Clear();
            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = @"SELECT id, cari_kodu, cari_unvan, alarm_tarihi, mesaj, odeme_turu, onem_derecesi, durum
                               FROM Alarmlar
                               ORDER BY datetime(alarm_tarihi) ASC, onem_derecesi DESC";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DateTime alarmTarihi = Convert.ToDateTime(reader["alarm_tarihi"]);
                            string durumText = reader["durum"].ToString();
                            int rowIndex = dgvAlarmlar.Rows.Add(
                                reader["id"],
                                reader["cari_unvan"].ToString(),
                                alarmTarihi.ToString("dd.MM.yyyy HH:mm"),
                                reader["odeme_turu"].ToString(),
                                reader["mesaj"].ToString(),
                                durumText
                            );
                            // Tamamlanmış alarmlar için stil
                            if (durumText == "Tamamlandı")
                            {
                                dgvAlarmlar.Rows[rowIndex].DefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Strikeout);
                                dgvAlarmlar.Rows[rowIndex].DefaultCellStyle.ForeColor = Color.Gray;
                            }
                            // Bugün olan alarmlar için stil
                            else if (alarmTarihi.Date == DateTime.Today)
                            {
                                dgvAlarmlar.Rows[rowIndex].DefaultCellStyle.ForeColor = Color.Crimson;
                                dgvAlarmlar.Rows[rowIndex].DefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                            }
                            // Geçmiş alarmlar için stil
                            else if (alarmTarihi.Date < DateTime.Today)
                            {
                                dgvAlarmlar.Rows[rowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 235, 235);
                            }
                            // Önem derecesine göre arka plan rengi
                            string onemDerecesi = reader["onem_derecesi"].ToString();
                            if (onemDerecesi == "Yüksek" && durumText != "Tamamlandı")
                            {
                                dgvAlarmlar.Rows[rowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 246, 222);
                            }
                            else if (onemDerecesi == "Kritik" && durumText != "Tamamlandı")
                            {
                                dgvAlarmlar.Rows[rowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 228, 225);
                            }
                        }
                    }
                }
            }
        }

        public bool GuncelleAlarmDurumu(int alarmId, string durum)
        {
            try
            {
                string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE Alarmlar SET durum = @durum WHERE id = @id";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@durum", durum);
                        command.Parameters.AddWithValue("@id", alarmId);
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Alarm durumu güncellenirken bir hata oluştu: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool SilAlarm(int alarmId)
        {
            try
            {
                DialogResult result = MessageBox.Show("Bu alarmı silmek istediğinize emin misiniz?",
                                                     "Alarm Sil",
                                                     MessageBoxButtons.YesNo,
                                                     MessageBoxIcon.Question);
                if (result == DialogResult.No)
                    return false;
                string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM Alarmlar WHERE id = @id";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", alarmId);
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Alarm silinirken bir hata oluştu: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        #endregion

        // BorcKaydi sınıfı
        public class BorcKaydi
        {
            public string CariKodu { get; set; }
            public string Unvan { get; set; }
            public decimal Bakiye { get; set; }
        }
               

        public void GuncelleAlarmPanelIcerigi()
        {
            flpAlarmIcerik.Controls.Clear();
            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                // Yaklaşan alarmları getir (bugün ve yarın)
                string query = @"SELECT id, cari_unvan, alarm_tarihi, odeme_turu
               FROM Alarmlar
               WHERE date(alarm_tarihi) BETWEEN date('now') AND date('now', '+1 day')
               AND durum = 'Bekliyor'
               ORDER BY alarm_tarihi
               LIMIT 5";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        int alarmSayisi = 0;
                        while (reader.Read())
                        {
                            alarmSayisi++;
                            int id = Convert.ToInt32(reader["id"]);
                            string cariUnvan = reader["cari_unvan"].ToString();
                            DateTime alarmTarihi = Convert.ToDateTime(reader["alarm_tarihi"]);
                            string odemeTuru = reader["odeme_turu"].ToString();
                            // Alarm için label oluştur
                            Label lblAlarm = new Label
                            {
                                Text = $"{alarmTarihi.ToString("dd.MM HH:mm")} • {cariUnvan} • {odemeTuru}",
                                ForeColor = DateTime.Today == alarmTarihi.Date ? Color.OrangeRed : Color.Yellow,
                                AutoSize = true,
                                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                                Margin = new Padding(10, 0, 10, 0),
                                Cursor = Cursors.Hand,
                                Tag = id
                            };
                            // Alarma tıklandığında detay göster
                            lblAlarm.Click += (s, e) => {
                                Label clickedLabel = (Label)s;
                                int alarmId = Convert.ToInt32(clickedLabel.Tag);
                                GosterAlarmDetay(alarmId);
                            };
                            flpAlarmIcerik.Controls.Add(lblAlarm);
                            // Ayırıcı ekle
                            if (alarmSayisi < 5 && reader.HasRows)
                            {
                                Label lblSeparator = new Label
                                {
                                    Text = "|",
                                    ForeColor = Color.DimGray,
                                    AutoSize = true,
                                    Margin = new Padding(5, 0, 5, 0)
                                };
                                flpAlarmIcerik.Controls.Add(lblSeparator);
                            }
                        }
                        // Eğer alarm yoksa bilgi metni göster
                        if (alarmSayisi == 0)
                        {
                            Label lblBilgi = new Label
                            {
                                Text = "Yakın zamanda alarm bulunmuyor",
                                ForeColor = Color.Silver,
                                AutoSize = true,
                                Font = new Font("Segoe UI", 9, FontStyle.Italic)
                            };
                            flpAlarmIcerik.Controls.Add(lblBilgi);
                        }
                        // Panel arka plan rengini alarm durumuna göre ayarla
                        pnlYaklasanAlarmlar.BackColor = Color.MidnightBlue; // Bu satırı ekleyin
                    }
                }
            }
        }

        public void hesapMakinesiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HesapMakinesi hesapMakinesi = new HesapMakinesi();
            hesapMakinesi.ShowDialog();
        }

        public void yedeklemeRehberiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // HTML rehberinin konumunu belirle
                string htmlPath = Path.Combine(Application.StartupPath, "yardim", "Veresiye2025_Yedekleme_Rehberi.html");
                // Dosya var mı kontrol et
                if (File.Exists(htmlPath))
                {
                    // Varsayılan tarayıcıda HTML dosyasını aç
                    Process.Start(htmlPath);
                }
                else
                {
                    MessageBox.Show("Yardım dosyası bulunamadı. Lütfen programı yeniden yükleyin.",
                        "Dosya Bulunamadı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Yardım dosyası açılırken bir hata oluştu: {ex.Message}",
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void DataGridView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                // Seçili satır var mı kontrol et
                if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Index >= 0)
                {
                    // Cari kodu var mı kontrol et
                    string selectedCariKodu = dataGridView1.CurrentRow.Cells["CariKodu"].Value?.ToString();
                    string selectedCariAdi = dataGridView1.CurrentRow.Cells["Unvani"].Value?.ToString();
                    if (!string.IsNullOrEmpty(selectedCariKodu) && !string.IsNullOrEmpty(selectedCariAdi))
                    {
                        // Cari hareket formunu aç
                        Carihareketler cariHareketForm = new Carihareketler(selectedCariKodu, cariKoduListesi, selectedCariAdi);
                        cariHareketForm.OnBakiyeGuncelle += UpdateColumn5FromCarihareketler;
                        this.Hide();
                        cariHareketForm.ShowDialog();
                        this.Show();
                        RefreshDataGridView();
                        UpdateColumn5();
                        cariHareketForm.UpdateButton13();
                    }
                    else
                    {
                        MessageBox.Show("Seçili satırda gerekli bilgiler eksik.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen bir satır seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void btnCarininHareketleriniAc_Enter(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Index >= 0)
            {
                string selectedCariKodu = dataGridView1.CurrentRow.Cells["CariKodu"].Value?.ToString();
                string selectedCariAdi = dataGridView1.CurrentRow.Cells["Unvani"].Value?.ToString();
                if (!string.IsNullOrEmpty(selectedCariKodu) && !string.IsNullOrEmpty(selectedCariAdi))
                {
                    // Cari hareket formunu aç
                    Carihareketler cariHareketForm = new Carihareketler(selectedCariKodu, cariKoduListesi, selectedCariAdi);
                    cariHareketForm.OnBakiyeGuncelle += UpdateColumn5FromCarihareketler;
                    this.Hide();
                    cariHareketForm.ShowDialog();
                    this.Show();
                    RefreshDataGridView();
                    UpdateColumn5();
                    // UpdateButton13 metodunu çağırarak button13'ü güncelle
                    cariHareketForm.UpdateButton13();
                }
                else
                {
                    MessageBox.Show("Seçili satırda gerekli bilgiler eksik.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Lütfen bir satır seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void hatırlatmaKurToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormOdemeYapmayanlar form = new FormOdemeYapmayanlar();
            form.ShowDialog();
        }

        private void form2cagir_Click(object sender, EventArgs e)
        {
            Form2 kayitFormu = new Form2();
            kayitFormu.ShowDialog(); // Modal olarak aç
        }

        // Acil durum versiyonu
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Tam özellikli UnifiedForm4'ü aç
                FullUnifiedForm4 fullForm = new FullUnifiedForm4("Test Şirketi");

                // Mevcut formu gizle
                this.Hide();

                // Tam formu göster
                fullForm.Show();

                // Form kapandığında bu formu da kapat
                fullForm.FormClosed += (s, args) => {
                    this.Close();
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Show(); // Hata durumunda geri göster
            }
        }
    }

    // Alarm sınıfı tanımı
    internal class Alarm
    {
        internal DateTime Tarih { get; set; }
        internal string OdemeTuru { get; set; }
        internal string Aciklama { get; set; }
        internal string CariUnvan { get; set; }
    }
}