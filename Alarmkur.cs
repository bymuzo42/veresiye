using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Veresiye2025.Controls;
using System.IO;

namespace Veresiye2025
{
    public partial class Alarmkur : Form
    {
        public string selectedCariUnvan;
        public string selectedCariKodu;
        public string _cariUnvan;
        public static Form _alarmListeForm = null;
        public bool isDarkMode = false; // Tema durumu için değişken
        public NotifyIcon currentNotifyIcon = null; //Alarm bildirimine tıklamak için
                                                     // Sınıf düzeyindeki değişkenler
        public bool isStyleApplied = false;

        // Buton adlarını eşleştirme
        public Guna.UI2.WinForms.Guna2Button KaydetButton { get { return btnKaydet; } }
        public Guna.UI2.WinForms.Guna2Button VazgecButton { get { return btnVazgec; } }

        public Alarmkur(string cariUnvan = "", string cariKodu = "")
        {
            InitializeComponent();

            // Cari bilgilerini ayarla
            selectedCariUnvan = cariUnvan;
            selectedCariKodu = cariKodu;
            _cariUnvan = cariUnvan;

            // Form başlığını ayarla
            string formTitle = string.IsNullOrEmpty(cariUnvan) ? "Alarm Kur" : $"Alarm Kur - {cariUnvan}";

            // Köşeleri yuvarla - Form yüklendikten sonra yapılacak ama constructor'da da çağıralım
            try
            {
                using (GraphicsPath path = new GraphicsPath())
                {
                    int radius = 10;
                    Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
                    path.AddArc(rect.X, rect.Y, radius * 2, radius * 2, 180, 90);
                    path.AddArc(rect.X + rect.Width - (radius * 2), rect.Y, radius * 2, radius * 2, 270, 90);
                    path.AddArc(rect.X + rect.Width - (radius * 2), rect.Y + rect.Height - (radius * 2), radius * 2, radius * 2, 0, 90);
                    path.AddArc(rect.X, rect.Y + rect.Height - (radius * 2), radius * 2, radius * 2, 90, 90);
                    path.CloseFigure();
                    this.Region = new Region(path);
                }
            }
            catch
            {
                // Hata olursa Form Load'da tekrar deneriz
            }

            // Form olaylarını ekle
            this.Load += Alarmkur_Load;
            this.Resize += Alarmkur_Resize;

            // btnThemeToggle olayını bağla (eğer Designer'da bağlanmadıysa)
            if (btnThemeToggle != null)
            {
                // Emin olmak için önce varsa kaldır, sonra ekle
                btnThemeToggle.Click -= btnThemeToggle_Click;
                btnThemeToggle.Click += btnThemeToggle_Click;

                // Başlangıç simgesini ayarla
                btnThemeToggle.Text = isDarkMode ? "🌙" : "☀️";
            }

            // NotifyIcon için sınıf değişkeni başlat
            if (currentNotifyIcon == null)
            {
                currentNotifyIcon = new NotifyIcon();
            }

            // Diğer olayları bağla
            if (btnVazgec != null) btnVazgec.Click += VazgecButton_Click;

            // Henüz uygulanmadıysa başlangıç temasını uygula
            if (!isStyleApplied)
            {
                ApplyCustomStyles();
                isStyleApplied = true;
            }
        }



        public void Alarmkur_Resize(object sender, EventArgs e)
        {
            // Form yeniden boyutlandırıldığında köşeleri güncelle
            try
            {
                using (GraphicsPath path = new GraphicsPath())
                {
                    int radius = 10;
                    Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
                    path.AddArc(rect.X, rect.Y, radius * 2, radius * 2, 180, 90);
                    path.AddArc(rect.X + rect.Width - (radius * 2), rect.Y, radius * 2, radius * 2, 270, 90);
                    path.AddArc(rect.X + rect.Width - (radius * 2), rect.Y + rect.Height - (radius * 2), radius * 2, radius * 2, 0, 90);
                    path.AddArc(rect.X, rect.Y + rect.Height - (radius * 2), radius * 2, radius * 2, 90, 90);
                    path.CloseFigure();
                    this.Region = new Region(path);
                }
            }
            catch
            {
                // Hata olursa görmezden gel
            }
        }

        public void Alarmkur_Load(object sender, EventArgs e)
        {
            // Tema uygulaması - modern stilleştirme
            ApplyCustomStyles();

            // Veritabanından cari listesini yükle
            CarileriYukle();

            // Ödeme türü ve önem derecesi için varsayılan değerleri ayarla
            cmbOdemeTuru.SelectedIndex = 0;
            cmbOnemDerecesi.SelectedIndex = 0;

            // Tarih için varsayılan değeri ayarla (yarın)
            dtpTarih.Value = DateTime.Now.AddDays(1);

            // Saat için varsayılan değeri ayarla (şimdi)
            dtpSaat.Value = DateTime.Now;

            // Eğer cari parametresi ile açıldıysa cariye özel ayarlamalar yap
            if (!string.IsNullOrEmpty(selectedCariKodu))
            {
                cmbCariListesi.SelectedIndex = cmbCariListesi.FindString(selectedCariUnvan);
                cmbCariListesi.Enabled = false; // Seçili cari değiştirilemez
            }

            // Alarm tablo kontrolü
            AlarmTablosunuKontrolEt();
        }

        public void ApplyCustomStyles()
        {
            // Panel arkaplan rengini tema durumuna göre ayarla
            alarmPanel.FillColor = isDarkMode ?
                FormStyleManager.DarkTheme.PanelBackground :
                FormStyleManager.LightTheme.PanelBackground;

            // Tüm etiketlere tema durumuna göre renk ayarla
            foreach (Control control in alarmPanel.Controls)
            {
                if (control is Guna.UI2.WinForms.Guna2HtmlLabel label)
                {
                    label.ForeColor = isDarkMode ?
                        FormStyleManager.DarkTheme.LabelText :
                        FormStyleManager.LightTheme.LabelText;
                }
                // TextBox ayarları
                else if (control is Guna.UI2.WinForms.Guna2TextBox textBox)
                {
                    textBox.ForeColor = isDarkMode ?
                        FormStyleManager.DarkTheme.TextBoxText :
                        FormStyleManager.LightTheme.TextBoxText;
                    textBox.FillColor = isDarkMode ?
                        FormStyleManager.DarkTheme.TextBoxBackground :
                        FormStyleManager.LightTheme.TextBoxBackground;
                    textBox.PlaceholderForeColor = isDarkMode ?
                        Color.Silver :
                        Color.Gray;
                }
                // ComboBox ayarları
                else if (control is Guna.UI2.WinForms.Guna2ComboBox comboBox)
                {
                    comboBox.ForeColor = isDarkMode ?
                        FormStyleManager.DarkTheme.TextBoxText :
                        FormStyleManager.LightTheme.TextBoxText;
                    comboBox.FillColor = isDarkMode ?
                        FormStyleManager.DarkTheme.TextBoxBackground :
                        FormStyleManager.LightTheme.TextBoxBackground;
                }
                // DateTimePicker ayarları
                else if (control is Guna.UI2.WinForms.Guna2DateTimePicker dateTimePicker)
                {
                    dateTimePicker.ForeColor = isDarkMode ?
                        FormStyleManager.DarkTheme.TextBoxText :
                        FormStyleManager.LightTheme.TextBoxText;
                    dateTimePicker.FillColor = isDarkMode ?
                        FormStyleManager.DarkTheme.TextBoxBackground :
                        FormStyleManager.LightTheme.TextBoxBackground;
                }
                // Button ayarları - vazgeç butonu hariç
                else if (control is Guna.UI2.WinForms.Guna2Button button)
                {
                    // btnVazgec butonu için ayrı stil
                    if (button == btnVazgec)
                    {
                        button.FillColor = isDarkMode ?
                            FormStyleManager.DarkTheme.SecondaryButtonBackground :
                            Color.FromArgb(200, 200, 200);
                        button.ForeColor = isDarkMode ?
                            Color.White :
                            Color.DimGray;
                    }
                    // Diğer butonlar için (btnKaydet, btnTumAlarmlar) ana buton stili
                    else if (button != btnThemeToggle) // Tema değiştirme butonu hariç
                    {
                        button.FillColor = isDarkMode ?
                            FormStyleManager.DarkTheme.PrimaryButtonBackground :
                            Color.FromArgb(26, 115, 232);
                        button.ForeColor = Color.White;
                    }
                }
            }

            // Form arkaplan rengi
            this.BackColor = isDarkMode ?
                FormStyleManager.DarkTheme.FormBackground :
                FormStyleManager.LightTheme.FormBackground;
        }

        public void btnThemeToggle_Click(object sender, EventArgs e)
        {
            ToggleTheme();

            // Tema değişimi sonrası buton metnini güncelle
            btnThemeToggle.Text = isDarkMode ? "🌙" : "☀️";
        }

        public void CarileriYukle()
        {
            cmbCariListesi.Items.Clear();
            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
            Dictionary<string, string> cariKodUnvanMap = new Dictionary<string, string>();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT CariKodu, Unvani FROM Cari ORDER BY Unvani";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string cariKodu = reader["CariKodu"].ToString();
                            string unvan = reader["Unvani"].ToString();
                            cmbCariListesi.Items.Add(unvan);
                            cariKodUnvanMap[unvan] = cariKodu;
                        }
                    }
                }
            }

            // Cari seçim olayını ekle
            cmbCariListesi.SelectedIndexChanged += (s, e) => {
                if (cmbCariListesi.SelectedItem != null)
                {
                    string selectedUnvan = cmbCariListesi.SelectedItem.ToString();
                    if (cariKodUnvanMap.ContainsKey(selectedUnvan))
                    {
                        selectedCariKodu = cariKodUnvanMap[selectedUnvan];
                        selectedCariUnvan = selectedUnvan;
                    }
                }
            };
        }

        public void AlarmTablosunuKontrolEt()
        {
            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                // Tablo var mı kontrol et
                string checkTableQuery = "SELECT name FROM sqlite_master WHERE type='table' AND name='Alarmlar'";

                using (SQLiteCommand checkCommand = new SQLiteCommand(checkTableQuery, connection))
                {
                    var result = checkCommand.ExecuteScalar();
                    // Tablo yoksa oluştur
                    if (result == null)
                    {
                        string createTableQuery = @"CREATE TABLE Alarmlar (
                            id INTEGER PRIMARY KEY AUTOINCREMENT,
                            cari_kodu TEXT NOT NULL,
                            cari_unvan TEXT NOT NULL,
                            alarm_tarihi DATETIME NOT NULL,
                            mesaj TEXT NOT NULL,
                            odeme_turu TEXT NOT NULL,
                            onem_derecesi TEXT DEFAULT 'Normal',
                            bildirildi INTEGER DEFAULT 0,
                            durum TEXT DEFAULT 'Bekliyor',
                            olusturulma_tarihi DATETIME DEFAULT CURRENT_TIMESTAMP,
                            erteleme_sayisi INTEGER DEFAULT 0,
                            son_erteleme_tarihi DATETIME
                        )";

                        using (SQLiteCommand createCommand = new SQLiteCommand(createTableQuery, connection))
                        {
                            createCommand.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        // Tablo varsa, gerekli sütunlar eklenmiş mi kontrol et
                        try
                        {
                            string alterTableQuery1 = "ALTER TABLE Alarmlar ADD COLUMN erteleme_sayisi INTEGER DEFAULT 0";
                            string alterTableQuery2 = "ALTER TABLE Alarmlar ADD COLUMN son_erteleme_tarihi DATETIME";

                            using (SQLiteCommand cmd1 = new SQLiteCommand(alterTableQuery1, connection))
                            {
                                try { cmd1.ExecuteNonQuery(); } catch { /* Sütun zaten var */ }
                            }

                            using (SQLiteCommand cmd2 = new SQLiteCommand(alterTableQuery2, connection))
                            {
                                try { cmd2.ExecuteNonQuery(); } catch { /* Sütun zaten var */ }
                            }
                        }
                        catch { /* Hata durumunda devam et */ }
                    }
                }
            }
        }

        public void KaydetButton_Click(object sender, EventArgs e)
        {
            // Validasyon
            if (cmbCariListesi.SelectedItem == null)
            {
                MessageBox.Show("Lütfen bir cari seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtAciklama.Text))
            {
                MessageBox.Show("Lütfen alarm açıklaması giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime alarmTarihi = dtpTarih.Value.Date.Add(dtpSaat.Value.TimeOfDay);
            string odemeTuru = cmbOdemeTuru.SelectedItem?.ToString() ?? "Bilinmiyor";
            string onemDerecesi = cmbOnemDerecesi.SelectedItem?.ToString() ?? "Normal";
            string aciklama = txtAciklama.Text;

            try
            {
                string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";

                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = @"INSERT INTO Alarmlar (cari_kodu, cari_unvan, alarm_tarihi, mesaj, odeme_turu, onem_derecesi)
                                     VALUES (@cariKodu, @cariUnvan, @alarmTarihi, @mesaj, @odemeTuru, @onemDerecesi)";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@cariKodu", selectedCariKodu);
                        command.Parameters.AddWithValue("@cariUnvan", selectedCariUnvan);
                        command.Parameters.AddWithValue("@alarmTarihi", alarmTarihi.ToString("yyyy-MM-dd HH:mm:ss"));
                        command.Parameters.AddWithValue("@mesaj", aciklama);
                        command.Parameters.AddWithValue("@odemeTuru", odemeTuru);
                        command.Parameters.AddWithValue("@onemDerecesi", onemDerecesi);
                        command.ExecuteNonQuery();
                    }
                }

                // Bildirim göster
                GosterBildirim("Alarm Kaydedildi",
                               $"{selectedCariUnvan} için {alarmTarihi.ToString("dd.MM.yyyy HH:mm")} tarihine alarm kuruldu.",
                               ToolTipIcon.Info);

                // Form4'ü güncelle
                Form4 form4 = (Form4)Application.OpenForms["Form4"];
                if (form4 != null)
                {
                    form4.KontrolEtAlarmlari();
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Alarm kaydedilirken bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void VazgecButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public void btnTumAlarmlar_Click(object sender, EventArgs e)
        {
            AlarmListesiniGoster();
        }

        

        // AlarmListesiniGoster metodunda FormStyleManager ile modernizasyon
        internal void AlarmListesiniGoster()
        {
            // Eğer alarm listesi formu zaten açıksa, onu öne getir ve işlemi sonlandır
            if (_alarmListeForm != null && !_alarmListeForm.IsDisposed)
            {
                _alarmListeForm.BringToFront();
                return;
            }

            // Çağıran formu belirle (Form4 mü, Alarmkur mu)
            bool isCalledFromAlarmkur = false;

            // Çağıran formun tipini incele
            Form activeForm = Form.ActiveForm;
            if (activeForm != null)
            {
                // Form tipini kontrol et
                if (activeForm.GetType() == typeof(Alarmkur) || activeForm.GetType().Name == "Alarmkur")
                {
                    isCalledFromAlarmkur = true;
                }
            }
            else
            {
                // Aktif form yoksa, o zaman muhtemelen bu Alarmkur formundan çağrıldı
                isCalledFromAlarmkur = (this.GetType() == typeof(Alarmkur) || this.GetType().Name == "Alarmkur");
            }

            // Eğer çağıran Alarmkur ise gizle, değilse gizleme
            if (isCalledFromAlarmkur)
            {
                this.Hide();
            }

            // Yeni form oluştur - Modern stilli
            _alarmListeForm = new Form
            {
                Size = new Size(700, 508), // 700x518 boyut
                StartPosition = FormStartPosition.CenterScreen,
                FormBorderStyle = FormBorderStyle.None, // Kenarlıksız form
                MaximizeBox = false,
                MinimizeBox = false,
                ShowInTaskbar = false, // Taskbar'da görünmesini engelle
                ShowIcon = false // Form başlığında icon'u kaldır
            };

            // Köşeleri yuvarla
            using (GraphicsPath path = new GraphicsPath())
            {
                int radius = 10;
                Rectangle rect = new Rectangle(0, 0, _alarmListeForm.Width, _alarmListeForm.Height);
                path.AddArc(rect.X, rect.Y, radius * 2, radius * 2, 180, 90);
                path.AddArc(rect.X + rect.Width - (radius * 2), rect.Y, radius * 2, radius * 2, 270, 90);
                path.AddArc(rect.X + rect.Width - (radius * 2), rect.Y + rect.Height - (radius * 2), radius * 2, radius * 2, 0, 90);
                path.AddArc(rect.X, rect.Y + rect.Height - (radius * 2), radius * 2, radius * 2, 90, 90);
                path.CloseFigure();
                _alarmListeForm.Region = new Region(path);
            }

            // Arkaplan rengini tema durumuna göre ayarla
            _alarmListeForm.BackColor = isDarkMode ?
                FormStyleManager.DarkTheme.FormBackground :
                FormStyleManager.LightTheme.FormBackground;

            // Özel başlık çubuğu ekle
            Panel titleBar = new Panel
            {
                Dock = DockStyle.Top,
                Height = 40,
                BackColor = isDarkMode ?
                    FormStyleManager.DarkTheme.TitleBarBackground :
                    FormStyleManager.LightTheme.TitleBarBackground
            };

            Label titleLabel = new Label
            {
                Text = "Alarm Listesi",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(15, 10)
            };

            Button closeButton = new Button
            {
                Text = "✕",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(40, 40),
                Location = new Point(_alarmListeForm.Width - 40, 0),
                Cursor = Cursors.Hand
            };

            closeButton.FlatAppearance.BorderSize = 0;
            closeButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(232, 17, 35);

            closeButton.Click += (s, e) => _alarmListeForm.Close();

            // Başlık çubuğunu sürüklenebilir yap
            bool isDragging = false;
            Point dragStartPoint = Point.Empty;

            titleBar.MouseDown += (s, e) => {
                isDragging = true;
                dragStartPoint = new Point(e.X, e.Y);
            };

            titleBar.MouseMove += (s, e) => {
                if (isDragging)
                {
                    Point diff = new Point(e.X - dragStartPoint.X, e.Y - dragStartPoint.Y);
                    _alarmListeForm.Location = new Point(_alarmListeForm.Location.X + diff.X, _alarmListeForm.Location.Y + diff.Y);
                }
            };

            titleBar.MouseUp += (s, e) => {
                isDragging = false;
            };

            titleBar.Controls.Add(titleLabel);
            titleBar.Controls.Add(closeButton);
            _alarmListeForm.Controls.Add(titleBar);

            // Form kapandığında referansı temizle ve sadece Alarmkur formundan çağrıldıysa tekrar göster
            _alarmListeForm.FormClosed += (s, e) => {
                _alarmListeForm = null;
                // Sadece Alarmkur formundan çağrıldıysa ve form hala varsa göster
                if (isCalledFromAlarmkur && !this.IsDisposed && this.Visible == false)
                {
                    this.Show(); // Alarmkur formunu tekrar göster
                }
            };

            // TabControl oluştur
            TabControl tabControl = new TabControl
            {
                Location = new Point(10, 50), // Başlık çubuğunun altında
                Size = new Size(675, 405), // Form boyutuna göre ayarlandı
                Font = new Font("Segoe UI", 9.5F, FontStyle.Regular)
            };

            // Sekmeleri oluştur
            TabPage tabAktif = new TabPage
            {
                Text = "Aktif Alarmlar",
                BackColor = isDarkMode ?
                    FormStyleManager.DarkTheme.PanelBackground :
                    FormStyleManager.LightTheme.PanelBackground
            };

            TabPage tabErtelenen = new TabPage
            {
                Text = "Ertelenenler",
                BackColor = isDarkMode ?
                    FormStyleManager.DarkTheme.PanelBackground :
                    FormStyleManager.LightTheme.PanelBackground
            };

            TabPage tabTamamlanan = new TabPage
            {
                Text = "Tamamlananlar",
                BackColor = isDarkMode ?
                    FormStyleManager.DarkTheme.PanelBackground :
                    FormStyleManager.LightTheme.PanelBackground
            };

            // DataGridView'lar için stil
            // Aktif alarmlar için DataGridView
            Guna.UI2.WinForms.Guna2DataGridView dgvAktifAlarmlar = OlusturDataGridView();
            dgvAktifAlarmlar.Dock = DockStyle.Fill;
            tabAktif.Controls.Add(dgvAktifAlarmlar);

            // Ertelenen alarmlar için DataGridView
            Guna.UI2.WinForms.Guna2DataGridView dgvErtelenenAlarmlar = OlusturDataGridView();
            dgvErtelenenAlarmlar.Dock = DockStyle.Fill;
            tabErtelenen.Controls.Add(dgvErtelenenAlarmlar);

            // Tamamlanan alarmlar için DataGridView
            Guna.UI2.WinForms.Guna2DataGridView dgvTamamlananAlarmlar = OlusturDataGridView();
            dgvTamamlananAlarmlar.Dock = DockStyle.Fill;
            tabTamamlanan.Controls.Add(dgvTamamlananAlarmlar);

            // Sağ tık menülerini her DataGridView için tanımla
            EkleSagTikMenusu(dgvAktifAlarmlar, "Aktif Alarmlar", dgvAktifAlarmlar, dgvErtelenenAlarmlar, dgvTamamlananAlarmlar);
            EkleSagTikMenusu(dgvErtelenenAlarmlar, "Ertelenenler", dgvAktifAlarmlar, dgvErtelenenAlarmlar, dgvTamamlananAlarmlar);
            EkleSagTikMenusu(dgvTamamlananAlarmlar, "Tamamlananlar", dgvAktifAlarmlar, dgvErtelenenAlarmlar, dgvTamamlananAlarmlar);

            // Sekmeleri TabControl'e ekle
            tabControl.Controls.Add(tabAktif);
            tabControl.Controls.Add(tabErtelenen);
            tabControl.Controls.Add(tabTamamlanan);

            // TabControl'ü forma ekle
            _alarmListeForm.Controls.Add(tabControl);

            // Butonların y konumu - altta hizalı olacak şekilde
            int buttonY = 465; // Form boyutuna göre ayarlandı

            // Buton boyutları ve konumları
            int buttonWidth = 120;
            int buttonSpacing = 15;

            // Butonları oluştur
            Guna.UI2.WinForms.Guna2Button btnTamamlandi = new Guna.UI2.WinForms.Guna2Button
            {
                Text = "Tamamlandı",
                Size = new Size(buttonWidth, 40),
                Location = new Point(10, buttonY),
                BorderRadius = 10,
                FillColor = Color.FromArgb(40, 167, 69),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.White
            };

            Guna.UI2.WinForms.Guna2Button btnErtele = new Guna.UI2.WinForms.Guna2Button
            {
                Text = "Ertele",
                Size = new Size(buttonWidth, 40),
                Location = new Point(10 + buttonWidth + buttonSpacing, buttonY),
                BorderRadius = 10,
                FillColor = Color.FromArgb(0, 123, 255),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.White
            };

            Guna.UI2.WinForms.Guna2Button btnSil = new Guna.UI2.WinForms.Guna2Button
            {
                Text = "Sil",
                Size = new Size(buttonWidth, 40),
                Location = new Point(10 + (buttonWidth + buttonSpacing) * 2, buttonY),
                BorderRadius = 10,
                FillColor = Color.FromArgb(220, 53, 69),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.White
            };

            Guna.UI2.WinForms.Guna2Button btnYeni = new Guna.UI2.WinForms.Guna2Button
            {
                Text = "Yeni Alarm",
                Size = new Size(buttonWidth, 40),
                Location = new Point(10 + (buttonWidth + buttonSpacing) * 3, buttonY),
                BorderRadius = 10,
                FillColor = Color.FromArgb(255, 153, 0),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.White
            };

            Guna.UI2.WinForms.Guna2Button btnKapat = new Guna.UI2.WinForms.Guna2Button
            {
                Text = "Kapat",
                Size = new Size(buttonWidth, 40),
                Location = new Point(10 + (buttonWidth + buttonSpacing) * 4, buttonY),
                BorderRadius = 10,
                FillColor = Color.FromArgb(108, 117, 125),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.White
            };

            // Verileri yükle
            YukleAktifAlarmlar(dgvAktifAlarmlar);
            YukleErtelenenAlarmlar(dgvErtelenenAlarmlar);
            YukleTamamlananAlarmlar(dgvTamamlananAlarmlar);

            // Buton olaylarını ekle
            btnTamamlandi.Click += (s, e) => {
                if (tabControl.SelectedIndex == 0 && dgvAktifAlarmlar.SelectedRows.Count > 0)
                {
                    int id = Convert.ToInt32(dgvAktifAlarmlar.SelectedRows[0].Cells["ID"].Value);
                    if (GuncelleAlarmDurumu(id, "Tamamlandı"))
                    {
                        // Tüm veri gridleri güncelle
                        YukleAktifAlarmlar(dgvAktifAlarmlar);
                        YukleErtelenenAlarmlar(dgvErtelenenAlarmlar);
                        YukleTamamlananAlarmlar(dgvTamamlananAlarmlar);
                        // Form4'ü güncelle (eğer açıksa)
                        Form4 form4 = Application.OpenForms["Form4"] as Form4;
                        if (form4 != null)
                        {
                            form4.KontrolEtAlarmlari();
                        }
                    }
                }
                else if (tabControl.SelectedIndex == 1 && dgvErtelenenAlarmlar.SelectedRows.Count > 0)
                {
                    int id = Convert.ToInt32(dgvErtelenenAlarmlar.SelectedRows[0].Cells["ID"].Value);
                    if (GuncelleAlarmDurumu(id, "Tamamlandı"))
                    {
                        // Tüm veri gridleri güncelle
                        YukleAktifAlarmlar(dgvAktifAlarmlar);
                        YukleErtelenenAlarmlar(dgvErtelenenAlarmlar);
                        YukleTamamlananAlarmlar(dgvTamamlananAlarmlar);
                        // Form4'ü güncelle (eğer açıksa)
                        Form4 form4 = Application.OpenForms["Form4"] as Form4;
                        if (form4 != null)
                        {
                            form4.KontrolEtAlarmlari();
                        }
                    }
                }
            };

            btnErtele.Click += (s, e) => {
                Guna.UI2.WinForms.Guna2DataGridView currentGrid = null;
                if (tabControl.SelectedIndex == 0)
                    currentGrid = dgvAktifAlarmlar;
                else if (tabControl.SelectedIndex == 1)
                    currentGrid = dgvErtelenenAlarmlar;

                if (currentGrid != null && currentGrid.SelectedRows.Count > 0)
                {
                    int id = Convert.ToInt32(currentGrid.SelectedRows[0].Cells["ID"].Value);
                    // Erteleme formu göster
                    GosterErtelemeFormu(id);
                    // İşlem sonrası veri gridleri güncelle
                    YukleAktifAlarmlar(dgvAktifAlarmlar);
                    YukleErtelenenAlarmlar(dgvErtelenenAlarmlar);
                    YukleTamamlananAlarmlar(dgvTamamlananAlarmlar);
                    // Form4'ü güncelle (eğer açıksa)
                    Form4 form4 = Application.OpenForms["Form4"] as Form4;
                    if (form4 != null)
                    {
                        form4.KontrolEtAlarmlari();
                    }
                }
            };

            btnSil.Click += (s, e) => {
                Guna.UI2.WinForms.Guna2DataGridView currentGrid = null;
                if (tabControl.SelectedIndex == 0)
                    currentGrid = dgvAktifAlarmlar;
                else if (tabControl.SelectedIndex == 1)
                    currentGrid = dgvErtelenenAlarmlar;
                else if (tabControl.SelectedIndex == 2)
                    currentGrid = dgvTamamlananAlarmlar;

                if (currentGrid != null && currentGrid.SelectedRows.Count > 0)
                {
                    int id = Convert.ToInt32(currentGrid.SelectedRows[0].Cells["ID"].Value);
                    if (MessageBox.Show("Seçili alarmı silmek istediğinizden emin misiniz?", "Onay",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (DeleteAlarm(id))
                        {
                            // Tüm veri gridleri güncelle
                            YukleAktifAlarmlar(dgvAktifAlarmlar);
                            YukleErtelenenAlarmlar(dgvErtelenenAlarmlar);
                            YukleTamamlananAlarmlar(dgvTamamlananAlarmlar);
                            // Form4'ü güncelle (eğer açıksa)
                            Form4 form4 = Application.OpenForms["Form4"] as Form4;
                            if (form4 != null)
                            {
                                form4.KontrolEtAlarmlari();
                            }
                        }
                    }
                }
            };

            btnYeni.Click += (s, e) => {
                // Yeni alarm eklemek için geçici olarak alarm listesi formunu kapat
                _alarmListeForm.Hide();
                // Yeni bir Alarmkur formu oluştur
                Alarmkur yeniAlarmForm = new Alarmkur();
                yeniAlarmForm.ShowInTaskbar = false; // Taskbar'da görünmemesi için
                yeniAlarmForm.ShowIcon = false; // Başlıkta icon olmasın

                if (yeniAlarmForm.ShowDialog() == DialogResult.OK)
                {
                    // Alarm eklendiyse güncelle
                    YukleAktifAlarmlar(dgvAktifAlarmlar);
                    YukleErtelenenAlarmlar(dgvErtelenenAlarmlar);
                    YukleTamamlananAlarmlar(dgvTamamlananAlarmlar);
                    // Form4'ü güncelle (eğer açıksa)
                    Form4 form4 = Application.OpenForms["Form4"] as Form4;
                    if (form4 != null)
                    {
                        form4.KontrolEtAlarmlari();
                    }
                }

                // Alarm listesi formunu tekrar göster
                _alarmListeForm.Show();
            };

            btnKapat.Click += (s, e) => {
                _alarmListeForm.Close();
            };

            // Butonları forma ekle
            _alarmListeForm.Controls.Add(btnTamamlandi);
            _alarmListeForm.Controls.Add(btnErtele);
            _alarmListeForm.Controls.Add(btnSil);
            _alarmListeForm.Controls.Add(btnYeni);
            _alarmListeForm.Controls.Add(btnKapat);

            // Formu göster
            _alarmListeForm.ShowDialog();
        }

        // Yardımcı metod: DataGridView oluşturma
        public Guna.UI2.WinForms.Guna2DataGridView OlusturDataGridView()
        {
            Guna.UI2.WinForms.Guna2DataGridView dgv = new Guna.UI2.WinForms.Guna2DataGridView
            {
                Dock = DockStyle.Fill,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None,
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = isDarkMode ?
                    FormStyleManager.DarkTheme.PanelBackground :
                    FormStyleManager.LightTheme.PanelBackground,
                BorderStyle = BorderStyle.None
            };

            // Başlık stilini manuel ayarla
            dgv.ColumnHeadersDefaultCellStyle.BackColor = isDarkMode ?
                Color.FromArgb(45, 45, 45) :
                Color.FromArgb(26, 115, 232);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgv.DefaultCellStyle.BackColor = isDarkMode ?
                FormStyleManager.DarkTheme.PanelBackground :
                Color.White;
            dgv.DefaultCellStyle.ForeColor = isDarkMode ?
                FormStyleManager.DarkTheme.LabelText :
                Color.DimGray;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            dgv.DefaultCellStyle.SelectionBackColor = isDarkMode ?
                Color.FromArgb(75, 110, 175) :
                Color.FromArgb(240, 240, 240);
            dgv.DefaultCellStyle.SelectionForeColor = isDarkMode ?
                Color.White :
                Color.Black;

            // DataGridView sütunları
            dgv.Columns.Add("ID", "ID");
            dgv.Columns.Add("CariUnvan", "Cari");
            dgv.Columns.Add("AlarmTarihi", "Tarih/Saat");
            dgv.Columns.Add("OdemeTuru", "Ödeme Türü");
            dgv.Columns.Add("Aciklama", "Açıklama");
            dgv.Columns.Add("ErtelemeSayisi", "Erteleme");
            dgv.Columns.Add("OnemDerecesi", "Önem");

            // ID sütununu gizle
            dgv.Columns["ID"].Visible = false;

            // Sütun genişlikleri
            dgv.Columns["CariUnvan"].Width = 160;
            dgv.Columns["AlarmTarihi"].Width = 120;
            dgv.Columns["OdemeTuru"].Width = 100;
            dgv.Columns["Aciklama"].Width = 200;
            dgv.Columns["ErtelemeSayisi"].Width = 80;
            dgv.Columns["OnemDerecesi"].Width = 80;

            return dgv;
        }

        // DataGridView oluşturduktan sonra sağ tık menüsü ekle
        public void EkleSagTikMenusu(Guna.UI2.WinForms.Guna2DataGridView dgv, string kategoriAdi,
                                    Guna.UI2.WinForms.Guna2DataGridView aktifAlarmlar,
                                    Guna.UI2.WinForms.Guna2DataGridView ertelenenAlarmlar,
                                    Guna.UI2.WinForms.Guna2DataGridView tamamlananAlarmlar)
        {
            // ContextMenuStrip oluştur
            ContextMenuStrip sagTikMenu = new ContextMenuStrip();

            // Menü stilini ayarla
            sagTikMenu.BackColor = isDarkMode ?
                FormStyleManager.DarkTheme.PanelBackground :
                FormStyleManager.LightTheme.PanelBackground;
            sagTikMenu.ForeColor = isDarkMode ?
                FormStyleManager.DarkTheme.LabelText :
                FormStyleManager.LightTheme.LabelText;
            sagTikMenu.Font = new Font("Segoe UI", 9F);

            // Tamamlandı menü öğesi
            ToolStripMenuItem tamamlandiMenuItem = new ToolStripMenuItem("Tamamlandı Olarak İşaretle");
            tamamlandiMenuItem.Click += (s, e) => {
                if (dgv.SelectedRows.Count > 0)
                {
                    int id = Convert.ToInt32(dgv.SelectedRows[0].Cells["ID"].Value);
                    GuncelleAlarmDurumu(id, "Tamamlandı");
                    YenileAlarmListesi(aktifAlarmlar, ertelenenAlarmlar, tamamlananAlarmlar);
                }
            };
            sagTikMenu.Items.Add(tamamlandiMenuItem);

            // Ertele menü öğesi
            ToolStripMenuItem erteleMenuItem = new ToolStripMenuItem("Ertele");
            erteleMenuItem.Click += (s, e) => {
                if (dgv.SelectedRows.Count > 0)
                {
                    int id = Convert.ToInt32(dgv.SelectedRows[0].Cells["ID"].Value);
                    GosterErtelemeFormu(id);
                    YenileAlarmListesi(aktifAlarmlar, ertelenenAlarmlar, tamamlananAlarmlar);
                }
            };
            sagTikMenu.Items.Add(erteleMenuItem);

            // Ayırıcı çizgi
            sagTikMenu.Items.Add(new ToolStripSeparator());

            // Sil menü öğesi
            ToolStripMenuItem silMenuItem = new ToolStripMenuItem("Sil");
            silMenuItem.Click += (s, e) => {
                if (dgv.SelectedRows.Count > 0)
                {
                    int id = Convert.ToInt32(dgv.SelectedRows[0].Cells["ID"].Value);
                    if (MessageBox.Show("Seçili alarmı silmek istediğinizden emin misiniz?", "Onay",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        DeleteAlarm(id);
                        YenileAlarmListesi(aktifAlarmlar, ertelenenAlarmlar, tamamlananAlarmlar);
                    }
                }
            };
            sagTikMenu.Items.Add(silMenuItem);

            // Tümünü sil menü öğesi
            ToolStripMenuItem tumunuSilMenuItem = new ToolStripMenuItem("Tümünü Sil");
            tumunuSilMenuItem.Click += (s, e) => {
                if (dgv.Rows.Count == 0)
                {
                    MessageBox.Show($"Silinecek {kategoriAdi} bulunamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string mesaj = $"Bu sekmedeki tüm {kategoriAdi} silinecek. Bu işlem geri alınamaz. Devam etmek istiyor musunuz?";
                DialogResult result = MessageBox.Show(mesaj, "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    // Silinecek alarm ID'lerini topla
                    List<int> silinecekIDler = new List<int>();
                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        int id = Convert.ToInt32(row.Cells["ID"].Value);
                        silinecekIDler.Add(id);
                    }

                    // Alarm silme işlemini gerçekleştir
                    int silinenSayisi = TumAlarmlariSil(silinecekIDler);
                    if (silinenSayisi > 0)
                    {
                        MessageBox.Show($"{silinenSayisi} alarm başarıyla silindi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        YenileAlarmListesi(aktifAlarmlar, ertelenenAlarmlar, tamamlananAlarmlar);
                    }
                    else
                    {
                        MessageBox.Show("Alarm silme işlemi başarısız oldu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            };
            sagTikMenu.Items.Add(tumunuSilMenuItem);

            // ContextMenuStrip'i DataGridView'e ata
            dgv.ContextMenuStrip = sagTikMenu;

            // Tamamlananlar sekmesi için işlevsiz yapılacak öğeler
            if (kategoriAdi == "Tamamlananlar")
            {
                tamamlandiMenuItem.Enabled = false; // Tamamlandı menüsünü devre dışı bırak
                erteleMenuItem.Enabled = false; // Ertele menüsünü devre dışı bırak
            }

            // Menü gösterilmeden önce etkinleştirme/devre dışı bırakma
            sagTikMenu.Opening += (s, e) => {
                if (dgv.SelectedRows.Count > 0)
                {
                    // Seçili satırın durumunu kontrol et
                    string durum = "";

                    // Tamamlananlar sekmesindeki tüm alarmlar zaten tamamlanmış durumda
                    if (kategoriAdi != "Tamamlananlar")
                    {
                        // Sadece Aktif ve Ertelenenler sekmeleri için durum kontrolü yap
                        if (dgv.Columns.Contains("Durum"))
                        {
                            // Normal DataGridView için Durum sütunu varsa
                            durum = dgv.SelectedRows[0].Cells["Durum"].Value?.ToString() ?? "";
                        }
                        else
                        {
                            // Durum sütunu yoksa (bizim oluşturduğumuz DataGridView'lerde)
                            // Örneğin satır stilinden anlayabiliriz (eğer strikethrough varsa tamamlanmış demektir)
                            Font font = dgv.SelectedRows[0].DefaultCellStyle.Font;
                            if (font != null && font.Strikeout)
                            {
                                durum = "Tamamlandı";
                            }
                        }
                        // Durum tamamlandıysa ilgili menü öğelerini devre dışı bırak
                        if (durum == "Tamamlandı")
                        {
                            tamamlandiMenuItem.Enabled = false;
                            erteleMenuItem.Enabled = false;
                        }
                        else
                        {
                            tamamlandiMenuItem.Enabled = true;
                            erteleMenuItem.Enabled = true;
                        }
                    }
                }
                else
                {
                    // Satır seçili değilse sadece "Tümünü Sil" etkin olmalı
                    tamamlandiMenuItem.Enabled = false;
                    erteleMenuItem.Enabled = false;
                    silMenuItem.Enabled = false;
                    tumunuSilMenuItem.Enabled = dgv.Rows.Count > 0;
                }
            };
        }

        // Belirli bir listedeki tüm alarmları silen metod
        public int TumAlarmlariSil(List<int> alarmIDler)
        {
            if (alarmIDler.Count == 0)
                return 0;

            int silinenSayisi = 0;
            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";

            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    using (SQLiteTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            string idListesi = string.Join(",", alarmIDler);
                            string query = $"DELETE FROM Alarmlar WHERE id IN ({idListesi})";

                            using (SQLiteCommand command = new SQLiteCommand(query, connection, transaction))
                            {
                                silinenSayisi = command.ExecuteNonQuery();
                            }

                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show($"Silme işlemi sırasında bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Veritabanı bağlantısı sırasında bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return silinenSayisi;
        }

        // Tüm listeleri yenileyen yardımcı metod
        public void YenileAlarmListesi(Guna.UI2.WinForms.Guna2DataGridView aktifAlarmlar,
                                       Guna.UI2.WinForms.Guna2DataGridView ertelenenAlarmlar,
                                       Guna.UI2.WinForms.Guna2DataGridView tamamlananAlarmlar)
        {
            // Tüm veri gridleri güncelle
            YukleAktifAlarmlar(aktifAlarmlar);
            YukleErtelenenAlarmlar(ertelenenAlarmlar);
            YukleTamamlananAlarmlar(tamamlananAlarmlar);

            // Form4'ü güncelle (eğer açıksa)
            Form4 form4 = (Form4)Application.OpenForms["Form4"];
            if (form4 != null)
            {
                form4.KontrolEtAlarmlari();
            }
        }

        // Erteleme formu göster - modernize edilmiş versiyon
        public void GosterErtelemeFormu(int alarmId)
        {
            Form ertelemeForm = new Form
            {
                Text = "Alarmı Ertele",
                Size = new Size(300, 200),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.None,
                MaximizeBox = false,
                MinimizeBox = false,
                ShowIcon = false, // Form başlığında icon olmasın
                ShowInTaskbar = false, // Taskbar'da görünmesin
                BackColor = isDarkMode ?
                    FormStyleManager.DarkTheme.FormBackground :
                    FormStyleManager.LightTheme.FormBackground
            };

            // Köşeleri yuvarla
            using (GraphicsPath path = new GraphicsPath())
            {
                int radius = 10;
                Rectangle rect = new Rectangle(0, 0, ertelemeForm.Width, ertelemeForm.Height);
                path.AddArc(rect.X, rect.Y, radius * 2, radius * 2, 180, 90);
                path.AddArc(rect.X + rect.Width - (radius * 2), rect.Y, radius * 2, radius * 2, 270, 90);
                path.AddArc(rect.X + rect.Width - (radius * 2), rect.Y + rect.Height - (radius * 2), radius * 2, radius * 2, 0, 90);
                path.AddArc(rect.X, rect.Y + rect.Height - (radius * 2), radius * 2, radius * 2, 90, 90);
                path.CloseFigure();
                ertelemeForm.Region = new Region(path);
            }

            // Başlık çubuğu
            Panel titleBar = new Panel
            {
                Dock = DockStyle.Top,
                Height = 30,
                BackColor = isDarkMode ?
                    FormStyleManager.DarkTheme.TitleBarBackground :
                    FormStyleManager.LightTheme.TitleBarBackground
            };

            Label titleLabel = new Label
            {
                Text = "Alarmı Ertele",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(10, 5)
            };

            Button closeButton = new Button
            {
                Text = "✕",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(30, 30),
                Location = new Point(ertelemeForm.Width - 30, 0),
                Cursor = Cursors.Hand
            };

            closeButton.FlatAppearance.BorderSize = 0;
            closeButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(232, 17, 35);

            closeButton.Click += (s, e) => ertelemeForm.Close();

            // Başlık çubuğunu sürüklenebilir yap
            bool isDragging = false;
            Point dragStartPoint = Point.Empty;

            titleBar.MouseDown += (s, e) => {
                isDragging = true;
                dragStartPoint = new Point(e.X, e.Y);
            };

            titleBar.MouseMove += (s, e) => {
                if (isDragging)
                {
                    Point diff = new Point(e.X - dragStartPoint.X, e.Y - dragStartPoint.Y);
                    ertelemeForm.Location = new Point(ertelemeForm.Location.X + diff.X, ertelemeForm.Location.Y + diff.Y);
                }
            };

            titleBar.MouseUp += (s, e) => {
                isDragging = false;
            };

            titleBar.Controls.Add(titleLabel);
            titleBar.Controls.Add(closeButton);
            ertelemeForm.Controls.Add(titleBar);

            Label lblAciklama = new Label
            {
                Text = "Erteleme süresini seçin:",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = isDarkMode ?
                    FormStyleManager.DarkTheme.LabelText :
                    FormStyleManager.LightTheme.LabelText,
                AutoSize = true,
                Location = new Point(20, 50)
            };

            ComboBox cmbSure = new ComboBox
            {
                Size = new Size(260, 30),
                Location = new Point(20, 80),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10),
                BackColor = isDarkMode ?
                    FormStyleManager.DarkTheme.TextBoxBackground :
                    FormStyleManager.LightTheme.TextBoxBackground,
                ForeColor = isDarkMode ?
                    FormStyleManager.DarkTheme.TextBoxText :
                    FormStyleManager.LightTheme.TextBoxText
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
                Location = new Point(20, 130),
                BackColor = isDarkMode ?
                    FormStyleManager.DarkTheme.PrimaryButtonBackground :
                    Color.FromArgb(0, 120, 215),
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
                Location = new Point(180, 130),
                BackColor = isDarkMode ?
                    FormStyleManager.DarkTheme.SecondaryButtonBackground :
                    Color.FromArgb(100, 100, 100),
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

                int ertelemeSayisi = GetAlarmErtelemeSayisi(alarmId) + 1;
                if (ErteleAlarm(alarmId, yeniTarih, ertelemeSayisi))
                {
                    ertelemeForm.Close();
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

            ertelemeForm.ShowDialog(_alarmListeForm);
        }

        // Alarm erteleme sayısını alma metodu
        public int GetAlarmErtelemeSayisi(int alarmId)
        {
            int ertelemeSayisi = 0;
            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT IFNULL(erteleme_sayisi, 0) FROM Alarmlar WHERE id = @id";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", alarmId);
                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        ertelemeSayisi = Convert.ToInt32(result);
                    }
                }
            }

            return ertelemeSayisi;
        }

        // Aktif alarmları yükle (Bekleyen alarmlar)
        public void YukleAktifAlarmlar(Guna.UI2.WinForms.Guna2DataGridView dgv)
        {
            dgv.Rows.Clear();
            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = @"SELECT id, cari_kodu, cari_unvan, alarm_tarihi, mesaj, odeme_turu,
                        onem_derecesi, IFNULL(erteleme_sayisi, 0) as erteleme_sayisi
                       FROM Alarmlar
                       WHERE durum = 'Bekliyor'
                       ORDER BY datetime(alarm_tarihi) ASC, onem_derecesi DESC";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DateTime alarmTarihi = Convert.ToDateTime(reader["alarm_tarihi"]);
                            int ertelemeSayisi = Convert.ToInt32(reader["erteleme_sayisi"]);
                            string onemDerecesi = reader["onem_derecesi"].ToString();

                            int rowIndex = dgv.Rows.Add(
                                reader["id"],
                                reader["cari_unvan"].ToString(),
                                alarmTarihi.ToString("dd.MM.yyyy HH:mm"),
                                reader["odeme_turu"].ToString(),
                                reader["mesaj"].ToString(),
                                ertelemeSayisi > 0 ? ertelemeSayisi.ToString() + " kez" : "-",
                                onemDerecesi
                            );

                            // Tema durumuna göre renkleri adapte et
                            // Bugün olan alarmlar için stil
                            if (alarmTarihi.Date == DateTime.Today)
                            {
                                dgv.Rows[rowIndex].DefaultCellStyle.ForeColor = Color.Crimson;
                                dgv.Rows[rowIndex].DefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                            }
                            // Geçmiş alarmlar için stil
                            else if (alarmTarihi.Date < DateTime.Today)
                            {
                                dgv.Rows[rowIndex].DefaultCellStyle.BackColor = isDarkMode ?
                                    Color.FromArgb(60, 30, 30) : Color.FromArgb(255, 235, 235);
                            }

                            // Önem derecesine göre arka plan rengi
                            if (onemDerecesi == "Yüksek")
                            {
                                dgv.Rows[rowIndex].DefaultCellStyle.BackColor = isDarkMode ?
                                    Color.FromArgb(60, 50, 20) : Color.FromArgb(255, 246, 222);
                            }
                            else if (onemDerecesi == "Kritik")
                            {
                                dgv.Rows[rowIndex].DefaultCellStyle.BackColor = isDarkMode ?
                                    Color.FromArgb(60, 30, 30) : Color.FromArgb(255, 228, 225);
                            }

                            // Erteleme sayısına göre stil
                            if (ertelemeSayisi > 0)
                            {
                                dgv.Rows[rowIndex].Cells["ErtelemeSayisi"].Style.ForeColor = Color.FromArgb(255, 128, 0);
                                dgv.Rows[rowIndex].Cells["ErtelemeSayisi"].Style.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                            }
                        }
                    }
                }
            }
        }

        // Ertelenen alarmları yükle (Bekleyen ve ertelenmiş olanlar)
        public void YukleErtelenenAlarmlar(Guna.UI2.WinForms.Guna2DataGridView dgv)
        {
            dgv.Rows.Clear();
            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = @"SELECT id, cari_kodu, cari_unvan, alarm_tarihi, mesaj, odeme_turu,
                        onem_derecesi, IFNULL(erteleme_sayisi, 0) as erteleme_sayisi
                       FROM Alarmlar
                       WHERE durum = 'Bekliyor' AND IFNULL(erteleme_sayisi, 0) > 0
                       ORDER BY erteleme_sayisi DESC, datetime(alarm_tarihi) ASC";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DateTime alarmTarihi = Convert.ToDateTime(reader["alarm_tarihi"]);
                            int ertelemeSayisi = Convert.ToInt32(reader["erteleme_sayisi"]);
                            string onemDerecesi = reader["onem_derecesi"].ToString();

                            int rowIndex = dgv.Rows.Add(
                                reader["id"],
                                reader["cari_unvan"].ToString(),
                                alarmTarihi.ToString("dd.MM.yyyy HH:mm"),
                                reader["odeme_turu"].ToString(),
                                reader["mesaj"].ToString(),
                                ertelemeSayisi.ToString() + " kez",
                                onemDerecesi
                            );

                            // Erteleme sayısına göre stil
                            if (ertelemeSayisi >= 3)
                            {
                                dgv.Rows[rowIndex].DefaultCellStyle.BackColor = isDarkMode ?
                                    Color.FromArgb(60, 30, 30) : Color.FromArgb(255, 230, 230);
                                dgv.Rows[rowIndex].Cells["ErtelemeSayisi"].Style.ForeColor = Color.Red;
                            }
                            else if (ertelemeSayisi >= 2)
                            {
                                dgv.Rows[rowIndex].Cells["ErtelemeSayisi"].Style.ForeColor = Color.OrangeRed;
                            }
                            else
                            {
                                dgv.Rows[rowIndex].Cells["ErtelemeSayisi"].Style.ForeColor = Color.Orange;
                            }

                            dgv.Rows[rowIndex].Cells["ErtelemeSayisi"].Style.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                        }
                    }
                }
            }
        }

        // Tamamlanan alarmları yükle
        public void YukleTamamlananAlarmlar(Guna.UI2.WinForms.Guna2DataGridView dgv)
        {
            dgv.Rows.Clear();
            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = @"SELECT id, cari_kodu, cari_unvan, alarm_tarihi, mesaj, odeme_turu,
                        onem_derecesi, IFNULL(erteleme_sayisi, 0) as erteleme_sayisi
                       FROM Alarmlar
                       WHERE durum = 'Tamamlandı'
                       ORDER BY datetime(alarm_tarihi) DESC";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DateTime alarmTarihi = Convert.ToDateTime(reader["alarm_tarihi"]);
                            int ertelemeSayisi = Convert.ToInt32(reader["erteleme_sayisi"]);

                            int rowIndex = dgv.Rows.Add(
                                reader["id"],
                                reader["cari_unvan"].ToString(),
                                alarmTarihi.ToString("dd.MM.yyyy HH:mm"),
                                reader["odeme_turu"].ToString(),
                                reader["mesaj"].ToString(),
                                ertelemeSayisi > 0 ? ertelemeSayisi.ToString() + " kez" : "-",
                                reader["onem_derecesi"].ToString()
                            );

                            // Tamamlanmış alarmlar için stil - koyu temada uyumlu olacak şekilde
                            dgv.Rows[rowIndex].DefaultCellStyle.ForeColor = isDarkMode ?
                                Color.FromArgb(150, 150, 150) : Color.Gray;
                            dgv.Rows[rowIndex].DefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Strikeout);
                        }
                    }
                }
            }
        }

        // Alarmı erteleme
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

        // Alarmları veritabanından yükle ve DataGridView'a ekle
        public void LoadAlarms(Guna.UI2.WinForms.Guna2DataGridView dgv)
        {
            dgv.Rows.Clear();
            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = @"SELECT id, cari_unvan, alarm_tarihi, mesaj, odeme_turu, onem_derecesi, durum
                                FROM Alarmlar
                                ORDER BY date(alarm_tarihi), time(alarm_tarihi)";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DateTime alarmTarihi = Convert.ToDateTime(reader["alarm_tarihi"]);
                            string durum = reader["durum"].ToString();
                            string onemDerecesi = reader["onem_derecesi"].ToString();
                            int rowIndex = dgv.Rows.Add(
                                reader["id"],
                                reader["cari_unvan"].ToString(),
                                alarmTarihi.ToString("dd.MM.yyyy HH:mm"),
                                reader["odeme_turu"].ToString(),
                                reader["mesaj"].ToString(),
                                durum
                            );

                            // Tema durumuna göre renkleri ayarla
                            // Duruma göre satır renklendirme
                            if (durum == "Tamamlandı")
                            {
                                dgv.Rows[rowIndex].DefaultCellStyle.ForeColor = isDarkMode ?
                                    Color.FromArgb(150, 150, 150) : Color.Gray;
                                dgv.Rows[rowIndex].DefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Strikeout);
                            }
                            else if (alarmTarihi.Date < DateTime.Today)
                            {
                                // Geçmiş tarihli alarmlar
                                dgv.Rows[rowIndex].DefaultCellStyle.BackColor = isDarkMode ?
                                    Color.FromArgb(60, 30, 30) : Color.FromArgb(255, 235, 235);
                            }
                            else if (alarmTarihi.Date == DateTime.Today)
                            {
                                // Bugünün alarmları
                                dgv.Rows[rowIndex].DefaultCellStyle.BackColor = isDarkMode ?
                                    Color.FromArgb(50, 50, 10) : Color.FromArgb(255, 248, 220);
                                dgv.Rows[rowIndex].DefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                            }

                            // Önem derecesine göre renklendirme
                            if (onemDerecesi == "Yüksek" && durum != "Tamamlandı")
                            {
                                dgv.Rows[rowIndex].DefaultCellStyle.ForeColor = isDarkMode ?
                                    Color.FromArgb(255, 100, 100) : Color.FromArgb(200, 0, 0);
                            }
                            else if (onemDerecesi == "Kritik" && durum != "Tamamlandı")
                            {
                                dgv.Rows[rowIndex].DefaultCellStyle.ForeColor = isDarkMode ?
                                    Color.FromArgb(255, 80, 80) : Color.Red;
                                dgv.Rows[rowIndex].DefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                            }
                        }
                    }
                }
            }
        }

        // Alarmı tamamlandı olarak işaretle
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
                MessageBox.Show($"Alarm durumu güncellenirken bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Alarmı sil
        public bool DeleteAlarm(int alarmId)
        {
            try
            {
                string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";

                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM Alarmlar WHERE id = @id";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", alarmId);
                        int result = command.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Alarm silinirken bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public void GosterBildirim(string baslik, string mesaj, ToolTipIcon ikonTipi = ToolTipIcon.Info)
        {
            // Mevcut bildirimi temizle
            if (currentNotifyIcon != null)
            {
                try
                {
                    currentNotifyIcon.Visible = false;
                    currentNotifyIcon.Dispose();
                }
                catch { }
                currentNotifyIcon = null;
            }

            // Yeni bildirim oluştur
            currentNotifyIcon = new NotifyIcon
            {
                Icon = SystemIcons.Information,
                Visible = true,
                BalloonTipTitle = baslik,
                BalloonTipText = mesaj,
                BalloonTipIcon = ikonTipi
            };

            // Bildirime tıklama işlemini basitleştir - sadece kaydettikten sonra gösterme
            // Özellikle alarm listesini doğrudan açmayalım

            // Bildirim göster ve 5 saniye sonra temizle
            currentNotifyIcon.ShowBalloonTip(5000);

            // 8 saniye sonra temizle
            System.Windows.Forms.Timer cleanupTimer = new System.Windows.Forms.Timer();
            cleanupTimer.Interval = 8000;
            cleanupTimer.Tick += (s, e) => {
                cleanupTimer.Stop();
                if (currentNotifyIcon != null)
                {
                    try
                    {
                        currentNotifyIcon.Visible = false;
                        currentNotifyIcon.Dispose();
                    }
                    catch { }
                    currentNotifyIcon = null;
                }
                cleanupTimer.Dispose();
            };
            cleanupTimer.Start();
        }

        public void GosterOzelBildirim(int alarmId, string baslik, string mesaj, string cariUnvan, string odemeTuru, DateTime tarih)
        {
            try
            {
                // Yeni bir form oluştur (başlık çubuğu olmadan)
                Form bildirimForm = new Form
                {
                    Size = new Size(400, 300),
                    StartPosition = FormStartPosition.CenterScreen,
                    FormBorderStyle = FormBorderStyle.None,
                    ShowInTaskbar = false,
                    TopMost = true,
                    BackColor = Color.FromArgb(33, 33, 33) // Koyu arka plan
                };

                // Köşeleri yuvarla
                using (GraphicsPath path = new GraphicsPath())
                {
                    int radius = 20; // 20px border radius
                    Rectangle rect = new Rectangle(0, 0, bildirimForm.Width, bildirimForm.Height);
                    path.AddArc(rect.X, rect.Y, radius * 2, radius * 2, 180, 90);
                    path.AddArc(rect.X + rect.Width - (radius * 2), rect.Y, radius * 2, radius * 2, 270, 90);
                    path.AddArc(rect.X + rect.Width - (radius * 2), rect.Y + rect.Height - (radius * 2), radius * 2, radius * 2, 0, 90);
                    path.AddArc(rect.X, rect.Y + rect.Height - (radius * 2), radius * 2, radius * 2, 90, 90);
                    path.CloseFigure();
                    bildirimForm.Region = new Region(path);
                }

                // Form sürükleme için değişkenler
                bool isDragging = false;
                Point dragStartPoint = Point.Empty;

                // Form sürükleme olayları
                bildirimForm.MouseDown += (s, e) => {
                    isDragging = true;
                    dragStartPoint = new Point(e.X, e.Y);
                };
                bildirimForm.MouseMove += (s, e) => {
                    if (isDragging)
                    {
                        Point diff = new Point(e.X - dragStartPoint.X, e.Y - dragStartPoint.Y);
                        bildirimForm.Location = new Point(bildirimForm.Location.X + diff.X, bildirimForm.Location.Y + diff.Y);
                    }
                };
                bildirimForm.MouseUp += (s, e) => {
                    isDragging = false;
                };

                // Kapatma butonu
                Guna.UI2.WinForms.Guna2Button btnKapat = new Guna.UI2.WinForms.Guna2Button
                {
                    Text = "✕",
                    Size = new Size(30, 30),
                    Location = new Point(bildirimForm.Width - 30, 0),
                    FillColor = Color.Transparent,
                    ForeColor = Color.White,
                    BorderRadius = 15,
                    Cursor = Cursors.Hand,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold)
                };
                btnKapat.Click += (s, e) => bildirimForm.Close();
                btnKapat.MouseEnter += (s, e) => btnKapat.FillColor = Color.FromArgb(232, 17, 35);
                btnKapat.MouseLeave += (s, e) => btnKapat.FillColor = Color.Transparent;

                // Alarm ikonu - projenizde varsa bu resmi kullanın, yoksa bir alarm ikonu ekleyin
                // Ya da PictureBox yerine Image (embedded resource) kullanabilirsiniz
                PictureBox imgAlarm = new PictureBox
                {
                    Size = new Size(64, 64),
                    Location = new Point(20, 15),
                    Image = GetAlarmIcon(), // Aşağıda tanımlayacağımız yardımcı metod
                    SizeMode = PictureBoxSizeMode.StretchImage
                };

                // Başlık
                Label lblBaslik = new Label
                {
                    Text = baslik,
                    Font = new Font("Segoe UI", 16, FontStyle.Bold),
                    ForeColor = Color.White,
                    Location = new Point(100, 15),
                    AutoSize = true
                };

                // Cari unvan
                Label lblCariUnvan = new Label
                {
                    Text = cariUnvan,
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    ForeColor = Color.DeepSkyBlue,
                    Location = new Point(100, 45),
                    AutoSize = true
                };

                // Açıklama
                Label lblAciklama = new Label
                {
                    Text = mesaj,
                    Font = new Font("Segoe UI", 9),
                    ForeColor = Color.White,
                    Location = new Point(20, 90),
                    Size = new Size(360, 70),
                    TextAlign = ContentAlignment.MiddleLeft
                };

                // Tarih
                Label lblTarih = new Label
                {
                    Text = $"Tarih: {tarih.ToString("dd.MM.yyyy HH:mm")}",
                    Font = new Font("Segoe UI", 8, FontStyle.Italic),
                    ForeColor = Color.Silver,
                    Location = new Point(20, 160),
                    AutoSize = true
                };

                // Ödeme türü
                Label lblOdemeTuru = new Label
                {
                    Text = $"Ödeme Türü: {odemeTuru}",
                    Font = new Font("Segoe UI", 8, FontStyle.Italic),
                    ForeColor = Color.Silver,
                    Location = new Point(20, 180),
                    AutoSize = true
                };

                // Tamamlandı butonu
                Guna.UI2.WinForms.Guna2Button btnTamamlandi = new Guna.UI2.WinForms.Guna2Button
                {
                    Text = "Tamamlandı",
                    Size = new Size(110, 40),
                    Location = new Point(20, 220),
                    BorderRadius = 20,
                    FillColor = Color.FromArgb(0, 177, 89), // Yeşil
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    Cursor = Cursors.Hand
                };

                // Ertele butonu
                Guna.UI2.WinForms.Guna2Button btnErtele = new Guna.UI2.WinForms.Guna2Button
                {
                    Text = "Ertele",
                    Size = new Size(110, 40),
                    Location = new Point(145, 220),
                    BorderRadius = 20,
                    FillColor = Color.FromArgb(0, 120, 215), // Mavi
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    Cursor = Cursors.Hand
                };

                // Sil butonu
                Guna.UI2.WinForms.Guna2Button btnSil = new Guna.UI2.WinForms.Guna2Button
                {
                    Text = "Sil",
                    Size = new Size(110, 40),
                    Location = new Point(270, 220),
                    BorderRadius = 20,
                    FillColor = Color.FromArgb(232, 17, 35), // Kırmızı
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    Cursor = Cursors.Hand
                };

                // Buton olayları
                btnTamamlandi.Click += (s, e) => {
                    GuncelleAlarmDurumu(alarmId, "Tamamlandı");
                    bildirimForm.Close();
                };

                btnErtele.Click += (s, e) => {
                    GosterErtelemeFormu(alarmId);
                    bildirimForm.Close();
                };

                btnSil.Click += (s, e) => {
                    if (MessageBox.Show("Bu alarmı silmek istediğinizden emin misiniz?", "Onay",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        DeleteAlarm(alarmId);
                        bildirimForm.Close();
                    }
                };

                // Kontrolleri forma ekle
                bildirimForm.Controls.Add(btnKapat);
                bildirimForm.Controls.Add(imgAlarm);
                bildirimForm.Controls.Add(lblBaslik);
                bildirimForm.Controls.Add(lblCariUnvan);
                bildirimForm.Controls.Add(lblAciklama);
                bildirimForm.Controls.Add(lblTarih);
                bildirimForm.Controls.Add(lblOdemeTuru);
                bildirimForm.Controls.Add(btnTamamlandi);
                bildirimForm.Controls.Add(btnErtele);
                bildirimForm.Controls.Add(btnSil);

                // Tüm kontrolleri forma ekledikten sonra, her bir kontrole de 
                // sürükleme özelliği ekleyelim (etiketler, resimler vb.)
                foreach (Control control in bildirimForm.Controls)
                {
                    if (control != btnKapat && control != btnTamamlandi &&
                        control != btnErtele && control != btnSil)
                    {
                        control.MouseDown += (s, e) => {
                            isDragging = true;
                            dragStartPoint = new Point(e.X, e.Y);
                        };
                        control.MouseMove += (s, e) => {
                            if (isDragging)
                            {
                                Point diff = new Point(e.X - dragStartPoint.X, e.Y - dragStartPoint.Y);
                                bildirimForm.Location = new Point(bildirimForm.Location.X + diff.X, bildirimForm.Location.Y + diff.Y);
                            }
                        };
                        control.MouseUp += (s, e) => {
                            isDragging = false;
                        };
                    }
                }

                // Formu göster
                bildirimForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bildirim gösterilirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public Image GetAlarmIcon()
        {
            try
            {
                // Eğer projenizde embedded resource olarak bir alarm ikonu varsa
                // return Properties.Resources.alarm_clock;

                // Ya da aşağıdaki gibi bir çözüm kullanabilirsiniz:
                // Eğer projenizde belirli bir konumda alarm ikonu varsa
                string iconPath = Path.Combine(Application.StartupPath, "images", "clock.png");
                if (File.Exists(iconPath))
                {
                    return Image.FromFile(iconPath);
                }

                // Sistem ikonlarından birini kullanarak bir bitmap oluşturabilirsiniz
                Bitmap bmp = new Bitmap(64, 64);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.Clear(Color.Transparent);
                    // System Information ikonunu çiz
                    g.DrawIcon(SystemIcons.Information, new Rectangle(0, 0, 64, 64));
                }
                return bmp;
            }
            catch
            {
                // Herhangi bir hata durumunda boş bir bitmap döndür
                return new Bitmap(64, 64);
            }
        }

        public void BildirimeTiklandi(object sender, EventArgs e)
        {
            try
            {
                // Debug bilgisi
                Console.WriteLine("Bildirime tıklandı");

                // Form4'ün açık olup olmadığını kontrol et
                Form4 form4 = null;
                foreach (Form form in Application.OpenForms)
                {
                    if (form is Form4)
                    {
                        form4 = (Form4)form;
                        break;
                    }
                }

                // Form4 varsa önce Form4 tarafından AlarmListesiniGoster çağrılmasını dene
                if (form4 != null)
                {
                    bool sonuc = false;
                    if (form4.InvokeRequired)
                    {
                        form4.Invoke(new Action(() => {
                            // Form4'te AlarmGoster metodu olduğunu varsayalım
                            Console.WriteLine("Form4 üzerinden alarm listesi gösteriliyor");
                            sonuc = form4.GosterAlarmListesi();
                        }));
                    }
                    else
                    {
                        Console.WriteLine("Form4 üzerinden alarm listesi gösteriliyor (Invoke gerekmedi)");
                        sonuc = form4.GosterAlarmListesi();
                    }

                    // Eğer Form4 üzerinden gösterilemezse kendimiz gösterelim
                    if (!sonuc)
                    {
                        AlarmListesiniGoster();
                    }
                }
              
            }
            catch (Exception ex)
            {
                Console.WriteLine($"HATA: Bildirim tıklamasında istisna - {ex.Message}");
                MessageBox.Show($"Bildirim işlenirken hata: {ex.Message}", "Hata",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Bildirim temizle
                if (currentNotifyIcon != null)
                {
                    currentNotifyIcon.Visible = false;
                    currentNotifyIcon.Dispose();
                    currentNotifyIcon = null;
                }
            }
        }

        // Bildirime tıklama olayı - ayrı bir metot olarak
        public void NotifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            try
            {
                // Daha basit bir yaklaşım deneyin
                Invoke(new Action(() => {
                    // Doğrudan bu formdan AlarmListesiniGoster çağır
                    this.AlarmListesiniGoster();
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bildirim tıklamasında hata: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        

        // Bildirim kapatıldığında
        public void NotifyIcon_BalloonTipClosed(object sender, EventArgs e)
        {
            try
            {
                if (currentNotifyIcon != null)
                {
                    currentNotifyIcon.Visible = false;
                    currentNotifyIcon.Dispose();
                    currentNotifyIcon = null;
                }
            }
            catch { /* Hata olursa görmezden gel */ }
        }

        // Bildirim ikonuna tıklandığında
        public void NotifyIcon_Click(object sender, EventArgs e)
        {
            NotifyIcon_BalloonTipClicked(sender, e);
        }

        // Alarmkur sınıfına eklenecek yardımcı metot
        public void YenileAcikAlarmListeForm()
        {
            try
            {
                if (_alarmListeForm == null || _alarmListeForm.IsDisposed)
                    return;

                // Control sınıfını kullanarak form içindeki TabControl kontrollerini buluyoruz
                foreach (Control control in _alarmListeForm.Controls)
                {
                    if (control is TabControl mainTabControl) // TabControl için farklı bir isim kullanıyoruz
                    {
                        // TabControl bulundu, şimdi içindeki sekmelere bak
                        foreach (TabPage tabPage in mainTabControl.TabPages)
                        {
                            // Her sekme sayfasında DataGridView'ları ara
                            foreach (Control pageControl in tabPage.Controls) // farklı isim kullanıyoruz
                            {
                                if (pageControl is Guna.UI2.WinForms.Guna2DataGridView dgv)
                                {
                                    // Sekmeye göre doğru yükleme metodunu çağır
                                    if (tabPage.Text == "Aktif Alarmlar")
                                        YukleAktifAlarmlar(dgv);
                                    else if (tabPage.Text == "Ertelenenler")
                                        YukleErtelenenAlarmlar(dgv);
                                    else if (tabPage.Text == "Tamamlananlar")
                                        YukleTamamlananAlarmlar(dgv);
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

                // Formu yenilemek için Refresh çağır
                _alarmListeForm.Refresh();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Açık alarm formu yenilenirken hata: {ex.Message}");
            }
        }

        // Alarmkur.cs sınıfına eklenecek - daha temiz bir yaklaşım
        public void SetDefaultDate(DateTime tarih)
        {
            if (dtpTarih != null)
                dtpTarih.Value = tarih.Date;
            if (dtpSaat != null)
                dtpSaat.Value = DateTime.Now; // Veya istediğiniz başka bir saat
        }

        // Tema değiştirme özelliği
        public void ToggleTheme()
        {
            isDarkMode = !isDarkMode;
            ApplyCustomStyles();
        }

        //kktakip formu için alarm kurma işlemi
        // Alarmkur.cs sınıfına eklenecek metot
        public void SetKrediKartiVerileri(string bankaAdi, string kartNo, DateTime sonOdemeTarihi, int alarmGunSayisi)
        {
            try
            {
                // Alarm tarihini hesapla
                DateTime alarmTarihi = sonOdemeTarihi.AddDays(-alarmGunSayisi);
                // Tarih kontrollerini ayarla
                if (dtpTarih != null)
                    dtpTarih.Value = alarmTarihi.Date;
                if (dtpSaat != null)
                    dtpSaat.Value = DateTime.Now.Date.AddHours(9).AddMinutes(0); // Sabah 9:00 gibi
                                                                                 // Son 4 haneyi al (güvenlik için)
                string sonDortHane = kartNo.Length > 4 ? kartNo.Substring(kartNo.Length - 4) : kartNo;
                // Açıklama alanını ayarla
                if (txtAciklama != null)
                {
                    txtAciklama.Text = $"{bankaAdi} bankasına ait **** **** **** {sonDortHane} numaralı kartın son ödeme tarihi yaklaşıyor. Son ödeme tarihi: {sonOdemeTarihi.ToString("dd.MM.yyyy")}";
                }
                // Ödeme türünü ayarla
                if (cmbOdemeTuru != null && cmbOdemeTuru.Items.Contains("Kredi Kartı"))
                {
                    cmbOdemeTuru.SelectedItem = "Kredi Kartı";
                }
                // Önem derecesini ayarla
                if (cmbOnemDerecesi != null && cmbOnemDerecesi.Items.Contains("Normal"))
                {
                    cmbOnemDerecesi.SelectedItem = "Normal";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kredi kartı verileri ayarlanırken hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
    }
}