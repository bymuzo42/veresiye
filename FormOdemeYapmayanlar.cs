using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using WinFont = System.Drawing.Font;

namespace Veresiye2025
{
    public partial class FormOdemeYapmayanlar : Form
    {
        #region Fields & Properties
        public List<BorcKaydi> borclular = new List<BorcKaydi>();
        public List<BorcKaydi> filtreliBorclular = new List<BorcKaydi>();
        public Timer refreshTimer;
        public Timer notificationTimer;
        public Timer animationTimer;
        public string currentFilter = "Tüm Borçlular";
        public string currentSort = "Borç Miktarı (Yüksek→Düşük)";
        public bool isDarkTheme = false;

        // Modern renkler - Material Design & Bootstrap esinlenmesi
        public readonly Color PrimaryBlue = Color.FromArgb(31, 81, 255);
        public readonly Color SecondaryBlue = Color.FromArgb(64, 123, 255);
        public readonly Color AccentOrange = Color.FromArgb(255, 133, 27);
        public readonly Color DarkBlue = Color.FromArgb(13, 27, 62);
        public readonly Color LightGray = Color.FromArgb(248, 250, 252);
        public readonly Color CardBackground = Color.FromArgb(255, 255, 255);
        public readonly Color DangerRed = Color.FromArgb(220, 53, 69);
        public readonly Color SuccessGreen = Color.FromArgb(40, 167, 69);
        public readonly Color WarningYellow = Color.FromArgb(255, 193, 7);
        public readonly Color InfoCyan = Color.FromArgb(23, 162, 184);
        public readonly Color ShadowColor = Color.FromArgb(30, 0, 0, 0);
        #endregion

        #region Constructor & Initialization
        public FormOdemeYapmayanlar()
        {
            InitializeComponent();
            InitializeForm();
        }

        public void InitializeForm()
        {
            // Form ayarları - 20px border radius
            this.Size = new Size(920, 580);
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = LightGray;
            this.Padding = new Padding(2);

            // ✅ Anti-aliasing - Pürüzsüz çizgiler
            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                         ControlStyles.UserPaint |
                         ControlStyles.DoubleBuffer |
                         ControlStyles.ResizeRedraw, true);

            // ✅ 20px Border Radius için custom paint
            this.Paint += Form_Paint;
            this.Region = CreateRoundedRegion(this.Size, 20);

            // Modern tasarım uygula
            ApplyModernDesign();

            // ComboBox'ları doldur
            InitializeComboBoxes();

            // DataGridView ayarları
            SetupDataGridView();

            // Timer'ları başlat
            InitializeTimers();

            // İlk veri yüklemesi
            LoadData();

            // Klavye kısayolları
            InitializeKeyboardShortcuts();

            // Animasyonlar
            InitializeAnimations();
        }

        // ✅ 20px Border Radius - Tüm köşeler yuvarlatıldı
        public Region CreateRoundedRegion(Size size, int radius)
        {
            var path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(size.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(size.Width - radius, size.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, size.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();
            return new Region(path);
        }

        // ✅ Custom Paint - Özel çizim işlemleri
        public void Form_Paint(object sender, PaintEventArgs e)
        {
            // Anti-aliasing aktif
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            // ✅ Card shadows - Outer shadow effect
            using (var shadowBrush = new SolidBrush(Color.FromArgb(20, 0, 0, 0)))
            {
                var shadowRect = new Rectangle(4, 4, this.Width - 8, this.Height - 8);
                var shadowPath = CreateRoundedRectangle(shadowRect, 20);
                e.Graphics.FillPath(shadowBrush, shadowPath);
            }

            // Ana form background
            using (var backgroundBrush = new SolidBrush(LightGray))
            {
                var mainRect = new Rectangle(0, 0, this.Width, this.Height);
                var mainPath = CreateRoundedRectangle(mainRect, 20);
                e.Graphics.FillPath(backgroundBrush, mainPath);
            }
        }

        public GraphicsPath CreateRoundedRectangle(Rectangle rect, int radius)
        {
            var path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
            path.CloseFigure();
            return path;
        }

        public void ApplyModernDesign()
        {
            // ✅ Header rounded - Sadece üst köşeler yuvarlatıldı
            panelHeader.Paint += PanelHeader_Paint;
            panelHeader.Height = 80;
            panelHeader.Region = CreateTopRoundedRegion(panelHeader.Size, 20);

            // ✅ Card shadows - İstatistik kartları için gölge efekti
            ApplyModernCardDesign(panelStat1, DangerRed, "");
            ApplyModernCardDesign(panelStat2, AccentOrange, "");
            ApplyModernCardDesign(panelStat3, WarningYellow, "");
            ApplyModernCardDesign(panelStat4, DarkBlue, "");

            // Toolbar modern tasarım
            ApplyToolbarDesign();

            // Ana panel tasarımı
            panelMain.BackColor = LightGray;
            panelMain.Padding = new Padding(20, 10, 20, 10);

            // Progress panel tasarımı
            ApplyProgressPanelDesign();

            // Bottom actions panel
            ApplyBottomActionsDesign();

            // ✅ Footer rounded - Sadece alt köşeler yuvarlatıldı
            ApplyFooterDesign();

            // ✅ Modern Button Design - Yuvarlatılmış butonlar
            ApplyModernButtonDesign();
        }

        // ✅ Header rounded - Sadece üst köşeler
        public Region CreateTopRoundedRegion(Size size, int radius)
        {
            var path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(size.Width - radius, 0, radius, radius, 270, 90);
            path.AddLine(size.Width, size.Height, 0, size.Height);
            path.CloseFigure();
            return new Region(path);
        }

        // ✅ Footer rounded - Sadece alt köşeler
        public Region CreateBottomRoundedRegion(Size size, int radius)
        {
            var path = new GraphicsPath();
            path.StartFigure();
            path.AddLine(0, 0, size.Width, 0);
            path.AddArc(size.Width - radius, size.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, size.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();
            return new Region(path);
        }

        public void PanelHeader_Paint(object sender, PaintEventArgs e)
        {
            // Anti-aliasing
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Gradient background
            using (var brush = new LinearGradientBrush(
                panelHeader.ClientRectangle,
                Color.MidnightBlue,
                Color.FromArgb(30, 50, 120),
                LinearGradientMode.Horizontal))
            {
                var headerPath = CreateRoundedRectangle(panelHeader.ClientRectangle, 20);
                e.Graphics.FillPath(brush, headerPath);
            }

            // Bottom border
            using (var pen = new Pen(Color.FromArgb(100, 255, 255, 255), 1))
            {
                e.Graphics.DrawLine(pen, 0, panelHeader.Height - 1, panelHeader.Width, panelHeader.Height - 1);
            }
        }

        // ✅ Card shadows - İstatistik kartları için gölge efekti
        public void ApplyModernCardDesign(Panel panel, Color accentColor, string icon)
        {
            panel.BackColor = CardBackground;
            panel.Padding = new Padding(15);

            // 20px border radius + shadow
            panel.Paint += (s, e) => {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                var rect = panel.ClientRectangle;

                // ✅ Card shadows
                using (var shadowBrush = new SolidBrush(Color.FromArgb(15, 0, 0, 0)))
                {
                    var shadowRect = new Rectangle(2, 2, rect.Width - 2, rect.Height - 2);
                    var shadowPath = CreateRoundedRectangle(shadowRect, 20);
                    e.Graphics.FillPath(shadowBrush, shadowPath);
                }

                // Card background with border radius
                using (var cardBrush = new SolidBrush(CardBackground))
                {
                    var cardPath = CreateRoundedRectangle(rect, 20);
                    e.Graphics.FillPath(cardBrush, cardPath);
                }

                // Accent border (left) - rounded
                using (var accentBrush = new SolidBrush(accentColor))
                {
                    var accentRect = new Rectangle(0, 0, 4, rect.Height);
                    e.Graphics.FillRectangle(accentBrush, accentRect);
                }

                // Icon
                using (var iconBrush = new SolidBrush(Color.FromArgb(80, accentColor)))
                {
                    using (var iconFont = new WinFont("Segoe UI Emoji", 16))
                    {
                        e.Graphics.DrawString(icon, iconFont, iconBrush,
                            new PointF(rect.Width - 35, 8));
                    }
                }

                // Border with radius
                using (var borderPen = new Pen(Color.FromArgb(30, 0, 0, 0), 1))
                {
                    var borderPath = CreateRoundedRectangle(new Rectangle(0, 0, rect.Width - 1, rect.Height - 1), 20);
                    e.Graphics.DrawPath(borderPen, borderPath);
                }
            };

            // ✅ Smooth animations - Hover effect
            panel.MouseEnter += (s, e) => {
                AnimatePanel(panel, CardBackground, Color.FromArgb(248, 252, 255), 200);
            };
            panel.MouseLeave += (s, e) => {
                AnimatePanel(panel, Color.FromArgb(248, 252, 255), CardBackground, 200);
            };
        }

        // ✅ Smooth animations - Panel animasyonu
        public void AnimatePanel(Panel panel, Color fromColor, Color toColor, int duration)
        {
            var timer = new Timer();
            var startTime = DateTime.Now;
            timer.Interval = 16; // ~60 FPS

            timer.Tick += (s, e) => {
                var elapsed = DateTime.Now - startTime;
                var progress = Math.Min(1.0, elapsed.TotalMilliseconds / duration);

                var r = (int)(fromColor.R + (toColor.R - fromColor.R) * progress);
                var g = (int)(fromColor.G + (toColor.G - fromColor.G) * progress);
                var b = (int)(fromColor.B + (toColor.B - fromColor.B) * progress);

                panel.BackColor = Color.FromArgb(r, g, b);
                panel.Invalidate();

                if (progress >= 1.0)
                {
                    timer.Stop();
                    timer.Dispose();
                }
            };

            timer.Start();
        }

        public void ApplyToolbarDesign()
        {
            panelToolbar.BackColor = CardBackground;
            panelToolbar.Height = 60;
            panelToolbar.Padding = new Padding(20, 10, 20, 10);

            // Shadow effect
            panelToolbar.Paint += (s, e) => {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (var shadowBrush = new SolidBrush(Color.FromArgb(8, 0, 0, 0)))
                {
                    e.Graphics.FillRectangle(shadowBrush, 0, panelToolbar.Height - 3, panelToolbar.Width, 3);
                }
            };

            // ComboBox'ları modern stil
            StyleComboBox(cmbFiltre);
            StyleComboBox(cmbSiralama);

            // TextBox modern stil
            StyleTextBox(txtArama);
        }

        // ✅ Modern Button Design - Yuvarlatılmış butonlar
        public void StyleComboBox(ComboBox cmb)
        {
            cmb.FlatStyle = FlatStyle.Flat;
            cmb.BackColor = Color.FromArgb(248, 249, 250);
            cmb.Font = new WinFont("Segoe UI", 9);
            cmb.Height = 35;

            // 15px border radius
            cmb.Paint += (s, e) => {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (var brush = new SolidBrush(cmb.BackColor))
                {
                    var path = CreateRoundedRectangle(cmb.ClientRectangle, 15);
                    e.Graphics.FillPath(brush, path);
                }
            };
        }

        public void StyleTextBox(TextBox txt)
        {
            txt.BorderStyle = BorderStyle.None;
            txt.BackColor = Color.FromArgb(248, 249, 250);
            txt.Font = new WinFont("Segoe UI", 10);
            txt.Height = 35;

            // Container panel ile border radius
            var container = new Panel();
            container.Size = new Size(txt.Width + 20, txt.Height + 10);
            container.Location = txt.Location;
            container.BackColor = Color.FromArgb(248, 249, 250);

            container.Paint += (s, e) => {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (var brush = new SolidBrush(container.BackColor))
                {
                    var path = CreateRoundedRectangle(container.ClientRectangle, 15);
                    e.Graphics.FillPath(brush, path);
                }
            };

            // Placeholder effect
            if (txt == txtArama)
            {
                txt.Text = "Müşteri adı, cari kod veya telefon ara...";
                txt.ForeColor = Color.Gray;
                txt.GotFocus += (s, e) => {
                    if (txt.Text == "Müşteri adı, cari kod veya telefon ara...")
                    {
                        txt.Text = "";
                        txt.ForeColor = Color.Black;
                    }
                };
                txt.LostFocus += (s, e) => {
                    if (string.IsNullOrEmpty(txt.Text))
                    {
                        txt.Text = "Müşteri adı, cari kod veya telefon ara...";
                        txt.ForeColor = Color.Gray;
                    }
                };
            }
        }

        public void ApplyProgressPanelDesign()
        {
            panelProgress.BackColor = CardBackground;
            panelProgress.Height = 45;
            panelProgress.Padding = new Padding(20, 8, 20, 8);

            // Modern progress bar with rounded corners
            progressBarTahsilat.Height = 25;
            progressBarTahsilat.Style = ProgressBarStyle.Continuous;

            // Custom paint for rounded progress bar
            progressBarTahsilat.Paint += (s, e) => {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                var rect = progressBarTahsilat.ClientRectangle;

                // Background
                using (var bgBrush = new SolidBrush(Color.FromArgb(230, 230, 230)))
                {
                    var bgPath = CreateRoundedRectangle(rect, 12);
                    e.Graphics.FillPath(bgBrush, bgPath);
                }

                // Progress
                var progressWidth = (int)((double)progressBarTahsilat.Value / progressBarTahsilat.Maximum * rect.Width);
                if (progressWidth > 0)
                {
                    var progressRect = new Rectangle(0, 0, progressWidth, rect.Height);
                    using (var progressBrush = new SolidBrush(SuccessGreen))
                    {
                        var progressPath = CreateRoundedRectangle(progressRect, 12);
                        e.Graphics.FillPath(progressBrush, progressPath);
                    }
                }
            };
        }

        public void ApplyBottomActionsDesign()
        {
            panelBottomActions.BackColor = LightGray;
            panelBottomActions.Height = 70;
            panelBottomActions.Padding = new Padding(20, 10, 20, 10);
        }

        public void ApplyFooterDesign()
        {
            panelFooter.BackColor = Color.MidnightBlue;
            panelFooter.Height = 35;
            panelFooter.Padding = new Padding(20, 8, 20, 8);

            // ✅ Footer rounded - Sadece alt köşeler
            panelFooter.Region = CreateBottomRoundedRegion(panelFooter.Size, 20);

            // Footer gradient
            panelFooter.Paint += (s, e) => {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (var brush = new LinearGradientBrush(
                    panelFooter.ClientRectangle,
                    Color.MidnightBlue,
                    Color.FromArgb(20, 40, 80),
                    LinearGradientMode.Horizontal))
                {
                    var footerPath = CreateRoundedRectangle(panelFooter.ClientRectangle, 20);
                    e.Graphics.FillPath(brush, footerPath);
                }
            };
        }

        // ✅ Modern Button Design - Yuvarlatılmış butonlar
        public void ApplyModernButtonDesign()
        {
            // Ana butonlar
            StyleModernButton(btnYenile, SuccessGreen, "🔄 Yenile");
            StyleModernButton(btnAra, PrimaryBlue, "🔍 Ara");

            // Action butonlar
            StyleModernButton(btnTumunuPasifYap, DangerRed, "Pasif Yap");
            StyleModernButton(btnTopluEmail, WarningYellow, "E-posta Gönder", Color.Black);
            StyleModernButton(btnRaporOlustur, InfoCyan, "Rapor Oluştur");
            StyleModernButton(btnExcelAktar, SuccessGreen, "Excel'e Aktar");
        }

        // ✅ Modern Button Design - 20px border radius + smooth animations
        public void StyleModernButton(Button btn, Color bgColor, string text, Color? textColor = null)
        {
            btn.BackColor = bgColor;
            btn.ForeColor = textColor ?? Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new WinFont("Segoe UI", 9F, FontStyle.Regular); // Medium değil Regular
            btn.Text = text;
            btn.Cursor = Cursors.Hand;
            btn.Height = 38;

            // 20px border radius
            btn.Paint += (s, e) => {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                // Shadow
                using (var shadowBrush = new SolidBrush(Color.FromArgb(20, 0, 0, 0)))
                {
                    var shadowRect = new Rectangle(1, 2, btn.Width - 2, btn.Height - 2);
                    var shadowPath = CreateRoundedRectangle(shadowRect, 18);
                    e.Graphics.FillPath(shadowBrush, shadowPath);
                }

                // Button background
                using (var btnBrush = new SolidBrush(btn.BackColor))
                {
                    var btnPath = CreateRoundedRectangle(btn.ClientRectangle, 20);
                    e.Graphics.FillPath(btnBrush, btnPath);
                }

                // Text
                var textSize = e.Graphics.MeasureString(btn.Text, btn.Font);
                var textX = (btn.Width - textSize.Width) / 2;
                var textY = (btn.Height - textSize.Height) / 2;

                using (var textBrush = new SolidBrush(btn.ForeColor))
                {
                    e.Graphics.DrawString(btn.Text, btn.Font, textBrush, textX, textY);
                }
            };

            // ✅ Smooth animations - Hover animasyonu
            btn.MouseEnter += (s, e) => {
                AnimateButton(btn, btn.BackColor, ControlPaint.Light(bgColor, 0.3f), 150);
            };
            btn.MouseLeave += (s, e) => {
                AnimateButton(btn, btn.BackColor, bgColor, 150);
            };

            // Click animasyonu
            btn.MouseDown += (s, e) => {
                AnimateButton(btn, btn.BackColor, ControlPaint.Dark(bgColor, 0.1f), 100);
            };
            btn.MouseUp += (s, e) => {
                AnimateButton(btn, btn.BackColor, ControlPaint.Light(bgColor, 0.3f), 100);
            };
        }

        // ✅ Smooth animations - Button animasyonu
        public void AnimateButton(Button btn, Color fromColor, Color toColor, int duration)
        {
            var timer = new Timer();
            var startTime = DateTime.Now;
            timer.Interval = 16; // ~60 FPS

            timer.Tick += (s, e) => {
                var elapsed = DateTime.Now - startTime;
                var progress = Math.Min(1.0, elapsed.TotalMilliseconds / duration);

                var r = (int)(fromColor.R + (toColor.R - fromColor.R) * progress);
                var g = (int)(fromColor.G + (toColor.G - fromColor.G) * progress);
                var b = (int)(fromColor.B + (toColor.B - fromColor.B) * progress);

                btn.BackColor = Color.FromArgb(r, g, b);
                btn.Invalidate();

                if (progress >= 1.0)
                {
                    timer.Stop();
                    timer.Dispose();
                }
            };

            timer.Start();
        }

        public void InitializeComboBoxes()
        {
            // Filtre ComboBox
            cmbFiltre.Items.Clear();
            cmbFiltre.Items.AddRange(new string[] {
                "Tüm Borçlular",
                "Sadece Kritik (90+ gün)",
                "Çok Riskli (60+ gün)",
                "Riskli (30+ gün)",
                "Yeni Borçlular (0-15 gün)",
                "Büyük Borçlar (>10.000₺)",
                "Orta Borçlar (1.000-10.000₺)",
                "Küçük Borçlar (<1.000₺)"
            });
            cmbFiltre.SelectedIndex = 0;

            // Sıralama ComboBox
            cmbSiralama.Items.Clear();
            cmbSiralama.Items.AddRange(new string[] {
                "Borç Miktarı (Yüksek→Düşük)",
                "Borç Miktarı (Düşük→Yüksek)",
                "Son Ödeme Tarihi (Eski→Yeni)",
                "Son Ödeme Tarihi (Yeni→Eski)",
                "Müşteri Adı (A→Z)",
                "Müşteri Adı (Z→A)",
                "Geçen Süre (Uzun→Kısa)",
                "Risk Skoru (Yüksek→Düşük)"
            });
            cmbSiralama.SelectedIndex = 0;
        }

        public void InitializeTimers()
        {
            // Otomatik yenileme timer'ı
            refreshTimer = new Timer();
            refreshTimer.Interval = 60000; // 1 dakika
            refreshTimer.Tick += RefreshTimer_Tick;
            refreshTimer.Start();

            // Bildirim timer'ı
            notificationTimer = new Timer();
            notificationTimer.Interval = 5000; // 5 saniye
            notificationTimer.Tick += NotificationTimer_Tick;
            notificationTimer.Start();
        }

        public void InitializeAnimations()
        {
            animationTimer = new Timer();
            animationTimer.Interval = 50;
            animationTimer.Tick += AnimationTimer_Tick;
            animationTimer.Start();
        }

        public void AnimationTimer_Tick(object sender, EventArgs e)
        {
            // Subtle animations
        }

        public void InitializeKeyboardShortcuts()
        {
            this.KeyPreview = true;
            this.KeyDown += (s, e) => {
                switch (e.KeyCode)
                {
                    case Keys.F5:
                        LoadData();
                        break;
                    case Keys.F when e.Control:
                        txtArama.Focus();
                        break;
                    case Keys.E when e.Control:
                        btnExcelAktar_Click(null, null);
                        break;
                    case Keys.Escape:
                        this.Close();
                        break;
                }
            };
        }
        #endregion

        #region DataGridView Setup
        public void SetupDataGridView()
        {
            // Önce tüm ayarları temizle
            dgvBorclular.Columns.Clear();
            dgvBorclular.DataSource = null;

            // Temel ayarlar
            dgvBorclular.AutoGenerateColumns = false;
            dgvBorclular.AllowUserToAddRows = false;
            dgvBorclular.AllowUserToDeleteRows = false;
            dgvBorclular.ReadOnly = true;
            dgvBorclular.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBorclular.MultiSelect = false;
            dgvBorclular.RowHeadersVisible = false;

            // Görünüm ayarları
            dgvBorclular.BackgroundColor = CardBackground;
            dgvBorclular.BorderStyle = BorderStyle.None;
            dgvBorclular.GridColor = Color.FromArgb(230, 230, 230);
            dgvBorclular.RowTemplate.Height = 40;

            // KRİTİK AYARLAR - SORUNUN ÇÖZÜMÜ
            dgvBorclular.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvBorclular.AllowUserToResizeColumns = false;
            dgvBorclular.AllowUserToResizeRows = false;
            dgvBorclular.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            // BOYUT VE KONUM - TAM UYUMLU
            dgvBorclular.Size = new Size(876, 226); // panelMain içindeki alan: 916-40=876
            dgvBorclular.Location = new Point(20, 10); // panelMain padding'i
            dgvBorclular.Dock = DockStyle.None; // Dock kullanma!

            // SÜTUNLAR - İLK RESİMDEKİ GİBİ GENİŞLİKLER
            // Toplam kullanılabilir: 876px
            AddGridColumn("CariKodu", "Cari Kod", 100);        // 100px
            AddGridColumn("Unvan", "Müşteri Adı", 240);       // 280px  
            AddGridColumn("Bakiye", "Borç Miktarı", 126); // 140px
            AddGridColumn("SonOdemeTarihi", "Son Ödeme", 130);  // 120px
            AddGridColumn("GecenSure", "Geçen Süre", 100);     // 100px
            AddGridColumn("RiskSkoru", "Risk Skoru", 90);      // 90px
            AddGridColumn("Durum", "Durum", 100);              // 100px

            // HEADER STİLLERİ - İLK RESİMDEKİ GİBİ
            dgvBorclular.ColumnHeadersDefaultCellStyle.Font = new WinFont("Segoe UI", 10, FontStyle.Bold);
            dgvBorclular.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvBorclular.ColumnHeadersDefaultCellStyle.BackColor = DarkBlue;
            dgvBorclular.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvBorclular.ColumnHeadersDefaultCellStyle.Padding = new Padding(8, 5, 8, 5);
            dgvBorclular.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False; // Tek satır
            dgvBorclular.ColumnHeadersHeight = 40;
            dgvBorclular.EnableHeadersVisualStyles = false;

            // SATIR STİLLERİ - İLK RESİMDEKİ GİBİ
            dgvBorclular.DefaultCellStyle.Font = new WinFont("Segoe UI", 9);
            dgvBorclular.DefaultCellStyle.Padding = new Padding(8, 3, 8, 3);
            dgvBorclular.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvBorclular.DefaultCellStyle.SelectionBackColor = SecondaryBlue;
            dgvBorclular.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvBorclular.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            dgvBorclular.AlternatingRowsDefaultCellStyle.Padding = new Padding(8, 3, 8, 3);

            // BORDER STİLLERİ
            dgvBorclular.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvBorclular.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvBorclular.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvBorclular.ScrollBars = ScrollBars.Vertical;
        }
        #endregion

        public void AddGridColumn(string name, string header, int width, bool isNumeric = false)
        {
            var column = new DataGridViewTextBoxColumn();
            column.Name = name;
            column.HeaderText = header;
            column.Width = width;
            column.DataPropertyName = name;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            column.Resizable = DataGridViewTriState.False;
            column.MinimumWidth = width;
            column.FillWeight = 1; // Önemli!

            if (isNumeric)
            {
                column.DefaultCellStyle.Format = "C0";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            else
            {
                switch (name)
                {
                    case "CariKodu":
                    case "RiskSkoru":
                    case "GecenSure":
                    case "SonOdemeTarihi":
                    case "Durum":
                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        break;
                    default: // Müşteri Adı
                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        break;
                }
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            dgvBorclular.Columns.Add(column);
        }

        #region Data Operations
        public async void LoadData()
        {
            try
            {
                ShowLoadingState("Veriler yükleniyor...");
                await Task.Run(() => {
                    borclular = GetOdemeYapmayanCariler();
                });

                UpdateDataGridView();
                UpdateStatistics();
                UpdateProgressBar();
                ShowSuccessState($"Son güncelleme: {DateTime.Now:HH:mm:ss} - {borclular.Count} kayıt");
            }
            catch (Exception ex)
            {
                ShowErrorState($"Veri yükleme hatası: {ex.Message}");
            }

            // Double check
            dgvBorclular.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvBorclular.AllowUserToResizeColumns = false;
        }

        public List<BorcKaydi> GetOdemeYapmayanCariler()
        {
            List<BorcKaydi> borclular = new List<BorcKaydi>();
            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = @"
                    SELECT C.CariKodu, C.Unvani, C.Bakiye AS Bakiye,
                           MAX(ch.Tarih) as SonOdemeTarihi,
                           julianday('now') - julianday(MAX(ch.Tarih)) as GecenGun,
                           C.Telefon, C.Adres
                    FROM Cari C
                    LEFT JOIN cari_hareketleri ch ON C.CariKodu = ch.cari_kodu
                        AND ch.Tur = 'Tahsilat Dekontu'
                    WHERE C.CariKodu NOT IN (
                        SELECT DISTINCT Cari_Kodu
                        FROM cari_hareketleri
                        WHERE Tur = 'Tahsilat Dekontu'
                        AND Tarih >= DATE('now', '-30 days')
                    )
                    AND C.Bakiye > 0
                    GROUP BY C.CariKodu, C.Unvani, C.Bakiye, C.Telefon, C.Adres
                    ORDER BY C.Bakiye DESC";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var kayit = new BorcKaydi
                            {
                                CariKodu = reader["CariKodu"].ToString(),
                                Unvan = reader["Unvani"].ToString(),
                                Bakiye = reader["Bakiye"] != DBNull.Value ? Convert.ToDecimal(reader["Bakiye"]) : 0,
                                SonOdemeTarihi = reader["SonOdemeTarihi"] != DBNull.Value ?
                                    Convert.ToDateTime(reader["SonOdemeTarihi"]) : DateTime.MinValue,
                                GecenGun = reader["GecenGun"] != DBNull.Value ?
                                    Convert.ToInt32(reader["GecenGun"]) : 999,
                                Telefon = reader["Telefon"]?.ToString() ?? "",
                                Adres = reader["Adres"]?.ToString() ?? ""
                            };

                            // Risk skoru hesapla
                            kayit.RiskSkoru = CalculateRiskScore(kayit);
                            borclular.Add(kayit);
                        }
                    }
                }
            }

            return borclular;
        }

        public int CalculateRiskScore(BorcKaydi kayit)
        {
            int score = 0;

            // Borç miktarına göre puan (40%)
            if (kayit.Bakiye > 10000) score += 40;
            else if (kayit.Bakiye > 5000) score += 30;
            else if (kayit.Bakiye > 1000) score += 20;
            else score += 10;

            // Geçen süreye göre puan (60%)
            if (kayit.GecenGun > 90) score += 60;
            else if (kayit.GecenGun > 60) score += 45;
            else if (kayit.GecenGun > 30) score += 30;
            else score += 15;

            return Math.Min(score, 100);
        }

        public void UpdateDataGridView()
        {
            ApplyFilter();
            ApplySort();

            dgvBorclular.Rows.Clear();

            foreach (var item in filtreliBorclular)
            {
                string durum = GetDurumText(item.GecenGun);

                int rowIndex = dgvBorclular.Rows.Add(
                    item.CariKodu,
                    item.Unvan,
                    item.Bakiye,
                    item.SonOdemeTarihi == DateTime.MinValue ? "Hiç ödeme yok" :
                        item.SonOdemeTarihi.ToString("dd.MM.yyyy"),
                    item.GecenGun > 900 ? "Hiç ödeme yok" : $"{item.GecenGun} gün",
                    $"{item.RiskSkoru}/100",
                    durum
                );

                // Satır renklandirme
                DataGridViewRow row = dgvBorclular.Rows[rowIndex];
                ApplyRowStyling(row, item);
            }

            lblToplamKayit.Text = $"Toplam {filtreliBorclular.Count} kayıt listeleniyor";
        }

        public void ApplyRowStyling(DataGridViewRow row, BorcKaydi item)
        {
            if (item.GecenGun >= 90)
            {
                row.DefaultCellStyle.BackColor = Color.FromArgb(255, 235, 235);
                row.DefaultCellStyle.ForeColor = Color.DarkRed;
                row.DefaultCellStyle.Font = new WinFont("Segoe UI", 9, FontStyle.Bold);
            }
            else if (item.GecenGun >= 60)
            {
                row.DefaultCellStyle.BackColor = Color.FromArgb(255, 248, 225);
                row.DefaultCellStyle.ForeColor = Color.DarkOrange;
            }
            else if (item.GecenGun >= 30)
            {
                row.DefaultCellStyle.BackColor = Color.FromArgb(255, 252, 235);
                row.DefaultCellStyle.ForeColor = Color.Orange;
            }

            // Durum sütunu özel renklendirme
            row.Cells["Durum"].Style.ForeColor = GetDurumColor(item.GecenGun);
            row.Cells["Durum"].Style.Font = new WinFont("Segoe UI", 9, FontStyle.Bold);

            // Risk skoru renklendirme
            Color riskColor = item.RiskSkoru >= 80 ? Color.Red :
                             item.RiskSkoru >= 60 ? Color.Orange :
                             item.RiskSkoru >= 40 ? Color.DarkOrange : Color.Green;
            row.Cells["RiskSkoru"].Style.ForeColor = riskColor;
            row.Cells["RiskSkoru"].Style.Font = new WinFont("Segoe UI", 9, FontStyle.Bold);
        }
        #endregion

        #region Filtering & Sorting
        public void ApplyFilter()
        {
            filtreliBorclular = borclular.ToList();

            // Arama filtresi
            if (!string.IsNullOrEmpty(txtArama.Text) && txtArama.Text != "Müşteri adı, cari kod veya telefon ara...")
            {
                string arama = txtArama.Text.ToLower();
                filtreliBorclular = filtreliBorclular.Where(x =>
                    x.Unvan.ToLower().Contains(arama) ||
                    x.CariKodu.ToLower().Contains(arama) ||
                    x.Telefon.ToLower().Contains(arama)).ToList();
            }

            // Kategori filtresi
            switch (currentFilter)
            {
                case "Sadece Kritik (90+ gün)":
                    filtreliBorclular = filtreliBorclular.Where(x => x.GecenGun >= 90).ToList();
                    break;
                case "Çok Riskli (60+ gün)":
                    filtreliBorclular = filtreliBorclular.Where(x => x.GecenGun >= 60).ToList();
                    break;
                case "Riskli (30+ gün)":
                    filtreliBorclular = filtreliBorclular.Where(x => x.GecenGun >= 30).ToList();
                    break;
                case "Yeni Borçlular (0-15 gün)":
                    filtreliBorclular = filtreliBorclular.Where(x => x.GecenGun <= 15).ToList();
                    break;
                case "Büyük Borçlar (>10.000₺)":
                    filtreliBorclular = filtreliBorclular.Where(x => x.Bakiye > 10000).ToList();
                    break;
                case "Orta Borçlar (1.000-10.000₺)":
                    filtreliBorclular = filtreliBorclular.Where(x => x.Bakiye >= 1000 && x.Bakiye <= 10000).ToList();
                    break;
                case "Küçük Borçlar (<1.000₺)":
                    filtreliBorclular = filtreliBorclular.Where(x => x.Bakiye < 1000).ToList();
                    break;
            }
        }

        public void ApplySort()
        {
            switch (currentSort)
            {
                case "Borç Miktarı (Yüksek→Düşük)":
                    filtreliBorclular = filtreliBorclular.OrderByDescending(x => x.Bakiye).ToList();
                    break;
                case "Borç Miktarı (Düşük→Yüksek)":
                    filtreliBorclular = filtreliBorclular.OrderBy(x => x.Bakiye).ToList();
                    break;
                case "Son Ödeme Tarihi (Eski→Yeni)":
                    filtreliBorclular = filtreliBorclular.OrderBy(x => x.SonOdemeTarihi).ToList();
                    break;
                case "Son Ödeme Tarihi (Yeni→Eski)":
                    filtreliBorclular = filtreliBorclular.OrderByDescending(x => x.SonOdemeTarihi).ToList();
                    break;
                case "Müşteri Adı (A→Z)":
                    filtreliBorclular = filtreliBorclular.OrderBy(x => x.Unvan).ToList();
                    break;
                case "Müşteri Adı (Z→A)":
                    filtreliBorclular = filtreliBorclular.OrderByDescending(x => x.Unvan).ToList();
                    break;
                case "Geçen Süre (Uzun→Kısa)":
                    filtreliBorclular = filtreliBorclular.OrderByDescending(x => x.GecenGun).ToList();
                    break;
                case "Risk Skoru (Yüksek→Düşük)":
                    filtreliBorclular = filtreliBorclular.OrderByDescending(x => x.RiskSkoru).ToList();
                    break;
            }
        }
        #endregion

        #region Statistics & Progress
        public void UpdateStatistics()
        {
            int toplamBorclu = borclular.Count;
            decimal toplamBorc = borclular.Sum(x => x.Bakiye);
            int kritikDurum = borclular.Count(x => x.GecenGun >= 90);
            decimal vadesiGecen = borclular.Where(x => x.GecenGun >= 30).Sum(x => x.Bakiye);

            // Animasyonlu sayı güncelleme
            AnimateNumber(lblToplamBorclu, toplamBorclu);
            AnimateNumber(lblToplamBorc, toplamBorc, true);
            AnimateNumber(lblKritikDurum, kritikDurum);
            AnimateNumber(lblVadesiGecen, vadesiGecen, true);
        }

        public void AnimateNumber(Label label, decimal targetValue, bool isCurrency = false)
        {
            // Basit animasyon - gerçek uygulamada Timer kullanılabilir
            if (isCurrency)
                label.Text = targetValue.ToString("C0");
            else
                label.Text = targetValue.ToString("N0");
        }

        public void UpdateProgressBar()
        {
            // Bu ay tahsilat hedefi hesaplama (örnek)
            decimal hedefTahsilat = 100000;
            decimal mevcutTahsilat = GetBuAyTahsilat();
            int yuzde = (int)Math.Min((mevcutTahsilat / hedefTahsilat) * 100, 100);

            progressBarTahsilat.Value = yuzde;
            lblTahsilatYuzde.Text = $"%{yuzde} (₺{mevcutTahsilat:N0} / ₺{hedefTahsilat:N0})";

            // Progress bar rengi
            if (yuzde >= 80) progressBarTahsilat.ForeColor = SuccessGreen;
            else if (yuzde >= 60) progressBarTahsilat.ForeColor = WarningYellow;
            else progressBarTahsilat.ForeColor = DangerRed;
        }

        public decimal GetBuAyTahsilat()
        {
            // Bu ayın tahsilat toplamını veritabanından çek
            decimal toplam = 0;
            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";

            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                        SELECT SUM(Tutar) as Toplam
                        FROM cari_hareketleri
                        WHERE Tur = 'Tahsilat Dekontu'
                        AND strftime('%Y-%m', Tarih) = strftime('%Y-%m', 'now')";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        var result = command.ExecuteScalar();
                        if (result != DBNull.Value && result != null)
                            toplam = Convert.ToDecimal(result);
                    }
                }
            }
            catch { }

            return toplam;
        }
        #endregion

        #region Utility Methods
        public string GetDurumText(int gecenGun)
        {
            if (gecenGun >= 90) return "KRİTİK";
            if (gecenGun >= 60) return "ÇOK RİSKLİ";
            if (gecenGun >= 30) return "RİSKLİ";
            if (gecenGun >= 15) return "DİKKAT";
            return "NORMAL";
        }

        public Color GetDurumColor(int gecenGun)
        {
            if (gecenGun >= 90) return Color.DarkRed;
            if (gecenGun >= 60) return Color.Red;
            if (gecenGun >= 30) return Color.Orange;
            if (gecenGun >= 15) return Color.DarkOrange;
            return SuccessGreen;
        }

        public void ShowLoadingState(string message)
        {
            lblDurum.Text = message;
            lblDurum.ForeColor = Color.Transparent;
            progressBar.Visible = true;
            progressBar.Style = ProgressBarStyle.Marquee;
        }

        public void ShowSuccessState(string message)
        {
            lblDurum.Text = message;
            lblDurum.ForeColor = Color.White;
            progressBar.Visible = false;
        }

        public void ShowErrorState(string message)
        {
            lblDurum.Text = message;
            lblDurum.ForeColor = DangerRed;
            progressBar.Visible = false;
            ShowNotification("Hata!", message, NotificationType.Error);
        }

        public void ShowNotification(string title, string message, NotificationType type)
        {
            MessageBox.Show($"{title}\n\n{message}",
                type == NotificationType.Error ? "Hata" : "Bilgi",
                MessageBoxButtons.OK,
                type == NotificationType.Error ? MessageBoxIcon.Error : MessageBoxIcon.Information);
        }
        #endregion

        #region Event Handlers
        public void RefreshTimer_Tick(object sender, EventArgs e)
        {
            LoadData();
        }

        public void NotificationTimer_Tick(object sender, EventArgs e)
        {
            // Kritik durumları kontrol et
            int kritikSayi = borclular.Count(x => x.GecenGun >= 90);
            if (kritikSayi > 0)
            {
                this.Text = $"⚠️ Ödeme Yapmayanlar ({kritikSayi} kritik)";
            }
            else
            {
                this.Text = "Ödeme Yapmayanlar Yönetimi";
            }
        }

        public void btnYenile_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        public void cmbFiltre_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentFilter = cmbFiltre.SelectedItem.ToString();
            UpdateDataGridView();
        }

        public void cmbSiralama_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentSort = cmbSiralama.SelectedItem.ToString();
            UpdateDataGridView();
        }

        public void txtArama_TextChanged(object sender, EventArgs e)
        {
            UpdateDataGridView();
        }

        public void btnTumunuPasifYap_Click(object sender, EventArgs e)
        {
            if (filtreliBorclular.Count == 0)
            {
                ShowNotification("Uyarı", "Pasif yapılacak kayıt bulunamadı.", NotificationType.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                $"{filtreliBorclular.Count} müşteriyi pasif hesap yapmak istediğinize emin misiniz?\n\n" +
                "Bu işlem geri alınamaz!",
                "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                PasifHesapYap(filtreliBorclular);
                LoadData();
            }
        }

        public void PasifHesapYap(List<BorcKaydi> liste)
        {
            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (SQLiteTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        foreach (var item in liste)
                        {
                            string query = "UPDATE Cari SET Hesap = 'Pasif Hesap' WHERE CariKodu = @CariKodu";
                            using (SQLiteCommand command = new SQLiteCommand(query, connection, transaction))
                            {
                                command.Parameters.AddWithValue("@CariKodu", item.CariKodu);
                                command.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                        ShowNotification("Başarılı", $"{liste.Count} müşteri pasif hesap yapıldı.", NotificationType.Success);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        ShowNotification("Hata", $"İşlem başarısız: {ex.Message}", NotificationType.Error);
                    }
                }
            }
        }

        public void btnExcelAktar_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }

        public void ExportToExcel()
        {
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                saveDialog.FileName = $"OdemeYapmayanlar_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    ShowLoadingState("Excel dosyası oluşturuluyor...");

                    Excel.Application excelApp = new Excel.Application();
                    Excel.Workbook workbook = excelApp.Workbooks.Add();
                    Excel.Worksheet worksheet = workbook.ActiveSheet;

                    // Başlık satırı
                    worksheet.Cells[1, 1] = "Cari Kod";
                    worksheet.Cells[1, 2] = "Müşteri Adı";
                    worksheet.Cells[1, 3] = "Borç Miktarı";
                    worksheet.Cells[1, 4] = "Son Ödeme Tarihi";
                    worksheet.Cells[1, 5] = "Geçen Süre (Gün)";
                    worksheet.Cells[1, 6] = "Risk Skoru";
                    worksheet.Cells[1, 7] = "Durum";
                    worksheet.Cells[1, 8] = "Telefon";
                    worksheet.Cells[1, 9] = "Adres";

                    // Veriler
                    for (int i = 0; i < filtreliBorclular.Count; i++)
                    {
                        var item = filtreliBorclular[i];
                        int row = i + 2;

                        worksheet.Cells[row, 1] = item.CariKodu;
                        worksheet.Cells[row, 2] = item.Unvan;
                        worksheet.Cells[row, 3] = item.Bakiye;
                        worksheet.Cells[row, 4] = item.SonOdemeTarihi == DateTime.MinValue ?
                            "Hiç ödeme yok" : item.SonOdemeTarihi.ToString("dd.MM.yyyy");
                        worksheet.Cells[row, 5] = item.GecenGun;
                        worksheet.Cells[row, 6] = item.RiskSkoru;
                        worksheet.Cells[row, 7] = GetDurumText(item.GecenGun);
                        worksheet.Cells[row, 8] = item.Telefon;
                        worksheet.Cells[row, 9] = item.Adres;
                    }

                    // Formatla
                    worksheet.Columns.AutoFit();
                    worksheet.Range["A1:I1"].Font.Bold = true;

                    workbook.SaveAs(saveDialog.FileName);
                    workbook.Close();
                    excelApp.Quit();

                    ShowSuccessState("Excel dosyası başarıyla oluşturuldu.");
                    ShowNotification("Başarılı", "Excel dosyası oluşturuldu.", NotificationType.Success);
                }
            }
            catch (Exception ex)
            {
                ShowErrorState($"Excel aktarma hatası: {ex.Message}");
            }
        }

        public void dgvBorclular_DoubleClick(object sender, EventArgs e)
        {
            if (dgvBorclular.CurrentRow != null)
            {
                string cariKodu = dgvBorclular.CurrentRow.Cells["CariKodu"].Value.ToString();
                string unvan = dgvBorclular.CurrentRow.Cells["Unvan"].Value.ToString();
                ShowNotification("Bilgi", $"Cari detay açılacak: {unvan} ({cariKodu})", NotificationType.Info);
            }
        }

        public void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            refreshTimer?.Stop();
            refreshTimer?.Dispose();
            notificationTimer?.Stop();
            notificationTimer?.Dispose();
            animationTimer?.Stop();
            animationTimer?.Dispose();
            base.OnFormClosing(e);
        }
        #endregion

        public void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void btnRaporOlustur_Click(object sender, EventArgs e)
        {
            // Modern popup dialog göster
            using (var raporDialog = new FormRaporPopup(filtreliBorclular))
            {
                raporDialog.ShowDialog();
            }
        }
    }

    #region Data Classes & Enums
    public class BorcKaydi
    {
        public string CariKodu { get; set; }
        public string Unvan { get; set; }
        public decimal Bakiye { get; set; }
        public DateTime SonOdemeTarihi { get; set; }
        public int GecenGun { get; set; }
        public int RiskSkoru { get; set; }
        public string Telefon { get; set; }
        public string Adres { get; set; }
    }

    public enum NotificationType
    {
        Info,
        Success,
        Warning,
        Error
    }
    #endregion
}