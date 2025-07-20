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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Veresiye2025
{
    public partial class CariEmanet : Form
    {
        public string cariKodu;
        public string cariAdi;
        public string fotografYolu;

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

        // Kapat butonu
        public Button btnClose;

        public CariEmanet(string cariKodu, string cariAdi)
        {
            InitializeComponent();
            // DataGridView sağ tık menüsünü konfigüre et
            ConfigureContextMenu();

            this.cariKodu = cariKodu;
            this.cariAdi = cariAdi;

            // Form köşelerini yuvarlatma
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

            // ESC tuşu ile kapatma
            this.KeyPreview = true;
            this.KeyDown += (s, e) => {
                if (e.KeyCode == Keys.Escape) this.Close();
            };

            // Ana formla uyumlu tema uygula
            ApplyMainFormCompatibleTheme();
        }

        //Datagridwiev Sağ Tık Ekleme Olayı Başlangıç.
        // DataGridView için sağ tık menüsü oluştur
        public void ConfigureContextMenu()
        {
            // Yeni bir ContextMenuStrip oluştur
            ContextMenuStrip contextMenu = new ContextMenuStrip();

            // Menü öğelerini ekle
            ToolStripMenuItem menuDuzenle = new ToolStripMenuItem("Düzenle");
            menuDuzenle.Image = Properties.Resources.edit; // İkonunuz varsa
            menuDuzenle.Font = new Font("Segoe UI", 9F);
            menuDuzenle.Click += (s, e) =>
            {
                if (dgvEmanetler.SelectedRows.Count > 0)
                {
                    int emanetId = Convert.ToInt32(dgvEmanetler.SelectedRows[0].Cells["ID"].Value);
                    EmanetDetayGoster(emanetId);
                    LoadEmanetler();

                    // Bildirim kontrolünü güncelle
                    if (this.Owner is Carihareketler carihareketlerForm)
                    {
                        carihareketlerForm.EmanetDurumunuKontrolEt(cariKodu);
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen düzenlemek için bir emanet seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };

            ToolStripMenuItem menuSil = new ToolStripMenuItem("Sil");
            menuSil.Image = Properties.Resources.delete; // İkonunuz varsa
            menuSil.Font = new Font("Segoe UI", 9F);
            menuSil.Click += (s, e) =>
            {
                if (dgvEmanetler.SelectedRows.Count > 0)
                {
                    int emanetId = Convert.ToInt32(dgvEmanetler.SelectedRows[0].Cells["ID"].Value);

                    if (MessageBox.Show("Seçili emanet kaydını silmek istediğinize emin misiniz?", "Onay",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {
                            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
                            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                            {
                                connection.Open();
                                string deleteQuery = "DELETE FROM Emanetler WHERE ID = @ID";
                                using (SQLiteCommand command = new SQLiteCommand(deleteQuery, connection))
                                {
                                    command.Parameters.AddWithValue("@ID", emanetId);
                                    command.ExecuteNonQuery();
                                }
                                connection.Close();
                            }

                            MessageBox.Show("Emanet kaydı başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadEmanetler();

                            // Bildirim kontrolünü güncelle
                            if (this.Owner is Carihareketler carihareketlerForm)
                            {
                                carihareketlerForm.EmanetDurumunuKontrolEt(cariKodu);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Silme sırasında bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen silmek için bir emanet seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };

            ToolStripMenuItem menuAlarmKur = new ToolStripMenuItem("Alarm Kur");
            menuAlarmKur.Image = Properties.Resources.clock; // İkonunuz varsa
            menuAlarmKur.Font = new Font("Segoe UI", 9F);
            menuAlarmKur.Click += (s, e) =>
            {
                if (dgvEmanetler.SelectedRows.Count > 0)
                {
                    int emanetId = Convert.ToInt32(dgvEmanetler.SelectedRows[0].Cells["ID"].Value);
                    string emanetAdi = dgvEmanetler.SelectedRows[0].Cells["EmanetAdi"].Value.ToString();

                    MessageBox.Show($"\"{emanetAdi}\" için alarm kurma özelliği yakında eklenecek.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Burada AlarmKur metodunu çağırabilirsiniz
                    // AlarmKur(emanetId, emanetAdi);
                }
                else
                {
                    MessageBox.Show("Lütfen alarm kurmak için bir emanet seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };

            // Menü öğelerini ana menüye ekle
            contextMenu.Items.Add(menuDuzenle);
            contextMenu.Items.Add(menuSil);
            contextMenu.Items.Add(new ToolStripSeparator()); // Ayırıcı çizgi
            contextMenu.Items.Add(menuAlarmKur);

            // ContextMenuStrip'i DataGridView'e ata
            dgvEmanetler.ContextMenuStrip = contextMenu;

            // Sağ tık olayını da kontrol edelim
            dgvEmanetler.CellMouseDown += (s, e) =>
            {
                // Sağ tık yapıldıysa ve geçerli bir satırsa
                if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
                {
                    // Tıklanan satırı seç
                    dgvEmanetler.ClearSelection();
                    dgvEmanetler.Rows[e.RowIndex].Selected = true;

                    // Sağ tık menüsünü gösterme konumu şimdiden ayarlanmış olduğu için
                    // özel bir konum ayarlamamıza gerek yok
                }
            };
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
            // Eski kapat butonunu kaldır
            if (btnClose != null && pnlHeader.Controls.Contains(btnClose))
            {
                pnlHeader.Controls.Remove(btnClose);
                btnClose.Dispose();
            }

            // Özel bir panel oluştur (daha kolay şekillendirmek için)
            Panel closePanel = new Panel
            {
                Size = new Size(30, 30),
                Location = new Point(pnlHeader.Width - 40, 10), // Sağ üst köşeye yakın
                BackColor = Color.FromArgb(232, 17, 35), // Kırmızı arka plan
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };

            // Paneli yuvarlatılmış köşeli yap
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(0, 0, closePanel.Width, closePanel.Height);
            closePanel.Region = new Region(path);

            // X işareti için label ekle
            Label lblX = new Label
            {
                Text = "x", // Unicode çarpı işareti
                Font = new Font("Arial", 12, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                Cursor = Cursors.Hand
            };

            closePanel.Controls.Add(lblX);

            // Panel tıklandığında formu kapat
            closePanel.Click += (s, e) => {
                if (MessageBox.Show("Formu kapatmak istediğinize emin misiniz?", "Onay",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.Close();
                }
            };

            lblX.Click += (s, e) => {
                if (MessageBox.Show("Formu kapatmak istediğinize emin misiniz?", "Onay",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.Close();
                }
            };

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

            // Buton referansını kapat butonuna ata
            btnClose = new Button { Visible = false }; // Sadece referans olması için
            pnlHeader.Controls.Add(btnClose);
        }

        // Ana formla uyumlu tema uygula
        public void ApplyMainFormCompatibleTheme()
        {
            // Form arka plan rengi
            this.BackColor = Color.WhiteSmoke;

            // Ana panel
            pnlMain.BackColor = Color.WhiteSmoke;

            // Header panel
            pnlHeader.BackColor = Color.FromArgb(28, 141, 243); // Ana formdaki mavi tone

            // Header panel'e yuvarlatılmış köşeler ekle
            pnlHeader.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, pnlHeader.Width, pnlHeader.Height, 20, 20));
            pnlHeader.Resize += (s, e) => {
                pnlHeader.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, pnlHeader.Width, pnlHeader.Height, 20, 20));
            };

            // Form paneli
            pnlForm.BackColor = Color.LightGray;

            // Label text renkleri
            foreach (Control control in pnlForm.Controls)
            {
                if (control is Label)
                {
                    control.ForeColor = Color.Black;
                }
            }

            // TextBox stilleri
            txtEmanetAdi.BackColor = Color.White;
            txtEmanetAdi.ForeColor = Color.Black;
            txtEmanetAdi.BorderStyle = BorderStyle.FixedSingle;

            txtAciklama.BackColor = Color.White;
            txtAciklama.ForeColor = Color.Black;
            txtAciklama.BorderStyle = BorderStyle.FixedSingle;

            // ComboBox stilleri
            cmbDurum.BackColor = Color.White;
            cmbDurum.ForeColor = Color.Black;
            cmbDurum.FlatStyle = FlatStyle.Flat;

            // DateTimePicker stilleri
            dtpEmanetTarihi.CalendarMonthBackground = Color.White;
            dtpGeriAlmaTarihi.CalendarMonthBackground = Color.White;

            // PictureBox kenar rengi
            picEmanet.BackColor = Color.White;
            picEmanet.BorderStyle = BorderStyle.FixedSingle;

            // DataGridView stilleri
            dgvEmanetler.BackgroundColor = Color.White;
            dgvEmanetler.ForeColor = Color.Black;
            dgvEmanetler.GridColor = Color.LightGray;
            dgvEmanetler.BorderStyle = BorderStyle.None;

            dgvEmanetler.EnableHeadersVisualStyles = false;
            dgvEmanetler.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(28, 141, 243);
            dgvEmanetler.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvEmanetler.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 122, 204);

            dgvEmanetler.DefaultCellStyle.BackColor = Color.White;
            dgvEmanetler.DefaultCellStyle.ForeColor = Color.Black;
            dgvEmanetler.DefaultCellStyle.SelectionBackColor = Color.FromArgb(28, 141, 243);
            dgvEmanetler.DefaultCellStyle.SelectionForeColor = Color.White;

            dgvEmanetler.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke;

            // Buton stilleri
            btnKaydet.BackColor = Color.FromArgb(28, 141, 243);
            btnIptal.BackColor = Color.FromArgb(255, 80, 80);
            btnFotografEkle.BackColor = Color.FromArgb(28, 141, 243);
        }

        public void CariEmanet_Load(object sender, EventArgs e)
        {
            try
            {
                // Form başlığını ayarla
                lblTitle.Text = "EMANET YÖNETİMİ - " + cariAdi;

                // Tarih kontrollerini ayarla
                dtpEmanetTarihi.Value = DateTime.Now;
                dtpGeriAlmaTarihi.Value = DateTime.Now.AddDays(7);
                dtpGeriAlmaTarihi.Checked = false;

                // Durum combobox'ı
                cmbDurum.SelectedIndex = 0; // Varsayılan olarak "Teslim Edilmedi" seçili

                // DataGridView sütun ayarları
                ConfigureDataGridView();

                // Var olan emanetleri yükle
                LoadEmanetler();

                // Kontrol stillerini güzelleştir
                StyleControls();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Form yüklenirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Kontrollerin stillerini güzelleştir
        public void StyleControls()
        {
            // Butonları güzelleştir
            btnKaydet.FlatStyle = FlatStyle.Flat;
            btnKaydet.FlatAppearance.BorderSize = 0;
            btnKaydet.BackColor = Color.FromArgb(28, 141, 243);
            btnKaydet.ForeColor = Color.White;
            btnKaydet.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnKaydet.Cursor = Cursors.Hand;

            btnIptal.FlatStyle = FlatStyle.Flat;
            btnIptal.FlatAppearance.BorderSize = 0;
            btnIptal.BackColor = Color.FromArgb(255, 80, 80);
            btnIptal.ForeColor = Color.White;
            btnIptal.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnIptal.Cursor = Cursors.Hand;

            btnFotografEkle.FlatStyle = FlatStyle.Flat;
            btnFotografEkle.FlatAppearance.BorderSize = 0;
            btnFotografEkle.BackColor = Color.FromArgb(28, 141, 243);
            btnFotografEkle.ForeColor = Color.White;
            btnFotografEkle.Cursor = Cursors.Hand;

            // Textbox kenarları
            txtEmanetAdi.BorderStyle = BorderStyle.FixedSingle;
            txtAciklama.BorderStyle = BorderStyle.FixedSingle;

            // Label fontları
            foreach (Control control in pnlForm.Controls)
            {
                if (control is Label)
                {
                    control.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                }
            }
        }

        public void ConfigureDataGridView()
        {
            // DataGridView görünüm ayarları
            dgvEmanetler.AutoGenerateColumns = false;
            dgvEmanetler.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvEmanetler.RowHeadersVisible = false;
            dgvEmanetler.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEmanetler.AllowUserToAddRows = false;
            dgvEmanetler.AllowUserToDeleteRows = false;
            dgvEmanetler.ReadOnly = true;
            dgvEmanetler.Columns.Clear();

            // Sütunları ekle
            dgvEmanetler.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ID",
                DataPropertyName = "ID",
                Visible = false
            });

            dgvEmanetler.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "EmanetAdi",
                DataPropertyName = "EmanetAdi",
                HeaderText = "Emanet Adı",
                Width = 200,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgvEmanetler.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "EmanetTarihi",
                DataPropertyName = "EmanetTarihi",
                HeaderText = "Emanet Tarihi",
                Width = 120
            });

            dgvEmanetler.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "GeriAlmaTarihi",
                DataPropertyName = "GeriAlmaTarihi",
                HeaderText = "Geri Alma Tarihi",
                Width = 120
            });

            dgvEmanetler.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Durum",
                DataPropertyName = "Durum",
                HeaderText = "Durum",
                Width = 150
            });

            dgvEmanetler.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Aciklama",
                DataPropertyName = "Aciklama",
                HeaderText = "Açıklama",
                Width = 250,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
        }

        public void btnFotografEkle_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
                openFileDialog.Title = "Emanet Fotoğrafı Seç";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Fotoğrafı göster
                        picEmanet.Image = Image.FromFile(openFileDialog.FileName);

                        // Dosya yolunu sakla
                        string emanetDizini = Path.Combine(Application.StartupPath, "EmanetFotograflari");

                        // Klasör yoksa oluştur
                        if (!Directory.Exists(emanetDizini))
                            Directory.CreateDirectory(emanetDizini);

                        // Benzersiz dosya adı oluştur
                        string fileName = $"{cariKodu}_{DateTime.Now:yyyyMMddHHmmss}{Path.GetExtension(openFileDialog.FileName)}";
                        string hedefYol = Path.Combine(emanetDizini, fileName);

                        // Dosyayı kopyala
                        File.Copy(openFileDialog.FileName, hedefYol, true);

                        // Dosya yolunu sakla
                        fotografYolu = hedefYol;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Fotoğraf yüklenirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        public void btnKaydet_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmanetAdi.Text))
            {
                MessageBox.Show("Lütfen emanet adı/tanımı giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmanetAdi.Focus();
                return;
            }

            try
            {
                string emanetAdi = txtEmanetAdi.Text;
                string emanetTarihi = dtpEmanetTarihi.Value.ToString("yyyy-MM-dd");
                string geriAlmaTarihi = dtpGeriAlmaTarihi.Checked ?
                                       dtpGeriAlmaTarihi.Value.ToString("yyyy-MM-dd") : null;
                string aciklama = txtAciklama.Text;
                string durum = cmbDurum.SelectedItem.ToString();

                string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Emanetler tablosunun varlığını kontrol et, yoksa oluştur
                    CheckAndCreateEmanetlerTable(connection);

                    string query = @"
                        INSERT INTO Emanetler
                        (CariKodu, EmanetAdi, EmanetTarihi, GeriAlmaTarihi, Aciklama, Durum, FotografYolu)
                        VALUES
                        (@CariKodu, @EmanetAdi, @EmanetTarihi, @GeriAlmaTarihi, @Aciklama, @Durum, @FotografYolu)";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CariKodu", cariKodu);
                        command.Parameters.AddWithValue("@EmanetAdi", emanetAdi);
                        command.Parameters.AddWithValue("@EmanetTarihi", emanetTarihi);
                        command.Parameters.AddWithValue("@GeriAlmaTarihi", geriAlmaTarihi ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Aciklama", aciklama);
                        command.Parameters.AddWithValue("@Durum", durum);
                        command.Parameters.AddWithValue("@FotografYolu", fotografYolu ?? (object)DBNull.Value);

                        command.ExecuteNonQuery();
                    }

                    connection.Close();
                }

                // Formu temizle
                ClearForm();

                // Listeyi güncelle
                LoadEmanetler();

                // Bildirim oluştur (eğer ana formda EmanetDurumunuKontrolEt metodu varsa)
                if (this.Owner is Carihareketler carihareketlerForm)
                {
                    carihareketlerForm.EmanetDurumunuKontrolEt(cariKodu);
                }

                MessageBox.Show("Emanet başarıyla kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kayıt sırasında bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void btnIptal_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Formu kapatmak istediğinize emin misiniz?", "Onay",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        public void ClearForm()
        {
            txtEmanetAdi.Text = "";
            dtpEmanetTarihi.Value = DateTime.Now;
            dtpGeriAlmaTarihi.Value = DateTime.Now.AddDays(7);
            dtpGeriAlmaTarihi.Checked = false;
            txtAciklama.Text = "";
            cmbDurum.SelectedIndex = 0;
            picEmanet.Image = null;
            fotografYolu = null;
        }

        public void LoadEmanetler()
        {
            dgvEmanetler.DataSource = null;
            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Tablo varlığını kontrol et, yoksa oluştur
                CheckAndCreateEmanetlerTable(connection);

                string query = @"
                    SELECT
                        ID,
                        EmanetAdi,
                        EmanetTarihi,
                        GeriAlmaTarihi,
                        Durum,
                        Aciklama
                    FROM
                        Emanetler
                    WHERE
                        CariKodu = @CariKodu
                    ORDER BY
                        EmanetTarihi DESC";

                SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, connection);
                adapter.SelectCommand.Parameters.AddWithValue("@CariKodu", cariKodu);

                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dgvEmanetler.DataSource = dt;

                // Kolon başlıklarını özelleştir
                if (dgvEmanetler.Columns.Count > 0)
                {
                    dgvEmanetler.Columns["ID"].Visible = false;
                    dgvEmanetler.Columns["EmanetAdi"].HeaderText = "Emanet Adı";
                    dgvEmanetler.Columns["EmanetTarihi"].HeaderText = "Emanet Tarihi";
                    dgvEmanetler.Columns["GeriAlmaTarihi"].HeaderText = "Geri Alma Tarihi";
                    dgvEmanetler.Columns["Durum"].HeaderText = "Durum";
                    dgvEmanetler.Columns["Aciklama"].HeaderText = "Açıklama";
                }

                connection.Close();
            }
        }

        public void CheckAndCreateEmanetlerTable(SQLiteConnection connection)
        {
            string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS Emanetler (
                    ID INTEGER PRIMARY KEY AUTOINCREMENT,
                    CariKodu TEXT NOT NULL,
                    EmanetAdi TEXT NOT NULL,
                    EmanetTarihi TEXT NOT NULL,
                    GeriAlmaTarihi TEXT,
                    Aciklama TEXT,
                    Durum TEXT DEFAULT 'Teslim Edilmedi',
                    FotografYolu TEXT
                );";

            using (SQLiteCommand command = new SQLiteCommand(createTableQuery, connection))
            {
                command.ExecuteNonQuery();
            }
        }

        public void dgvEmanetler_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Durum sütununu renklendir
                if (dgvEmanetler.Columns[e.ColumnIndex].Name == "Durum")
                {
                    string durumText = e.Value?.ToString();
                    if (durumText == "Teslim Edilmedi")
                    {
                        e.CellStyle.ForeColor = Color.Red;
                        e.CellStyle.Font = new Font(dgvEmanetler.Font, FontStyle.Bold);
                    }
                    else if (durumText == "Kısmen Teslim Edildi")
                    {
                        e.CellStyle.ForeColor = Color.Orange;
                        e.CellStyle.Font = new Font(dgvEmanetler.Font, FontStyle.Bold);
                    }
                    else if (durumText == "Teslim Edildi")
                    {
                        e.CellStyle.ForeColor = Color.Green;
                    }
                }
            }
        }

        public void dgvEmanetler_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int emanetId = Convert.ToInt32(dgvEmanetler.Rows[e.RowIndex].Cells["ID"].Value);

                // Emanet detayını göster ve güncelleme imkanı ver
                EmanetDetayGoster(emanetId);

                // Detay gösterildikten sonra listeyi güncelle
                LoadEmanetler();

                // Bildirim kontrolünü de güncelle
                if (this.Owner is Carihareketler carihareketlerForm)
                {
                    carihareketlerForm.EmanetDurumunuKontrolEt(cariKodu);
                }
            }
        }

        public void EmanetDetayGoster(int emanetId)
        {
            try
            {
                string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                SELECT * FROM Emanetler WHERE ID = @ID";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ID", emanetId);
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Emanet detaylarını gösterecek ve güncelleyecek form veya DialogBox
                                using (Form detayForm = new Form())
                                {
                                    detayForm.Text = "Emanet Detayı / Güncelleme";
                                    detayForm.Size = new Size(500, 450);
                                    detayForm.StartPosition = FormStartPosition.CenterParent;
                                    detayForm.BackColor = Color.WhiteSmoke;

                                    // Yuvarlatılmış köşeler ekle
                                    detayForm.FormBorderStyle = FormBorderStyle.None;
                                    detayForm.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, detayForm.Width, detayForm.Height, 20, 20));

                                    // Form başlık çubuğu oluştur
                                    Panel pnlTitle = new Panel();
                                    pnlTitle.Dock = DockStyle.Top;
                                    pnlTitle.Height = 40;
                                    pnlTitle.BackColor = Color.FromArgb(28, 141, 243);

                                    // Panel başlık çubuğuna yuvarlatılmış köşeler ekle
                                    pnlTitle.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, detayForm.Width, pnlTitle.Height, 20, 20));
                                    detayForm.Controls.Add(pnlTitle);

                                    // Başlık etiketi
                                    Label lblTitle = new Label();
                                    lblTitle.Text = "Emanet Detayı / Güncelleme";
                                    lblTitle.Location = new Point(10, 10);
                                    lblTitle.AutoSize = true;
                                    lblTitle.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                                    lblTitle.ForeColor = Color.White;
                                    pnlTitle.Controls.Add(lblTitle);

                                    // Form taşımak için olaylar
                                    pnlTitle.MouseDown += (s, args) => {
                                        if (args.Button == MouseButtons.Left)
                                        {
                                            ReleaseCapture();
                                            SendMessage(detayForm.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                                        }
                                    };
                                    lblTitle.MouseDown += (s, args) => {
                                        if (args.Button == MouseButtons.Left)
                                        {
                                            ReleaseCapture();
                                            SendMessage(detayForm.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                                        }
                                    };

                                    // Kapat butonu için özel panel
                                    Panel closeDetailPanel = new Panel
                                    {
                                        Size = new Size(30, 30),
                                        Location = new Point(detayForm.Width - 60, 5), // Sağ üst köşeye yakın
                                        BackColor = Color.FromArgb(232, 17, 35), // Kırmızı arka plan
                                        Anchor = AnchorStyles.Top | AnchorStyles.Right,
                                        Cursor = Cursors.Hand
                                    };

                                    // Paneli yuvarlatılmış köşeli yap (tam daire)
                                    GraphicsPath pathDetail = new GraphicsPath();
                                    pathDetail.AddEllipse(0, 0, closeDetailPanel.Width, closeDetailPanel.Height);
                                    closeDetailPanel.Region = new Region(pathDetail);

                                    // X işareti için label ekle
                                    Label lblXDetail = new Label
                                    {
                                        Text = "×", // Unicode çarpı işareti
                                        Font = new Font("Arial", 12, FontStyle.Bold),
                                        ForeColor = Color.White,
                                        BackColor = Color.Transparent,
                                        TextAlign = ContentAlignment.MiddleCenter,
                                        Dock = DockStyle.Fill,
                                        Cursor = Cursors.Hand
                                    };

                                    closeDetailPanel.Controls.Add(lblXDetail);

                                    // Panel tıklandığında formu kapat
                                    closeDetailPanel.Click += (s, args) => detayForm.Close();
                                    lblXDetail.Click += (s, args) => detayForm.Close();

                                    // Hover efekti ekle
                                    closeDetailPanel.MouseEnter += (s, args) => {
                                        closeDetailPanel.BackColor = Color.FromArgb(255, 0, 0); // Daha parlak kırmızı hover
                                    };

                                    closeDetailPanel.MouseLeave += (s, args) => {
                                        closeDetailPanel.BackColor = Color.FromArgb(232, 17, 35); // Normal kırmızı
                                    };

                                    // Label'a da hover efekti ekle
                                    lblXDetail.MouseEnter += (s, args) => {
                                        closeDetailPanel.BackColor = Color.FromArgb(255, 0, 0);
                                    };

                                    lblXDetail.MouseLeave += (s, args) => {
                                        closeDetailPanel.BackColor = Color.FromArgb(232, 17, 35);
                                    };

                                    // Başlık paneline ekle
                                    pnlTitle.Controls.Add(closeDetailPanel);

                                    // Detay formunun arayüzünü oluştur
                                    Label lblEmanetAdi = new Label
                                    {
                                        Text = "Emanet Adı / Tanımı:",
                                        Location = new Point(20, 60),
                                        AutoSize = true,
                                        Font = new Font("Segoe UI", 9, FontStyle.Bold),
                                        ForeColor = Color.Black
                                    };
                                    detayForm.Controls.Add(lblEmanetAdi);

                                    TextBox txtEmanetAdi = new TextBox
                                    {
                                        Text = reader["EmanetAdi"].ToString(),
                                        Location = new Point(150, 60),
                                        Width = 300,
                                        BorderStyle = BorderStyle.FixedSingle,
                                        BackColor = Color.White,
                                        ForeColor = Color.Black
                                    };
                                    detayForm.Controls.Add(txtEmanetAdi);

                                    Label lblEmanetTarihi = new Label
                                    {
                                        Text = "Emanet Tarihi:",
                                        Location = new Point(20, 90),
                                        AutoSize = true,
                                        Font = new Font("Segoe UI", 9, FontStyle.Bold),
                                        ForeColor = Color.Black
                                    };
                                    detayForm.Controls.Add(lblEmanetTarihi);

                                    DateTimePicker dtpEmanetTarihi = new DateTimePicker
                                    {
                                        Format = DateTimePickerFormat.Short,
                                        Location = new Point(150, 90),
                                        Width = 300
                                    };
                                    if (DateTime.TryParse(reader["EmanetTarihi"].ToString(), out DateTime emanetTarihi))
                                    {
                                        dtpEmanetTarihi.Value = emanetTarihi;
                                    }
                                    detayForm.Controls.Add(dtpEmanetTarihi);

                                    Label lblGeriAlmaTarihi = new Label
                                    {
                                        Text = "Geri Alma Tarihi:",
                                        Location = new Point(20, 120),
                                        AutoSize = true,
                                        Font = new Font("Segoe UI", 9, FontStyle.Bold),
                                        ForeColor = Color.Black
                                    };
                                    detayForm.Controls.Add(lblGeriAlmaTarihi);

                                    DateTimePicker dtpGeriAlmaTarihi = new DateTimePicker
                                    {
                                        Format = DateTimePickerFormat.Short,
                                        Location = new Point(150, 120),
                                        Width = 300,
                                        ShowCheckBox = true
                                    };
                                    if (!reader.IsDBNull(reader.GetOrdinal("GeriAlmaTarihi")) &&
                                        DateTime.TryParse(reader["GeriAlmaTarihi"].ToString(), out DateTime geriAlmaTarihi))
                                    {
                                        dtpGeriAlmaTarihi.Value = geriAlmaTarihi;
                                        dtpGeriAlmaTarihi.Checked = true;
                                    }
                                    else
                                    {
                                        dtpGeriAlmaTarihi.Checked = false;
                                    }
                                    detayForm.Controls.Add(dtpGeriAlmaTarihi);

                                    Label lblDurum = new Label
                                    {
                                        Text = "Durum:",
                                        Location = new Point(20, 150),
                                        AutoSize = true,
                                        Font = new Font("Segoe UI", 9, FontStyle.Bold),
                                        ForeColor = Color.Black
                                    };
                                    detayForm.Controls.Add(lblDurum);

                                    ComboBox cmbDurum = new ComboBox
                                    {
                                        Location = new Point(150, 150),
                                        Width = 300,
                                        DropDownStyle = ComboBoxStyle.DropDownList,
                                        BackColor = Color.White,
                                        ForeColor = Color.Black,
                                        FlatStyle = FlatStyle.Flat
                                    };
                                    cmbDurum.Items.AddRange(new object[] { "Teslim Edilmedi", "Kısmen Teslim Edildi", "Teslim Edildi" });
                                    cmbDurum.SelectedItem = reader["Durum"].ToString();
                                    detayForm.Controls.Add(cmbDurum);

                                    Label lblAciklama = new Label
                                    {
                                        Text = "Açıklama:",
                                        Location = new Point(20, 180),
                                        AutoSize = true,
                                        Font = new Font("Segoe UI", 9, FontStyle.Bold),
                                        ForeColor = Color.Black
                                    };
                                    detayForm.Controls.Add(lblAciklama);

                                    TextBox txtAciklama = new TextBox
                                    {
                                        Text = reader["Aciklama"].ToString(),
                                        Location = new Point(150, 180),
                                        Width = 300,
                                        Height = 100,
                                        Multiline = true,
                                        ScrollBars = ScrollBars.Vertical,
                                        BorderStyle = BorderStyle.FixedSingle,
                                        BackColor = Color.White,
                                        ForeColor = Color.Black
                                    };
                                    detayForm.Controls.Add(txtAciklama);

                                    // Fotoğraf gösterimi
                                    Label lblFotograf = new Label
                                    {
                                        Text = "Fotoğraf:",
                                        Location = new Point(20, 290),
                                        AutoSize = true,
                                        Font = new Font("Segoe UI", 9, FontStyle.Bold),
                                        ForeColor = Color.Black
                                    };
                                    detayForm.Controls.Add(lblFotograf);

                                    PictureBox picEmanet = new PictureBox
                                    {
                                        Location = new Point(150, 290),
                                        Size = new Size(100, 100),
                                        BorderStyle = BorderStyle.FixedSingle,
                                        SizeMode = PictureBoxSizeMode.Zoom,
                                        BackColor = Color.White
                                    };
                                    string fotografYolu = reader["FotografYolu"].ToString();
                                    if (!string.IsNullOrEmpty(fotografYolu) && File.Exists(fotografYolu))
                                    {
                                        try
                                        {
                                            picEmanet.Image = Image.FromFile(fotografYolu);
                                        }
                                        catch
                                        {
                                            // Resim yüklenemezse sessizce devam et
                                        }
                                    }
                                    detayForm.Controls.Add(picEmanet);

                                    // İşlem butonları
                                    Button btnGuncelle = new Button
                                    {
                                        Text = "GÜNCELLE",
                                        Location = new Point(150, 400),
                                        Size = new Size(100, 40),
                                        BackColor = Color.FromArgb(28, 141, 243),
                                        ForeColor = Color.White,
                                        FlatStyle = FlatStyle.Flat,
                                        FlatAppearance = { BorderSize = 0 },
                                        Font = new Font("Segoe UI", 10, FontStyle.Bold),
                                        Cursor = Cursors.Hand
                                    };
                                    detayForm.Controls.Add(btnGuncelle);

                                    Button btnIptal = new Button
                                    {
                                        Text = "İPTAL",
                                        Location = new Point(260, 400),
                                        Size = new Size(100, 40),
                                        BackColor = Color.FromArgb(255, 80, 80),
                                        ForeColor = Color.White,
                                        FlatStyle = FlatStyle.Flat,
                                        FlatAppearance = { BorderSize = 0 },
                                        Font = new Font("Segoe UI", 10, FontStyle.Bold),
                                        Cursor = Cursors.Hand
                                    };
                                    detayForm.Controls.Add(btnIptal);

                                    Button btnSil = new Button
                                    {
                                        Text = "SİL",
                                        Location = new Point(370, 400),
                                        Size = new Size(100, 40),
                                        BackColor = Color.FromArgb(155, 89, 182),
                                        ForeColor = Color.White,
                                        FlatStyle = FlatStyle.Flat,
                                        FlatAppearance = { BorderSize = 0 },
                                        Font = new Font("Segoe UI", 10, FontStyle.Bold),
                                        Cursor = Cursors.Hand
                                    };
                                    detayForm.Controls.Add(btnSil);

                                    // Güncelleme butonuna tıklandığında
                                    btnGuncelle.Click += (s, args) =>
                                    {
                                        // Güncelleme işlemi
                                        try
                                        {
                                            string updateQuery = @"
                                        UPDATE Emanetler
                                        SET EmanetAdi = @EmanetAdi,
                                            EmanetTarihi = @EmanetTarihi,
                                            GeriAlmaTarihi = @GeriAlmaTarihi,
                                            Durum = @Durum,
                                            Aciklama = @Aciklama
                                        WHERE ID = @ID";

                                            using (SQLiteCommand updateCommand = new SQLiteCommand(updateQuery, connection))
                                            {
                                                updateCommand.Parameters.AddWithValue("@EmanetAdi", txtEmanetAdi.Text);
                                                updateCommand.Parameters.AddWithValue("@EmanetTarihi", dtpEmanetTarihi.Value.ToString("yyyy-MM-dd"));
                                                updateCommand.Parameters.AddWithValue("@GeriAlmaTarihi",
                                                    dtpGeriAlmaTarihi.Checked ?
                                                    dtpGeriAlmaTarihi.Value.ToString("yyyy-MM-dd") :
                                                    (object)DBNull.Value);
                                                updateCommand.Parameters.AddWithValue("@Durum", cmbDurum.SelectedItem.ToString());
                                                updateCommand.Parameters.AddWithValue("@Aciklama", txtAciklama.Text);
                                                updateCommand.Parameters.AddWithValue("@ID", emanetId);

                                                updateCommand.ExecuteNonQuery();
                                            }

                                            MessageBox.Show("Emanet bilgileri başarıyla güncellendi.", "Bilgi",
                                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            detayForm.DialogResult = DialogResult.OK;
                                            detayForm.Close();
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show($"Güncelleme sırasında bir hata oluştu: {ex.Message}", "Hata",
                                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    };

                                    // İptal butonuna tıklandığında
                                    btnIptal.Click += (s, args) =>
                                    {
                                        detayForm.DialogResult = DialogResult.Cancel;
                                        detayForm.Close();
                                    };

                                    // Sil butonuna tıklandığında
                                    btnSil.Click += (s, args) =>
                                    {
                                        if (MessageBox.Show("Bu emanet kaydını silmek istediğinize emin misiniz?", "Onay",
                                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                        {
                                            try
                                            {
                                                string deleteQuery = "DELETE FROM Emanetler WHERE ID = @ID";
                                                using (SQLiteCommand deleteCommand = new SQLiteCommand(deleteQuery, connection))
                                                {
                                                    deleteCommand.Parameters.AddWithValue("@ID", emanetId);
                                                    deleteCommand.ExecuteNonQuery();
                                                }

                                                MessageBox.Show("Emanet kaydı başarıyla silindi.", "Bilgi",
                                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                detayForm.DialogResult = DialogResult.OK;
                                                detayForm.Close();
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show($"Silme sırasında bir hata oluştu: {ex.Message}", "Hata",
                                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            }
                                        }
                                    };

                                    // Formu göster
                                    detayForm.ShowDialog(this);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Emanet kaydı bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Emanet detayı gösterilirken bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Form yuvarlaklığını korumak için Paint olayını ezin
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // Formun yuvarlatılmış kenarları çizildiğinde kenarlık çizmek için
            using (Pen pen = new Pen(Color.FromArgb(200, 200, 200), 1))
            {
                e.Graphics.DrawPath(pen, GetRoundRectPath(new Rectangle(0, 0, this.Width - 1, this.Height - 1), 20));
            }
        }

        // Yuvarlak köşe için yardımcı metot
        public GraphicsPath GetRoundRectPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.X + rect.Width - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.X + rect.Width - radius, rect.Y + rect.Height - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Y + rect.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}