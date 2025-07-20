using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace Veresiye2025
{
    public partial class FormNotEkle : Form
    {
        public string cariKodu;
        public int selectedNoteId = -1;
        public bool isEditing = false;

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

        // Kategori Çeşitleri
        enum NoteCategory
        {
            Normal,
            Important,
            Info,
            Reminder
        }

        public FormNotEkle(string cariKodu)
        {
            InitializeComponent();
            this.cariKodu = cariKodu;
            this.KeyPreview = true;
            this.Load += FormNotEkle_Load;

            // Form köşelerini yuvarlatma
            this.FormBorderStyle = FormBorderStyle.None;
            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 20, 20));

            // Form boyutu değiştiğinde köşeleri güncelle
            this.Resize += (s, e) => {
                this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 20, 20));
            };

            // Form taşıma için olay ekle
            pnlHeader.MouseDown += PnlHeader_MouseDown;
            lblTitle.MouseDown += PnlHeader_MouseDown;

            // Kapat butonu oluştur
            CreateCloseButton();

            // Veri kontrolleri
            SetupDataGridView();
            SetupComboBoxes();
            SetupSearchBox();
            SetupContextMenu();

            // Kısayol tuşları
            SetupShortcuts();

            // Notları yükle
            LoadNotlar();
        }

        public void FormNotEkle_Load(object sender, EventArgs e)
        {
            // Form yüklendiğinde bilgi popup'u göster
            ShowWelcomePopup("Not ekleme ekranına hoş geldiniz!\n\nBurada müşterileriniz için önemli notlar ekleyebilir, düzenleyebilir ve yönetebilirsiniz.\n\nDüzenlemek istediğiniz nota çift tıklayabilirsiniz.");
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

        // Kapat butonu oluştur
        public void CreateCloseButton()
        {
            // Özel bir panel oluştur (kapat butonu için)
            System.Windows.Forms.Panel closePanel = new System.Windows.Forms.Panel
            {
                Size = new Size(30, 30),
                Location = new Point(pnlHeader.Width - 50, 5), // Sağ üst köşeye yakın
                BackColor = Color.FromArgb(232, 17, 35), // Kırmızı arka plan
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Cursor = Cursors.Hand
            };

            // Paneli yuvarlatılmış köşeli yap (tam daire)
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(0, 0, closePanel.Width, closePanel.Height);
            closePanel.Region = new Region(path);

            // X işareti için label ekle
            System.Windows.Forms.Label lblX = new System.Windows.Forms.Label
            {
                Text = "×", // Unicode çarpı işareti
                Font = new Font("Arial", 12, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                Cursor = Cursors.Hand
            };

            closePanel.Controls.Add(lblX);

            // Panel tıklandığında formu kapat
            closePanel.Click += (s, e) => this.Close();
            lblX.Click += (s, e) => this.Close();

            // Hover efekti ekle
            closePanel.MouseEnter += (s, e) => {
                closePanel.BackColor = Color.FromArgb(255, 0, 0); // Daha parlak kırmızı hover
            };

            closePanel.MouseLeave += (s, e) => {
                closePanel.BackColor = Color.FromArgb(232, 17, 35); // Normal kırmızı
            };

            // Label'a da hover efekti ekle
            lblX.MouseEnter += (s, e) => {
                closePanel.BackColor = Color.FromArgb(255, 0, 0);
            };

            lblX.MouseLeave += (s, e) => {
                closePanel.BackColor = Color.FromArgb(232, 17, 35);
            };

            // Header'a ekle
            pnlHeader.Controls.Add(closePanel);

            // Header boyutu değiştiğinde konumu güncelle
            pnlHeader.Resize += (s, e) => {
                closePanel.Location = new Point(pnlHeader.Width - 40, 5);
            };
        }

        public void SetupDataGridView()
        {
            // DataGridView genel ayarları
            dgvNotlar.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvNotlar.SelectionMode = DataGridViewSelectionMode.CellSelect; // Hücre seçimini aktif et
            dgvNotlar.MultiSelect = false;
            dgvNotlar.AllowUserToAddRows = false;
            dgvNotlar.AllowUserToDeleteRows = false;
            dgvNotlar.AllowUserToResizeRows = false;
            dgvNotlar.ReadOnly = true;
            dgvNotlar.RowHeadersVisible = false;
            dgvNotlar.BorderStyle = BorderStyle.None;
            dgvNotlar.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal; // Sadece yatay çizgiler
            dgvNotlar.RowTemplate.Height = 35; // Satır yüksekliğini artır

            // Tema ayarları
            dgvNotlar.EnableHeadersVisualStyles = false;
            dgvNotlar.BackgroundColor = Color.White;
            dgvNotlar.DefaultCellStyle.SelectionBackColor = Color.FromArgb(28, 141, 243);
            dgvNotlar.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvNotlar.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(28, 141, 243);
            dgvNotlar.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvNotlar.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dgvNotlar.ColumnHeadersHeight = 40; // Başlık yüksekliğini artır
            dgvNotlar.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);

            // Sütunları oluştur
            dgvNotlar.Columns.Add("ID", "ID");
            dgvNotlar.Columns.Add("Kategori", "Kategori");
            dgvNotlar.Columns.Add("Tarih", "Tarih");
            dgvNotlar.Columns.Add("Not", "Not");
            dgvNotlar.Columns.Add("GuncellenmeTarihi", "Güncel. Tarihi");

            // Sütun genişlikleri ve özellikleri
            dgvNotlar.Columns["ID"].Visible = false; // ID gizli
            dgvNotlar.Columns["Kategori"].Width = 100;
            dgvNotlar.Columns["Kategori"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvNotlar.Columns["Tarih"].Width = 150;
            dgvNotlar.Columns["Not"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvNotlar.Columns["GuncellenmeTarihi"].Width = 150;

            // DataGridView olayları
            dgvNotlar.CellClick += DgvNotlar_CellClick;
            dgvNotlar.CellFormatting += DgvNotlar_CellFormatting;
            dgvNotlar.CellDoubleClick += DgvNotlar_CellDoubleClick;
            dgvNotlar.SelectionChanged += DgvNotlar_SelectionChanged;

            // Sadece Tarih, Not ve Güncel Tarihi sütunlarını seçilebilir yap
            dgvNotlar.CellClick += (s, e) => {
                if (e.RowIndex >= 0)
                {
                    // Tıklanan sütun Kategori ise, seçimi engelle
                    if (e.ColumnIndex == dgvNotlar.Columns["Kategori"].Index)
                    {
                        // Kategori sütunu seçimini iptal et, diğer sütunları seçili yap
                        dgvNotlar.ClearSelection();

                        // Diğer sütunları seç
                        dgvNotlar.Rows[e.RowIndex].Cells["Tarih"].Selected = true;
                        dgvNotlar.Rows[e.RowIndex].Cells["Not"].Selected = true;
                        dgvNotlar.Rows[e.RowIndex].Cells["GuncellenmeTarihi"].Selected = true;
                    }
                }
            };
        }

        // DataGridView'da satır seçildiğinde
        //public bool isUpdatingSelection = false; // Seçim güncellenirken kontrol etmek için bayrak

        public void DgvNotlar_SelectionChanged(object sender, EventArgs e)
        {
            // Eğer zaten seçim güncelleme işlemi yapılıyorsa, çık
            if (isUpdatingSelection)
                return;

            try
            {
                isUpdatingSelection = true; // Bayrağı ayarla

                if (dgvNotlar.SelectedCells.Count > 0)
                {
                    int rowIndex = dgvNotlar.SelectedCells[0].RowIndex;

                    // Not bilgilerini form kontrollerine aktar
                    selectedNoteId = Convert.ToInt32(dgvNotlar.Rows[rowIndex].Cells["ID"].Value);
                    string notMetni = dgvNotlar.Rows[rowIndex].Cells["Not"].Value.ToString();
                    string kategori = dgvNotlar.Rows[rowIndex].Cells["Kategori"].Value.ToString();

                    // Not metnini metin kutusuna aktar
                    txtNot.Text = notMetni;

                    // cmbKategori içinde kategori var mı kontrol et
                    if (cmbKategori.Items.Contains(kategori))
                        cmbKategori.SelectedItem = kategori;
                    else
                        cmbKategori.SelectedIndex = 0; // Varsayılan olarak ilk öğeyi seç

                    // Düzenleme moduna GEÇMEMELİ, sadece görüntüleme modunda kalmalı
                    SetEditingMode(false); // Bu satırı false olarak değiştirdik
                }
            }
            finally
            {
                isUpdatingSelection = false; // İşlem bittikten sonra bayrağı sıfırla
            }
        }

        public void SetupComboBoxes()
        {
            // Kategori combobox
            cmbKategori.Items.Clear();
            cmbKategori.Items.Add("Normal");
            cmbKategori.Items.Add("Önemli");
            cmbKategori.Items.Add("Bilgi");
            cmbKategori.Items.Add("Hatırlatma");
            cmbKategori.SelectedIndex = 0;

            // Filtreleme combobox
            cmbFilter.Items.Clear();
            cmbFilter.Items.Add("Tümü");
            cmbFilter.Items.Add("Normal");
            cmbFilter.Items.Add("Önemli");
            cmbFilter.Items.Add("Bilgi");
            cmbFilter.Items.Add("Hatırlatma");
            cmbFilter.Items.Add("Bugün");
            cmbFilter.Items.Add("Bu Hafta");
            cmbFilter.Items.Add("Bu Ay");
            cmbFilter.SelectedIndex = 0;

            // Filtreleme olayı
            cmbFilter.SelectedIndexChanged += (s, e) => LoadNotlar();
        }

        public void SetupSearchBox()
        {
            // Arama kutusu placeholder
            txtSearch.PlaceholderText = "Ara...";

            // Arama kutusu olayı
            txtSearch.TextChanged += (s, e) => SearchNotes();
        }

        public void SetupContextMenu()
        {
            // Context menu oluştur
            ContextMenuStrip contextMenu = new ContextMenuStrip();

            // Menü öğeleri
            ToolStripMenuItem editItem = new ToolStripMenuItem("Düzenle", Properties.Resources.edit);
            editItem.Click += (s, e) => EditSelectedNote();

            ToolStripMenuItem deleteItem = new ToolStripMenuItem("Sil", Properties.Resources.delete);
            deleteItem.Click += (s, e) => DeleteSelectedNote();

            ToolStripMenuItem alarmItem = new ToolStripMenuItem("Alarm Kur", Properties.Resources.clock);
            alarmItem.Click += (s, e) => SetAlarmForSelectedNote();

            ToolStripMenuItem copyItem = new ToolStripMenuItem("Kopyala");
            copyItem.Click += (s, e) => CopySelectedNote();

            // Menüye öğeleri ekle
            contextMenu.Items.Add(editItem);
            contextMenu.Items.Add(deleteItem);
            contextMenu.Items.Add(new ToolStripSeparator());
            contextMenu.Items.Add(alarmItem);
            contextMenu.Items.Add(copyItem);

            // DataGridView'e context menu atama
            dgvNotlar.ContextMenuStrip = contextMenu;

            // Mouse down olayı ile seçim
            dgvNotlar.MouseDown += (s, e) => {
                if (e.Button == MouseButtons.Right)
                {
                    var hti = dgvNotlar.HitTest(e.X, e.Y);
                    if (hti.RowIndex >= 0)
                    {
                        dgvNotlar.ClearSelection();
                        dgvNotlar.Rows[hti.RowIndex].Selected = true;
                    }
                }
            };
        }

        public void SetupShortcuts()
        {
            // Kısayol tuşları
            this.KeyDown += (s, e) => {
                // Esc: Formu kapat
                if (e.KeyCode == Keys.Escape)
                    this.Close();

                // F2 veya Ctrl+E: Seçili notu düzenle
                if (e.KeyCode == Keys.F2 || (e.Control && e.KeyCode == Keys.E))
                    EditSelectedNote();

                // Delete: Seçili notu sil
                if (e.KeyCode == Keys.Delete)
                    DeleteSelectedNote();

                // Ctrl+N: Yeni not odaklan
                if (e.Control && e.KeyCode == Keys.N)
                {
                    ClearSelection();
                    txtNot.Focus();
                }

                // Ctrl+S: Kaydet/Güncelle
                if (e.Control && e.KeyCode == Keys.S)
                {
                    if (isEditing)
                        UpdateNote();
                    else
                        AddNote();
                }

                // Enter (not alanında): Kaydet
                if (e.KeyCode == Keys.Enter && txtNot.Focused && !e.Shift && !string.IsNullOrEmpty(txtNot.Text))
                {
                    e.SuppressKeyPress = true; // Enter'ın textbox'a geçmesini engelle
                    if (isEditing)
                        UpdateNote();
                    else
                        AddNote();
                }
            };
        }

        public bool isUpdatingSelection = false; // Sınıf düzeyinde tanımlayın

        public void LoadNotlar(string searchTerm = "")
        {
            dgvNotlar.Rows.Clear();
            selectedNoteId = -1;

            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Filtreleme koşullarını oluştur
                List<string> conditions = new List<string>();

                // Kategori ve tarih filtrelerini ekle
                string filterCondition = GetFilterCondition();
                if (!string.IsNullOrEmpty(filterCondition))
                {
                    conditions.Add(filterCondition);
                }

                // Arama filtresi ekle
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    conditions.Add($"NotMetni LIKE '%{searchTerm}%'");
                }

                // Her zaman eklenecek koşullar
                conditions.Add("CariKodu = @CariKodu");
                conditions.Add("(Deleted = 0 OR Deleted IS NULL)");

                // WHERE ifadesini oluştur
                string whereClause = conditions.Count > 0
                    ? "WHERE " + string.Join(" AND ", conditions)
                    : "";

                string query = $@"
            SELECT
                ID,
                NotMetni,
                TarihSaat,
                GuncellenmeTarihi,
                Kategori,
                Deleted
            FROM Notlar
            {whereClause}
            ORDER BY TarihSaat DESC";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CariKodu", cariKodu);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["ID"]);
                            string notMetni = reader["NotMetni"].ToString();
                            string kategori = reader["Kategori"] != DBNull.Value ? reader["Kategori"].ToString() : "Normal";

                            // Oluşturulma tarihi - daha şık format
                            DateTime tarihSaat;
                            string tarihDisplay = "Tarih yok";
                            if (reader["TarihSaat"] != DBNull.Value && !string.IsNullOrEmpty(reader["TarihSaat"].ToString()))
                            {
                                if (DateTime.TryParse(reader["TarihSaat"].ToString(), out tarihSaat))
                                {
                                    tarihDisplay = tarihSaat.ToString("dd.MM.yyyy HH:mm");
                                }
                            }

                            // Güncellenme tarihi - eğer varsa güzel format
                            string guncellemeTarihi = "";
                            if (reader["GuncellenmeTarihi"] != DBNull.Value && !string.IsNullOrEmpty(reader["GuncellenmeTarihi"].ToString()))
                            {
                                if (DateTime.TryParse(reader["GuncellenmeTarihi"].ToString(), out DateTime guncellemeDate))
                                {
                                    guncellemeTarihi = guncellemeDate.ToString("dd.MM.yyyy HH:mm");
                                }
                            }

                            // Satır ekle - görsel özelliklerle birlikte
                            int rowIndex = dgvNotlar.Rows.Add(id, kategori, tarihDisplay, notMetni, guncellemeTarihi);

                            // Satır yüksekliğini ve hücre stillerini ayarla
                            DataGridViewRow row = dgvNotlar.Rows[rowIndex];

                            // Kategori hücresi için özel stil
                            switch (kategori)
                            {
                                case "Önemli":
                                    row.Cells["Kategori"].Style.ForeColor = Color.White;
                                    row.Cells["Kategori"].Style.BackColor = Color.FromArgb(232, 17, 35);
                                    row.Cells["Kategori"].Style.Font = new Font(dgvNotlar.Font, FontStyle.Bold);
                                    row.Cells["Kategori"].Style.SelectionBackColor = Color.FromArgb(232, 17, 35);
                                    row.Cells["Kategori"].Style.SelectionForeColor = Color.White;
                                    break;

                                case "Bilgi":
                                    row.Cells["Kategori"].Style.ForeColor = Color.White;
                                    row.Cells["Kategori"].Style.BackColor = Color.FromArgb(0, 120, 215);
                                    row.Cells["Kategori"].Style.SelectionBackColor = Color.FromArgb(0, 120, 215);
                                    row.Cells["Kategori"].Style.SelectionForeColor = Color.White;
                                    break;

                                case "Hatırlatma":
                                    row.Cells["Kategori"].Style.ForeColor = Color.Black;
                                    row.Cells["Kategori"].Style.BackColor = Color.FromArgb(255, 185, 0);
                                    row.Cells["Kategori"].Style.SelectionBackColor = Color.FromArgb(255, 185, 0);
                                    row.Cells["Kategori"].Style.SelectionForeColor = Color.Black;
                                    break;

                                default: // Normal
                                    row.Cells["Kategori"].Style.ForeColor = Color.White;
                                    row.Cells["Kategori"].Style.BackColor = Color.FromArgb(0, 153, 188);
                                    row.Cells["Kategori"].Style.SelectionBackColor = Color.FromArgb(0, 153, 188);
                                    row.Cells["Kategori"].Style.SelectionForeColor = Color.White;
                                    break;
                            }

                            // Kategori hücresini ortalı yap
                            row.Cells["Kategori"].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                            // Tarih ve güncelleme tarihi hücrelerini ortalı yap
                            row.Cells["Tarih"].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                            if (!string.IsNullOrEmpty(guncellemeTarihi))
                            {
                                row.Cells["GuncellenmeTarihi"].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                row.Cells["GuncellenmeTarihi"].Style.Font = new Font(dgvNotlar.Font, FontStyle.Italic);
                                row.Cells["GuncellenmeTarihi"].Style.ForeColor = Color.FromArgb(0, 120, 215);
                            }
                        }
                    }
                }
                connection.Close();
            }

            // Satır sayısını etiket olarak göster
            lblNoteCount.Text = $"Toplam: {dgvNotlar.Rows.Count} not";

            // Form durumunu sıfırla
            ClearEditing();

            // Satır yoksa bilgi ver
            if (dgvNotlar.Rows.Count == 0 && !string.IsNullOrEmpty(searchTerm))
            {
                ShowNotification("Arama kriterlerine uygun not bulunamadı.", NotificationType.Info);
            }
            else if (dgvNotlar.Rows.Count == 0)
            {
                // Hiç not yoksa bilgi mesajı göster
                lblNoteCount.Text = "Henüz not eklenmemiş.";
            }

            // İlk satırı seçmek için güvenli kod
            // İlk satırı seçmek için güvenli kod
            if (dgvNotlar.Rows.Count > 0)
            {
                try
                {
                    // Bayrak ile sonsuz döngüyü engelliyoruz
                    isUpdatingSelection = true;

                    // Tüm seçimleri temizle
                    dgvNotlar.ClearSelection();

                    // İlk satırın sadece belirli hücrelerini seç
                    dgvNotlar.Rows[0].Cells["Tarih"].Selected = true;
                    dgvNotlar.Rows[0].Cells["Not"].Selected = true;
                    if (dgvNotlar.Columns.Contains("GuncellenmeTarihi"))
                        dgvNotlar.Rows[0].Cells["GuncellenmeTarihi"].Selected = true;

                    // İlk satırın bilgilerini form kontrollerine yükle
                    selectedNoteId = Convert.ToInt32(dgvNotlar.Rows[0].Cells["ID"].Value);
                    txtNot.Text = dgvNotlar.Rows[0].Cells["Not"].Value.ToString();
                    string kategori = dgvNotlar.Rows[0].Cells["Kategori"].Value.ToString();

                    // cmbKategori içinde kategori var mı kontrol et
                    if (cmbKategori.Items.Contains(kategori))
                        cmbKategori.SelectedItem = kategori;
                    else
                        cmbKategori.SelectedIndex = 0; // Varsayılan olarak ilk öğeyi seç

                    // Düzenleme moduna GEÇMEMELİ, sadece görüntüleme modunda kalmalı
                    SetEditingMode(false); // Bu satırı false olarak değiştirdik
                }
                finally
                {
                    // İşlem bittikten sonra bayrağı sıfırla
                    isUpdatingSelection = false;
                }
            }
        }

        // Filtre koşulunu oluşturan yardımcı metod
        public string GetFilterCondition()
        {
            if (cmbFilter.SelectedIndex <= 0)
                return "";

            string selectedFilter = cmbFilter.SelectedItem.ToString();

            switch (selectedFilter)
            {
                case "Normal":
                case "Önemli":
                case "Bilgi":
                case "Hatırlatma":
                    return $"Kategori = '{selectedFilter}'";

                case "Bugün":
                    return $"date(TarihSaat) = date('{DateTime.Now:yyyy-MM-dd}')";

                case "Bu Hafta":
                    DateTime weekStart = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek);
                    DateTime weekEnd = weekStart.AddDays(7);
                    return $"date(TarihSaat) BETWEEN date('{weekStart:yyyy-MM-dd}') AND date('{weekEnd:yyyy-MM-dd}')";

                case "Bu Ay":
                    DateTime monthStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    DateTime monthEnd = monthStart.AddMonths(1).AddDays(-1);
                    return $"date(TarihSaat) BETWEEN date('{monthStart:yyyy-MM-dd}') AND date('{monthEnd:yyyy-MM-dd}')";

                default:
                    return "";
            }
        }

        public string GetFilterWhereClause()
        {
            string whereClause = "";

            if (cmbFilter.SelectedIndex > 0)
            {
                whereClause = " WHERE ";

                switch (cmbFilter.SelectedItem.ToString())
                {
                    case "Normal":
                    case "Önemli":
                    case "Bilgi":
                    case "Hatırlatma":
                        whereClause += $"Kategori = '{cmbFilter.SelectedItem}'";
                        break;
                    case "Bugün":
                        whereClause += $"date(TarihSaat) = date('{DateTime.Now:yyyy-MM-dd}')";
                        break;
                    case "Bu Hafta":
                        DateTime weekStart = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek);
                        DateTime weekEnd = weekStart.AddDays(7);
                        whereClause += $"date(TarihSaat) BETWEEN date('{weekStart:yyyy-MM-dd}') AND date('{weekEnd:yyyy-MM-dd}')";
                        break;
                    case "Bu Ay":
                        DateTime monthStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                        DateTime monthEnd = monthStart.AddMonths(1);
                        whereClause += $"date(TarihSaat) BETWEEN date('{monthStart:yyyy-MM-dd}') AND date('{monthEnd:yyyy-MM-dd}')";
                        break;
                }
            }

            return whereClause;
        }

        public void EnsureTablesExist(SQLiteConnection connection)
        {
            // Notlar tablosunu oluştur veya güncelle
            string tableQuery = @"
                CREATE TABLE IF NOT EXISTS Notlar (
                    ID INTEGER PRIMARY KEY AUTOINCREMENT,
                    CariKodu TEXT NOT NULL,
                    NotMetni TEXT NOT NULL,
                    TarihSaat TEXT NOT NULL,
                    GuncellenmeTarihi TEXT,
                    Kategori TEXT DEFAULT 'Normal',
                    Deleted INTEGER DEFAULT 0,
                    AlarmTarihi TEXT
                );";

            using (SQLiteCommand command = new SQLiteCommand(tableQuery, connection))
            {
                command.ExecuteNonQuery();
            }

            // Kategori sütununu kontrol et, yoksa ekle
            string checkColumnQuery = "PRAGMA table_info(Notlar);";
            using (SQLiteCommand command = new SQLiteCommand(checkColumnQuery, connection))
            {
                bool hasKategori = false;
                bool hasDeleted = false;
                bool hasAlarmTarihi = false;

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string columnName = reader["name"].ToString();
                        if (columnName == "Kategori")
                            hasKategori = true;
                        else if (columnName == "Deleted")
                            hasDeleted = true;
                        else if (columnName == "AlarmTarihi")
                            hasAlarmTarihi = true;
                    }
                }

                // Eksik sütunları ekle
                if (!hasKategori)
                {
                    using (SQLiteCommand alterCommand = new SQLiteCommand(
                        "ALTER TABLE Notlar ADD COLUMN Kategori TEXT DEFAULT 'Normal';", connection))
                    {
                        alterCommand.ExecuteNonQuery();
                    }
                }

                if (!hasDeleted)
                {
                    using (SQLiteCommand alterCommand = new SQLiteCommand(
                        "ALTER TABLE Notlar ADD COLUMN Deleted INTEGER DEFAULT 0;", connection))
                    {
                        alterCommand.ExecuteNonQuery();
                    }
                }

                if (!hasAlarmTarihi)
                {
                    using (SQLiteCommand alterCommand = new SQLiteCommand(
                        "ALTER TABLE Notlar ADD COLUMN AlarmTarihi TEXT;", connection))
                    {
                        alterCommand.ExecuteNonQuery();
                    }
                }
            }
        }

        // DataGridView hücre tıklama olayı - seçim mantığını buraya taşıyın
        public void DgvNotlar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Başlık satırı tıklaması veya geçersiz satır indeksi
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            try
            {
                isUpdatingSelection = true; // Bayrağı ayarla

                // Kategori sütununa tıklandıysa özel işlem yap
                if (e.ColumnIndex == dgvNotlar.Columns["Kategori"].Index)
                {
                    // Seçimi temizle
                    dgvNotlar.ClearSelection();

                    // Sadece diğer hücreleri seç
                    dgvNotlar.Rows[e.RowIndex].Cells["Tarih"].Selected = true;
                    dgvNotlar.Rows[e.RowIndex].Cells["Not"].Selected = true;
                    if (dgvNotlar.Columns.Contains("GuncellenmeTarihi"))
                        dgvNotlar.Rows[e.RowIndex].Cells["GuncellenmeTarihi"].Selected = true;
                }
            }
            finally
            {
                isUpdatingSelection = false; // İşlem bittikten sonra bayrağı sıfırla
            }
        }

        public void DgvNotlar_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                EditSelectedNote(); // Düzenleme moduna geç
            }
        }

        // Hücre formatlamalarını düzenle
        public void DgvNotlar_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            // Kategori hücresini formatla - daha görsel bir şekilde
            if (e.ColumnIndex == dgvNotlar.Columns["Kategori"].Index)
            {
                string kategori = e.Value?.ToString() ?? "Normal";

                switch (kategori)
                {
                    case "Önemli":
                        e.CellStyle.ForeColor = Color.White;
                        e.CellStyle.BackColor = Color.FromArgb(232, 17, 35);
                        e.CellStyle.Font = new Font(dgvNotlar.Font, FontStyle.Bold);
                        break;

                    case "Bilgi":
                        e.CellStyle.ForeColor = Color.White;
                        e.CellStyle.BackColor = Color.FromArgb(0, 120, 215);
                        e.CellStyle.SelectionBackColor = Color.FromArgb(0, 90, 158); // Seçiliyken daha koyu mavi
                        break;

                    case "Hatırlatma":
                        e.CellStyle.ForeColor = Color.Black;
                        e.CellStyle.BackColor = Color.FromArgb(255, 185, 0);
                        e.CellStyle.SelectionBackColor = Color.FromArgb(229, 166, 0); // Seçiliyken daha koyu sarı
                        break;

                    default: // Normal
                        e.CellStyle.ForeColor = Color.Black;
                        e.CellStyle.BackColor = Color.FromArgb(0, 153, 188);
                        e.CellStyle.ForeColor = Color.White;
                        break;
                }

                // Kategori hücrelerini ortalı göster ve biraz daha şık hale getir
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                e.CellStyle.Padding = new Padding(5, 0, 5, 0);
            }

            // Tarih sütunu formatlaması
            if (e.ColumnIndex == dgvNotlar.Columns["Tarih"].Index ||
                e.ColumnIndex == dgvNotlar.Columns["GuncellenmeTarihi"].Index)
            {
                if (!string.IsNullOrEmpty(e.Value?.ToString()))
                {
                    e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    // Güncellenme tarihi varsa italik yap
                    if (e.ColumnIndex == dgvNotlar.Columns["GuncellenmeTarihi"].Index)
                    {
                        e.CellStyle.Font = new Font(dgvNotlar.Font, FontStyle.Italic);
                        e.CellStyle.ForeColor = Color.FromArgb(0, 120, 215); // Mavi ton
                    }
                }
            }

            // Not sütunu - içeriği kırparak göster
            if (e.ColumnIndex == dgvNotlar.Columns["Not"].Index)
            {
                string notText = e.Value?.ToString() ?? "";
                if (notText.Length > 100)
                {
                    // e.Value = notText.Substring(0, 97) + "...";
                    // Değeri değiştirmiyoruz, sadece görünümü ayarlıyoruz
                    e.CellStyle.WrapMode = DataGridViewTriState.True; // Yazıyı birden fazla satıra böl
                }
            }
        }

        public void EditSelectedNote()
        {
            if (dgvNotlar.SelectedRows.Count > 0 || dgvNotlar.SelectedCells.Count > 0)
            {
                int rowIndex = dgvNotlar.SelectedCells.Count > 0
                    ? dgvNotlar.SelectedCells[0].RowIndex
                    : dgvNotlar.SelectedRows[0].Index;

                selectedNoteId = Convert.ToInt32(dgvNotlar.Rows[rowIndex].Cells["ID"].Value);
                string notMetni = dgvNotlar.Rows[rowIndex].Cells["Not"].Value.ToString();
                string kategori = dgvNotlar.Rows[rowIndex].Cells["Kategori"].Value.ToString();

                // Not metnini metin kutusuna aktar
                txtNot.Text = notMetni;

                // Kategoriyi seç
                if (cmbKategori.Items.Contains(kategori))
                    cmbKategori.SelectedItem = kategori;
                else
                    cmbKategori.SelectedIndex = 0;

                // Düzenleme modunu etkinleştir
                SetEditingMode(true); // Burası true olacak - Düzenleme moduna geç

                // Metin kutusuna odaklan
                txtNot.Focus();
            }
            else
            {
                ShowNotification("Lütfen düzenlemek için bir not seçin.", NotificationType.Warning);
            }
        }


        public void DeleteSelectedNote()
        {
            if (dgvNotlar.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvNotlar.SelectedRows[0];
                int noteId = Convert.ToInt32(row.Cells["ID"].Value);
                string notMetni = row.Cells["Not"].Value.ToString();

                // Onay iste
                var result = ShowCustomMessageBox(
                    "Notu Sil",
                    $"Bu notu silmek istediğinize emin misiniz?\n\n{notMetni.Substring(0, Math.Min(50, notMetni.Length))}{(notMetni.Length > 50 ? "..." : "")}",
                    MessageType.Question);

                if (result == DialogResult.Yes)
                {
                    // Notu veritabanından tamamen sil
                    string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
                    using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                    {
                        connection.Open();
                        string query = "DELETE FROM Notlar WHERE ID = @ID";
                        using (SQLiteCommand command = new SQLiteCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@ID", noteId);
                            command.ExecuteNonQuery();
                        }
                        connection.Close();
                    }

                    // Notları yeniden yükle
                    LoadNotlar();

                    // Bildirim göster
                    ShowNotification("Not başarıyla silindi.", NotificationType.Success);
                }
            }
            else
            {
                ShowNotification("Lütfen silmek için bir not seçin.", NotificationType.Warning);
            }
        }

        public void SetAlarmForSelectedNote()
        {
            if (dgvNotlar.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvNotlar.SelectedRows[0];
                int noteId = Convert.ToInt32(row.Cells["ID"].Value);
                string notMetni = row.Cells["Not"].Value.ToString();

                ShowNotification("Alarm kurma özelliği yakında eklenecek.", NotificationType.Info);

                // Alarm kurma penceresi buraya eklenecek
                /* 
                using (var alarmForm = new FormAlarmKur(noteId, notMetni))
                {
                    alarmForm.ShowDialog(this);
                    
                    // Alarm eklenirse yeniden yükle
                    if (alarmForm.DialogResult == DialogResult.OK)
                    {
                        LoadNotlar();
                    }
                }
                */
            }
            else
            {
                ShowNotification("Lütfen alarm kurmak için bir not seçin.", NotificationType.Warning);
            }
        }

        public void CopySelectedNote()
        {
            if (dgvNotlar.SelectedRows.Count > 0)
            {
                string notMetni = dgvNotlar.SelectedRows[0].Cells["Not"].Value.ToString();
                Clipboard.SetText(notMetni);
                ShowNotification("Not metni panoya kopyalandı.", NotificationType.Success);
            }
        }

        public void SearchNotes()
        {
            string searchTerm = txtSearch.Text.Trim();
            LoadNotlar(searchTerm);
        }

        public void ClearSelection()
        {
            dgvNotlar.ClearSelection();
            selectedNoteId = -1;
            txtNot.Clear();
            cmbKategori.SelectedIndex = 0;
            SetEditingMode(false);
        }

        public void SetEditingMode(bool editing)
        {
            isEditing = editing;

            // Butonların görünürlüğünü ayarla
            btnAdd.Visible = !editing;
            btnUpdate.Visible = editing;
            btnCancel.Visible = editing;

            // Görsel geri bildirim
            if (editing)
            {
                txtNot.BackColor = Color.FromArgb(255, 255, 200); // Düzenleme modunda hafif sarı arka plan
            }
            else
            {
                txtNot.BackColor = Color.White;
            }
        }

        public void ClearEditing()
        {
            txtNot.Clear();
            if (cmbKategori.Items.Count > 0)
                cmbKategori.SelectedIndex = 0;
            selectedNoteId = -1;
            SetEditingMode(false);
        }

        public void AddNote()
        {
            string notMetni = txtNot.Text.Trim();
            if (string.IsNullOrEmpty(notMetni))
            {
                ShowNotification("Not metni boş olamaz.", NotificationType.Warning);
                return;
            }

            string kategori = cmbKategori.SelectedItem.ToString();

            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Tablo kontrol et/oluştur
                EnsureTablesExist(connection);

                string query = @"
            INSERT INTO Notlar (CariKodu, NotMetni, TarihSaat, Kategori) 
            VALUES (@CariKodu, @NotMetni, @TarihSaat, @Kategori)";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CariKodu", cariKodu);
                    command.Parameters.AddWithValue("@NotMetni", notMetni);
                    command.Parameters.AddWithValue("@TarihSaat", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    command.Parameters.AddWithValue("@Kategori", kategori);
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }

            // NOT BİLGİLERİNİ SIFIRLA
            txtNot.Clear();
            cmbKategori.SelectedIndex = 0;
            selectedNoteId = -1;
            SetEditingMode(false); // Düzenleme modunu kapat

            // Notları yeniden yükle
            LoadNotlar();

            // Bildirim göster
            ShowNotification("Not başarıyla eklendi.", NotificationType.Success);
        }

        public void UpdateNote()
        {
            if (selectedNoteId < 0)
            {
                ShowNotification("Lütfen güncellenecek bir not seçin.", NotificationType.Warning);
                return;
            }

            string notMetni = txtNot.Text.Trim();
            if (string.IsNullOrEmpty(notMetni))
            {
                ShowNotification("Not metni boş olamaz.", NotificationType.Warning);
                return;
            }

            string kategori = cmbKategori.SelectedItem.ToString();

            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = @"
                    UPDATE Notlar 
                    SET NotMetni = @NotMetni, 
                        GuncellenmeTarihi = @GuncellenmeTarihi, 
                        Kategori = @Kategori 
                    WHERE ID = @ID";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", selectedNoteId);
                    command.Parameters.AddWithValue("@NotMetni", notMetni);
                    command.Parameters.AddWithValue("@GuncellenmeTarihi", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    command.Parameters.AddWithValue("@Kategori", kategori);
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }

            // Form durumunu sıfırla
            ClearEditing();

            // Notları yeniden yükle
            LoadNotlar();

            // Bildirim göster
            ShowNotification("Not başarıyla güncellendi.", NotificationType.Success);
        }

        #region UI Events

        public void btnAdd_Click(object sender, EventArgs e)
        {
            AddNote();
            ClearEditing();
        }

        public void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateNote();
        }

        public void btnCancel_Click(object sender, EventArgs e)
        {
            ClearEditing();
        }

        public void btnClear_Click(object sender, EventArgs e)
        {
            ClearSelection();
            txtNot.Focus();
        }

        public void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadNotlar();
            
        }

        #endregion

        #region Helper Methods

        // Özel mesaj kutusu türleri
        public enum MessageType
        {
            Info,
            Success,
            Warning,
            Error,
            Question
        }

        // Notification tipleri
        public enum NotificationType
        {
            Info,
            Success,
            Warning,
            Error
        }

        // Özel mesaj kutusu göster
        public DialogResult ShowCustomMessageBox(string title, string message, MessageType type)
        {
            // Özel mesaj kutusu oluştur
            using (Form msgBox = new Form())
            {
                msgBox.FormBorderStyle = FormBorderStyle.None;
                msgBox.BackColor = Color.White;
                msgBox.StartPosition = FormStartPosition.CenterParent;
                msgBox.Size = new Size(400, 200);
                msgBox.Font = new Font("Segoe UI", 9);

                // Yuvarlatılmış köşeler
                msgBox.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, msgBox.Width, msgBox.Height, 20, 20));

                // Başlık panel
                System.Windows.Forms.Panel titlePanel = new System.Windows.Forms.Panel
                {
                    Dock = DockStyle.Top,
                    Height = 40,
                    BackColor = GetColorForMessageType(type)
                };

                System.Windows.Forms.Label titleLabel = new System.Windows.Forms.Label
                {
                    Text = title,
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    ForeColor = Color.White,
                    AutoSize = true,
                    Location = new Point(15, 10)
                };

                titlePanel.Controls.Add(titleLabel);
                msgBox.Controls.Add(titlePanel);

                // Mesaj label
                System.Windows.Forms.Label messageLabel = new System.Windows.Forms.Label
                {
                    Text = message,
                    Font = new Font("Segoe UI", 10),
                    ForeColor = Color.Black,
                    AutoSize = false,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Dock = DockStyle.Fill,
                    Padding = new Padding(20, 20, 20, 0)
                };

                msgBox.Controls.Add(messageLabel);

                // Butonlar panel
                System.Windows.Forms.Panel buttonPanel = new System.Windows.Forms.Panel
                {
                    Dock = DockStyle.Bottom,
                    Height = 60,
                    BackColor = Color.White
                };

                // Butonları oluştur
                DialogResult result = DialogResult.None;

                if (type == MessageType.Question)
                {
                    Guna2Button yesButton = new Guna2Button
                    {
                        Text = "Evet",
                        Size = new Size(100, 35),
                        Location = new Point(msgBox.Width - 235, 15),
                        FillColor = Color.FromArgb(28, 141, 243),
                        ForeColor = Color.White,
                        Font = new Font("Segoe UI", 9, FontStyle.Bold),
                        BorderRadius = 5
                    };

                    yesButton.Click += (s, e) => {
                        result = DialogResult.Yes;
                        msgBox.Close();
                    };

                    Guna2Button noButton = new Guna2Button
                    {
                        Text = "Hayır",
                        Size = new Size(100, 35),
                        Location = new Point(msgBox.Width - 120, 15),
                        FillColor = Color.FromArgb(220, 220, 220),
                        ForeColor = Color.Black,
                        Font = new Font("Segoe UI", 9, FontStyle.Bold),
                        BorderRadius = 5
                    };

                    noButton.Click += (s, e) => {
                        result = DialogResult.No;
                        msgBox.Close();
                    };

                    buttonPanel.Controls.Add(yesButton);
                    buttonPanel.Controls.Add(noButton);
                }
                else
                {
                    Guna2Button okButton = new Guna2Button
                    {
                        Text = "Tamam",
                        Size = new Size(100, 35),
                        Location = new Point(msgBox.Width - 120, 15),
                        FillColor = Color.FromArgb(28, 141, 243),
                        ForeColor = Color.White,
                        Font = new Font("Segoe UI", 9, FontStyle.Bold),
                        BorderRadius = 5
                    };

                    okButton.Click += (s, e) => {
                        result = DialogResult.OK;
                        msgBox.Close();
                    };

                    buttonPanel.Controls.Add(okButton);
                }

                msgBox.Controls.Add(buttonPanel);

                // Form taşıma
                titlePanel.MouseDown += (s, e) => {
                    if (e.Button == MouseButtons.Left)
                    {
                        ReleaseCapture();
                        SendMessage(msgBox.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                    }
                };

                // Formu göster
                msgBox.ShowDialog();

                return result;
            }
        }

        public Color GetColorForMessageType(MessageType type)
        {
            switch (type)
            {
                case MessageType.Info:
                    return Color.FromArgb(0, 120, 215);
                case MessageType.Success:
                    return Color.FromArgb(16, 172, 132);
                case MessageType.Warning:
                    return Color.FromArgb(245, 159, 0);
                case MessageType.Error:
                    return Color.FromArgb(232, 17, 35);
                case MessageType.Question:
                    return Color.FromArgb(48, 71, 94);
                default:
                    return Color.FromArgb(28, 141, 243);
            }
        }

        // Form açılışında bilgi popup'u gösterme metodu
        public void ShowWelcomePopup(string message, int durationSeconds = 7)
        {
            // Popup form oluştur
            Form popupForm = new Form
            {
                Size = new Size(400, 300),
                StartPosition = FormStartPosition.CenterScreen,
                FormBorderStyle = FormBorderStyle.None,
                BackColor = Color.FromArgb(28, 141, 243), // Mavi arka plan28; 141; 243
                TopMost = true, // Her zaman üstte göster
                ShowInTaskbar = false // Görev çubuğunda gösterme
            };

            // Yuvarlatılmış köşeler
            popupForm.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, popupForm.Width, popupForm.Height, 20, 20));

            // İçerik paneli
            Panel contentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10, 10, 10, 10),
            };

            // Başlık etiketi
            Label titleLabel = new Label
            {
                Text = "Bilgi",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.White,
                Dock = DockStyle.Top,
                Height = 40,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Mesaj etiketi
            Label messageLabel = new Label
            {
                Text = message,
                Font = new Font("Segoe UI", 11),
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Alt bilgi etiketi (otomatik kapanma sayacı)
            Label infoLabel = new Label
            {
                Text = $"{durationSeconds} saniye içinde otomatik kapanacak",
                Font = new Font("Segoe UI", 9, FontStyle.Italic),
                ForeColor = Color.White,
                Dock = DockStyle.Bottom,
                Height = 30,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Kapat butonu (sağ üst köşe)
            Button closeButton = new Button
            {
                Text = "✕",
                Font = new Font("Arial", 10, FontStyle.Bold),
                Size = new Size(30, 30),
                Location = new Point(popupForm.Width - 40, 10),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                ForeColor = Color.White,
                Cursor = Cursors.Hand
            };
            closeButton.FlatAppearance.BorderSize = 0;
            closeButton.Click += (s, e) => popupForm.Close();

            // Kontrolleri forma ekle
            contentPanel.Controls.Add(messageLabel);
            contentPanel.Controls.Add(titleLabel);
            contentPanel.Controls.Add(infoLabel);
            contentPanel.Controls.Add(closeButton);
            popupForm.Controls.Add(contentPanel);

            // Animasyon efekti için
            double opacity = 0;

            // Fade in/out için timer
            Timer animationTimer = new Timer { Interval = 30 };
            bool isFadingOut = false;
            animationTimer.Tick += (s, e) => {
                if (!isFadingOut)
                {
                    // Fade in
                    opacity += 0.1;
                    if (opacity >= 1)
                        opacity = 1;

                    popupForm.Opacity = opacity;

                    if (opacity >= 1)
                        animationTimer.Stop();
                }
                else
                {
                    // Fade out
                    opacity -= 0.1;
                    if (opacity <= 0)
                    {
                        opacity = 0;
                        popupForm.Opacity = 0;
                        animationTimer.Stop();
                        popupForm.Close();
                    }
                    else
                    {
                        popupForm.Opacity = opacity;
                    }
                }
            };

            // Otomatik kapanma için timer
            Timer closeTimer = new Timer { Interval = 1000 };
            int remainingSeconds = durationSeconds;

            closeTimer.Tick += (s, e) => {
                remainingSeconds--;
                infoLabel.Text = $"{remainingSeconds} saniye içinde otomatik kapanacak";

                if (remainingSeconds <= 0)
                {
                    closeTimer.Stop();
                    isFadingOut = true;
                    animationTimer.Start(); // Fade out başlat
                }
            };

            // Form taşıma için gerekli kod
            popupForm.MouseDown += (s, e) => {
                if (e.Button == MouseButtons.Left)
                {
                    ReleaseCapture();
                    SendMessage(popupForm.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                }
            };

            // Formu göster
            popupForm.Load += (s, e) => {
                popupForm.Opacity = 0;
                animationTimer.Start(); // Fade in başlat
                closeTimer.Start(); // Otomatik kapanma timerını başlat
            };

            popupForm.Show(this);
        }

        // Bildirim göster
        public void ShowNotification(string message, NotificationType type)
        {
            // Toast bildirimi şeklinde göster
            Color bgColor;
            Color textColor = Color.White;

            switch (type)
            {
                case NotificationType.Success:
                    bgColor = Color.FromArgb(16, 172, 132);
                    break;
                case NotificationType.Warning:
                    bgColor = Color.FromArgb(245, 159, 0);
                    break;
                case NotificationType.Error:
                    bgColor = Color.FromArgb(232, 17, 35);
                    break;
                case NotificationType.Info:
                default:
                    bgColor = Color.FromArgb(0, 120, 215);
                    break;
            }

            lblNotification.Text = message;
            lblNotification.BackColor = bgColor;
            lblNotification.ForeColor = textColor;

            // Bildirim panelini göster
            pnlNotification.Visible = true;

            // Timer başlat (3 saniye sonra otomatik kaybolacak)
            Timer notificationTimer = new Timer();
            notificationTimer.Interval = 3000;
            notificationTimer.Tick += (s, e) => {
                pnlNotification.Visible = false;
                notificationTimer.Stop();
                notificationTimer.Dispose();
            };
            notificationTimer.Start();
        }

        #endregion
    }
}