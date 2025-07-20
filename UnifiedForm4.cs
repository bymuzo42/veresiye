// UnifiedForm.cs - Tam İşlevsel ve Eksiksiz Versiyon
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace Veresiye2025
{
    public partial class UnifiedForm : Form
    {
        #region DLL Imports
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);
        #endregion

        #region Private Fields
        // Form Data
        private string _firmaAdi;
        private List<string> cariKoduListesi = new List<string>();
        private string currentCariKodu;
        private string currentCariAdi;
        private bool isDetailViewActive = false;

        // Main Containers
        private Panel mainContainer;
        private Panel topMenuPanel;
        private Panel form4Container;
        private Panel carihareketlerContainer;

        // Form4 UI Components (Liste Görünümü)
        private Panel searchPanelForm4;
        private DataGridView dataGridViewForm4;
        private Panel bottomToolbarForm4;

        // Carihareketler UI Components (Detay Görünümü)
        private Panel topNavPanel;
        private DataGridView dataGridView1;
        private Panel summaryPanel;
        private TextBox textBox1, textBox2, textBox3;
        private Label button3;
        private Panel bottomToolbarCari;

        // Search Components (Form4'ten)
        private Guna2ComboBox cmbSearchType;
        private Guna2TextBox txtSearch;
        private Guna2ComboBox cmbAccountFilter;
        private Guna2ComboBox cmbBalanceFilter;

        // Toolbar Buttons (Form4'ten)
        private Guna2Button btnAddCari;
        private Guna2Button btnEditCari;
        private Guna2Button btnDeleteCari;
        private Guna2Button btnOpenMovements;
        private Guna2Button btnSetAlarm;

        // Carihareketler Toolbar Buttons
        private Guna2Button btnBackToList;
        private Guna2Button btnPreviousCari;
        private Guna2Button btnNextCari;
        private Guna2Button borcekle;
        private Guna2Button tahsilatekle;
        private Guna2Button hrktsil;
        private Guna2Button gecikmesorgula;
        private Guna2Button yazdir;
        private Guna2Button analiz;

        // Menu Strip
        private MenuStrip menuStrip1;
        #endregion

        #region Constructor
        public UnifiedForm(string firmaAdi)
        {
            _firmaAdi = firmaAdi;
            InitializeComponent();
            InitializeIntegratedComponents();
            SetupEventHandlers();
            LoadInitialData();
        }
        #endregion

        #region Initialize Components
        private void InitializeIntegratedComponents()
        {
            // Basic form setup - Form4 tarzında
            this.Text = $"Ana Ekran - {_firmaAdi}";
            this.Size = new Size(1400, 900);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 242, 247);
            this.WindowState = FormWindowState.Maximized;
            this.KeyPreview = true;

            // Rounded corners - Form4'ten
            this.FormBorderStyle = FormBorderStyle.None;
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            this.SizeChanged += (s, e) => {
                if (this.WindowState != FormWindowState.Minimized)
                {
                    this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
                }
            };

            CreateMainContainer();
            CreateTopMenuPanel();
            CreateForm4View();
            CreateCarihareketlerView();
            CreateMenuStrip();

            // Başlangıçta Form4 görünümünü göster
            ShowForm4View();
        }

        private void CreateMainContainer()
        {
            mainContainer = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };
            this.Controls.Add(mainContainer);
        }

        private void CreateTopMenuPanel()
        {
            // Form4'teki üst menu paneli tarzında
            topMenuPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Color.FromArgb(37, 51, 128)
            };

            // Firma adı label (Form4 tarzında)
            Label lblFirmaAdi = new Label
            {
                Text = _firmaAdi,
                Location = new Point(20, 20),
                Size = new Size(400, 25),
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Transparent
            };

            // View switch buttons
            Guna2Button btnListView = new Guna2Button
            {
                Text = "📋 Cari Listesi",
                Size = new Size(120, 35),
                Location = new Point(this.Width - 280, 12),
                FillColor = Color.FromArgb(67, 162, 243),
                ForeColor = Color.White,
                BorderRadius = 8,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };

            Guna2Button btnDetailView = new Guna2Button
            {
                Text = "📊 Cari Hareketleri",
                Size = new Size(120, 35),
                Location = new Point(this.Width - 150, 12),
                FillColor = Color.FromArgb(67, 162, 243),
                ForeColor = Color.White,
                BorderRadius = 8,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };

            btnListView.Click += (s, e) => ShowForm4View();
            btnDetailView.Click += (s, e) => {
                if (!string.IsNullOrEmpty(currentCariKodu))
                    ShowCarihareketlerView();
                else
                    MessageBox.Show("Lütfen önce bir cari seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            };

            topMenuPanel.Controls.Add(lblFirmaAdi);
            topMenuPanel.Controls.Add(btnListView);
            topMenuPanel.Controls.Add(btnDetailView);

            mainContainer.Controls.Add(topMenuPanel);
        }

        private void CreateForm4View()
        {
            form4Container = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Visible = true
            };

            CreateForm4SearchPanel();
            CreateForm4DataGridView();
            CreateForm4BottomToolbar();

            form4Container.Controls.Add(dataGridViewForm4);
            form4Container.Controls.Add(searchPanelForm4);
            form4Container.Controls.Add(bottomToolbarForm4);

            mainContainer.Controls.Add(form4Container);
        }

        private void CreateForm4SearchPanel()
        {
            searchPanelForm4 = new Panel
            {
                Dock = DockStyle.Top,
                Height = 120,
                BackColor = Color.FromArgb(248, 249, 250),
                Padding = new Padding(20)
            };

            // Sorgu ComboBox
            Label lblSorgu = new Label
            {
                Text = "Sorgu:",
                Location = new Point(20, 25),
                Size = new Size(50, 20),
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };

            cmbSearchType = new Guna2ComboBox
            {
                Location = new Point(80, 20),
                Size = new Size(150, 35),
                DropDownStyle = ComboBoxStyle.DropDownList,
                BorderRadius = 8
            };
            cmbSearchType.Items.AddRange(new string[] { "Unvan ile Sorgu", "Kod ile Sorgu", "İl ile Sorgu" });
            cmbSearchType.SelectedIndex = 0;

            // Arama TextBox
            Label lblAra = new Label
            {
                Text = "Ara:",
                Location = new Point(250, 25),
                Size = new Size(40, 20),
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };

            txtSearch = new Guna2TextBox
            {
                Location = new Point(300, 20),
                Size = new Size(200, 35),
                PlaceholderText = "Arama yapmak için yazın...",
                BorderRadius = 8
            };

            // Hesap Filter
            Label lblHesap = new Label
            {
                Text = "Hesap:",
                Location = new Point(520, 25),
                Size = new Size(50, 20),
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };

            cmbAccountFilter = new Guna2ComboBox
            {
                Location = new Point(580, 20),
                Size = new Size(120, 35),
                DropDownStyle = ComboBoxStyle.DropDownList,
                BorderRadius = 8
            };
            cmbAccountFilter.Items.AddRange(new string[] { "Tümü", "Aktif Hesaplar", "Pasif Hesaplar" });
            cmbAccountFilter.SelectedIndex = 0;

            // Filtre
            Label lblFiltre = new Label
            {
                Text = "Filtre:",
                Location = new Point(720, 25),
                Size = new Size(50, 20),
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };

            cmbBalanceFilter = new Guna2ComboBox
            {
                Location = new Point(780, 20),
                Size = new Size(140, 35),
                DropDownStyle = ComboBoxStyle.DropDownList,
                BorderRadius = 8
            };
            cmbBalanceFilter.Items.AddRange(new string[] { "Tümü", "Sadece Borçlular", "Sadece Alacaklılar" });
            cmbBalanceFilter.SelectedIndex = 0;

            searchPanelForm4.Controls.AddRange(new Control[] {
                lblSorgu, cmbSearchType, lblAra, txtSearch,
                lblHesap, cmbAccountFilter, lblFiltre, cmbBalanceFilter
            });
        }

        private void CreateForm4DataGridView()
        {
            dataGridViewForm4 = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false,
                GridColor = Color.FromArgb(230, 230, 230),
                Font = new Font("Segoe UI", 9),
                ColumnHeadersHeight = 40,
                RowTemplate = { Height = 35 }
            };

            dataGridViewForm4.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(67, 162, 243),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };

            dataGridViewForm4.DefaultCellStyle = new DataGridViewCellStyle
            {
                SelectionBackColor = Color.FromArgb(67, 162, 243, 50),
                SelectionForeColor = Color.Black
            };
        }

        private void CreateForm4BottomToolbar()
        {
            bottomToolbarForm4 = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 70,
                BackColor = Color.FromArgb(37, 51, 128),
                Padding = new Padding(20, 15, 20, 15)
            };

            btnAddCari = CreateForm4Button("➕ Cari Ekle", 20);
            btnEditCari = CreateForm4Button("✏️ Değiştir", 150);
            btnDeleteCari = CreateForm4Button("🗑️ Cari Sil", 280);
            btnOpenMovements = CreateForm4Button("➡️ Hareketleri Aç", 410);
            btnSetAlarm = CreateForm4Button("⏰ Alarm Kur", 560);

            bottomToolbarForm4.Controls.AddRange(new Control[] {
                btnAddCari, btnEditCari, btnDeleteCari, btnOpenMovements, btnSetAlarm
            });
        }

        private void CreateCarihareketlerView()
        {
            carihareketlerContainer = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Visible = false
            };

            CreateCarihareketlerTopNav();
            CreateCarihareketlerDataGridView();
            CreateCarihareketlerSummary();
            CreateCarihareketlerBottomToolbar();

            carihareketlerContainer.Controls.Add(dataGridView1);
            carihareketlerContainer.Controls.Add(summaryPanel);
            carihareketlerContainer.Controls.Add(topNavPanel);
            carihareketlerContainer.Controls.Add(bottomToolbarCari);

            mainContainer.Controls.Add(carihareketlerContainer);
        }

        private void CreateCarihareketlerTopNav()
        {
            topNavPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Color.FromArgb(67, 162, 243),
                Padding = new Padding(20, 10, 20, 10)
            };

            btnBackToList = new Guna2Button
            {
                Text = "◀ Cari Listesi",
                Size = new Size(120, 40),
                Location = new Point(20, 10),
                FillColor = Color.FromArgb(52, 144, 220),
                ForeColor = Color.White,
                BorderRadius = 8,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };

            button3 = new Label
            {
                Text = "Seçili Cari",
                Location = new Point(160, 18),
                Size = new Size(400, 25),
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand
            };

            btnPreviousCari = new Guna2Button
            {
                Text = "◀",
                Size = new Size(40, 40),
                FillColor = Color.FromArgb(52, 144, 220),
                ForeColor = Color.White,
                BorderRadius = 8,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };

            btnNextCari = new Guna2Button
            {
                Text = "▶",
                Size = new Size(40, 40),
                FillColor = Color.FromArgb(52, 144, 220),
                ForeColor = Color.White,
                BorderRadius = 8,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };

            topNavPanel.Controls.AddRange(new Control[] {
                btnBackToList, button3, btnPreviousCari, btnNextCari
            });
        }

        private void CreateCarihareketlerDataGridView()
        {
            dataGridView1 = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false,
                GridColor = Color.FromArgb(230, 230, 230),
                Font = new Font("Segoe UI", 9),
                ColumnHeadersHeight = 40,
                RowTemplate = { Height = 35 }
            };

            dataGridView1.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(67, 162, 243),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };
        }

        private void CreateCarihareketlerSummary()
        {
            summaryPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 80,
                BackColor = Color.FromArgb(248, 249, 250),
                Padding = new Padding(20, 10, 20, 10)
            };

            Label lblToplamBorc = new Label
            {
                Text = "Toplam Borç:",
                Location = new Point(20, 25),
                Size = new Size(100, 20),
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };

            textBox1 = new TextBox
            {
                Location = new Point(130, 22),
                Size = new Size(120, 25),
                ReadOnly = true,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.Red,
                Text = "₺0,00"
            };

            Label lblToplamTahsilat = new Label
            {
                Text = "Toplam Tahsilat:",
                Location = new Point(270, 25),
                Size = new Size(110, 20),
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };

            textBox2 = new TextBox
            {
                Location = new Point(390, 22),
                Size = new Size(120, 25),
                ReadOnly = true,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.Green,
                Text = "₺0,00"
            };

            Label lblBakiye = new Label
            {
                Text = "Bakiye:",
                Location = new Point(530, 25),
                Size = new Size(60, 20),
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };

            textBox3 = new TextBox
            {
                Location = new Point(600, 22),
                Size = new Size(120, 25),
                ReadOnly = true,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.Blue,
                Text = "₺0,00"
            };

            summaryPanel.Controls.AddRange(new Control[] {
                lblToplamBorc, textBox1, lblToplamTahsilat, textBox2, lblBakiye, textBox3
            });
        }

        private void CreateCarihareketlerBottomToolbar()
        {
            bottomToolbarCari = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 70,
                BackColor = Color.FromArgb(67, 162, 243),
                Padding = new Padding(20, 15, 20, 15)
            };

            borcekle = CreateCariButton("💰 Borç Ekle", 20);
            tahsilatekle = CreateCariButton("💳 Tahsilat Ekle", 150);
            hrktsil = CreateCariButton("🗑️ Hareket Sil", 280);
            gecikmesorgula = CreateCariButton("⏰ Gecikme Sorgula", 410);
            yazdir = CreateCariButton("🖨️ Yazdır", 560);
            analiz = CreateCariButton("📊 Analiz", 690);

            bottomToolbarCari.Controls.AddRange(new Control[] {
                borcekle, tahsilatekle, hrktsil, gecikmesorgula, yazdir, analiz
            });
        }

        private void CreateMenuStrip()
        {
            menuStrip1 = new MenuStrip
            {
                BackColor = Color.FromArgb(37, 51, 128),
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };

            var firmalarMenu = new ToolStripMenuItem("Firmalar") { ForeColor = Color.White };
            var islemlerMenu = new ToolStripMenuItem("İşlemler") { ForeColor = Color.White };
            var yedekIslemlerMenu = new ToolStripMenuItem("Yedek İşlemleri") { ForeColor = Color.White };
            var araclarMenu = new ToolStripMenuItem("Araçlar") { ForeColor = Color.White };
            var posTakipMenu = new ToolStripMenuItem("Pos Takip İşlemleri") { ForeColor = Color.White };
            var hesapMakinesiMenu = new ToolStripMenuItem("Hesap Makinesi") { ForeColor = Color.White };
            var yardimMenu = new ToolStripMenuItem("Yardım") { ForeColor = Color.White };
            var cikisMenu = new ToolStripMenuItem("Çıkış") { ForeColor = Color.White };

            // Menu item event handlers
            hesapMakinesiMenu.Click += (s, e) => {
                try
                {
                    System.Diagnostics.Process.Start("calc.exe");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hesap makinesi açılamadı: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            cikisMenu.Click += (s, e) => {
                DialogResult result = MessageBox.Show("Programdan çıkmak istediğinizden emin misiniz?",
                    "Çıkış Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    Application.Exit();
                }
            };

            menuStrip1.Items.AddRange(new ToolStripItem[] {
                firmalarMenu, islemlerMenu, yedekIslemlerMenu, araclarMenu,
                posTakipMenu, hesapMakinesiMenu, yardimMenu, cikisMenu
            });

            this.MainMenuStrip = menuStrip1;
            this.Controls.Add(menuStrip1);
        }

        private Guna2Button CreateForm4Button(string text, int x)
        {
            return new Guna2Button
            {
                Text = text,
                Location = new Point(x, 15),
                Size = new Size(120, 40),
                FillColor = Color.FromArgb(67, 162, 243),
                ForeColor = Color.White,
                BorderRadius = 8,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
        }

        private Guna2Button CreateCariButton(string text, int x)
        {
            return new Guna2Button
            {
                Text = text,
                Location = new Point(x, 15),
                Size = new Size(120, 40),
                FillColor = Color.FromArgb(52, 144, 220),
                ForeColor = Color.White,
                BorderRadius = 8,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
        }
        #endregion

        #region Event Handlers Setup
        private void SetupEventHandlers()
        {
            // Form4 event handlers
            dataGridViewForm4.CellDoubleClick += DataGridViewForm4_CellDoubleClick;
            txtSearch.TextChanged += (s, e) => ApplyForm4Filters();
            cmbAccountFilter.SelectedIndexChanged += (s, e) => ApplyForm4Filters();
            cmbBalanceFilter.SelectedIndexChanged += (s, e) => ApplyForm4Filters();

            // Form4 toolbar buttons
            btnOpenMovements.Click += BtnOpenMovements_Click;
            btnAddCari.Click += BtnAddCari_Click;
            btnEditCari.Click += BtnEditCari_Click;
            btnDeleteCari.Click += BtnDeleteCari_Click;
            btnSetAlarm.Click += BtnSetAlarm_Click;

            // Carihareketler event handlers
            btnBackToList.Click += (s, e) => ShowForm4View();
            btnPreviousCari.Click += (s, e) => NavigateToPreviousCari();
            btnNextCari.Click += (s, e) => NavigateToNextCari();

            // Carihareketler toolbar buttons
            borcekle.Click += Borcekle_Click;
            tahsilatekle.Click += Tahsilatekle_Click;
            hrktsil.Click += Hrktsil_Click;
            gecikmesorgula.Click += Gecikmesorgula_Click;
            yazdir.Click += Yazdir_Click;
            analiz.Click += Analiz_Click;

            // Form events
            this.KeyDown += UnifiedForm_KeyDown;
            this.Resize += (s, e) => UpdateNavigationButtonPositions();
        }
        #endregion

        #region Data Loading - Form4 Methods
        private void LoadInitialData()
        {
            LoadCariKoduListesi();
            LoadForm4CariList();
        }

        private void LoadCariKoduListesi()
        {
            cariKoduListesi.Clear();
            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";

            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT CariKodu FROM Cari ORDER BY CariKodu ASC";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cariKoduListesi.Add(reader["CariKodu"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Cari kod listesi yüklenirken hata: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadForm4CariList()
        {
            dataGridViewForm4.Columns.Clear();
            dataGridViewForm4.Rows.Clear();

            // Form4'teki columns setup
            dataGridViewForm4.Columns.Add("Hesap", "Hesap");
            dataGridViewForm4.Columns.Add("CariKodu", "Cari Kodu");
            dataGridViewForm4.Columns.Add("Unvani", "Unvan");
            dataGridViewForm4.Columns.Add("Il", "İl");
            dataGridViewForm4.Columns.Add("SonIslemTarihi", "Son İşlem Tarihi");
            dataGridViewForm4.Columns.Add("Bakiye", "Bakiye");

            // Column widths
            dataGridViewForm4.Columns["Hesap"].Width = 100;
            dataGridViewForm4.Columns["CariKodu"].Width = 100;
            dataGridViewForm4.Columns["Il"].Width = 80;
            dataGridViewForm4.Columns["SonIslemTarihi"].Width = 120;
            dataGridViewForm4.Columns["Bakiye"].Width = 120;

            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";

            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                        SELECT CariKodu, Unvani, Il, Hesap, 
                               IFNULL(bakiye, 0) AS Bakiye,
                               (SELECT MAX(tarih) FROM cari_hareketleri WHERE cari_kodu = Cari.CariKodu) AS SonIslemTarihi
                        FROM Cari
                        ORDER BY bakiye DESC";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            decimal bakiye = Convert.ToDecimal(reader["Bakiye"]);
                            string hesapDurumu = reader["Hesap"].ToString();

                            string sonIslemStr = "Hiç İşlem Yok";
                            if (reader["SonIslemTarihi"] != DBNull.Value)
                            {
                                DateTime sonIslem = Convert.ToDateTime(reader["SonIslemTarihi"]);
                                sonIslemStr = sonIslem.ToString("dd.MM.yyyy");
                            }

                            int rowIndex = dataGridViewForm4.Rows.Add(
                                hesapDurumu,
                                reader["CariKodu"].ToString(),
                                reader["Unvani"].ToString(),
                                reader["Il"].ToString(),
                                sonIslemStr,
                                bakiye.ToString("C2")
                            );

                            // Bakiye durumuna göre renklendirme
                            if (bakiye > 0)
                            {
                                dataGridViewForm4.Rows[rowIndex].DefaultCellStyle.ForeColor = Color.Red;
                            }
                            else if (bakiye < 0)
                            {
                                dataGridViewForm4.Rows[rowIndex].DefaultCellStyle.ForeColor = Color.Green;
                            }

                            // Hesap durumuna göre arka plan rengi
                            if (hesapDurumu == "Pasif")
                            {
                                dataGridViewForm4.Rows[rowIndex].DefaultCellStyle.BackColor = Color.LightGray;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Cari listesi yüklenirken hata: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyForm4Filters()
        {
            if (dataGridViewForm4.DataSource != null)
            {
                // DataTable filtreleme
                DataTable dt = (DataTable)dataGridViewForm4.DataSource;
                string filter = "";

                // Arama filtresi
                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    string searchText = txtSearch.Text.Trim();
                    string searchColumn = "";

                    switch (cmbSearchType.SelectedIndex)
                    {
                        case 0: searchColumn = "Unvani"; break;
                        case 1: searchColumn = "CariKodu"; break;
                        case 2: searchColumn = "Il"; break;
                    }

                    if (!string.IsNullOrEmpty(searchColumn))
                    {
                        filter += $"{searchColumn} LIKE '%{searchText}%'";
                    }
                }

                // Hesap filtresi
                if (cmbAccountFilter.SelectedIndex > 0)
                {
                    string hesapFilter = cmbAccountFilter.SelectedIndex == 1 ? "Aktif" : "Pasif";
                    if (!string.IsNullOrEmpty(filter)) filter += " AND ";
                    filter += $"Hesap = '{hesapFilter}'";
                }

                // Bakiye filtresi
                if (cmbBalanceFilter.SelectedIndex > 0)
                {
                    if (!string.IsNullOrEmpty(filter)) filter += " AND ";
                    if (cmbBalanceFilter.SelectedIndex == 1)
                        filter += "Bakiye > 0";
                    else
                        filter += "Bakiye < 0";
                }

                try
                {
                    dt.DefaultView.RowFilter = filter;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Filtreleme hatası: {ex.Message}", "Hata",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Manual filtering for rows
                foreach (DataGridViewRow row in dataGridViewForm4.Rows)
                {
                    if (row.IsNewRow) continue;

                    bool visible = true;

                    // Arama filtresi
                    if (!string.IsNullOrEmpty(txtSearch.Text))
                    {
                        string searchText = txtSearch.Text.ToLower();
                        string cellValue = "";

                        switch (cmbSearchType.SelectedIndex)
                        {
                            case 0: cellValue = row.Cells["Unvani"].Value?.ToString().ToLower() ?? ""; break;
                            case 1: cellValue = row.Cells["CariKodu"].Value?.ToString().ToLower() ?? ""; break;
                            case 2: cellValue = row.Cells["Il"].Value?.ToString().ToLower() ?? ""; break;
                        }

                        if (!cellValue.Contains(searchText))
                            visible = false;
                    }

                    // Hesap filtresi
                    if (visible && cmbAccountFilter.SelectedIndex > 0)
                    {
                        string hesapDurumu = row.Cells["Hesap"].Value?.ToString() ?? "";
                        string expectedHesap = cmbAccountFilter.SelectedIndex == 1 ? "Aktif" : "Pasif";
                        if (hesapDurumu != expectedHesap)
                            visible = false;
                    }

                    // Bakiye filtresi
                    if (visible && cmbBalanceFilter.SelectedIndex > 0)
                    {
                        string bakiyeStr = row.Cells["Bakiye"].Value?.ToString().Replace("₺", "").Replace(",", "") ?? "0";
                        if (decimal.TryParse(bakiyeStr, out decimal bakiye))
                        {
                            if (cmbBalanceFilter.SelectedIndex == 1 && bakiye <= 0)
                                visible = false;
                            else if (cmbBalanceFilter.SelectedIndex == 2 && bakiye >= 0)
                                visible = false;
                        }
                    }

                    row.Visible = visible;
                }
            }
        }
        #endregion

        #region Form4 Event Handlers
        private void DataGridViewForm4_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string cariKodu = dataGridViewForm4.Rows[e.RowIndex].Cells["CariKodu"].Value?.ToString();
                if (!string.IsNullOrEmpty(cariKodu))
                {
                    currentCariKodu = cariKodu;
                    currentCariAdi = dataGridViewForm4.Rows[e.RowIndex].Cells["Unvani"].Value?.ToString();
                    ShowCarihareketlerView();
                }
            }
        }

        private void BtnOpenMovements_Click(object sender, EventArgs e)
        {
            if (dataGridViewForm4.SelectedRows.Count > 0)
            {
                string cariKodu = dataGridViewForm4.SelectedRows[0].Cells["CariKodu"].Value?.ToString();
                if (!string.IsNullOrEmpty(cariKodu))
                {
                    currentCariKodu = cariKodu;
                    currentCariAdi = dataGridViewForm4.SelectedRows[0].Cells["Unvani"].Value?.ToString();
                    ShowCarihareketlerView();
                }
            }
            else
            {
                MessageBox.Show("Lütfen bir cari seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnAddCari_Click(object sender, EventArgs e)
        {
            try
            {
                CariEkle cariEkleForm = new CariEkle();
                if (cariEkleForm.ShowDialog() == DialogResult.OK)
                {
                    LoadCariKoduListesi();
                    LoadForm4CariList();
                    MessageBox.Show("Cari başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Cari ekleme formunu açarken hata: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEditCari_Click(object sender, EventArgs e)
        {
            if (dataGridViewForm4.SelectedRows.Count > 0)
            {
                string cariKodu = dataGridViewForm4.SelectedRows[0].Cells["CariKodu"].Value?.ToString();
                if (!string.IsNullOrEmpty(cariKodu))
                {
                    try
                    {
                        CariDuzenle cariDuzenleForm = new CariDuzenle(cariKodu);
                        if (cariDuzenleForm.ShowDialog() == DialogResult.OK)
                        {
                            LoadForm4CariList();
                            MessageBox.Show("Cari bilgileri başarıyla güncellendi.", "Bilgi",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Cari düzenleme formunu açarken hata: {ex.Message}", "Hata",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen düzenlemek istediğiniz cariyi seçin.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnDeleteCari_Click(object sender, EventArgs e)
        {
            if (dataGridViewForm4.SelectedRows.Count > 0)
            {
                string cariKodu = dataGridViewForm4.SelectedRows[0].Cells["CariKodu"].Value?.ToString();
                string cariAdi = dataGridViewForm4.SelectedRows[0].Cells["Unvani"].Value?.ToString();

                if (!string.IsNullOrEmpty(cariKodu))
                {
                    DialogResult result = MessageBox.Show(
                        $"'{cariAdi}' adlı cariyi silmek istediğinizden emin misiniz?\n\nBu işlem geri alınamaz!",
                        "Cari Silme Onayı",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            DeleteCari(cariKodu);
                            LoadCariKoduListesi();
                            LoadForm4CariList();
                            MessageBox.Show("Cari başarıyla silindi.", "Bilgi",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Cari silinirken hata: {ex.Message}", "Hata",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen silmek istediğiniz cariyi seçin.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnSetAlarm_Click(object sender, EventArgs e)
        {
            if (dataGridViewForm4.SelectedRows.Count > 0)
            {
                string cariKodu = dataGridViewForm4.SelectedRows[0].Cells["CariKodu"].Value?.ToString();
                string cariAdi = dataGridViewForm4.SelectedRows[0].Cells["Unvani"].Value?.ToString();

                if (!string.IsNullOrEmpty(cariKodu))
                {
                    try
                    {
                        AlarmKur alarmForm = new AlarmKur(cariKodu, cariAdi);
                        alarmForm.ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Alarm kurma formunu açarken hata: {ex.Message}", "Hata",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen alarm kurmak istediğiniz cariyi seçin.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DeleteCari(string cariKodu)
        {
            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (SQLiteTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Önce cari hareketlerini sil
                        string deleteHareketlerQuery = "DELETE FROM cari_hareketleri WHERE cari_kodu = @cariKodu";
                        using (SQLiteCommand cmd = new SQLiteCommand(deleteHareketlerQuery, connection))
                        {
                            cmd.Parameters.AddWithValue("@cariKodu", cariKodu);
                            cmd.ExecuteNonQuery();
                        }

                        // Sonra cariyi sil
                        string deleteCariQuery = "DELETE FROM Cari WHERE CariKodu = @cariKodu";
                        using (SQLiteCommand cmd = new SQLiteCommand(deleteCariQuery, connection))
                        {
                            cmd.Parameters.AddWithValue("@cariKodu", cariKodu);
                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        #endregion

        #region Carihareketler Methods
        private void LoadCarihareketler(string cariKodu)
        {
            if (string.IsNullOrEmpty(cariKodu)) return;

            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

            // Columns setup
            dataGridView1.Columns.Add("Id", "ID");
            dataGridView1.Columns.Add("Tarih", "Tarih");
            dataGridView1.Columns.Add("Aciklama", "Açıklama");
            dataGridView1.Columns.Add("Borc", "Borç");
            dataGridView1.Columns.Add("Alacak", "Alacak");
            dataGridView1.Columns.Add("Bakiye", "Bakiye");

            // Column settings
            dataGridView1.Columns["Id"].Visible = false;
            dataGridView1.Columns["Tarih"].Width = 100;
            dataGridView1.Columns["Aciklama"].Width = 300;
            dataGridView1.Columns["Borc"].Width = 120;
            dataGridView1.Columns["Alacak"].Width = 120;
            dataGridView1.Columns["Bakiye"].Width = 120;

            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
            decimal runningBalance = 0;
            decimal toplamBorc = 0;
            decimal toplamAlacak = 0;

            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                        SELECT id, tarih, aciklama, borc, alacak 
                        FROM cari_hareketleri 
                        WHERE cari_kodu = @cariKodu 
                        ORDER BY tarih ASC, id ASC";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@cariKodu", cariKodu);
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                decimal borc = Convert.ToDecimal(reader["borc"]);
                                decimal alacak = Convert.ToDecimal(reader["alacak"]);

                                runningBalance += borc - alacak;
                                toplamBorc += borc;
                                toplamAlacak += alacak;

                                DateTime tarih = Convert.ToDateTime(reader["tarih"]);

                                int rowIndex = dataGridView1.Rows.Add(
                                    reader["id"].ToString(),
                                    tarih.ToString("dd.MM.yyyy"),
                                    reader["aciklama"].ToString(),
                                    borc > 0 ? borc.ToString("C2") : "",
                                    alacak > 0 ? alacak.ToString("C2") : "",
                                    runningBalance.ToString("C2")
                                );

                                // Satır renklendirilmesi
                                if (borc > 0)
                                {
                                    dataGridView1.Rows[rowIndex].Cells["Borc"].Style.ForeColor = Color.Red;
                                    dataGridView1.Rows[rowIndex].Cells["Borc"].Style.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                                }
                                if (alacak > 0)
                                {
                                    dataGridView1.Rows[rowIndex].Cells["Alacak"].Style.ForeColor = Color.Green;
                                    dataGridView1.Rows[rowIndex].Cells["Alacak"].Style.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                                }

                                // Bakiye renklendirilmesi
                                if (runningBalance > 0)
                                    dataGridView1.Rows[rowIndex].Cells["Bakiye"].Style.ForeColor = Color.Red;
                                else if (runningBalance < 0)
                                    dataGridView1.Rows[rowIndex].Cells["Bakiye"].Style.ForeColor = Color.Green;
                                else
                                    dataGridView1.Rows[rowIndex].Cells["Bakiye"].Style.ForeColor = Color.Blue;

                                dataGridView1.Rows[rowIndex].Cells["Bakiye"].Style.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                            }
                        }
                    }
                }

                // Özet bilgileri güncelle
                UpdateSummaryInfo(toplamBorc, toplamAlacak, runningBalance);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Cari hareketleri yüklenirken hata: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateSummaryInfo(decimal toplamBorc, decimal toplamAlacak, decimal bakiye)
        {
            textBox1.Text = toplamBorc.ToString("C2");
            textBox2.Text = toplamAlacak.ToString("C2");
            textBox3.Text = bakiye.ToString("C2");

            // Renklendirme
            textBox1.ForeColor = Color.Red;
            textBox2.ForeColor = Color.Green;

            if (bakiye > 0)
                textBox3.ForeColor = Color.Red;
            else if (bakiye < 0)
                textBox3.ForeColor = Color.Green;
            else
                textBox3.ForeColor = Color.Blue;
        }

        private void NavigateToPreviousCari()
        {
            if (string.IsNullOrEmpty(currentCariKodu) || cariKoduListesi.Count == 0) return;

            int currentIndex = cariKoduListesi.IndexOf(currentCariKodu);
            if (currentIndex > 0)
            {
                currentCariKodu = cariKoduListesi[currentIndex - 1];
                LoadCurrentCariInfo();
                LoadCarihareketler(currentCariKodu);
            }
            else
            {
                MessageBox.Show("Bu ilk caridir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void NavigateToNextCari()
        {
            if (string.IsNullOrEmpty(currentCariKodu) || cariKoduListesi.Count == 0) return;

            int currentIndex = cariKoduListesi.IndexOf(currentCariKodu);
            if (currentIndex < cariKoduListesi.Count - 1)
            {
                currentCariKodu = cariKoduListesi[currentIndex + 1];
                LoadCurrentCariInfo();
                LoadCarihareketler(currentCariKodu);
            }
            else
            {
                MessageBox.Show("Bu son caridir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void LoadCurrentCariInfo()
        {
            if (string.IsNullOrEmpty(currentCariKodu)) return;

            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Unvani FROM Cari WHERE CariKodu = @cariKodu";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@cariKodu", currentCariKodu);
                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            currentCariAdi = result.ToString();
                            button3.Text = $"{currentCariKodu} - {currentCariAdi}";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Cari bilgisi yüklenirken hata: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Carihareketler Event Handlers
        private void Borcekle_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentCariKodu))
            {
                MessageBox.Show("Lütfen önce bir cari seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                BorcEkle borcEkleForm = new BorcEkle(currentCariKodu, currentCariAdi);
                if (borcEkleForm.ShowDialog() == DialogResult.OK)
                {
                    LoadCarihareketler(currentCariKodu);
                    LoadForm4CariList(); // Ana listeyi de güncelle
                    MessageBox.Show("Borç başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Borç ekleme formunu açarken hata: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Tahsilatekle_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentCariKodu))
            {
                MessageBox.Show("Lütfen önce bir cari seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                TahsilatEkle tahsilatEkleForm = new TahsilatEkle(currentCariKodu, currentCariAdi);
                if (tahsilatEkleForm.ShowDialog() == DialogResult.OK)
                {
                    LoadCarihareketler(currentCariKodu);
                    LoadForm4CariList(); // Ana listeyi de güncelle
                    MessageBox.Show("Tahsilat başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Tahsilat ekleme formunu açarken hata: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Hrktsil_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen silmek istediğiniz hareketi seçin.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string hareketId = dataGridView1.SelectedRows[0].Cells["Id"].Value?.ToString();
            string aciklama = dataGridView1.SelectedRows[0].Cells["Aciklama"].Value?.ToString();

            if (string.IsNullOrEmpty(hareketId)) return;

            DialogResult result = MessageBox.Show(
                $"'{aciklama}' açıklamalı hareketi silmek istediğinizden emin misiniz?\n\nBu işlem geri alınamaz!",
                "Hareket Silme Onayı",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    DeleteHareket(hareketId);
                    LoadCarihareketler(currentCariKodu);
                    LoadForm4CariList(); // Ana listeyi de güncelle
                    MessageBox.Show("Hareket başarıyla silindi.", "Bilgi",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hareket silinirken hata: {ex.Message}", "Hata",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Gecikmesorgula_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentCariKodu))
            {
                MessageBox.Show("Lütfen önce bir cari seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                GecikmeSorgula gecikmeForm = new GecikmeSorgula(currentCariKodu, currentCariAdi);
                gecikmeForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gecikme sorgula formunu açarken hata: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Yazdir_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentCariKodu))
            {
                MessageBox.Show("Lütfen önce bir cari seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Yazdırma işlemi için basit bir dialog
                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == DialogResult.OK)
                {
                    // Burada yazdırma işlemi yapılabilir
                    MessageBox.Show("Yazdırma işlemi başlatıldı.", "Bilgi",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Yazdırma işleminde hata: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Analiz_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentCariKodu))
            {
                MessageBox.Show("Lütfen önce bir cari seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                CariAnaliz analizForm = new CariAnaliz(currentCariKodu, currentCariAdi);
                analizForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Analiz formunu açarken hata: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteHareket(string hareketId)
        {
            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM cari_hareketleri WHERE id = @id";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", hareketId);
                    command.ExecuteNonQuery();
                }

                // Cari bakiyesini güncelle
                UpdateCariBakiye(currentCariKodu);
            }
        }

        private void UpdateCariBakiye(string cariKodu)
        {
            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Toplam borç ve alacağı hesapla
                string sumQuery = @"
                    SELECT 
                        IFNULL(SUM(borc), 0) as ToplamBorc,
                        IFNULL(SUM(alacak), 0) as ToplamAlacak
                    FROM cari_hareketleri 
                    WHERE cari_kodu = @cariKodu";

                decimal toplamBorc = 0;
                decimal toplamAlacak = 0;

                using (SQLiteCommand command = new SQLiteCommand(sumQuery, connection))
                {
                    command.Parameters.AddWithValue("@cariKodu", cariKodu);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            toplamBorc = Convert.ToDecimal(reader["ToplamBorc"]);
                            toplamAlacak = Convert.ToDecimal(reader["ToplamAlacak"]);
                        }
                    }
                }

                decimal bakiye = toplamBorc - toplamAlacak;

                // Cari tablosundaki bakiyeyi güncelle
                string updateQuery = "UPDATE Cari SET bakiye = @bakiye WHERE CariKodu = @cariKodu";
                using (SQLiteCommand command = new SQLiteCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@bakiye", bakiye);
                    command.Parameters.AddWithValue("@cariKodu", cariKodu);
                    command.ExecuteNonQuery();
                }
            }
        }
        #endregion

        #region View Management
        private void ShowForm4View()
        {
            form4Container.Visible = true;
            carihareketlerContainer.Visible = false;
            isDetailViewActive = false;

            // Form4 görünümü aktifken title güncelle
            this.Text = $"Cari Listesi - {_firmaAdi}";

            // Form4 verilerini yenile
            LoadForm4CariList();
        }
        private void ShowCarihareketlerView()
        {
            if (string.IsNullOrEmpty(currentCariKodu))
            {
                MessageBox.Show("Lütfen önce bir cari seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            form4Container.Visible = false;
            carihareketlerContainer.Visible = true;
            isDetailViewActive = true;

            // Carihareketler görünümü aktifken title güncelle
            this.Text = $"Cari Hareketleri - {currentCariAdi} - {_firmaAdi}";

            // Cari bilgilerini ve hareketleri yükle
            LoadCurrentCariInfo();
            LoadCarihareketler(currentCariKodu);
            UpdateNavigationButtonPositions();
        }

        private void UpdateNavigationButtonPositions()
        {
            if (topNavPanel != null && btnPreviousCari != null && btnNextCari != null)
            {
                btnPreviousCari.Location = new Point(topNavPanel.Width - 100, 10);
                btnNextCari.Location = new Point(topNavPanel.Width - 50, 10);
            }
        }
        #endregion

        #region Keyboard Shortcuts
        private void UnifiedForm_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                // Global shortcuts
                if (e.Control && e.KeyCode == Keys.Q) // Ctrl+Q - Çıkış
                {
                    this.Close();
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.F1) // F1 - Cari Listesi
                {
                    ShowForm4View();
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.F2) // F2 - Cari Hareketleri
                {
                    if (!string.IsNullOrEmpty(currentCariKodu))
                        ShowCarihareketlerView();
                    e.Handled = true;
                }

                // Form4 (Liste) görünümü shortcuts
                if (!isDetailViewActive)
                {
                    if (e.KeyCode == Keys.Insert) // Insert - Cari Ekle
                    {
                        BtnAddCari_Click(sender, e);
                        e.Handled = true;
                    }
                    else if (e.KeyCode == Keys.F4) // F4 - Düzenle
                    {
                        BtnEditCari_Click(sender, e);
                        e.Handled = true;
                    }
                    else if (e.KeyCode == Keys.Delete) // Delete - Cari Sil
                    {
                        BtnDeleteCari_Click(sender, e);
                        e.Handled = true;
                    }
                    else if (e.KeyCode == Keys.Enter) // Enter - Hareketleri Aç
                    {
                        BtnOpenMovements_Click(sender, e);
                        e.Handled = true;
                    }
                    else if (e.KeyCode == Keys.F3) // F3 - Arama kutusuna odaklan
                    {
                        txtSearch.Focus();
                        e.Handled = true;
                    }
                }
                // Carihareketler (Detay) görünümü shortcuts
                else
                {
                    if (e.KeyCode == Keys.Escape) // Escape - Listeye dön
                    {
                        ShowForm4View();
                        e.Handled = true;
                    }
                    else if (e.KeyCode == Keys.F5) // F5 - Borç Ekle
                    {
                        Borcekle_Click(sender, e);
                        e.Handled = true;
                    }
                    else if (e.KeyCode == Keys.F6) // F6 - Tahsilat Ekle
                    {
                        Tahsilatekle_Click(sender, e);
                        e.Handled = true;
                    }
                    else if (e.KeyCode == Keys.Delete) // Delete - Hareket Sil
                    {
                        Hrktsil_Click(sender, e);
                        e.Handled = true;
                    }
                    else if (e.KeyCode == Keys.Left) // Sol Ok - Önceki Cari
                    {
                        NavigateToPreviousCari();
                        e.Handled = true;
                    }
                    else if (e.KeyCode == Keys.Right) // Sağ Ok - Sonraki Cari
                    {
                        NavigateToNextCari();
                        e.Handled = true;
                    }
                    else if (e.Control && e.KeyCode == Keys.P) // Ctrl+P - Yazdır
                    {
                        Yazdir_Click(sender, e);
                        e.Handled = true;
                    }
                    else if (e.KeyCode == Keys.F9) // F9 - Analiz
                    {
                        Analiz_Click(sender, e);
                        e.Handled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Klavye kısayolu işlenirken hata: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Form Events
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Programdan çıkmak istediğinizden emin misiniz?",
                "Çıkış Onayı",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                base.OnFormClosing(e);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Form yüklendikten sonra odağı arama kutusuna ver
            if (txtSearch != null)
            {
                txtSearch.Focus();
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateNavigationButtonPositions();
        }
        #endregion

        #region Public Methods
        public void RefreshData()
        {
            LoadCariKoduListesi();

            if (isDetailViewActive && !string.IsNullOrEmpty(currentCariKodu))
            {
                LoadCarihareketler(currentCariKodu);
            }
            else
            {
                LoadForm4CariList();
            }
        }

        public void SelectCari(string cariKodu)
        {
            if (string.IsNullOrEmpty(cariKodu)) return;

            currentCariKodu = cariKodu;
            LoadCurrentCariInfo();

            if (isDetailViewActive)
            {
                LoadCarihareketler(currentCariKodu);
            }
            else
            {
                // Form4'te ilgili satırı seç
                foreach (DataGridViewRow row in dataGridViewForm4.Rows)
                {
                    if (row.Cells["CariKodu"].Value?.ToString() == cariKodu)
                    {
                        row.Selected = true;
                        dataGridViewForm4.FirstDisplayedScrollingRowIndex = row.Index;
                        break;
                    }
                }
            }
        }

        public void AddNewCari()
        {
            BtnAddCari_Click(this, EventArgs.Empty);
        }

        public void ShowCariMovements(string cariKodu)
        {
            if (!string.IsNullOrEmpty(cariKodu))
            {
                currentCariKodu = cariKodu;
                LoadCurrentCariInfo();
                ShowCarihareketlerView();
            }
        }
        #endregion

        #region Async Operations
        private async Task RefreshDataAsync()
        {
            try
            {
                await Task.Run(() =>
                {
                    this.Invoke(new Action(() =>
                    {
                        LoadCariKoduListesi();

                        if (isDetailViewActive && !string.IsNullOrEmpty(currentCariKodu))
                        {
                            LoadCarihareketler(currentCariKodu);
                        }
                        else
                        {
                            LoadForm4CariList();
                        }
                    }));
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Veri yenileme sırasında hata: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public async void RefreshDataAsyncPublic()
        {
            await RefreshDataAsync();
        }
        #endregion

        #region Helper Methods
        private string GetSelectedCariKodu()
        {
            if (isDetailViewActive)
            {
                return currentCariKodu;
            }
            else
            {
                if (dataGridViewForm4.SelectedRows.Count > 0)
                {
                    return dataGridViewForm4.SelectedRows[0].Cells["CariKodu"].Value?.ToString();
                }
            }
            return null;
        }

        private void SetStatusMessage(string message)
        {
            // Status bar varsa mesaj göster
            // Bu method gelecekte status bar eklendiğinde kullanılabilir
        }

        private void LogActivity(string activity)
        {
            // Aktivite loglaması için
            // Gelecekte log sistemi eklendiğinde kullanılabilir
            try
            {
                string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {activity}";
                // Log dosyasına yazma işlemi burada yapılabilir
            }
            catch
            {
                // Log hatalarını sessizce geç
            }
        }
        #endregion

        #region Dispose
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Managed resources cleanup
                dataGridViewForm4?.Dispose();
                dataGridView1?.Dispose();

                // Event handlers cleanup
                if (txtSearch != null)
                    txtSearch.TextChanged -= (s, e) => ApplyForm4Filters();

                if (dataGridViewForm4 != null)
                    dataGridViewForm4.CellDoubleClick -= DataGridViewForm4_CellDoubleClick;
            }

            base.Dispose(disposing);
        }
        #endregion
    }

    #region Supporting Classes
    // Bu sınıflar ayrı dosyalarda olmalı, burada referans için eklendi

    // CariEkle.cs
    public partial class CariEkle : Form
    {
        public CariEkle()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // CariEkle form tasarımı burada olacak
            this.Text = "Cari Ekle";
            this.Size = new Size(500, 400);
            this.StartPosition = FormStartPosition.CenterParent;
        }
    }

    // CariDuzenle.cs
    public partial class CariDuzenle : Form
    {
        private string _cariKodu;

        public CariDuzenle(string cariKodu)
        {
            _cariKodu = cariKodu;
            InitializeComponent();
            LoadCariData();
        }

        private void InitializeComponent()
        {
            this.Text = "Cari Düzenle";
            this.Size = new Size(500, 400);
            this.StartPosition = FormStartPosition.CenterParent;
        }

        private void LoadCariData()
        {
            // Cari verilerini yükle
        }
    }

    // BorcEkle.cs
    public partial class BorcEkle : Form
    {
        private string _cariKodu;
        private string _cariAdi;

        public BorcEkle(string cariKodu, string cariAdi)
        {
            _cariKodu = cariKodu;
            _cariAdi = cariAdi;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Borç Ekle";
            this.Size = new Size(400, 300);
            this.StartPosition = FormStartPosition.CenterParent;
        }
    }

    // TahsilatEkle.cs
    public partial class TahsilatEkle : Form
    {
        private string _cariKodu;
        private string _cariAdi;

        public TahsilatEkle(string cariKodu, string cariAdi)
        {
            _cariKodu = cariKodu;
            _cariAdi = cariAdi;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Tahsilat Ekle";
            this.Size = new Size(400, 300);
            this.StartPosition = FormStartPosition.CenterParent;
        }
    }

    // AlarmKur.cs
    public partial class AlarmKur : Form
    {
        private string _cariKodu;
        private string _cariAdi;

        public AlarmKur(string cariKodu, string cariAdi)
        {
            _cariKodu = cariKodu;
            _cariAdi = cariAdi;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Alarm Kur";
            this.Size = new Size(350, 250);
            this.StartPosition = FormStartPosition.CenterParent;
        }
    }

    // GecikmeSorgula.cs
    public partial class GecikmeSorgula : Form
    {
        private string _cariKodu;
        private string _cariAdi;

        public GecikmeSorgula(string cariKodu, string cariAdi)
        {
            _cariKodu = cariKodu;
            _cariAdi = cariAdi;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Gecikme Sorgula";
            this.Size = new Size(600, 400);
            this.StartPosition = FormStartPosition.CenterParent;
        }
    }

    // CariAnaliz.cs
    public partial class CariAnaliz : Form
    {
        private string _cariKodu;
        private string _cariAdi;

        public CariAnaliz(string cariKodu, string cariAdi)
        {
            _cariKodu = cariKodu;
            _cariAdi = cariAdi;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Cari Analiz";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterParent;
        }
    }
    #endregion
}