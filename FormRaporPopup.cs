using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;

namespace Veresiye2025
{
    public partial class FormRaporPopup : Form
    {
        public List<BorcKaydi> borclular;
        public Panel selectedPanel = null;

        // Modern renkler
        public readonly Color PrimaryBlue = Color.FromArgb(31, 81, 255);
        public readonly Color SuccessGreen = Color.FromArgb(40, 167, 69);
        public readonly Color DangerRed = Color.FromArgb(220, 53, 69);
        public readonly Color WarningOrange = Color.FromArgb(255, 133, 27);
        public readonly Color InfoCyan = Color.FromArgb(23, 162, 184);
        public readonly Color LightGray = Color.FromArgb(248, 250, 252);
        public readonly Color DarkBlue = Color.FromArgb(13, 27, 62);

        public FormRaporPopup(List<BorcKaydi> borclular)
        {
            this.borclular = borclular;
            InitializeComponent();
            SetupForm();
            ApplyRoundedCorners();
        }

        public void SetupForm()
        {
            // Form ayarları
            this.BackColor = Color.White;
            this.Paint += FormRaporPopup_Paint;

            // Header panel ayarları
            panelHeader.Paint += PanelHeader_Paint;

            // Labels ayarları
            lblTitle.Text = "📊 RAPOR OLUŞTUR";
            lblTitle.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;

            lblSubtitle.Text = "İstediğiniz rapor türünü seçin ve oluşturun";
            lblSubtitle.Font = new Font("Segoe UI", 10);
            lblSubtitle.ForeColor = Color.FromArgb(200, 220, 255);

            // Close button ayarları
            btnClose.Text = "✕";
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.BackColor = Color.Transparent;
            btnClose.ForeColor = Color.White;
            btnClose.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnClose.Cursor = Cursors.Hand;
            btnClose.MouseEnter += BtnClose_MouseEnter;
            btnClose.MouseLeave += BtnClose_MouseLeave;
            btnClose.Click += BtnClose_Click;

            // İstatistik panel ayarları
            panelStats.Paint += PanelStats_Paint;
            CreateQuickStats();

            // Rapor kartları oluştur
            CreateReportCards();

            // Footer ayarları
            panelFooter.BackColor = Color.MidnightBlue;
            lblFooter.Text = $"📋 Toplam {borclular.Count} müşteri • 💰 {borclular.Sum(x => x.Bakiye):C} toplam borç • ⚠️ {borclular.Count(x => x.GecenGun >= 90)} kritik durum";
            lblFooter.Font = new Font("Segoe UI", 9);
            lblFooter.ForeColor = Color.White;
        }

        public void ApplyRoundedCorners()
        {
            // 20px border radius için region oluştur
            GraphicsPath path = new GraphicsPath();
            int radius = 20;

            path.StartFigure();
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(this.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(this.Width - radius, this.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, this.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();

            this.Region = new Region(path);
        }

        public void FormRaporPopup_Paint(object sender, PaintEventArgs e)
        {
            // Drop shadow effect
            using (var pen = new Pen(Color.FromArgb(30, 0, 0, 0), 2))
            {
                e.Graphics.DrawPath(pen, GetRoundedRectPath(this.ClientRectangle, 20));
            }
        }

        public void PanelHeader_Paint(object sender, PaintEventArgs e)
        {
            // Gradient background
            using (var brush = new LinearGradientBrush(
                panelHeader.ClientRectangle,
                Color.MidnightBlue,
                Color.MidnightBlue,
                LinearGradientMode.Horizontal))
            {
                e.Graphics.FillRectangle(brush, panelHeader.ClientRectangle);
            }
        }

        public void PanelStats_Paint(object sender, PaintEventArgs e)
        {
            // Stats panel border
            using (var pen = new Pen(Color.FromArgb(50, 0, 0, 0), 1))
            {
                e.Graphics.DrawRectangle(pen, 0, 0, panelStats.Width - 1, panelStats.Height - 1);
            }
        }

        public GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
            path.CloseFigure();
            return path;
        }

        public void CreateQuickStats()
        {
            // İstatistik labels'ları oluştur
            CreateQuickStat(lblStat1Icon, lblStat1Value, lblStat1Label, "👥", borclular.Count.ToString(), "Toplam Borçlu");
            CreateQuickStat(lblStat2Icon, lblStat2Value, lblStat2Label, "💰", borclular.Sum(x => x.Bakiye).ToString("C0"), "Toplam Borç");
            CreateQuickStat(lblStat3Icon, lblStat3Value, lblStat3Label, "⚠️", borclular.Count(x => x.GecenGun >= 90).ToString(), "Kritik Durum");
            CreateQuickStat(lblStat4Icon, lblStat4Value, lblStat4Label, "📊", borclular.Count(x => x.RiskSkoru >= 80).ToString(), "Yüksek Risk");
        }

        public void CreateQuickStat(Label iconLabel, Label valueLabel, Label textLabel, string icon, string value, string label)
        {
            iconLabel.Text = icon;
            iconLabel.Font = new Font("Segoe UI Emoji", 16);

            valueLabel.Text = value;
            valueLabel.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            valueLabel.ForeColor = DarkBlue;

            textLabel.Text = label;
            textLabel.Font = new Font("Segoe UI", 8);
            textLabel.ForeColor = Color.FromArgb(108, 117, 125);
        }

        public void CreateReportCards()
        {
            // Rapor kartlarını ayarla
            SetupReportCard(panelCard1, lblCard1Title, lblCard1Desc, btnCard1, "📈 Aylık Takip", "Son 30 günün detaylı analizi", SuccessGreen, "AylikTakip");
            SetupReportCard(panelCard2, lblCard2Title, lblCard2Desc, btnCard2, "🎯 Risk Analizi", "Müşteri risk skorları", DangerRed, "RiskAnalizi");
            SetupReportCard(panelCard3, lblCard3Title, lblCard3Desc, btnCard3, "💰 Tahsilat Performans", "Hedef vs gerçekleşen", PrimaryBlue, "TahsilatPerformans");
            SetupReportCard(panelCard4, lblCard4Title, lblCard4Desc, btnCard4, "👥 Müşteri Segmentasyon", "Borç grupları analizi", InfoCyan, "MusteriSegmentasyon");
            SetupReportCard(panelCard5, lblCard5Title, lblCard5Desc, btnCard5, "📋 Detaylı Liste", "Tüm müşteri detayları", WarningOrange, "DetayliListe");
            SetupReportCard(panelCard6, lblCard6Title, lblCard6Desc, btnCard6, "📊 Dashboard", "Görsel grafikler", Color.FromArgb(108, 117, 125), "Dashboard");
        }

        public void SetupReportCard(Panel card, Label title, Label desc, Button btn, string titleText, string descText, Color color, string reportType)
        {
            card.BackColor = Color.White;
            card.Cursor = Cursors.Hand;
            card.Tag = reportType;

            card.Paint += (s, e) => {
                var rect = card.ClientRectangle;

                // Shadow
                using (var shadowBrush = new SolidBrush(Color.FromArgb(10, 0, 0, 0)))
                {
                    e.Graphics.FillRectangle(shadowBrush, new Rectangle(3, 3, rect.Width, rect.Height));
                }

                // Card background
                using (var cardBrush = new SolidBrush(Color.White))
                {
                    e.Graphics.FillRectangle(cardBrush, rect);
                }

                // Top colored border
                using (var colorBrush = new SolidBrush(color))
                {
                    e.Graphics.FillRectangle(colorBrush, new Rectangle(0, 0, rect.Width, 4));
                }

                // Border
                using (var borderPen = new Pen(Color.FromArgb(20, 0, 0, 0), 1))
                {
                    e.Graphics.DrawRectangle(borderPen, 0, 0, rect.Width - 1, rect.Height - 1);
                }
            };

            title.Text = titleText;
            title.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            title.ForeColor = DarkBlue;

            desc.Text = descText;
            desc.Font = new Font("Segoe UI", 8);
            desc.ForeColor = Color.FromArgb(108, 117, 125);

            btn.Text = "Oluştur";
            btn.BackColor = color;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI", 8, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
            btn.Click += (s, e) => CreateReport(reportType);

            card.MouseEnter += (s, e) => {
                card.BackColor = Color.FromArgb(250, 250, 255);
            };

            card.MouseLeave += (s, e) => {
                card.BackColor = Color.White;
            };

            card.Click += (s, e) => CreateReport(reportType);
        }

        public void CreateReport(string reportType)
        {
            try
            {
                this.Hide();

                switch (reportType)
                {
                    case "AylikTakip":
                        MessageBox.Show("Aylık takip raporu oluşturuluyor...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    case "RiskAnalizi":
                        MessageBox.Show("Risk analizi raporu oluşturuluyor...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    case "TahsilatPerformans":
                        MessageBox.Show("Tahsilat performans raporu oluşturuluyor...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    case "MusteriSegmentasyon":
                        MessageBox.Show("Müşteri segmentasyon raporu oluşturuluyor...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    case "DetayliListe":
                        MessageBox.Show("Detaylı liste raporu oluşturuluyor...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    case "Dashboard":
                        MessageBox.Show("Dashboard raporu oluşturuluyor...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                }

                this.Close();
            }
            catch (Exception ex)
            {
                this.Show();
                MessageBox.Show($"Rapor oluşturma hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Event handlers
        public void BtnClose_MouseEnter(object sender, EventArgs e)
        {
            btnClose.BackColor = Color.FromArgb(100, 255, 255, 255);
        }

        public void BtnClose_MouseLeave(object sender, EventArgs e)
        {
            btnClose.BackColor = Color.Transparent;
        }

        public void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ApplyRoundedCorners();
        }
    }
}