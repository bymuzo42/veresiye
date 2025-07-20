using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;

namespace Veresiye2025
{
    public partial class Karttakip : Form
    {
        public string duzenlemeModu = ""; // Düzenleme modu için global değişken
        // Form taşıma işlemleri için değişkenler
        public bool isDragging = false;
        public Point dragStartPoint;

        public Karttakip()
        {
            InitializeComponent();

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
            btnClose.Click += btnClose_Click;
            btnKaydet.Click += btnKaydet_Click;
            btnAlarm.Click += btnAlarm_Click;
            btnTumunuSil.Click += btnTumunuSil_Click;

            // ContextMenuStrip'i burada oluşturuyoruz
            ToolStripMenuItem editItem = new ToolStripMenuItem("Seçili Kartı Düzenle");
            editItem.Click += EditItem_Click;
            contextMenu.Items.Add(editItem);

            ToolStripMenuItem deleteItem = new ToolStripMenuItem("Seçili Kartı Sil");
            deleteItem.Click += DeleteItem_Click;
            contextMenu.Items.Add(deleteItem);

            // Ayırıcı çizgi ekle
            contextMenu.Items.Add(new ToolStripSeparator());

            // Alarm kur öğesi ekle
            ToolStripMenuItem alarmItem = new ToolStripMenuItem("Kart İçin Alarm Kur");
            alarmItem.Click += AlarmItem_Click;
            contextMenu.Items.Add(alarmItem);

            // MouseUp olayını ekle
            dataGridView1.MouseUp += DataGridView1_MouseUp;

            // Form olayları
            this.Load += Karttakip_Load;
            this.KeyPreview = true;
            this.KeyDown += Karttakip_KeyDown;
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

        // Buton event handler'ları
        public void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void btnClose_MouseEnter(object sender, EventArgs e)
        {
            btnClose.FillColor = Color.FromArgb(232, 17, 35);
        }

        public void btnClose_MouseLeave(object sender, EventArgs e)
        {
            btnClose.FillColor = Color.Transparent;
        }

        public void Karttakip_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        // Form yüklenince kartları yükle
        // Form yüklenince kartları yükle
        public void Karttakip_Load(object sender, EventArgs e)
        {
            try
            {
                // Veritabanı ve tabloyu kontrol et, gerekirse tabloyu oluştur
                KrediKartTabloOlustur();

                // Kartları yükle
                KartlariYukle();

                // DataGridView görünümünü profesyonelleştir
                EnhanceDataGridViewAppearance();

                // Bugünün tarihini al
                DateTime today = DateTime.Today;

                // "sonkullanmatarih" DateTimePicker kontrolüne bugünün tarihini MinDate olarak ayarla
                datePickerSonKullanma.MinDate = today;
                datePickerSonKullanma.Value = today;
                datePickerHesapKesim.Value = today;

                // Tooltip'leri ayarla
                SetupTooltips();

                // TextBox event handler'larını ekle
                txtBankaAdi.KeyPress += TxtBankaAdi_KeyPress;
                txtKartNo.KeyPress += TxtKartNo_KeyPress;
                txtKartNo.TextChanged += TxtKartNo_TextChanged;
                txtCVC.KeyPress += TxtCVC_KeyPress;
                txtCVC.TextChanged += TxtCVC_TextChanged;
                txtAlarmGun.KeyPress += TxtAlarmGun_KeyPress;
                txtAlarmGun.Leave += TxtAlarmGun_Leave;
                datePickerSonKullanma.ValueChanged += DatePickerSonKullanma_ValueChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Form yüklenirken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // DataGridView görünümünü profesyonelleştirmek için yeni metod
        public void EnhanceDataGridViewAppearance()
        {
            // Temizlik için önce EnableHeadersVisualStyles'ı false yapın
            dataGridView1.EnableHeadersVisualStyles = false;

            // 1. Sütun başlıklarını daha profesyonel hale getir
            if (dataGridView1.Columns.Contains("Banka_Adi"))
                dataGridView1.Columns["Banka_Adi"].HeaderText = "Banka Adı";
            if (dataGridView1.Columns.Contains("KartNo"))
                dataGridView1.Columns["KartNo"].HeaderText = "Kart Numarası";
            if (dataGridView1.Columns.Contains("SonKullanmaTarihi"))
                dataGridView1.Columns["SonKullanmaTarihi"].HeaderText = "Son Kullanma";
            if (dataGridView1.Columns.Contains("Cvc"))
                dataGridView1.Columns["Cvc"].HeaderText = "CVC";
            if (dataGridView1.Columns.Contains("HesapKesimTarihi"))
                dataGridView1.Columns["HesapKesimTarihi"].HeaderText = "Hesap Kesim";
            if (dataGridView1.Columns.Contains("KartLimit"))
                dataGridView1.Columns["KartLimit"].HeaderText = "Limit";
            if (dataGridView1.Columns.Contains("AlarmGunSayisi"))
                dataGridView1.Columns["AlarmGunSayisi"].HeaderText = "Alarm (Gün)";

            // 2. Modern görünüm için başlık stilini ayarla
            Color headerBackColor = Color.FromArgb(47, 54, 64); // Koyu gri/siyah
            Color headerForeColor = Color.White;

            // Header (Başlık) ayarları
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = headerBackColor;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = headerForeColor;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10F);
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.ColumnHeadersHeight = 45; // Daha yüksek başlıklar
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridView1.ColumnHeadersDefaultCellStyle.Padding = new Padding(5, 0, 5, 0);

            // Guna UI ThemeStyle ayarları
            this.dataGridView1.ThemeStyle.HeaderStyle.BackColor = headerBackColor;
            this.dataGridView1.ThemeStyle.HeaderStyle.ForeColor = headerForeColor;
            this.dataGridView1.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI Semibold", 10F);
            this.dataGridView1.ThemeStyle.HeaderStyle.Height = 45;
            this.dataGridView1.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.Single;

            // 3. Seçili satır rengini daha profesyonel bir renkle değiştir
            Color selectionBackColor = Color.FromArgb(231, 240, 253); // Açık mavi
            Color selectionForeColor = Color.Black;

            dataGridView1.DefaultCellStyle.SelectionBackColor = selectionBackColor;
            dataGridView1.DefaultCellStyle.SelectionForeColor = selectionForeColor;

            // ThemeStyle için de aynı renkleri uygula
            this.dataGridView1.ThemeStyle.RowsStyle.SelectionBackColor = selectionBackColor;
            this.dataGridView1.ThemeStyle.RowsStyle.SelectionForeColor = selectionForeColor;

            // 4. Genel grid görünümünü iyileştir
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.GridColor = Color.FromArgb(224, 224, 224); // Açık gri grid çizgileri

            // 5. Satır yüksekliğini biraz artır
            dataGridView1.RowTemplate.Height = 38;
            this.dataGridView1.ThemeStyle.RowsStyle.Height = 38;

            // 6. Alternatif satır rengini ayarla
            Color alternatingRowColor = Color.FromArgb(247, 248, 249); // Çok açık gri
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = alternatingRowColor;
            this.dataGridView1.ThemeStyle.AlternatingRowsStyle.BackColor = alternatingRowColor;

            // 7. DataGridView üzerine gelince seçme işaretini standart yerine el işareti yap (opsiyonel)
            dataGridView1.Cursor = Cursors.Hand;

            // 8. Sütun genişliklerini otomatik ayarla (isteğe bağlı)
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Context menüsünü tekrar ata
            dataGridView1.ContextMenuStrip = contextMenu;
        }

        public void SetupTooltips()
        {
            tooltip.SetToolTip(txtBankaAdi, "Banka adını giriniz");
            tooltip.SetToolTip(txtKartNo, "16 haneli kart numarasını giriniz");
            tooltip.SetToolTip(datePickerSonKullanma, "Kartın son kullanma tarihini seçiniz");
            tooltip.SetToolTip(txtCVC, "Kartın arkasındaki 3 haneli güvenlik kodunu giriniz");
            tooltip.SetToolTip(datePickerHesapKesim, "Hesap kesim tarihini seçiniz");
            tooltip.SetToolTip(txtLimit, "Kart limitini giriniz");
            tooltip.SetToolTip(txtAlarmGun, "Kartın son kullanım tarihi yaklaştığında, kaç gün önce uyarı almak istediğinizi giriniz (1-30)");
        }

        // Veritabanı tablosunu oluştur
        public void KrediKartTabloOlustur()
        {
            string connectionString = "Data Source=veresiye.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    // Tablo oluşturma sorgusu
                    string createTableQuery = @"
                    CREATE TABLE IF NOT EXISTS KrediKartlari (
                        ID INTEGER PRIMARY KEY AUTOINCREMENT,
                        Banka_Adi TEXT NOT NULL,
                        KartNo TEXT NOT NULL,
                        SonKullanmaTarihi TEXT NOT NULL,
                        Cvc TEXT NOT NULL,
                        HesapKesimTarihi TEXT NOT NULL,
                        KartLimit REAL NOT NULL,
                        AlarmGunSayisi INTEGER NOT NULL,
                        OlusturulmaTarihi TEXT DEFAULT CURRENT_TIMESTAMP,
                        GuncellenmeTarihi TEXT DEFAULT CURRENT_TIMESTAMP
                    )";
                    using (SQLiteCommand command = new SQLiteCommand(createTableQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veritabanı tablosu oluşturulurken hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Veritabanından kartları çekip DataGridView'e yükleyen metod
        internal void KartlariYukle()
        {
            // Veritabanı bağlantı dizesi
            string connectionString = "Data Source=veresiye.db;Version=3;";
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    // Kartları sorgulama
                    string query = "SELECT Banka_Adi, KartNo, SonKullanmaTarihi, Cvc, HesapKesimTarihi, KartLimit, AlarmGunSayisi FROM KrediKartlari";
                    SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    // Veriyi çek ve DataTable'a yükle
                    dataAdapter.Fill(dataTable);
                    // DataGridView'e verileri bağla
                    dataGridView1.DataSource = dataTable;
                    // Kolonları gizle (gerekirse)
                    if (dataGridView1.Columns.Contains("OlusturulmaTarihi"))
                        dataGridView1.Columns["OlusturulmaTarihi"].Visible = false;
                    if (dataGridView1.Columns.Contains("GuncellenmeTarihi"))
                        dataGridView1.Columns["GuncellenmeTarihi"].Visible = false;
                    // DataSource'tan sonra ContextMenuStrip'i tekrar ata
                    dataGridView1.ContextMenuStrip = contextMenu;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veritabanı hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Sağ tık menüsünden düzenleme işlemi
        public void EditItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    // Seçili satırdan kart bilgilerini al
                    string bankaAdi = dataGridView1.SelectedRows[0].Cells["Banka_Adi"].Value.ToString();
                    string kartNo = dataGridView1.SelectedRows[0].Cells["KartNo"].Value.ToString();
                    string cvc = dataGridView1.SelectedRows[0].Cells["Cvc"].Value.ToString();
                    DateTime sonKullanmaTarihi = Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells["SonKullanmaTarihi"].Value);
                    DateTime hesapKesimTarihi = Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells["HesapKesimTarihi"].Value);
                    decimal kartLimit = Convert.ToDecimal(dataGridView1.SelectedRows[0].Cells["KartLimit"].Value);
                    int alarmGunSayisi = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["AlarmGunSayisi"].Value);

                    // Kart bilgilerini form alanlarına doldur
                    txtBankaAdi.Text = bankaAdi;
                    txtKartNo.Text = kartNo;
                    txtCVC.Text = cvc;
                    datePickerSonKullanma.Value = sonKullanmaTarihi;
                    datePickerHesapKesim.Value = hesapKesimTarihi;
                    txtLimit.Text = kartLimit.ToString();
                    txtAlarmGun.Text = alarmGunSayisi.ToString();

                    // Düzenleme modunu etkinleştir
                    duzenlemeModu = kartNo; // Düzenlenen kartın orijinal numarasını sakla

                    // Kaydet butonunun metnini değiştir
                    btnKaydet.Text = "Güncelle";

                    // Alarm butonunu gizle
                    btnAlarm.Visible = false;

                    // İptal butonu ekle
                    // İptal butonunun konumunu alarm butonunun konumu ile aynı yap
                    Point alarmLocation = btnAlarm.Location;

                    if (!panel1.Controls.ContainsKey("btnIptal"))
                    {
                        Guna.UI2.WinForms.Guna2Button iptalButon = new Guna.UI2.WinForms.Guna2Button();
                        iptalButon.Name = "btnIptal";
                        iptalButon.Text = "İptal";
                        iptalButon.BorderRadius = 10;
                        iptalButon.Location = alarmLocation; // Alarm butonu ile aynı konumu kullan
                        iptalButon.Size = btnAlarm.Size; // Alarm butonu ile aynı boyutu kullan
                        iptalButon.FillColor = Color.Gray;
                        iptalButon.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                        iptalButon.ForeColor = Color.White;
                        iptalButon.Click += IptalButon_Click;
                        iptalButon.Visible = true;
                        iptalButon.BringToFront(); // Diğer kontrollerden üstte olduğundan emin ol
                        panel1.Controls.Add(iptalButon);

                        // Debug için konum bilgisi
                        Console.WriteLine($"İptal butonu oluşturuldu: Görünür = {iptalButon.Visible}, Konum = {iptalButon.Location}");
                    }
                    else
                    {
                        Control iptalButon = panel1.Controls["btnIptal"];
                        iptalButon.Location = alarmLocation;
                        iptalButon.Size = btnAlarm.Size;
                        iptalButon.Visible = true;
                        iptalButon.BringToFront();

                        // Debug için konum bilgisi
                        Console.WriteLine($"Mevcut iptal butonu güncellendi: Görünür = {iptalButon.Visible}, Konum = {iptalButon.Location}");
                    }

                    // Kullanıcıya düzenleme modunda olduğunu belirt
                    MessageBox.Show("Kart düzenleme modu aktif. Bilgileri düzenleyip 'Güncelle' butonuna tıklayın veya 'İptal' ile düzenlemeyi iptal edin.",
                                   "Düzenleme Modu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Kart bilgilerini yükleme hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void IptalButon_Click(object sender, EventArgs e)
        {
            // Düzenleme modunu kapat
            duzenlemeModu = "";

            // Formu temizle
            FormuTemizle();

            // Kaydet butonunu normal haline getir
            btnKaydet.Text = "Kaydet";

            // İptal butonunu gizle
            if (panel1.Controls.ContainsKey("btnIptal"))
            {
                panel1.Controls["btnIptal"].Visible = false;
            }

            // Alarm butonunu tekrar göster
            btnAlarm.Visible = true;
        }

        public void FormuTemizle()
        {
            txtBankaAdi.Clear();
            txtKartNo.Clear();
            txtCVC.Clear();
            txtLimit.Clear();
            txtAlarmGun.Clear();
            datePickerSonKullanma.Value = DateTime.Today;
            datePickerHesapKesim.Value = DateTime.Today;
        }

        // Sağ tık menüsünden silme işlemi
        public void DeleteItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Kullanıcıya onay mesajı göster
                DialogResult result = MessageBox.Show("Seçili kart silinecek. Emin misiniz?",
                                                     "Silme Onayı",
                                                     MessageBoxButtons.YesNo,
                                                     MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    // Seçili satırdan kart numarasını al
                    string kartNo = dataGridView1.SelectedRows[0].Cells["KartNo"].Value.ToString();
                    // Silme işlemini gerçekleştir
                    KartSil(kartNo);
                }
            }
            else
            {
                MessageBox.Show("Lütfen silmek istediğiniz bir kart seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // DataGridView sağ tık işlemi
        public void DataGridView1_MouseUp(object sender, MouseEventArgs e)
        {
            // Sağ tık yapıldığında
            if (e.Button == MouseButtons.Right)
            {
                // Tıklanan satırı seç
                DataGridView.HitTestInfo hitTest = dataGridView1.HitTest(e.X, e.Y);
                if (hitTest.RowIndex >= 0)
                {
                    dataGridView1.ClearSelection();
                    dataGridView1.Rows[hitTest.RowIndex].Selected = true;
                    // Menüyü göster
                    contextMenu.Show(dataGridView1, e.Location);
                }
            }
        }

        // Belirli bir kart numarasına sahip kartı silme metodu
        public void KartSil(string kartNo)
        {
            string connectionString = "Data Source=veresiye.db;Version=3;";
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM KrediKartlari WHERE KartNo = @KartNo";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@KartNo", kartNo);
                        int affectedRows = cmd.ExecuteNonQuery();
                        if (affectedRows > 0)
                        {
                            MessageBox.Show("Kart başarıyla silindi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // DataGridView'i yenile
                            KartlariYukle();
                        }
                        else
                        {
                            MessageBox.Show("Kart silinirken bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veritabanı hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // TextBox ve DateTimePicker olayları
        public void TxtBankaAdi_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Eğer girilen karakter bir harfse (A-Z veya a-z), boşluksa (Space) veya Backspace tuşu ise geçerli olsun
            if (char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Space || e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false; // Karakter geçerli, işleme devam et
            }
            else
            {
                e.Handled = true; // Karakter geçerli değil, işleme engel ol
            }
        }

        public void TxtKartNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Eğer girilen karakter bir rakam, '-' karakteri veya Backspace tuşu ise geçerli olsun
            if (char.IsDigit(e.KeyChar) || e.KeyChar == '-' || e.KeyChar == (char)Keys.Back)
            {
                // Kart numarasının uzunluğunu kontrol et
                string currentText = txtKartNo.Text;
                // Eğer uzunluk 19 karakteri geçiyorsa (16 haneli + 3 tane '-')
                if (currentText.Length >= 19 && e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true; // Fazla karakter girişine izin verme
                }
                else
                {
                    e.Handled = false; // Geçerli karakter
                }
            }
            else
            {
                // Harf veya başka bir karakter girmeye çalıştığında engelle
                e.Handled = true;
            }
        }

        public void TxtKartNo_TextChanged(object sender, EventArgs e)
        {
            // Başlangıçta TextBox'taki metni al
            string text = txtKartNo.Text;
            // Eğer metin 4 karakterden fazlaysa
            if (text.Length == 4 || text.Length == 9 || text.Length == 14)
            {
                // Eğer '-' yoksa, ekle
                if (text.Length > 0 && text[text.Length - 1] != '-')
                {
                    txtKartNo.Text = text + "-";
                    txtKartNo.SelectionStart = txtKartNo.Text.Length;  // Cursor'ü sona taşı
                }
            }
        }

        public void DatePickerSonKullanma_ValueChanged(object sender, EventArgs e)
        {
            // Eğer seçilen tarih bugünden önceyse
            if (datePickerSonKullanma.Value < DateTime.Today)
            {
                // Hata mesajı göster
                MessageBox.Show("Geçmiş tarih seçilemez. Lütfen son kullanma tarihini kontrol edin!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Seçilen tarihi bugüne geri ayarla
                datePickerSonKullanma.Value = DateTime.Today;
            }
        }

        public void TxtCVC_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Eğer girilen karakter bir rakam veya backspace veya delete tuşu ise geçerli olsun
            if (char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Delete)
            {
                e.Handled = false; // Geçerli karakter
            }
            else
            {
                e.Handled = true; // Geçersiz karakter, engelle
            }
        }

        public void TxtCVC_TextChanged(object sender, EventArgs e)
        {
            // Eğer girilen metin 3 haneli değilse, işlemi engelle
            if (txtCVC.Text.Length > 3)
            {
                // 4. karakter girildiyse, sadece ilk 3 karakteri kabul et
                txtCVC.Text = txtCVC.Text.Substring(0, 3);
                txtCVC.SelectionStart = txtCVC.Text.Length; // Cursor'ü sona taşı
            }
        }

        public void TxtAlarmGun_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Eğer girilen karakter bir rakam veya backspace tuşu ise geçerli olsun
            if (char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back)
            {
                // Eğer TextBox'taki karakter sayısı 2'yi geçiyorsa yeni karakter girmeyi engelle
                if (txtAlarmGun.Text.Length >= 2 && e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true; // 2 karakterden fazlasını engelle
                }
                else
                {
                    e.Handled = false; // Geçerli rakam veya tuşa izin ver
                }
            }
            else
            {
                // Harf veya başka bir tuş girildiğinde engelle
                e.Handled = true;
            }
        }

        public void TxtAlarmGun_Leave(object sender, EventArgs e)
        {
            // Girilen değeri integer'a çevir
            if (int.TryParse(txtAlarmGun.Text, out int alarmGunSayisi))
            {
                // Eğer girilen değer 30'dan büyükse, uyarı göster
                if (alarmGunSayisi > 30)
                {
                    // Hata mesajı göster
                    MessageBox.Show("Alarm gün sayısı 30'dan büyük olamaz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // TextBox'ı 30 olarak ayarla
                    txtAlarmGun.Text = "30";
                }
            }
            else
            {
                // Eğer geçerli bir rakam girilmediyse, hata mesajı göster
                MessageBox.Show("Lütfen geçerli bir rakam giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Buton tıklama olayları
        public void btnKaydet_Click(object sender, EventArgs e)
        {
            // Kullanıcıdan alınan verileri al
            string bankaAdi = txtBankaAdi.Text;
            string kartNo = txtKartNo.Text;
            string sonKullanmaTarihi = datePickerSonKullanma.Value.ToString("yyyy-MM-dd"); // Tarih formatı
            string cvc = txtCVC.Text;
            string hesapKesimTarihi = datePickerHesapKesim.Value.ToString("yyyy-MM-dd"); // Tarih formatı
            decimal kartLimit;
            int alarmGunSayisi;

            // Alanları kontrol et: Boş olan varsa hata mesajı ver
            if (string.IsNullOrWhiteSpace(bankaAdi))
            {
                MessageBox.Show("Banka adı boş bırakılamaz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Hata varsa işlemi durdur
            }

            if (string.IsNullOrWhiteSpace(kartNo))
            {
                MessageBox.Show("Kart numarası boş bırakılamaz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Hata varsa işlemi durdur
            }

            // Kart numarasını kontrol et (16 haneli olmalı)
            string temizKartNo = kartNo.Replace("-", "");
            if (temizKartNo.Length != 16)
            {
                MessageBox.Show("Kart numarası 16 haneli olmalıdır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(cvc))
            {
                MessageBox.Show("CVC kodu boş bırakılamaz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Hata varsa işlemi durdur
            }

            // CVC kodunu kontrol et (3 haneli olmalı)
            if (cvc.Length != 3)
            {
                MessageBox.Show("CVC kodu 3 haneli olmalıdır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtLimit.Text) || !decimal.TryParse(txtLimit.Text, out kartLimit))
            {
                MessageBox.Show("Kart limiti boş bırakılamaz ve geçerli bir sayı olmalıdır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Hata varsa işlemi durdur
            }

            if (string.IsNullOrWhiteSpace(txtAlarmGun.Text) || !int.TryParse(txtAlarmGun.Text, out alarmGunSayisi))
            {
                MessageBox.Show("Alarm gün sayısı boş bırakılamaz ve geçerli bir sayı olmalıdır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Hata varsa işlemi durdur
            }

            // Düzenleme modunda mıyız kontrol et
            if (!string.IsNullOrEmpty(duzenlemeModu))
            {
                // Güncelleme işlemi
                string connectionString = "Data Source=veresiye.db;Version=3;";
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        string query = "UPDATE KrediKartlari SET " +
                                       "Banka_Adi = @Banka_Adi, " +
                                       "KartNo = @KartNo, " +
                                       "SonKullanmaTarihi = @SonKullanmaTarihi, " +
                                       "Cvc = @Cvc, " +
                                       "HesapKesimTarihi = @HesapKesimTarihi, " +
                                       "KartLimit = @KartLimit, " +
                                       "AlarmGunSayisi = @AlarmGunSayisi, " +
                                       "GuncellenmeTarihi = CURRENT_TIMESTAMP " +
                                       "WHERE KartNo = @EskiKartNo";
                        using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Banka_Adi", bankaAdi);
                            cmd.Parameters.AddWithValue("@KartNo", kartNo);
                            cmd.Parameters.AddWithValue("@SonKullanmaTarihi", sonKullanmaTarihi);
                            cmd.Parameters.AddWithValue("@Cvc", cvc);
                            cmd.Parameters.AddWithValue("@HesapKesimTarihi", hesapKesimTarihi);
                            cmd.Parameters.AddWithValue("@KartLimit", kartLimit);
                            cmd.Parameters.AddWithValue("@AlarmGunSayisi", alarmGunSayisi);
                            cmd.Parameters.AddWithValue("@EskiKartNo", duzenlemeModu);
                            int affectedRows = cmd.ExecuteNonQuery();
                            if (affectedRows > 0)
                            {
                                MessageBox.Show("Kart bilgileri başarıyla güncellendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Kart güncellenirken bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Veritabanı hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                // Düzenleme modundan çık
                duzenlemeModu = "";
                // Kaydet butonunu normal haline getir
                btnKaydet.Text = "Kaydet";
                // İptal butonunu gizle
                if (panel1.Controls.ContainsKey("btnIptal"))
                {
                    panel1.Controls["btnIptal"].Visible = false;
                }
                // Alarm butonunu tekrar göster
                btnAlarm.Visible = true;
            }
            else
            {
                // Normal kaydetme işlemi
                string connectionString = "Data Source=veresiye.db;Version=3;";
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        string query = "INSERT INTO KrediKartlari (Banka_Adi, KartNo, SonKullanmaTarihi, Cvc, HesapKesimTarihi, KartLimit, AlarmGunSayisi) " +
                                       "VALUES (@Banka_Adi, @KartNo, @SonKullanmaTarihi, @Cvc, @HesapKesimTarihi, @KartLimit, @AlarmGunSayisi)";
                        using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                        {
                            // Parametreleri ekle
                            cmd.Parameters.AddWithValue("@Banka_Adi", bankaAdi);
                            cmd.Parameters.AddWithValue("@KartNo", kartNo);
                            cmd.Parameters.AddWithValue("@SonKullanmaTarihi", sonKullanmaTarihi);
                            cmd.Parameters.AddWithValue("@Cvc", cvc);
                            cmd.Parameters.AddWithValue("@HesapKesimTarihi", hesapKesimTarihi);
                            cmd.Parameters.AddWithValue("@KartLimit", kartLimit);
                            cmd.Parameters.AddWithValue("@AlarmGunSayisi", alarmGunSayisi);
                            // Komutu çalıştır
                            cmd.ExecuteNonQuery();
                        }
                        MessageBox.Show("Kredi kartınız başarıyla kaydedildi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Veritabanı hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            // DataGridView'i yeniden yükle
            KartlariYukle();
            // Tüm alanları sıfırlayalım (boşaltalım)
            FormuTemizle();
        }


        public void btnAlarm_Click(object sender, EventArgs e)
        {
            // Validasyon kontrolü
            if (string.IsNullOrEmpty(txtAlarmGun.Text))
            {
                MessageBox.Show("Lütfen alarm gün sayısını girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kart bilgilerini kontrol et
            if (string.IsNullOrEmpty(txtBankaAdi.Text) || string.IsNullOrEmpty(txtKartNo.Text))
            {
                MessageBox.Show("Banka adı ve kart numarası gereklidir.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Gün sayısını doğrula
            if (!int.TryParse(txtAlarmGun.Text, out int alarmGunSayisi))
            {
                MessageBox.Show("Geçerli bir alarm gün sayısı giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Kart bilgilerini al
                string bankaAdi = txtBankaAdi.Text;
                string kartNo = txtKartNo.Text;
                DateTime hesapKesimTarihi = datePickerHesapKesim.Value;

                // Hesap kesim tarihini baz alarak ödeme için alarm tarihi hesapla 
                // (Kullanıcının girdiği gün sayısı, hesap kesim tarihine eklenecek)
                DateTime odemeTarihi = hesapKesimTarihi.AddDays(alarmGunSayisi);

                // Sabah ve akşam alarmları için tarih belirle
                DateTime sabahAlarmTarihi = odemeTarihi.Date.AddHours(9);
                DateTime aksamAlarmTarihi = odemeTarihi.Date.AddHours(15);

                // Alarm tarihinin geçmiş olup olmadığını kontrol et
                if (odemeTarihi < DateTime.Today)
                {
                    MessageBox.Show("Hesaplanan ödeme tarihi geçmiş bir tarih. Lütfen güncel bir hesap kesim tarihi girin.",
                                   "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Son 4 haneyi al (güvenlik için)
                string sonDortHane = kartNo.Length > 4 ? kartNo.Substring(kartNo.Length - 4) : kartNo;

                // Alarm mesajını oluştur
                string alarmMesaji = $"{bankaAdi} bankasına ait **** **** **** {sonDortHane} numaralı kartın ödeme tarihi geldi. " +
                                    $"Hesap kesim tarihi: {hesapKesimTarihi.ToString("dd.MM.yyyy")}";

                // Alarmları ekle
                bool sabahAlarmEklendi = KrediKartiAlarmEkle(bankaAdi, kartNo, sabahAlarmTarihi, alarmMesaji, "Kredi Kartı", "Normal");
                bool aksamAlarmEklendi = KrediKartiAlarmEkle(bankaAdi, kartNo, aksamAlarmTarihi, alarmMesaji, "Kredi Kartı", "Normal");

                // Her iki alarm da başarıyla eklendiyse bilgi mesajı göster
                if (sabahAlarmEklendi && aksamAlarmEklendi)
                {
                    MessageBox.Show($"{bankaAdi} kartı için hesap kesim tarihinden {alarmGunSayisi} gün sonra " +
                                   $"({odemeTarihi.ToString("dd.MM.yyyy")}) sabah ve akşam olmak üzere iki adet ödeme hatırlatma alarmı kuruldu.",
                                   "Alarm Kuruldu", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // İşlem başarılıysa textbox'ı temizle
                    txtAlarmGun.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Alarm kurulurken bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void btnTumunuSil_Click(object sender, EventArgs e)
        {
            // Kullanıcıya onay mesajı göster
            DialogResult result = MessageBox.Show("Tüm kartlar silinecek. Emin misiniz?",
                                                  "Silme Onayı",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Warning);
            // Eğer kullanıcı "Evet" butonuna tıklarsa, silme işlemini yap
            if (result == DialogResult.Yes)
            {
                // SQLite bağlantısını oluştur
                string connectionString = "Data Source=veresiye.db;Version=3;";
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        // Tüm kartları silen SQL komutunu yaz
                        string query = "DELETE FROM KrediKartlari";
                        using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                        {
                            // Komutu çalıştır
                            cmd.ExecuteNonQuery();
                        }
                        // DataGridView'i yeniden yükle (kartlar silindikten sonra boş bir tablo gösterilsin)
                        KartlariYukle();
                        MessageBox.Show("Tüm kartlar başarıyla silindi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Veritabanı hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                // Kullanıcı "Hayır" dediğinde herhangi bir işlem yapılmaz
                MessageBox.Show("Silme işlemi iptal edildi.", "İptal", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void btnAlarm_Click_1(object sender, EventArgs e)
        {

        }
        //alarm kurma başlıyor
        public bool KrediKartiAlarmEkle(string bankaAdi, string kartNo, DateTime alarmTarihi, string mesaj, string odemeTuru, string onemDerecesi)
        {
            try
            {
                // SQLite bağlantı dizesini oluştur
                string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";

                // Bağlantıyı oluştur ve aç
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Kart numarasının son 4 hanesini al
                    string sonDortHane = kartNo.Replace(" ", "").Replace("-", "");
                    sonDortHane = sonDortHane.Length > 4 ? sonDortHane.Substring(sonDortHane.Length - 4) : sonDortHane;

                    // Cari bilgilerini belirle
                    string cariKodu = "KART-" + sonDortHane;
                    string cariUnvan = bankaAdi + " Kart";

                    // SQL sorgusunu hazırla
                    string query = @"INSERT INTO Alarmlar (
                            cari_kodu, 
                            cari_unvan, 
                            alarm_tarihi, 
                            mesaj, 
                            odeme_turu, 
                            onem_derecesi,
                            bildirildi,
                            durum)
                          VALUES (
                            @cariKodu, 
                            @cariUnvan, 
                            @alarmTarihi, 
                            @mesaj, 
                            @odemeTuru, 
                            @onemDerecesi,
                            0,
                            'Bekliyor')";

                    // SQLite komutunu oluştur ve parametreleri ekle
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@cariKodu", cariKodu);
                        command.Parameters.AddWithValue("@cariUnvan", cariUnvan);
                        command.Parameters.AddWithValue("@alarmTarihi", alarmTarihi.ToString("yyyy-MM-dd HH:mm:ss"));
                        command.Parameters.AddWithValue("@mesaj", mesaj);
                        command.Parameters.AddWithValue("@odemeTuru", odemeTuru);
                        command.Parameters.AddWithValue("@onemDerecesi", onemDerecesi);

                        // Sorguyu çalıştır
                        int affectedRows = command.ExecuteNonQuery();

                        // Eğer kayıt başarılıysa true döndür
                        return affectedRows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kredi kartı alarmı eklenirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public void AlarmItem_Click(object sender, EventArgs e)
        {
            // Seçili kart kontrolü
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen alarm kurmak istediğiniz bir kart seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Seçilen kartın bilgilerini al
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                string bankaAdi = selectedRow.Cells["Banka_Adi"].Value.ToString();
                string kartNo = selectedRow.Cells["KartNo"].Value.ToString();
                DateTime hesapKesimTarihi = Convert.ToDateTime(selectedRow.Cells["HesapKesimTarihi"].Value);

                // Alarm gün sayısını kullanıcıdan iste
                int odemeSuresi = GetOdemeSuresi(bankaAdi, kartNo);

                // İptal edildiyse işlemi sonlandır
                if (odemeSuresi <= 0)
                    return;

                // Hesap kesim tarihini baz alarak ödeme için alarm tarihi hesapla
                DateTime odemeTarihi = hesapKesimTarihi.AddDays(odemeSuresi);

                // Sabah ve akşam alarmları için tarih belirle
                DateTime sabahAlarmTarihi = odemeTarihi.Date.AddHours(9);
                DateTime aksamAlarmTarihi = odemeTarihi.Date.AddHours(15);

                // Alarm tarihinin geçmiş olup olmadığını kontrol et
                if (odemeTarihi < DateTime.Today)
                {
                    MessageBox.Show("Hesaplanan ödeme tarihi geçmiş bir tarih. Lütfen güncel bir kart seçin veya daha kısa bir ödeme süresi belirleyin.",
                                   "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Son 4 haneyi al (güvenlik için)
                string sonDortHane = kartNo.Length > 4 ? kartNo.Substring(kartNo.Length - 4) : kartNo;

                // Alarm mesajını oluştur
                string alarmMesaji = $"{bankaAdi} bankasına ait **** **** **** {sonDortHane} numaralı kartın ödeme tarihi geldi. " +
                                     $"Hesap kesim tarihi: {hesapKesimTarihi.ToString("dd.MM.yyyy")}";

                // Alarmları ekle
                bool sabahAlarmEklendi = KrediKartiAlarmEkle(bankaAdi, kartNo, sabahAlarmTarihi, alarmMesaji, "Kredi Kartı", "Normal");
                bool aksamAlarmEklendi = KrediKartiAlarmEkle(bankaAdi, kartNo, aksamAlarmTarihi, alarmMesaji, "Kredi Kartı", "Normal");

                // Her iki alarm da başarıyla eklendiyse bilgi mesajı göster
                if (sabahAlarmEklendi && aksamAlarmEklendi)
                {
                    MessageBox.Show($"{bankaAdi} kartı için hesap kesim tarihinden {odemeSuresi} gün sonra " +
                                   $"({odemeTarihi.ToString("dd.MM.yyyy")}) sabah ve akşam olmak üzere iki adet ödeme hatırlatma alarmı kuruldu.",
                                   "Alarm Kuruldu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Alarm kurulurken bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Ödeme süresi için kullanıcıdan gün sayısı al
        public int GetOdemeSuresi(string bankaAdi, string kartNo)
        {
            // Varsayılan değer
            int result = 10;

            using (Form inputForm = new Form())
            {
                inputForm.Text = "Ödeme Süresi";
                inputForm.Size = new Size(380, 170);
                inputForm.StartPosition = FormStartPosition.CenterParent;
                inputForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                inputForm.MaximizeBox = false;
                inputForm.MinimizeBox = false;

                Label label = new Label();
                label.Text = $"{bankaAdi} kartı için hesap kesim tarihinden kaç gün sonra ödeme yapmak istiyorsunuz?";
                label.Location = new Point(10, 15);
                label.AutoSize = true;
                label.Width = 350;

                NumericUpDown numInput = new NumericUpDown();
                numInput.Location = new Point(10, 60);
                numInput.Size = new Size(150, 25);
                numInput.Minimum = 1;
                numInput.Maximum = 30;
                numInput.Value = 10; // Varsayılan değer

                Label infoLabel = new Label();
                infoLabel.Text = "Not: Ödeme tarihinde sabah ve akşam olmak üzere iki adet hatırlatma alacaksınız.";
                infoLabel.Location = new Point(10, 90);
                infoLabel.AutoSize = true;
                infoLabel.Font = new Font(infoLabel.Font, FontStyle.Italic);
                infoLabel.ForeColor = Color.Gray;

                Button okButton = new Button();
                okButton.Text = "Tamam";
                okButton.Location = new Point(180, 120);
                okButton.DialogResult = DialogResult.OK;

                Button cancelButton = new Button();
                cancelButton.Text = "İptal";
                cancelButton.Location = new Point(260, 120);
                cancelButton.DialogResult = DialogResult.Cancel;

                inputForm.Controls.Add(label);
                inputForm.Controls.Add(numInput);
                inputForm.Controls.Add(infoLabel);
                inputForm.Controls.Add(okButton);
                inputForm.Controls.Add(cancelButton);

                inputForm.AcceptButton = okButton;
                inputForm.CancelButton = cancelButton;

                if (inputForm.ShowDialog() == DialogResult.OK)
                {
                    result = (int)numInput.Value;
                }
                else
                {
                    result = -1; // İptal edildi
                }
            }

            return result;
        }

        // Gün sayısını kullanıcıdan alan metod
        public int GetAlarmGunSayisi(string bankaAdi, string kartNo)
        {
            // Varsayılan değer
            int result = 30;

            using (Form inputForm = new Form())
            {
                inputForm.Text = "Alarm Gün Sayısı";
                inputForm.Size = new Size(350, 150);
                inputForm.StartPosition = FormStartPosition.CenterParent;
                inputForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                inputForm.MaximizeBox = false;
                inputForm.MinimizeBox = false;

                Label label = new Label();
                label.Text = $"{bankaAdi} kartı için son kullanma tarihinden kaç gün önce alarm almak istersiniz?";
                label.Location = new Point(10, 15);
                label.AutoSize = true;
                label.Width = 300;

                NumericUpDown numInput = new NumericUpDown();
                numInput.Location = new Point(10, 40);
                numInput.Size = new Size(150, 25);
                numInput.Minimum = 1;
                numInput.Maximum = 365;
                numInput.Value = 30; // Varsayılan değer

                Button okButton = new Button();
                okButton.Text = "Tamam";
                okButton.Location = new Point(180, 70);
                okButton.DialogResult = DialogResult.OK;

                Button cancelButton = new Button();
                cancelButton.Text = "İptal";
                cancelButton.Location = new Point(260, 70);
                cancelButton.DialogResult = DialogResult.Cancel;

                inputForm.Controls.Add(label);
                inputForm.Controls.Add(numInput);
                inputForm.Controls.Add(okButton);
                inputForm.Controls.Add(cancelButton);

                inputForm.AcceptButton = okButton;
                inputForm.CancelButton = cancelButton;

                if (inputForm.ShowDialog() == DialogResult.OK)
                {
                    result = (int)numInput.Value;
                }
                else
                {
                    result = -1; // İptal edildi
                }
            }

            return result;
        }
    }
}