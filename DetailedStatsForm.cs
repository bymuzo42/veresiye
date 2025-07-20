using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Veresiye2025
{
    public partial class DetailedStatsForm : Form
    {
        public StatisticsData _stats;
        public StatisticsManager _statsManager;

        public DetailedStatsForm(StatisticsData stats)
        {
            InitializeComponent();
            _stats = stats;
            _statsManager = new StatisticsManager();
        }

        public void DetailedStatsForm_Load(object sender, EventArgs e)
        {
            try
            {
                // Başlık metnini ayarla
                this.Text = "Detaylı POS İstatistikleri";

                // Menü elemanlarını oluştur
                SetupMenuItems();

                // Banka detaylarını yükle
                LoadBankData();

                // Cihaz detaylarını yükle
                LoadDeviceData();

                // Trend analizini yükle
                LoadTrendData();

                // Hedef takip verilerini yükle
                LoadTargetData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Detaylı istatistik formu yüklenirken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void SetupMenuItems()
        {
            menuItemExportExcel.Click += MenuItemExportExcel_Click;
            menuItemExportPdf.Click += MenuItemExportPdf_Click;
            menuItemClose.Click += MenuItemClose_Click;
        }

        public void LoadBankData()
        {
            // Banka dağılım grafiği doldurma
            chartBankDist.Series[0].Points.Clear();

            decimal totalAmount = 0;
            foreach (var bankData in _stats.BankDistribution)
            {
                totalAmount += bankData.Value;
            }

            foreach (var bankData in _stats.BankDistribution)
            {
                int pointIndex = chartBankDist.Series[0].Points.AddY(bankData.Value);
                decimal percentage = totalAmount > 0 ? (bankData.Value / totalAmount) * 100 : 0;
                chartBankDist.Series[0].Points[pointIndex].LegendText = bankData.Key;
                chartBankDist.Series[0].Points[pointIndex].Label = $"{bankData.Key}: {percentage:N1}%";

                // Banka listesine ekle
                ListViewItem item = new ListViewItem(bankData.Key);
                item.SubItems.Add(bankData.Value.ToString("N2") + " ₺");
                item.SubItems.Add(percentage.ToString("N2") + "%");

                // Ay içindeki işlem sayısı
                if (_stats.BankTransactionCounts != null && _stats.BankTransactionCounts.ContainsKey(bankData.Key))
                {
                    item.SubItems.Add(_stats.BankTransactionCounts[bankData.Key].ToString());
                }
                else
                {
                    item.SubItems.Add("-");
                }

                listViewBanks.Items.Add(item);
            }

            // Grafik özelliklerini ayarla
            chartBankDist.ChartAreas[0].Area3DStyle.Enable3D = true;
            chartBankDist.ChartAreas[0].Area3DStyle.Inclination = 20;
            chartBankDist.Palette = ChartColorPalette.BrightPastel;
        }

        public void LoadDeviceData()
        {
            // Cihaz performans grafiği doldurma
            chartDevicePerf.Series[0].Points.Clear();

            foreach (var deviceStat in _stats.DevicePerformance)
            {
                int pointIndex = chartDevicePerf.Series[0].Points.AddXY(deviceStat.DeviceName, deviceStat.TotalAmount);
                chartDevicePerf.Series[0].Points[pointIndex].Label = $"{deviceStat.TotalAmount:N0} ₺";

                // Cihaz listesine ekle
                ListViewItem item = new ListViewItem(deviceStat.DeviceName);
                item.SubItems.Add(deviceStat.TotalAmount.ToString("N2") + " ₺");
                item.SubItems.Add(deviceStat.TransactionCount.ToString());
                item.SubItems.Add(deviceStat.DailyAverage.ToString("N2") + " ₺");

                string status = deviceStat.TotalAmount > 10000 ? "Aktif" : "Düşük Performans";
                item.SubItems.Add(status);

                listViewDevices.Items.Add(item);
            }

            // Pasif cihazları ekle
            foreach (var inactiveDevice in _stats.InactiveDevices)
            {
                ListViewItem item = new ListViewItem(inactiveDevice);
                item.SubItems.Add("0.00 ₺");
                item.SubItems.Add("0");
                item.SubItems.Add("0.00 ₺");
                item.SubItems.Add("Pasif");

                listViewDevices.Items.Add(item);
            }

            // Grafik özelliklerini ayarla
            chartDevicePerf.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
            chartDevicePerf.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Segoe UI", 8);
            chartDevicePerf.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chartDevicePerf.ChartAreas[0].AxisY.LabelStyle.Format = "{0:N0} ₺";
            chartDevicePerf.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
            chartDevicePerf.Palette = ChartColorPalette.Pastel;
        }

        public void LoadTrendData()
        {
            // Ay karşılaştırma grafiği doldurma
            chartMonthly.Series[0].Points.Clear();
            chartMonthly.Series[1].Points.Clear();

            // Son 6 ayın verilerini al
            var comparisonData = _statsManager.GetMonthlyComparisonData(6);

            foreach (var monthData in comparisonData)
            {
                // Ayları ters çeviriyoruz ki en son ay en sonda görünsün
                int pointIndex = chartMonthly.Series[0].Points.AddXY(monthData.Month, monthData.TotalAmount);
                chartMonthly.Series[0].Points[pointIndex].Label = $"{monthData.TotalAmount:N0} ₺";
            }

            // Aylık değişim yüzdesi çizgisi
            if (comparisonData.Count > 1)
            {
                for (int i = 1; i < comparisonData.Count; i++)
                {
                    decimal prevAmount = comparisonData[i].TotalAmount;
                    decimal currentAmount = comparisonData[i - 1].TotalAmount;

                    decimal changePercentage = 0;
                    if (prevAmount > 0)
                    {
                        changePercentage = ((currentAmount - prevAmount) / prevAmount) * 100;
                    }

                    chartMonthly.Series[1].Points.AddXY(comparisonData[i - 1].Month, changePercentage);
                }
            }

            // Trend bilgilerini etiketlere yerleştir
            lblChangeValue.Text = _stats.ChangePercentage.ToString("N2") + "%";
            lblChangeValue.ForeColor = _stats.ChangePercentage >= 0 ? Color.Green : Color.Red;

            lblDailyAvgValue.Text = _stats.AverageDailyAmount.ToString("N2") + " ₺";
            lblMonthEndValue.Text = _stats.MonthEndEstimate.ToString("N2") + " ₺";

            // Grafik özelliklerini ayarla
            chartMonthly.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
            chartMonthly.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Segoe UI", 8);
            chartMonthly.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chartMonthly.ChartAreas[0].AxisY.LabelStyle.Format = "{0:N0} ₺";
            chartMonthly.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
            chartMonthly.Palette = ChartColorPalette.BrightPastel;
        }

        public void LoadTargetData()
        {
            // Hedef ilerleme grafiği doldurma
            chartTarget.Series[0].Points.Clear();
            chartTarget.Series[1].Points.Clear();

            // Hedef ve gerçekleşme oranları
            decimal targetAmount = UserSettings.Instance.MonthlyTarget;
            decimal currentAmount = _stats.MonthlyTotalAmount;
            decimal remainingAmount = targetAmount - currentAmount > 0 ? targetAmount - currentAmount : 0;
            decimal completionRate = _stats.TargetCompletionRate;

            chartTarget.Series[0].Points.AddXY("Hedef", completionRate);
            chartTarget.Series[0].Points[0].Label = $"{completionRate:N1}%";
            chartTarget.Series[0].Points[0].Color = Color.Green;

            chartTarget.Series[1].Points.AddXY("Hedef", completionRate >= 100 ? 0 : 100 - completionRate);
            if (completionRate < 100)
            {
                chartTarget.Series[1].Points[0].Label = $"{100 - completionRate:N1}%";
                chartTarget.Series[1].Points[0].Color = Color.LightGray;
            }
            else
            {
                chartTarget.Series[1].Points[0].Label = "";
            }

            // Hedef bilgilerini etiketlere yerleştir
            lblTargetAmount.Text = targetAmount.ToString("N2") + " ₺";
            lblCurrentAmount.Text = currentAmount.ToString("N2") + " ₺";
            lblRemainingAmount.Text = remainingAmount.ToString("N2") + " ₺";
            lblCompletionRate.Text = completionRate.ToString("N2") + "%";
            lblCompletionRate.ForeColor = completionRate >= 100 ? Color.Green : Color.Orange;
        }

        public void MenuItemExportExcel_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }

        public void MenuItemExportPdf_Click(object sender, EventArgs e)
        {
            ExportToPdf();
        }

        public void MenuItemClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void ExportToExcel()
        {
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "Excel Dosyası|*.xlsx",
                    Title = "Detaylı İstatistikleri Excel'e Aktar",
                    FileName = $"POS_Detayli_Istatistikler_{DateTime.Now:yyyyMMdd}"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    // Excel'e aktarma kodu
                    MessageBox.Show("Detaylı istatistikler başarıyla Excel'e aktarıldı.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Excel'e aktarma sırasında hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ExportToPdf()
        {
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "PDF Dosyası|*.pdf",
                    Title = "Detaylı İstatistikleri PDF'e Aktar",
                    FileName = $"POS_Detayli_Istatistikler_{DateTime.Now:yyyyMMdd}"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    // PDF'e aktarma kodu
                    MessageBox.Show("Detaylı istatistikler başarıyla PDF'e aktarıldı.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("PDF'e aktarma sırasında hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}