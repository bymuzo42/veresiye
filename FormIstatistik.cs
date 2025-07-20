using System;
using System.Data.SQLite;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Veresiye2025
{
    public partial class FormIstatistik : Form
    {
        public string cariBilgisi;
        public string connectionString = "Data Source=veresiye.db;Version=3;";
        // Ana mavi tonu (#2196F3) ve diğer renkler
        public Color primaryColor = Color.FromArgb(33, 150, 243); // Ana mavi
        public Color successColor = Color.FromArgb(0, 150, 136);  // Yeşil
        public Color dangerColor = Color.FromArgb(229, 57, 53);   // Kırmızı
        public Color warningColor = Color.FromArgb(255, 152, 0);  // Turuncu

        public FormIstatistik(string cariBilgisi)
        {
            InitializeComponent();
            this.cariBilgisi = cariBilgisi;
            this.Load += new EventHandler(FormIstatistik_Load);
        }

        public void FormIstatistik_Load(object sender, EventArgs e)
        {
            ApplyModernStyle();
            cmbTarihAraligi.SelectedIndex = 1; // Varsayılan olarak "Son 6 Ay"
            YuklemeYap();
        }

        public void ApplyModernStyle()
        {
            // Form genel ayarları
            this.BackColor = Color.White;
            this.Font = new Font("Segoe UI", 9F);
            this.ClientSize = new Size(908, 518);

            // Panel stilleri
            
            pnlUst.Height = 50;

            // ComboBox stili
            cmbTarihAraligi.FlatStyle = FlatStyle.Flat;
            cmbTarihAraligi.BackColor = primaryColor;
            cmbTarihAraligi.ForeColor = Color.White;
            cmbTarihAraligi.Font = new Font("Segoe UI", 9F);
            cmbTarihAraligi.DropDownStyle = ComboBoxStyle.DropDownList;

            

            // Özet etiketi stili
            lblOzet.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblOzet.ForeColor = dangerColor; // Varsayılan olarak kırmızı (borçlu durumu)

            // DataGridView stili
            dgvIstatistik.BorderStyle = BorderStyle.None;
            dgvIstatistik.BackgroundColor = Color.White;
            dgvIstatistik.GridColor = Color.FromArgb(230, 230, 230);
            dgvIstatistik.RowTemplate.Height = 35;
            dgvIstatistik.ColumnHeadersHeight = 40;
            dgvIstatistik.EnableHeadersVisualStyles = false;
            dgvIstatistik.ColumnHeadersDefaultCellStyle.BackColor = primaryColor;
            dgvIstatistik.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvIstatistik.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvIstatistik.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 249, 255);
            dgvIstatistik.DefaultCellStyle.SelectionBackColor = Color.FromArgb(66, 165, 245);
            dgvIstatistik.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvIstatistik.RowsDefaultCellStyle.Padding = new Padding(5);

            // Grafik stili
            chartCiro.BackColor = Color.White;
            chartCiro.BorderlineColor = Color.White;
            chartCiro.BorderSkin.SkinStyle = BorderSkinStyle.None;

            // ChartArea
            chartCiro.ChartAreas[0].BackColor = Color.White;
            chartCiro.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.FromArgb(230, 230, 230);
            chartCiro.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.FromArgb(230, 230, 230);
            chartCiro.ChartAreas[0].AxisX.LineColor = Color.FromArgb(200, 200, 200);
            chartCiro.ChartAreas[0].AxisY.LineColor = Color.FromArgb(200, 200, 200);
            chartCiro.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Segoe UI", 8F);
            chartCiro.ChartAreas[0].AxisY.LabelStyle.Font = new Font("Segoe UI", 8F);
            chartCiro.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.FromArgb(100, 100, 100);
            chartCiro.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.FromArgb(100, 100, 100);

            // Legend
            chartCiro.Legends[0].BackColor = Color.Transparent;
            chartCiro.Legends[0].Font = new Font("Segoe UI", 8F);
            chartCiro.Legends[0].TitleFont = new Font("Segoe UI", 9F, FontStyle.Bold);

            // Seri renkleri (resimdeki gibi)
            chartCiro.Series["Ciro"].Color = Color.FromArgb(33, 150, 243); // Mavi
            chartCiro.Series["Borç"].Color = Color.FromArgb(229, 57, 53);  // Kırmızı
            chartCiro.Series["Tahsilat"].Color = Color.FromArgb(0, 150, 136); // Yeşil

            // Grafik tipleri
            chartCiro.Series["Ciro"].ChartType = SeriesChartType.Line;
            chartCiro.Series["Borç"].ChartType = SeriesChartType.Column;
            chartCiro.Series["Tahsilat"].ChartType = SeriesChartType.Column;

            // Çizgi grafiğini kalınlaştır
            chartCiro.Series["Ciro"].BorderWidth = 3;
            chartCiro.Series["Ciro"].MarkerStyle = MarkerStyle.Circle;
            chartCiro.Series["Ciro"].MarkerSize = 8;
        }

        public void YuklemeYap()
        {
            dgvIstatistik.Rows.Clear();
            chartCiro.Series["Ciro"].Points.Clear();
            chartCiro.Series["Borç"].Points.Clear();
            chartCiro.Series["Tahsilat"].Points.Clear();

            int ayLimit = 0;
            switch (cmbTarihAraligi.SelectedIndex)
            {
                case 0: ayLimit = 3; break;
                case 1: ayLimit = 6; break;
                case 2: ayLimit = 12; break;
                default: ayLimit = 0; break; // 0 = tüm zamanlar
            }

            string whereClause = ayLimit > 0
                ? $" AND tarih >= date('now', '-{ayLimit} month', 'start of month')"
                : "";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = @"
                    SELECT strftime('%Y-%m', tarih) AS Ay,
                           SUM(borc) AS ToplamBorc,
                           SUM(tahsilat) AS ToplamTahsilat
                    FROM cari_hareketleri
                    WHERE cari_kodu = @CariKodu" + whereClause + @"
                    GROUP BY strftime('%Y-%m', tarih)
                    ORDER BY Ay";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CariKodu", cariBilgisi);

                    double toplamGenel = 0;
                    double? oncekiCiro = null;
                    int satirSayisi = 0;

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            satirSayisi++;
                            string yilAy = reader["Ay"].ToString();
                            double toplamBorc = reader["ToplamBorc"] == DBNull.Value ? 0 : Convert.ToDouble(reader["ToplamBorc"]);
                            double toplamTahsilat = reader["ToplamTahsilat"] == DBNull.Value ? 0 : Convert.ToDouble(reader["ToplamTahsilat"]);
                            double aylikCiro = toplamTahsilat - toplamBorc;
                            double ciroDegisimi = 0;
                            string ciroArtisi = "Aynı";

                            if (oncekiCiro != null)
                            {
                                ciroDegisimi = aylikCiro - oncekiCiro.Value;

                                if (ciroDegisimi > 0)
                                    ciroArtisi = "Artış 📈";
                                else if (ciroDegisimi < 0)
                                    ciroArtisi = "Düşüş 📉";
                            }

                            oncekiCiro = aylikCiro;
                            toplamGenel += aylikCiro;

                            // DataGridView'e satır ekle
                            int idx = dgvIstatistik.Rows.Add(GetAyAdı(yilAy),
                                String.Format("{0:N2}", toplamBorc),
                                String.Format("{0:N2}", toplamTahsilat),
                                String.Format("{0:N2}", aylikCiro),
                                String.Format("{0:N2}", ciroDegisimi),
                                ciroArtisi);

                            // Ciro negatifse kırmızı, pozitifse yeşil
                            if (aylikCiro < 0)
                                dgvIstatistik.Rows[idx].Cells["AylikCiro"].Style.ForeColor = dangerColor;
                            else if (aylikCiro > 0)
                                dgvIstatistik.Rows[idx].Cells["AylikCiro"].Style.ForeColor = successColor;

                            // Ciro değişimi negatifse kırmızı, pozitifse yeşil
                            if (ciroDegisimi < 0)
                                dgvIstatistik.Rows[idx].Cells["CiroDegisimi"].Style.ForeColor = dangerColor;
                            else if (ciroDegisimi > 0)
                                dgvIstatistik.Rows[idx].Cells["CiroDegisimi"].Style.ForeColor = successColor;

                            // İlk satırsa seçili yap
                            if (satirSayisi == 1)
                                dgvIstatistik.Rows[idx].Selected = true;

                            // Grafik verileri ekle
                            chartCiro.Series["Ciro"].Points.AddXY(GetAyAdı(yilAy), aylikCiro);
                            chartCiro.Series["Borç"].Points.AddXY(GetAyAdı(yilAy), toplamBorc);
                            chartCiro.Series["Tahsilat"].Points.AddXY(GetAyAdı(yilAy), toplamTahsilat);
                        }
                    }

                    // Özet bilgisini güncelle
                    string durumMetni = toplamGenel < 0 ? "borçlu" : "alacaklı";
                    lblOzet.Text = $"Toplam {satirSayisi} ay | Toplam Durum: {Math.Abs(toplamGenel):N2} ₺ {durumMetni}";
                    lblOzet.ForeColor = toplamGenel < 0 ? dangerColor : successColor;

                    // Eğer veri yoksa uyarı göster
                    if (satirSayisi == 0)
                    {
                        lblOzet.Text = "Bu tarih aralığında veri bulunamadı.";
                        lblOzet.ForeColor = warningColor;
                    }
                }
            }

            // Grafik ayarlarını optimize et
            OptimizeChartAppearance();
        }

        public void OptimizeChartAppearance()
        {
            // Eğer veri varsa grafik görünümünü optimize et
            if (chartCiro.Series["Ciro"].Points.Count > 0)
            {
                // X ekseni etiketlerini açılı göster
                chartCiro.ChartAreas[0].AxisX.Interval = 1;
                chartCiro.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
                chartCiro.ChartAreas[0].AxisX.LabelStyle.IsStaggered = false;

                // Y ekseni için otomatik ölçeklendirme
                chartCiro.ChartAreas[0].RecalculateAxesScale();

                // Y eksenini genişlet
                if (chartCiro.ChartAreas[0].AxisY.Maximum > 0)
                    chartCiro.ChartAreas[0].AxisY.Maximum *= 1.1;
                if (chartCiro.ChartAreas[0].AxisY.Minimum < 0)
                    chartCiro.ChartAreas[0].AxisY.Minimum *= 1.1;

                // Sütun grafikleri yan yana değil, üst üste gelsin
                chartCiro.Series["Borç"]["DrawSideBySide"] = "false";
                chartCiro.Series["Tahsilat"]["DrawSideBySide"] = "false";

                // Sütun genişlikleri
                chartCiro.Series["Borç"]["PointWidth"] = "0.6";
                chartCiro.Series["Tahsilat"]["PointWidth"] = "0.6";
            }
        }

        public string GetAyAdı(string yilAy)
        {
            string[] aylar = { "Ocak", "Şubat", "Mart", "Nisan", "Mayıs", "Haziran", "Temmuz", "Ağustos", "Eylül", "Ekim", "Kasım", "Aralık" };
            string[] parcalar = yilAy.Split('-');

            if (parcalar.Length != 2) return yilAy;

            if (int.TryParse(parcalar[1], out int ayIndex) && ayIndex >= 1 && ayIndex <= 12)
                return $"{parcalar[0]} {aylar[ayIndex - 1]}";

            return yilAy;
        }

        public void cmbTarihAraligi_SelectedIndexChanged(object sender, EventArgs e)
        {
            YuklemeYap();
        }

        

        public void PrintDataGridView()
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                PrintDocument printDocument = new PrintDocument();
                printDocument.PrintPage += new PrintPageEventHandler(PrintPage);
                printDocument.Print();
            }
        }

        public void PrintPage(object sender, PrintPageEventArgs e)
        {
            // Form başlığını yazdır
            string baslik = "Cari Hesap İstatistikleri";
            Font baslikFont = new Font("Segoe UI", 18, FontStyle.Bold);
            SizeF baslikBoyut = e.Graphics.MeasureString(baslik, baslikFont);
            e.Graphics.DrawString(baslik, baslikFont, new SolidBrush(Color.Black),
                (e.PageBounds.Width - baslikBoyut.Width) / 2, 20);

            // Özet bilgiyi yazdır
            Font ozetFont = new Font("Segoe UI", 11);
            SolidBrush ozetFirca = new SolidBrush(lblOzet.ForeColor);
            e.Graphics.DrawString(lblOzet.Text, ozetFont, ozetFirca, 50, 60);

            // Tablo başlıklarını yazdır
            Font baslikCellFont = new Font("Segoe UI", 10, FontStyle.Bold);
            int y = 100;
            int x = 50;
            int genislik = 120;
            int yukseklik = 30;

            for (int i = 0; i < dgvIstatistik.Columns.Count; i++)
            {
                // Başlık arkaplanı
                e.Graphics.FillRectangle(new SolidBrush(primaryColor), x, y, genislik, yukseklik);
                e.Graphics.DrawRectangle(new Pen(Color.Black), x, y, genislik, yukseklik);
                e.Graphics.DrawString(dgvIstatistik.Columns[i].HeaderText, baslikCellFont,
                    new SolidBrush(Color.White), x + 5, y + 5);
                x += genislik;
            }

            // Tablo hücrelerini yazdır
            Font cellFont = new Font("Segoe UI", 9);
            y += yukseklik;

            for (int satir = 0; satir < dgvIstatistik.Rows.Count; satir++)
            {
                x = 50;

                // Satır arkaplanı (alternatif renklendirme)
                if (satir % 2 == 0)
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(240, 249, 255)),
                        x, y, genislik * dgvIstatistik.Columns.Count, yukseklik);

                for (int sutun = 0; sutun < dgvIstatistik.Columns.Count; sutun++)
                {
                    e.Graphics.DrawRectangle(new Pen(Color.Black), x, y, genislik, yukseklik);

                    // Özel renklendirme (Ciro ve CiroDegisimi sütunları için)
                    SolidBrush textBrush = new SolidBrush(Color.Black); // Varsayılan
                    if (sutun == 3) // AylikCiro sütunu
                    {
                        double ciro = 0;
                        string deger = dgvIstatistik.Rows[satir].Cells[sutun].Value?.ToString() ?? "";
                        deger = deger.Replace(".", "").Replace(",", ".");
                        if (double.TryParse(deger, out ciro))
                        {
                            if (ciro < 0)
                                textBrush = new SolidBrush(dangerColor);
                            else if (ciro > 0)
                                textBrush = new SolidBrush(successColor);
                        }
                    }
                    else if (sutun == 4) // CiroDegisimi sütunu
                    {
                        double degisim = 0;
                        string deger = dgvIstatistik.Rows[satir].Cells[sutun].Value?.ToString() ?? "";
                        deger = deger.Replace(".", "").Replace(",", ".");
                        if (double.TryParse(deger, out degisim))
                        {
                            if (degisim < 0)
                                textBrush = new SolidBrush(dangerColor);
                            else if (degisim > 0)
                                textBrush = new SolidBrush(successColor);
                        }
                    }

                    // Metin çiz
                    e.Graphics.DrawString(
                        dgvIstatistik.Rows[satir].Cells[sutun].Value?.ToString() ?? "",
                        cellFont, textBrush, x + 5, y + 5);

                    x += genislik;
                }
                y += yukseklik;
            }

            // Grafik resmini yazdır (daha küçük boyutta)
            if (chartCiro.Series[0].Points.Count > 0)
            {
                Bitmap bmp = new Bitmap(chartCiro.Width, chartCiro.Height);
                chartCiro.DrawToBitmap(bmp, chartCiro.ClientRectangle);

                // Grafiği biraz daha küçük ölçeklendir
                float genislikOlcek = 0.6f;
                e.Graphics.DrawImage(bmp,
                    new Rectangle(50, y + 20,
                        (int)(bmp.Width * genislikOlcek),
                        (int)(bmp.Height * genislikOlcek)));
            }
        }

        public void yazdirrapor_Click(object sender, EventArgs e)
        {
            PrintDataGridView();
        }
    }
}