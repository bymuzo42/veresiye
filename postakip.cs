using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;
using System.Drawing;
using System.Globalization;
namespace Veresiye2025
{
    internal partial class postakip : Form
    {
        // postakip formunun statik örneğini tutuyoruz
        internal static postakip Instance { get; set; }
        // Sayfa başına gösterilecek satır sayısı
        public int pageSize = 10;
        // Şu anda gösterilen sayfa numarası
        public int currentPage = 1;
        // Toplam sayfa sayısı
        public int totalPages = 1;
        // Parametre saklama işlemleri
        public string selectedBank = "Tümü"; // Seçilen banka
        public DateTime startDate = DateTime.Now; // Başlangıç tarihi (bugün)
        public DateTime endDate = DateTime.Now; // Bitiş tarihi (bugün)
        // Form taşıma işlemleri için değişkenler
        public bool isDragging = false;
        public Point dragStartPoint;
        internal postakip()
        {
            InitializeComponent();
            // Event handler'ları ekle
            this.Shown += new EventHandler(Postakip_Shown);
            this.Load += new EventHandler(Postakip_Load);
            this.KeyDown += new KeyEventHandler(Postakip_KeyDown);
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
            // Buton click olaylarını bağla
            btnClose.Click += (s, e) => this.Close();
            bankaekle.Click += bankaekle_Click;
            gunsonuekle.Click += gunsonuekle_Click;
            hareketsil.Click += hareketsil_Click;
            hareketduzenle.Click += hareketduzenle_Click;
            excelaktar.Click += excelaktar_Click;
            filtrele.Click += filtrele_Click;
            oncekisayfa.Click += oncekisayfa_Click;
            sonrakisayfa.Click += sonrakisayfa_Click;
            tumunusil.Click += tumunusil_Click;
            // DateTimePicker ayarları
            startDatePicker.Format = DateTimePickerFormat.Long;
            endDatePicker.Format = DateTimePickerFormat.Long;
            // Bugünün tarihine göre değerleri ayarla
            DateTime today = DateTime.Now;
            startDatePicker.Value = today; // Bugünün tarihi
            endDatePicker.Value = today;   // Bugünün tarihi
            // Singleton pattern
            Instance = this;
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
        public void ResetFilters()
        {
            selectedBank = "Tümü";  // Tüm bankalar
            startDate = DateTime.Now;  // Bugünün tarihi
            endDate = DateTime.Now;   // Bugünün tarihi
            // DateTimePicker'ları sıfırla
            if (startDatePicker != null)
                startDatePicker.Value = startDate;
            if (endDatePicker != null)
                endDatePicker.Value = endDate;
        }
        public void Postakip_Shown(object sender, EventArgs e)
        {
            // Verileri yükle
            BankalariYukle();
        }
        internal void Postakip_Load(object sender, EventArgs e)
        {
            try
            {
                // ESC tuşu ile formu kapatma işlemi için KeyPreview özelliğini true yapıyoruz
                this.KeyPreview = true;

                // DateTimePicker kontrollerinin değerlerini ayarla
                startDate = startDatePicker.Value;
                endDate = endDatePicker.Value;

                // ComboBox ayarı
                if (!bankaComboBox.Items.Contains("Tümü"))
                {
                    bankaComboBox.Items.Insert(0, "Tümü");
                }
                bankaComboBox.SelectedIndex = 0;

                // İlk sayfayı 1 olarak ayarla
                currentPage = 1;

                // Debug için başlangıçta toplam kayıt sayısını göster
                int totalRows = GetTotalRowCount();
                totalPages = (int)Math.Ceiling((double)totalRows / pageSize);
                Console.WriteLine($"Form yüklendiğinde toplam kayıt: {totalRows}, Sayfa sayısı: {totalPages}");

                // Bankaları ve verileri yükle
                BankalariYukle();
                LoadData(currentPage, true);

                // Sayfalama kontrollerini güncelle
                UpdatePaginationControls();

                // Diğer yükleme işlemleri
                DataGridViewAyarla();
                CihazSayisiniGoster();
                BankaSayisiniGoster();
                HesaplamalariYukle();

                // Tooltip ve istatistik ayarları
                SetupToolTips();
                if (UserSettings.Instance.ShowStatisticsPopup)
                {
                    ShowStatisticsPopup();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Form yüklenirken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void SetupToolTips()
        {
            // ComboBox ve düğmeler için açıklamalar
            toolTip1.SetToolTip(bankaComboBox, "Banka adı seçin, tüm veriyi görmek için 'Tümü' seçeneğini kullanın.");
            toolTip1.SetToolTip(gunsonuekle, "Yeni bir gün sonu ekleyebilirsiniz.");
            toolTip1.SetToolTip(hareketsil, "Seçili hareketi silmek için tıklayın.");
            toolTip1.SetToolTip(hareketduzenle, "Seçili hareketi düzenlemek için tıklayın.");
            toolTip1.SetToolTip(excelaktar, "Verileri Excel'e aktarmak için tıklayın.");
            toolTip1.SetToolTip(bankaekle, "Yeni bir banka eklemek için tıklayın.");
            toolTip1.SetToolTip(filtrele, "Seçilen banka ve tarih aralığına göre verileri filtreleyin.");
            toolTip1.SetToolTip(startDatePicker, "Başlangıç tarihini seçin. Filtrelemede bu tarihten önceki veriler göz ardı edilir.");
            toolTip1.SetToolTip(endDatePicker, "Bitiş tarihini seçin. Filtrelemede bu tarihten sonraki veriler göz ardı edilir.");
            // GroupBox'lar için açıklamalar
            toolTip1.SetToolTip(groupBox1, "Bu bölümde kayıtlı olan bankalar ve cihazların sayıları gösterilmektedir.");
            toolTip1.SetToolTip(groupBox2, "Bu bölümde bugün ve yarın hesabınıza geçecek olan tutarlar gösterilmektedir.");
            toolTip1.SetToolTip(groupBox3, "Bu bölümde toplam banka blokeniz ve toplam ciro hesaplamalarınız yer almaktadır.");
            toolTip1.SetToolTip(groupBox4, "Burada banka adını seçerek, seçilen bankaya ait verileri filtreleyebilirsiniz.");
        }
        public void Postakip_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();  // Escape tuşuna basıldığında formu kapat
            }
        }
        // Verileri yükle
        internal void LoadData(int page, bool initialLoad = false)
        {
            string connectionString = "Data Source=veresiye.db;Version=3;";
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Başlangıç indeksini hesapla
                    int startIndex = (page - 1) * pageSize;

                    // SQL sorgusu - sıralamayla birlikte
                    string query = "SELECT GunsonuID, CihazAdi, BankaAdi, Valör, Tutar, IslemTarihi, GececegiTarih " +
                                   "FROM Gunsonu ORDER BY IslemTarihi DESC " +
                                   $"LIMIT {pageSize} OFFSET {startIndex}";

                    // Debug çıktıları
                    Console.WriteLine($"Page: {page}, StartIndex: {startIndex}, PageSize: {pageSize}");
                    Console.WriteLine($"SQL Query: {query}");

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            // Debug çıktısı - yüklenen satır sayısı
                            Console.WriteLine($"Loaded rows for page {page}: {dt.Rows.Count}");

                            dataGridView1.DataSource = dt;
                        }
                    }

                    // DataGridView stillerini ayarla
                    DataGridViewAyarla();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veri yüklenirken hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        // Sayfa numarasını güncelle
        public void UpdatePaginationControls()
        {
            try
            {
                // Toplam kayıt sayısını al
                int totalRows = GetTotalRowCount();

                // Sayfa sayısı hesaplama 
                totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

                // Eğer hiç kayıt yoksa
                if (totalPages < 1) totalPages = 1;

                // Geçerli sayfa numarası kontrolü
                if (currentPage > totalPages) currentPage = totalPages;
                if (currentPage < 1) currentPage = 1;

                // Debug mesajları
                Console.WriteLine($"Total rows: {totalRows}, PageSize: {pageSize}");
                Console.WriteLine($"Current page: {currentPage}, Total pages: {totalPages}");

                // Sayfa bilgisini güncelle
                hangisayfa.Text = $"Sayfa {currentPage} / {totalPages}";

                // Sayfalama butonlarını etkinleştirme/devre dışı bırakma
                oncekisayfa.Enabled = (currentPage > 1);
                sonrakisayfa.Enabled = (currentPage < totalPages);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sayfalama kontrollerini güncellerken hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Veritabanındaki toplam veri sayısını al
        public int GetTotalRowCount()
        {
            string connectionString = "Data Source=veresiye.db;Version=3;";
            int totalRows = 0;

            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    // Basit ve direkt SQL sorgusu
                    string countQuery = "SELECT COUNT(*) FROM Gunsonu";

                    using (SQLiteCommand cmd = new SQLiteCommand(countQuery, conn))
                    {
                        totalRows = Convert.ToInt32(cmd.ExecuteScalar());
                        Console.WriteLine($"Gunsonu tablosundaki toplam satır sayısı: {totalRows}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Toplam satır sayısı alınırken hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return totalRows;
        }

        // Aşağıdaki metodu da sınıfınıza eklemelisiniz
        public void ShowStatisticsPopup()
        {
            // UI yüklenmesi tamamlandıktan sonra istatistik formunu göster
            this.BeginInvoke(new Action(() =>
            {
                try
                {
                    using (StatisticsForm statsForm = new StatisticsForm())
                    {
                        statsForm.ShowDialog();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("İstatistik popup'ı açılırken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }));
        }

        public void CihazSayisiniGoster()
        {
            string connectionString = "Data Source=veresiye.db;Version=3;";
            string query = "SELECT COUNT(DISTINCT CihazAdi) FROM Banka";
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        var result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            int cihazSayisi = Convert.ToInt32(result);
                            eklicihazgoster2.Text = "Cihaz Sayısı: " + cihazSayisi.ToString();
                        }
                        else
                        {
                            eklicihazgoster2.Text = "Cihaz Sayısı: 0";
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veritabanı hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        public void CihazSayisiniGuncelle()
        {
            string connectionString = "Data Source=veresiye.db;Version=3;";
            string query = "SELECT COUNT(DISTINCT CihazAdi) FROM Banka";
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        var result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            int cihazSayisi = Convert.ToInt32(result);
                            eklicihazgoster2.Text = "Cihaz Sayısı: " + cihazSayisi.ToString();
                            this.Invalidate();
                            this.Refresh();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Cihaz sayısı alınırken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        public void BankaSayisiniGoster()
        {
            string connectionString = "Data Source=veresiye.db;Version=3;";
            string query = "SELECT COUNT(DISTINCT BankaAdi) FROM Banka";
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        var result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            int bankaSayisi = Convert.ToInt32(result);
                            kayitlibanka.Text = "Banka Sayısı: " + bankaSayisi.ToString();
                        }
                        else
                        {
                            kayitlibanka.Text = "Banka Sayısı: 0";
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veritabanı hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        public void BankaSayisiniGuncelle()
        {
            string connectionString = "Data Source=veresiye.db;Version=3;";
            string query = "SELECT COUNT(DISTINCT BankaAdi) FROM Banka";
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        var result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            int bankaSayisi = Convert.ToInt32(result);
                            kayitlibanka.Text = "Banka Sayısı: " + bankaSayisi.ToString();
                            this.Invalidate();
                            this.Refresh();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Banka sayısı alınırken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        public void DataGridViewAyarla()
        {
            try
            {
                // Guna2DataGridView için stil ayarları
                dataGridView1.BorderStyle = BorderStyle.None;
                dataGridView1.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                dataGridView1.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(0, 123, 255);
                dataGridView1.ThemeStyle.HeaderStyle.ForeColor = Color.White;
                // Başlıklara tıklamayı ve sıralamayı tamamen etkisiz hale getirme
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                // Başlık stillerini ayarla
                dataGridView1.EnableHeadersVisualStyles = false;
                dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 123, 255);
                dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            catch (Exception ex)
            {
                Console.WriteLine("DataGridView ayarlanırken hata: " + ex.Message);
            }
        }
        // Banka ekleme butonunun tıklanması
        public void bankaekle_Click(object sender, EventArgs e)
        {
            try
            {
                using (bankaekle bankaForm = new bankaekle())
                {
                    bankaForm.ShowDialog();
                }
                // Banka ekleme işlemi sonrası listeyi güncelle
                BankalariYukle();
                CihazSayisiniGuncelle();
                BankaSayisiniGuncelle();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Günsonu ekleme butonunun tıklanması
        // LoadData ve UpdatePaginationControls metodlarını her veri değişiminde çağırın
        public void gunsonuekle_Click(object sender, EventArgs e)
        {
            try
            {
                // Ekleme öncesi kayıt sayısını al
                int beforeCount = GetTotalRowCount();
                Console.WriteLine($"Ekleme öncesi kayıt sayısı: {beforeCount}");

                // Yeni gün sonu formu aç
                using (GunSonuForm gunSonuForm = new GunSonuForm())
                {
                    gunSonuForm.Owner = this;
                    gunSonuForm.BankaAdiTextBox.Visible = false;
                    gunSonuForm.CihazAdiTextBox.Visible = false;
                    gunSonuForm.ShowDialog();
                }

                // Ekleme sonrası kayıt sayısını al
                int afterCount = GetTotalRowCount();
                Console.WriteLine($"Ekleme sonrası kayıt sayısı: {afterCount}");

                // Eğer yeni kayıt eklendiyse
                if (afterCount > beforeCount)
                {
                    // Yeni kayıt sayısına göre sayfa sayısını hesapla
                    int newTotalPages = (int)Math.Ceiling((double)afterCount / pageSize);

                    // Eğer yeni bir kayıt eklendikten sonra tam 10. kayıt oluştuysa
                    // ve önceki toplam kayıt sayısı 9 idiyse, 2. sayfaya geç
                    if (beforeCount % pageSize == (pageSize - 1) && afterCount % pageSize == 0)
                    {
                        currentPage = newTotalPages;
                        Console.WriteLine($"10. kayıt eklenince 2. sayfaya geçiş yapıldı. Sayfa: {currentPage}");
                    }
                }

                // Verileri yenile
                BankalariYukle();
                LoadData(currentPage);
                UpdatePaginationControls();
                HesaplamalariYukle();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Ayın kısaltmasını döndüren metot (Ocak -> OCA, Şubat -> SUB, ...)
        public string GetMonthPrefix()
        {
            string[] ayKisaltmalari = { "OCA", "SUB", "MAR", "NIS", "MAY", "HAZ", "TEM", "AGU", "EYL", "EKI", "KAS", "ARA" };
            int ay = DateTime.Now.Month - 1; // Dizi 0'dan başladığı için
            return ayKisaltmalari[ay];
        }

        // Aya göre sayaç ID oluşturan metot
        public string GenerateAyaSayaçID()
        {
            string ayKisaltmasi = GetMonthPrefix();
            int counter = GetMaxGunSonuIDForMonth(ayKisaltmasi);
            return $"{ayKisaltmasi}{(counter + 1):D4}";
        }

        // Belirli bir ay için maksimum GunSonuID'yi almak
        public int GetMaxGunSonuIDForMonth(string monthPrefix)
        {
            int counter = 0;
            string connectionString = "Data Source=veresiye.db;Version=3;";

            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
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

            return counter;
        }
        public int GetMonthCounter()
        {
            int counter = 0;
            string currentMonth = DateTime.Now.ToString("yyyyMM");
            string connectionString = "Data Source=veresiye.db;Version=3;";
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM Gunsonu WHERE strftime('%Y%m', IslemTarihi) = @Month";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Month", currentMonth);
                    counter = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            return counter + 1;
        }
        public string GenerateSayaçID()
        {
            string currentMonthPrefix = DateTime.Now.ToString("MMM").ToUpper();
            int currentCounter = GetMonthCounter();
            return currentMonthPrefix + currentCounter.ToString("D4");
        }
        // Veritabanından bankaları alıp DataGridView'e yükleme
        internal void BankalariYukle()
        {
            string connectionString = "Data Source=veresiye.db;Version=3;";
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    // Banka adlarını çekmek için sorgu
                    string queryBankalar = "SELECT DISTINCT BankaAdi FROM Gunsonu ORDER BY BankaAdi";
                    using (SQLiteCommand cmdBankalar = new SQLiteCommand(queryBankalar, conn))
                    {
                        using (SQLiteDataReader reader = cmdBankalar.ExecuteReader())
                        {
                            bankaComboBox.Items.Clear();
                            bankaComboBox.Items.Add("Tümü");
                            while (reader.Read())
                            {
                                string bankaAdi = reader["BankaAdi"].ToString();
                                bankaComboBox.Items.Add(bankaAdi);
                            }
                            if (bankaComboBox.Items.Count == 1) // sadece "Tümü" varsa
                            {
                                bankaComboBox.Items.Add("Seçiniz");
                            }
                            // Seçim önceden yapılmış mı kontrol et
                            if (bankaComboBox.SelectedIndex < 0)
                            {
                                bankaComboBox.SelectedIndex = 0;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Banka bilgileri yüklenirken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        public void hareketsil_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    var selectedRow = dataGridView1.SelectedRows[0];
                    if (selectedRow.Cells["GunsonuID"].Value != null)
                    {
                        var gunsonuID = selectedRow.Cells["GunsonuID"].Value;
                        DialogResult result = MessageBox.Show(
                            "Bu kaydı silmek istediğinizden emin misiniz?",
                            "Silme Onayı",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question
                        );
                        if (result == DialogResult.Yes)
                        {
                            string connectionString = "Data Source=veresiye.db;Version=3;";
                            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                            {
                                conn.Open();
                                string deleteQuery = "DELETE FROM Gunsonu WHERE GunsonuID = @GunsonuID";
                                using (SQLiteCommand cmd = new SQLiteCommand(deleteQuery, conn))
                                {
                                    cmd.Parameters.AddWithValue("@GunsonuID", gunsonuID);
                                    int affectedRows = cmd.ExecuteNonQuery();
                                    if (affectedRows > 0)
                                    {
                                        MessageBox.Show(
                                            "Seçilen kayıt başarıyla silindi.",
                                            "Başarılı",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Information
                                        );
                                        // ComboBox için bankaları güncelle
                                        BankalariYukle();
                                        // DataGridView'i güncelle - mevcut filtreleri koruyarak!
                                        LoadData(currentPage);
                                        // Hesaplamaları güncelle
                                        HesaplamalariYukle();
                                        // Sayfalama kontrollerini güncelle
                                        UpdatePaginationControls();
                                    }
                                    else
                                    {
                                        MessageBox.Show(
                                            "Silinecek kayıt bulunamadı.",
                                            "Bilgi",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Information
                                        );
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show(
                            "Seçilen satırda geçerli bir ID yok.",
                            "Uyarı",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                    }
                }
                else
                {
                    MessageBox.Show(
                        "Lütfen silinecek bir satır seçin.",
                        "Uyarı",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Silme işlemi sırasında hata oluştu: " + ex.Message,
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
        public void hareketduzenle_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    var selectedRow = dataGridView1.SelectedRows[0];
                    string gunsonuID = selectedRow.Cells["GunsonuID"].Value.ToString();
                    string bankaAdi = selectedRow.Cells["BankaAdi"].Value.ToString();
                    string cihazAdi = selectedRow.Cells["CihazAdi"].Value.ToString();
                    string valor = selectedRow.Cells["Valör"].Value.ToString();
                    // Tutar decimal'e çevir ve InvariantCulture ile string'e geri çevir
                    decimal tutarDecimal = Convert.ToDecimal(selectedRow.Cells["Tutar"].Value);
                    string tutar = tutarDecimal.ToString(CultureInfo.InvariantCulture);
                    DateTime islemTarihi = Convert.ToDateTime(selectedRow.Cells["IslemTarihi"].Value);

                    using (GunSonuForm gunSonuForm = new GunSonuForm())
                    {
                        // Form özelliklerini tek bir metodla ayarla
                        gunSonuForm.SetupForEditing(gunsonuID, bankaAdi, cihazAdi, valor, tutar, islemTarihi);

                        // Formu göster
                        gunSonuForm.ShowDialog();

                        // Form kapandıktan sonra verileri güncelle
                        BankalariYukle();
                        HesaplamalariYukle();
                        LoadData(currentPage);
                    }
                }
                else
                {
                    MessageBox.Show(
                        "Lütfen düzenlenecek bir satır seçin.",
                        "Uyarı",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Düzenleme işlemi sırasında hata oluştu: " + ex.Message,
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        public dynamic GetGunSonuDataByID(string gunSonuID)
        {
            string connectionString = "Data Source=veresiye.db;Version=3;";
            string query = "SELECT GunsonuID, BankaAdi, CihazAdi, Valör, Tutar, IslemTarihi FROM Gunsonu WHERE GunsonuID = @GunSonuID";
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@GunSonuID", gunSonuID);
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string valorStr = reader["Valör"].ToString();
                            return new
                            {
                                GunSonuID = reader["GunsonuID"].ToString(),
                                BankaAdi = reader["BankaAdi"].ToString(),
                                CihazAdi = reader["CihazAdi"].ToString(),
                                Valör = valorStr,
                                Tutar = reader["Tutar"].ToString(),
                                IslemTarihi = Convert.ToDateTime(reader["IslemTarihi"])
                            };
                        }
                    }
                }
            }
            return null;
        }
        internal void HesaplamalariYukle()
        {
            string connectionString = "Data Source=veresiye.db;Version=3;";
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    // Bugün hesaba geçecek tutar
                    using (SQLiteCommand cmd = new SQLiteCommand(
                        "SELECT SUM(G.Tutar * (1 - B.KesintiOrani / 100)) " +
                        "FROM Gunsonu G JOIN Banka B ON G.BankaAdi = B.BankaAdi " +
                        "WHERE DATE(G.GececegiTarih) = DATE('now');", conn))
                    {
                        object result = cmd.ExecuteScalar();
                        decimal bugunGecenTutar = (result != DBNull.Value && result != null)
                            ? Convert.ToDecimal(result)
                            : 0;
                        lblBugunGecen.Text = "Bugün Hesaba Geçecek: " + bugunGecenTutar.ToString("N2") + " ₺";
                    }
                    // Yarın hesaba geçecek tutar
                    using (SQLiteCommand cmd = new SQLiteCommand(
                        "SELECT SUM(G.Tutar * (1 - B.ErkenBozumOrani / 100)) " +
                        "FROM Gunsonu G JOIN Banka B ON G.BankaAdi = B.BankaAdi " +
                        "WHERE DATE(G.GececegiTarih) = DATE('now', '+1 day');", conn))
                    {
                        object result = cmd.ExecuteScalar();
                        decimal yarinGecenTutar = (result != DBNull.Value && result != null)
                            ? Convert.ToDecimal(result)
                            : 0;
                        lblYarinGecen.Text = "Yarın Hesaba Geçecek: " + yarinGecenTutar.ToString("N2") + " ₺";
                    }
                    // Toplam banka blokesi
                    using (SQLiteCommand cmd = new SQLiteCommand(
                        "SELECT SUM(G.Tutar) " +
                        "FROM Gunsonu G WHERE DATE(G.GececegiTarih) >= DATE('now');", conn))
                    {
                        object result = cmd.ExecuteScalar();
                        decimal toplamBloke = (result != DBNull.Value && result != null)
                            ? Convert.ToDecimal(result)
                            : 0;
                        lblToplamBloke.Text = "Toplam Banka Bloke: " + toplamBloke.ToString("N2") + " ₺";
                    }
                    // Toplam banka cirosu
                    using (SQLiteCommand cmd = new SQLiteCommand(
                        "SELECT SUM(G.Tutar) FROM Gunsonu G;", conn))
                    {
                        object result = cmd.ExecuteScalar();
                        decimal toplamCiro = (result != DBNull.Value && result != null)
                            ? Convert.ToDecimal(result)
                            : 0;
                        lblToplamCiro.Text = "Toplam Banka Ciro: " + toplamCiro.ToString("N2") + " ₺";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hesaplamalar yüklenirken hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        public void excelaktar_Click(object sender, EventArgs e)
        {
            try
            {
                // Excel uygulamasını başlat
                var excelApp = new Microsoft.Office.Interop.Excel.Application();
                excelApp.Visible = true;
                var workBook = excelApp.Workbooks.Add();
                var workSheet = (Microsoft.Office.Interop.Excel.Worksheet)workBook.Sheets[1];
                // Çalışma sayfasının adını ayarla
                string sheetName = "Pos Takip İşlemleri " + DateTime.Now.ToString("MMMM yyyy");
                workSheet.Name = sheetName;
                // Başlıkları aktar
                for (int col = 0; col < dataGridView1.Columns.Count; col++)
                {
                    workSheet.Cells[1, col + 1] = dataGridView1.Columns[col].HeaderText;
                    workSheet.Cells[1, col + 1].Font.Bold = true;
                    workSheet.Cells[1, col + 1].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                }
                // Verileri aktar
                for (int row = 0; row < dataGridView1.Rows.Count; row++)
                {
                    for (int col = 0; col < dataGridView1.Columns.Count; col++)
                    {
                        if (dataGridView1.Rows[row].Cells[col].Value != null)
                        {
                            string cellValue = dataGridView1.Rows[row].Cells[col].Value.ToString();
                            // Tarih sütunları için özel işlem
                            if (dataGridView1.Columns[col].Name == "IslemTarihi" ||
                                dataGridView1.Columns[col].Name == "GececegiTarih")
                            {
                                DateTime dateValue;
                                if (DateTime.TryParse(cellValue, out dateValue))
                                {
                                    workSheet.Cells[row + 2, col + 1] = dateValue.ToString("dd.MM.yyyy");
                                }
                                else
                                {
                                    workSheet.Cells[row + 2, col + 1] = cellValue;
                                }
                            }
                            else
                            {
                                workSheet.Cells[row + 2, col + 1] = cellValue;
                            }
                        }
                    }
                }
                // Tarih formatlarını ayarla
                int islemTarihiIndex = FindColumnIndex(dataGridView1, "IslemTarihi");
                int gececegiTarihIndex = FindColumnIndex(dataGridView1, "GececegiTarih");
                if (islemTarihiIndex >= 0)
                {
                    workSheet.Columns[islemTarihiIndex + 1].NumberFormat = "DD.MM.YYYY";
                }
                if (gececegiTarihIndex >= 0)
                {
                    workSheet.Columns[gececegiTarihIndex + 1].NumberFormat = "DD.MM.YYYY";
                }
                // Sütun genişliklerini ayarla
                for (int col = 0; col < dataGridView1.Columns.Count; col++)
                {
                    workSheet.Columns[col + 1].AutoFit();
                }
                // Kenarlıkları ekle
                var range = workSheet.Range["A1", workSheet.Cells[dataGridView1.Rows.Count + 1, dataGridView1.Columns.Count]];
                range.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                // Dosyayı kaydet
                string fileName = $"Pos_Takip_Islemleri_{DateTime.Now.ToString("MMMM_yyyy")}.xlsx";
                workBook.SaveAs(fileName);
                MessageBox.Show(
                    "Veriler başarıyla Excel'e aktarıldı.",
                    "Başarılı",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Excel'e aktarırken bir hata oluştu: " + ex.Message,
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
        // DataGridView sütun indeksi bulma yardımcı metodu
        public int FindColumnIndex(DataGridView dgv, string columnName)
        {
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                if (dgv.Columns[i].Name == columnName)
                {
                    return i;
                }
            }
            return -1;
        }
        public void filtrele_Click(object sender, EventArgs e)
        {
            try
            {
                // Seçilen banka ve tarih aralığını al
                selectedBank = bankaComboBox.SelectedItem?.ToString() ?? "Tümü";
                startDate = startDatePicker.Value;
                endDate = endDatePicker.Value;

                // Tarihleri kontrol et
                if (startDate > endDate)
                {
                    MessageBox.Show(
                        "Başlangıç tarihi, bitiş tarihinden sonra olamaz.",
                        "Tarih Hatası",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                // Sayfa numarasını başa al
                currentPage = 1;

                // Toplam sayfa sayısını tekrar hesapla
                int totalRows = GetTotalRowCount();
                totalPages = (int)Math.Ceiling(totalRows / (double)pageSize);

                // Filtrelenmiş veriyi yükle
                LoadData(currentPage);
                UpdatePaginationControls();

                // Kullanıcıya bilgilendirme mesajı göster
                string message = $"Filtre uygulandı.\n" +
                                 $"Banka: {selectedBank}\n" +
                                 $"Tarih Aralığı: {startDate.ToShortDateString()} - {endDate.ToShortDateString()}\n" +
                                 $"Toplam Kayıt: {totalRows}, Sayfa: {currentPage}/{totalPages}";

                MessageBox.Show(
                    message,
                    "Filtre Uygulandı",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Filtreleme sırasında hata oluştu: " + ex.Message,
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
        public void oncekisayfa_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                Console.WriteLine($"Önceki sayfa düğmesine tıklandı. Yeni sayfa: {currentPage}");
                LoadData(currentPage);
                UpdatePaginationControls();
            }
        }

        public void sonrakisayfa_Click(object sender, EventArgs e)
        {
            // Toplam satır sayısını tekrar kontrol et
            int totalRows = GetTotalRowCount();
            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

            if (currentPage < totalPages)
            {
                currentPage++;
                Console.WriteLine($"Sonraki sayfa düğmesine tıklandı. Yeni sayfa: {currentPage}");
                LoadData(currentPage);
                UpdatePaginationControls();
            }
        }
        internal void SilGececegiTarihiGecmisVeriler()
        {
            string connectionString = "Data Source=veresiye.db;Version=3;";
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    string bugunTarihi = DateTime.Now.ToString("yyyy-MM-dd");
                    // GececegiTarih'i geçmiş olan verileri silme
                    string deleteQuery = "DELETE FROM Gunsonu WHERE DATE(GececegiTarih) < @BugunTarihi";
                    using (SQLiteCommand cmd = new SQLiteCommand(deleteQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@BugunTarihi", bugunTarihi);
                        int etkilenenSatir = cmd.ExecuteNonQuery();
                        if (etkilenenSatir > 0)
                        {
                            MessageBox.Show(
                                $"Geçmiş tarihli {etkilenenSatir} kayıt başarıyla silindi.",
                                "Bilgi",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                            );
                        }
                        else
                        {
                            MessageBox.Show(
                                "Silinecek geçmiş tarihli kayıt bulunamadı.",
                                "Bilgi",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                            );
                        }
                        // Verileri güncelle
                        BankalariYukle();
                        HesaplamalariYukle();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Geçmiş tarihli veriler silinirken hata oluştu: " + ex.Message,
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
        public void tumunusil_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Tüm veriler silinecek. Bu işlem geri alınamaz. Emin misiniz?",
                "Silme Onayı",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );
            if (result == DialogResult.Yes)
            {
                string connectionString = "Data Source=veresiye.db;Version=3;";
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        string deleteQuery = "DELETE FROM Gunsonu";
                        using (SQLiteCommand cmd = new SQLiteCommand(deleteQuery, conn))
                        {
                            cmd.ExecuteNonQuery();
                            MessageBox.Show(
                                "Tüm veriler başarıyla silindi.",
                                "Başarılı",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                            );
                            dataGridView1.DataSource = null;
                            HesaplamalariYukle();
                            BankalariYukle();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            "Veri silinirken hata oluştu: " + ex.Message,
                            "Hata",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                    }
                }
            }
        }
    }
}