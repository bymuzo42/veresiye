using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Windows.Forms;
using Veresiye2025.Properties;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace Veresiye2025
{
    public partial class Carihareketler : Form
    {
        // Halihazırda Designer.cs dosyasında tanımlanmış olabilecek aynı isimli metotlar
        // Burada tanımlamalarımızı public static yap ve farklı isimler verelim

        // Form taşıma için gerekli DLL import - Yeni isimlendirme


        // Yuvarlatılmış köşeler için API - Orijinal isimlendirme korunabilir
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        public static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );

        public List<string> cariKoduListesi;
        public string currentCariKodu;
        public string currentcariUnvan;
        public Form4 form4;

        public Carihareketler(string initialCariKodu, List<string> cariListesi, string cariAdi)
        {
            // Form flicker'ını önle
            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.UserPaint |
                          ControlStyles.DoubleBuffer |
                          ControlStyles.ResizeRedraw, true);
            InitializeComponent();

            // Form köşelerini yuvarla (20px)
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

            // Form büyüklüğü değiştiğinde köşeleri güncelle
            this.SizeChanged += Form_SizeChanged;

            // Tema rengini ayarla - DodgerBlue (30, 144, 255)
            SetThemeColors();

            // Sağ tık menüsü ayarları
            SetupDataGridViewContextMenu();

            // Alarm göstergeleri başlatma
            InitializeAlarmIndicators();

            // DataGridView olaylarını bağla
            dataGridView1.MouseClick += dataGridView1_MouseClick;
            dataGridView1.CellMouseEnter += DataGridView1_CellMouseEnter;
            dataGridView1.RowsAdded += DataGridView1_RowsAdded;
            dataGridView1.RowsRemoved += DataGridView1_RowsRemoved;

            // Başlangıçta butonu güncelle
            UpdateButton13();

            // Form boyutu ve konum ayarları
            this.Size = new Size(908, 518);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.MaximizeBox = false;
            this.KeyPreview = true; // Formun KeyDown olaylarını yakalamasını sağla

            // Klavye olaylarını bağla
            this.KeyDown += Carihareketler_KeyDown;

            // Yazdırma olayı
            printDocument.PrintPage += printDocument_PrintPage;

            // Cari bilgilerini ayarla
            cariKoduListesi = cariListesi;
            currentCariKodu = initialCariKodu;

            // Cari için emanet durumunu kontrol et
            if (!string.IsNullOrEmpty(currentCariKodu))
            {
                EmanetDurumunuKontrolEt(currentCariKodu);
            }

            // button3 ayarları (Firma adı gösterimi)
            button3.Font = new Font(button3.Font, FontStyle.Bold);
            button3.ForeColor = Color.White; // DodgerBlue temasıyla uyumlu olarak beyaz yazı
            button3.Text = cariAdi;
            button3.Enabled = true;

            // İlk cari hareketlerini yükle
            LoadCariHareketleri(currentCariKodu);

            // contextMenuStrip1 öğe olaylarını bağla
            this.borcekle.Click += Borcekle_Click;
            this.tahsilatekle.Click += Tahsilatekle_Click;

            // Menü öğelerinin işlevlerini tanımla
            menuStrip1.ItemClicked += MenuStrip1_ItemClicked;

            // Form4'ün menüsünü Carihareketler formuna ekle
            Form4 form4Instance = Application.OpenForms["Form4"] as Form4;
            if (form4Instance != null)
            {
                this.menuStrip1 = form4Instance.menuStrip1;
            }
        }

        // Form boyutu değiştiğinde köşeleri tekrar yuvarla - Metot ismini değiştirdik
        public void Form_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Minimized)
            {
                Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            }
        }

        // Tema rengini ayarla
        public void SetThemeColors()
        {
            // Ana tema rengi DodgerBlue - (30, 144, 255)
            Color themeColor = Color.DodgerBlue;

            // Panellerin rengini ayarla
            panelTop.BackColor = Color.MidnightBlue;
            panelHeader.BackColor = themeColor;
            panelButtons.BackColor = Color.MidnightBlue; // Bu satırı değiştirin
            panelFooter.BackColor = themeColor;

            // Context menüsünü ayarla
            contextMenuStrip1.BackColor = themeColor;
            contextMenuStrip1.ForeColor = Color.White;
            contextMenuStrip1.Renderer = new CustomToolStripRenderer(themeColor);

            foreach (ToolStripMenuItem item in contextMenuStrip1.Items)
            {
                item.BackColor = themeColor;
                item.ForeColor = Color.White;

                // İkonları göster
                if (item.Image != null)
                {
                    item.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                }
            }

            // Butonların rengini ve görünümünü ayarla
            StyleButtons(this.Controls);
        }

        // Butonları stilize eden metod
        public void StyleButtons(Control.ControlCollection controls)
        {
            foreach (Control ctrl in controls)
            {
                if (ctrl is Panel panel)
                {
                    StyleButtons(panel.Controls);
                }
                else if (ctrl is Button button)
                {
                    button.BackColor = Color.DodgerBlue;
                    button.FlatStyle = FlatStyle.Flat;
                    button.FlatAppearance.BorderSize = 0;
                    button.ForeColor = Color.White;
                    button.Cursor = Cursors.Hand;
                }
            }
        }

        // Özel ToolStrip Renderer sınıfı
        public class CustomToolStripRenderer : ToolStripProfessionalRenderer
        {
            public Color baseColor;

            public CustomToolStripRenderer(Color baseColor)
            {
                this.baseColor = baseColor;
            }

            protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
            {
                if (e.Item.Selected || e.Item.Pressed)
                {
                    // Hover durumunda siyah arka plan
                    Rectangle rect = new Rectangle(0, 0, e.Item.Width, e.Item.Height);
                    using (SolidBrush brush = new SolidBrush(Color.Black))
                    {
                        e.Graphics.FillRectangle(brush, rect);
                    }
                }
                else
                {
                    // Normal durum
                    Rectangle rect = new Rectangle(0, 0, e.Item.Width, e.Item.Height);
                    using (SolidBrush brush = new SolidBrush(baseColor))
                    {
                        e.Graphics.FillRectangle(brush, rect);
                    }
                }
            }

            protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
            {
                if (e.Item.Selected || e.Item.Pressed)
                {
                    // Hover durumunda kalın beyaz yazı
                    e.TextFont = new Font(e.TextFont, FontStyle.Bold);
                    e.TextColor = Color.White;
                }
                else
                {
                    // Normal durum
                    e.TextColor = Color.White;
                }
                base.OnRenderItemText(e);
            }
        }



        public void MenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "Ekle") // Eğer "Ekle" öğesi tıklandıysa
            {
                EkleMenuItem_Click(sender, e);
            }
            // Diğer menü öğeleri için de aynı mantıkla kontrol ekleyebilirsiniz.
        }

        public void EkleMenuItem_Click(object sender, EventArgs e)
        {
            // Eklemenizi sağlayacak işlev
            MessageBox.Show("Ekle menu item clicked in Carihareketler");
        }

        // Yeni satır eklendiğinde çalışacak olay
        public void DataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            UpdateButton13();
        }

        // Satır silindiğinde çalışacak olay
        public void DataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            UpdateButton13();
        }

        // İşlem sayısını güncellemek için metodu tanımlayalım
        internal void UpdateButton13()
        {
            int toplamIslemSayisi = dataGridView1.Rows.Count;
            button13.Text = "Toplam İşlem Sayısı = " + toplamIslemSayisi;

            // Button özelliklerini ayarlayalım
            button13.Enabled = false; // Tıklanamaz yap
            button13.Font = new Font(button13.Font, FontStyle.Bold); // Kalın yazı tipi
            button13.ForeColor = Color.Blue; // Mavi renk
        }



        public void RefreshDataGrid()
        {
            // DataGridView'i yeniden yüklemek için mevcut cari kodunu kullanın
            LoadCariHareketleri(currentCariKodu);
            RefreshAlarmStatuses();

        }
        

        // Modern hücre formatlaması
        public void DataGridView1_CellFormatting_Modern(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dataGridView1.Rows.Count) return;

            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

            // Alternatif satır renkleri (Zebra pattern)
            if (e.RowIndex % 2 == 0)
            {
                row.DefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            }
            else
            {
                row.DefaultCellStyle.BackColor = Color.White;
            }

            // Tür sütunu için özel renklendirme
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Tür" && e.Value != null)
            {
                string tur = e.Value.ToString().ToLower();
                if (tur.Contains("borç"))
                {
                    e.CellStyle.BackColor = Color.FromArgb(255, 235, 238);
                    e.CellStyle.ForeColor = Color.Crimson;
                }
                else if (tur.Contains("tahsilat"))
                {
                    e.CellStyle.BackColor = Color.FromArgb(235, 255, 238);
                    e.CellStyle.ForeColor = Color.ForestGreen;
                }
            }

            // Alarm kontrolü ve düzenlenmiş satır kontrolü (mevcut kodunuz)
            if (row.Cells[0].Value != null)
            {
                int hareketId = Convert.ToInt32(row.Cells[0].Value);
                if (KayitDuzenlenmisMi(hareketId))
                {
                    e.CellStyle.BackColor = Color.FromArgb(255, 248, 220); // Açık sarı
                    e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                }
            }
        }

        // Hover efekti
        public void DataGridView1_CellMouseEnter_Modern(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(67, 162, 243, 30);
                dataGridView1.Cursor = Cursors.Hand;
            }
        }

        public void DataGridView1_CellMouseLeave_Modern(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Orijinal renge dön
                if (e.RowIndex % 2 == 0)
                {
                    dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
                }
                else
                {
                    dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                }
                dataGridView1.Cursor = Cursors.Default;
            }
        }

        public void UpdateCariBakiye(string cariKodu, decimal bakiye)
        {

            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE Cari SET bakiye = @Bakiye WHERE CariKodu = @CariKodu";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Bakiye", bakiye);
                    command.Parameters.AddWithValue("@CariKodu", cariKodu);
                    command.ExecuteNonQuery();
                }
            }
        }


        // Load Olayı
        public void Carihareketler_Load(object sender, EventArgs e)
        {
            // DataGridView sütunlarını temizle ve yeniden oluştur
            //dataGridView1.Columns.Clear();
            

            // Eğer cari kodu listesi boşsa, dolduralım
            if (cariKoduListesi == null || cariKoduListesi.Count == 0)
                // Eğer cari kodu listesi boşsa, dolduralım
                if (cariKoduListesi == null || cariKoduListesi.Count == 0)
            {
                cariKoduListesi = new List<string>();
                LoadCariKoduListesi();
                RefreshAlarmStatuses();
                InitializeAlarmIndicators();
            }

            // DataGridView'da tüm satırlar için alarm durumunu güncelle
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    CheckAlarmForRow(row);
                }
            }
            dataGridView1.Refresh(); // Görünümü yenile

            EmanetDurumunuKontrolEt(currentCariKodu);
            //InitializeDataGridView();
            LoadCariHareketleri(currentCariKodu);
            dataGridView1.Visible = true; // Görünürlük sağlanır
            dataGridView1.Refresh(); // Yeniden çizilir
            UpdateFirmaAdi(); // Firma adı ve durum güncelleme
            borcekle.ShortcutKeys = Keys.F5;
            tahsilatekle.ShortcutKeys = Keys.F6;
            //alacakekle.ShortcutKeys = Keys.F7;[şimdilik iptal ettim]
            //odemeekle.ShortcutKeys = Keys.F8;[şimdilik iptal ettim]
            this.KeyPreview = true; // Form klavye olaylarını önce işlesin
            this.KeyDown += Form1_KeyDown; // KeyDown olayını bağla
            hareketlerF3ToolStripMenuItem.ShortcutKeys = Keys.F3; // F3 tuşunu kısa yol olarak tanımla
            UpdateButton13();
            CariNotDurumunuKontrolEt(currentCariKodu);
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
            
        }
        public void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                hrktsil.PerformClick(); // Sil butonunu tetikler
            }

            if (e.KeyCode == Keys.F12)
            {
                HesapMakinesi hesapMakinesi = new HesapMakinesi();
                hesapMakinesi.ShowDialog();
                e.Handled = true;
            }
        }

        public void GuncelleCariBilgileri(string cariKodu)
        {
            // Veritabanı bağlantı dizesi
            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";

            // Toplam borç ve alacak bilgilerini hesaplama
            decimal toplamBorc = 0;
            decimal toplamAlacak = 0;

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Toplam borç sorgusu
                string borcQuery = "SELECT SUM(Borc) FROM cari_hareketleri WHERE CariKodu = @CariKodu";
                using (SQLiteCommand command = new SQLiteCommand(borcQuery, connection))
                {
                    command.Parameters.AddWithValue("@CariKodu", cariKodu);
                    object borcResult = command.ExecuteScalar();
                    toplamBorc = borcResult != DBNull.Value ? Convert.ToDecimal(borcResult) : 0;
                }

                // Toplam alacak sorgusu
                string alacakQuery = "SELECT SUM(Alacak) FROM cari_hareketleri WHERE CariKodu = @CariKodu";
                using (SQLiteCommand command = new SQLiteCommand(alacakQuery, connection))
                {
                    command.Parameters.AddWithValue("@CariKodu", cariKodu);
                    object alacakResult = command.ExecuteScalar();
                    toplamAlacak = alacakResult != DBNull.Value ? Convert.ToDecimal(alacakResult) : 0;
                }
            }

            // Kalan borç bilgisi
            decimal kalanBorc = toplamBorc - toplamAlacak;

            // TextBox'lara bilgileri yaz
            textBox1.Text = toplamBorc.ToString("C2");  // Toplam Borç
            textBox2.Text = toplamAlacak.ToString("C2");  // Toplam Tahsilat
            textBox3.Text = kalanBorc.ToString("C2");  // Kalan Borç

            // TextBox'ların düzenlenmesini engelle
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            textBox3.ReadOnly = true;
        }

        // Olay tanımı
        public event Action<string, decimal> OnBakiyeGuncelle;

        
        public DateTime GetSonIslemTarihi(string cariKodu)
        {
            DateTime sonIslemTarihi = DateTime.Now; // Varsayılan olarak şimdiki zaman(düzenle)

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["cari_kodu"].Value?.ToString() == cariKodu && row.Cells["Tarih"].Value != null)
                {
                    sonIslemTarihi = Convert.ToDateTime(row.Cells["Tarih"].Value);
                }
            }

            return sonIslemTarihi;
        }


        public void LoadCariHareketleri(string cariKodu)
        {
            try
            {
                // DataGridView'ı dondur
                dataGridView1.SuspendLayout();
                dataGridView1.Rows.Clear();

                decimal toplamBorc = 0;
                decimal toplamTahsilat = 0;
                satirAlarmlari.Clear();

                string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";

                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // PRAGMA optimizasyonları
                    using (SQLiteCommand pragmaCmd = new SQLiteCommand(connection))
                    {
                        pragmaCmd.CommandText = "PRAGMA journal_mode = WAL;";
                        pragmaCmd.ExecuteNonQuery();
                        pragmaCmd.CommandText = "PRAGMA synchronous = NORMAL;";
                        pragmaCmd.ExecuteNonQuery();
                        pragmaCmd.CommandText = "PRAGMA cache_size = 10000;";
                        pragmaCmd.ExecuteNonQuery();
                        pragmaCmd.CommandText = "PRAGMA temp_store = MEMORY;";
                        pragmaCmd.ExecuteNonQuery();
                    }

                    // INDEX kullan (eğer yoksa ekle)
                    string indexQuery = "CREATE INDEX IF NOT EXISTS idx_cari_tarih ON cari_hareketleri(cari_kodu, tarih);";
                    using (SQLiteCommand indexCmd = new SQLiteCommand(indexQuery, connection))
                    {
                        indexCmd.ExecuteNonQuery();
                    }

                    // Tek sorguda tüm verileri al
                    string query = @"
                SELECT * FROM cari_hareketleri 
                WHERE cari_kodu = @CariKodu 
                ORDER BY tarih, id";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CariKodu", cariKodu);

                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            // Satırları toplu ekle
                            List<object[]> rows = new List<object[]>();

                            while (reader.Read())
                            {
                                decimal borc = reader["borc"] != DBNull.Value ? Convert.ToDecimal(reader["borc"]) : 0;
                                decimal tahsilat = reader["tahsilat"] != DBNull.Value ? Convert.ToDecimal(reader["tahsilat"]) : 0;

                                toplamBorc += borc;
                                toplamTahsilat += tahsilat;
                                decimal kalan = toplamBorc - toplamTahsilat;

                                DateTime islemTarihi = Convert.ToDateTime(reader["tarih"]);

                                rows.Add(new object[] {
                            reader["id"].ToString(),
                            cariKodu,
                            islemTarihi.ToString("dd.MM.yyyy"),
                            reader["tur"] ?? "",
                            reader["aciklama"] ?? "",
                            borc > 0 ? borc.ToString("C2") : "",
                            tahsilat > 0 ? tahsilat.ToString("C2") : "",
                            kalan.ToString("C2")
                        });
                            }

                            // Toplu satır ekleme
                            foreach (var row in rows)
                            {
                                dataGridView1.Rows.Add(row);
                            }
                        }
                    }
                }

                // Özet bilgileri güncelle
                UpdateSummaryTextBoxes(toplamBorc, toplamTahsilat);
                UpdateTransactionCount();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dataGridView1.ResumeLayout();
                dataGridView1.Refresh();
            }
        }

        // ============ YARDIMCI METOTLAR ============

        /// <summary>
        /// Özet bilgilerini günceller (TextBox'lar)
        /// </summary>
        public void UpdateSummaryTextBoxes(decimal toplamBorc, decimal toplamTahsilat)
        {
            try
            {
                // TextBox'ları güncelle
                textBox1.Text = toplamBorc.ToString("C2");
                textBox2.Text = toplamTahsilat.ToString("C2");
                textBox3.Text = (toplamBorc - toplamTahsilat).ToString("C2");

                // Modern font stilleri
                textBox1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                textBox2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                textBox3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

                // Bakiye rengi (pozitif/negatif durumuna göre)
                decimal netDurum = toplamBorc - toplamTahsilat;
                if (netDurum < 0)
                {
                    textBox3.ForeColor = Color.Crimson;           // Negatif için kırmızı
                    textBox3.BorderColor = Color.Crimson;         // Guna2 TextBox için
                }
                else if (netDurum > 0)
                {
                    textBox3.ForeColor = Color.ForestGreen;       // Pozitif için yeşil
                    textBox3.BorderColor = Color.ForestGreen;     // Guna2 TextBox için
                }
                else
                {
                    textBox3.ForeColor = Color.DarkBlue;          // Sıfır için mavi
                    textBox3.BorderColor = Color.DarkBlue;        // Guna2 TextBox için
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"UpdateSummaryTextBoxes Hatası: {ex.Message}");
            }
        }

        /// <summary>
        /// İşlem sayısı butonunu günceller
        /// </summary>
        public void UpdateTransactionCount()
        {
            try
            {
                int toplamIslemSayisi = dataGridView1.Rows.Count;
                button13.Text = $"📊 Toplam İşlem: {toplamIslemSayisi}";

                // Modern buton stilleri
                button13.Enabled = false;
                button13.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                button13.ForeColor = Color.White;
                button13.FillColor = Color.FromArgb(67, 162, 243);

                // UpdateButton13 metodunu da çağır (mevcut sisteminizle uyumluluk için)
                UpdateButton13();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"UpdateTransactionCount Hatası: {ex.Message}");
            }
        }

        /// <summary>
        /// Son satıra odaklanır
        /// </summary>
        public void FocusLastRow()
        {
            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    int lastRowIndex = dataGridView1.Rows.Count - 1;
                    dataGridView1.ClearSelection();
                    dataGridView1.Rows[lastRowIndex].Selected = true;
                    dataGridView1.FirstDisplayedScrollingRowIndex = lastRowIndex;

                    // Seçili satır için özel stil
                    dataGridView1.Rows[lastRowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(67, 162, 243);
                    dataGridView1.Rows[lastRowIndex].DefaultCellStyle.SelectionForeColor = Color.White;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"FocusLastRow Hatası: {ex.Message}");
            }
        }

        /// <summary>
        /// DataGridView görünümünü yeniler
        /// </summary>
        public void RefreshDataGridView()
        {
            try
            {
                dataGridView1.Invalidate();
                dataGridView1.Refresh();
                dataGridView1.Update();

                // Alarmları yeniden kontrol et
                RefreshAlarmStatuses();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"RefreshDataGridView Hatası: {ex.Message}");
            }
        }

        // DataGridView için context menu ayarları
        public void SetupDataGridViewContextMenu()
        {
            // Yeni context menu oluştur
            ContextMenuStrip contextMenu = new ContextMenuStrip();

            // Tahsilat detayı menü öğesi
            ToolStripMenuItem tahsilatDetayItem = new ToolStripMenuItem("Tahsilat Detaylarını Göster");
            tahsilatDetayItem.Name = "tahsilatDetayItem";
            tahsilatDetayItem.Click += (sender, e) => {
                ShowTahsilatDetails_Click(sender, e);
            };
            contextMenu.Items.Add(tahsilatDetayItem);

            // Borç detayı menü öğesi
            ToolStripMenuItem borcDetayItem = new ToolStripMenuItem("Borç Detaylarını Göster");
            borcDetayItem.Name = "borcDetayItem";
            borcDetayItem.Click += (sender, e) => {
                ShowBorcDetails_Click(sender, e);
            };
            contextMenu.Items.Add(borcDetayItem);

            // Ayırıcı
            ToolStripSeparator separator = new ToolStripSeparator();
            separator.Name = "separator";
            contextMenu.Items.Add(separator);

            // Düzenleme geçmişi menü öğesi
            ToolStripMenuItem gecmisItem = new ToolStripMenuItem("Düzenleme Geçmişini Göster");
            gecmisItem.Name = "gecmisItem";
            gecmisItem.Click += (sender, e) => {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int rowIndex = dataGridView1.SelectedRows[0].Index;
                    if (dataGridView1.Rows[rowIndex].Cells[0].Value != null)
                    {
                        int hareketId = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells[0].Value);
                        GoruntuleIslemGecmisi(hareketId);
                    }
                }
            };


            contextMenu.Items.Add(gecmisItem);

            // Yeni bir ayırıcı
            ToolStripSeparator separatorAlarm = new ToolStripSeparator();
            separatorAlarm.Name = "separatorAlarm";
            contextMenu.Items.Add(separatorAlarm);

            // Alarm Ekle menü öğesi
            ToolStripMenuItem alarmEkleItem = new ToolStripMenuItem("Alarm Ekle");
            alarmEkleItem.Name = "alarmEkleItem";
            alarmEkleItem.Image = Veresiye2025.Properties.Resources.clock; // İsterseniz bir saat ikonu ekleyebilirsiniz
            alarmEkleItem.Click += (sender, e) => {
                try
                {
                    if (string.IsNullOrEmpty(currentCariKodu) || string.IsNullOrEmpty(button3.Text))
                    {
                        MessageBox.Show("Cari bilgileri alınamadı. Lütfen geçerli bir cari seçin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Mevcut carinin kodunu ve unvanını doğrudan kullan
                    string cariKodu = currentCariKodu;
                    string cariUnvan = button3.Text;

                    // Alarm formunu aç
                    Alarmkur alarmForm = new Alarmkur(cariUnvan, cariKodu);

                    // Alarm formunu göster
                    DialogResult result = alarmForm.ShowDialog();

                    // Alarm eklendiyse (OK döndüyse) alarmları yenile
                    if (result == DialogResult.OK)
                    {
                        RefreshAlarmStatuses(); // Alarm durumlarını güncelle
                        dataGridView1.Refresh(); // DataGridView'ı yenile
                    }

                    // Form4'ü güncelle (eğer mevcutsa)
                    Form4 form4 = Application.OpenForms["Form4"] as Form4;
                    if (form4 != null)
                    {
                        form4.KontrolEtAlarmlari();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Alarm eklenirken hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            contextMenu.Items.Add(alarmEkleItem);

            // Context menu'yü ata
            dataGridView1.ContextMenuStrip = contextMenu;

            // Menü açılmadan önce öğelerin etkin/devre dışı durumlarını belirle
            contextMenu.Opening += (sender, e) => {
                bool rowSelected = dataGridView1.SelectedRows.Count > 0;

                tahsilatDetayItem.Enabled = rowSelected;
                borcDetayItem.Enabled = rowSelected;
                gecmisItem.Enabled = rowSelected;
                alarmEkleItem.Enabled = rowSelected;

                // Eğer satır seçiliyse, hangi tür işlem olduğuna göre detay menülerini etkinleştir/devre dışı bırak
                if (rowSelected)
                {
                    int rowIndex = dataGridView1.SelectedRows[0].Index;
                    string islemTipi = "";

                    // "Tür" sütunu varsa
                    if (dataGridView1.Columns.Contains("tur") && dataGridView1.Rows[rowIndex].Cells["tur"].Value != null)
                    {
                        islemTipi = dataGridView1.Rows[rowIndex].Cells["tur"].Value.ToString();
                    }

                    tahsilatDetayItem.Enabled = islemTipi.Contains("Tahsilat");
                    borcDetayItem.Enabled = islemTipi.Contains("Borç");
                }
            };
        }

        

        // Cari unvanını veritabanından çekmek için yardımcı metod
        public string GetCariUnvan(string cariKodu)
        {
            string cariUnvan = "";

            try
            {
                string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Unvani FROM Cari WHERE CariKodu = @cariKodu";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@cariKodu", cariKodu);
                        object result = command.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            cariUnvan = result.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Cari unvanı alınırken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return cariUnvan;
        }

        // Alarm formunda tarih ayarlamak için yardımcı metod
        public void SetAlarmDate(Alarmkur alarmForm, DateTime tarih)
        {
            alarmForm.SetDefaultDate(tarih);
        }

        public void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            // Sağ tık olayını kontrol et
            if (e.Button == MouseButtons.Right)
            {
                // Fare pozisyonundan hücre ve satır bilgisini al
                DataGridView.HitTestInfo hit = dataGridView1.HitTest(e.X, e.Y);

                if (hit.Type == DataGridViewHitTestType.Cell && hit.RowIndex >= 0)
                {
                    // İlgili satırı seç
                    dataGridView1.ClearSelection();
                    dataGridView1.Rows[hit.RowIndex].Selected = true;

                    try
                    {
                        // Doğru sütun adını bul - kullanıcı arabiriminde gösterilen ad ile sütun adı farklı olabilir
                        string turSutunAdi = "Tür"; // Eğer bu doğru değilse, aşağıdaki kodu kullanın

                        // Alternatif: Tür sütununu bulma (sütun başlığı "Tür" olan)
                        foreach (DataGridViewColumn col in dataGridView1.Columns)
                        {
                            if (col.HeaderText == "Tür" || col.Name == "Tür" || col.Name == "tur" || col.HeaderText == "tur")
                            {
                                turSutunAdi = col.Name;
                                break;
                            }
                        }

                        // İşlem tipini kontrol et
                        string islemTipi = dataGridView1.Rows[hit.RowIndex].Cells[turSutunAdi].Value?.ToString() ?? "";

                        // ID'yi al
                        int hareketId = 0;
                        if (dataGridView1.Rows[hit.RowIndex].Cells[0].Value != null)
                        {
                            hareketId = Convert.ToInt32(dataGridView1.Rows[hit.RowIndex].Cells[0].Value);
                        }

                        // Düzenleme geçmişi kontrolü
                        bool duzenlenmisMi = KayitDuzenlenmisMi(hareketId);

                        // Context menü öğelerini görünürlüklerini ayarla
                        if (dataGridView1.ContextMenuStrip != null)
                        {
                            // Tahsilat detayı görünürlüğü
                            ToolStripItem tahsilatItem = dataGridView1.ContextMenuStrip.Items["tahsilatDetayItem"];
                            if (tahsilatItem != null)
                            {
                                tahsilatItem.Visible = islemTipi.Contains("Tahsilat");
                            }

                            // Borç detayı görünürlüğü
                            ToolStripItem borcItem = dataGridView1.ContextMenuStrip.Items["borcDetayItem"];
                            if (borcItem != null)
                            {
                                borcItem.Visible = islemTipi.Contains("Borç");
                            }

                            // Ayırıcı ve geçmiş görüntüleme
                            ToolStripItem seperatorItem = dataGridView1.ContextMenuStrip.Items["separator"];
                            ToolStripItem gecmisItem = dataGridView1.ContextMenuStrip.Items["gecmisItem"];

                            if (seperatorItem != null)
                                seperatorItem.Visible = duzenlenmisMi;

                            if (gecmisItem != null)
                                gecmisItem.Visible = duzenlenmisMi;
                        }

                        // Debug çıktısı
                        Console.WriteLine($"Satır: {hit.RowIndex}, Tür: {islemTipi}, Düzenlenmiş: {duzenlenmisMi}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Hata: {ex.Message}");
                        // Hatayı kullanıcıya gösterme, sessizce devam et
                    }
                }
            }
        }
                
        // Tahsilat detaylarını gösterme olay işleyicisi
        public void TahsilatDetayMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedRowIndex = dataGridView1.SelectedRows[0].Index;
                if (dataGridView1.Rows[selectedRowIndex].Cells[0].Value != null &&
                    !string.IsNullOrEmpty(dataGridView1.Rows[selectedRowIndex].Cells[0].Value.ToString()))
                {
                    int hareketId = Convert.ToInt32(dataGridView1.Rows[selectedRowIndex].Cells[0].Value);

                    // Tahsilat detaylarını gösterme işlemi
                    // Örnek olarak:
                    ShowTahsilatDetay(hareketId);
                }
            }
        }

        // Borç detaylarını gösterme metodu
        public void ShowBorcDetay(int hareketId)
        {
            // Burada borç detaylarını gösteren kodu yazın
            // Örneğin, bir form açabilir veya mesaj kutusu gösterebilirsiniz
            MessageBox.Show($"Borç detayları gösteriliyor, ID: {hareketId}", "Borç Detayları",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Tahsilat detaylarını gösterme metodu
        public void ShowTahsilatDetay(int hareketId)
        {
            // Burada tahsilat detaylarını gösteren kodu yazın
            // Örneğin, bir form açabilir veya mesaj kutusu gösterebilirsiniz
            MessageBox.Show($"Tahsilat detayları gösteriliyor, ID: {hareketId}", "Tahsilat Detayları",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Yardımcı metot - Menü öğesinin var olup olmadığını kontrol eder
        public bool MenuItemExists(ContextMenuStrip menu, string itemName)
        {
            foreach (ToolStripItem item in menu.Items)
            {
                if (item.Name == itemName)
                    return true;
            }
            return false;
        }


        // ContextMenuStrip öğelerinin tıklama olayları
        public void Borcekle_Click(object sender, EventArgs e)
        {
            if (!IsHesapAktif(currentCariKodu)) // Hesap durumunu kontrol et
            {
                MessageBox.Show("Hesap Pasif durumda olduğu için borç eklenemez. Lütfen Tahsilat Yapınız.",
                                "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // İşlemi iptal et
            }

            {
                string cariUnvan = button3.Text; // Cari unvanını al
                Borcekle borcEkleFormu = new Borcekle(cariUnvan)
                {
                    Owner = this // Bu formu sahip olarak ayarla
                };

                borcEkleFormu.ShowDialog(); // Formu modal olarak aç
            }


            // Form kapandıktan sonra DataGridView'i yenile
            LoadCariHareketleri(currentCariKodu);
            dataGridView1.Refresh();
        }

        // Hesap durumunu kontrol eden metot
        public bool IsHesapAktif(string cariKodu)
        {
            try
            {
                string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Hesap FROM Cari WHERE CariKodu = @CariKodu";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CariKodu", cariKodu);
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return reader["Hesap"].ToString() != "Pasif Hesap";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hesap durumunu kontrol ederken bir hata oluştu: {ex.Message}",
                                "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false; // Varsayılan olarak pasif kabul edilir
        }




        public void Tahsilatekle_Click(object sender, EventArgs e)
        {
            // Cari unvanı almak için gerekli değişken
            string cariUnvan = button3.Text; // button3'te cari unvan gösterildiği varsayımıyla

            // Tahsilat formunu cari unvanıyla açmak için bir örneğini oluştur
            Tahsilat tahsilatFormu = new Tahsilat(cariUnvan)
            {
                Owner = this // Bu formu sahip olarak ayarla
            };

            // Tahsilat formunu diyalog olarak göster (modal)
            tahsilatFormu.ShowDialog();
        }



        internal void UpdateFirmaAdi()
        {
            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Unvani, Hesap FROM Cari WHERE CariKodu = @CariKodu";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CariKodu", currentCariKodu);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Firma adını güncelle
                            button3.Text = reader["Unvani"].ToString();

                            // Hesap durumuna göre button3'ün rengini ayarla
                            if (reader["Hesap"].ToString() == "Pasif Hesap")
                            {
                                button3.ForeColor = Color.Silver;
                                borcekle.Enabled = false;

                                // Modern MessageBox
                                ShowModernWarningMessage(
                                    "Bu Hesaba Borç Eklenemez!",
                                    "Hesap pasif durumda olduğu için borç işlemi yapılamaz.",
                                    "Pasif Hesap Uyarısı"
                                );
                            }
                            else
                            {
                                button3.ForeColor = Color.White;
                                borcekle.Enabled = true;
                            }

                            button3.Refresh();
                            this.Refresh();
                        }
                    }
                }
            }
        }

        public void ShowModernWarningMessage(string title, string message, string caption)
        {
            Form modernMessageBox = new Form
            {
                FormBorderStyle = FormBorderStyle.None,
                BackColor = Color.White,
                Size = new Size(400, 200),
                StartPosition = FormStartPosition.CenterParent,
                TopMost = true
            };

            // Yuvarlatılmış köşeler
            modernMessageBox.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, 400, 200, 15, 15));

            // Header panel (kırmızı)
            Panel headerPanel = new Panel
            {
                BackColor = Color.FromArgb(220, 53, 69),
                Dock = DockStyle.Top,
                Height = 60
            };

            // İkon (Uyarı)
            Label iconLabel = new Label
            {
                Text = "⚠",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(20, 15),
                Size = new Size(40, 40),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Başlık
            Label titleLabel = new Label
            {
                Text = caption,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(70, 20),
                Size = new Size(300, 30),
                TextAlign = ContentAlignment.MiddleLeft
            };

            // Ana mesaj
            Label messageLabel = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(60, 60, 60),
                Location = new Point(30, 80),
                Size = new Size(340, 30),
                TextAlign = ContentAlignment.MiddleLeft
            };

            // Alt mesaj
            Label subMessageLabel = new Label
            {
                Text = message,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(120, 120, 120),
                Location = new Point(30, 110),
                Size = new Size(340, 25),
                TextAlign = ContentAlignment.MiddleLeft
            };

            // Tamam butonu
            Guna.UI2.WinForms.Guna2Button okButton = new Guna.UI2.WinForms.Guna2Button
            {
                Text = "Tamam",
                Size = new Size(100, 35),
                Location = new Point(280, 150),
                FillColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                BorderRadius = 8,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };

            okButton.Click += (s, e) => modernMessageBox.Close();

            // Kontrolleri ekle
            headerPanel.Controls.Add(iconLabel);
            headerPanel.Controls.Add(titleLabel);
            modernMessageBox.Controls.Add(headerPanel);
            modernMessageBox.Controls.Add(messageLabel);
            modernMessageBox.Controls.Add(subMessageLabel);
            modernMessageBox.Controls.Add(okButton);

            // Kapatma olayları
            modernMessageBox.KeyDown += (s, e) => { if (e.KeyCode == Keys.Escape) modernMessageBox.Close(); };
            modernMessageBox.KeyPreview = true;

            // Göster
            modernMessageBox.ShowDialog(this);
        }

        public void ContextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Bu olay gerektiğinde ContextMenuStrip açılmadan önce yapılacak işlemler için tanımlanmıştır.
        }

        // Carihareketler.cs de yer alan önceki sonraki buton yapılandırmaları//

        public void LoadCariHareketleriById(int id)
        {
            RefreshAlarmStatuses();
            dataGridView1.Rows.Clear(); // DataGridView'i temizle

            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM cari_hareketleri WHERE id = @Id"; // Veritabanından id'ye göre veri çekme
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string cariKodu = reader["cari_kodu"]?.ToString();
                            string tarih = reader["tarih"]?.ToString();
                            string tur = reader["tur"]?.ToString();
                            string aciklama = reader["aciklama"]?.ToString();
                            decimal borc = reader["borc"] != DBNull.Value ? Convert.ToDecimal(reader["borc"]) : 0;
                            decimal tahsilat = reader["tahsilat"] != DBNull.Value ? Convert.ToDecimal(reader["tahsilat"]) : 0;

                            // DataGridView'e satır ekleyin
                            dataGridView1.Rows.Add(cariKodu, tarih, tur, aciklama, borc.ToString("C2"), tahsilat.ToString("C2"));
                        }
                    }
                }
            }
        }

        public int GetPreviousId(int currentId)
        {
            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT id FROM cari_hareketleri WHERE id < @CurrentId ORDER BY id DESC LIMIT 1"; // Önceki id'yi al
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CurrentId", currentId);
                    object result = command.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : -1;
                }
            }
        }

        public int GetNextId(int currentId)
        {
            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT id FROM cari_hareketleri WHERE id > @CurrentId ORDER BY id ASC LIMIT 1"; // Sonraki id'yi al
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CurrentId", currentId);
                    object result = command.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : -1;
                }
            }
        }


        

        public string GetPreviousCariKodu()
        {
            InitializeAlarmIndicators();
            int currentIndex = cariKoduListesi.IndexOf(currentCariKodu);
            if (currentIndex > 0) // İlk kayıt değilse
            {
                return cariKoduListesi[currentIndex - 1];
            }
            return null; // Önceki kayıt yok
            
        }


        public string GetNextCariKodu()
        {
            InitializeAlarmIndicators();
            int currentIndex = cariKoduListesi.IndexOf(currentCariKodu);
            if (currentIndex < cariKoduListesi.Count - 1) // Son kayıt değilse
            {
                return cariKoduListesi[currentIndex + 1];
            }
            return null; // Sonraki kayıt yok
           
        }



        public void Carihareketler_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
            {
                // F4 tuşuna basıldığında yedekAlToolStripMenuItem_Click metodunu tetikliyoruz
                yedekAlToolStripMenuItem_Click(sender, e);
            }

            if (e.KeyCode == Keys.F5)
            {
                // F5 tuşuna basıldığında Borç Ekle öğesini tetikle
                borcekle.PerformClick();
                e.Handled = true;

            }
            else if (e.KeyCode == Keys.F6)
            {
                // F6 tuşuna basıldığında Tahsilat Ekle öğesini tetikle
                tahsilatekle.PerformClick();
                e.Handled = true;
            }

            if (e.KeyCode == Keys.F12)
            {
                HesapMakinesi hesapMakinesi = new HesapMakinesi();
                hesapMakinesi.ShowDialog();
                e.Handled = true;
            }

        }
                

        // "Delete" tuşu ile aynı işlevi yapması için KeyDown olayını bağla
        public void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete) // Eğer "Delete" tuşuna basıldıysa
            {
                DeleteSelectedRow(); // Satır silme işlemini çağır
                e.Handled = true; // Olayı işlenmiş olarak işaretle
            }
        }

        // Satır silme işlemini gerçekleştiren metot
        public void DeleteSelectedRow()
        {
            // DataGridView'de seçili satır var mı kontrol et
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Seçili satırı al
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                // Seçili satırdan id sütununu al
                if (selectedRow.Cells["id"].Value == null)
                {
                    MessageBox.Show("Seçili satırda bir ID bulunamadı. Lütfen doğru bir satır seçtiğinizden emin olun.",
                                    "Hata",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    return;
                }

                string id = selectedRow.Cells["id"].Value.ToString();

                // Kullanıcıdan silme onayı al
                var result = MessageBox.Show("Seçili hareketi silmek istediğinizden emin misiniz?",
                                             "Silme Onayı",
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        // Veritabanından silme işlemi
                        DeleteRowFromDatabaseById(id);

                        // DataGridView'den sil
                        dataGridView1.Rows.Remove(selectedRow);

                        // Güncellemeler
                        UpdateTextBoxesAndButton();

                        MessageBox.Show("Seçilen hareket başarıyla silindi.",
                                        "Bilgi",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Bir hata oluştu: {ex.Message}",
                                        "Hata",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen silmek istediğiniz bir satırı seçin.",
                                "Uyarı",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
        }


        public void UpdateTextBoxesAndButton()
        {
            decimal toplamBorc = 0;
            decimal toplamTahsilat = 0;

            // DataGridView'deki mevcut satırları dolaşarak toplamları hesapla
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue; // Boş satırları atla

                // Borç sütunu kontrolü
                if (row.Cells["Borç"].Value != null && !string.IsNullOrEmpty(row.Cells["Borç"].Value.ToString()))
                {
                    string borcText = row.Cells["Borç"].Value.ToString();
                    // Para birimi sembollerini temizle
                    borcText = borcText.Replace("₺", "").Replace("TL", "").Replace(".", "").Replace(",", ".").Trim();

                    if (decimal.TryParse(borcText, System.Globalization.NumberStyles.Any,
                        System.Globalization.CultureInfo.InvariantCulture, out decimal borcValue))
                    {
                        toplamBorc += borcValue;
                    }
                }

                // CariTema.cs'de "Alacak" olarak tanımlandığı için bu ismi kullan
                string tahsilatColumnName = "Alacak"; // veya "Tahsilat" - hangisi varsa

                // Önce sütunun var olup olmadığını kontrol et
                if (dataGridView1.Columns.Contains(tahsilatColumnName))
                {
                    if (row.Cells[tahsilatColumnName].Value != null && !string.IsNullOrEmpty(row.Cells[tahsilatColumnName].Value.ToString()))
                    {
                        string tahsilatText = row.Cells[tahsilatColumnName].Value.ToString();
                        // Para birimi sembollerini temizle
                        tahsilatText = tahsilatText.Replace("₺", "").Replace("TL", "").Replace(".", "").Replace(",", ".").Trim();

                        if (decimal.TryParse(tahsilatText, System.Globalization.NumberStyles.Any,
                            System.Globalization.CultureInfo.InvariantCulture, out decimal tahsilatValue))
                        {
                            toplamTahsilat += tahsilatValue;
                        }
                    }
                }
                else
                {
                    // Eğer "Alacak" yoksa "Tahsilat" dene
                    tahsilatColumnName = "Tahsilat";
                    if (dataGridView1.Columns.Contains(tahsilatColumnName))
                    {
                        if (row.Cells[tahsilatColumnName].Value != null && !string.IsNullOrEmpty(row.Cells[tahsilatColumnName].Value.ToString()))
                        {
                            string tahsilatText = row.Cells[tahsilatColumnName].Value.ToString();
                            tahsilatText = tahsilatText.Replace("₺", "").Replace("TL", "").Replace(".", "").Replace(",", ".").Trim();

                            if (decimal.TryParse(tahsilatText, System.Globalization.NumberStyles.Any,
                                System.Globalization.CultureInfo.InvariantCulture, out decimal tahsilatValue))
                            {
                                toplamTahsilat += tahsilatValue;
                            }
                        }
                    }
                }
            }

            // TextBox'ları güncelle
            textBox1.Text = toplamBorc.ToString("C2", new System.Globalization.CultureInfo("tr-TR"));
            textBox2.Text = toplamTahsilat.ToString("C2", new System.Globalization.CultureInfo("tr-TR"));

            decimal netDurum = toplamBorc - toplamTahsilat;
            textBox3.Text = netDurum.ToString("C2", new System.Globalization.CultureInfo("tr-TR"));

            // Cari tablosunu güncelle
            UpdateCariBakiye(currentCariKodu, netDurum);

            // TextBox yazı stilini kalın yap
            textBox1.Font = new Font(textBox1.Font, FontStyle.Bold);
            textBox2.Font = new Font(textBox2.Font, FontStyle.Bold);
            textBox3.Font = new Font(textBox3.Font, FontStyle.Bold);

            // TextBox3 rengi pozitif/negatif durumuna göre değiştir
            if (netDurum < 0)
            {
                textBox3.ForeColor = Color.Red;
            }
            else
            {
                textBox3.ForeColor = Color.Black;
            }

            // Button13 işlem sayısını güncelle
            button13.Text = "Toplam İşlem Sayısı = " + dataGridView1.Rows.Count;
            button13.ForeColor = Color.Blue;
            button13.Font = new Font(button13.Font, FontStyle.Bold);
        }


        // Veritabanından ID ile satır silme metodu
        public void DeleteRowFromDatabaseById(string id)
        {
            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM cari_hareketleri WHERE id = @Id";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        // notekle_Click metodu mevcut - değiştirmeye gerek yok
        public void notekle_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentCariKodu))
            {
                MessageBox.Show("Lütfen bir cari seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            FormNotEkle formNotEkle = new FormNotEkle(currentCariKodu);
            formNotEkle.ShowDialog();

            // Not ekledikten sonra durumu kontrol et
            CariNotDurumunuKontrolEt(currentCariKodu);
        }

        // Sınıf seviyesinde tanımlamalar
        public Label notBildirimLabel = null;
        public Timer notBildirimiTimer = null;

        // Bu metodu CariNotDurumunuKontrolEt olarak güncelleyin
        public void CariNotDurumunuKontrolEt(string cariKodu)
        {
            // Tüm mevcut bildirimleri temizle
            TemizleBildirimler();

            // Cariye ait not sayısını kontrol et
            int notSayisi = GetNotSayisi(cariKodu);
            if (notSayisi > 0)
            {
                // notekle butonunun bulunduğu container'ı bul
                Control container = notekle.Parent;

                // Oval kırmızı bildirim etiketi oluştur
                notBildirimLabel = new Label
                {
                    Text = notSayisi.ToString(),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    TextAlign = ContentAlignment.MiddleCenter,
                    BackColor = Color.FromArgb(239, 68, 93),
                    Size = new Size(22, 22),
                    Location = new Point(
                        notekle.Right - 22,
                        notekle.Top - 2
                    )
                };

                // Oval yapmak için Paint olayını ekle
                notBildirimLabel.Paint += (s, e) => {
                    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    using (var path = new System.Drawing.Drawing2D.GraphicsPath())
                    {
                        path.AddEllipse(0, 0, notBildirimLabel.Width - 1, notBildirimLabel.Height - 1);
                        notBildirimLabel.Region = new Region(path);
                    }
                };

                // Butonu içeren container'a ekle
                container.Controls.Add(notBildirimLabel);
                notBildirimLabel.BringToFront();
            }
        }

        // Not sayısını getiren metod
        public int GetNotSayisi(string cariKodu)
        {
            int notSayisi = 0;

            try
            {
                string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string sorgu = "SELECT COUNT(*) FROM Notlar WHERE CariKodu = @CariKodu";
                    using (SQLiteCommand command = new SQLiteCommand(sorgu, connection))
                    {
                        command.Parameters.AddWithValue("@CariKodu", cariKodu);
                        notSayisi = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Not sayısı kontrolü sırasında hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return notSayisi;
        }

        // Tüm bildirimleri temizleyen güvenli metod
        public void TemizleBildirimler()
        {
            // Timer'ı durdur ve temizle
            if (notBildirimiTimer != null)
            {
                notBildirimiTimer.Stop();
                notBildirimiTimer.Dispose();
                notBildirimiTimer = null;
            }

            // Bildirim etiketini kaldır
            if (notBildirimLabel != null)
            {
                if (this.Controls.Contains(notBildirimLabel))
                {
                    this.Controls.Remove(notBildirimLabel);
                }
                notBildirimLabel.Dispose();
                notBildirimLabel = null;
            }
        }

        // Not var mı yok mu kontrolü
        public bool CariNotKontrol(string cariKodu)
        {
            int notSayisi = GetNotSayisi(cariKodu);
            return notSayisi > 0;
        }
        //notlar bitiş

        // Sınıf düzeyinde sayfalama değişkenleri
        public int currentPageIndex = 0;
        public List<DataGridViewRow> allRowsCache = null;

        public void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                // İlk yazdırma çağrısında
                if (currentPageIndex == 0)
                {
                    // Yazdırılacak satırları önbelleğe al
                    allRowsCache = new List<DataGridViewRow>();
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (!row.IsNewRow)
                            allRowsCache.Add(row);
                    }
                }

                // Yazı tipleri
                Font titleFont = new Font("Arial", 14, FontStyle.Bold);
                Font subtitleFont = new Font("Arial", 10, FontStyle.Bold);
                Font headerFont = new Font("Arial", 10, FontStyle.Bold);
                Font contentFont = new Font("Arial", 9);
                Font footerFont = new Font("Arial", 9, FontStyle.Bold);

                // Sayfa kenarları
                float leftMargin = e.MarginBounds.Left;
                float topMargin = e.MarginBounds.Top;
                float pageWidth = e.MarginBounds.Width;
                float pageHeight = e.MarginBounds.Height;

                // Mevcut sayfanın Y pozisyonu
                float currentY = topMargin;

                // BAŞLIK - Her sayfada gösterilir
                Form4 form4Instance = Application.OpenForms.OfType<Form4>().FirstOrDefault();
                string firmaAdi = form4Instance?.FirmaAdi ?? "Firma Adı Belirtilmemiş";

                // Firma adı başlığı
                string baslik = "Firma: " + firmaAdi;
                SizeF baslikSize = e.Graphics.MeasureString(baslik, titleFont);
                float baslikX = leftMargin + (pageWidth - baslikSize.Width) / 2;
                e.Graphics.DrawString(baslik, titleFont, Brushes.Black, baslikX, currentY);
                currentY += titleFont.GetHeight(e.Graphics) + 5;

                // Başlık altı çizgi
                e.Graphics.DrawLine(Pens.Red, leftMargin, currentY, leftMargin + pageWidth, currentY);
                currentY += 10;

                // Cari bilgisi ve tarih
                string cariBilgisi = "Cari: " + button3.Text;
                string tarihBilgisi = "Rapor Tarihi: " + DateTime.Now.ToString("dd.MM.yyyy - HH:mm");
                e.Graphics.DrawString(cariBilgisi, subtitleFont, Brushes.Black, leftMargin, currentY);
                SizeF tarihSize = e.Graphics.MeasureString(tarihBilgisi, subtitleFont);
                e.Graphics.DrawString(tarihBilgisi, subtitleFont, Brushes.Black,
                                        leftMargin + pageWidth - tarihSize.Width, currentY);
                currentY += subtitleFont.GetHeight(e.Graphics) + 5;

                // Alt çizgi
                e.Graphics.DrawLine(Pens.Red, leftMargin, currentY, leftMargin + pageWidth, currentY);
                currentY += 10;

                // KOLON BAŞLIKLARI
                // Kolon genişlikleri - oran olarak belirliyoruz
                float[] colWidthRatios = { 0.15f, 0.20f, 0.35f, 0.15f, 0.15f };
                float[] colWidths = new float[colWidthRatios.Length];
                float[] colPositions = new float[colWidthRatios.Length];

                colPositions[0] = leftMargin;
                for (int i = 0; i < colWidthRatios.Length; i++)
                {
                    colWidths[i] = pageWidth * colWidthRatios[i];
                    if (i > 0)
                        colPositions[i] = colPositions[i - 1] + colWidths[i - 1];
                }

                // Kolon başlıkları
                string[] headers = { "Tarih", "Tür", "Açıklama", "Borç", "Tahsilat" };
                for (int i = 0; i < headers.Length; i++)
                {
                    RectangleF headerRect = new RectangleF(colPositions[i], currentY, colWidths[i], headerFont.GetHeight(e.Graphics));

                    StringFormat sf = new StringFormat();
                    if (i <= 2) // İlk 3 sütun sola hizalı
                        sf.Alignment = StringAlignment.Near;
                    else // Son 2 sütun sağa hizalı
                        sf.Alignment = StringAlignment.Far;

                    e.Graphics.DrawString(headers[i], headerFont, Brushes.Black, headerRect, sf);
                }
                currentY += headerFont.GetHeight(e.Graphics) + 5;

                // Başlık altı çizgi
                e.Graphics.DrawLine(Pens.Red, leftMargin, currentY, leftMargin + pageWidth, currentY);
                currentY += 5;

                // Bir sayfaya sığacak satır sayısını hesaplayalım
                float headerHeight = 150; // Başlık, tarih bilgisi vb. için alan
                float standardRowHeight = 30; // Bir satırın standart yüksekliği
                float footerHeight = 30; // Sayfa numarası için alan
                

                // Kullanılabilir içerik alanı (son sayfada özet için yer ayırarak)
                float availableHeight = pageHeight - headerHeight - footerHeight;
                

                // Bu sayfada gösterilecek maksimum satır sayısı
                int rowsPerPage = (int)(availableHeight / standardRowHeight);

                // Toplam sayfa sayısını hesaplayalım
                int totalPages = (int)Math.Ceiling((double)allRowsCache.Count / rowsPerPage);

                // Bu sayfada gösterilecek satırların başlangıç ve bitiş indeksleri
                int startIndex = currentPageIndex * rowsPerPage;
                int endIndex = Math.Min(startIndex + rowsPerPage - 1, allRowsCache.Count - 1);

                // İÇERİK SATIRLARI
                for (int i = startIndex; i <= endIndex && i < allRowsCache.Count; i++)
                {
                    DataGridViewRow row = allRowsCache[i];

                    // Satırın bilgilerini al
                    string tarih = row.Cells["Tarih"].Value?.ToString() ?? "";
                    string tur = row.Cells["Tür"].Value?.ToString() ?? "";
                    string aciklama = row.Cells["Açıklama"].Value?.ToString() ?? "";
                    string borc = row.Cells["Borç"].Value?.ToString() ?? "₺0,00";
                    string tahsilat = row.Cells["Tahsilat"].Value?.ToString() ?? "₺0,00";

                    // Satırı yazdır
                    // Tarih
                    RectangleF tarihRect = new RectangleF(colPositions[0], currentY, colWidths[0], standardRowHeight);
                    e.Graphics.DrawString(tarih, contentFont, Brushes.Black, tarihRect);

                    // Tür
                    RectangleF turRect = new RectangleF(colPositions[1], currentY, colWidths[1], standardRowHeight);
                    e.Graphics.DrawString(tur, contentFont, Brushes.Black, turRect);

                    // Açıklama
                    RectangleF aciklamaRect = new RectangleF(colPositions[2], currentY, colWidths[2], standardRowHeight);
                    e.Graphics.DrawString(aciklama, contentFont, Brushes.Black, aciklamaRect);

                    // Borç (sağa hizalı ve kırmızı)
                    RectangleF borcRect = new RectangleF(colPositions[3], currentY, colWidths[3], standardRowHeight);
                    StringFormat rightAlign = new StringFormat { Alignment = StringAlignment.Far };
                    e.Graphics.DrawString(borc, contentFont, Brushes.Red, borcRect, rightAlign);

                    // Tahsilat (sağa hizalı ve mavi)
                    RectangleF tahsilatRect = new RectangleF(colPositions[4], currentY, colWidths[4], standardRowHeight);
                    e.Graphics.DrawString(tahsilat, contentFont, Brushes.Blue, tahsilatRect, rightAlign);

                    // Satır ayırıcı çizgi
                    currentY += standardRowHeight;
                    e.Graphics.DrawLine(Pens.LightGray, leftMargin, currentY, leftMargin + pageWidth, currentY);
                }

                // SAYFA NUMARASI
                string pageText = $"Sayfa {currentPageIndex + 1} / {totalPages}";
                SizeF pageSize = e.Graphics.MeasureString(pageText, contentFont);
                e.Graphics.DrawString(pageText, contentFont, Brushes.Gray,
                                        leftMargin + pageWidth - pageSize.Width,
                                        e.MarginBounds.Bottom - pageSize.Height);

                // Sayfalamanın devam edip etmeyeceğini belirle
                if (currentPageIndex < totalPages - 1)
                {
                    currentPageIndex++;
                    e.HasMorePages = true;
                }
                else
                {
                    // Yazdırma tamamlandı, değişkenleri sıfırla
                    currentPageIndex = 0;
                    allRowsCache = null;
                    e.HasMorePages = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Yazdırma sırasında hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                currentPageIndex = 0;
                allRowsCache = null;
                e.HasMorePages = false;
            }
        }

        public void yazdir_Click(object sender, EventArgs e)
        {
            try
            {
                // Yazdırma işlemi başlamadan önce, sayfa indeksini sıfırla
                currentPageIndex = 0;
                allRowsCache = null;

                // Print Preview Dialog ayarları
                printPreviewDialog.Document = printDocument;
                printPreviewDialog.StartPosition = FormStartPosition.CenterScreen;
                printPreviewDialog.Size = new Size(1000, 600);

                // Önizleme göster
                printPreviewDialog.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Yazdırma önizleme sırasında hata oluştu: {ex.Message}",
                                "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public string GetFirmaAdi()
        {
            Form4 form4Instance = Application.OpenForms.OfType<Form4>().FirstOrDefault();
            return form4Instance?.FirmaAdi ?? "Firma Adı Belirtilmemiş";
        }

        public string GetCariBilgisi()
        {
            return button3.Text;
        }



        public string GetCariKoduByUnvan(string unvani)
        {
            string cariKodu = "";

            using (SQLiteConnection connection = new SQLiteConnection("Data Source=veresiye.db;Version=3;"))
            {
                connection.Open();

                string query = "SELECT CariKodu FROM Cari WHERE Unvani = @Unvani";

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
            // Form4'ün mevcut örneğini buluyoruz
            Form4 form4Instance = Application.OpenForms.OfType<Form4>().FirstOrDefault();

            if (form4Instance != null)
            {
                // Form4'ün örneğinden OpenBackupRestoreForm metodunu çağırıyoruz
                form4Instance.OpenBackupRestoreForm();
            }
            else
            {
                MessageBox.Show("Form4 bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        public void genelAyarlarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // FormGenelAyarlar formunu açıyoruz
            FormGenelAyarlar formGenelAyarlar = new FormGenelAyarlar();
            formGenelAyarlar.ShowDialog();  // Modal olarak açıyoruz
        }

        public void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
                            // Form4'ün mevcut örneğini alalım
                            Form4 form4Instance = Application.OpenForms.OfType<Form4>().FirstOrDefault();

                            if (form4Instance != null)
                            {
                                // Form4'ün örneğinden DownloadAndInstallUpdate metodunu çağırıyoruz
                                form4Instance.DownloadAndInstallUpdate(downloadLink);
                            }
                            else
                            {
                                MessageBox.Show("Form4 bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
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

        public void hareketlerF3ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            // Hareketler formunu açıyoruz
            Hareketler hareketlerForm = new Hareketler();
            hareketlerForm.StartPosition = FormStartPosition.CenterScreen;  // Formun ekranın ortasında açılmasını sağlar
            hareketlerForm.ShowDialog();  // Modal olarak açıyoruz
        }


        public void GecikmisBorcuHesapla()
        {
            if (string.IsNullOrEmpty(currentCariKodu))
            {
                MessageBox.Show("Lütfen geçerli bir cari seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal gecikmisBorc = 0;
            decimal toplamTahsilat = 0;
            DateTime bugun = DateTime.Today;
            int vadeGunu = GetVadeGunu(currentCariKodu);
            DateTime gecikmeyeDusenTarih = DateTime.MinValue;

            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Borçları al
                string borcQuery = "SELECT tarih, borc FROM cari_hareketleri WHERE cari_kodu = @CariKodu AND borc > 0 ORDER BY tarih";
                using (SQLiteCommand command = new SQLiteCommand(borcQuery, connection))
                {
                    command.Parameters.AddWithValue("@CariKodu", currentCariKodu);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DateTime tarih = Convert.ToDateTime(reader["tarih"]);
                            decimal borc = Convert.ToDecimal(reader["borc"]);

                            DateTime vadeTarihi = tarih.AddDays(vadeGunu);

                            if (vadeTarihi < bugun) // Eğer vadesi geçmişse gecikmiş borca ekle
                            {
                                gecikmisBorc += borc;
                                if (gecikmeyeDusenTarih == DateTime.MinValue)
                                    gecikmeyeDusenTarih = vadeTarihi;
                            }
                        }
                    }
                }

                // Tahsilatları al
                string tahsilatQuery = "SELECT SUM(tahsilat) FROM cari_hareketleri WHERE cari_kodu = @CariKodu";
                using (SQLiteCommand command = new SQLiteCommand(tahsilatQuery, connection))
                {
                    command.Parameters.AddWithValue("@CariKodu", currentCariKodu);
                    object result = command.ExecuteScalar();
                    toplamTahsilat = result != DBNull.Value ? Convert.ToDecimal(result) : 0;
                }
            }

            // Tahsilatları gecikmiş borçtan düş
            gecikmisBorc -= toplamTahsilat;
            if (gecikmisBorc < 0) gecikmisBorc = 0;

            // Popup göster
            FormGecikmeBorc popup = new FormGecikmeBorc(
                bakiye: textBox3.Text,
                gecikmisBorc: gecikmisBorc.ToString("C2"),
                vadeGun: vadeGunu.ToString(),
                faturaTarihi: gecikmeyeDusenTarih.ToString("dd.MM.yyyy"),
                cariAdi: button3.Text // 🔥 Buton3'te cari adı yazıyordu, onu aldık
            );
            popup.ShowDialog();
        }


        public void RefreshDataGridAndFocusLastRow()
        {
            // 🔥 DataGridView güncelleniyor
            LoadCariHareketleri(currentCariKodu);

            // 🔥 En son satıra odaklan
            if (dataGridView1.Rows.Count > 0)
            {
                int lastRowIndex = dataGridView1.Rows.Count - 1;
                dataGridView1.ClearSelection();
                dataGridView1.Rows[lastRowIndex].Selected = true;
                dataGridView1.FirstDisplayedScrollingRowIndex = lastRowIndex;
            }
        }


        public int GetVadeGunu(string cariKodu)
        {
            int vadeGunu = 0;
            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT VadeGunu FROM Cari WHERE CariKodu = @CariKodu";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CariKodu", cariKodu);
                    object result = command.ExecuteScalar();

                    if (result != DBNull.Value && result != null)
                    {
                        vadeGunu = Convert.ToInt32(result);
                    }
                }
            }

            return vadeGunu;
        }
               

        public void posTakipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // posTakip formunu oluştur
            postakip posForm = new postakip();

            // Formu açmadan önce DataGridView'i güncelle
            posForm.BankalariYukle(); // Verileri yükle

            // Formu modal olarak aç
            posForm.ShowDialog(); // Yeni formu aç
        }

        public void krediKartıTakipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Karttakip formunun bir örneğini oluştur
            Karttakip karttakipForm = new Karttakip();

            // Karttakip formunu göster
            karttakipForm.Show();
        }

        // Form constructor'ına veya Carihareketler_Load metoduna ekleyin
        // DataGridView için context menu ve düzenleme geçmişini görüntüleme işlemleri
        

        // Düzenlenmiş satırları belirginleştirmek için CellFormatting eventi
        public void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.RowIndex >= dataGridView1.Rows.Count)
                    return;

                int hareketId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id"].Value);

                // Düzenlenmiş kaydı kontrol et
                if (KayitDuzenlenmisMi(hareketId))
                {
                    // Satırın arka plan rengini değiştir
                    dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightYellow;
                }
            }
            catch
            {
                // Hata durumunda hiçbir şey yapma
            }
        }

        // Düzenlenmiş hücreler için ipucu (tooltip) gösterme
        public void DataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.RowIndex >= dataGridView1.Rows.Count)
                    return;

                // İlk sütun ID olarak varsayıyoruz
                if (dataGridView1.Rows[e.RowIndex].Cells[0].Value == null)
                    return;

                int hareketId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);

                // Düzenlenmiş kaydı kontrol et
                if (KayitDuzenlenmisMi(hareketId))
                {
                    // Hücreye ipucu metni ekle
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText =
                        "Bu kayıt düzenlenmiştir. Geçmişi görmek için sağ tıklayın ve 'Düzenleme Geçmişini Göster' seçeneğini seçin.";
                }
                else
                {
                    // Düzenlenmemişse tooltip temizle
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = "";
                }
            }
            catch
            {
                // Hata durumunda hiçbir şey yapma
            }
        }

        // Düzenleme işlemini başlat
        public void DuzenleHareket(int hareketId)
        {
            try
            {
                // Veritabanından hareket bilgilerini al
                string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    // SQL sorgusunu düzelttim - Alt sorgu ile CariUnvan alınıyor
                    string query = @"
                SELECT ch.id, ch.cari_kodu, 
                       (SELECT CariUnvan FROM Cari WHERE CariKodu = ch.cari_kodu) AS CariUnvan,
                       ch.tarih, ch.aciklama, ch.borc, ch.tahsilat,
                       ch.tahsilat_turu, ch.odemeyiyapan, ch.vade_gun, ch.borcu_ekleyen
                FROM cari_hareketleri ch
                WHERE ch.id = @HareketId";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@HareketId", hareketId);

                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Hareket verilerini dynamic objede topla
                                dynamic formData = new System.Dynamic.ExpandoObject();
                                formData.HareketId = reader.GetInt32(0);
                                formData.CariKodu = reader.GetString(1);
                                formData.CariUnvan = reader.IsDBNull(2) ? "" : reader.GetString(2);
                                formData.Tarih = reader.GetDateTime(3);
                                formData.Aciklama = reader.GetString(4);

                                // Borc ve tahsilat için null kontrolü
                                formData.BorcTutar = reader.IsDBNull(5) ? 0m : reader.GetDecimal(5);
                                formData.TahsilatTutar = reader.IsDBNull(6) ? 0m : reader.GetDecimal(6);

                                // Tahsilat türü ve ödemeyi yapan kişi için null kontrolü
                                formData.TahsilatTuru = reader.IsDBNull(7) ? "" : reader.GetString(7);
                                formData.OdemeyiYapan = reader.IsDBNull(8) ? "" : reader.GetString(8);

                                // Vade günü ve borcu ekleyen kişi için null kontrolü
                                formData.VadeGun = reader.IsDBNull(9) ? 0 : reader.GetInt32(9);
                                formData.BorcuEkleyen = reader.IsDBNull(10) ? "" : reader.GetString(10);

                                // Kayıt tipine göre uygun düzenleme formunu aç
                                if (formData.TahsilatTutar > 0)
                                {
                                    GuncelleTahsilatForm(formData);
                                }
                                else if (formData.BorcTutar > 0)
                                {
                                    GuncelleBorcForm(formData);
                                }
                                else
                                {
                                    MessageBox.Show("Bu kayıt türü tanınmadı veya düzenlenemez.", "Uyarı",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Kayıt bulunamadı.", "Hata",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kayıt düzenleme sırasında bir hata oluştu: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       
        // Menü öğesi tıklama olayı için handler ekle
        public void ShowTahsilatDetails_Click(object sender, EventArgs e)
        {
            // Seçili satır var mı kontrol et
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen önce bir satır seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

            // ID değerini al
            if (selectedRow.Cells["id"].Value == null)
            {
                MessageBox.Show("Seçili satırda geçerli bir ID bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string id = selectedRow.Cells["id"].Value.ToString();
            string tur = selectedRow.Cells["Tür"].Value.ToString();

            // Sadece "Tahsilat" içeren kayıtlar için detay göster
            if (!tur.Contains("Tahsilat"))
            {
                MessageBox.Show("Bu kayıt bir tahsilat işlemi değil!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Veritabanından tahsilat detaylarını al
            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT odemeyi_yapan, tahsilat_turu FROM cari_hareketleri WHERE id = @Id";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string odemeyiYapan = reader["odemeyi_yapan"] != DBNull.Value ? reader["odemeyi_yapan"].ToString() : "Belirtilmemiş";
                            string tahsilatTuru = reader["tahsilat_turu"] != DBNull.Value ? reader["tahsilat_turu"].ToString() : "Belirtilmemiş";

                            // Daha şık bir dialog gösterimi için özel form kullanıyoruz
                            ShowTahsilatDetailsForm(selectedRow, odemeyiYapan, tahsilatTuru);
                        }
                        else
                        {
                            MessageBox.Show("Tahsilat detayları bulunamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
        }

        // Daha şık bir detay formu gösterme metodu
        public void ShowTahsilatDetailsForm(DataGridViewRow row, string odemeyiYapan, string tahsilatTuru)
        {
            // Tahsilat bilgilerini al
            string tarih = row.Cells["Tarih"].Value.ToString();
            string aciklama = row.Cells["Açıklama"].Value.ToString();
            string tahsilatTutar = row.Cells["Tahsilat"].Value.ToString();

            // Modern detay formu oluştur
            Form detailsForm = new Form();
            detailsForm.Text = "Tahsilat Detayları";
            detailsForm.Size = new Size(420, 320);
            detailsForm.StartPosition = FormStartPosition.CenterParent;
            detailsForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            detailsForm.MaximizeBox = false;
            detailsForm.MinimizeBox = false;
            detailsForm.BackColor = Color.White;
            detailsForm.ShowIcon = false;
            detailsForm.ShowInTaskbar = false;

            // Panel oluştur
            Panel panel = new Panel();
            panel.Dock = DockStyle.Fill;
            panel.Padding = new Padding(20);

            // Başlık label
            Label lblHeader = new Label();
            lblHeader.Text = "TAHSİLAT DETAYLARI";
            lblHeader.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblHeader.ForeColor = Color.FromArgb(30, 30, 30);
            lblHeader.Location = new Point(20, 15);
            lblHeader.AutoSize = true;

            // Ayırıcı çizgi
            Panel separatorLine = new Panel();
            separatorLine.BackColor = Color.FromArgb(230, 230, 230);
            separatorLine.Location = new Point(20, 45);
            separatorLine.Size = new Size(370, 1);

            // Alan başlıkları ve değerleri
            Label[] labels = new Label[5];
            Label[] values = new Label[5];
            string[] titles = { "Tarih:", "Tutar:", "Açıklama:", "Ödemeyi Yapan:", "Ödeme Türü:" };
            string[] dataValues = { tarih, tahsilatTutar, aciklama, odemeyiYapan, tahsilatTuru };
            Color[] valueColors = {
        Color.FromArgb(30, 30, 30),          // Tarih için koyu gri
        Color.FromArgb(0, 150, 0),           // Tutar için yeşil
        Color.FromArgb(30, 30, 30),          // Açıklama için koyu gri
        Color.FromArgb(0, 0, 220),           // Ödemeyi Yapan için mavi
        Color.FromArgb(0, 0, 220)            // Ödeme Türü için mavi
    };

            for (int i = 0; i < 5; i++)
            {
                // Label (başlık)
                labels[i] = new Label();
                labels[i].Text = titles[i];
                labels[i].Font = new Font("Segoe UI", 10, FontStyle.Bold);
                labels[i].ForeColor = Color.FromArgb(80, 80, 80);
                labels[i].Location = new Point(25, 60 + (i * 35));
                labels[i].AutoSize = true;

                // Value (değer)
                values[i] = new Label();
                values[i].Text = dataValues[i];
                values[i].Font = new Font("Segoe UI", 10);
                values[i].ForeColor = valueColors[i];
                values[i].Location = new Point(150, 60 + (i * 35));
                values[i].AutoSize = true;
                values[i].MaximumSize = new Size(220, 0);
            }

            // Kapat butonu
            Button btnClose = new Button();
            btnClose.Text = "Kapat";
            btnClose.DialogResult = DialogResult.OK;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.BackColor = Color.FromArgb(0, 120, 215);
            btnClose.ForeColor = Color.White;
            btnClose.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            btnClose.Location = new Point(300, 230);
            btnClose.Size = new Size(90, 30);
            btnClose.Cursor = Cursors.Hand;

            // Buton hover efekti
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 100, 200);

            // Forma kontrolleri ekle
            panel.Controls.Add(lblHeader);
            panel.Controls.Add(separatorLine);

            for (int i = 0; i < 5; i++)
            {
                panel.Controls.Add(labels[i]);
                panel.Controls.Add(values[i]);
            }

            panel.Controls.Add(btnClose);
            detailsForm.Controls.Add(panel);

            // Formu göster
            detailsForm.ShowDialog(this);
        }
        
        // Borç detayları için sağ tık menüsü ve detay formu Carihareketler.cs'e eklenir
        public void ShowBorcDetails_Click(object sender, EventArgs e)
        {
            // Seçili satır var mı kontrol et
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen önce bir satır seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

            // ID değerini al
            if (selectedRow.Cells["id"].Value == null)
            {
                MessageBox.Show("Seçili satırda geçerli bir ID bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string id = selectedRow.Cells["id"].Value.ToString();
            string tur = selectedRow.Cells["Tür"].Value.ToString();

            // Sadece "Borç" içeren kayıtlar için detay göster
            if (!tur.Contains("Borç"))
            {
                MessageBox.Show("Bu kayıt bir borç işlemi değil!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Veritabanından borç detaylarını al
            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM cari_hareketleri WHERE id = @Id";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            DateTime islemTarihi = Convert.ToDateTime(reader["tarih"]);
                            string aciklama = reader["aciklama"] != DBNull.Value ? reader["aciklama"].ToString() : "";
                            decimal borcTutar = reader["borc"] != DBNull.Value ? Convert.ToDecimal(reader["borc"]) : 0;
                            string borcuEkleyen = reader["borcu_ekleyen"] != DBNull.Value ? reader["borcu_ekleyen"].ToString() : "Belirtilmemiş";
                            int vadeGun = reader["vade_gun"] != DBNull.Value ? Convert.ToInt32(reader["vade_gun"]) : 0;

                            // Şık bir detay formu göster
                            ShowBorcDetailsForm(selectedRow, islemTarihi, aciklama, borcTutar, borcuEkleyen, vadeGun);
                        }
                        else
                        {
                            MessageBox.Show("Borç detayları bulunamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
        }

        // Borç detayları formu - Yeni sürüm
        public void ShowBorcDetailsForm(DataGridViewRow row, DateTime islemTarihi, string aciklama, decimal borcTutar, string borcuEkleyen, int vadeGun)
        {
            // Borç bilgilerini al
            string tarih = islemTarihi.ToString("dd.MM.yyyy HH:mm");
            string tutar = borcTutar.ToString("C2");

            // Modern detay formu oluştur
            Form detailsForm = new Form();
            detailsForm.Text = "Borç Detayları";
            detailsForm.Size = new Size(420, 320); // Daha fazla alan için boyutu arttırdık
            detailsForm.StartPosition = FormStartPosition.CenterParent;
            detailsForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            detailsForm.MaximizeBox = false;
            detailsForm.MinimizeBox = false;
            detailsForm.BackColor = Color.White;
            detailsForm.ShowIcon = false;
            detailsForm.ShowInTaskbar = false;

            // Panel oluştur
            Panel panel = new Panel();
            panel.Dock = DockStyle.Fill;
            panel.Padding = new Padding(20);

            // Başlık label
            Label lblHeader = new Label();
            lblHeader.Text = "BORÇ DETAYLARI";
            lblHeader.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblHeader.ForeColor = Color.FromArgb(30, 30, 30);
            lblHeader.Location = new Point(20, 15);
            lblHeader.AutoSize = true;

            // Ayırıcı çizgi
            Panel separatorLine = new Panel();
            separatorLine.BackColor = Color.FromArgb(230, 230, 230);
            separatorLine.Location = new Point(20, 45);
            separatorLine.Size = new Size(370, 1);

            // Alan başlıkları ve değerleri
            Label[] labels = new Label[5]; // Yeni alanlar için arttırdık
            Label[] values = new Label[5];
            string[] titles = { "Tarih:", "Tutar:", "Açıklama:", "Borcu Ekleyen:", "Vade Günü:" }; // Yeni alanlar ekledik
            string[] dataValues = { tarih, tutar, aciklama, borcuEkleyen, vadeGun.ToString() + " gün" }; // Yeni veriler ekledik
            Color[] valueColors = {
        Color.FromArgb(30, 30, 30),          // Tarih için koyu gri
        Color.FromArgb(200, 0, 0),           // Tutar için kırmızı (borç olduğu için)
        Color.FromArgb(30, 30, 30),          // Açıklama için koyu gri
        Color.FromArgb(0, 0, 220),           // Borcu Ekleyen için mavi
        Color.FromArgb(30, 30, 30)           // Vade Günü için koyu gri
    };

            for (int i = 0; i < 5; i++) // Yeni alanlar için döngüyü uzattık
            {
                // Label (başlık)
                labels[i] = new Label();
                labels[i].Text = titles[i];
                labels[i].Font = new Font("Segoe UI", 10, FontStyle.Bold);
                labels[i].ForeColor = Color.FromArgb(80, 80, 80);
                labels[i].Location = new Point(25, 60 + (i * 35));
                labels[i].AutoSize = true;

                // Value (değer)
                values[i] = new Label();
                values[i].Text = dataValues[i];
                values[i].Font = new Font("Segoe UI", 10);
                values[i].ForeColor = valueColors[i];
                values[i].Location = new Point(150, 60 + (i * 35));
                values[i].AutoSize = true;
                values[i].MaximumSize = new Size(220, 0);
            }

            // Kapat butonu
            Button btnClose = new Button();
            btnClose.Text = "Kapat";
            btnClose.DialogResult = DialogResult.OK;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.BackColor = Color.FromArgb(204, 0, 0); // Borç için kırmızı tonu
            btnClose.ForeColor = Color.White;
            btnClose.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            btnClose.Location = new Point(300, 235); // Konumu güncelledik
            btnClose.Size = new Size(90, 30);
            btnClose.Cursor = Cursors.Hand;

            // Buton hover efekti
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatAppearance.MouseOverBackColor = Color.FromArgb(180, 0, 0); // Hover için daha koyu kırmızı

            // Forma kontrolleri ekle
            panel.Controls.Add(lblHeader);
            panel.Controls.Add(separatorLine);

            for (int i = 0; i < 5; i++) // Yeni alanlar için döngüyü uzattık
            {
                panel.Controls.Add(labels[i]);
                panel.Controls.Add(values[i]);
            }

            panel.Controls.Add(btnClose);
            detailsForm.Controls.Add(panel);

            // Formu göster
            detailsForm.ShowDialog(this);
        }

        public void hrktlergerigit_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(
                textBox3.Text.Replace("₺", "").Trim(), // ₺ sembolünü kaldır
                NumberStyles.Currency, // Para birimi stili
                new CultureInfo("tr-TR"), // Türkçe kültür
                out decimal bakiye))
            {
                // ✅ Cari tablosundaki bakiyeyi güncelle
                UpdateCariBakiye(currentCariKodu, bakiye);

                // ✅ Olayı tetikle (Form4 için)
                OnBakiyeGuncelle?.Invoke(currentCariKodu, bakiye);

                // ✅ Pasif Hesap kontrolünü yap
                FormCariEkle formCariEkle = new FormCariEkle();
                formCariEkle.PasifHesapKontrol(currentCariKodu);

                // ✅ Eğer "Carihareketler" formu açıksa, "UpdateFirmaAdi()" çağır
                if (Application.OpenForms["Carihareketler"] is Carihareketler cariForm)
                {
                    cariForm.UpdateFirmaAdi();
                }
            }
            else
            {
                MessageBox.Show("Bakiye değeri parse edilemedi. Lütfen geçerli bir sayı girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // ✅ Formu kapat (Geri tuşu olduğu için)
            this.Close();
        }

        public void gecikmesorgula_Click(object sender, EventArgs e)
        {
            GecikmisBorcuHesapla();
        }

        public void hrktsil_Click(object sender, EventArgs e)
        {
            DeleteSelectedRow(); // Satır silme işlemi
        }

        public void hrketekle_Click(object sender, EventArgs e)
        {
            // ContextMenuStrip'i üste doğru açmak için butonun yüksekliğinden çıkararak gösteriyoruz
            contextMenuStrip1.Show(hrketekle, new System.Drawing.Point(0, -contextMenuStrip1.Height));
        }

        public void hrktdegistir_Click(object sender, EventArgs e)
        {
            try
            {
                // Seçili satır var mı kontrol et
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Lütfen düzenlemek istediğiniz bir hareket seçin.", "Uyarı",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                // Seçili kaydın ID'sini al
                if (selectedRow.Cells["id"].Value == null)
                {
                    MessageBox.Show("Seçili satırda geçerli bir ID bulunamadı.", "Hata",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int hareketId = Convert.ToInt32(selectedRow.Cells["id"].Value);

                // Tür sütununu bul ve değerini al
                string islemTipi = "";
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    if (column.HeaderText.Contains("Tür") || column.HeaderText == "tur" || column.HeaderText == "Tur")
                    {
                        islemTipi = selectedRow.Cells[column.Index].Value?.ToString() ?? "";
                        break;
                    }
                }

                if (string.IsNullOrEmpty(islemTipi))
                {
                    MessageBox.Show("Hareket türü belirlenemedi.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Veritabanı ayarlarını güncelle (kilitlenmeyi önlemek için)
                SQLiteConnection.ClearAllPools();
                GC.Collect();
                GC.WaitForPendingFinalizers();

                string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // PRAGMA ayarları ile kilitlenmeyi önle
                    using (SQLiteCommand pragmaCommand = new SQLiteCommand(connection))
                    {
                        pragmaCommand.CommandText = "PRAGMA journal_mode = WAL;";
                        pragmaCommand.ExecuteNonQuery();

                        pragmaCommand.CommandText = "PRAGMA synchronous = NORMAL;";
                        pragmaCommand.ExecuteNonQuery();

                        pragmaCommand.CommandText = "PRAGMA busy_timeout = 30000;";
                        pragmaCommand.ExecuteNonQuery();
                    }

                    // Hareket detaylarını al
                    string query = "SELECT * FROM cari_hareketleri WHERE id = @Id";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", hareketId);
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string cariKodu = reader["cari_kodu"].ToString();

                                // Cari unvanını al
                                string cariUnvan = GetCariUnvanFromKodu(cariKodu);
                                if (string.IsNullOrEmpty(cariUnvan))
                                {
                                    MessageBox.Show("Cari unvanı bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }

                                // Hareket bilgilerini al
                                DateTime islemTarihi = Convert.ToDateTime(reader["tarih"]);
                                string aciklama = reader["aciklama"] != DBNull.Value ? reader["aciklama"].ToString() : "";

                                if (islemTipi.Contains("Borç"))
                                {
                                    // Borç bilgilerini al
                                    decimal borcTutar = reader["borc"] != DBNull.Value ? Convert.ToDecimal(reader["borc"]) : 0;
                                    string borcuEkleyen = reader["borcu_ekleyen"] != DBNull.Value ? reader["borcu_ekleyen"].ToString() : "";
                                    int vadeGun = reader["vade_gun"] != DBNull.Value ? Convert.ToInt32(reader["vade_gun"]) : 0;

                                    // Form için verileri hazırla
                                    var formData = new
                                    {
                                        HareketId = hareketId,
                                        CariKodu = cariKodu,
                                        CariUnvan = cariUnvan,
                                        Tarih = islemTarihi,
                                        Aciklama = aciklama,
                                        BorcTutar = borcTutar,
                                        BorcuEkleyen = borcuEkleyen,
                                        VadeGun = vadeGun
                                    };

                                    // Borcekle formunu başlatmak için ayrı bir metot kullan
                                    GuncelleBorcForm(formData);
                                }
                                else if (islemTipi.Contains("Tahsilat"))
                                {
                                    // Tahsilat bilgilerini al
                                    decimal tahsilatTutar = reader["tahsilat"] != DBNull.Value ? Convert.ToDecimal(reader["tahsilat"]) : 0;
                                    string odemeyiYapan = reader["odemeyi_yapan"] != DBNull.Value ? reader["odemeyi_yapan"].ToString() : "";
                                    string tahsilatTuru = reader["tahsilat_turu"] != DBNull.Value ? reader["tahsilat_turu"].ToString() : "";

                                    // Form için verileri hazırla
                                    var formData = new
                                    {
                                        HareketId = hareketId,
                                        CariKodu = cariKodu,
                                        CariUnvan = cariUnvan,
                                        Tarih = islemTarihi,
                                        Aciklama = aciklama,
                                        TahsilatTutar = tahsilatTutar,
                                        OdemeyiYapan = odemeyiYapan,
                                        TahsilatTuru = tahsilatTuru
                                    };

                                    // Tahsilat formunu başlatmak için ayrı bir metot kullan
                                    GuncelleTahsilatForm(formData);
                                }
                                else
                                {
                                    MessageBox.Show("Bu tür işlem düzenlenemez.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Kayıt bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"İşlem sırasında bir hata oluştu: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Borcekle formunu güncelleme için kullanma
        public void GuncelleBorcForm(dynamic formData)
        {
            // Temizleme ve eski bağlantıları kapatma işlemleri
            SQLiteConnection.ClearAllPools();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            // Borcekle formu oluştur
            Borcekle borcekleForm = new Borcekle(formData.CariUnvan);
            // Form başlığını düzenleme uygun hale getir
            borcekleForm.Text = "Borç Kaydı Düzenle";
            // Form yüklendiğinde verileri doldur
            borcekleForm.Load += (s, e) => {
                try
                {
                    // Verileri form alanlarına doldur
                    borcekleForm.textBox2.Text = formData.Aciklama;
                    borcekleForm.textBox3.Text = formData.BorcTutar.ToString("0.00").Replace(".", ",") + " TL";
                    if (borcekleForm.borcuekleyentextbox != null)
                    {
                        borcekleForm.borcuekleyentextbox.Text = formData.BorcuEkleyen;
                        borcekleForm.borcuekleyentextbox.ReadOnly = true; // Salt okunur yap
                        borcekleForm.borcuekleyentextbox.BackColor = Color.FromArgb(240, 240, 240); // Gri arka plan
                    }
                    if (borcekleForm.vadegungir != null)
                        borcekleForm.vadegungir.Text = formData.VadeGun.ToString();
                    borcekleForm.dateTimePicker1.Value = formData.Tarih;
                    borcekleForm.dateTimePicker2.Value = formData.Tarih;
                    // Kaydet butonunu Güncelle olarak değiştir
                    borcekleForm.kaydet.Text = "Güncelle";
                    // Eski click olayını kaldır, yeni click olayı ekle
                    borcekleForm.kaydet.Click -= borcekleForm.kaydet_Click;
                    borcekleForm.kaydet.Click += (sender, args) => {
                        // Değiştirme nedeni sor
                        string degistirmeNedeni = "";
                        using (var nedenForm = new Form())
                        {
                            nedenForm.Text = "Değiştirme Nedeni";
                            nedenForm.Size = new Size(400, 200);
                            nedenForm.StartPosition = FormStartPosition.CenterParent;
                            nedenForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                            nedenForm.MaximizeBox = false;
                            nedenForm.MinimizeBox = false;
                            Label lblNeden = new Label
                            {
                                Text = "Lütfen bu değişikliğin nedenini yazınız:",
                                Location = new Point(20, 20),
                                Size = new Size(350, 20)
                            };
                            TextBox txtNeden = new TextBox
                            {
                                Location = new Point(20, 50),
                                Size = new Size(350, 60),
                                Multiline = true
                            };
                            Button btnTamam = new Button
                            {
                                Text = "Tamam",
                                DialogResult = DialogResult.OK,
                                Location = new Point(210, 120),
                                Size = new Size(75, 30)
                            };
                            Button btnIptal = new Button
                            {
                                Text = "İptal",
                                DialogResult = DialogResult.Cancel,
                                Location = new Point(295, 120),
                                Size = new Size(75, 30)
                            };
                            nedenForm.Controls.AddRange(new Control[] { lblNeden, txtNeden, btnTamam, btnIptal });
                            nedenForm.AcceptButton = btnTamam;
                            nedenForm.CancelButton = btnIptal;
                            if (nedenForm.ShowDialog() == DialogResult.OK)
                            {
                                degistirmeNedeni = txtNeden.Text.Trim();
                                if (string.IsNullOrWhiteSpace(degistirmeNedeni))
                                {
                                    MessageBox.Show("Lütfen değiştirme nedenini belirtiniz.", "Uyarı",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }
                            else
                            {
                                return; // İptal edildi
                            }
                        }
                        // Vade gün kontrolü - boş bırakılamaz ve sayısal olmalı
                        if (string.IsNullOrWhiteSpace(borcekleForm.vadegungir?.Text))
                        {
                            MessageBox.Show("Vade gün bilgisi boş bırakılamaz.", "Uyarı",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            borcekleForm.vadegungir.Focus();
                            return;
                        }
                        if (!int.TryParse(borcekleForm.vadegungir?.Text, out int vadeGun))
                        {
                            MessageBox.Show("Vade gün bilgisi sayısal olmalıdır.", "Uyarı",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            borcekleForm.vadegungir.Focus();
                            return;
                        }
                        // Form verilerini al
                        string aciklama = borcekleForm.textBox2.Text;
                        string borcuEkleyen = borcekleForm.borcuekleyentextbox?.Text?.Trim() ?? "";
                        // Tutar kontrolü
                        string tutarText = borcekleForm.textBox3.Text.Replace(" TL", "").Replace(",", ".");
                        if (!decimal.TryParse(tutarText, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal borcTutar) || borcTutar <= 0)
                        {
                            MessageBox.Show("Lütfen geçerli bir borç tutarı giriniz.", "Hata",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        // Tarih bilgilerini al
                        DateTime secilenTarih = borcekleForm.dateTimePicker1.Value;
                        DateTime secilenSaat = borcekleForm.dateTimePicker2.Value;
                        // Tarih ve saati birleştir
                        DateTime birlesikTarihSaat = new DateTime(
                            secilenTarih.Year, secilenTarih.Month, secilenTarih.Day,
                            secilenSaat.Hour, secilenSaat.Minute, secilenSaat.Second);
                        string formattedDate = birlesikTarihSaat.ToString("yyyy-MM-dd HH:mm:ss");
                        // Değişiklik var mı kontrol et
                        bool degisiklikVar =
                            aciklama != formData.Aciklama ||
                            borcTutar != formData.BorcTutar ||
                            vadeGun != formData.VadeGun ||
                            birlesikTarihSaat != formData.Tarih;
                        if (!degisiklikVar)
                        {
                            MessageBox.Show("Herhangi bir değişiklik yapmadınız.", "Bilgi",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        try
                        {
                            // Veritabanı bağlantısını açıkça kapat ve havuzu temizle
                            SQLiteConnection.ClearAllPools();
                            GC.Collect();
                            GC.WaitForPendingFinalizers();
                            // Veritabanı güncelleme işlemi
                            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;" +
                                                    "Journal Mode=WAL;Synchronous=NORMAL;";
                            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                            {
                                connection.Open();
                                // Veritabanı ayarları
                                using (SQLiteCommand pragmaCommand = new SQLiteCommand(connection))
                                {
                                    pragmaCommand.CommandText = "PRAGMA journal_mode = WAL;";
                                    pragmaCommand.ExecuteNonQuery();
                                    pragmaCommand.CommandText = "PRAGMA synchronous = NORMAL;";
                                    pragmaCommand.ExecuteNonQuery();
                                    pragmaCommand.CommandText = "PRAGMA busy_timeout = 30000;";
                                    pragmaCommand.ExecuteNonQuery();
                                }
                                // Transaction başlat
                                using (SQLiteTransaction transaction = connection.BeginTransaction())
                                {
                                    try
                                    {
                                        // Eski borç tutarını al
                                        decimal eskiBorcTutar = formData.BorcTutar;
                                        // İşlem geçmişi için eski ve yeni değerleri JSON olarak oluştur
                                        string eskiDegerler = $"{{\"tarih\":\"{formData.Tarih.ToString("yyyy-MM-dd HH:mm:ss")}\",\"aciklama\":\"{formData.Aciklama}\",\"borc\":{eskiBorcTutar},\"vadeGun\":{formData.VadeGun}}}";
                                        string yeniDegerler = $"{{\"tarih\":\"{formattedDate}\",\"aciklama\":\"{aciklama}\",\"borc\":{borcTutar},\"vadeGun\":{vadeGun}}}";
                                        // Borç güncelleme
                                        string updateQuery = @"
                                    UPDATE cari_hareketleri
                                    SET tarih = @Tarih,
                                        aciklama = @Aciklama,
                                        borc = @Borc,
                                        vade_gun = @VadeGun
                                    WHERE id = @Id";
                                        using (SQLiteCommand command = new SQLiteCommand(updateQuery, connection, transaction))
                                        {
                                            command.Parameters.AddWithValue("@Id", formData.HareketId);
                                            command.Parameters.AddWithValue("@Tarih", formattedDate);
                                            command.Parameters.AddWithValue("@Aciklama", aciklama);
                                            command.Parameters.AddWithValue("@Borc", borcTutar);
                                            command.Parameters.AddWithValue("@VadeGun", vadeGun);
                                            int rowsAffected = command.ExecuteNonQuery();
                                            if (rowsAffected > 0)
                                            {
                                                // Bakiye farkını hesapla
                                                decimal bakiyeFarki = borcTutar - eskiBorcTutar;
                                                if (bakiyeFarki != 0)
                                                {
                                                    // Bakiyeyi güncelle
                                                    string bakiyeQuery = @"
                                                UPDATE Cari
                                                SET Bakiye = Bakiye + @BakiyeFarki
                                                WHERE CariKodu = @CariKodu";
                                                    using (SQLiteCommand bakiyeCommand = new SQLiteCommand(bakiyeQuery, connection, transaction))
                                                    {
                                                        bakiyeCommand.Parameters.AddWithValue("@BakiyeFarki", bakiyeFarki);
                                                        bakiyeCommand.Parameters.AddWithValue("@CariKodu", formData.CariKodu);
                                                        bakiyeCommand.ExecuteNonQuery();
                                                    }
                                                }
                                                // İşlem geçmişine kaydet
                                                string logQuery = @"
                                            INSERT INTO islem_gecmisi
                                            (islem_turu, hareket_id, kullanici, eski_degerler, yeni_degerler, degistirme_nedeni, ip_adresi)
                                            VALUES
                                            (@IslemTuru, @HareketId, @Kullanici, @EskiDegerler, @YeniDegerler, @DegistirmeNedeni, @IpAdresi)";
                                                using (SQLiteCommand logCommand = new SQLiteCommand(logQuery, connection, transaction))
                                                {
                                                    logCommand.Parameters.AddWithValue("@IslemTuru", "Borç Düzenleme");
                                                    logCommand.Parameters.AddWithValue("@HareketId", formData.HareketId);
                                                    logCommand.Parameters.AddWithValue("@IslemTarihi", DateTime.Now); // Güncel zamanı kaydet!
                                                    logCommand.Parameters.AddWithValue("@Kullanici", Environment.UserName);
                                                    logCommand.Parameters.AddWithValue("@EskiDegerler", eskiDegerler);
                                                    logCommand.Parameters.AddWithValue("@YeniDegerler", yeniDegerler);
                                                    logCommand.Parameters.AddWithValue("@DegistirmeNedeni", degistirmeNedeni);
                                                    logCommand.Parameters.AddWithValue("@IpAdresi", GetLocalIPAddress());
                                                    logCommand.ExecuteNonQuery();
                                                }
                                                transaction.Commit();
                                                MessageBox.Show("Borç kaydı başarıyla güncellendi.", "Bilgi",
                                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                borcekleForm.Close();
                                                // DataGridView'i yenile
                                                LoadCariHareketleri(formData.CariKodu);
                                            }
                                            else
                                            {
                                                transaction.Rollback();
                                                MessageBox.Show("Güncelleme işlemi başarısız oldu.", "Hata",
                                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        transaction.Rollback();
                                        MessageBox.Show($"Güncelleme sırasında bir hata oluştu: {ex.Message}", "Hata",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                                connection.Close();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Veritabanı işlemi sırasında bir hata oluştu: {ex.Message}", "Hata",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    };
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Form yüklenirken bir hata oluştu: {ex.Message}", "Hata",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };
            borcekleForm.ShowDialog();
        }

       
        // Seçili kaydın işlem geçmişini görüntüle
        public void GoruntuleIslemGecmisi(int hareketId)
        {
            try
            {
                // Form oluştur
                Form gecmisForm = new Form();
                gecmisForm.Text = "İşlem Geçmişi";
                gecmisForm.Size = new Size(990, 600); // Genişletilmiş form
                gecmisForm.StartPosition = FormStartPosition.CenterParent;
                gecmisForm.FormBorderStyle = FormBorderStyle.FixedSingle; // Sabit kenarlık
                gecmisForm.MaximizeBox = false; // Büyütme butonu yok
                gecmisForm.MinimizeBox = false; // Sadece küçültme butonu var
                gecmisForm.ShowInTaskbar = false; // Görev çubuğunda gösterme
                gecmisForm.Font = new Font("Segoe UI", 9, FontStyle.Regular); // Segoe UI font
                gecmisForm.ShowIcon = false; // Bu satır form çerçevesindeki ikonu gizler

                // Başlık paneli oluştur (koyu gri arka plan)
                Panel headerPanel = new Panel();
                headerPanel.Dock = DockStyle.Top;
                headerPanel.Height = 50;
                headerPanel.BackColor = Color.FromArgb(240, 240, 240);
                headerPanel.BorderStyle = BorderStyle.FixedSingle;

                // Başlık etiketi
                Label headerLabel = new Label();
                headerLabel.Text = "İŞLEM GEÇMİŞİ";
                headerLabel.Font = new Font("Segoe UI", 14, FontStyle.Bold);
                headerLabel.ForeColor = Color.FromArgb(64, 64, 64);
                headerLabel.Dock = DockStyle.Fill;
                headerLabel.TextAlign = ContentAlignment.MiddleCenter;
                headerPanel.Controls.Add(headerLabel);

                // ListView oluştur
                ListView listView = new ListView();
                listView.Dock = DockStyle.Fill;
                listView.View = View.Details;
                listView.FullRowSelect = true;
                listView.GridLines = true;
                listView.Font = new Font("Segoe UI", 9, FontStyle.Regular);
                listView.BorderStyle = BorderStyle.FixedSingle;

                // Sütunları ekle
                listView.Columns.Add("İşlem Tarihi", 150);
                listView.Columns.Add("İşlem Türü", 120);
                listView.Columns.Add("Kullanıcı", 100);
                listView.Columns.Add("Değiştirme Nedeni", 200);
                listView.Columns.Add("Eski Değerler", 200);
                listView.Columns.Add("Yeni Değerler", 200);

                // Veritabanından geçmiş kayıtlarını al
                string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
    SELECT islem_tarihi, islem_turu, kullanici, degistirme_nedeni,
           eski_degerler, yeni_degerler, ip_adresi
    FROM islem_gecmisi
    WHERE hareket_id = @HareketId
    ORDER BY islem_tarihi DESC";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@HareketId", hareketId);

                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Tarihi formatla
                                DateTime islemTarihi = reader.GetDateTime(0);
                                string tarihText = islemTarihi.ToString("dd.MM.yyyy HH:mm:ss");

                                // Diğer alanları al
                                string islemTuru = reader.GetString(1);
                                string kullanici = reader.GetString(2);
                                string degistirmeNedeni = reader.GetString(3);
                                string eskiDegerlerJson = reader.GetString(4);
                                string yeniDegerlerJson = reader.GetString(5);
                                string ipAdresi = reader.GetString(6);

                                // JSON verilerini daha okunabilir hale getir
                                string eskiDegerlerOzet = FormatlaJsonDegerleri(eskiDegerlerJson);
                                string yeniDegerlerOzet = FormatlaJsonDegerleri(yeniDegerlerJson);

                                // ListView'e ekle
                                ListViewItem item = new ListViewItem(tarihText);
                                item.SubItems.Add(islemTuru);
                                item.SubItems.Add(kullanici);
                                item.SubItems.Add(degistirmeNedeni);
                                item.SubItems.Add(eskiDegerlerOzet); // Eski değerler
                                item.SubItems.Add(yeniDegerlerOzet); // Yeni değerler

                                // Tüm veriyi Tag olarak sakla (detay gösterimi için)
                                item.Tag = new
                                {
                                    IslemTarihi = islemTarihi,
                                    IslemTuru = islemTuru,
                                    Kullanici = kullanici,
                                    DegistirmeNedeni = degistirmeNedeni,
                                    EskiDegerlerJson = eskiDegerlerJson,
                                    YeniDegerlerJson = yeniDegerlerJson,
                                    IpAdresi = ipAdresi
                                };

                                listView.Items.Add(item);
                            }
                        }
                    }
                }

                // Alt panel oluştur (Kapat butonu için)
                Panel footerPanel = new Panel();
                footerPanel.Dock = DockStyle.Bottom;
                footerPanel.Height = 60;
                footerPanel.BackColor = Color.FromArgb(240, 240, 240);
                footerPanel.BorderStyle = BorderStyle.FixedSingle;

                // Kapat butonu
                Button btnKapat = new Button();
                btnKapat.Text = "Kapat";
                btnKapat.Size = new Size(100, 30);
                btnKapat.Location = new Point(
                    footerPanel.Width - btnKapat.Width - 20,
                    (footerPanel.Height - btnKapat.Height) / 2);
                btnKapat.Anchor = AnchorStyles.Right | AnchorStyles.None;
                btnKapat.BackColor = Color.FromArgb(220, 53, 69); // Kırmızı renk
                btnKapat.ForeColor = Color.White;
                btnKapat.FlatStyle = FlatStyle.Flat;
                btnKapat.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                btnKapat.Click += (s, e) => { gecmisForm.Close(); };
                footerPanel.Controls.Add(btnKapat);

                // Eğer kayıt yoksa bilgi ver
                if (listView.Items.Count == 0)
                {
                    MessageBox.Show("Bu kayıt için düzenleme geçmişi bulunmamaktadır.", "Bilgi",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // ListView çift tıklama olayı - Detaylı görünüm için
                listView.DoubleClick += (sender, e) => {
                    if (listView.SelectedItems.Count > 0)
                    {
                        ListViewItem selectedItem = listView.SelectedItems[0];
                        dynamic itemData = selectedItem.Tag;

                        GosterDetayliDegisiklik(
                            itemData.IslemTarihi,
                            itemData.IslemTuru,
                            itemData.Kullanici,
                            itemData.DegistirmeNedeni,
                            itemData.EskiDegerlerJson,
                            itemData.YeniDegerlerJson,
                            itemData.IpAdresi
                        );

                     }

                };

                // Form yüklendiğinde bilgi baloncuğu göster ve ses çal
                gecmisForm.Load += (s, args) => {
                    // Form tamamen yüklendikten sonra bilgi baloncuğunu göster
                    gecmisForm.BeginInvoke(new Action(() => {
                        // Bilgi baloncuğu için ToolTip oluştur
                        ToolTip infoToolTip = new ToolTip();
                        infoToolTip.IsBalloon = true; // Balon şeklinde göster
                        infoToolTip.ToolTipTitle = "Bilgi";
                        infoToolTip.ToolTipIcon = ToolTipIcon.Info;
                        infoToolTip.UseAnimation = true;
                        infoToolTip.UseFading = true;
                        infoToolTip.AutoPopDelay = 6000; // 10 saniye göster
                        infoToolTip.InitialDelay = 500;
                        infoToolTip.ReshowDelay = 500;

                        // ListView kontrolüne bilgi baloncuğu göster
                        infoToolTip.Show(
                            "İşlem detaylarını görmek için herhangi bir satıra çift tıklayabilirsiniz.",
                            listView,
                            listView.Width / 2, // X pozisyonu
                            listView.Height / 2, // Y pozisyonu
                            60000 // Gösterme süresi (10 saniye)
                        );

                        // Sesli bilgilendirme (Windows sistem sesi)
                        try
                        {
                            // Byte dizisi olarak saklanan ses verilerini MemoryStream'e çevir
                            using (var stream = new System.IO.MemoryStream(Properties.Resources.detaygor))
                            {
                                using (System.Media.SoundPlayer player = new System.Media.SoundPlayer(stream))
                                {
                                    player.Play();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Ses çalınamadı: " + ex.Message);
                            System.Media.SystemSounds.Asterisk.Play();
                        }
                    }));
                };

                // Formu oluştur
                gecmisForm.Controls.Add(listView);
                gecmisForm.Controls.Add(headerPanel);
                gecmisForm.Controls.Add(footerPanel);

                // Formu göster
                gecmisForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"İşlem geçmişi görüntülenirken bir hata oluştu: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Detaylı değişiklik görünümü - Yenilenmiş tasarım
        public void GosterDetayliDegisiklik(DateTime islemTarihi, string islemTuru, string kullanici,
                                            string degistirmeNedeni, string eskiDegerlerJson,
                                            string yeniDegerlerJson, string ipAdresi)
        {
            // Form oluştur
            Form detayForm = new Form();
            detayForm.Text = "Değişiklik Detayları";
            detayForm.Size = new Size(800, 600);
            detayForm.StartPosition = FormStartPosition.CenterParent;
            detayForm.FormBorderStyle = FormBorderStyle.FixedSingle; // Sabit kenarlık
            detayForm.MaximizeBox = false;
            detayForm.MinimizeBox = false;
            detayForm.ShowInTaskbar = false; // Görev çubuğunda gösterme
            detayForm.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            detayForm.ShowIcon = false; // Bu satır form çerçevesindeki ikonu gizler

            // Ana panel
            Panel mainPanel = new Panel();
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.BackColor = Color.White;
            mainPanel.Padding = new Padding(20);
            mainPanel.AutoScroll = true;

            // Başlık paneli
            Panel headerPanel = new Panel();
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Height = 50;
            headerPanel.BackColor = Color.FromArgb(240, 240, 240);
            headerPanel.BorderStyle = BorderStyle.FixedSingle;

            // Başlık etiketi
            Label headerLabel = new Label();
            headerLabel.Text = "DEĞİŞİKLİK DETAYLARI";
            headerLabel.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            headerLabel.ForeColor = Color.FromArgb(64, 64, 64);
            headerLabel.Dock = DockStyle.Fill;
            headerLabel.TextAlign = ContentAlignment.MiddleCenter;
            headerPanel.Controls.Add(headerLabel);

            // Üst panel iyileştirmeleri - headerPanel oluşturulduktan SONRA ekleyin
            Panel topLeftPanel = new Panel();
            topLeftPanel.Dock = DockStyle.Left;
            topLeftPanel.Width = headerPanel.Width / 2 - 100;
            topLeftPanel.Height = 50;
            topLeftPanel.BackColor = Color.FromArgb(240, 240, 240);
            topLeftPanel.BorderStyle = BorderStyle.None;

            // Filtreleme için ComboBox
            Label filterLabel = new Label();
            filterLabel.Text = "İşlem Türü:";
            filterLabel.AutoSize = true;
            filterLabel.Location = new Point(10, 17);
            filterLabel.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            topLeftPanel.Controls.Add(filterLabel);

            ComboBox filterCombo = new ComboBox();
            filterCombo.Items.AddRange(new object[] { "Tüm İşlemler", "Tahsilat Düzenleme", "Borç Düzenleme" });
            filterCombo.SelectedIndex = 0;
            filterCombo.Location = new Point(filterLabel.Right + 5, 15);
            filterCombo.Width = 150;
            filterCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            topLeftPanel.Controls.Add(filterCombo);

            // Sağ üst panel
            Panel topRightPanel = new Panel();
            topRightPanel.Dock = DockStyle.Right;
            topRightPanel.Width = headerPanel.Width / 2 - 100;
            topRightPanel.Height = 50;
            topRightPanel.BackColor = Color.FromArgb(240, 240, 240);
            topRightPanel.BorderStyle = BorderStyle.None;

            // Hızlı butonlar
            Button refreshButton = new Button();
            refreshButton.Text = "Yenile";
            refreshButton.Size = new Size(80, 30);
            refreshButton.Location = new Point(10, 10);
            refreshButton.BackColor = Color.FromArgb(0, 122, 204);
            refreshButton.ForeColor = Color.White;
            refreshButton.FlatStyle = FlatStyle.Flat;
            refreshButton.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            topRightPanel.Controls.Add(refreshButton);

            Button exportButton = new Button();
            exportButton.Text = "Dışa Aktar";
            exportButton.Size = new Size(80, 30);
            exportButton.Location = new Point(refreshButton.Right + 10, 10);
            exportButton.BackColor = Color.FromArgb(0, 150, 136);
            exportButton.ForeColor = Color.White;
            exportButton.FlatStyle = FlatStyle.Flat;
            exportButton.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            topRightPanel.Controls.Add(exportButton);

            // Panelleri header panele ekle
            headerPanel.Controls.Add(topLeftPanel);
            headerPanel.Controls.Add(topRightPanel);

            // İşlem bilgileri grup panel
            GroupBox infoGroup = new GroupBox();
            infoGroup.Text = "İşlem Bilgileri";
            infoGroup.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            infoGroup.Dock = DockStyle.Top;
            infoGroup.Height = 100;
            infoGroup.Padding = new Padding(10);
            infoGroup.Margin = new Padding(0, 10, 0, 10);

            // İşlem bilgileri panel
            TableLayoutPanel infoTable = new TableLayoutPanel();
            infoTable.Dock = DockStyle.Fill;
            infoTable.ColumnCount = 4;
            infoTable.RowCount = 2;
            infoTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            infoTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            infoTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            infoTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));

            // Etiketleri ekle
            AddInfoLabel(infoTable, "İşlem Tarihi:", 0, 0);
            AddInfoLabel(infoTable, islemTarihi.ToString("dd.MM.yyyy HH:mm:ss"), 0, 1, false);

            AddInfoLabel(infoTable, "İşlem Türü:", 0, 2);
            AddInfoLabel(infoTable, islemTuru, 0, 3, false);

            AddInfoLabel(infoTable, "Kullanıcı:", 1, 0);
            AddInfoLabel(infoTable, kullanici, 1, 1, false);

            AddInfoLabel(infoTable, "IP Adresi:", 1, 2);
            AddInfoLabel(infoTable, ipAdresi, 1, 3, false);

            infoGroup.Controls.Add(infoTable);

            // Değiştirme nedeni grup panel
            GroupBox reasonGroup = new GroupBox();
            reasonGroup.Text = "Değiştirme Nedeni";
            reasonGroup.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            reasonGroup.Dock = DockStyle.Top;
            reasonGroup.Height = 90;
            reasonGroup.Padding = new Padding(10);
            reasonGroup.Margin = new Padding(0, 10, 0, 10);
            reasonGroup.Top = infoGroup.Bottom + 10;

            // Değiştirme nedeni metin kutusu
            TextBox txtNeden = new TextBox();
            txtNeden.Text = degistirmeNedeni;
            txtNeden.Multiline = true;
            txtNeden.ReadOnly = true;
            txtNeden.Dock = DockStyle.Fill;
            txtNeden.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            txtNeden.BackColor = Color.White;
            reasonGroup.Controls.Add(txtNeden);

            // Değişiklik tablosu grup panel
            GroupBox changesGroup = new GroupBox();
            changesGroup.Text = "Değişiklikler";
            changesGroup.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            changesGroup.Dock = DockStyle.Top;
            changesGroup.Height = 200;
            changesGroup.Padding = new Padding(10);
            changesGroup.Margin = new Padding(0, 10, 0, 10);
            changesGroup.Top = reasonGroup.Bottom + 10;
            changesGroup.AutoSize = true;

            // Değişiklik tablosu
            TableLayoutPanel changesTable = new TableLayoutPanel();
            changesTable.Dock = DockStyle.Fill;
            changesTable.AutoSize = true;
            changesTable.ColumnCount = 3;
            changesTable.RowCount = 1;
            changesTable.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            changesTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            changesTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            changesTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));

            // Başlık satırı
            AddTableHeader(changesTable, "Alan", 0, 0);
            AddTableHeader(changesTable, "Eski Değer", 0, 1);
            AddTableHeader(changesTable, "Yeni Değer", 0, 2);

            // JSON verilerini parse et ve tabloya ekle
            Dictionary<string, string> eskiDegerler = ParseJsonToDictionary(eskiDegerlerJson);
            Dictionary<string, string> yeniDegerler = ParseJsonToDictionary(yeniDegerlerJson);

            int row = 1;
            foreach (KeyValuePair<string, string> pair in eskiDegerler)
            {
                string key = pair.Key;
                string eskiDeger = pair.Value;
                string yeniDeger = yeniDegerler.ContainsKey(key) ? yeniDegerler[key] : "";

                // Alan adını anlaşılır hale getir
                string displayKey = GetDisplayName(key);

                // Para birimi ekle
                if (key == "borc" || key == "tahsilat")
                {
                    if (!string.IsNullOrEmpty(eskiDeger) && !eskiDeger.Contains("TL"))
                        eskiDeger = "₺" + eskiDeger;

                    if (!string.IsNullOrEmpty(yeniDeger) && !yeniDeger.Contains("TL"))
                        yeniDeger = "₺" + yeniDeger;
                }

                // Tabloya yeni satır ekle
                changesTable.RowCount = row + 1;
                changesTable.RowStyles.Add(new RowStyle(SizeType.AutoSize));

                // Sütunları ekle
                AddTableCell(changesTable, displayKey, row, 0);
                AddTableCell(changesTable, eskiDeger, row, 1);

                // Değişen değeri vurgula
                bool changed = eskiDeger != yeniDeger;
                AddTableCell(changesTable, yeniDeger, row, 2, changed);

                row++;
            }

            changesGroup.Controls.Add(changesTable);

            
            // Alt panel (Kapat butonu için)
            Panel footerPanel = new Panel();
            footerPanel.Dock = DockStyle.Bottom;
            footerPanel.Height = 60;
            footerPanel.BackColor = Color.FromArgb(240, 240, 240);
            footerPanel.BorderStyle = BorderStyle.FixedSingle;



            // Kapat butonu
            Button btnKapat = new Button();
            btnKapat.Text = "Kapat";
            btnKapat.Size = new Size(100, 30);
            btnKapat.Location = new Point(
                footerPanel.Width - btnKapat.Width - 20,
                (footerPanel.Height - btnKapat.Height) / 2);
            btnKapat.Anchor = AnchorStyles.Right | AnchorStyles.None;
            btnKapat.BackColor = Color.FromArgb(220, 53, 69); // Kırmızı renk
            btnKapat.ForeColor = Color.White;
            btnKapat.FlatStyle = FlatStyle.Flat;
            btnKapat.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btnKapat.Click += (s, e) => { detayForm.Close(); };
            footerPanel.Controls.Add(btnKapat);

            // Panelleri ana panele ekle
            mainPanel.Controls.Add(changesGroup);
            mainPanel.Controls.Add(reasonGroup);
            mainPanel.Controls.Add(infoGroup);

            // Panelleri forma ekle
            detayForm.Controls.Add(mainPanel);
            detayForm.Controls.Add(headerPanel);
            detayForm.Controls.Add(footerPanel);

            // Formu göster
            detayForm.ShowDialog();
        }

        // Yardımcı metodlar
        public void AddInfoLabel(TableLayoutPanel table, string text, int row, int col, bool isBold = true)
        {
            Label label = new Label();
            label.Text = text;
            label.Dock = DockStyle.Fill;
            label.TextAlign = ContentAlignment.MiddleLeft;
            label.Margin = new Padding(5);

            if (isBold)
                label.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            else
                label.Font = new Font("Segoe UI", 9, FontStyle.Regular);

            table.Controls.Add(label, col, row);
        }

        public void AddTableHeader(TableLayoutPanel table, string text, int row, int col)
        {
            Label label = new Label();
            label.Text = text;
            label.Dock = DockStyle.Fill;
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            label.BackColor = Color.FromArgb(240, 240, 240);
            label.Margin = new Padding(3);
            table.Controls.Add(label, col, row);
        }

        public void AddTableCell(TableLayoutPanel table, string text, int row, int col, bool highlight = false)
        {
            Label label = new Label();
            label.Text = text;
            label.Dock = DockStyle.Fill;
            label.TextAlign = ContentAlignment.MiddleLeft;
            label.Margin = new Padding(5);

            if (highlight)
            {
                label.ForeColor = Color.Blue;
                label.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            }
            else
            {
                label.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            }

            table.Controls.Add(label, col, row);
        }

        public string GetDisplayName(string key)
        {
            switch (key)
            {
                case "tarih": return "Tarih";
                case "aciklama": return "Açıklama";
                case "borc": return "Borç";
                case "tahsilat": return "Tahsilat";
                case "vadeGun": return "Vade Günü";
                case "tahsilatTuru": return "Tahsilat Türü";
                default: return key;
            }
        }

        // JSON verilerini formatlama ve özetleme
        public string FormatlaJsonDegerleri(string jsonText)
        {
            try
            {
                // JSON formatını temizle
                jsonText = jsonText.Trim('{', '}');

                // Virgülle ayrılmış key-value çiftlerini ayır
                string[] pairs = jsonText.Split(',');

                // Sonuç dizmisi
                List<string> lines = new List<string>();

                foreach (string pair in pairs)
                {
                    // Key-value çiftini ayır
                    string[] keyValue = pair.Split(':');
                    if (keyValue.Length == 2)
                    {
                        string key = keyValue[0].Trim('\"', ' ');
                        string value = keyValue[1].Trim('\"', ' ');

                        // Key'i anlaşılır hale getir
                        string displayKey = GetDisplayName(key);

                        // Para birimi ekle
                        if (key == "borc" || key == "tahsilat")
                        {
                            if (!string.IsNullOrEmpty(value) && !value.Contains("TL"))
                                value = "₺" + value;
                        }

                        // Satırı ekle
                        lines.Add($"{displayKey}: {value}");
                    }
                }

                // Tüm satırları birleştir
                return string.Join(Environment.NewLine, lines);
            }
            catch
            {
                return jsonText; // Hata durumunda orijinal metni döndür
            }
        }

        // JSON'dan Dictionary'e dönüştürme
        public Dictionary<string, string> ParseJsonToDictionary(string jsonText)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            try
            {
                // JSON formatını temizle
                jsonText = jsonText.Trim('{', '}');

                // Boşsa boş dictionary döndür
                if (string.IsNullOrWhiteSpace(jsonText))
                    return result;

                // Virgülle ayrılmış key-value çiftlerini ayır
                string[] pairs = jsonText.Split(',');

                foreach (string pair in pairs)
                {
                    // Key-value çiftini ayır
                    string[] keyValue = pair.Split(':');
                    if (keyValue.Length == 2)
                    {
                        string key = keyValue[0].Trim('\"', ' ');
                        string value = keyValue[1].Trim('\"', ' ');

                        result[key] = value;
                    }
                }
            }
            catch
            {
                // Hata durumunda boş dictionary döndür
            }

            return result;
        }

        // Tahsilat formunu güncelleme için kullanma
        public void GuncelleTahsilatForm(dynamic formData)
        {
            // Temizleme ve eski bağlantıları kapatma işlemleri
            SQLiteConnection.ClearAllPools();
            GC.Collect();
            GC.WaitForPendingFinalizers();

            // Tahsilat formu oluştur
            Tahsilat tahsilatForm = new Tahsilat(formData.CariUnvan);

            // Form başlığını değiştir
            tahsilatForm.Text = "Tahsilat Kaydı Düzenle";

            // Form yüklendiğinde verileri doldur
            tahsilatForm.Load += (s, e) => {
                try
                {
                    // Verileri form alanlarına doldur
                    tahsilatForm.textBox2.Text = formData.Aciklama;
                    tahsilatForm.textBox3.Text = formData.TahsilatTutar.ToString("0.00").Replace(".", ",") + " TL";

                    if (tahsilatForm.odemeyiyapantextbox != null)
                    {
                        tahsilatForm.odemeyiyapantextbox.Text = formData.OdemeyiYapan;
                        tahsilatForm.odemeyiyapantextbox.ReadOnly = true; // Salt okunur yap
                        tahsilatForm.odemeyiyapantextbox.BackColor = Color.FromArgb(240, 240, 240); // Gri arka plan
                    }

                    if (tahsilatForm.tahsilatturusecme != null)
                    {
                        for (int i = 0; i < tahsilatForm.tahsilatturusecme.Items.Count; i++)
                        {
                            if (tahsilatForm.tahsilatturusecme.Items[i].ToString() == formData.TahsilatTuru)
                            {
                                tahsilatForm.tahsilatturusecme.SelectedIndex = i;
                                break;
                            }
                        }
                    }

                    tahsilatForm.dateTimePicker1.Value = formData.Tarih;
                    tahsilatForm.dateTimePicker2.Value = formData.Tarih;

                    // Kaydet butonunu Güncelle olarak değiştir
                    tahsilatForm.kaydet.Text = "Güncelle";

                    // Eski click olayını kaldır, yeni click olayı ekle
                    tahsilatForm.kaydet.Click -= tahsilatForm.kaydet_Click;
                    tahsilatForm.kaydet.Click += (sender, args) => {
                        // Değiştirme nedeni sor
                        string degistirmeNedeni = "";
                        using (var nedenForm = new Form())
                        {
                            nedenForm.Text = "Değiştirme Nedeni";
                            nedenForm.Size = new Size(400, 200);
                            nedenForm.StartPosition = FormStartPosition.CenterParent;
                            nedenForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                            nedenForm.MaximizeBox = false;
                            nedenForm.MinimizeBox = false;

                            Label lblNeden = new Label
                            {
                                Text = "Lütfen bu değişikliğin nedenini yazınız:",
                                Location = new Point(20, 20),
                                Size = new Size(350, 20)
                            };

                            TextBox txtNeden = new TextBox
                            {
                                Location = new Point(20, 50),
                                Size = new Size(350, 60),
                                Multiline = true
                            };

                            Button btnTamam = new Button
                            {
                                Text = "Tamam",
                                DialogResult = DialogResult.OK,
                                Location = new Point(210, 120),
                                Size = new Size(75, 30)
                            };

                            Button btnIptal = new Button
                            {
                                Text = "İptal",
                                DialogResult = DialogResult.Cancel,
                                Location = new Point(295, 120),
                                Size = new Size(75, 30)
                            };

                            nedenForm.Controls.AddRange(new Control[] { lblNeden, txtNeden, btnTamam, btnIptal });
                            nedenForm.AcceptButton = btnTamam;
                            nedenForm.CancelButton = btnIptal;

                            if (nedenForm.ShowDialog() == DialogResult.OK)
                            {
                                degistirmeNedeni = txtNeden.Text.Trim();
                                if (string.IsNullOrWhiteSpace(degistirmeNedeni))
                                {
                                    MessageBox.Show("Lütfen değiştirme nedenini belirtiniz.", "Uyarı",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }
                            else
                            {
                                return; // İptal edildi
                            }
                        }

                        // Form verilerini al
                        string aciklama = tahsilatForm.textBox2.Text;
                        string odemeyiYapan = tahsilatForm.odemeyiyapantextbox?.Text?.Trim() ?? "";
                        string tahsilatTuru = tahsilatForm.tahsilatturusecme?.SelectedItem?.ToString() ?? "";

                        // Tutar kontrolü
                        string tutarText = tahsilatForm.textBox3.Text.Replace(" TL", "").Replace(",", ".");
                        if (!decimal.TryParse(tutarText, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal tahsilatTutar) || tahsilatTutar <= 0)
                        {
                            MessageBox.Show("Lütfen geçerli bir tahsilat tutarı giriniz.", "Hata",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Tarih bilgilerini al
                        DateTime secilenTarih = tahsilatForm.dateTimePicker1.Value;
                        DateTime secilenSaat = tahsilatForm.dateTimePicker2.Value;

                        // Tarih ve saati birleştir
                        DateTime birlesikTarihSaat = new DateTime(
                            secilenTarih.Year, secilenTarih.Month, secilenTarih.Day,
                            secilenSaat.Hour, secilenSaat.Minute, secilenSaat.Second);
                        string formattedDate = birlesikTarihSaat.ToString("yyyy-MM-dd HH:mm:ss");

                        // Değişiklik var mı kontrol et
                        bool degisiklikVar =
                            aciklama != formData.Aciklama ||
                            tahsilatTutar != formData.TahsilatTutar ||
                            tahsilatTuru != formData.TahsilatTuru ||
                            birlesikTarihSaat != formData.Tarih;

                        if (!degisiklikVar)
                        {
                            MessageBox.Show("Herhangi bir değişiklik yapmadınız.", "Bilgi",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        try
                        {
                            // Veritabanı bağlantısını açıkça kapat ve havuzu temizle
                            SQLiteConnection.ClearAllPools();
                            GC.Collect();
                            GC.WaitForPendingFinalizers();

                            // Veritabanı güncelleme işlemi
                            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;" +
                                                    "Journal Mode=WAL;Synchronous=NORMAL;";

                            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                            {
                                connection.Open();

                                // Veritabanı ayarları
                                using (SQLiteCommand pragmaCommand = new SQLiteCommand(connection))
                                {
                                    pragmaCommand.CommandText = "PRAGMA journal_mode = WAL;";
                                    pragmaCommand.ExecuteNonQuery();
                                    pragmaCommand.CommandText = "PRAGMA synchronous = NORMAL;";
                                    pragmaCommand.ExecuteNonQuery();
                                    pragmaCommand.CommandText = "PRAGMA busy_timeout = 30000;";
                                    pragmaCommand.ExecuteNonQuery();
                                }

                                // Transaction başlat
                                using (SQLiteTransaction transaction = connection.BeginTransaction())
                                {
                                    try
                                    {
                                        // Eski tahsilat tutarını al
                                        decimal eskiTahsilatTutar = formData.TahsilatTutar;

                                        // İşlem geçmişi için eski ve yeni değerleri JSON olarak oluştur
                                        string eskiDegerler = $"{{\"tarih\":\"{formData.Tarih.ToString("yyyy-MM-dd HH:mm:ss")}\",\"aciklama\":\"{formData.Aciklama}\",\"tahsilat\":{eskiTahsilatTutar},\"tahsilatTuru\":\"{formData.TahsilatTuru}\"}}";
                                        string yeniDegerler = $"{{\"tarih\":\"{formattedDate}\",\"aciklama\":\"{aciklama}\",\"tahsilat\":{tahsilatTutar},\"tahsilatTuru\":\"{tahsilatTuru}\"}}";

                                        // Tahsilat güncelleme
                                        string updateQuery = @"
                                    UPDATE cari_hareketleri
                                    SET tarih = @Tarih,
                                        aciklama = @Aciklama,
                                        tahsilat = @Tahsilat,
                                        tahsilat_turu = @TahsilatTuru
                                    WHERE id = @Id";

                                        using (SQLiteCommand command = new SQLiteCommand(updateQuery, connection, transaction))
                                        {
                                            command.Parameters.AddWithValue("@Id", formData.HareketId);
                                            command.Parameters.AddWithValue("@Tarih", formattedDate);
                                            command.Parameters.AddWithValue("@Aciklama", aciklama);
                                            command.Parameters.AddWithValue("@Tahsilat", tahsilatTutar);
                                            command.Parameters.AddWithValue("@TahsilatTuru", tahsilatTuru);

                                            int rowsAffected = command.ExecuteNonQuery();

                                            if (rowsAffected > 0)
                                            {
                                                // Bakiye farkını hesapla
                                                decimal bakiyeFarki = tahsilatTutar - eskiTahsilatTutar;

                                                if (bakiyeFarki != 0)
                                                {
                                                    // Bakiyeyi güncelle
                                                    string bakiyeQuery = @"
                                                UPDATE Cari
                                                SET Bakiye = Bakiye - @BakiyeFarki
                                                WHERE CariKodu = @CariKodu";

                                                    using (SQLiteCommand bakiyeCommand = new SQLiteCommand(bakiyeQuery, connection, transaction))
                                                    {
                                                        bakiyeCommand.Parameters.AddWithValue("@BakiyeFarki", bakiyeFarki);
                                                        bakiyeCommand.Parameters.AddWithValue("@CariKodu", formData.CariKodu);
                                                        bakiyeCommand.ExecuteNonQuery();
                                                    }
                                                }

                                                // İşlem geçmişine kaydet
                                                // İşlem geçmişine kaydet
                                                string logQuery = @"
    INSERT INTO islem_gecmisi
    (islem_turu, hareket_id, kullanici, eski_degerler, yeni_degerler, degistirme_nedeni, ip_adresi)
    VALUES
    (@IslemTuru, @HareketId, @Kullanici, @EskiDegerler, @YeniDegerler, @DegistirmeNedeni, @IpAdresi)";
                                                using (SQLiteCommand logCommand = new SQLiteCommand(logQuery, connection, transaction))
                                                {
                                                    logCommand.Parameters.AddWithValue("@IslemTuru", "Borç Düzenleme");
                                                    logCommand.Parameters.AddWithValue("@HareketId", formData.HareketId);
                                                    logCommand.Parameters.AddWithValue("@IslemTarihi", DateTime.Now); // Şuanki tarih
                                                    logCommand.Parameters.AddWithValue("@Kullanici", Environment.UserName);
                                                    logCommand.Parameters.AddWithValue("@EskiDegerler", eskiDegerler);
                                                    logCommand.Parameters.AddWithValue("@YeniDegerler", yeniDegerler);
                                                    logCommand.Parameters.AddWithValue("@DegistirmeNedeni", degistirmeNedeni);
                                                    logCommand.Parameters.AddWithValue("@IpAdresi", GetLocalIPAddress());
                                                    logCommand.ExecuteNonQuery();
                                                }

                                                transaction.Commit();

                                                MessageBox.Show("Tahsilat kaydı başarıyla güncellendi.", "Bilgi",
                                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                                                tahsilatForm.Close();

                                                // DataGridView'i yenile
                                                LoadCariHareketleri(formData.CariKodu);
                                            }
                                            else
                                            {
                                                transaction.Rollback();
                                                MessageBox.Show("Güncelleme işlemi başarısız oldu.", "Hata",
                                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        transaction.Rollback();
                                        MessageBox.Show($"Güncelleme sırasında bir hata oluştu: {ex.Message}", "Hata",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                                connection.Close();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Veritabanı işlemi sırasında bir hata oluştu: {ex.Message}", "Hata",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    };
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Form yüklenirken bir hata oluştu: {ex.Message}", "Hata",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            tahsilatForm.ShowDialog();
        }

        // Cari kodu ile unvan getir
        public string GetCariUnvanFromKodu(string cariKodu)
{
    string cariUnvan = "";
    string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
    using (SQLiteConnection connection = new SQLiteConnection(connectionString))
    {
        connection.Open();
        string query = "SELECT Unvani FROM Cari WHERE CariKodu = @CariKodu";
        using (SQLiteCommand command = new SQLiteCommand(query, connection))
        {
            command.Parameters.AddWithValue("@CariKodu", cariKodu);
            object result = command.ExecuteScalar();
            if (result != null)
            {
                cariUnvan = result.ToString();
            }
        }
    }
    return cariUnvan;
}

        // Borç güncelleme işlemi
        public void BorcGuncelle_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null) return;

            Borcekle form = btn.FindForm() as Borcekle;
            if (form == null) return;

            int hareketId = Convert.ToInt32(form.Tag);

            // Borcu ekleyen kontrolü - boş bırakılamaz
            if (string.IsNullOrWhiteSpace(form.borcuekleyentextbox.Text))
            {
                MessageBox.Show("Borcu ekleyen kişi bilgisi boş bırakılamaz.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                form.borcuekleyentextbox.Focus();
                return;
            }

            // Vade gün kontrolü - boş bırakılamaz
            if (string.IsNullOrWhiteSpace(form.vadegungir.Text))
            {
                MessageBox.Show("Vade gün bilgisi boş bırakılamaz.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                form.vadegungir.Focus();
                return;
            }

            // Vade gün sayısal kontrol
            if (!int.TryParse(form.vadegungir.Text, out int vadeGun))
            {
                MessageBox.Show("Vade gün bilgisi geçerli bir sayı olmalıdır.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                form.vadegungir.Focus();
                return;
            }

            string cariUnvani = form.textBox1.Text;
            string cariKodu = GetCariKoduFromUnvani(cariUnvani);
            string aciklama = form.textBox2.Text;
            string borcuEkleyen = form.borcuekleyentextbox.Text.Trim();

            string tutarText = form.textBox3.Text.Replace(" TL", "").Replace(",", ".");
            if (decimal.TryParse(tutarText, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal borcTutar) && borcTutar > 0)
            {
                try
                {
                    DateTime secilenTarih = form.dateTimePicker1.Value;
                    DateTime secilenSaat = form.dateTimePicker2.Value;

                    // Tarih ve saati birleştir
                    DateTime birlesikTarihSaat = new DateTime(
                        secilenTarih.Year,
                        secilenTarih.Month,
                        secilenTarih.Day,
                        secilenSaat.Hour,
                        secilenSaat.Minute,
                        secilenSaat.Second);

                    string formattedDate = birlesikTarihSaat.ToString("yyyy-MM-dd HH:mm:ss");

                    string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
                    using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                    {
                        connection.Open();

                        // Önce eski bakiyeyi geri al
                        decimal eskiBorcTutar = 0;
                        string getBorcQuery = "SELECT borc FROM cari_hareketleri WHERE id = @Id";
                        using (SQLiteCommand getBorcCommand = new SQLiteCommand(getBorcQuery, connection))
                        {
                            getBorcCommand.Parameters.AddWithValue("@Id", hareketId);
                            object result = getBorcCommand.ExecuteScalar();
                            if (result != null && result != DBNull.Value)
                            {
                                eskiBorcTutar = Convert.ToDecimal(result);
                            }
                        }

                        // Limit kontrolünü eskiBorcTutar'ı düşerek yap
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

                        // Düzeltilmiş bakiye (eski borç düşülmüş hali)
                        decimal duzeltilmisBakiye = mevcutBakiye - eskiBorcTutar;

                        // Kalan limit hesabı
                        decimal kalanLimit = cariLimit - duzeltilmisBakiye;

                        // Yeni borç tutarı limiti aşıyor mu kontrolü
                        if (borcTutar > kalanLimit)
                        {
                            MessageBox.Show($"Yeni borç tutarı ({borcTutar}) kalan limiti ({kalanLimit}) aşıyor.",
                                "Limit Aşımı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        // Borç kaydını güncelle
                        string updateQuery = @"
                    UPDATE cari_hareketleri 
                    SET tarih = @Tarih, 
                        aciklama = @Aciklama, 
                        borc = @Borc, 
                        borcu_ekleyen = @BorcuEkleyen, 
                        vade_gun = @VadeGun
                    WHERE id = @Id";

                        using (SQLiteCommand command = new SQLiteCommand(updateQuery, connection))
                        {
                            command.Parameters.AddWithValue("@Id", hareketId);
                            command.Parameters.AddWithValue("@Tarih", formattedDate);
                            command.Parameters.AddWithValue("@Aciklama", aciklama);
                            command.Parameters.AddWithValue("@Borc", borcTutar);
                            command.Parameters.AddWithValue("@BorcuEkleyen", borcuEkleyen);
                            command.Parameters.AddWithValue("@VadeGun", vadeGun);
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                // Bakiyeyi güncelle
                                FormCariEkle formCariEkle = new FormCariEkle();
                                formCariEkle.GuncelleBakiye(cariKodu);

                                MessageBox.Show("Borç kaydı başarıyla güncellendi.", "Bilgi",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // DataGridView'i yenile
                                LoadCariHareketleri(cariKodu);

                                // Formu kapat
                                form.Close();
                            }
                            else
                            {
                                MessageBox.Show("Borç kaydı güncellenemedi.", "Hata",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Güncelleme sırasında bir hata oluştu: {ex.Message}", "Hata",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Lütfen geçerli bir borç tutarı giriniz.", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Tahsilat güncelleme işlemi
        // TahsilatGuncelle_Click veya BorcGuncelle_Click metodunu şu şekilde düzenleyelim
        public void TahsilatGuncelle_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null) return;

            Tahsilat form = btn.FindForm() as Tahsilat;
            if (form == null) return;

            int hareketId = Convert.ToInt32(form.Tag);

            // Validasyon kodları...

            string cariUnvani = form.textBox1.Text;
            string cariKodu = GetCariKoduFromUnvani(cariUnvani);
            string aciklama = form.textBox2.Text;
            string odemeyiYapan = form.odemeyiyapantextbox.Text.Trim();
            string tahsilatTuru = form.tahsilatturusecme.SelectedItem.ToString();

            string tutarText = form.textBox3.Text.Replace(" TL", "").Replace(",", ".");
            if (decimal.TryParse(tutarText, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal tahsilatTutar) && tahsilatTutar > 0)
            {
                try
                {
                    DateTime secilenTarih = form.dateTimePicker1.Value;
                    DateTime secilenSaat = form.dateTimePicker2.Value;

                    // Tarih ve saati birleştir
                    DateTime birlesikTarihSaat = new DateTime(
                        secilenTarih.Year,
                        secilenTarih.Month,
                        secilenTarih.Day,
                        secilenSaat.Hour,
                        secilenSaat.Minute,
                        secilenSaat.Second);

                    string formattedDate = birlesikTarihSaat.ToString("yyyy-MM-dd HH:mm:ss");

                    string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";

                    // Bağlantıyı açık tutma süresini artır ve Lock_Timeout ayarla
                    connectionString += "Pooling=True;Max Pool Size=100;Default Timeout=30;";

                    using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                    {
                        connection.Open();

                        // Command Timeout değerini artır
                        using (SQLiteCommand pragmaCommand = new SQLiteCommand("PRAGMA busy_timeout = 5000;", connection))
                        {
                            pragmaCommand.ExecuteNonQuery();
                        }

                        // İşlemleri bir transaction içine al
                        using (SQLiteTransaction transaction = connection.BeginTransaction())
                        {
                            try
                            {
                                // Önce eski tahsilat tutarını al
                                decimal eskiTahsilatTutar = 0;
                                string getTahsilatQuery = "SELECT tahsilat FROM cari_hareketleri WHERE id = @Id";
                                using (SQLiteCommand getTahsilatCommand = new SQLiteCommand(getTahsilatQuery, connection, transaction))
                                {
                                    getTahsilatCommand.Parameters.AddWithValue("@Id", hareketId);
                                    object result = getTahsilatCommand.ExecuteScalar();
                                    if (result != null && result != DBNull.Value)
                                    {
                                        eskiTahsilatTutar = Convert.ToDecimal(result);
                                    }
                                }

                                // Tahsilat kaydını güncelle
                                string updateQuery = @"
                            UPDATE cari_hareketleri 
                            SET tarih = @Tarih, 
                                aciklama = @Aciklama, 
                                tahsilat = @Tahsilat, 
                                odemeyi_yapan = @OdemeyiYapan, 
                                tahsilat_turu = @TahsilatTuru
                            WHERE id = @Id";

                                int rowsAffected = 0;
                                using (SQLiteCommand command = new SQLiteCommand(updateQuery, connection, transaction))
                                {
                                    command.Parameters.AddWithValue("@Id", hareketId);
                                    command.Parameters.AddWithValue("@Tarih", formattedDate);
                                    command.Parameters.AddWithValue("@Aciklama", aciklama);
                                    command.Parameters.AddWithValue("@Tahsilat", tahsilatTutar);
                                    command.Parameters.AddWithValue("@OdemeyiYapan", odemeyiYapan);
                                    command.Parameters.AddWithValue("@TahsilatTuru", tahsilatTuru);
                                    command.CommandTimeout = 60; // Timeout süresini artır
                                    rowsAffected = command.ExecuteNonQuery();
                                }

                                // Cari bakiyeyi güncelle - aynı transaction içinde
                                if (rowsAffected > 0)
                                {
                                    // Bakiye farkını hesapla
                                    decimal bakiyeFarki = tahsilatTutar - eskiTahsilatTutar;

                                    string updateBakiyeQuery = @"
                                UPDATE Cari
                                SET Bakiye = Bakiye - @BakiyeFarki
                                WHERE CariKodu = @CariKodu";

                                    using (SQLiteCommand updateBakiyeCommand = new SQLiteCommand(updateBakiyeQuery, connection, transaction))
                                    {
                                        updateBakiyeCommand.Parameters.AddWithValue("@BakiyeFarki", bakiyeFarki);
                                        updateBakiyeCommand.Parameters.AddWithValue("@CariKodu", cariKodu);
                                        updateBakiyeCommand.CommandTimeout = 60; // Timeout süresini artır
                                        updateBakiyeCommand.ExecuteNonQuery();
                                    }
                                }

                                // İşlem başarılıysa transaction'ı tamamla
                                transaction.Commit();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Tahsilat kaydı başarıyla güncellendi.", "Bilgi",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    // DataGridView'i yenile - form kapatıldıktan sonra
                                    form.DialogResult = DialogResult.OK;
                                    form.Close();

                                    // Burada direkt çağırmak yerine formun kapanmasından sonra yükle
                                    LoadCariHareketleri(cariKodu);
                                }
                                else
                                {
                                    MessageBox.Show("Tahsilat kaydı güncellenemedi. Kayıt bulunamadı.", "Hata",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            catch (Exception innerEx)
                            {
                                // Hata durumunda transaction'ı geri al
                                transaction.Rollback();
                                throw innerEx;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Güncelleme sırasında bir hata oluştu: {ex.Message}", "Hata",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Lütfen geçerli bir tahsilat tutarı giriniz.", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Carihareketler.cs içindeki GetCariKoduFromUnvani metodunu kullan
        public string GetCariKoduFromUnvani(string unvani)
        {
            string cariKodu = "";
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

        // IP adresi alma yardımcı metodu
        public string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return "127.0.0.1";
        }

        // Bir kaydın düzenlenip düzenlenmediğini kontrol eden fonksiyon
        public bool KayitDuzenlenmisMi(int hareketId)
        {
            try
            {
                string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM islem_gecmisi WHERE hareket_id = @HareketId";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@HareketId", hareketId);
                        int count = Convert.ToInt32(command.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch (Exception)
            {
                return false; // Hata durumunda düzenlenmemiş olarak kabul et
            }
        }

        public void oncekicari_Click(object sender, EventArgs e)
        {
            // Cari kodu listesi boşsa yükleyelim
            if (cariKoduListesi == null || cariKoduListesi.Count == 0)
            {
                cariKoduListesi = new List<string>();
                LoadCariKoduListesi();
                if (cariKoduListesi.Count == 0)
                {
                    ShowModernInfoMessage(
                        "Firma Listesi Boş!",
                        "Sisteminizde kayıtlı firma bulunamadı. Lütfen önce firma ekleyin.",
                        "Firma Bilgi"
                    );
                    return;
                }
            }

            int currentIndex = cariKoduListesi.IndexOf(currentCariKodu);
            if (currentIndex > 0) // İlk kayıt değilse
            {
                currentCariKodu = cariKoduListesi[currentIndex - 1];
                LoadCariHareketleri(currentCariKodu);
                UpdateFirmaAdi();
                CariNotDurumunuKontrolEt(currentCariKodu); // not varmı sorguluyoruz
                                                           // Cari değişti, emanetleri kontrol et
                EmanetDurumunuKontrolEt(currentCariKodu);
            }
            else
            {
                ShowModernInfoMessage(
                    "İlk Firma",
                    "Bu, ilk firmadır. Lütfen ileri butonunu kullanın !",
                    "Firma Bilgisi"
                );
            }
        }

        public void sonrakicari_Click(object sender, EventArgs e)
        {
            // Cari kodu listesi boşsa yükleyelim
            if (cariKoduListesi == null || cariKoduListesi.Count == 0)
            {
                cariKoduListesi = new List<string>();
                LoadCariKoduListesi();
                if (cariKoduListesi.Count == 0)
                {
                    ShowModernInfoMessage(
                        "Firma Listesi Boş!",
                        "Sisteminizde kayıtlı firma bulunamadı. Lütfen önce firma ekleyin.",
                        "Firma Bilgisi"
                    );
                    return;
                }
            }

            int currentIndex = cariKoduListesi.IndexOf(currentCariKodu);
            if (currentIndex < cariKoduListesi.Count - 1) // Son kayıt değilse
            {
                currentCariKodu = cariKoduListesi[currentIndex + 1];
                LoadCariHareketleri(currentCariKodu);
                UpdateFirmaAdi();
                CariNotDurumunuKontrolEt(currentCariKodu); // not varmı sorguluyoruz
                                                           // Cari değişti, emanetleri kontrol et
                EmanetDurumunuKontrolEt(currentCariKodu);
            }
            else
            {
                ShowModernInfoMessage(
                    "Son Firma",
                    "Bu, son firmadır. Kayıtlarımızda başka bir firma bulunmuyor.",
                    "Firma Bilgisi"
                );
            }
        }

        // Modern Info MessageBox metodu - MidnightBlue versiyonu
        public void ShowModernInfoMessage(string title, string message, string caption)
        {
            Form modernMessageBox = new Form
            {
                FormBorderStyle = FormBorderStyle.None,
                BackColor = Color.White,
                Size = new Size(420, 200),
                StartPosition = FormStartPosition.CenterParent,
                TopMost = true
            };

            // Yuvarlatılmış köşeler
            modernMessageBox.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, 420, 200, 15, 15));

            // Header panel (MidnightBlue)
            Panel headerPanel = new Panel
            {
                BackColor = Color.MidnightBlue,
                Dock = DockStyle.Top,
                Height = 60
            };

            // İkon (Bilgi)
            Label iconLabel = new Label
            {
                Text = "ℹ",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(20, 15),
                Size = new Size(40, 40),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Başlık
            Label titleLabel = new Label
            {
                Text = caption,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(70, 20),
                Size = new Size(320, 30),
                TextAlign = ContentAlignment.MiddleLeft
            };

            // Ana mesaj
            Label messageLabel = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(60, 60, 60),
                Location = new Point(30, 80),
                Size = new Size(360, 30),
                TextAlign = ContentAlignment.MiddleLeft
            };

            // Alt mesaj
            Label subMessageLabel = new Label
            {
                Text = message,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(120, 120, 120),
                Location = new Point(30, 110),
                Size = new Size(360, 35),
                TextAlign = ContentAlignment.MiddleLeft
            };

            // Tamam butonu (MidnightBlue)
            Guna.UI2.WinForms.Guna2Button okButton = new Guna.UI2.WinForms.Guna2Button
            {
                Text = "Tamam",
                Size = new Size(100, 35),
                Location = new Point(300, 150),
                FillColor = Color.MidnightBlue,
                ForeColor = Color.White,
                BorderRadius = 8,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };

            okButton.Click += (s, e) => modernMessageBox.Close();

            // Kontrolleri ekle
            headerPanel.Controls.Add(iconLabel);
            headerPanel.Controls.Add(titleLabel);
            modernMessageBox.Controls.Add(headerPanel);
            modernMessageBox.Controls.Add(messageLabel);
            modernMessageBox.Controls.Add(subMessageLabel);
            modernMessageBox.Controls.Add(okButton);

            // Kapatma olayları
            modernMessageBox.KeyDown += (s, e) => { if (e.KeyCode == Keys.Escape) modernMessageBox.Close(); };
            modernMessageBox.KeyPreview = true;

            // Hover efekti (MidnightBlue tonları)
            okButton.MouseEnter += (s, e) => okButton.FillColor = Color.FromArgb(72, 61, 139); // Daha açık MidnightBlue
            okButton.MouseLeave += (s, e) => okButton.FillColor = Color.MidnightBlue;

            // Fade-in animasyonu
            modernMessageBox.Opacity = 0;
            Timer fadeTimer = new Timer { Interval = 30 };
            fadeTimer.Tick += (s, e) => {
                modernMessageBox.Opacity += 0.1;
                if (modernMessageBox.Opacity >= 1.0)
                {
                    fadeTimer.Stop();
                    fadeTimer.Dispose();
                }
            };
            fadeTimer.Start();

            // Göster
            modernMessageBox.ShowDialog(this);
        }


        public void LoadCariKoduListesi()
        {
            RefreshAlarmStatuses();
            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT CariKodu FROM Cari ORDER BY CariKodu ASC";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cariKoduListesi.Add(reader["CariKodu"].ToString());
                        }
                    }
                }
            }

            if (cariKoduListesi.Count > 0)
            {
                currentCariKodu = cariKoduListesi[0];
                LoadCariHareketleri(currentCariKodu);
            }
            else
            {
                MessageBox.Show("Cari listesi boş. Lütfen önce cari ekleyin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void button3_Click_1(object sender, EventArgs e)
        {
            // currentCariKodu'nun mevcut olduğundan emin ol
            if (!string.IsNullOrEmpty(currentCariKodu))
            {
                // FormCariEkle formunu düzenleme modunda aç
                FormCariEkle cariEkleFormu = new FormCariEkle(currentCariKodu); // Cari kodunu geçerek düzenleme modunda aç
                if (cariEkleFormu.ShowDialog() == DialogResult.OK) // FormCariEkle'de işlemin tamamlanıp tamamlanmadığını kontrol eder
                {
                    // Düzenleme sonrası firma adını güncelle
                    UpdateFirmaAdi();

                    // Form4 örneğini bul ve LoadCariList metodunu çağırarak DataGridView'i güncelle
                    foreach (Form openForm in Application.OpenForms)
                    {
                        if (openForm is Form4 form4)
                        {
                            form4.LoadCariList();
                            break;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen düzenlemek için geçerli bir cariyi seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }




        public void analiz_Click(object sender, EventArgs e)
        {
            string unvani = button3.Text.Trim();
            if (string.IsNullOrEmpty(unvani))
            {
                ShowCustomErrorMessage(
                    "Cari Ünvan Bilgisi Alınamadı!",
                    "Lütfen geçerli bir cari seçtiğinizden emin olun."
                );
                return;
            }

            string cariKodu = GetCariKoduByUnvan(unvani);
            if (string.IsNullOrEmpty(cariKodu))
            {
                ShowCustomErrorMessage(
                    "Cari Kod Bulunamadı!",
                    $"Seçilen firma ({unvani}) için cari kod bulunamadı!"
                );
                return;
            }

            FormIstatistik istatistikFormu = new FormIstatistik(cariKodu);
            istatistikFormu.ShowDialog();
        }

        // Özel hata mesajı metodu (sadece bu metod için)
        public void ShowCustomErrorMessage(string title, string message)
        {
            Form modernMessageBox = new Form
            {
                FormBorderStyle = FormBorderStyle.None,
                BackColor = Color.White,
                Size = new Size(420, 200),
                StartPosition = FormStartPosition.CenterParent,
                TopMost = true
            };

            // Yuvarlatılmış köşeler
            modernMessageBox.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, 420, 200, 15, 15));

            // Header panel (Kırmızı - hata için)
            Panel headerPanel = new Panel
            {
                BackColor = Color.FromArgb(220, 53, 69),
                Dock = DockStyle.Top,
                Height = 60
            };

            // İkon (Hata)
            Label iconLabel = new Label
            {
                Text = "✖",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(20, 15),
                Size = new Size(40, 40),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Başlık
            Label titleLabel = new Label
            {
                Text = "Hata",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(70, 20),
                Size = new Size(320, 30),
                TextAlign = ContentAlignment.MiddleLeft
            };

            // Ana mesaj
            Label messageLabel = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(60, 60, 60),
                Location = new Point(30, 80),
                Size = new Size(360, 30),
                TextAlign = ContentAlignment.MiddleLeft
            };

            // Alt mesaj
            Label subMessageLabel = new Label
            {
                Text = message,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(120, 120, 120),
                Location = new Point(30, 110),
                Size = new Size(360, 35),
                TextAlign = ContentAlignment.MiddleLeft
            };

            // Tamam butonu
            Guna.UI2.WinForms.Guna2Button okButton = new Guna.UI2.WinForms.Guna2Button
            {
                Text = "Tamam",
                Size = new Size(100, 35),
                Location = new Point(300, 150),
                FillColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                BorderRadius = 8,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };

            okButton.Click += (s, e) => modernMessageBox.Close();

            // Kontrolleri ekle
            headerPanel.Controls.Add(iconLabel);
            headerPanel.Controls.Add(titleLabel);
            modernMessageBox.Controls.Add(headerPanel);
            modernMessageBox.Controls.Add(messageLabel);
            modernMessageBox.Controls.Add(subMessageLabel);
            modernMessageBox.Controls.Add(okButton);

            // Kapatma olayları
            modernMessageBox.KeyDown += (s, e) => { if (e.KeyCode == Keys.Escape) modernMessageBox.Close(); };
            modernMessageBox.KeyPreview = true;

            // Hover efekti
            okButton.MouseEnter += (s, e) => okButton.FillColor = Color.FromArgb(200, 35, 51);
            okButton.MouseLeave += (s, e) => okButton.FillColor = Color.FromArgb(220, 53, 69);

            // Fade-in animasyonu
            modernMessageBox.Opacity = 0;
            Timer fadeTimer = new Timer { Interval = 30 };
            fadeTimer.Tick += (s, e) => {
                modernMessageBox.Opacity += 0.1;
                if (modernMessageBox.Opacity >= 1.0)
                {
                    fadeTimer.Stop();
                    fadeTimer.Dispose();
                }
            };
            fadeTimer.Start();

            // Göster
            modernMessageBox.ShowDialog(this);
        }

        public void Sorgulabtn_Click(object sender, EventArgs e)
        {
            // Guna2 bileşenleriyle sorgulama formunu aç
            using (var sorgulamaForm = new Form())
            {
                // Form ayarları
                sorgulamaForm.Text = "Cari Hareket Sorgulama";
                sorgulamaForm.Size = new Size(500, 550);
                sorgulamaForm.StartPosition = FormStartPosition.CenterParent;
                sorgulamaForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                sorgulamaForm.MaximizeBox = false;
                sorgulamaForm.MinimizeBox = false;
                sorgulamaForm.BackColor = Color.White;

                // Başlık
                Label lblTitle = new Label
                {
                    Text = "Cari Hareket Sorgulama Kriterleri",
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    ForeColor = Color.FromArgb(94, 148, 255),
                    Location = new Point(20, 20),
                    AutoSize = true
                };

                // Tarih aralığı bölümü
                Label lblTarihAralik = new Label
                {
                    Text = "Tarih Aralığı",
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    ForeColor = Color.FromArgb(64, 64, 64),
                    Location = new Point(20, 60),
                    AutoSize = true
                };

                Label lblBaslangic = new Label
                {
                    Text = "Başlangıç:",
                    Font = new Font("Segoe UI", 9),
                    ForeColor = Color.FromArgb(64, 64, 64),
                    Location = new Point(40, 90),
                    AutoSize = true
                };

                Label lblBitis = new Label
                {
                    Text = "Bitiş:",
                    Font = new Font("Segoe UI", 9),
                    ForeColor = Color.FromArgb(64, 64, 64),
                    Location = new Point(40, 120),
                    AutoSize = true
                };

                // Guna2 DateTimePicker'lar
                Guna.UI2.WinForms.Guna2DateTimePicker dtpBaslangic = new Guna.UI2.WinForms.Guna2DateTimePicker
                {
                    Location = new Point(130, 90),
                    Size = new Size(200, 36),
                    Format = DateTimePickerFormat.Short,
                    Value = DateTime.Now.AddMonths(-1),
                    FillColor = Color.White,
                    BorderColor = Color.FromArgb(94, 148, 255)
                };

                Guna.UI2.WinForms.Guna2DateTimePicker dtpBitis = new Guna.UI2.WinForms.Guna2DateTimePicker
                {
                    Location = new Point(130, 120),
                    Size = new Size(200, 36),
                    Format = DateTimePickerFormat.Short,
                    Value = DateTime.Now,
                    FillColor = Color.White,
                    BorderColor = Color.FromArgb(94, 148, 255)
                };

                // İşlem türü bölümü
                Label lblIslemTuru = new Label
                {
                    Text = "İşlem Türü",
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    ForeColor = Color.FromArgb(64, 64, 64),
                    Location = new Point(20, 170),
                    AutoSize = true
                };

                // Guna2 ComboBox
                Guna.UI2.WinForms.Guna2ComboBox cmbIslemTuru = new Guna.UI2.WinForms.Guna2ComboBox
                {
                    Location = new Point(130, 170),
                    Size = new Size(200, 36),
                    FillColor = Color.White,
                    BorderColor = Color.FromArgb(94, 148, 255),
                    DropDownStyle = ComboBoxStyle.DropDownList
                };
                cmbIslemTuru.Items.AddRange(new object[] { "Tümü", "Sadece Borçlar", "Sadece Tahsilatlar" });
                cmbIslemTuru.SelectedIndex = 0;

                // Tutar aralığı bölümü
                Label lblTutarAralik = new Label
                {
                    Text = "Tutar Aralığı",
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    ForeColor = Color.FromArgb(64, 64, 64),
                    Location = new Point(20, 220),
                    AutoSize = true
                };

                Label lblMinTutar = new Label
                {
                    Text = "Min Tutar:",
                    Font = new Font("Segoe UI", 9),
                    ForeColor = Color.FromArgb(64, 64, 64),
                    Location = new Point(40, 250),
                    AutoSize = true
                };

                Label lblMaxTutar = new Label
                {
                    Text = "Max Tutar:",
                    Font = new Font("Segoe UI", 9),
                    ForeColor = Color.FromArgb(64, 64, 64),
                    Location = new Point(40, 280),
                    AutoSize = true
                };

                // Guna2 TextBox'lar
                Guna.UI2.WinForms.Guna2TextBox txtMinTutar = new Guna.UI2.WinForms.Guna2TextBox
                {
                    Location = new Point(130, 250),
                    Size = new Size(200, 36),
                    PlaceholderText = "0",
                    FillColor = Color.White,
                    BorderColor = Color.FromArgb(94, 148, 255)
                };

                Guna.UI2.WinForms.Guna2TextBox txtMaxTutar = new Guna.UI2.WinForms.Guna2TextBox
                {
                    Location = new Point(130, 280),
                    Size = new Size(200, 36),
                    PlaceholderText = "10000",
                    FillColor = Color.White,
                    BorderColor = Color.FromArgb(94, 148, 255)
                };

                // Açıklama arama bölümü
                Label lblAciklamaAra = new Label
                {
                    Text = "Açıklama Ara",
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    ForeColor = Color.FromArgb(64, 64, 64),
                    Location = new Point(20, 330),
                    AutoSize = true
                };

                Guna.UI2.WinForms.Guna2TextBox txtAciklamaAra = new Guna.UI2.WinForms.Guna2TextBox
                {
                    Location = new Point(130, 330),
                    Size = new Size(325, 36),
                    PlaceholderText = "Açıklama içinde ara...",
                    FillColor = Color.White,
                    BorderColor = Color.FromArgb(94, 148, 255)
                };

                // Butonlar
                Guna.UI2.WinForms.Guna2Button btnSorgula = new Guna.UI2.WinForms.Guna2Button
                {
                    Text = "Sorgula",
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    ForeColor = Color.White,
                    FillColor = Color.FromArgb(94, 148, 255),
                    BorderRadius = 20,
                    Location = new Point(130, 380),
                    Size = new Size(150, 40)
                };

                Guna.UI2.WinForms.Guna2Button btnIptal = new Guna.UI2.WinForms.Guna2Button
                {
                    Text = "İptal",
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    ForeColor = Color.White,
                    FillColor = Color.Gray,
                    BorderRadius = 20,
                    Location = new Point(300, 380),
                    Size = new Size(150, 40)
                };

                // Buton olaylarını ekleyelim
                btnSorgula.Click += (s, args) => {
                    try
                    {
                        // Değerleri al
                        DateTime baslangicTarih = dtpBaslangic.Value.Date;
                        DateTime bitisTarih = dtpBitis.Value.Date.AddDays(1).AddSeconds(-1); // Günün sonuna ayarla

                        // Tutar aralığı
                        decimal minTutar = 0;
                        decimal maxTutar = decimal.MaxValue;

                        if (!string.IsNullOrEmpty(txtMinTutar.Text))
                            decimal.TryParse(txtMinTutar.Text, out minTutar);

                        if (!string.IsNullOrEmpty(txtMaxTutar.Text))
                            decimal.TryParse(txtMaxTutar.Text, out maxTutar);

                        // İşlem türü
                        string islemTuru = cmbIslemTuru.SelectedIndex == 0 ? "Tümü" :
                                         cmbIslemTuru.SelectedIndex == 1 ? "Borç" : "Tahsilat";

                        // Açıklama araması
                        string aciklamaAra = txtAciklamaAra.Text;

                        // Sadece görüntüleme için sorgulama işlemi yap
                        SorgulamaYap(baslangicTarih, bitisTarih, islemTuru, minTutar, maxTutar, aciklamaAra);

                        sorgulamaForm.DialogResult = DialogResult.OK;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Sorgulama sırasında bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                };

                btnIptal.Click += (s, args) => {
                    sorgulamaForm.DialogResult = DialogResult.Cancel;
                };

                // Kontrolleri forma ekle
                sorgulamaForm.Controls.AddRange(new Control[] {
            lblTitle,
            lblTarihAralik, lblBaslangic, lblBitis, dtpBaslangic, dtpBitis,
            lblIslemTuru, cmbIslemTuru,
            lblTutarAralik, lblMinTutar, lblMaxTutar, txtMinTutar, txtMaxTutar,
            lblAciklamaAra, txtAciklamaAra,
            btnSorgula, btnIptal
        });

                // Formu göster
                if (sorgulamaForm.ShowDialog() == DialogResult.OK)
                {
                    // Sorgulama zaten yapıldı
                }
                else
                {
                    // İptal edildi - varsayılan değerlere dön
                    LoadCariHareketleri(currentCariKodu);
                }
            }
        }

        // Sadece görüntüleme amaçlı sorgulama işlemini gerçekleştiren metod
        public void SorgulamaYap(DateTime baslangicTarih, DateTime bitisTarih, string islemTuru,
                                  decimal minTutar, decimal maxTutar, string aciklamaAra)
        {
            // Mevcut dataGridView1'i temizle
            dataGridView1.Rows.Clear();

            try
            {
                string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Sorgu koşullarını oluştur
                    List<string> conditions = new List<string>();
                    conditions.Add("cari_kodu = @CariKodu");
                    conditions.Add("tarih BETWEEN @BaslangicTarih AND @BitisTarih");

                    // İşlem türü filtresi
                    if (islemTuru == "Borç")
                        conditions.Add("borc > 0");
                    else if (islemTuru == "Tahsilat")
                        conditions.Add("tahsilat > 0");

                    // Tutar aralığı filtresi
                    if (minTutar > 0 || maxTutar < decimal.MaxValue)
                    {
                        conditions.Add("((borc BETWEEN @MinTutar AND @MaxTutar AND borc > 0) OR " +
                                     "(tahsilat BETWEEN @MinTutar AND @MaxTutar AND tahsilat > 0))");
                    }

                    // Açıklama araması filtresi
                    if (!string.IsNullOrWhiteSpace(aciklamaAra))
                        conditions.Add("aciklama LIKE @AciklamaAra");

                    // Sorgu oluştur
                    string whereClause = string.Join(" AND ", conditions);
                    string query = $"SELECT * FROM cari_hareketleri WHERE {whereClause} ORDER BY tarih DESC";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        // Parametreleri ekle
                        command.Parameters.AddWithValue("@CariKodu", currentCariKodu);
                        command.Parameters.AddWithValue("@BaslangicTarih", baslangicTarih.ToString("yyyy-MM-dd"));
                        command.Parameters.AddWithValue("@BitisTarih", bitisTarih.ToString("yyyy-MM-dd HH:mm:ss"));

                        if (minTutar > 0 || maxTutar < decimal.MaxValue)
                        {
                            command.Parameters.AddWithValue("@MinTutar", minTutar);
                            command.Parameters.AddWithValue("@MaxTutar", maxTutar);
                        }

                        if (!string.IsNullOrWhiteSpace(aciklamaAra))
                            command.Parameters.AddWithValue("@AciklamaAra", "%" + aciklamaAra + "%");

                        // Sorguyu çalıştır ve sonuçları doldur
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            decimal toplamBorc = 0;
                            decimal toplamTahsilat = 0;

                            while (reader.Read())
                            {
                                // Değerleri al
                                string tarih = reader["tarih"] != DBNull.Value ?
                                              Convert.ToDateTime(reader["tarih"]).ToString("dd.MM.yyyy") : "";
                                string tur = reader["tur"] != DBNull.Value ? reader["tur"].ToString() : "";
                                string aciklama = reader["aciklama"] != DBNull.Value ? reader["aciklama"].ToString() : "";

                                decimal borc = reader["borc"] != DBNull.Value ? Convert.ToDecimal(reader["borc"]) : 0;
                                decimal tahsilat = reader["tahsilat"] != DBNull.Value ? Convert.ToDecimal(reader["tahsilat"]) : 0;

                                // Toplamları güncelle
                                toplamBorc += borc;
                                toplamTahsilat += tahsilat;

                                // DataGridView'e ekle
                                int rowIndex = dataGridView1.Rows.Add(
                                    reader["id"], // ID (gizli sütun)
                                    tarih,        // Tarih
                                    tur,          // Tür
                                    aciklama,     // Açıklama
                                    borc > 0 ? borc.ToString("C2") : "",  // Borç
                                    tahsilat > 0 ? tahsilat.ToString("C2") : "" // Tahsilat
                                );
                            }

                            // Alt toplamları güncelle - Sadece görüntüleme amaçlı
                            textBox1.Text = toplamBorc.ToString("C2");
                            textBox2.Text = toplamTahsilat.ToString("C2");
                            textBox3.Text = (toplamBorc - toplamTahsilat).ToString("C2");

                            // Yazıları kalın yap
                            textBox1.Font = new Font(textBox1.Font, FontStyle.Bold);
                            textBox2.Font = new Font(textBox2.Font, FontStyle.Bold);
                            textBox3.Font = new Font(textBox3.Font, FontStyle.Bold);

                            // Bakiye durumuna göre renklendirme
                            decimal bakiye = toplamBorc - toplamTahsilat;
                            textBox3.ForeColor = bakiye < 0 ? Color.Red : Color.Black;

                            // İşlem sayısını güncelle
                            button13.Text = "Toplam İşlem Sayısı = " + dataGridView1.Rows.Count;

                            // Sorgulama bilgisi ekle - Kullanıcıya filtreleme yapıldığını bildirmek için
                            Label sorgulamaInfo = new Label
                            {
                                Text = "✓ Görüntülenen sonuçlar filtrelenmiştir. Tüm kayıtları görmek için 'Geri' butonuna basın.",
                                ForeColor = Color.Blue,
                                Font = new Font("Segoe UI", 8, FontStyle.Italic),
                                AutoSize = true,
                                Location = new Point(5, 5)
                            };

                            // Eğer zaten bir bilgi etiketi varsa kaldır
                            foreach (Control ctrl in this.Controls)
                            {
                                if (ctrl is Label && ctrl.Text.Contains("filtrelenmiştir"))
                                {
                                    this.Controls.Remove(ctrl);
                                    ctrl.Dispose();
                                    break;
                                }
                            }

                            this.Controls.Add(sorgulamaInfo);
                            sorgulamaInfo.BringToFront();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veritabanı sorgusu sırasında bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Hata durumunda orijinal verileri yükle
                LoadCariHareketleri(currentCariKodu);
            }
        }

        public void Excelaktar_Click(object sender, EventArgs e)
        {
            // Excel için alias kullanımı
            Excel.Application excelApp = null;
            Excel.Workbook workbook = null;
            Excel.Worksheet sheet = null;

            try
            {
                // İşlem başlamadan önce fare imlecini bekleme simgesine çevir
                this.Cursor = Cursors.WaitCursor;

                // Excel uygulamasını başlat
                excelApp = new Excel.Application();
                excelApp.Visible = false; // İşlem bitene kadar görünmez tutuyoruz

                // Yeni Excel çalışma kitabı ve çalışma sayfası oluştur
                workbook = excelApp.Workbooks.Add();
                sheet = workbook.ActiveSheet;

                // ---------- BAŞLIK BÖLÜMÜ ----------
                // Firma adını al 
                Form4 form4Instance = System.Windows.Forms.Application.OpenForms.OfType<Form4>().FirstOrDefault();
                string firmaAdi = form4Instance?.FirmaAdi ?? "Firma Adı";

                // Cari adını button3'ten al
                string cariAdi = button3.Text;

                // Tarih bilgisi
                string tarih = DateTime.Now.ToString("dd.MM.yyyy HH:mm");

                // Başlık ekleme - Firma adı
                sheet.Cells[1, 1] = "FİRMA:";
                sheet.Cells[1, 2] = firmaAdi;

                // Birleştirme ve stil ayarları
                Excel.Range titleRange = sheet.Range["A1:B1"];
                titleRange.Font.Bold = true;
                titleRange.Font.Size = 12;

                // Cari adı
                sheet.Cells[2, 1] = "CARİ:";
                sheet.Cells[2, 2] = cariAdi;

                // Birleştirme ve stil ayarları
                Excel.Range cariRange = sheet.Range["A2:B2"];
                cariRange.Font.Bold = true;
                cariRange.Font.Size = 12;

                // Rapor tarihi
                sheet.Cells[3, 1] = "RAPOR TARİHİ:";
                sheet.Cells[3, 2] = tarih;

                // ---------- KOLON BAŞLIKLARI ----------
                int headerRow = 5; // Başlık satırı
                int colIndex = 1;

                // DataGridView'deki görünür sütunları ekle
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    if (col.Visible)
                    {
                        sheet.Cells[headerRow, colIndex] = col.HeaderText;

                        // Kolon başlıklarını formatla
                        Excel.Range headerCell = sheet.Cells[headerRow, colIndex];
                        headerCell.Font.Bold = true;
                        headerCell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                        headerCell.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                        headerCell.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                        colIndex++;
                    }
                }

                // ---------- VERİ SATIRLARINI EKLE ----------
                int rowIndex = headerRow + 1; // Başlık satırından sonrasından başla

                // Verileri satır satır ekle
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.IsNewRow) continue; // Yeni (boş) satırı atla

                    colIndex = 1;

                    foreach (DataGridViewColumn col in dataGridView1.Columns)
                    {
                        if (col.Visible)
                        {
                            // Hücre değerini al
                            object cellValue = row.Cells[col.Index].Value;

                            // Null kontrolü
                            if (cellValue != null)
                            {
                                // Para birimi formatını kontrol et
                                if (cellValue.ToString().StartsWith("₺"))
                                {
                                    // Para birimi formatında sayıyı ayır
                                    decimal value;
                                    if (decimal.TryParse(cellValue.ToString().Replace("₺", "").Replace(".", "").Replace(",", "."),
                                                       System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out value))
                                    {
                                        // Excel'de para birimi olarak format
                                        sheet.Cells[rowIndex, colIndex] = value;
                                        sheet.Cells[rowIndex, colIndex].NumberFormat = "#,##0.00 ₺";

                                        // Borç sütunu ise kırmızı renk
                                        if (col.HeaderText == "Borç" && value > 0)
                                        {
                                            sheet.Cells[rowIndex, colIndex].Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                        }
                                        // Tahsilat sütunu ise mavi renk
                                        else if (col.HeaderText == "Tahsilat" && value > 0)
                                        {
                                            sheet.Cells[rowIndex, colIndex].Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue);
                                        }
                                    }
                                    else
                                    {
                                        // Dönüştürülemezse düz metin olarak ekle
                                        sheet.Cells[rowIndex, colIndex] = cellValue.ToString();
                                    }
                                }
                                else
                                {
                                    // Normal değeri ekle
                                    sheet.Cells[rowIndex, colIndex] = cellValue.ToString();
                                }
                            }

                            // Alt kenarlık ekle
                            sheet.Cells[rowIndex, colIndex].Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            sheet.Cells[rowIndex, colIndex].Borders[Excel.XlBordersIndex.xlEdgeBottom].Weight = Excel.XlBorderWeight.xlThin;

                            colIndex++;
                        }
                    }

                    // Tek ve çift satırlar için farklı renk
                    if (rowIndex % 2 == 0)
                    {
                        Excel.Range rowRange = sheet.Range[sheet.Cells[rowIndex, 1], sheet.Cells[rowIndex, colIndex - 1]];
                        rowRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(240, 240, 240));
                    }

                    rowIndex++;
                }

                // ---------- TOPLAM BİLGİLERİ ----------
                // Alt çizgi çek
                Excel.Range lineRange = sheet.Range[sheet.Cells[rowIndex, 1], sheet.Cells[rowIndex, colIndex - 1]];
                lineRange.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
                lineRange.Borders[Excel.XlBordersIndex.xlEdgeTop].Weight = Excel.XlBorderWeight.xlMedium;

                rowIndex++; // Bir satır aşağı in

                // Toplam borç
                sheet.Cells[rowIndex, 1] = "TOPLAM BORÇ:";
                string borcText = textBox1.Text.Replace("₺", "").Trim();

                // Ondalık ayırıcıları düzelt
                decimal borc;
                if (decimal.TryParse(borcText.Replace(".", "").Replace(",", "."),
                                  System.Globalization.NumberStyles.Any,
                                  System.Globalization.CultureInfo.InvariantCulture, out borc))
                {
                    sheet.Cells[rowIndex, 2] = borc;
                    sheet.Cells[rowIndex, 2].NumberFormat = "#,##0.00 ₺";
                }
                else
                {
                    sheet.Cells[rowIndex, 2] = borcText;
                }

                Excel.Range borcRange = sheet.Range[sheet.Cells[rowIndex, 1], sheet.Cells[rowIndex, 2]];
                borcRange.Font.Bold = true;

                rowIndex++; // Bir satır aşağı in

                // Toplam tahsilat
                sheet.Cells[rowIndex, 1] = "TOPLAM TAHSİLAT:";
                string tahsilatText = textBox2.Text.Replace("₺", "").Trim();

                decimal tahsilat;
                if (decimal.TryParse(tahsilatText.Replace(".", "").Replace(",", "."),
                                  System.Globalization.NumberStyles.Any,
                                  System.Globalization.CultureInfo.InvariantCulture, out tahsilat))
                {
                    sheet.Cells[rowIndex, 2] = tahsilat;
                    sheet.Cells[rowIndex, 2].NumberFormat = "#,##0.00 ₺";
                }
                else
                {
                    sheet.Cells[rowIndex, 2] = tahsilatText;
                }

                Excel.Range tahsilatRange = sheet.Range[sheet.Cells[rowIndex, 1], sheet.Cells[rowIndex, 2]];
                tahsilatRange.Font.Bold = true;

                rowIndex++; // Bir satır aşağı in

                // Bakiye
                sheet.Cells[rowIndex, 1] = "BAKİYE:";
                string bakiyeText = textBox3.Text.Replace("₺", "").Trim();

                decimal bakiye;
                if (decimal.TryParse(bakiyeText.Replace(".", "").Replace(",", "."),
                                  System.Globalization.NumberStyles.Any,
                                  System.Globalization.CultureInfo.InvariantCulture, out bakiye))
                {
                    sheet.Cells[rowIndex, 2] = bakiye;
                    sheet.Cells[rowIndex, 2].NumberFormat = "#,##0.00 ₺";

                    // Negatif bakiye için kırmızı renk
                    if (bakiye < 0)
                    {
                        sheet.Cells[rowIndex, 2].Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                    }
                }
                else
                {
                    sheet.Cells[rowIndex, 2] = bakiyeText;

                    // Negatif bakiye için kırmızı renk
                    if (bakiyeText.Contains("-"))
                    {
                        sheet.Cells[rowIndex, 2].Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                    }
                }

                Excel.Range bakiyeRange = sheet.Range[sheet.Cells[rowIndex, 1], sheet.Cells[rowIndex, 2]];
                bakiyeRange.Font.Bold = true;

                // ---------- BİÇİMLENDİRME ----------
                // Sütun genişliklerini otomatik ayarla
                sheet.Columns.AutoFit();

                // Tarih sütunu için özel genişlik
                int tarihSutunu = -1;
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    if (dataGridView1.Columns[i].Visible && dataGridView1.Columns[i].HeaderText == "Tarih")
                    {
                        tarihSutunu = i + 1; // Excel 1'den başlar
                        break;
                    }
                }

                if (tarihSutunu > 0)
                {
                    sheet.Columns[tarihSutunu].ColumnWidth = 15; // Tarih sütunu için sabit genişlik
                }

                // Açıklama sütunu için özel genişlik
                int aciklamaSutunu = -1;
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    if (dataGridView1.Columns[i].Visible && dataGridView1.Columns[i].HeaderText == "Açıklama")
                    {
                        aciklamaSutunu = i + 1; // Excel 1'den başlar
                        break;
                    }
                }

                if (aciklamaSutunu > 0)
                {
                    sheet.Columns[aciklamaSutunu].ColumnWidth = 40; // Açıklama sütunu için geniş alan
                }

                // Dosya adını oluştur
                string dosyaAdi = $"{cariAdi.Replace(" ", "-")}-Cari-Hareketleri-{DateTime.Now:dd.MM.yyyy}.xlsx";
                string filePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), dosyaAdi);

                // Dosyayı kaydet
                workbook.SaveAs(filePath);

                // Excel'i görünür yap
                excelApp.Visible = true;

                // İşlem tamamlandı, fare imlecini normal duruma getir
                this.Cursor = Cursors.Default;

                MessageBox.Show("Veriler başarıyla Excel'e aktarıldı.\nDosya masaüstüne kaydedildi: " + dosyaAdi,
                              "İşlem Tamamlandı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show($"Excel'e aktarım sırasında bir hata oluştu: {ex.Message}",
                              "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Kaynakları serbest bırak
                if (sheet != null) Marshal.ReleaseComObject(sheet);
                if (workbook != null) Marshal.ReleaseComObject(workbook);
                if (excelApp != null)
                {
                    // Herhangi bir çalışan Excel örneğini kapatmaya zorla 
                    // (istemiyorsanız bu kısmı kaldırabilirsiniz)
                    // excelApp.Quit();
                    Marshal.ReleaseComObject(excelApp);
                }

                sheet = null;
                workbook = null;
                excelApp = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        //Emanet ekleme sistemi başlangıç
        // Sınıf seviyesinde tanımlanacak değişkenler - Carihareketler sınıfı içinde
        public Label emanetBildirimLabel = null;

        // Emanet durumunu kontrol eden metod
        public void EmanetDurumunuKontrolEt(string cariKodu)
        {
            // Tüm mevcut emanet bildirimlerini temizle
            TemizleEmanetBildirimleri();

            // Teslim edilmemiş emanet sayısını kontrol et
            int emanetSayisi = GetTeslimEdilmemisEmanetSayisi(cariKodu);
            if (emanetSayisi > 0)
            {
                // emanet butonunun bulunduğu container'ı bul
                Control container = emanet.Parent;

                // Sarı oval bildirim etiketi oluştur
                emanetBildirimLabel = new Label
                {
                    Text = emanetSayisi.ToString(),
                    ForeColor = Color.Black,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    TextAlign = ContentAlignment.MiddleCenter,
                    BackColor = Color.FromArgb(241, 196, 15), // Sarı
                    Size = new Size(22, 22),
                    Location = new Point(
                        emanet.Right - 22,
                        emanet.Top - 2
                    )
                };

                // Oval yapmak için Paint olayını ekle
                emanetBildirimLabel.Paint += (s, e) => {
                    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    using (var path = new System.Drawing.Drawing2D.GraphicsPath())
                    {
                        path.AddEllipse(0, 0, emanetBildirimLabel.Width - 1, emanetBildirimLabel.Height - 1);
                        emanetBildirimLabel.Region = new Region(path);
                    }
                };

                // Butonu içeren container'a ekle
                container.Controls.Add(emanetBildirimLabel);
                emanetBildirimLabel.BringToFront();
            }
        }

        // Teslim edilmemiş emanet sayısını getiren metod
        public int GetTeslimEdilmemisEmanetSayisi(string cariKodu)
        {
            int emanetSayisi = 0;
            try
            {
                string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    

                    string sorgu = "SELECT COUNT(*) FROM Emanetler WHERE CariKodu = @CariKodu AND Durum != 'Teslim Edildi'";
                    using (SQLiteCommand command = new SQLiteCommand(sorgu, connection))
                    {
                        command.Parameters.AddWithValue("@CariKodu", cariKodu);
                        emanetSayisi = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Emanet sayısı kontrolü sırasında hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return emanetSayisi;
        }

        // Bildirimleri temizleyen güvenli metod
        public void TemizleEmanetBildirimleri()
        {
            // Bildirim etiketini kaldır
            if (emanetBildirimLabel != null)
            {
                if (this.Controls.Contains(emanetBildirimLabel))
                {
                    this.Controls.Remove(emanetBildirimLabel);
                }
                emanetBildirimLabel.Dispose();
                emanetBildirimLabel = null;
            }
        }

        // Emanetler tablosunu kontrol et ve yoksa oluştur

        // Emanet butonuna tıklama olayı
        public void emanet_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentCariKodu))
            {
                MessageBox.Show("Lütfen bir cari seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CariEmanet formEmanet = new CariEmanet(currentCariKodu, button3.Text);
            formEmanet.ShowDialog(this);

            // Emanet durumunu kontrol et
            EmanetDurumunuKontrolEt(currentCariKodu);
        }

        // AlarmListesiniGoster metodu - Carihareketler sınıfı içinde bir yere ekleyin
        // Carihareketler sınıfı içinde
        // Carihareketler sınıfı içinde
        public void AlarmListesiniGoster()
        {
            // AcAlarmListesiniFormu metodunu çağır
            AcAlarmListesiniFormu();
        }

        public void AcAlarmListesiniFormu()
        {
            try
            {
                // Alarmkur sınıfından AlarmListesiniGoster metodunu çağır
                Alarmkur alarmkur = new Alarmkur();
                alarmkur.AlarmListesiniGoster();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Alarm listesi açılırken bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Geçersiz hücre tıklamasını kontrol et
                if (e.RowIndex < 0 || e.ColumnIndex < 0)
                    return;

                // Bu satır için alarm var mı kontrol et
                bool hasAlarm = satirAlarmlari.ContainsKey(e.RowIndex) && satirAlarmlari[e.RowIndex];

                if (!hasAlarm)
                    return; // Bu satırda alarm yoksa işlem yapma

                // Tıklanan sütunun borç veya tahsilat olup olmadığını kontrol et
                string headerText = dataGridView1.Columns[e.ColumnIndex].HeaderText.ToLower();
                string columnName = dataGridView1.Columns[e.ColumnIndex].Name.ToLower();

                bool isBorcOrTahsilatColumn =
                    headerText.Contains("borç") || headerText.Contains("borc") || columnName.Contains("borç") || columnName.Contains("borc") ||
                    headerText.Contains("tahsilat") || columnName.Contains("tahsilat");

                if (isBorcOrTahsilatColumn)
                {
                    // Alarm ovaline tıklandığını tespit etmek için, hücrenin sağ üst köşesine yakın bir tıklama mı kontrol et
                    Rectangle cellRect = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                    Point clickPoint = dataGridView1.PointToClient(Cursor.Position);

                    // Oval bildirim alanı (sağ üst köşeden 20px çapında bir alan)
                    int ovalSize = 20;
                    Rectangle ovalArea = new Rectangle(
                        cellRect.Right - ovalSize,
                        cellRect.Y,
                        ovalSize,
                        ovalSize);

                    // Tıklama oval alana mı yapıldı?
                    if (ovalArea.Contains(clickPoint))
                    {
                        // Olay burada gerçekleşiyor - Alarm Listesini açma
                        AcAlarmListesiniFormu(); // Bu satırda bizim yeni metodumuzu çağırıyoruz
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Cell Click olayında hata: {ex.Message}");
                MessageBox.Show($"Alarm göstergesi işlenirken bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // EkleSagTikMenusu metodu - Carihareketler sınıfı içinde bir yere ekleyin
        public void EkleSagTikMenusu(DataGridView dgv)
        {
            // Yeni context menu oluştur
            ContextMenuStrip contextMenu = new ContextMenuStrip();

            // Tamamlandı menü öğesi
            ToolStripMenuItem tamamlandiItem = new ToolStripMenuItem("Tamamlandı");
            tamamlandiItem.Name = "tamamlandiItem";
            // Resources klasöründe check ikonu varsa kullanabilirsiniz
            // tamamlandiItem.Image = Properties.Resources.check;
            tamamlandiItem.Click += (sender, e) => {
                if (dgv.SelectedRows.Count > 0 && dgv.SelectedRows[0].Cells["ID"].Value != null)
                {
                    int alarmId = Convert.ToInt32(dgv.SelectedRows[0].Cells["ID"].Value);
                    GuncelleAlarmDurumu(alarmId, "Tamamlandı");
                    YenileAlarmListesi(dgv);
                }
            };
            contextMenu.Items.Add(tamamlandiItem);

            // Ertele menü öğesi
            ToolStripMenuItem erteleItem = new ToolStripMenuItem("Ertele");
            erteleItem.Name = "erteleItem";
            // Resources klasöründe clock ikonu varsa kullanabilirsiniz
            // erteleItem.Image = Properties.Resources.clock;
            erteleItem.Click += (sender, e) => {
                if (dgv.SelectedRows.Count > 0 && dgv.SelectedRows[0].Cells["ID"].Value != null)
                {
                    int alarmId = Convert.ToInt32(dgv.SelectedRows[0].Cells["ID"].Value);
                    GosterErtelemeFormu(alarmId);
                    YenileAlarmListesi(dgv);
                }
            };
            contextMenu.Items.Add(erteleItem);

            // Ayırıcı çizgi
            contextMenu.Items.Add(new ToolStripSeparator());

            // Tümünü Sil menü öğesi
            ToolStripMenuItem tumunuSilItem = new ToolStripMenuItem("Tümünü Sil");
            tumunuSilItem.Name = "tumunuSilItem";
            // Resources klasöründe delete ikonu varsa kullanabilirsiniz
            // tumunuSilItem.Image = Properties.Resources.delete;
            tumunuSilItem.Click += (sender, e) => {
                DialogResult result = MessageBox.Show(
                    "Tüm alarmları silmek istediğinizden emin misiniz?",
                    "Onay",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    TumAlarmlariSil();
                    YenileAlarmListesi(dgv);
                }
            };
            contextMenu.Items.Add(tumunuSilItem);

            // Context menu açılmadan önce menü öğelerinin durumunu ayarla
            contextMenu.Opening += (sender, e) => {
                bool satirSecili = dgv.SelectedRows.Count > 0;

                // Tamamlandı ve Ertele butonlarını sadece seçili satır varsa etkinleştir
                tamamlandiItem.Enabled = satirSecili;
                erteleItem.Enabled = satirSecili;

                if (satirSecili)
                {
                    // Seçili satırın durumunu kontrol et (eğer durum sütunu varsa)
                    if (dgv.Columns.Contains("Durum") &&
                        dgv.SelectedRows[0].Cells["Durum"].Value != null &&
                        dgv.SelectedRows[0].Cells["Durum"].Value.ToString() == "Tamamlandı")
                    {
                        // Zaten tamamlanmışsa işlevsiz yap
                        tamamlandiItem.Enabled = false;
                        erteleItem.Enabled = false;
                    }
                }

                // Tümünü sil menü öğesi için dgv boş değilse etkinleştir
                tumunuSilItem.Enabled = dgv.Rows.Count > 0;
            };

            // DataGridView'a context menüyü ata
            dgv.ContextMenuStrip = contextMenu;
        }

        // YenileAlarmListesi metodu - Carihareketler sınıfı içinde bir yere ekleyin
        public void YenileAlarmListesi(DataGridView dgv = null)
        {
            try
            {
                // Parametre olarak gelen DataGridView null ise tüm açık formlardaki DataGridView'ları güncelle
                if (dgv == null)
                {
                    // Açık olan AlarmListesi formunu bul ve oradaki DataGridView'ı güncelle
                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.Text == "Alarm Listesi")
                        {
                            foreach (Control control in form.Controls)
                            {
                                if (control is DataGridView)
                                {
                                    dgv = (DataGridView)control;
                                    break;
                                }
                            }
                            break;
                        }
                    }

                    // Hala dgv null ise yapılacak bir şey yok
                    if (dgv == null) return;
                }

                // DataGridView'ı temizle
                dgv.Rows.Clear();

                string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                SELECT 
                    id,
                    cari_kodu,
                    cari_unvan,
                    alarm_tarihi,
                    mesaj,
                    odeme_turu,
                    onem_derecesi,
                    durum,
                    IFNULL(erteleme_sayisi, 0) as erteleme_sayisi
                FROM Alarmlar
                ORDER BY 
                    CASE 
                        WHEN durum = 'Tamamlandı' THEN 2
                        ELSE 1
                    END,
                    datetime(alarm_tarihi) ASC";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DateTime alarmTarihi = Convert.ToDateTime(reader["alarm_tarihi"]);
                                string durum = reader["durum"].ToString();
                                string onemDerecesi = reader["onem_derecesi"].ToString();
                                int ertelemeSayisi = Convert.ToInt32(reader["erteleme_sayisi"]);

                                int rowIndex = dgv.Rows.Add(
                                    reader["id"],
                                    reader["cari_unvan"].ToString(),
                                    alarmTarihi.ToString("dd.MM.yyyy HH:mm"),
                                    reader["odeme_turu"].ToString(),
                                    reader["mesaj"].ToString(),
                                    durum,
                                    onemDerecesi
                                );

                                // Duruma göre satır stilini ayarla
                                if (durum == "Tamamlandı")
                                {
                                    dgv.Rows[rowIndex].DefaultCellStyle.ForeColor = Color.Gray;
                                    dgv.Rows[rowIndex].DefaultCellStyle.Font = new Font(dgv.Font, FontStyle.Strikeout);
                                }
                                else if (alarmTarihi < DateTime.Now)
                                {
                                    // Geçmiş alarm
                                    dgv.Rows[rowIndex].DefaultCellStyle.BackColor = Color.MistyRose;
                                }

                                // Önem derecesine göre görsel farklılıklar
                                if (onemDerecesi == "Yüksek" && durum != "Tamamlandı")
                                {
                                    dgv.Rows[rowIndex].DefaultCellStyle.Font = new Font(dgv.Font, FontStyle.Bold);
                                }
                                else if (onemDerecesi == "Kritik" && durum != "Tamamlandı")
                                {
                                    dgv.Rows[rowIndex].DefaultCellStyle.BackColor = Color.LightSalmon;
                                    dgv.Rows[rowIndex].DefaultCellStyle.Font = new Font(dgv.Font, FontStyle.Bold);
                                }
                            }
                        }
                    }
                }

                // Form4'ü güncelle (eğer açıksa)
                Form4 form4 = Application.OpenForms["Form4"] as Form4;
                if (form4 != null)
                {
                    form4.KontrolEtAlarmlari();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Alarm listesi güncellenirken bir hata oluştu: {ex.Message}",
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // GuncelleAlarmDurumu metodu - Carihareketler sınıfı içinde bir yere ekleyin
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
                        int result = command.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Alarm durumu güncellenirken bir hata oluştu: {ex.Message}",
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // TumAlarmlariSil metodu - Carihareketler sınıfı içinde bir yere ekleyin
        public bool TumAlarmlariSil()
        {
            try
            {
                string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM Alarmlar";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        int result = command.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Alarmlar silinirken bir hata oluştu: {ex.Message}",
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // GosterErtelemeFormu metodu - Carihareketler sınıfı içinde bir yere ekleyin
        public void GosterErtelemeFormu(int alarmId)
        {
            // Erteleme için yeni form göster
            Form ertelemeForm = new Form
            {
                Text = "Alarmı Ertele",
                Size = new Size(350, 200),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            Label lblBilgi = new Label
            {
                Text = "Alarmı ne kadar süre ertelemek istiyorsunuz?",
                Location = new Point(20, 20),
                Size = new Size(300, 20)
            };

            ComboBox cmbSure = new ComboBox
            {
                Location = new Point(20, 50),
                Size = new Size(300, 30),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            cmbSure.Items.AddRange(new object[] {
        "1 Saat", "3 Saat", "6 Saat", "12 Saat",
        "1 Gün", "2 Gün", "3 Gün", "1 Hafta", "2 Hafta", "1 Ay"
    });
            cmbSure.SelectedIndex = 0;

            Button btnErtele = new Button
            {
                Text = "Ertele",
                Location = new Point(135, 100),
                Size = new Size(90, 35),
                DialogResult = DialogResult.OK
            };

            Button btnIptal = new Button
            {
                Text = "İptal",
                Location = new Point(230, 100),
                Size = new Size(90, 35),
                DialogResult = DialogResult.Cancel
            };

            ertelemeForm.Controls.AddRange(new Control[] { lblBilgi, cmbSure, btnErtele, btnIptal });
            ertelemeForm.AcceptButton = btnErtele;
            ertelemeForm.CancelButton = btnIptal;

            if (ertelemeForm.ShowDialog() == DialogResult.OK)
            {
                // Seçilen süreye göre yeni alarm tarihini hesapla
                DateTime yeniTarih = DateTime.Now;
                string secim = cmbSure.SelectedItem.ToString();

                switch (secim)
                {
                    case "1 Saat": yeniTarih = DateTime.Now.AddHours(1); break;
                    case "3 Saat": yeniTarih = DateTime.Now.AddHours(3); break;
                    case "6 Saat": yeniTarih = DateTime.Now.AddHours(6); break;
                    case "12 Saat": yeniTarih = DateTime.Now.AddHours(12); break;
                    case "1 Gün": yeniTarih = DateTime.Now.AddDays(1); break;
                    case "2 Gün": yeniTarih = DateTime.Now.AddDays(2); break;
                    case "3 Gün": yeniTarih = DateTime.Now.AddDays(3); break;
                    case "1 Hafta": yeniTarih = DateTime.Now.AddDays(7); break;
                    case "2 Hafta": yeniTarih = DateTime.Now.AddDays(14); break;
                    case "1 Ay": yeniTarih = DateTime.Now.AddMonths(1); break;
                }

                // Alarmı ertele
                ErteleAlarm(alarmId, yeniTarih);
            }
        }

        // ErteleAlarm metodu - Carihareketler sınıfı içinde bir yere ekleyin
        public bool ErteleAlarm(int alarmId, DateTime yeniTarih)
        {
            try
            {
                string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Erteleme sayısını al
                    int ertelemeSayisi = 0;
                    string getQuery = "SELECT IFNULL(erteleme_sayisi, 0) FROM Alarmlar WHERE id = @id";
                    using (SQLiteCommand getCommand = new SQLiteCommand(getQuery, connection))
                    {
                        getCommand.Parameters.AddWithValue("@id", alarmId);
                        object result = getCommand.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            ertelemeSayisi = Convert.ToInt32(result);
                        }
                    }

                    // Erteleme sayısını artır
                    ertelemeSayisi++;

                    // Alarmı güncelle
                    string query = @"UPDATE Alarmlar 
                           SET alarm_tarihi = @yeniTarih, 
                               erteleme_sayisi = @ertelemeSayisi,
                               son_erteleme_tarihi = @sonErtelemeTarihi,
                               bildirildi = 0
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
                                "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return true;
                        }
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Alarm ertelenirken bir hata oluştu: {ex.Message}",
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public void AlarmEkle(string cariKodu, string cariUnvan)
        {
            try
            {
                if (string.IsNullOrEmpty(cariKodu) || string.IsNullOrEmpty(cariUnvan))
                {
                    MessageBox.Show("Cari bilgileri alınamadı. Lütfen geçerli bir cari seçin.",
                        "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Alarmkur formunu oluştur ve göster
                Alarmkur alarmForm = new Alarmkur(cariUnvan, cariKodu);
                DialogResult result = alarmForm.ShowDialog();

                // Sadece OK sonucu döndüyse (alarm eklendiğinde) yenileme işlemleri yap
                if (result == DialogResult.OK)
                {
                    Console.WriteLine("Alarm eklendi, yenileme işlemleri başlıyor...");

                    // Alarmları kontrol et
                    AlarmlarinDogrulugunuKontrolEt(cariKodu);

                    // Alarm statülerini yenile
                    RefreshAlarmStatuses();

                    // DataGridView'ı yenile
                    dataGridView1.Invalidate();
                    dataGridView1.Refresh();

                    // Form4'ü güncelle (eğer açıksa)
                    Form4 form4 = Application.OpenForms["Form4"] as Form4;
                    if (form4 != null)
                    {
                        form4.KontrolEtAlarmlari();
                    }

                    Console.WriteLine("Alarm yenileme işlemleri tamamlandı.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Alarm eklenirken bir hata oluştu: {ex.Message}",
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Alarm formundan sonra, her satır için alarm durumunu yeniden kontrol ediyoruz
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    CheckAlarmForRow(row);
                }
            }

            // Görünümü yenile
            dataGridView1.Invalidate();
            dataGridView1.Refresh();
        }

        // Sınıf düzeyinde tanımlamalar
        public Dictionary<int, bool> satirAlarmlari = new Dictionary<int, bool>();

        // Carihareketler_Load metodunda çağırın
        public void InitializeAlarmIndicators()
        {
            Console.WriteLine("InitializeAlarmIndicators başladı");

            // Sağ tık menüsüne ikon ekleme olayına bir üye ekle
            dataGridView1.CellPainting += DataGridView1_CellPainting;

            // Mevcut alarmlara bak ve dataGridView'daki ilgili satırları işaretle
            RefreshAlarmStatuses();

            Console.WriteLine("InitializeAlarmIndicators tamamlandı");
        }

        // Satırlardaki alarm durumlarını güncelle
        public void RefreshAlarmStatuses()
        {
            satirAlarmlari.Clear();

            // Alarmları bir kez çek ve cache'le
            HashSet<string> alarmTarihleri = new HashSet<string>();

            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // INDEX kullan
                string query = @"
            SELECT DISTINCT DATE(alarm_tarihi) as alarm_tarihi
            FROM Alarmlar
            WHERE cari_kodu = @CariKodu
            AND durum != 'Tamamlandı'";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CariKodu", currentCariKodu);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            alarmTarihleri.Add(Convert.ToDateTime(reader["alarm_tarihi"]).ToString("yyyy-MM-dd"));
                        }
                    }
                }
            }

            // Satırları işle (daha hızlı)
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].IsNewRow) continue;

                var tarihCell = dataGridView1.Rows[i].Cells["Tarih"];
                if (tarihCell.Value != null && DateTime.TryParse(tarihCell.Value.ToString(), out DateTime dt))
                {
                    if (alarmTarihleri.Contains(dt.ToString("yyyy-MM-dd")))
                    {
                        satirAlarmlari[i] = true;
                    }
                }
            }
        }

        // Sütun indeksini bulmak için yardımcı metot
        public int FindColumnIndex(params string[] possibleNames)
        {
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                string headerText = dataGridView1.Columns[i].HeaderText.ToLower();
                string columnName = dataGridView1.Columns[i].Name.ToLower();

                foreach (string name in possibleNames)
                {
                    if (headerText.Contains(name.ToLower()) || columnName.Contains(name.ToLower()))
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        // AlarmEkle metodundan sonra şu kodu ekleyin
        public void AlarmlarinDogrulugunuKontrolEt(string cariKodu)
        {
            Console.WriteLine($"------------------ ALARM KONTROL ------------------");
            Console.WriteLine($"Cari Kodu: {cariKodu}");

            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Alarmlar WHERE cari_kodu = @CariKodu AND durum != 'Tamamlandı'";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CariKodu", cariKodu);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        int i = 0;
                        while (reader.Read())
                        {
                            i++;
                            Console.WriteLine($"Alarm {i}:");
                            Console.WriteLine($"  ID: {reader["id"]}");
                            Console.WriteLine($"  Cari Unvan: {reader["cari_unvan"]}");
                            Console.WriteLine($"  Alarm Tarihi: {Convert.ToDateTime(reader["alarm_tarihi"])}");
                            Console.WriteLine($"  Durum: {reader["durum"]}");
                        }

                        if (i == 0)
                        {
                            Console.WriteLine("Bu cari için aktif alarm bulunamadı!");
                        }
                    }
                }
            }
            Console.WriteLine($"--------------------------------------------------");
        }

        // Her satırı kontrol eden ve alarm varsa işaretleyen metod
        public bool CheckAlarmForRow(DataGridViewRow row)
        {
            try
            {
                // Tarih sütunu bulundu mu kontrol et
                if (!dataGridView1.Columns.Contains("Tarih"))
                {
                    MessageBox.Show("Tarih sütunu bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                // Tür sütunu indeksini bul
                int turColumnIndex = -1;
                string turColumnName = "";

                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    string columnName = column.Name.ToLower();
                    if (columnName == "tür" || columnName == "tur" ||
                        column.HeaderText.ToLower() == "tür" || column.HeaderText.ToLower() == "tur")
                    {
                        turColumnIndex = column.Index;
                        turColumnName = column.Name;
                        break;
                    }
                }

                if (turColumnIndex == -1)
                {
                    MessageBox.Show("Tür sütunu bulunamadı! Mevcut sütunlar: " +
                        string.Join(", ", dataGridView1.Columns.Cast<DataGridViewColumn>().Select(c => c.Name)),
                        "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                // Satırdan gerekli bilgileri çıkar
                string tarih = "";
                if (row.Cells["Tarih"].Value != null)
                {
                    tarih = row.Cells["Tarih"].Value.ToString();
                }

                // Satır için alarm olup olmadığını kontrol et
                bool hasAlarm = SatirIcinAlarmVarMi(currentCariKodu, tarih, row.Index);

                // Alarm varsa Tag özelliğine işaretle
                row.Tag = hasAlarm ? "HasAlarm" : null;

                // Debug için konsola yaz
                Console.WriteLine($"Satır {row.Index}: Tarih={tarih}, Alarm={hasAlarm}");

                return hasAlarm;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Satır için alarm kontrolü sırasında hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Özel bir satır için alarm olup olmadığını kontrol eden metod - Debug eklenmiş versiyon
        public bool SatirIcinAlarmVarMi(string cariKodu, string tarih, int rowIndex)
        {
            try
            {
                if (string.IsNullOrEmpty(cariKodu))
                {
                    Console.WriteLine($"Satır {rowIndex}: Cari kodu boş!");
                    return false;
                }

                if (string.IsNullOrEmpty(tarih))
                {
                    Console.WriteLine($"Satır {rowIndex}: Tarih boş!");
                    return false;
                }

                // Tarih formatını normalize et
                DateTime dt;
                if (!DateTime.TryParse(tarih, out dt))
                {
                    Console.WriteLine($"Satır {rowIndex}: Tarih formatı geçersiz: {tarih}");
                    return false;
                }

                string formattedDate = dt.ToString("yyyy-MM-dd");

                string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                SELECT COUNT(*) FROM Alarmlar 
                WHERE cari_kodu = @CariKodu 
                AND DATE(alarm_tarihi) = @Tarih
                AND durum != 'Tamamlandı'";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CariKodu", cariKodu);
                        command.Parameters.AddWithValue("@Tarih", formattedDate);

                        int count = Convert.ToInt32(command.ExecuteScalar());
                        Console.WriteLine($"Satır {rowIndex}: CariKodu={cariKodu}, Tarih={formattedDate}, AlarmSayisi={count}");
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Alarm kontrolü sırasında hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public void DataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Başlık hücresi veya geçersiz hücre ise işlem yapma
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            // Bu satır için alarm var mı kontrol et
            bool hasAlarm = satirAlarmlari.ContainsKey(e.RowIndex) && satirAlarmlari[e.RowIndex];

            if (!hasAlarm)
                return; // Alarm yoksa çıkış yap

            // İşlem tipini belirle - Borç mu Tahsilat mı?
            bool isBorcColumn = false;
            bool isTahsilatColumn = false;

            // Sütun başlığı ve adı kontrolü
            string headerText = dataGridView1.Columns[e.ColumnIndex].HeaderText.ToLower();
            string columnName = dataGridView1.Columns[e.ColumnIndex].Name.ToLower();

            if (headerText.Contains("borç") || headerText.Contains("borc") ||
                columnName.Contains("borç") || columnName.Contains("borc"))
            {
                isBorcColumn = true;
            }
            else if (headerText.Contains("tahsilat") || columnName.Contains("tahsilat"))
            {
                isTahsilatColumn = true;
            }

            // Eğer bu bir Borç veya Tahsilat sütunu ise ve bu satırda bu sütunda değer varsa
            if ((isBorcColumn || isTahsilatColumn))
            {
                var cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (cell.Value != null)
                {
                    decimal value = 0;
                    string valueStr = cell.Value.ToString().Replace("₺", "").Trim();

                    if (decimal.TryParse(valueStr, out value) && value > 0)
                    {
                        // Normal hücre içeriğini çiz
                        e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                        // Özel çizim için Graphics nesnesi kullan
                        using (Graphics g = e.Graphics)
                        {
                            // Çizim kalitesini ayarla
                            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                            // Oval alarm göstergesini çiz (sağ üst köşeye)
                            int ovalSize = 12;
                            int ovalX = e.CellBounds.Right - ovalSize - 5;
                            int ovalY = e.CellBounds.Y + 5; // Üst kısma doğru

                            // Rengi ayarla - Borç için kırmızı, Tahsilat için mavi
                            Color ovalColor = isBorcColumn ? Color.Red : Color.Blue;

                            // Oval gösterge
                            using (SolidBrush brush = new SolidBrush(ovalColor))
                            {
                                g.FillEllipse(brush, ovalX, ovalY, ovalSize, ovalSize);
                            }

                            // Beyaz kenarlık
                            using (Pen pen = new Pen(Color.White, 1))
                            {
                                g.DrawEllipse(pen, ovalX, ovalY, ovalSize, ovalSize);
                            }

                            // İçine "!" işareti
                            using (Font font = new Font("Arial", 8, FontStyle.Bold))
                            using (SolidBrush brush = new SolidBrush(Color.White))
                            {
                                StringFormat format = new StringFormat
                                {
                                    Alignment = StringAlignment.Center,
                                    LineAlignment = StringAlignment.Center
                                };

                                g.DrawString("!", font, brush, new RectangleF(ovalX, ovalY, ovalSize, ovalSize), format);
                            }
                        }

                        // Çizimi biz hallettik
                        e.Handled = true;

                        // Debug amaçlı log
                        Console.WriteLine($"Alarm göstergesi çizildi: Satır {e.RowIndex}, Sütun {e.ColumnIndex}, İşlem: {(isBorcColumn ? "Borç" : "Tahsilat")}");
                    }
                }
            }
        }
    }
}
