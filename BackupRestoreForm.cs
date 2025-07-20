using Guna.UI2.WinForms;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;
using Dropbox.Api;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using System.Threading;
using FluentFTP;
using System.Linq;
using Dropbox.Api.Files;
using System.Net.Http.Headers;
using System.Net.Http;
using Microsoft.Kiota.Abstractions;
using Microsoft.Graph.Authentication;
using Microsoft.Kiota.Abstractions.Authentication;
using Google.Apis.Auth.OAuth2.Flows;

namespace Veresiye2025
{
    internal partial class BackupRestoreForm : Form
    {
        //cloud yedekleme tanımları
        public Guna2Panel panelCloudBackup;
        public bool isCloudPanelVisible = false;
        public string _connectedService = "";
        public string _targetFolder = "";
        public string _ftpServer = "";
        public string _ftpUsername = "";
        public string _ftpPassword = "";
        public string _msalAccessToken = "";
        // API istemcileri
        public DriveService _driveService;
        public DropboxClient _dropboxClient;
        public GraphServiceClient _graphClient;
        // Uygulama kimlik bilgileri
        public readonly string GoogleClientId = "330304563455-upmot462399ps50i94ovhrsqg94skpdq.apps.googleusercontent.com";
        public readonly string GoogleClientSecret = "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";
        public readonly string DropboxAppKey = ""; // Dropbox Developer Console'dan alınacak
        public readonly string MSALClientId = ""; // Microsoft Azure Portal'dan alınacak
        //cloud yedekleme tanımları bitiş
        public System.Windows.Forms.Timer autoBackupTimer;
        public readonly string firmaAdi;
        public event Action OnRestoreCompleted;
        public bool isDarkMode = false;
        public Color accentColor = Color.FromArgb(0, 123, 255); // Ana vurgu rengi (Tema durumuna göre değişecek)
        // Form taşıma değişkenleri
        public bool isDragging = false;
        public Point dragStartPoint;
        // Form taşıma değişkenleri bitiş


        // Cloud Storage Manager Sınıfı
        public class CloudStorageManager
        {
            // Bulut servisi türleri
            public enum CloudServiceType
            {
                GoogleDrive,
                Dropbox,
                OneDrive,
                FTP
            }
            // Özellikler
            public bool IsConnected { get; set; } = false;
            public CloudServiceType CurrentService { get; set; }
            public string Username { get; set; } = "";
            // Olaylar
            public event EventHandler<bool> ConnectionStateChanged;
            public event EventHandler<string> StatusMessageChanged;

            public CloudStorageManager()
            {
                // Yapıcı metot
                LoadSavedSettings();
            }

            // Ayarları yükle
            public void LoadSavedSettings()
            {
                // Burada kaydedilmiş ayarları yükleyebilirsiniz
                // Şimdilik basit bir implementasyon
            }

            // Bağlantı durumu değiştiğinde olayı tetikle
            public void RaiseConnectionStateChanged(bool connected)
            {
                ConnectionStateChanged?.Invoke(this, connected);
            }

            // Durum mesajı değiştiğinde olayı tetikle
            public void RaiseStatusMessageChanged(string message)
            {
                StatusMessageChanged?.Invoke(this, message);
            }

            // Bulut servisine bağlan
            public Task<bool> ConnectAsync(CloudServiceType serviceType, string folder = "")
            {
                // Basit bir implementasyon
                return Task.Run(() => {
                    // Gerçek uygulamada burada OAuth2 flow'u olacak
                    try
                    {
                        System.Threading.Thread.Sleep(1000); // Bağlantı simülasyonu
                        CurrentService = serviceType;
                        IsConnected = true;
                        Username = "test.user@email.com";
                        RaiseConnectionStateChanged(true);
                        RaiseStatusMessageChanged($"Bağlantı başarılı: {CurrentService}");
                        return true;
                    }
                    catch (Exception ex)
                    {
                        RaiseStatusMessageChanged($"Bağlantı hatası: {ex.Message}");
                        return false;
                    }
                });
            }

            // FTP sunucusuna bağlan
            public bool ConnectToFTP(string server, string username, string password, string folder = "")
            {
                try
                {
                    // Gerçek uygulamada FTP bağlantısı test edilecek
                    CurrentService = CloudServiceType.FTP;
                    IsConnected = true;
                    Username = username;
                    RaiseConnectionStateChanged(true);
                    RaiseStatusMessageChanged($"FTP bağlantısı başarılı: {server}");
                    return true;
                }
                catch (Exception ex)
                {
                    RaiseStatusMessageChanged($"FTP bağlantı hatası: {ex.Message}");
                    return false;
                }
            }

            // Dosyayı buluta yükle
            public Task<bool> UploadFileAsync(string filePath, string fileName = null)
            {
                if (!IsConnected)
                {
                    RaiseStatusMessageChanged("Yükleme yapabilmek için önce bulut servisine bağlanın");
                    return Task.FromResult(false);
                }
                return Task.Run(() => {
                    try
                    {
                        // Gerçek uygulamada dosya burada yüklenir
                        System.Threading.Thread.Sleep(2000); // Yükleme simülasyonu
                        RaiseStatusMessageChanged($"Dosya başarıyla yüklendi");
                        return true;
                    }
                    catch (Exception ex)
                    {
                        RaiseStatusMessageChanged($"Dosya yükleme hatası: {ex.Message}");
                        return false;
                    }
                });
            }
        }

        internal BackupRestoreForm(string firmaAdi)
        {
            InitializeComponent();
            
            //bulut panelini başlat
            // Form boyutunu açıkça belirt
            this.AutoScaleMode = AutoScaleMode.None; // DPI sorunlarını önler
            this.Size = new Size(950, 508);
            this.MinimumSize = new Size(950, 508); // Minimum boyut belirle
            // Form kenarlıklarını kaldır
            this.FormBorderStyle = FormBorderStyle.None;
            // Firma adını kaydet
            this.firmaAdi = firmaAdi;
            if (string.IsNullOrWhiteSpace(firmaAdi))
            {
                MessageBox.Show("Firma adı geçersiz. Form yüklenemedi.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }
            // Form olaylarını ayarla
            this.Load += BackupRestoreForm_Load;
            this.Paint += BackupRestoreForm_Paint;
            // Program açılırken otomatik yedek al
            this.Load += (s, e) => AutoBackup();
            // Program kapanırken otomatik yedek al
            Application.ApplicationExit += (s, e) => AutoBackup();
            // Tema ayarlarını yükle
            ApplyTheme();
            // Başlık paneli için sürükleme kontrollerini bağla
            panelTitleBar.MouseDown += TitleBar_MouseDown;
            panelTitleBar.MouseMove += TitleBar_MouseMove;
            panelTitleBar.MouseUp += TitleBar_MouseUp;
            lblTitle.MouseDown += TitleBar_MouseDown;
            lblTitle.MouseMove += TitleBar_MouseMove;
            lblTitle.MouseUp += TitleBar_MouseUp;
            // Buton olaylarını bağla
            btnClose.Click += btnClose_Click_1;
            // Fare üzeri olaylarını bağla
            btnClose.MouseEnter += btnClose_MouseEnter;
            btnClose.MouseLeave += btnClose_MouseLeave;
            // Başlık çubuğu düğmelerini konumlandır
            RepositionTitleBarButtons();
            // Form yüklendikten sonra köşeleri yuvarla - bunu en sona alın
            this.Shown += (s, e) => {
                ApplyRoundedCorners();
                // Form yüklendikten sonra butonları tekrar konumlandır ve öne getir
                RepositionTitleBarButtons();
                btnClose.BringToFront();
                // Form güncellemesini zorla
                this.Refresh();
                Application.DoEvents();
            };
        }

        // Form class içinde tanımlanacak yeni sınıf
        public class PopupCalendarForm : Form
        {
            public Guna2Panel calendarPanel;
            public Guna2DateTimePicker datePicker;
            public Guna2Button btnClose;
            public Label lblTitle;
            public string backupPath;
            public bool isDarkMode;

            public PopupCalendarForm(string backupPath, bool isDarkMode)
            {
                this.backupPath = backupPath;
                this.isDarkMode = isDarkMode;
                InitializePopupCalendar();
            }

            public void InitializePopupCalendar()
            {
                this.FormBorderStyle = FormBorderStyle.None;
                this.StartPosition = FormStartPosition.CenterParent;
                this.Size = new Size(380, 400);
                this.ShowInTaskbar = false;
                // Formun kendisi için yuvarlak köşeler
                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddArc(0, 0, 20, 20, 180, 90);
                    path.AddArc(this.Width - 20, 0, 20, 20, 270, 90);
                    path.AddArc(this.Width - 20, this.Height - 20, 20, 20, 0, 90);
                    path.AddArc(0, this.Height - 20, 20, 20, 90, 90);
                    path.CloseFigure();
                    this.Region = new Region(path);
                }
                // Ana panel
                calendarPanel = new Guna2Panel();
                calendarPanel.Dock = DockStyle.Fill;
                calendarPanel.BackColor = isDarkMode ? Color.FromArgb(45, 45, 45) : Color.White;
                calendarPanel.BorderColor = isDarkMode ? Color.FromArgb(60, 60, 60) : Color.FromArgb(213, 218, 223);
                calendarPanel.BorderThickness = 1;
                calendarPanel.ShadowDecoration.Enabled = true;
                calendarPanel.ShadowDecoration.Depth = 5;
                calendarPanel.ShadowDecoration.Color = Color.FromArgb(50, 0, 0, 0);
                // Başlık
                lblTitle = new Label();
                lblTitle.Text = "Yedekleme Takvimi";
                lblTitle.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                lblTitle.ForeColor = isDarkMode ? Color.White : Color.FromArgb(0, 123, 255);
                lblTitle.Size = new Size(200, 30);
                lblTitle.Location = new Point(90, 10);
                lblTitle.TextAlign = ContentAlignment.MiddleCenter;
                // Kapat butonu
                btnClose = new Guna2Button();
                btnClose.Text = "X";
                btnClose.Size = new Size(30, 30);
                btnClose.BorderRadius = 15;
                btnClose.FillColor = Color.FromArgb(220, 53, 69);
                btnClose.Location = new Point(this.Width - 40, 10);
                btnClose.Cursor = Cursors.Hand;
                btnClose.Click += (s, e) => this.Close();
                // Tarih seçici
                datePicker = new Guna2DateTimePicker();
                datePicker.Location = new Point(20, 50);
                datePicker.Size = new Size(340, 36);
                datePicker.BorderRadius = 5;
                datePicker.FillColor = isDarkMode ? Color.FromArgb(55, 55, 55) : Color.White;
                datePicker.ForeColor = isDarkMode ? Color.White : Color.Black;
                datePicker.Font = new Font("Segoe UI", 9, FontStyle.Regular);
                datePicker.Format = DateTimePickerFormat.Long;
                datePicker.Value = DateTime.Now;
                datePicker.ValueChanged += (s, e) => LoadCalendarData();
                // Kontrolleri panele ekle
                calendarPanel.Controls.Add(lblTitle);
                calendarPanel.Controls.Add(btnClose);
                calendarPanel.Controls.Add(datePicker);
                // Paneli forma ekle
                this.Controls.Add(calendarPanel);
                // Takvim verilerini yükle
                LoadCalendarData();
                // Esc tuşuna basınca formu kapat
                this.KeyPreview = true;
                this.KeyDown += (s, e) => { if (e.KeyCode == Keys.Escape) this.Close(); };
            }

            public void LoadCalendarData()
            {
                try
                {
                    // Önceki gün butonlarını temizle
                    foreach (Control ctrl in calendarPanel.Controls)
                    {
                        if (ctrl is Guna2Button &&
                            !(ctrl == btnClose) &&
                            int.TryParse(ctrl.Text, out _))
                        {
                            calendarPanel.Controls.Remove(ctrl);
                        }
                    }
                    // Seçilen ayın takvimini oluştur
                    DateTime selectedDate = datePicker.Value;
                    int year = selectedDate.Year;
                    int month = selectedDate.Month;
                    // Ayın ilk ve son günlerini bul
                    DateTime firstDayOfMonth = new DateTime(year, month, 1);
                    DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                    // Haftanın günleri için etiket ekle
                    string[] dayNames = { "Pzt", "Sal", "Çar", "Per", "Cum", "Cmt", "Paz" };
                    for (int i = 0; i < 7; i++)
                    {
                        Label lblDay = new Label();
                        lblDay.Text = dayNames[i];
                        lblDay.Font = new Font("Segoe UI", 8, FontStyle.Bold);
                        lblDay.Size = new Size(40, 20);
                        lblDay.ForeColor = isDarkMode ? Color.LightGray : Color.FromArgb(70, 70, 70);
                        lblDay.TextAlign = ContentAlignment.MiddleCenter;
                        lblDay.Location = new Point(20 + i * 48, 95);
                        lblDay.Tag = "DayHeader";  // Tag ile işaretleyelim ki silmeyelim
                        // Eğer aynı konumda başka bir etiket yoksa ekle
                        bool exists = false;
                        foreach (Control ctrl in calendarPanel.Controls)
                        {
                            if (ctrl is Label && ctrl.Location == lblDay.Location && ctrl.Tag?.ToString() == "DayHeader")
                            {
                                exists = true;
                                break;
                            }
                        }
                        if (!exists)
                            calendarPanel.Controls.Add(lblDay);
                    }
                    // Ayın başladığı günü hesapla (0: Pazartesi, ... 6: Pazar)
                    int firstDayOfWeek = ((int)firstDayOfMonth.DayOfWeek + 6) % 7;
                    // Günleri takvimde göster
                    for (int i = 1; i <= lastDayOfMonth.Day; i++)
                    {
                        DateTime currentDate = new DateTime(year, month, i);
                        int row = (firstDayOfWeek + i - 1) / 7;
                        int col = (firstDayOfWeek + i - 1) % 7;
                        Guna2Button btnDay = new Guna2Button();
                        btnDay.Text = i.ToString();
                        btnDay.Size = new Size(40, 35);
                        btnDay.BorderRadius = 5;
                        btnDay.Location = new Point(20 + col * 48, 120 + row * 40);
                        btnDay.FillColor = isDarkMode ? Color.FromArgb(60, 60, 60) : Color.FromArgb(240, 240, 240);
                        btnDay.ForeColor = isDarkMode ? Color.White : Color.Black;
                        btnDay.Tag = currentDate.ToString("yyyy-MM-dd");  // Günün tarihini tag olarak saklayalım
                        // Bugünün tarihini vurgula
                        if (currentDate.Date == DateTime.Now.Date)
                        {
                            btnDay.FillColor = Color.FromArgb(0, 123, 255);
                            btnDay.ForeColor = Color.White;
                            btnDay.BorderThickness = 1;
                            btnDay.BorderColor = Color.White;
                        }
                        // Bu tarihte yedek var mı kontrol et
                        bool hasBackup = CheckIfBackupExistsOnDate(currentDate);
                        if (hasBackup)
                        {
                            btnDay.FillColor = Color.FromArgb(40, 167, 69);  // Yeşil
                            btnDay.ForeColor = Color.White;
                        }
                        // Tıklama olayı
                        btnDay.Click += BtnCalendarDay_Click;
                        calendarPanel.Controls.Add(btnDay);
                    }
                    // Ay-yıl göstergesini güncelle
                    lblTitle.Text = $"{GetMonthName(month)} {year}";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Takvim yüklenirken bir hata oluştu: " + ex.Message,
                        "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            public void BtnCalendarDay_Click(object sender, EventArgs e)
            {
                if (sender is Guna2Button btn && btn.Tag != null)
                {
                    try
                    {
                        string dateStr = btn.Tag.ToString();
                        DateTime selectedDate = DateTime.Parse(dateStr);
                        // Bu tarihte alınan yedekleri bul
                        List<string> backups = GetBackupsOnDate(selectedDate);
                        if (backups.Count > 0)
                        {
                            // Yedekleme listesini göster
                            string message = $"{selectedDate.ToShortDateString()} tarihinde alınan yedekler:\n\n";
                            foreach (string backup in backups)
                            {
                                message += "• " + Path.GetFileName(backup) + "\n";
                            }
                            MessageBox.Show(message, "Yedekleme Bilgisi",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Bu tarihte alınmış bir yedek bulunamadı.",
                                "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Yedekleme bilgisi görüntülenirken bir hata oluştu: " + ex.Message,
                            "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            public List<string> GetBackupsOnDate(DateTime date)
            {
                List<string> backups = new List<string>();
                try
                {
                    if (string.IsNullOrEmpty(backupPath) || !Directory.Exists(backupPath))
                        return backups;
                    // Yedekleme dosyalarını ara
                    string[] backupFiles = Directory.GetFiles(backupPath, "*.bak");
                    // Dosya adından tarihi çıkar ve kontrol et
                    foreach (string file in backupFiles)
                    {
                        string fileName = Path.GetFileName(file);
                        // Dosya isminden tarih bilgisini çıkarmaya çalış (farklı formatları dene)
                        if (fileName.Contains(date.ToString("yyyy-MM-dd")) ||
                            fileName.Contains(date.ToString("dd.MM.yyyy")) ||
                            fileName.Contains(date.ToString("yyyyMMdd")))
                        {
                            backups.Add(file);
                        }
                    }
                }
                catch
                {
                    // Hata durumunda boş liste döndür
                }
                return backups;
            }

            public bool CheckIfBackupExistsOnDate(DateTime date)
            {
                return GetBackupsOnDate(date).Count > 0;
            }

            public string GetMonthName(int month)
            {
                string[] months = {
                    "Ocak", "Şubat", "Mart", "Nisan", "Mayıs", "Haziran",
                    "Temmuz", "Ağustos", "Eylül", "Ekim", "Kasım", "Aralık"
                };
                return months[month - 1];
            }
        }

        // Kapatma butonu fare olayları
        public void btnClose_MouseEnter(object sender, EventArgs e)
        {
            btnClose.FillColor = Color.Red;
        }

        public void btnClose_MouseLeave(object sender, EventArgs e)
        {
            btnClose.FillColor = Color.Transparent;
        }

        public void RepositionTitleBarButtons()
        {
            try
            {
                Debug.WriteLine("RepositionTitleBarButtons çağrıldı");
                // Düğmelerin konumunu ayarla
                btnClose.Location = new Point(panelTitleBar.Width - 35, 5);
                // Anchor özelliklerini ayarla
                btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                // Görünürlük özelliklerini ayarla
                btnClose.Visible = true;
                // Butonların boyutlarını ayarla
                btnClose.Size = new Size(30, 30);
                // Kapatma butonu stilleri
                btnClose.FillColor = Color.Transparent;
                btnClose.ForeColor = Color.White;
                btnClose.Font = new Font("Segoe UI", 11, FontStyle.Bold);
                btnClose.Text = "X";
                btnClose.BorderRadius = 10;
                btnClose.UseTransparentBackground = true;
                btnClose.Cursor = Cursors.Hand;
                // Kontrollerin düzenini yenile
                btnClose.BringToFront();
                Debug.WriteLine($"btnClose: Visible={btnClose.Visible}, Location={btnClose.Location}, Size={btnClose.Size}, Text={btnClose.Text}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"RepositionTitleBarButtons hatası: {ex.Message}");
            }
        }

        // Köşeleri yuvarlatmak için gerekli Win32 API fonksiyonu
        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        public static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // width of ellipse
            int nHeightEllipse // height of ellipse
        );

        #region Form Olayları ve Tasarım
        public void BackupRestoreForm_Load(object sender, EventArgs e)
        {
            UpdateYedeklemeDurumuPanel();//bilgi paneli
            // Başlık çubuğu düğmelerini güncelle
            RepositionTitleBarButtons();
            // Not panellerini yükle
            UpdateNotePanels();
            // Eğer otomatik yedekleme açıksa, program açılışında yedek al
            if (Properties.Settings.Default.AutoBackupEnabled)
            {
                AutoBackup();
            }
            InitializeAutoBackupTimer(); // Otomatik yedekleme zamanlayıcısını başlat
            LoadAutoBackupSettings();    // Otomatik yedekleme ayarlarını yükle
            // Eğer yedekleme yolu NULL ise masaüstünü ata
            if (string.IsNullOrEmpty(Properties.Settings.Default.LastBackupPath))
            {
                Properties.Settings.Default.LastBackupPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "VeresiyeYedekler");
                Properties.Settings.Default.Save();
            }
            // "Yedek Al" bilgilerini ayarlardan yükle
            txtBackupPath.Text = Properties.Settings.Default.LastBackupPath;
            lblLastBackupDate.Text = string.IsNullOrEmpty(Properties.Settings.Default.LastBackupDate)
                ? "Son Yedek Alma: Henüz yapılmadı"
                : $"Son Yedek Alma: {Properties.Settings.Default.LastBackupDate}";
            // "Geri Yükle" bilgilerini ayarlardan yükle
            txtRestoreFile.Text = Properties.Settings.Default.LastRestoreFile;
            lblLastRestoreInfo.Text = string.IsNullOrEmpty(Properties.Settings.Default.LastRestoreDate)
                ? "Son Geri Yükleme: Henüz yapılmadı"
                : $"Son Geri Yükleme: {Properties.Settings.Default.LastRestoreDate}";
            // Kaydedilen dili al ve uygula (eğer LanguageManager varsa)
            if (LanguageManager.Instance != null)
            {
                string savedLanguage = LanguageManager.Instance.GetSavedLanguage();
                LanguageManager.Instance.LoadLanguage(savedLanguage);
                LanguageManager.Instance.ApplyLanguage(this);
                // Eğer FormGenelAyarlar açıksa, oradaki dilsec ComboBox'ına eriş ve dili değiştir
                FormGenelAyarlar genelAyarlarForm = Application.OpenForms["FormGenelAyarlar"] as FormGenelAyarlar;
                if (genelAyarlarForm != null)
                {
                    genelAyarlarForm.SetSelectedLanguage(savedLanguage);
                }
            }
            // Tab kontrolüne özel stil uygula
            ConfigureTabControl();
            PositionCloudPanel();
        }

        public void BackupRestoreForm_Paint(object sender, PaintEventArgs e)
        {
            // Kenar çizgisi
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (Pen pen = new Pen(isDarkMode ? Color.FromArgb(70, 70, 70) : Color.FromArgb(220, 220, 220), 1))
            {
                e.Graphics.DrawPath(pen, GetRoundedRectPath(0, 0, this.Width - 1, this.Height - 1, 10));
            }
        }

        public void TitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            dragStartPoint = new Point(e.X, e.Y);
        }

        public void TitleBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point dif = Point.Subtract(this.PointToScreen(e.Location), new Size(dragStartPoint));
                this.Location = dif;
            }
        }

        public void TitleBar_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        public void ApplyRoundedCorners()
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

        public GraphicsPath GetRoundedRectPath(int x, int y, int width, int height, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(x, y, radius * 2, radius * 2, 180, 90);
            path.AddArc(x + width - (radius * 2), y, radius * 2, radius * 2, 270, 90);
            path.AddArc(x + width - (radius * 2), y + height - (radius * 2), radius * 2, radius * 2, 0, 90);
            path.AddArc(x, y + height - (radius * 2), radius * 2, radius * 2, 90, 90);
            path.CloseFigure();
            return path;
        }

        public void ConfigureTabControl()
        {
            tabControl.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControl.DrawItem += (sender, e) =>
            {
                Graphics g = e.Graphics;
                TabPage tabPage = tabControl.TabPages[e.Index];
                Rectangle tabBounds = tabControl.GetTabRect(e.Index);
                bool isSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
                Color bgColor;
                Color textColor;
                if (isSelected)
                {
                    bgColor = isDarkMode ?
                        Color.FromArgb(75, 110, 175) : // Koyu tema için seçili sekme
                        Color.FromArgb(0, 123, 255);   // Açık tema için seçili sekme
                    textColor = Color.White;
                }
                else
                {
                    bgColor = isDarkMode ?
                        Color.FromArgb(45, 45, 45) :   // Koyu tema için normal sekme
                        Color.FromArgb(240, 240, 240); // Açık tema için normal sekme
                    textColor = isDarkMode ?
                        Color.LightGray :
                        Color.FromArgb(70, 70, 70);
                }
                // Sekme arka planını çiz
                using (SolidBrush brush = new SolidBrush(bgColor))
                {
                    g.FillRectangle(brush, tabBounds);
                }
                // Sekme metnini çiz
                using (SolidBrush brush = new SolidBrush(textColor))
                {
                    StringFormat sf = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };
                    g.DrawString(tabPage.Text, new Font("Segoe UI", 9.5f, isSelected ? FontStyle.Bold : FontStyle.Regular), brush, tabBounds, sf);
                }
            };
        }

        public void ApplyTheme()
        {
            Debug.WriteLine("ApplyTheme çağrıldı - isDarkMode: " + isDarkMode);
            // Ana form arkaplanı
            this.BackColor = isDarkMode ?
                Color.FromArgb(30, 30, 30) :
                Color.White;
            // Başlık çubuğu
            panelTitleBar.FillColor = isDarkMode ?
                Color.FromArgb(45, 45, 45) :
                Color.FromArgb(0, 123, 255);
            lblTitle.ForeColor = Color.White;
            // Tab sayfalarının arkaplanı
            foreach (TabPage tabPage in tabControl.TabPages)
            {
                tabPage.BackColor = isDarkMode ?
                    Color.FromArgb(35, 35, 35) :
                    Color.FromArgb(248, 249, 250);
            }
            // TextBox kontrolleri
            ApplyTextBoxTheme(txtBackupPath);
            ApplyTextBoxTheme(txtRestoreFile);
            ApplyTextBoxTheme(txtAutoBackupPath);
            // Butonlar
            ApplyButtonTheme(yedekalgozat, "Gözat", (System.Drawing.Image)Properties.Resources.sorguhareket);
            ApplyButtonTheme(yedekle, "Yedekle", (System.Drawing.Image)Properties.Resources.save_icon);
            ApplyButtonTheme(yedekgoster, "Yedek Göster", (System.Drawing.Image)Properties.Resources.klasor);
            ApplyButtonTheme(gozat, "Gözat", (System.Drawing.Image)Properties.Resources.sorguhareket);
            ApplyButtonTheme(geriyukle, "Geri Yükle", (System.Drawing.Image)Properties.Resources._return);
            ApplyButtonTheme(otogozat, "Gözat", (System.Drawing.Image)Properties.Resources.sorguhareket);
            ApplyButtonTheme(otoayarkaydet, "Ayarı Kaydet", (System.Drawing.Image)Properties.Resources.open);
            // RadioButton ve CheckBox kontrolleri
            ApplyRadioButtonTheme(radioDaily, "Günlük");
            ApplyRadioButtonTheme(radioWeekly, "Haftalık");
            ApplyRadioButtonTheme(radioMonthly, "Aylık");
            ApplyRadioButtonTheme(radioCustom, "Yedekleme Aralığı (Dakika)");
            ApplyCheckBoxTheme(chkEnableAutoBackup, "Otomatik Yedeklemeyi Aktif Et");
            // Numeric kontrol
            ApplyNumericUpDownTheme(nudBackupInterval);
            // Label kontrolleri
            ApplyLabelTheme(lblBackupInfo);
            ApplyLabelTheme(lblLastBackupDate);
            ApplyLabelTheme(lblRestoreInfo);
            ApplyLabelTheme(lblLastRestoreInfo);
            ApplyLabelTheme(lblLastAutoBackupInfo);
            // ProgressBar
            progressBarRestore.Style = isDarkMode ?
                MetroFramework.MetroColorStyle.Silver :
                MetroFramework.MetroColorStyle.Blue;
            progressBarRestore.Theme = isDarkMode ?
                MetroFramework.MetroThemeStyle.Dark :
                MetroFramework.MetroThemeStyle.Light;
            // Başlık çubuğu düğmelerini yeniden konumlandır
            RepositionTitleBarButtons();
            // Form güncellemesi
            this.Refresh();
        }

        public void ApplyTextBoxTheme(Guna2TextBox textBox)
        {
            textBox.BorderColor = isDarkMode ?
                Color.FromArgb(60, 60, 60) :
                Color.FromArgb(213, 218, 223);
            textBox.FillColor = isDarkMode ?
                Color.FromArgb(45, 45, 45) :
                Color.White;
            textBox.ForeColor = isDarkMode ?
                Color.White :
                Color.FromArgb(71, 69, 94);
            textBox.PlaceholderForeColor = isDarkMode ?
                Color.Gray :
                Color.Silver;
            textBox.BorderRadius = 8;
            textBox.BorderThickness = 1;
            textBox.Font = new Font("Segoe UI", 9);
        }

        public void ApplyButtonTheme(Guna2Button button, string text, Image icon)
        {
            button.Text = text;
            button.Image = icon;
            button.BorderRadius = 8;
            button.FillColor = isDarkMode ?
                Color.FromArgb(75, 110, 175) :
                Color.FromArgb(0, 123, 255);
            button.ForeColor = Color.White;
            button.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            button.ImageSize = new Size(24, 24);
            button.ImageAlign = HorizontalAlignment.Left;
            button.TextAlign = HorizontalAlignment.Center;
            // Düzeltme: Metin ve resim düzgün konumlandırma
            button.ImageOffset = new Point(5, 0);
            button.TextOffset = new Point(15, 0);
            // Padding ekleyerek içeriği dengele
            button.Padding = new Padding(10, 0, 10, 0);
            button.Cursor = Cursors.Hand;
            // Gölge ayarları
            button.ShadowDecoration.Enabled = true;
            button.ShadowDecoration.Depth = 3;
            button.ShadowDecoration.Color = Color.FromArgb(50, 0, 0, 0);
        }

        public void ApplyRadioButtonTheme(Guna2RadioButton radio, string text)
        {
            radio.Text = text;
            radio.ForeColor = isDarkMode ?
                Color.LightGray :
                Color.FromArgb(60, 60, 60);
            radio.Font = new Font("Segoe UI", 9);
            radio.CheckedState.BorderColor = isDarkMode ?
                Color.FromArgb(75, 110, 175) :
                Color.FromArgb(0, 123, 255);
            radio.CheckedState.FillColor = isDarkMode ?
                Color.FromArgb(75, 110, 175) :
                Color.FromArgb(0, 123, 255);
            radio.UncheckedState.BorderColor = isDarkMode ?
                Color.Gray :
                Color.FromArgb(125, 137, 149);
        }

        public void ApplyCheckBoxTheme(Guna2CheckBox checkBox, string text)
        {
            checkBox.Text = text;
            checkBox.ForeColor = isDarkMode ?
                Color.LightGray :
                Color.FromArgb(60, 60, 60);
            checkBox.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            checkBox.CheckedState.BorderColor = isDarkMode ?
                Color.FromArgb(75, 110, 175) :
                Color.FromArgb(0, 123, 255);
            checkBox.CheckedState.FillColor = isDarkMode ?
                Color.FromArgb(75, 110, 175) :
                Color.FromArgb(0, 123, 255);
            checkBox.UncheckedState.BorderColor = isDarkMode ?
                Color.Gray :
                Color.FromArgb(125, 137, 149);
        }

        public void ApplyNumericUpDownTheme(Guna2NumericUpDown numeric)
        {
            numeric.BorderColor = isDarkMode ?
                Color.FromArgb(60, 60, 60) :
                Color.FromArgb(213, 218, 223);
            numeric.FillColor = isDarkMode ?
                Color.FromArgb(45, 45, 45) :
                Color.White;
            numeric.ForeColor = isDarkMode ?
                Color.White :
                Color.FromArgb(71, 69, 94);
            numeric.BorderRadius = 8;
            numeric.BorderThickness = 1;
            numeric.Font = new Font("Segoe UI", 9);
            numeric.UpDownButtonFillColor = isDarkMode ?
                Color.FromArgb(60, 60, 60) :
                Color.FromArgb(240, 240, 240);
            numeric.UpDownButtonForeColor = isDarkMode ?
                Color.White :
                Color.Gray;
        }

        public void ApplyLabelTheme(Label label)
        {
            label.ForeColor = isDarkMode ?
                Color.LightGray :
                (label.ForeColor == Color.Red ? Color.Red : Color.FromArgb(60, 60, 60));
        }
        #endregion

        #region Not Panelleri
        public void UpdateNotePanels()
        {
            SetYedekNotePanelContent();
            SetGeriYukleNotePanelContent();
            SetOtoYedekNotePanelContent();
        }

        public void SetYedekNotePanelContent()
        {
            if (guna2not == null) return;
            guna2not.Controls.Clear();
            // Panel stilini ayarla
            guna2not.FillColor = isDarkMode ?
                Color.FromArgb(40, 40, 40) :
                Color.FromArgb(248, 249, 250);
            guna2not.BorderColor = isDarkMode ?
                Color.FromArgb(60, 60, 60) :
                Color.FromArgb(213, 218, 223);
            // Başlık Label
            Guna2HtmlLabel lblTitle = new Guna2HtmlLabel
            {
                Text = "Önemli Bilgilendirme",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(220, 53, 69),
                Location = new Point(10, 10),
                AutoSize = true
            };
            // Açıklama metni
            Guna2HtmlLabel lblNote = new Guna2HtmlLabel
            {
                Text = "• Lütfen düzenli olarak yedek alın.<br>" +
                       "• Sistemsel hatalar, donanım arızaları veya elektrik kesintileri nedeniyle veri kaybı yaşanabilir.<br>" +
                       "• <b>PostakipDB.db</b> dosyası otomatik olarak yedeklenmez.<br>" +
                       "• Manuel olarak yedek almayı unutmayın.<br><br>",
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                ForeColor = isDarkMode ? Color.LightGray : Color.FromArgb(60, 60, 60),
                Location = new Point(10, 40),
                Size = new Size(guna2not.Width - 20, 100),
                AutoSize = false
            };
            // "PostakipDB.db" dosyasının konumunu açacak butonu ekle
            Guna2Button btnPosakipDB = new Guna2Button
            {
                Text = "PostakipDB.db Konumunu Aç",
                Location = new Point(10, guna2not.Height - 50),
                Size = new Size(250, 40),
                BorderRadius = 8,
                FillColor = isDarkMode ?
                    Color.FromArgb(75, 110, 175) :
                    Color.FromArgb(0, 123, 255),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Image = Properties.Resources.klasor,
                ImageAlign = HorizontalAlignment.Left,
                ImageSize = new Size(24, 24),
                TextAlign = HorizontalAlignment.Center,
                TextOffset = new Point(15, 0),
                ImageOffset = new Point(5, 0),
                Cursor = Cursors.Hand
            };
            // Gölge ayarları
            btnPosakipDB.ShadowDecoration.Enabled = true;
            btnPosakipDB.ShadowDecoration.Depth = 3;
            btnPosakipDB.ShadowDecoration.Color = Color.FromArgb(50, 0, 0, 0);
            btnPosakipDB.Click += posdbgoster_Click;
            // Panelin içine eklemeler
            guna2not.Controls.Add(lblTitle);
            guna2not.Controls.Add(lblNote);
            guna2not.Controls.Add(btnPosakipDB);
        }

        public void SetGeriYukleNotePanelContent()
        {
            if (geriyuklenott == null) return;
            geriyuklenott.Controls.Clear();
            // Panel stilini ayarla
            geriyuklenott.FillColor = isDarkMode ?
                Color.FromArgb(40, 40, 40) :
                Color.FromArgb(248, 249, 250);
            geriyuklenott.BorderColor = isDarkMode ?
                Color.FromArgb(60, 60, 60) :
                Color.FromArgb(213, 218, 223);
            // Başlık Label
            Guna2HtmlLabel lblTitle = new Guna2HtmlLabel
            {
                Text = "Geri Yükleme Önemli Bilgilendirme",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(220, 53, 69),
                Location = new Point(10, 10),
                AutoSize = true
            };
            // Açıklama metni
            Guna2HtmlLabel lblNote = new Guna2HtmlLabel
            {
                Text = "• Not: Geri yükleme işleminde veresiye üzerinde bulunan bilgilerin tamamı silinecektir.<br>" +
                       "• Sadece yedek dosyası (veresiye.db) yüklenecektir.<br>" +
                       "• <b>PostakipDB.db</b> dosyasını manuel olarak yüklemelisiniz.<br>" +
                       "• Gözat butonuna tıkla dosyası seç ve geri yükle.<br><br>" +
                       "• (PostakipDB.db) dosyanızın konumunu kontrol etmeyi unutmayın",
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                ForeColor = isDarkMode ? Color.LightGray : Color.FromArgb(60, 60, 60),
                Location = new Point(10, 40),
                Size = new Size(geriyuklenott.Width - 20, 120),
                AutoSize = false
            };
            // "PostakipDB.db" dosyasının konumunu açacak butonu ekle
            Guna2Button btnPosakipDB = new Guna2Button
            {
                Text = "PostakipDB.db Konumunu Aç",
                Location = new Point(10, geriyuklenott.Height - 50),
                Size = new Size(250, 40),
                BorderRadius = 8,
                FillColor = isDarkMode ?
                    Color.FromArgb(75, 110, 175) :
                    Color.FromArgb(0, 123, 255),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Image = Properties.Resources.klasor,
                ImageAlign = HorizontalAlignment.Left,
                ImageSize = new Size(24, 24),
                TextAlign = HorizontalAlignment.Center,
                TextOffset = new Point(15, 0),
                ImageOffset = new Point(5, 0),
                Cursor = Cursors.Hand
            };
            // Gölge ayarları
            btnPosakipDB.ShadowDecoration.Enabled = true;
            btnPosakipDB.ShadowDecoration.Depth = 3;
            btnPosakipDB.ShadowDecoration.Color = Color.FromArgb(50, 0, 0, 0);
            btnPosakipDB.Click += posdbgoster_Click;
            // Panelin içine eklemeler
            geriyuklenott.Controls.Add(lblTitle);
            geriyuklenott.Controls.Add(lblNote);
            geriyuklenott.Controls.Add(btnPosakipDB);
        }

        public void SetOtoYedekNotePanelContent()
        {
            if (otonotpanel == null) return;
            otonotpanel.Controls.Clear();
            // Panel stilini ayarla
            otonotpanel.FillColor = isDarkMode ?
                Color.FromArgb(40, 40, 40) :
                Color.FromArgb(248, 249, 250);
            otonotpanel.BorderColor = isDarkMode ?
                Color.FromArgb(60, 60, 60) :
                Color.FromArgb(213, 218, 223);
            // Başlık Label
            Guna2HtmlLabel lblTitle = new Guna2HtmlLabel
            {
                Text = "Otomatik Yedekleme Talimatları",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(220, 53, 69),
                Location = new Point(10, 10),
                AutoSize = true
            };
            // Açıklama metni
            Guna2HtmlLabel lblNote = new Guna2HtmlLabel
            {
                Text = "1. Otomatik yedeklemeyi etkinleştirmek için 'Otomatik Yedeklemeyi Aktif Et' seçeneğini işaretleyin.<br>" +
                       "2. Yedekleme yapılacak klasörü 'Gözat' butonuna tıklayarak seçin.<br>" +
                       "3. Günlük, haftalık veya aylık yedekleme seçeneklerinden birini seçin.<br>" +
                       "4. Kendi belirlediğiniz bir aralık için 'Yedekleme Aralığı (Dakika)' seçeneğini kullanabilirsiniz.<br>" +
                       "5. Ayarları kaydetmek için 'Ayarları Kaydet' butonuna tıklayın.<br>" +
                       "Not: Belirlediğiniz zaman aralığında otomatik yedekleme yapılacaktır.",
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                ForeColor = isDarkMode ? Color.LightGray : Color.FromArgb(60, 60, 60),
                Location = new Point(10, 40),
                Size = new Size(otonotpanel.Width - 20, 140),
                AutoSize = false
            };
            // Panelin içine eklemeler
            otonotpanel.Controls.Add(lblTitle);
            otonotpanel.Controls.Add(lblNote);
        }

        public void posdbgoster_Click(object sender, EventArgs e)
        {
            try
            {
                string filePath = Path.Combine(Application.StartupPath, "PostakipDB.db");
                if (File.Exists(filePath))
                {
                    Process.Start("explorer.exe", $"/select,{filePath}");
                }
                else
                {
                    MessageBox.Show("PostakipDB.db dosyası bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region İşlevsel Metodlar
        public void AutoBackup()
        {
            try
            {
                // Kullanıcının belirlediği yedekleme yolunu al
                string yedekKlasoru = txtBackupPath.Text.Trim();
                // Eğer yedekleme yolu boşsa "Acil Yedek Dosyaları" klasörünü oluştur
                if (string.IsNullOrEmpty(yedekKlasoru))
                {
                    yedekKlasoru = Path.Combine(Application.StartupPath, "Acil Yedek Dosyaları");
                    txtBackupPath.Text = yedekKlasoru;
                    // Varsayılan yedekleme yolunu kaydet
                    Properties.Settings.Default.LastBackupPath = yedekKlasoru;
                    Properties.Settings.Default.Save();
                }
                // Eğer klasör yoksa oluştur
                if (!Directory.Exists(yedekKlasoru))
                {
                    Directory.CreateDirectory(yedekKlasoru);
                }
                string yedekDosyaAdi = Path.Combine(yedekKlasoru, $"Yedek_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.bak");
                // Veritabanı dosyası yoksa hata ver
                if (!File.Exists("veresiye.db"))
                {
                    throw new Exception("Veritabanı dosyası bulunamadı! Yedekleme yapılamadı.");
                }
                // Eğer dosya zaten varsa, üzerine yazmak için işlemi başlat
                if (File.Exists(yedekDosyaAdi))
                {
                    File.Delete(yedekDosyaAdi); // Eski dosyayı sil
                }
                File.Copy("veresiye.db", yedekDosyaAdi, true);
                // Kullanıcıya yedekleme bilgisi ver
                MessageBox.Show($"✅ Otomatik Yedekleme Tamamlandı!\n📂 Yedek Konumu: {yedekDosyaAdi}",
                                "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Otomatik yedekleme başarısız: {ex.Message}",
                                "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Otomatik yedekleme zamanlayıcısını başlat
        public void InitializeAutoBackupTimer()
        {
            if (autoBackupTimer != null)
            {
                autoBackupTimer.Stop();
                autoBackupTimer.Dispose();
            }
            // Yeni bir timer oluştur - burada değer ataması yapılıyor
            autoBackupTimer = new System.Windows.Forms.Timer();
            // Süreyi hesapla
            int interval = CalculateIntervalFromSettings();
            // Geçersiz süre kontrolü
            if (interval <= 0)
            {
                MessageBox.Show("Otomatik yedekleme için geçerli bir süre seçin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // İşlemi iptal et
            }
            // Timer'ı başlat
            autoBackupTimer.Interval = interval;
            autoBackupTimer.Tick += AutoBackupTimer_Tick;
            autoBackupTimer.Start();
        }

        public int CalculateIntervalFromSettings()
        {
            string period = Properties.Settings.Default.AutoBackupPeriod;
            if (period == "Daily")
                return (int)TimeSpan.FromDays(1).TotalMilliseconds;
            if (period == "Weekly")
                return (int)TimeSpan.FromDays(7).TotalMilliseconds;
            if (period == "Monthly")
                return (int)TimeSpan.FromDays(30).TotalMilliseconds;
            // Dakika bazlı yedekleme
            if (period == "Custom")
            {
                return Properties.Settings.Default.AutoBackupInterval * 60 * 1000; // Dakika -> Milisaniye
            }
            return 0; // Geçersiz ayar
        }

        public void AutoBackupTimer_Tick(object sender, EventArgs e)
        {
            string autoBackupFolder = Properties.Settings.Default.AutoBackupPath;
            if (string.IsNullOrWhiteSpace(autoBackupFolder) || !Directory.Exists(autoBackupFolder))
            {
                MessageBox.Show("Geçersiz otomatik yedekleme yolu. Lütfen ayarları kontrol edin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                autoBackupTimer.Stop();
                return;
            }
            SaveAutoBackup(autoBackupFolder);
        }

        public void SaveAutoBackup(string autoBackupFolder)
        {
            // Yedekleme türüne göre alt klasör oluştur
            string backupType = Properties.Settings.Default.AutoBackupPeriod;
            string subFolderName = backupType switch
            {
                "Daily" => "Günlük",
                "Weekly" => "Haftalık",
                "Monthly" => "Aylık",
                _ => "Diğer" // Varsayılan
            };
            string backupSubFolder = Path.Combine(autoBackupFolder, subFolderName);
            // Alt klasör oluşturulmazsa oluştur
            if (!Directory.Exists(backupSubFolder))
            {
                Directory.CreateDirectory(backupSubFolder);
            }
            // Yedekleme dosyasını alt klasöre oluştur
            string autoBackupFile = Path.Combine(backupSubFolder, $"{firmaAdi}-{DateTime.Now:dd.MM.yyyy-HH.mm.ss}-AutoBackup.bak");
            // Log dosyasını kontrol et ve oluştur
            string logFilePath = Path.Combine(autoBackupFolder, "OtomatikYedeklemelog.txt");
            if (!File.Exists(logFilePath))
            {
                File.WriteAllText(logFilePath, "Otomatik Yedekleme Log Dosyası\n-------------------------\n");
            }
            try
            {
                File.Copy("veresiye.db", autoBackupFile, true);
                // Log dosyasına yaz
                File.AppendAllText(logFilePath, $"[Otomatik Yedekleme] {DateTime.Now}: Yedekleme başarıyla oluşturuldu - {autoBackupFile}{Environment.NewLine}");
                // Son yedekleme bilgisini güncelle
                Properties.Settings.Default.LastAutoBackupTime = $"{DateTime.Now:dd.MM.yyyy HH:mm:ss}";
                Properties.Settings.Default.Save();
                lblLastAutoBackupInfo.Text = $"Son Oto Yedek: {Properties.Settings.Default.LastAutoBackupTime}";
            }
            catch (Exception ex)
            {
                // Log dosyasına hata yaz
                File.AppendAllText(logFilePath, $"[Otomatik Yedekleme] {DateTime.Now}: Yedekleme sırasında hata - {ex.Message}{Environment.NewLine}");
                MessageBox.Show($"Oto yedekleme sırasında bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void LoadAutoBackupSettings()
        {
            chkEnableAutoBackup.Checked = Properties.Settings.Default.AutoBackupEnabled;
            txtAutoBackupPath.Text = Properties.Settings.Default.AutoBackupPath;
            string period = Properties.Settings.Default.AutoBackupPeriod;
            if (period == "Daily")
                radioDaily.Checked = true;
            else if (period == "Weekly")
                radioWeekly.Checked = true;
            else if (period == "Monthly")
                radioMonthly.Checked = true;
            else
            {
                radioCustom.Checked = true;
                nudBackupInterval.Value = Properties.Settings.Default.AutoBackupInterval > 0
                    ? Properties.Settings.Default.AutoBackupInterval
                    : 5; // Varsayılan: 5 dakika
            }
            lblLastAutoBackupInfo.Text = string.IsNullOrEmpty(Properties.Settings.Default.LastAutoBackupTime)
                ? "Son Oto Yedek: Henüz yapılmadı"
                : $"Son Oto Yedek: {Properties.Settings.Default.LastAutoBackupTime}";
        }

        public void ShowLastBackupInfo()
        {
            string lastBackupPath = Properties.Settings.Default.LastBackupPath;
            if (!string.IsNullOrEmpty(lastBackupPath))
            {
                MessageBox.Show($"📂 Son yedek konumu: {lastBackupPath}", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region Buton Olayları
        // Yedek Al - Gözat
        public void yedekalgozat_Click(object sender, EventArgs e)
        {
            try
            {
                using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
                {
                    folderDialog.Description = "Yedekleme için bir klasör seçin.";
                    folderDialog.ShowNewFolderButton = true;
                    if (folderDialog.ShowDialog() == DialogResult.OK)
                    {
                        txtBackupPath.Text = folderDialog.SelectedPath;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message);
            }
        }

        // Yedek Al - Şimdi Yedekle
        public void yedekle_Click(object sender, EventArgs e)
        {
            string backupPath = txtBackupPath.Text;
            if (string.IsNullOrWhiteSpace(backupPath) || !Directory.Exists(backupPath))
            {
                MessageBox.Show("Geçerli bir yedekleme yolu seçin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Firma adını dosya adına ekliyoruz
            string backupFileName = Path.Combine(backupPath, $"{firmaAdi}-{DateTime.Now:dd.MM.yyyy-HH.mm.ss} - Yedek.bak");
            try
            {
                File.Copy("veresiye.db", backupFileName, true);
                // Son yedek tarihini ve dosya yolunu ayarlara kaydet
                Properties.Settings.Default.LastBackupPath = backupPath;
                Properties.Settings.Default.LastBackupDate = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
                Properties.Settings.Default.Save();
                // Label'i güncelle
                lblLastBackupDate.Text = $"Son Yedek Alma: {DateTime.Now:dd.MM.yyyy HH:mm:ss}";
                MessageBox.Show("Yedekleme başarıyla tamamlandı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Yedekleme durumu panelini güncelle
                UpdateYedeklemeDurumuPanel();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Yedekleme sırasında bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Geri Yükle - Gözat
        public void gozat_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Backup Files (*.bak)|*.bak|All Files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtRestoreFile.Text = openFileDialog.FileName;
                }
            }
        }

        // Geri Yükle - Şimdi Geri Yükle
        public async void geriyukle_Click(object sender, EventArgs e)
        {
            string restoreFile = txtRestoreFile.Text;
            if (string.IsNullOrWhiteSpace(restoreFile) || !File.Exists(restoreFile))
            {
                MessageBox.Show("Geçerli bir yedekleme dosyası seçin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                progressBarRestore.Visible = true;
                progressBarRestore.Value = 0;
                // ProgressBar'ın durumunu gösteren bir async işlem
                await Task.Run(() =>
                {
                    for (int i = 0; i <= 100; i += 10)
                    {
                        System.Threading.Thread.Sleep(100);
                        Invoke(new Action(() => progressBarRestore.Value = i));
                    }
                });
                // Yedekleme dosyasını geri yükle
                File.Copy(restoreFile, "veresiye.db", true);
                lblLastRestoreInfo.Text = $"Son Geri Yükleme: {Path.GetFileName(restoreFile)} - {DateTime.Now:dd.MM.yyyy HH:mm:ss}";
                MessageBox.Show("Geri yükleme başarıyla tamamlandı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                OnRestoreCompleted?.Invoke(); // Event'i tetikle
                // Formu kapat
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Geri yükleme sırasında bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                progressBarRestore.Visible = false;
            }
        }

        public void yedekgoster_Click(object sender, EventArgs e)
        {
            try
            {
                string lastBackupPath = Properties.Settings.Default.LastBackupPath;

                if (string.IsNullOrEmpty(lastBackupPath))
                {
                    MessageBox.Show("Henüz bir yedekleme yapılmamış veya yedekleme konumu bulunamadı.",
                        "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Klasör var mı kontrol et
                if (Directory.Exists(lastBackupPath))
                {
                    // En son yedek dosyasını ara
                    string lastBackupFile = FindLastBackupFile(lastBackupPath);

                    if (!string.IsNullOrEmpty(lastBackupFile))
                    {
                        // Explorer'ı aç ve dosyayı seçili göster
                        Process.Start("explorer.exe", $"/select,\"{lastBackupFile}\"");
                    }
                    else
                    {
                        // Klasör var ama içinde .bak dosyası yoksa sadece klasörü aç
                        Process.Start("explorer.exe", lastBackupPath);
                        MessageBox.Show($"Yedekleme klasörünüz açıldı, ancak klasörde hiç .bak uzantılı yedek dosyası bulunamadı.\nKlasör: {lastBackupPath}",
                            "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    // Klasör artık mevcut değilse kullanıcıya bildir
                    MessageBox.Show($"Belirtilen yedekleme klasörü bulunamadı: {lastBackupPath}\nLütfen yeni bir yedekleme yapın.",
                        "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Yedekleme klasörü açılırken bir hata oluştu: {ex.Message}",
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // En son alınan yedek dosyasını bulmak için yardımcı metot
        public string FindLastBackupFile(string backupFolderPath)
        {
            try
            {
                string[] backupFiles = Directory.GetFiles(backupFolderPath, "*.bak");

                if (backupFiles.Length == 0)
                    return null;

                // Öncelikle firma adı ve tarih formatına göre en son yedek dosyasını bulmaya çalış
                // Dosya adı formatı: "{firmaAdi}-{tarih} - Yedek.bak" veya "Yedek_{tarih}.bak" olabilir

                // Yedekleme tarihine göre sırala (önce dosya adından tarih çıkarmayı dene)
                var sortedByName = backupFiles
                    .Select(f => new
                    {
                        FilePath = f,
                        FileName = Path.GetFileName(f),
                        CreationTime = File.GetCreationTime(f)
                    })
                    .OrderByDescending(f => f.CreationTime); // Önce oluşturma tarihine göre sırala

                // Eğer firma adımız varsa ve dosya adında da geçiyorsa, öncelik ver
                if (!string.IsNullOrEmpty(firmaAdi))
                {
                    var firmaBakupFiles = sortedByName.Where(f => f.FileName.Contains(firmaAdi)).ToList();
                    if (firmaBakupFiles.Any())
                    {
                        return firmaBakupFiles.First().FilePath;
                    }
                }

                // Firma adına göre filtreleme yapılamadıysa, en son oluşturulan dosyayı döndür
                return sortedByName.First().FilePath;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Son yedek dosyası aranırken hata: {ex.Message}");
                return null;
            }
        }

        public void otogozat_Click(object sender, EventArgs e)
        {
            using (var folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    txtAutoBackupPath.Text = folderDialog.SelectedPath;
                }
            }
        }

        public void otoayarkaydet_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.AutoBackupEnabled = chkEnableAutoBackup.Checked;
            Properties.Settings.Default.Save();
            if (string.IsNullOrWhiteSpace(txtAutoBackupPath.Text))
            {
                MessageBox.Show("Lütfen otomatik yedekleme için bir klasör yolu seçin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!Directory.Exists(txtAutoBackupPath.Text))
            {
                MessageBox.Show("Seçilen dosya yolu geçersiz. Lütfen doğru bir klasör seçin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            UpdateYedeklemeDurumuPanel();
            Properties.Settings.Default.AutoBackupEnabled = chkEnableAutoBackup.Checked;
            Properties.Settings.Default.AutoBackupPath = txtAutoBackupPath.Text;
            if (radioDaily.Checked)
                Properties.Settings.Default.AutoBackupPeriod = "Daily";
            else if (radioWeekly.Checked)
                Properties.Settings.Default.AutoBackupPeriod = "Weekly";
            else if (radioMonthly.Checked)
                Properties.Settings.Default.AutoBackupPeriod = "Monthly";
            else
            {
                Properties.Settings.Default.AutoBackupPeriod = "Custom";
                Properties.Settings.Default.AutoBackupInterval = (int)nudBackupInterval.Value;
            }
            Properties.Settings.Default.Save();
            InitializeAutoBackupTimer();
            MessageBox.Show("Oto Yedekleme Ayarları Kaydedildi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Boyut hesaplama butonu için olay işleyicisi
        public async void btnCalculateSize_Click(object sender, EventArgs e)
        {
            try
            {
                lblEstimatedSize.Text = "Boyut hesaplanıyor...";
                lblEstimatedSize.ForeColor = isDarkMode ? Color.LightGray : Color.FromArgb(70, 70, 70);
                btnCalculateSize.Enabled = false;
                // Yedekleme dosyasının bulunduğu yol
                string dbPath = "veresiye.db";
                // Asenkron olarak dosya boyutunu hesapla
                long fileSize = await Task.Run(() => {
                    if (!File.Exists(dbPath))
                        return 0;
                    FileInfo fileInfo = new FileInfo(dbPath);
                    // Yaklaşık sıkıştırma oranını hesapla (genellikle veritabanları %30-70 arası sıkıştırılabilir)
                    double compressionRatio = 0.6; // %60 oranında sıkıştırma tahmini
                    return (long)(fileInfo.Length * compressionRatio);
                });
                // Boyutu okunabilir formata dönüştür (KB, MB, GB)
                string readableSize = GetReadableFileSize(fileSize);
                // Sonucu göster
                lblEstimatedSize.Text = $"Tahmini Yedek Boyutu: {readableSize}";
                // Hedef diskte yeterli alan var mı kontrol et
                if (!string.IsNullOrEmpty(txtBackupPath.Text) && Directory.Exists(txtBackupPath.Text))
                {
                    DriveInfo drive = new DriveInfo(Path.GetPathRoot(txtBackupPath.Text));
                    if (drive.AvailableFreeSpace < fileSize)
                    {
                        lblEstimatedSize.ForeColor = Color.Red;
                        lblEstimatedSize.Text += " - UYARI: Hedef diskte yeterli alan yok!";
                    }
                    else
                    {
                        lblEstimatedSize.ForeColor = Color.Green;
                    }
                }
            }
            catch (Exception ex)
            {
                lblEstimatedSize.Text = "Boyut hesaplanamadı: " + ex.Message;
                lblEstimatedSize.ForeColor = Color.Red;
            }
            finally
            {
                btnCalculateSize.Enabled = true;
            }
        }

        // Dosya boyutunu okunabilir formata dönüştüren yardımcı metot
        public string GetReadableFileSize(long size)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double len = size;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }
            return $"{len:0.##} {sizes[order]}";
        }

        // Takvimi gösteren buton için olay işleyicisi
        public void btnShowCalendar_Click(object sender, EventArgs e)
        {
            try
            {
                // Takvim popup'ını göster
                using (PopupCalendarForm calendarForm = new PopupCalendarForm(txtBackupPath.Text, isDarkMode))
                {
                    calendarForm.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Takvim açılırken bir hata oluştu: " + ex.Message,
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void datePicker_ValueChanged(object sender, EventArgs e)
        {
            // Seçilen tarihe göre takvimi yenile
            LoadBackupCalendar();
        }

        public void LoadBackupCalendar()
        {
            try
            {
                // Tarih seçiciden seçilen ay ve yıl bilgisini al
                DateTime selectedDate = datePicker.Value;
                int year = selectedDate.Year;
                int month = selectedDate.Month;
                // Ayın ilk gününü bul
                DateTime firstDayOfMonth = new DateTime(year, month, 1);
                // Ayın son gününü bul
                DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                // Haftanın günleri için başlıkları ekle
                string[] dayNames = { "Pzt", "Sal", "Çar", "Per", "Cum", "Cmt", "Paz" };
                for (int i = 0; i < 7; i++)
                {
                    Label lblDay = new Label();
                    lblDay.Text = dayNames[i];
                    lblDay.Font = new Font("Segoe UI", 8, FontStyle.Bold);
                    lblDay.Size = new Size(40, 20);
                    lblDay.TextAlign = ContentAlignment.MiddleCenter;
                    lblDay.Location = new Point(10 + i * 45, 50);
                }
                // Ayın başladığı gün (0: Pazartesi, ... 6: Pazar)
                int firstDayOfWeek = ((int)firstDayOfMonth.DayOfWeek + 6) % 7;
                // Ayın günlerini takvimde göster
                for (int i = 1; i <= lastDayOfMonth.Day; i++)
                {
                    DateTime currentDate = new DateTime(year, month, i);
                    int row = (firstDayOfWeek + i - 1) / 7;
                    int col = (firstDayOfWeek + i - 1) % 7;
                    // Gün butonu
                    Guna2Button btnDay = new Guna2Button();
                    btnDay.Text = i.ToString();
                    btnDay.Size = new Size(40, 30);
                    btnDay.BorderRadius = 5;
                    btnDay.Location = new Point(10 + col * 45, 75 + row * 35);
                    btnDay.Tag = currentDate.ToString("yyyy-MM-dd");
                    // Bugün ise farklı renkte göster
                    if (currentDate.Date == DateTime.Now.Date)
                    {
                        btnDay.FillColor = Color.LightBlue;
                        btnDay.ForeColor = Color.Black;
                    }
                    // Bu tarihte yedekleme var mı kontrol et
                    bool hasBackup = CheckIfBackupExistsOnDate(currentDate);
                    if (hasBackup)
                    {
                        btnDay.FillColor = Color.FromArgb(0, 180, 0); // Yeşil
                        btnDay.ForeColor = Color.White;
                        btnDay.Click += BtnDay_Click; // Yedekleme detaylarını göster
                    }
                }
                // Takvim başlığı ekle
                Label lblCalendarTitle = new Label();
                lblCalendarTitle.Text = $"{GetMonthName(month)} {year}";
                lblCalendarTitle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                lblCalendarTitle.Size = new Size(200, 20);
                lblCalendarTitle.Location = new Point(70, 50);
                lblCalendarTitle.TextAlign = ContentAlignment.MiddleCenter;
                // Takvimi kapat butonu ekle
                Guna2Button btnCloseCalendar = new Guna2Button();
                btnCloseCalendar.Text = "X";
                btnCloseCalendar.Size = new Size(30, 30);
                btnCloseCalendar.BorderRadius = 15;
                btnCloseCalendar.FillColor = Color.Red;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Takvim yüklenirken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Tarihte yedekleme var mı kontrol eden metot
        public bool CheckIfBackupExistsOnDate(DateTime date)
        {
            try
            {
                string backupPath = txtBackupPath.Text;
                if (string.IsNullOrEmpty(backupPath) || !Directory.Exists(backupPath))
                    return false;
                // Yedekleme dosyalarını ara
                string[] backupFiles = Directory.GetFiles(backupPath, "*.bak");
                // Dosya adından tarihi çıkar ve kontrol et
                foreach (string file in backupFiles)
                {
                    string fileName = Path.GetFileName(file);
                    // Dosya isminden tarih bilgisini çıkarmaya çalış
                    if (fileName.Contains(date.ToString("yyyy-MM-dd")) ||
                        fileName.Contains(date.ToString("dd.MM.yyyy")))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        // Yedekleme olan güne tıklandığında çalışacak metot
        public void BtnDay_Click(object sender, EventArgs e)
        {
            try
            {
                Guna2Button btn = sender as Guna2Button;
                if (btn != null && btn.Tag != null)
                {
                    string dateStr = btn.Tag.ToString();
                    DateTime selectedDate = DateTime.Parse(dateStr);
                    // O tarihteki yedeklemeleri listele
                    List<string> backups = GetBackupsOnDate(selectedDate);
                    if (backups.Count > 0)
                    {
                        // Yedekleme listesini göster
                        string message = $"{selectedDate.ToShortDateString()} tarihindeki yedeklemeler:\n\n";
                        foreach (string backup in backups)
                        {
                            message += "• " + Path.GetFileName(backup) + "\n";
                        }
                        MessageBox.Show(message, "Yedekleme Bilgisi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Bu tarihte yedekleme bulunamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Yedekleme bilgisi görüntülenirken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Belirli bir tarihteki tüm yedeklemeleri bulan metot
        public List<string> GetBackupsOnDate(DateTime date)
        {
            List<string> backups = new List<string>();
            try
            {
                string backupPath = txtBackupPath.Text;
                if (string.IsNullOrEmpty(backupPath) || !Directory.Exists(backupPath))
                    return backups;
                // Yedekleme dosyalarını ara
                string[] backupFiles = Directory.GetFiles(backupPath, "*.bak");
                // Dosya adından tarihi çıkar ve kontrol et
                foreach (string file in backupFiles)
                {
                    string fileName = Path.GetFileName(file);
                    // Dosya isminden tarih bilgisini çıkarmaya çalış
                    if (fileName.Contains(date.ToString("yyyy-MM-dd")) ||
                        fileName.Contains(date.ToString("dd.MM.yyyy")))
                    {
                        backups.Add(file);
                    }
                }
            }
            catch
            {
                // Hata durumunda boş liste döndür
            }
            return backups;
        }

        // Ay adını döndüren yardımcı metot
        public string GetMonthName(int month)
        {
            string[] months = {
                "Ocak", "Şubat", "Mart", "Nisan", "Mayıs", "Haziran",
                "Temmuz", "Ağustos", "Eylül", "Ekim", "Kasım", "Aralık"
            };
            return months[month - 1];
        }

        // Bulut yedekleme butonu tıklandığında
        public void btnCloudBackup_Click(object sender, EventArgs e)
        {
            // Panel görünürlüğünü değiştir
            isCloudPanelVisible = !isCloudPanelVisible;
            panelCloudBackup.Visible = isCloudPanelVisible;
            // Panel görünürse, diğer kontrolleri devre dışı bırak
            foreach (Control control in this.Controls)
            {
                if (control != panelCloudBackup && control is TabControl == false)
                {
                    control.Enabled = !isCloudPanelVisible;
                }
            }
            // Panel görünür hale geldiğinde, en üstte olmasını sağla
            if (isCloudPanelVisible)
            {
                panelCloudBackup.BringToFront();
            }
        }

        

        // CloudManager başlatma ve UI'ya bağlama
        public CloudStorageManager cloudManager;
        public void InitializeCloudManagerForPanel(Label statusLabel, Guna2Button backupButton)
        {
            // CloudManager örneğini oluştur (eğer yoksa)
            if (cloudManager == null)
            {
                cloudManager = new CloudStorageManager();
            }
            // Olayları bağla
            cloudManager.ConnectionStateChanged += (s, connected) =>
            {
                this.Invoke(new Action(() => {
                    statusLabel.Text = connected ?
                        $"Durum: Bağlı ({cloudManager.CurrentService})" :
                        "Durum: Bağlı değil";
                    statusLabel.ForeColor = connected ? Color.Green : Color.Red;
                    backupButton.Enabled = connected;
                }));
            };
            cloudManager.StatusMessageChanged += (s, message) =>
            {
                this.Invoke(new Action(() => {
                    statusLabel.Text = $"Durum: {message}";
                    statusLabel.ForeColor = message.Contains("başarı") ? Color.Green : Color.Red;
                }));
            };
            // Mevcut bağlantı durumunu ayarla
            backupButton.Enabled = cloudManager.IsConnected;
            statusLabel.Text = cloudManager.IsConnected ?
                $"Durum: Bağlı ({cloudManager.CurrentService})" :
                "Durum: Bağlı değil";
            statusLabel.ForeColor = cloudManager.IsConnected ? Color.Green : Color.Red;
        }

        // Bulut servisine bağlanma
        public async void ConnectToCloudService(string serviceName, string folder)
        {
            CloudStorageManager.CloudServiceType serviceType;
            switch (serviceName)
            {
                case "Google Drive":
                    serviceType = CloudStorageManager.CloudServiceType.GoogleDrive;
                    break;
                case "Dropbox":
                    serviceType = CloudStorageManager.CloudServiceType.Dropbox;
                    break;
                case "OneDrive":
                    serviceType = CloudStorageManager.CloudServiceType.OneDrive;
                    break;
                default:
                    MessageBox.Show("Lütfen desteklenen bir bulut servisi seçin", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
            }
            try
            {
                await cloudManager.ConnectAsync(serviceType, folder);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bağlantı hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // FTP sunucusuna bağlanma
        public void ConnectToFTP(string server, string username, string password)
        {
            if (string.IsNullOrEmpty(server) || string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Lütfen gerekli tüm FTP bilgilerini girin", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                cloudManager.ConnectToFTP(server, username, password, "");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"FTP bağlantı hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Buluta yedekleme
        public async void BackupToCloud()
        {
            if (!cloudManager.IsConnected)
            {
                MessageBox.Show("Lütfen önce bir bulut servisine bağlanın", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Geçici yedek dosyası oluştur
            string tempBackupFile = Path.Combine(Path.GetTempPath(), $"{firmaAdi}-{DateTime.Now:yyyy-MM-dd-HH-mm-ss}.bak");
            try
            {
                // Veritabanını geçici dosyaya kopyala
                File.Copy("veresiye.db", tempBackupFile, true);
                // Buluta yükle
                bool success = await cloudManager.UploadFileAsync(tempBackupFile);
                if (success)
                {
                    MessageBox.Show("Yedek başarıyla buluta yüklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Yedekleme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Geçici dosyayı temizle
                if (File.Exists(tempBackupFile))
                    File.Delete(tempBackupFile);
            }
        }

        public void PositionCloudPanel()
        {
            if (panelCloudBackup != null)
            {
                // Paneli ortala
                panelCloudBackup.Location = new Point(
                    (this.ClientSize.Width - panelCloudBackup.Width) / 2,
                    (this.ClientSize.Height - panelCloudBackup.Height) / 2);
            }
        }
        #endregion

        public void btnClose_Click_1(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // Bulut servislerine bağlanma düğmesi tıklandığında
        public async void btnCloudConnect_Click(object sender, EventArgs e)
        {
            string selectedService = cmbCloudService.SelectedItem.ToString();
            string folderName = txtCloudFolder.Text.Trim();
            try
            {
                this.Cursor = Cursors.WaitCursor;
                lblCloudStatus.Text = "Bağlanıyor...";
                lblCloudStatus.ForeColor = Color.Blue;
                switch (selectedService)
                {
                    case "Google Drive":
                        await ConnectToGoogleDrive(folderName);
                        break;
                    case "Dropbox":
                        await ConnectToDropbox(folderName);
                        break;
                    case "OneDrive":
                        await ConnectToOneDrive(folderName);
                        break;
                    default:
                        MessageBox.Show("Lütfen desteklenen bir servis seçin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            catch (Exception ex)
            {
                lblCloudStatus.Text = "Bağlantı hatası: " + ex.Message;
                lblCloudStatus.ForeColor = Color.Red;
                MessageBox.Show("Bağlantı sırasında bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        public async Task ConnectToGoogleDrive(string folderName)
        {
            try
            {
                string[] scopes = { DriveService.Scope.DriveFile }; // Sadece dosya erişimi için izin
                UserCredential credential;
                string credPath = Path.Combine(Application.StartupPath, "token.json");
                // Google'ın kullanacağı yönlendirme URI'sini açıkça belirt
                string redirectUri = "http://localhost";
                // Kimlik bilgilerini yapılandır
                var clientSecrets = new ClientSecrets
                {
                    ClientId = GoogleClientId,
                    ClientSecret = GoogleClientSecret
                };
                // OAuth yetkilendirme akışını başlat (redirectUri açıkça belirtiliyor)
                var flow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
                {
                    ClientSecrets = clientSecrets,
                    Scopes = scopes,
                    DataStore = new FileDataStore(credPath, true)
                });
                // Tarayıcıda yetkilendirme sayfasını göster (LocalServerCodeReceiver kullanarak)
                var codeReceiver = new LocalServerCodeReceiver(redirectUri);
                // Yetkilendirme işlemini gerçekleştir
                credential = await new AuthorizationCodeInstalledApp(flow, codeReceiver).AuthorizeAsync(
                    "user", CancellationToken.None);
                // Kullanıcıya bilgi ver
                Debug.WriteLine("Kimlik doğrulama başarılı. Token kaydedildi: " + credPath);
                // Drive servisini oluştur
                _driveService = new DriveService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "Veresiye Bulut Yedekleme",
                });
                // Hedef klasörün varlığını kontrol et
                if (!string.IsNullOrEmpty(folderName))
                {
                    var listRequest = _driveService.Files.List();
                    listRequest.Q = $"mimeType='application/vnd.google-apps.folder' and name='{folderName}' and trashed=false";
                    var folders = await listRequest.ExecuteAsync();
                    // Klasör yoksa oluştur
                    if (folders.Files.Count == 0)
                    {
                        var folderMetadata = new Google.Apis.Drive.v3.Data.File()
                        {
                            Name = folderName,
                            MimeType = "application/vnd.google-apps.folder"
                        };
                        var request = _driveService.Files.Create(folderMetadata);
                        request.Fields = "id";
                        var folder = await request.ExecuteAsync();
                        _targetFolder = folder.Id; // Klasör ID'sini sakla
                    }
                    else
                    {
                        _targetFolder = folders.Files[0].Id; // Mevcut klasör ID'sini sakla
                    }
                }
                // Bağlantı bilgilerini sakla
                _connectedService = "Google Drive";
                // UI güncelle
                lblCloudStatus.Text = $"Durum: Google Drive'a bağlı";
                lblCloudStatus.ForeColor = Color.Green;
                btnCloudBackupNow.Enabled = true;
                btnCloudBackupNow.FillColor = Color.FromArgb(0, 123, 255);
                btnCloudBackupNow.ForeColor = Color.White;
                MessageBox.Show("Google Drive hesabınıza başarıyla bağlandınız.", "Bağlantı Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Hata mesajını detaylı olarak göster
                MessageBox.Show($"Google Drive bağlantısı sırasında hata oluştu: {ex.Message}\n\nDetaylar: {ex.StackTrace}",
                              "Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Hata ayıklama için konsola da yaz
                Debug.WriteLine($"Google Drive hatası: {ex.Message}\n{ex.StackTrace}");
            }
        }

        public async Task ConnectToDropbox(string folderName)
        {
            var key = DropboxAppKey;
            if (string.IsNullOrEmpty(key))
            {
                throw new Exception("Dropbox App Key tanımlanmamış. Lütfen DropboxAppKey özelliğini ayarlayın.");
            }
            // OAuth 2.0 akışı başlat
            var oauth2State = Guid.NewGuid().ToString("N");
            // Dropbox için OAuth 2.0 URL'sini oluştur
            var authorizeUri = DropboxOAuth2Helper.GetAuthorizeUri(
                OAuthResponseType.Token,
                key,
                redirectUri: "http://localhost",
                state: oauth2State);
            // URL'yi varsayılan tarayıcıda aç
            Process.Start(new ProcessStartInfo
            {
                FileName = authorizeUri.ToString(),
                UseShellExecute = true
            });
            // Kullanıcıdan token'ı kopyalayıp yapıştırmasını iste
            var tokenForm = new Form
            {
                Width = 500,
                Height = 200,
                Text = "Dropbox Yetkilendirme",
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };
            var label = new Label
            {
                Text = "Tarayıcıda oluşturulan erişim token'ını buraya yapıştırın:",
                AutoSize = true,
                Location = new Point(20, 20)
            };
            var textBox = new TextBox
            {
                Width = 450,
                Location = new Point(20, 50)
            };
            var button = new Button
            {
                Text = "Tamam",
                Location = new Point(200, 100),
                DialogResult = DialogResult.OK
            };
            tokenForm.Controls.Add(label);
            tokenForm.Controls.Add(textBox);
            tokenForm.Controls.Add(button);
            tokenForm.AcceptButton = button;
            if (tokenForm.ShowDialog() != DialogResult.OK || string.IsNullOrEmpty(textBox.Text))
            {
                throw new Exception("Token alınamadı.");
            }
            var accessToken = textBox.Text.Trim();
            // Dropbox istemcisini oluştur
            _dropboxClient = new DropboxClient(accessToken);
            // Kullanıcı bilgilerini al
            var account = await _dropboxClient.Users.GetCurrentAccountAsync();
            // Hedef klasörün varlığını kontrol et
            if (!string.IsNullOrEmpty(folderName))
            {
                try
                {
                    var folderPath = "/" + folderName;
                    // Klasörün var olup olmadığını kontrol et
                    try
                    {
                        var metadata = await _dropboxClient.Files.GetMetadataAsync(folderPath);
                    }
                    catch (Exception)
                    {
                        // Klasör yoksa oluştur
                        var folder = await _dropboxClient.Files.CreateFolderV2Async(folderPath);
                    }
                    _targetFolder = folderPath;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Klasör oluşturulurken hata: {ex.Message}", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _targetFolder = "";
                }
            }
            // Bağlantı bilgilerini sakla
            _connectedService = "Dropbox";
            // UI güncelle
            lblCloudStatus.Text = $"Durum: {account.Name.DisplayName} olarak Dropbox'a bağlı";
            lblCloudStatus.ForeColor = Color.Green;
            btnCloudBackupNow.Enabled = true;
            btnCloudBackupNow.FillColor = Color.FromArgb(0, 123, 255);
            btnCloudBackupNow.ForeColor = Color.White;
            MessageBox.Show("Dropbox hesabınıza başarıyla bağlandınız.", "Bağlantı Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public async Task ConnectToOneDrive(string folderName)
        {
            if (string.IsNullOrEmpty(MSALClientId))
            {
                throw new Exception("Microsoft App ID tanımlanmamış. Lütfen MSALClientId özelliğini ayarlayın.");
            }
            string[] scopes = { "Files.ReadWrite.All" };
            var pca = PublicClientApplicationBuilder
                .Create(MSALClientId)
                .WithRedirectUri("http://localhost")
                .Build();
            AuthenticationResult authResult;
            try
            {
                // MSAL ile yetkilendirme akışını başlat
                var accounts = await pca.GetAccountsAsync();
                authResult = await pca.AcquireTokenSilent(scopes, accounts.FirstOrDefault())
                    .ExecuteAsync();
            }
            catch (MsalUiRequiredException)
            {
                try
                {
                    // Sessiz yetkilendirme başarısız olursa, kullanıcı etkileşimli yetkilendirme kullan
                    authResult = await pca.AcquireTokenInteractive(scopes)
                        .ExecuteAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Microsoft hesabı yetkilendirme hatası: {ex.Message}");
                }
            }
            _msalAccessToken = authResult.AccessToken;
            // YENİ: Bu iki satır değişti
            var authProvider = new BaseBearerTokenAuthenticationProvider(
                new AccessTokenProvider(_msalAccessToken));
            _graphClient = new GraphServiceClient(authProvider);
            // Kalan kod aynı
            _connectedService = "OneDrive";
            lblCloudStatus.Text = $"Durum: OneDrive'a bağlı";
            lblCloudStatus.ForeColor = Color.Green;
            btnCloudBackupNow.Enabled = true;
            btnCloudBackupNow.FillColor = Color.FromArgb(0, 123, 255);
            btnCloudBackupNow.ForeColor = Color.White;
            MessageBox.Show("OneDrive hesabınıza başarıyla bağlandınız.", "Bağlantı Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // FTP'ye bağlanma düğmesi tıklandığında
        public void btnFTPConnect_Click(object sender, EventArgs e)
        {
            string server = txtFTPServer.Text.Trim();
            string username = txtFTPUsername.Text.Trim();
            string password = txtFTPPassword.Text;
            if (string.IsNullOrEmpty(server) || string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Lütfen FTP sunucu adresi ve kullanıcı adını girin.",
                    "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                this.Cursor = Cursors.WaitCursor;
                lblCloudStatus.Text = "FTP'ye bağlanıyor...";
                lblCloudStatus.ForeColor = Color.Blue;
                // Sunucu adresinden 'ftp://' ön ekini kaldır (varsa)
                if (server.StartsWith("ftp://", StringComparison.OrdinalIgnoreCase))
                {
                    server = server.Substring(6);
                }
                // FTP bağlantısını test et
                using (var client = new FtpClient(server, username, password))
                {
                    client.Connect();
                    if (client.IsConnected)
                    {
                        // Bağlantı bilgilerini kaydet
                        _ftpServer = server;
                        _ftpUsername = username;
                        _ftpPassword = password;
                        _connectedService = "FTP";
                        // UI güncelle
                        lblCloudStatus.Text = $"Durum: {server} sunucusuna bağlı";
                        lblCloudStatus.ForeColor = Color.Green;
                        btnCloudBackupNow.Enabled = true;
                        btnCloudBackupNow.FillColor = Color.FromArgb(0, 123, 255);
                        btnCloudBackupNow.ForeColor = Color.White;
                        MessageBox.Show($"{server} FTP sunucusuna başarıyla bağlandınız.",
                            "Bağlantı Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        throw new Exception("FTP sunucusuna bağlanılamadı.");
                    }
                }
            }
            catch (Exception ex)
            {
                lblCloudStatus.Text = "FTP Bağlantı hatası: " + ex.Message;
                lblCloudStatus.ForeColor = Color.Red;
                MessageBox.Show("FTP sunucusuna bağlanırken bir hata oluştu: " + ex.Message,
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        public async void btnCloudBackupNow_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_connectedService))
            {
                MessageBox.Show("Lütfen önce bir servise bağlanın.",
                    "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                lblCloudStatus.Text = "Yedekleniyor...";
                lblCloudStatus.ForeColor = Color.Blue;

                // Geçici bir yedek dosyası oluştur
                string backupFileName = $"{firmaAdi}_{DateTime.Now:yyyyMMdd_HHmmss}.bak";
                string tempBackupPath = Path.Combine(Path.GetTempPath(), backupFileName);

                // Veritabanını geçici dosyaya kopyala
                File.Copy("veresiye.db", tempBackupPath, true);

                bool success = false;

                try
                {
                    // Servis türüne göre uygun yükleme metodunu çağır
                    switch (_connectedService)
                    {
                        case "Google Drive":
                            success = await UploadToGoogleDrive(tempBackupPath, backupFileName);
                            break;
                        case "Dropbox":
                            success = await UploadToDropbox(tempBackupPath, backupFileName);
                            break;
                        case "OneDrive":
                            success = await UploadToOneDrive(tempBackupPath, backupFileName);
                            break;
                        case "FTP":
                            success = await UploadToFTP(tempBackupPath, backupFileName);
                            break;
                        default:
                            MessageBox.Show($"{_connectedService} servisi için yükleme henüz uygulanmadı.",
                                "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                    }
                }
                catch (Exception uploadEx)
                {
                    Debug.WriteLine($"Yükleme hatası: {uploadEx.Message}");
                    success = false;
                    throw; // Ana catch bloğuna gönder
                }
                finally
                {
                    // Geçici dosyayı temizle
                    if (File.Exists(tempBackupPath))
                    {
                        try { File.Delete(tempBackupPath); } catch { }
                    }
                }

                if (success)
                {
                    lblCloudStatus.Text = $"Durum: Yedekleme başarılı - {_connectedService}";
                    lblCloudStatus.ForeColor = Color.Green;
                    MessageBox.Show($"Veritabanı başarıyla {_connectedService} servisine yedeklendi.",
                        "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                lblCloudStatus.Text = "Yedekleme hatası: " + ex.Message;
                lblCloudStatus.ForeColor = Color.Red;
                MessageBox.Show($"Yedekleme sırasında bir hata oluştu: {ex.Message}\n\n" +
                    (ex.InnerException != null ? $"Detay: {ex.InnerException.Message}" : ""),
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        public async Task<bool> UploadToFTP(string filePath, string fileName)
        {
            try
            {
                // FTP istemcisini oluştur
                using (var client = new FtpClient(_ftpServer, _ftpUsername, _ftpPassword))
                {
                    await Task.Run(() => {
                        client.Connect();
                        if (!client.IsConnected)
                        {
                            throw new Exception("FTP sunucusuna bağlanılamadı.");
                        }
                        // Hedef klasör varsa ve mevcut değilse oluştur
                        if (!string.IsNullOrEmpty(_targetFolder))
                        {
                            try
                            {
                                if (!client.DirectoryExists(_targetFolder))
                                {
                                    client.CreateDirectory(_targetFolder);
                                }
                                // Klasöre geç
                                client.SetWorkingDirectory(_targetFolder);
                            }
                            catch
                            {
                                // Klasör oluşturma başarısız olursa, kök dizine yükle
                                _targetFolder = "";
                            }
                        }
                        // Yükleme yolunu belirle
                        string uploadPath = string.IsNullOrEmpty(_targetFolder) ?
                            fileName : $"{_targetFolder}/{fileName}";
                        // Dosyayı yükle
                        client.UploadFile(filePath, uploadPath, FtpRemoteExists.Overwrite);
                    });
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"FTP'ye yükleme hatası: {ex.Message}");
            }
        }

        public async Task<bool> UploadToGoogleDrive(string filePath, string fileName)
        {
            try
            {
                if (_driveService == null)
                {
                    throw new Exception("Google Drive bağlantısı bulunamadı.");
                }

                // Dosya metadata'sını oluştur
                var fileMetadata = new Google.Apis.Drive.v3.Data.File()
                {
                    Name = fileName
                };

                // Eğer hedef klasör tanımlanmışsa, dosyayı o klasöre yükle
                if (!string.IsNullOrEmpty(_targetFolder))
                {
                    fileMetadata.Parents = new List<string> { _targetFolder };
                }

                using (var stream = new FileStream(filePath, FileMode.Open))
                {
                    var request = _driveService.Files.Create(fileMetadata, stream, "application/octet-stream");
                    request.Fields = "id, name, size";

                    request.Upload();

                    if (request.ResponseBody != null)
                    {
                        var file = request.ResponseBody;
                        Debug.WriteLine($"Uploaded file: {file.Name}, ID: {file.Id}");

                        // Dosya yüklendikten sonra izinleri özel olarak ayarla
                        await SetFilePermissionsTopublic(file.Id);

                        return true;
                    }
                    else
                    {
                        throw new Exception("Dosya yüklenemedi: Yanıt alınamadı");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Google Drive'a yükleme hatası: {ex.Message}", ex);
            }
        }

        // Dosya izinlerini özel yapan metot
        public async Task SetFilePermissionsTopublic(string fileId)
        {
            try
            {
                // Önce dosyanın mevcut izinlerini alın
                var permissionList = await _driveService.Permissions.List(fileId).ExecuteAsync();

                // Dosyanın herhangi bir paylaşım izni varsa kaldırın
                // (domain veya anyone ile paylaşılan izinleri kaldır)
                if (permissionList.Permissions != null)
                {
                    foreach (var permission in permissionList.Permissions)
                    {
                        // Sadece owner olmayan izinleri kaldır (sahibi her zaman erişebilmeli)
                        if (permission.Role != "owner")
                        {
                            await _driveService.Permissions.Delete(fileId, permission.Id).ExecuteAsync();
                        }
                    }
                }

                Debug.WriteLine($"File ID: {fileId} set to public successfully");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error setting file permissions: {ex.Message}");
                // İzinleri ayarlamada bir hata olursa devam et, yükleme başarılı oldu
            }
        }

        public async Task<bool> UploadToDropbox(string filePath, string fileName)
        {
            try
            {
                if (_dropboxClient == null)
                {
                    throw new Exception("Dropbox bağlantısı bulunamadı.");
                }
                var dropboxPath = string.IsNullOrEmpty(_targetFolder) ?
                    $"/{fileName}" : $"{_targetFolder}/{fileName}";
                using (var stream = new FileStream(filePath, FileMode.Open))
                {
                    var response = await _dropboxClient.Files.UploadAsync(
                        dropboxPath,
                        WriteMode.Overwrite.Instance,
                        body: stream);
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Dropbox'a yükleme hatası: {ex.Message}");
            }
        }

        // OneDrive yükleme
        public async Task<bool> UploadToOneDrive(string filePath, string fileName)
        {
            try
            {
                if (_graphClient == null)
                {
                    throw new Exception("OneDrive bağlantısı bulunamadı.");
                }
                using (var stream = new FileStream(filePath, FileMode.Open))
                {
                    // HttpClient kullanarak Manuel yükleme
                    var httpClient = new HttpClient();
                    // Token ile yetkilendirme
                    httpClient.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _msalAccessToken);
                    // Dosya içeriğini bir MemoryStream'e kopyala
                    using (var memoryStream = new MemoryStream())
                    {
                        await stream.CopyToAsync(memoryStream);
                        memoryStream.Position = 0;
                        // Hedef URL'yi belirle
                        string uploadUrl;
                        if (string.IsNullOrEmpty(_targetFolder))
                        {
                            // Kök dizine yükle
                            uploadUrl = $"https://graph.microsoft.com/v1.0/me/drive/root:/{fileName}:/content";
                        }
                        else
                        {
                            // Belirli klasöre yükle
                            uploadUrl = $"https://graph.microsoft.com/v1.0/me/drive/items/{_targetFolder}:/{fileName}:/content";
                        }
                        // İstek oluştur
                        using (var content = new StreamContent(memoryStream))
                        {
                            content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                            // PUT isteği gönder
                            var response = await httpClient.PutAsync(uploadUrl, content);
                            // Yanıtı kontrol et
                            if (response.IsSuccessStatusCode)
                            {
                                return true;
                            }
                            else
                            {
                                string errorResponse = await response.Content.ReadAsStringAsync();
                                throw new Exception($"Yükleme başarısız: {response.StatusCode} - {errorResponse}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"OneDrive'a yükleme hatası: {ex.Message}");
            }
        }

        // AccessTokenProvider sınıfı
        public class AccessTokenProvider : IAccessTokenProvider
        {
            public string _token;
            public AccessTokenProvider(string token)
            {
                _token = token;
            }

            public Task<string> GetAuthorizationTokenAsync(Uri uri, Dictionary<string, object> additionalAuthenticationContext = null, CancellationToken cancellationToken = default)
            {
                return Task.FromResult(_token);
            }

            public AllowedHostsValidator AllowedHostsValidator => new AllowedHostsValidator(new[] { "graph.microsoft.com" });
        }

        public void btninfo2_Click(object sender, EventArgs e)
        {
            // Bilgi dialog'unu oluştur
            Guna2MessageDialog messageDialog = new Guna2MessageDialog
            {
                Text = "Google Drive Yedekleme:\n" +
                    "• İlk bağlantıda Google hesabınıza giriş yapmanız istenecektir\n" +
                    "• Her cihaz için ilk kullanımda yetkilendirme gereklidir\n" +
                    "• Yüklenen dosyalar Drive'da görüntülenebilir\n\n" +
                    "FTP Yedekleme:\n" +
                    "• FTP sunucu bilgilerinizi sistem yöneticinizden alabilirsiniz\n" +
                    "• Sunucu adresini tam adı ile girin (örn: ftp://orneksite.com)\n" +
                    "• Port farklıysa belirtmeyi unutmayın",
                Caption = "Bulut Yedekleme Bilgisi",
                Style = MessageDialogStyle.Light,
                Icon = MessageDialogIcon.Information,
                Buttons = MessageDialogButtons.OK
            };

            // Dialog'u göster
            messageDialog.Show();
        }

        public void UpdateYedeklemeDurumuPanel()
        {
            try
            {
                // Son yedek bilgisini güncelle
                string lastBackupDate = Properties.Settings.Default.LastBackupDate;
                if (!string.IsNullOrEmpty(lastBackupDate))
                {
                    DateTime lastBackup = DateTime.Parse(lastBackupDate);
                    TimeSpan timeSince = DateTime.Now - lastBackup;
                    if (timeSince.TotalDays < 1)
                    {
                        lblSonYedekDeger.Text = $"Bugün {lastBackup.ToString("HH:mm")}";
                    }
                    else if (timeSince.TotalDays < 2)
                    {
                        lblSonYedekDeger.Text = "Dün";
                    }
                    else
                    {
                        lblSonYedekDeger.Text = $"{(int)timeSince.TotalDays} gün önce";
                    }
                }
                else
                {
                    lblSonYedekDeger.Text = "Henüz alınmadı";
                }

                // Toplam yedek sayısı ve boyutunu hesapla
                string backupPath = Properties.Settings.Default.LastBackupPath;
                if (!string.IsNullOrEmpty(backupPath) && Directory.Exists(backupPath))
                {
                    // .bak uzantılı dosyaları say
                    string[] backupFiles = Directory.GetFiles(backupPath, "*.bak");
                    int backupCount = backupFiles.Length;

                    // Toplam boyutu hesapla
                    long totalSize = 0;
                    foreach (string file in backupFiles)
                    {
                        FileInfo fileInfo = new FileInfo(file);
                        totalSize += fileInfo.Length;
                    }

                    // MB veya GB olarak formatla
                    string sizeStr;
                    if (totalSize > 1024 * 1024 * 1024) // > 1GB
                    {
                        sizeStr = $"{totalSize / (1024.0 * 1024 * 1024):0.#} GB";
                    }
                    else
                    {
                        sizeStr = $"{totalSize / (1024.0 * 1024):0.#} MB";
                    }

                    lblToplamYedekDeger.Text = $"{backupCount} yedek ({sizeStr})";
                }
                else
                {
                    lblToplamYedekDeger.Text = "0 yedek (0 MB)";
                }

                // Sonraki otomatik yedekleme bilgisini güncelle
                bool autoBackupEnabled = Properties.Settings.Default.AutoBackupEnabled;
                if (autoBackupEnabled)
                {
                    string period = Properties.Settings.Default.AutoBackupPeriod;
                    DateTime lastAutoBackup = DateTime.Now;
                    if (!string.IsNullOrEmpty(Properties.Settings.Default.LastAutoBackupTime))
                    {
                        try
                        {
                            lastAutoBackup = DateTime.Parse(Properties.Settings.Default.LastAutoBackupTime);
                        }
                        catch { }
                    }

                    // Sonraki yedekleme zamanını hesapla
                    DateTime nextBackup = DateTime.Now;
                    switch (period)
                    {
                        case "Daily":
                            nextBackup = lastAutoBackup.AddDays(1);
                            break;
                        case "Weekly":
                            nextBackup = lastAutoBackup.AddDays(7);
                            break;
                        case "Monthly":
                            nextBackup = lastAutoBackup.AddMonths(1);
                            break;
                        case "Custom":
                            int interval = Properties.Settings.Default.AutoBackupInterval;
                            nextBackup = lastAutoBackup.AddMinutes(interval);
                            break;
                    }

                    // Sonraki zamanı göster
                    if (nextBackup < DateTime.Now)
                    {
                        lblSonrakiOtoDeger.Text = "Yedekleme bekliyor";
                    }
                    else if ((nextBackup - DateTime.Now).TotalDays < 1)
                    {
                        lblSonrakiOtoDeger.Text = $"Bugün {nextBackup.ToString("HH:mm")}";
                    }
                    else if ((nextBackup - DateTime.Now).TotalDays < 2)
                    {
                        lblSonrakiOtoDeger.Text = $"Yarın {nextBackup.ToString("HH:mm")}";
                    }
                    else
                    {
                        lblSonrakiOtoDeger.Text = nextBackup.ToString("dd.MM.yyyy");
                    }
                }
                else
                {
                    lblSonrakiOtoDeger.Text = "Otomatik kapalı";
                }

                // Tavsiye güncelleme
                string advice = "Düzenli yedek alın";
                if (!string.IsNullOrEmpty(lastBackupDate))
                {
                    DateTime lastBackup = DateTime.Parse(lastBackupDate);
                    TimeSpan timeSince = DateTime.Now - lastBackup;
                    if (timeSince.TotalDays > 7)
                    {
                        advice = "Acilen yedek alın!";
                        lblTavsiyeDeger.ForeColor = System.Drawing.Color.Red;
                    }
                    else if (timeSince.TotalDays > 3)
                    {
                        advice = "Yeni bir yedek alın";
                        lblTavsiyeDeger.ForeColor = System.Drawing.Color.FromArgb(220, 53, 69);
                    }
                    else
                    {
                        advice = "Yedekleriniz güncel";
                        lblTavsiyeDeger.ForeColor = System.Drawing.Color.Green;
                    }
                }
                lblTavsiyeDeger.Text = advice;
            }
            catch (Exception ex)
            {
                // Hata olursa varsayılan değerleri göster
                System.Diagnostics.Debug.WriteLine($"Yedekleme durumu güncellenirken hata: {ex.Message}");
            }
        }

        public void gerinoktasi_Click(object sender, EventArgs e)
        {
            try
            {
                // Açık olan tüm formları bul ve gizle (BackupRestoreForm dahil)
                List<Form> openForms = new List<Form>();
                foreach (Form form in Application.OpenForms)
                {
                    if (form.Visible) // Tüm görünür formları gizle (BackupRestoreForm dahil)
                    {
                        openForms.Add(form);
                        form.Hide(); // Formu gizle
                    }
                }

                // Veritabanı yolunu belirle
                string dbPath = "veresiye.db"; // Veritabanı yolunuzu buraya yazın

                // Yedek dizinini belirle
                string backupDir = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "Veresiye2025", "RestorePoints");

                // Yedek dizinini oluştur (yoksa)
                if (!Directory.Exists(backupDir))
                    Directory.CreateDirectory(backupDir);

                // Geri yükleme noktası yönetim formu
                RestorePointManagerForm restorePointForm = new RestorePointManagerForm(dbPath, backupDir);

                // Dialog kapandığında diğer formları tekrar göster
                restorePointForm.FormClosed += (s, args) => {
                    foreach (Form form in openForms)
                    {
                        if (!form.IsDisposed) // Form kapatılmadıysa tekrar göster
                        {
                            form.Show();
                        }
                    }
                };

                restorePointForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"İşlem sırasında bir hata oluştu: {ex.Message}",
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Form sınıfı
        public class RestorePointManagerForm : Form
        {
            public readonly string _dbPath;
            public readonly string _backupDir;
            public List<KeyValuePair<string, DateTime>> restorePointsList = new List<KeyValuePair<string, DateTime>>();

            public RestorePointManagerForm(string dbPath, string backupDir)
            {
                _dbPath = dbPath;
                _backupDir = backupDir;
                InitializeForm();
                LoadRestorePoints();
            }

            public void InitializeForm()
            {
                this.Width = 750;
                this.Height = 500;
                this.Text = "Geri Yükleme Noktası Yönetimi";
                this.StartPosition = FormStartPosition.CenterParent;
                this.FormBorderStyle = FormBorderStyle.None;
                this.BackColor = Color.FromArgb(240, 240, 240);

                // Form başlık çubuğu
                Guna.UI2.WinForms.Guna2Panel panelTitleBar = new Guna.UI2.WinForms.Guna2Panel()
                {
                    Dock = DockStyle.Top,
                    Height = 40,
                    FillColor = Color.FromArgb(0, 123, 255)
                };

                Label lblTitle = new Label()
                {
                    Text = "Veritabanı Geri Yükleme Noktaları",
                    Font = new Font("Segoe UI", 11, FontStyle.Bold),
                    ForeColor = Color.White,
                    BackColor = Color.Transparent,
                    AutoSize = true,
                    Location = new Point(12, 10)
                };
                                

                Guna.UI2.WinForms.Guna2Button btnClose = new Guna.UI2.WinForms.Guna2Button()
                {
                    Text = "X",
                    Font = new Font("Segoe UI", 11, FontStyle.Bold),
                    ForeColor = Color.White,
                    Size = new Size(30, 40),
                    FillColor = Color.Red,
                    Animated = true,
                    Location = new Point(this.Width - 30, 0)
                };

                btnClose.Click += (s, ev) => { this.Close(); };

                panelTitleBar.Controls.Add(lblTitle);
                panelTitleBar.Controls.Add(btnClose);

                // Ana Panel (2 sütunlu düzen)
                Guna.UI2.WinForms.Guna2Panel mainPanel = new Guna.UI2.WinForms.Guna2Panel()
                {
                    Dock = DockStyle.Fill
                };

                // Sol Panel - Geri yükleme noktası oluşturma
                Guna.UI2.WinForms.Guna2Panel leftPanel = new Guna.UI2.WinForms.Guna2Panel()
                {
                    Width = 350,
                    Height = this.Height - 40,
                    Location = new Point(0, 0),
                    Dock = DockStyle.Left,
                    Padding = new Padding(20)
                };

                Label lblCreateTitle = new Label()
                {
                    Text = "Yeni Geri Yükleme Noktası Oluştur",
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    ForeColor = Color.FromArgb(70, 70, 70),
                    AutoSize = true,
                    Location = new Point(0, 20)
                };

                Label lblDescription = new Label()
                {
                    Text = "Açıklama:",
                    Font = new Font("Segoe UI", 10),
                    AutoSize = true,
                    Location = new Point(0, 70)
                };

                Guna.UI2.WinForms.Guna2TextBox txtDescription = new Guna.UI2.WinForms.Guna2TextBox()
                {
                    PlaceholderText = "Geri yükleme noktası için açıklama girin",
                    Text = $"Manuel yedek - {DateTime.Now.ToString("dd.MM.yyyy HH:mm")}",
                    Location = new Point(0, 100),
                    Width = 310,
                    Height = 36,
                    BorderRadius = 5,
                    Font = new Font("Segoe UI", 9)
                };

                PictureBox pictureCreatePoint = new PictureBox()
                {
                    Image = Veresiye2025.Properties.Resources.save_icon, // Uygun bir simge kullanın
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Size = new Size(50, 50),
                    Location = new Point(205, 145)
                };

                Guna.UI2.WinForms.Guna2Button btnCreatePoint = new Guna.UI2.WinForms.Guna2Button()
                {
                    Text = "Geri Yükleme Noktası Oluştur",
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    ForeColor = Color.White,
                    FillColor = Color.FromArgb(0, 123, 255),
                    BorderRadius = 5,
                    Size = new Size(310, 40),
                    Location = new Point(0, 260),
                    Image = Veresiye2025.Properties.Resources.save_icon, // Uygun bir simge kullanın
                    ImageAlign = HorizontalAlignment.Left,
                    ImageOffset = new Point(5, 0),
                    TextOffset = new Point(5, 0)
                };

                // Bilgi paneli
                Guna.UI2.WinForms.Guna2Panel infoPanel = new Guna.UI2.WinForms.Guna2Panel()
                {
                    BorderColor = Color.FromArgb(220, 220, 220),
                    BorderRadius = 10,
                    BorderThickness = 1,
                    FillColor = Color.FromArgb(248, 249, 250),
                    Location = new Point(0, 310),
                    Size = new Size(310, 120),
                    ShadowDecoration = { Enabled = true, Depth = 1, Color = Color.FromArgb(30, 0, 0, 0) }
                };

                Guna.UI2.WinForms.Guna2HtmlLabel lblInfoText = new Guna.UI2.WinForms.Guna2HtmlLabel()
                {
                    Text = "Geri yükleme noktaları, veritabanınızın anlık görüntüleridir. " +
                          "İleriki bir tarihte, seçtiğiniz bir noktaya geri dönebilirsiniz. " +
                          "Önemli işlemlerden önce geri yükleme noktası oluşturmak iyi bir uygulamadır.",
                    Location = new Point(15, 15),
                    AutoSize = false,
                    Size = new Size(280, 100),
                    Font = new Font("Segoe UI", 9),
                    ForeColor = Color.FromArgb(70, 70, 70)
                };

                infoPanel.Controls.Add(lblInfoText);

                // Sağ Panel - Geri yükleme noktaları listesi
                Guna.UI2.WinForms.Guna2Panel rightPanel = new Guna.UI2.WinForms.Guna2Panel()
                {
                    Dock = DockStyle.Fill,
                    Padding = new Padding(20)
                };

                Label lblRestoreTitle = new Label()
                {
                    Text = "Geri Yükleme Noktaları",
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    ForeColor = Color.FromArgb(70, 70, 70),
                    AutoSize = true,
                    Location = new Point(0, 20)
                };

                // Geri yükleme noktaları DataGridView
                Guna.UI2.WinForms.Guna2DataGridView dgvRestorePoints = new Guna.UI2.WinForms.Guna2DataGridView()
                {
                    Location = new Point(0, 70),
                    Size = new Size(365, 300),
                    // BorderRadius özelliği Guna2DataGridView'de bulunmayabilir
                    ThemeStyle = { HeaderStyle = { Font = new Font("Segoe UI Semibold", 10) } },
                    RowHeadersVisible = false,
                    ReadOnly = true,
                    AllowUserToAddRows = false,
                    AllowUserToDeleteRows = false,
                    AllowUserToResizeRows = false,
                    SelectionMode = DataGridViewSelectionMode.FullRowSelect
                };

                dgvRestorePoints.Columns.Add("Date", "Tarih");
                dgvRestorePoints.Columns.Add("Description", "Açıklama");
                dgvRestorePoints.Columns.Add("Size", "Boyut");

                dgvRestorePoints.Columns[0].Width = 130;
                dgvRestorePoints.Columns[1].Width = 170;
                dgvRestorePoints.Columns[2].Width = 65;

                Guna.UI2.WinForms.Guna2Button btnRestoreToPoint = new Guna.UI2.WinForms.Guna2Button()
                {
                    Text = "Seçilen Noktaya Geri Dön",
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    ForeColor = Color.White,
                    FillColor = Color.FromArgb(0, 123, 255),
                    BorderRadius = 5,
                    Size = new Size(250, 40),
                    Location = new Point(115, 380),
                    Image = Veresiye2025.Properties.Resources._return, // Uygun bir simge kullanın
                    ImageAlign = HorizontalAlignment.Left,
                    ImageOffset = new Point(5, 0),
                    TextOffset = new Point(5, 0)
                };

                // Buton olayları
                btnCreatePoint.Click += (s, ev) => CreateRestorePoint(txtDescription.Text, dgvRestorePoints);
                btnRestoreToPoint.Click += (s, ev) => RestoreToPoint(dgvRestorePoints);

                // Kontrolleri panellere ekle
                leftPanel.Controls.Add(lblCreateTitle);
                leftPanel.Controls.Add(lblDescription);
                leftPanel.Controls.Add(txtDescription);
                leftPanel.Controls.Add(pictureCreatePoint);
                leftPanel.Controls.Add(btnCreatePoint);
                leftPanel.Controls.Add(infoPanel);

                rightPanel.Controls.Add(lblRestoreTitle);
                rightPanel.Controls.Add(dgvRestorePoints);
                rightPanel.Controls.Add(btnRestoreToPoint);

                // Panelleri ana forma ekle
                mainPanel.Controls.Add(rightPanel);
                mainPanel.Controls.Add(leftPanel);

                this.Controls.Add(mainPanel);
                this.Controls.Add(panelTitleBar);

                // Form hareket ettirme özelliği - doğru şekilde uygula
                panelTitleBar.MouseDown += (s, ev) => {
                    if (ev.Button == MouseButtons.Left)
                    {
                        ReleaseCapture();
                        SendMessage(this.Handle, 0xA1, 0x2, 0);
                    }
                };
            }

            // P/Invoke için metotlar
            [System.Runtime.InteropServices.DllImport("user32.dll")]
            public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

            [System.Runtime.InteropServices.DllImport("user32.dll")]
            public static extern bool ReleaseCapture();

            public void LoadRestorePoints()
            {
                // Form içindeki DataGridView'i bul
                Guna.UI2.WinForms.Guna2DataGridView dgvRestorePoints = null;
                foreach (Control control in this.Controls)
                {
                    if (control is Guna.UI2.WinForms.Guna2Panel mainPanel)
                    {
                        foreach (Control childControl in mainPanel.Controls)
                        {
                            if (childControl is Guna.UI2.WinForms.Guna2Panel rightPanel && childControl.Dock == DockStyle.Fill)
                            {
                                foreach (Control rightControl in rightPanel.Controls)
                                {
                                    if (rightControl is Guna.UI2.WinForms.Guna2DataGridView grid)
                                    {
                                        dgvRestorePoints = grid;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                if (dgvRestorePoints == null) return;

                // Geri yükleme noktalarını yükle
                string[] backupFiles = Directory.GetFiles(_backupDir, "RestorePoint_*.bak");

                // Restore noktalarını tutan liste
                restorePointsList.Clear();
                dgvRestorePoints.Rows.Clear();

                // Gridview'i doldur
                foreach (string file in backupFiles)
                {
                    string fileName = Path.GetFileNameWithoutExtension(file);
                    string descriptionPath = Path.Combine(_backupDir, fileName + ".txt");
                    string description = "Açıklama yok";

                    if (File.Exists(descriptionPath))
                        description = File.ReadAllText(descriptionPath);

                    // Zaman damgasını çıkar
                    string timestamp = fileName.Replace("RestorePoint_", "");
                    DateTime pointDate = DateTime.ParseExact(timestamp, "yyyy-MM-dd_HH-mm-ss",
                        System.Globalization.CultureInfo.InvariantCulture);

                    long fileSize = new FileInfo(file).Length;
                    string sizeStr = FormatFileSize(fileSize);

                    // DataGridView'e ekle
                    dgvRestorePoints.Rows.Add(pointDate.ToString("dd.MM.yyyy HH:mm"), description, sizeStr);

                    // Listeye ekle
                    restorePointsList.Add(new KeyValuePair<string, DateTime>(file, pointDate));
                }

                // Restore noktalarını son tarihten ilk tarihe doğru sırala
                restorePointsList = restorePointsList.OrderByDescending(x => x.Value).ToList();

                // Tarihe göre sırala
                if (dgvRestorePoints.Rows.Count > 0)
                    dgvRestorePoints.Sort(dgvRestorePoints.Columns[0], System.ComponentModel.ListSortDirection.Descending);
            }

            public void CreateRestorePoint(string description, Guna.UI2.WinForms.Guna2DataGridView dgvRestorePoints)
            {
                try
                {
                    string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
                    string backupFileName = $"RestorePoint_{timestamp}.bak";
                    string backupPath = Path.Combine(_backupDir, backupFileName);

                    // Veritabanını kopyala
                    File.Copy(_dbPath, backupPath, true);

                    // Açıklama dosyasını oluştur
                    string descriptionPath = Path.Combine(_backupDir, $"RestorePoint_{timestamp}.txt");
                    File.WriteAllText(descriptionPath, description);

                    // DataGridView'i güncelle
                    long fileSize = new FileInfo(backupPath).Length;
                    string sizeStr = FormatFileSize(fileSize);

                    dgvRestorePoints.Rows.Insert(0, DateTime.Now.ToString("dd.MM.yyyy HH:mm"), description, sizeStr);

                    // restorePointsList'i güncelle
                    restorePointsList.Insert(0, new KeyValuePair<string, DateTime>(backupPath, DateTime.Now));

                    MessageBox.Show("Geri yükleme noktası başarıyla oluşturuldu.",
                        "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Geri yükleme noktası oluşturulurken bir hata oluştu: {ex.Message}",
                        "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            public void RestoreToPoint(Guna.UI2.WinForms.Guna2DataGridView dgvRestorePoints)
            {
                if (dgvRestorePoints.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Lütfen bir geri yükleme noktası seçin.",
                        "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int selectedIndex = dgvRestorePoints.SelectedRows[0].Index;
                if (selectedIndex >= 0 && selectedIndex < restorePointsList.Count)
                {
                    string selectedBackupFile = restorePointsList[selectedIndex].Key;
                    DateTime pointDate = restorePointsList[selectedIndex].Value;

                    // Onay iste
                    DialogResult confirmResult = MessageBox.Show(
                        $"Seçilen geri yükleme noktasına dönmek istediğinizden emin misiniz?\n\n" +
                        $"Tarih: {pointDate.ToString("dd.MM.yyyy HH:mm:ss")}\n" +
                        $"Açıklama: {dgvRestorePoints.SelectedRows[0].Cells[1].Value}\n\n" +
                        "Bu işlem, veritabanınızı seçilen noktaya döndürecek ve sonraki tüm değişiklikler kaybolacaktır.",
                        "Geri Yükleme Onayı",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (confirmResult == DialogResult.Yes)
                    {
                        try
                        {
                            // Önce mevcut veritabanının yedeğini al
                            string beforeRestoreBackup = Path.Combine(_backupDir,
                                $"BeforeRestore_{DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")}.bak");
                            File.Copy(_dbPath, beforeRestoreBackup, true);

                            // Geri yükleme işlemi
                            File.Copy(selectedBackupFile, _dbPath, true);

                            MessageBox.Show("Geri yükleme işlemi başarılı. Değişikliklerin geçerli olması için uygulama yeniden başlatılacak.",
                                "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Formu kapat
                            this.Close();

                            // Uygulamayı yeniden başlat
                            Application.Restart();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Geri yükleme işlemi sırasında bir hata oluştu: {ex.Message}",
                                "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }

            public string FormatFileSize(long bytes)
            {
                string[] sizes = { "B", "KB", "MB", "GB", "TB" };
                int order = 0;
                double size = bytes;

                while (size >= 1024 && order < sizes.Length - 1)
                {
                    order++;
                    size = size / 1024;
                }

                return $"{size:0.##} {sizes[order]}";
            }
        }
    }
}