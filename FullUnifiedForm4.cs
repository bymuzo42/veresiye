// FullUnifiedForm4.cs - Tam özellikli, resources hatası olmayan versiyon
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace Veresiye2025
{
    public partial class FullUnifiedForm4 : Form
    {
        #region Private Fields
        private string _firmaAdi;
        private List<string> cariKoduListesi = new List<string>();
        private string currentCariKodu;
        private string currentCariAdi;

        // UI Panels
        private Panel mainContainer;
        private Panel navigationPanel;
        private Panel contentPanel;
        private Panel toolbarPanel;

        // Navigation State
        private bool isDetailViewActive = false;

        // Content Panels
        private Panel listViewPanel;
        private Panel detailViewPanel;

        // Navigation Controls
        private Label lblTitle;
        private Label lblBreadcrumb;
        private Guna2Button btnBack;
        private Guna2Button btnPrevious;
        private Guna2Button btnNext;

        // DataGridViews
        private DataGridView dgvCariList;
        private DataGridView dgvMovements;

        // Search Controls
        private Panel searchPanel;
        private Guna2ComboBox cmbSearchType;
        private Guna2TextBox txtSearch;
        private Guna2ComboBox cmbAccountFilter;

        // Summary Controls
        private Panel summaryPanel;
        private Label lblCurrentCari;
        private TextBox txtTotalDebt;
        private TextBox txtTotalPayment;
        private TextBox txtBalance;

        // Toolbar Buttons
        private Guna2Button btnAddCari;
        private Guna2Button btnEditCari;
        private Guna2Button btnDeleteCari;
        private Guna2Button btnViewMovements;
        private Guna2Button btnAddDebt;
        private Guna2Button btnAddPayment;
        #endregion

        #region Constructor
        public FullUnifiedForm4(string firmaAdi)
        {
            _firmaAdi = firmaAdi;
            InitializeFullComponents();
            SetupEventHandlers();
            LoadInitialData();
        }
        #endregion

        #region Initialize Components
        private void InitializeFullComponents()
        {
            // Form basic properties - Resources kullanmadan
            this.Text = $"Ana Ekran - {_firmaAdi}";
            this.Size = new Size(1400, 900);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(245, 246, 250);
            this.Font = new Font("Segoe UI", 9F);
            this.MinimumSize = new Size(1200, 700);
            this.WindowState = FormWindowState.Maximized;

            CreateMainContainer();
            CreateNavigationPanel();
            CreateContentPanels();
            CreateToolbarPanel();

            // Initial view
            ShowListView();
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

        private void CreateNavigationPanel()
        {
            navigationPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 70,
                BackColor = Color.FromArgb(67, 162, 243)
            };

            // Back button
            btnBack = new Guna2Button
            {
                Text = "◀ Geri",
                Size = new Size(100, 40),
                Location = new Point(20, 15),
                FillColor = Color.FromArgb(52, 144, 220),
                ForeColor = Color.White,
                BorderRadius = 10,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Visible = false
            };

            // Title
            lblTitle = new Label
            {
                Text = "Cari Listesi",
                Location = new Point(140, 15),
                Size = new Size(400, 25),
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Transparent
            };

            // Breadcrumb
            lblBreadcrumb = new Label
            {
                Text = "Ana Sayfa > Cari Listesi",
                Location = new Point(140, 45),
                Size = new Size(400, 20),
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(220, 220, 220),
                BackColor = Color.Transparent
            };

            // Navigation buttons
            btnPrevious = new Guna2Button
            {
                Text = "◀",
                Size = new Size(45, 40),
                FillColor = Color.FromArgb(52, 144, 220),
                ForeColor = Color.White,
                BorderRadius = 10,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Visible = false,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };

            btnNext = new Guna2Button
            {
                Text = "▶",
                Size = new Size(45, 40),
                FillColor = Color.FromArgb(52, 144, 220),
                ForeColor = Color.White,
                BorderRadius = 10,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Visible = false,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };

            navigationPanel.Controls.AddRange(new Control[] {
                btnBack, lblTitle, lblBreadcrumb, btnPrevious, btnNext
            });

            mainContainer.Controls.Add(navigationPanel);
        }

        private void CreateContentPanels()
        {
            contentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };

            CreateListViewPanel();
            CreateDetailViewPanel();

            contentPanel.Controls.AddRange(new Control[] { detailViewPanel, listViewPanel });
            mainContainer.Controls.Add(contentPanel);
        }

        private void CreateListViewPanel()
        {
            listViewPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Visible = true
            };

            // Search Panel
            CreateSearchPanel();

            // DataGridView
            dgvCariList = new DataGridView
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
                GridColor = Color.FromArgb(240, 240, 240),
                Font = new Font("Segoe UI", 9),
                ColumnHeadersHeight = 40,
                RowTemplate = { Height = 35 }
            };

            // Style DataGridView
            dgvCariList.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(67, 162, 243),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };

            dgvCariList.DefaultCellStyle = new DataGridViewCellStyle
            {
                SelectionBackColor = Color.FromArgb(67, 162, 243, 50),
                SelectionForeColor = Color.Black
            };

            listViewPanel.Controls.AddRange(new Control[] { dgvCariList, searchPanel });
        }

        private void CreateSearchPanel()
        {
            searchPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = Color.FromArgb(248, 249, 250),
                Padding = new Padding(20)
            };

            // Search Type
            Label lblSearchType = new Label
            {
                Text = "Arama:",
                Location = new Point(20, 25),
                Size = new Size(60, 20),
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };

            cmbSearchType = new Guna2ComboBox
            {
                Location = new Point(90, 20),
                Size = new Size(150, 30),
                DropDownStyle = ComboBoxStyle.DropDownList,
                BorderRadius = 5
            };
            cmbSearchType.Items.AddRange(new string[] { "Unvan ile Sorgu", "Kod ile Sorgu", "İl ile Sorgu" });
            cmbSearchType.SelectedIndex = 0;

            // Search TextBox
            txtSearch = new Guna2TextBox
            {
                Location = new Point(260, 20),
                Size = new Size(200, 30),
                PlaceholderText = "Arama yapın...",
                BorderRadius = 5
            };

            // Account Filter
            Label lblAccount = new Label
            {
                Text = "Hesap:",
                Location = new Point(480, 25),
                Size = new Size(60, 20),
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };

            cmbAccountFilter = new Guna2ComboBox
            {
                Location = new Point(550, 20),
                Size = new Size(120, 30),
                DropDownStyle = ComboBoxStyle.DropDownList,
                BorderRadius = 5
            };
            cmbAccountFilter.Items.AddRange(new string[] { "Tümü", "Aktif Hesaplar", "Pasif Hesaplar" });
            cmbAccountFilter.SelectedIndex = 0;

            searchPanel.Controls.AddRange(new Control[] {
                lblSearchType, cmbSearchType, txtSearch, lblAccount, cmbAccountFilter
            });
        }

        private void CreateDetailViewPanel()
        {
            detailViewPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Visible = false
            };

            // Summary Panel
            CreateSummaryPanel();

            // Movements DataGridView
            dgvMovements = new DataGridView
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
                GridColor = Color.FromArgb(240, 240, 240),
                Font = new Font("Segoe UI", 9),
                ColumnHeadersHeight = 40,
                RowTemplate = { Height = 35 }
            };

            dgvMovements.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(67, 162, 243),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };

            detailViewPanel.Controls.AddRange(new Control[] { dgvMovements, summaryPanel });
        }

        private void CreateSummaryPanel()
        {
            summaryPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 120,
                BackColor = Color.FromArgb(248, 249, 250),
                Padding = new Padding(20)
            };

            // Current Cari Label
            lblCurrentCari = new Label
            {
                Location = new Point(20, 15),
                Size = new Size(400, 30),
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(67, 162, 243),
                Text = "Seçili Cari"
            };

            // Summary cards
            Panel cardPanel = new Panel
            {
                Location = new Point(20, 50),
                Size = new Size(800, 50),
                BackColor = Color.Transparent
            };

            // Create summary cards
            Panel debtCard = CreateSummaryCard("Toplam Borç", "₺0,00", Color.Red, 0);
            Panel paymentCard = CreateSummaryCard("Toplam Tahsilat", "₺0,00", Color.Green, 270);
            Panel balanceCard = CreateSummaryCard("Net Bakiye", "₺0,00", Color.Blue, 540);

            txtTotalDebt = (TextBox)debtCard.Controls[1];
            txtTotalPayment = (TextBox)paymentCard.Controls[1];
            txtBalance = (TextBox)balanceCard.Controls[1];

            cardPanel.Controls.AddRange(new Control[] { debtCard, paymentCard, balanceCard });

            summaryPanel.Controls.AddRange(new Control[] { lblCurrentCari, cardPanel });
        }

        private Panel CreateSummaryCard(string title, string value, Color color, int xPos)
        {
            Panel card = new Panel
            {
                Location = new Point(xPos, 0),
                Size = new Size(260, 50),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            Label lblTitle = new Label
            {
                Text = title,
                Location = new Point(10, 5),
                Size = new Size(240, 15),
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                ForeColor = Color.Gray
            };

            TextBox txtValue = new TextBox
            {
                Text = value,
                Location = new Point(10, 25),
                Size = new Size(240, 20),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = color,
                ReadOnly = true,
                BorderStyle = BorderStyle.None,
                BackColor = Color.White
            };

            card.Controls.AddRange(new Control[] { lblTitle, txtValue });
            return card;
        }

        private void CreateToolbarPanel()
        {
            toolbarPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 60,
                BackColor = Color.FromArgb(67, 162, 243),
                Padding = new Padding(20, 10, 20, 10)
            };

            // List View Buttons
            btnAddCari = CreateToolbarButton("➕ Cari Ekle", 20, true);
            btnEditCari = CreateToolbarButton("✏️ Düzenle", 150, true);
            btnDeleteCari = CreateToolbarButton("🗑️ Sil", 280, true);
            btnViewMovements = CreateToolbarButton("📋 Hareketler", 410, true);

            // Detail View Buttons
            btnAddDebt = CreateToolbarButton("💰 Borç Ekle", 20, false);
            btnAddPayment = CreateToolbarButton("💳 Tahsilat", 150, false);

            toolbarPanel.Controls.AddRange(new Control[] {
                btnAddCari, btnEditCari, btnDeleteCari, btnViewMovements,
                btnAddDebt, btnAddPayment
            });

            mainContainer.Controls.Add(toolbarPanel);
        }

        private Guna2Button CreateToolbarButton(string text, int x, bool visible)
        {
            return new Guna2Button
            {
                Text = text,
                Location = new Point(x, 10),
                Size = new Size(120, 40),
                FillColor = Color.FromArgb(52, 144, 220),
                ForeColor = Color.White,
                BorderRadius = 10,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Visible = visible
            };
        }
        #endregion

        #region Event Handlers Setup
        private void SetupEventHandlers()
        {
            // Navigation
            btnBack.Click += (s, e) => ShowListView();
            btnPrevious.Click += (s, e) => NavigateToPreviousCari();
            btnNext.Click += (s, e) => NavigateToNextCari();

            // DataGridView Events
            dgvCariList.CellDoubleClick += DgvCariList_CellDoubleClick;

            // Toolbar Events
            btnViewMovements.Click += BtnViewMovements_Click;
            btnAddDebt.Click += BtnAddDebt_Click;
            btnAddPayment.Click += BtnAddPayment_Click;

            // Search
            txtSearch.TextChanged += (s, e) => ApplyFilters();
            cmbAccountFilter.SelectedIndexChanged += (s, e) => ApplyFilters();
        }
        #endregion

        #region Data Loading
        private void LoadInitialData()
        {
            LoadCariKoduListesi();
            LoadCariListData();
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

        private void LoadCariListData()
        {
            dgvCariList.Columns.Clear();
            dgvCariList.Rows.Clear();

            // Setup columns
            dgvCariList.Columns.Add("CariKodu", "Cari Kodu");
            dgvCariList.Columns.Add("Unvani", "Unvanı");
            dgvCariList.Columns.Add("Il", "İl");
            dgvCariList.Columns.Add("Telefon", "Telefon");
            dgvCariList.Columns.Add("Bakiye", "Bakiye");

            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";

            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                        SELECT CariKodu, Unvani, Il, Telefon, 
                               IFNULL(bakiye, 0) AS Bakiye
                        FROM Cari
                        ORDER BY Unvani ASC";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            decimal bakiye = Convert.ToDecimal(reader["Bakiye"]);

                            int rowIndex = dgvCariList.Rows.Add(
                                reader["CariKodu"].ToString(),
                                reader["Unvani"].ToString(),
                                reader["Il"].ToString(),
                                reader["Telefon"].ToString(),
                                bakiye.ToString("C2")
                            );

                            // Balance color coding
                            var row = dgvCariList.Rows[rowIndex];
                            if (bakiye > 0)
                                row.Cells["Bakiye"].Style.ForeColor = Color.Red;
                            else if (bakiye < 0)
                                row.Cells["Bakiye"].Style.ForeColor = Color.Green;
                            else
                                row.Cells["Bakiye"].Style.ForeColor = Color.Blue;
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
        #endregion

        #region View Management
        private void ShowListView()
        {
            isDetailViewActive = false;
            lblTitle.Text = "Cari Listesi";
            lblBreadcrumb.Text = "Ana Sayfa > Cari Listesi";

            btnBack.Visible = false;
            btnPrevious.Visible = false;
            btnNext.Visible = false;

            ShowListViewButtons();

            detailViewPanel.Visible = false;
            listViewPanel.Visible = true;
            listViewPanel.BringToFront();
        }

        private void ShowDetailView()
        {
            isDetailViewActive = true;
            lblTitle.Text = "Cari Hareketleri";
            lblBreadcrumb.Text = $"Ana Sayfa > Cari Listesi > {currentCariAdi}";
            lblCurrentCari.Text = currentCariAdi;

            btnBack.Visible = true;
            btnPrevious.Visible = true;
            btnNext.Visible = true;

            ShowDetailViewButtons();

            listViewPanel.Visible = false;
            detailViewPanel.Visible = true;
            detailViewPanel.BringToFront();

            LoadCariMovements(currentCariKodu);
        }

        private void ShowListViewButtons()
        {
            btnAddCari.Visible = true;
            btnEditCari.Visible = true;
            btnDeleteCari.Visible = true;
            btnViewMovements.Visible = true;

            btnAddDebt.Visible = false;
            btnAddPayment.Visible = false;
        }

        private void ShowDetailViewButtons()
        {
            btnAddCari.Visible = false;
            btnEditCari.Visible = false;
            btnDeleteCari.Visible = false;
            btnViewMovements.Visible = false;

            btnAddDebt.Visible = true;
            btnAddPayment.Visible = true;
        }
        #endregion

        #region Event Handlers
        private void DgvCariList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var selectedRow = dgvCariList.Rows[e.RowIndex];
                currentCariKodu = selectedRow.Cells["CariKodu"].Value?.ToString();
                currentCariAdi = selectedRow.Cells["Unvani"].Value?.ToString();

                if (!string.IsNullOrEmpty(currentCariKodu))
                {
                    ShowDetailView();
                }
            }
        }

        private void BtnViewMovements_Click(object sender, EventArgs e)
        {
            if (dgvCariList.CurrentRow != null)
            {
                currentCariKodu = dgvCariList.CurrentRow.Cells["CariKodu"].Value?.ToString();
                currentCariAdi = dgvCariList.CurrentRow.Cells["Unvani"].Value?.ToString();

                if (!string.IsNullOrEmpty(currentCariKodu))
                {
                    ShowDetailView();
                }
            }
            else
            {
                MessageBox.Show("Lütfen bir cari seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnAddDebt_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Borç ekleme formu açılacak: {currentCariAdi}", "Bilgi",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnAddPayment_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Tahsilat formu açılacak: {currentCariAdi}", "Bilgi",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void NavigateToPreviousCari()
        {
            if (cariKoduListesi.Count == 0) return;

            int currentIndex = cariKoduListesi.IndexOf(currentCariKodu);
            if (currentIndex > 0)
            {
                currentCariKodu = cariKoduListesi[currentIndex - 1];
                currentCariAdi = GetCariAdi(currentCariKodu);
                LoadCariMovements(currentCariKodu);
                lblCurrentCari.Text = currentCariAdi;
            }
        }

        private void NavigateToNextCari()
        {
            if (cariKoduListesi.Count == 0) return;

            int currentIndex = cariKoduListesi.IndexOf(currentCariKodu);
            if (currentIndex < cariKoduListesi.Count - 1)
            {
                currentCariKodu = cariKoduListesi[currentIndex + 1];
                currentCariAdi = GetCariAdi(currentCariKodu);
                LoadCariMovements(currentCariKodu);
                lblCurrentCari.Text = currentCariAdi;
            }
        }
        #endregion

        #region Helper Methods
        private string GetCariAdi(string cariKodu)
        {
            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";

            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Unvani FROM Cari WHERE CariKodu = @CariKodu";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CariKodu", cariKodu);
                        return command.ExecuteScalar()?.ToString() ?? "";
                    }
                }
            }
            catch
            {
                return "";
            }
        }

        private void LoadCariMovements(string cariKodu)
        {
            // Placeholder - movements loading işlemi
            dgvMovements.Columns.Clear();
            dgvMovements.Columns.Add("Tarih", "Tarih");
            dgvMovements.Columns.Add("Tur", "Tür");
            dgvMovements.Columns.Add("Aciklama", "Açıklama");
            dgvMovements.Columns.Add("Borc", "Borç");
            dgvMovements.Columns.Add("Tahsilat", "Tahsilat");

            // Sample data
            dgvMovements.Rows.Add("01.01.2024", "Borç", "Test borç", "₺1.000,00", "");
            dgvMovements.Rows.Add("02.01.2024", "Tahsilat", "Test tahsilat", "", "₺500,00");

            // Update summary
            txtTotalDebt.Text = "₺1.000,00";
            txtTotalPayment.Text = "₺500,00";
            txtBalance.Text = "₺500,00";
            txtBalance.ForeColor = Color.Red;
        }

        private void ApplyFilters()
        {
            // Basit filtreleme
            string searchValue = txtSearch.Text.ToLower();

            foreach (DataGridViewRow row in dgvCariList.Rows)
            {
                if (row.IsNewRow) continue;

                bool visible = true;

                if (!string.IsNullOrEmpty(searchValue))
                {
                    string unvan = row.Cells["Unvani"].Value?.ToString()?.ToLower() ?? "";
                    visible = unvan.Contains(searchValue);
                }

                row.Visible = visible;
            }
        }
        #endregion

        #region Form Events
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
        }
        #endregion
    }
}