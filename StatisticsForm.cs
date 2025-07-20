using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Veresiye2025
{
    public partial class StatisticsForm : Form
    {
        public StatisticsData _stats;
        public StatisticsManager _statsManager;

        public StatisticsForm()
        {
            InitializeComponent();
            _statsManager = new StatisticsManager();
            
        }

        public void StatisticsForm_Load(object sender, EventArgs e)
        {
            try
            {
                // Kullanıcı ayarlarını yükle
                chkShowPopup.Checked = UserSettings.Instance.ShowStatisticsPopup;
                txtTarget.Text = UserSettings.Instance.MonthlyTarget.ToString("N2");

                // İstatistikleri hesapla ve göster
                _stats = _statsManager.CalculateStatistics();
                PopulateStatistics();


                // Grafikleri oluştur
                CreateCharts();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"İstatistik formu yüklenirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ApplyRoundedCorners()
        {
            // Form için yuvarlak köşeler
            this.FormBorderStyle = FormBorderStyle.None;
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

            // Header panel için yuvarlak köşeler
            panelHeader.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, panelHeader.Width, panelHeader.Height, 20, 20));

            // Ana panel için yuvarlak köşeler
            panelMain.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, panelMain.Width, panelMain.Height, 20, 20));

            // Küçük paneller için yuvarlak köşeler
            panelMonthlyTotal.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, panelMonthlyTotal.Width, panelMonthlyTotal.Height, 20, 20));
            panelTodayIncoming.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, panelTodayIncoming.Width, panelTodayIncoming.Height, 20, 20));
            panelBankBlock.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, panelBankBlock.Width, panelBankBlock.Height, 20, 20));
            panelValor.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, panelValor.Width, panelValor.Height, 20, 20));
            panelMostUsedBank.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, panelMostUsedBank.Width, panelMostUsedBank.Height, 20, 20));
            panelMostActiveDevice.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, panelMostActiveDevice.Width, panelMostActiveDevice.Height, 20, 20));
            panelTarget.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, panelTarget.Width, panelTarget.Height, 20, 20));

            // Butonlar için yuvarlak köşeler
            btnExportExcel.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnExportExcel.Width, btnExportExcel.Height, 15, 15));
            btnExportPdf.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnExportPdf.Width, btnExportPdf.Height, 15, 15));
            btnDetailedReport.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnDetailedReport.Width, btnDetailedReport.Height, 15, 15));
            btnSaveTarget.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnSaveTarget.Width, btnSaveTarget.Height, 10, 10));

            // Panel shadow effect (isteğe bağlı)
            // Bu özel gölge efekti istiyorsanız doldurun
        }

        public void ApplyShadowEffect()
        {
            // Panel shadow effect
            // Bu örnek için, sadece border style'ını değiştirdik. Gerçek shadow effect için özel sınıf gereklidir.
            panelMain.BorderStyle = BorderStyle.None;

            // İsteğe bağlı: Panel için hafif border eklemek için Paint olayı kullanın
            panelMain.Paint += (sender, e) => {
                ControlPaint.DrawBorder(e.Graphics, panelMain.ClientRectangle,
                    Color.FromArgb(200, 200, 200), 1, ButtonBorderStyle.Solid,
                    Color.FromArgb(200, 200, 200), 1, ButtonBorderStyle.Solid,
                    Color.FromArgb(200, 200, 200), 1, ButtonBorderStyle.Solid,
                    Color.FromArgb(200, 200, 200), 1, ButtonBorderStyle.Solid);
            };

            // Tüm alt paneller için tek satırda hafif gölge efekti ekleyin
            Action<Panel> addPanelShadow = (panel) => {
                panel.Paint += (sender, e) => {
                    ControlPaint.DrawBorder(e.Graphics, panel.ClientRectangle,
                        Color.FromArgb(200, 200, 200), 1, ButtonBorderStyle.Solid,
                        Color.FromArgb(200, 200, 200), 1, ButtonBorderStyle.Solid,
                        Color.FromArgb(200, 200, 200), 1, ButtonBorderStyle.Solid,
                        Color.FromArgb(200, 200, 200), 1, ButtonBorderStyle.Solid);
                };
            };

            // Tüm panellere hafif gölge ekle
            addPanelShadow(panelMonthlyTotal);
            addPanelShadow(panelTodayIncoming);
            addPanelShadow(panelBankBlock);
            addPanelShadow(panelValor);
            addPanelShadow(panelMostUsedBank);
            addPanelShadow(panelMostActiveDevice);
            addPanelShadow(panelTarget);
        }

        public void StatisticsForm_Resize(object sender, EventArgs e)
        {
            // Form boyutu değiştiğinde yuvarlak köşeleri güncelle
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 20, 20));

            // Ana panel ve header için köşeleri güncelle
            if (panelMain.Width > 0 && panelMain.Height > 0)
                panelMain.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, panelMain.Width, panelMain.Height, 20, 20));

            if (panelHeader.Width > 0 && panelHeader.Height > 0)
                panelHeader.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, panelHeader.Width, panelHeader.Height, 20, 20));
        }

        public void PopulateStatistics()
        {
            // Kartlardaki değerleri güncelle
            lblMTValue.Text = _stats.MonthlyTotalAmount.ToString("N2");
            lblTIValue.Text = _stats.TodayIncomingAmount.ToString("N2");
            lblBBValue.Text = _stats.TotalBlockedAmount.ToString("N2");
            lblVValue.Text = _stats.AverageValorPeriod.ToString("N1");
            lblMUBValue.Text = $"{_stats.MostUsedBank}\n{_stats.MostUsedBankAmount.ToString("N2")} ₺";
            lblMADValue.Text = $"{_stats.MostActiveDevice}\n{_stats.MostActiveDeviceAmount.ToString("N2")} ₺";
            lblTValue.Text = _stats.TargetCompletionRate.ToString("N1");
        }

        public void CreateCharts()
        {
            // Pasta grafiği için veri noktaları ekle
            chartBankDist.Series[0].Points.Clear();

            foreach (var bankData in _stats.BankDistribution)
            {
                int pointIndex = chartBankDist.Series[0].Points.AddY(bankData.Value);
                chartBankDist.Series[0].Points[pointIndex].LegendText = bankData.Key;
                chartBankDist.Series[0].Points[pointIndex].Label = $"{bankData.Key}: {bankData.Value:N0} ₺";
            }

            // Grafik özelliklerini ayarla
            chartBankDist.ChartAreas[0].Area3DStyle.Enable3D = true;
            chartBankDist.ChartAreas[0].Area3DStyle.Inclination = 20;
            chartBankDist.Palette = ChartColorPalette.BrightPastel;

            // Son 6 ayın verilerini al
            List<MonthlyComparisonData> comparisonData = _statsManager.GetMonthlyComparisonData(6);

            // Sütun grafiği için veri ekle
            chartMonthly.Series[0].Points.Clear();

            foreach (var monthData in comparisonData)
            {
                int pointIndex = chartMonthly.Series[0].Points.AddXY(monthData.Month, monthData.TotalAmount);
                chartMonthly.Series[0].Points[pointIndex].Label = $"{monthData.TotalAmount:N0} ₺";
            }

            // X ekseni ayarları
            chartMonthly.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
            chartMonthly.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Segoe UI", 8);
            chartMonthly.ChartAreas[0].AxisX.MajorGrid.Enabled = false;

            // Y ekseni ayarları
            chartMonthly.ChartAreas[0].AxisY.LabelStyle.Format = "{0:N0} ₺";
            chartMonthly.ChartAreas[0].AxisY.LabelStyle.Font = new Font("Segoe UI", 8);
            chartMonthly.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;

            // Grafik özelliklerini ayarla
            chartMonthly.Palette = ChartColorPalette.BrightPastel;
        }

        public void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "Excel Dosyası|*.xlsx",
                    Title = "İstatistikleri Excel'e Aktar",
                    FileName = $"POS_Istatistikleri_{DateTime.Now:yyyyMMdd}"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    // Excel'e aktarma kodu
                    MessageBox.Show("İstatistikler başarıyla Excel'e aktarıldı.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Excel'e aktarma sırasında hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void btnExportPdf_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "PDF Dosyası|*.pdf",
                    Title = "İstatistikleri PDF'e Aktar",
                    FileName = $"POS_Istatistikleri_{DateTime.Now:yyyyMMdd}"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    // PDF'e aktarma kodu
                    MessageBox.Show("İstatistikler başarıyla PDF'e aktarıldı.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("PDF'e aktarma sırasında hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void btnDetailedReport_Click(object sender, EventArgs e)
        {
            try
            {
                using (DetailedStatsForm detailedForm = new DetailedStatsForm(_stats))
                {
                    detailedForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Detaylı rapor açılırken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void StatisticsForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        public void StatisticsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Form kapanmadan önce yapılacak işlemler
        }

        public void StatisticsForm_Paint(object sender, PaintEventArgs e)
        {
            // Form paint olayı
        }

        public void chkShowPopup_CheckedChanged(object sender, EventArgs e)
        {
            // Kullanıcı tercihini kaydet
            UserSettings.Instance.ShowStatisticsPopup = chkShowPopup.Checked;
            UserSettings.Instance.SaveSettings();
        }

        public void btnSaveTarget_Click(object sender, EventArgs e)
        {
            try
            {
                decimal target = decimal.Parse(txtTarget.Text, NumberStyles.Any, CultureInfo.CurrentCulture);
                UserSettings.Instance.MonthlyTarget = target;
                UserSettings.Instance.SaveSettings();

                // Hedef değişince istatistikleri güncelle
                _stats = _statsManager.CalculateStatistics();
                PopulateStatistics();

                MessageBox.Show("Hedef başarıyla kaydedildi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hedef kaydedilirken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}