using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Veresiye2025
{
    public partial class Hareketler : Form
    {
        public string connectionString = "Data Source=veresiye.db;Version=3;";
        // Hareket verilerini tutacağımız koleksiyon
        public List<Hareket> hareketData = new List<Hareket>();

        // Form taşıma için gerekli DLL import
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        public extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        public extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        // Yuvarlatılmış köşeler için API
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        public static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );

        public Hareketler()
        {
            InitializeComponent();

            // Form köşelerini yuvarla (20px)
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

            // Form başlık paneli için olay ekle (form taşıma için)
            pnlHeader.MouseDown += PnlHeader_MouseDown;
            lblFormTitle.MouseDown += PnlHeader_MouseDown;

            InitializeContextMenu();
            // DataBindingComplete olayını tanımla
            hareketgrid.DataBindingComplete += Hareketgrid_DataBindingComplete;
            // Form yüklenirken bugünün tarihini ayarla
            ilk.Value = DateTime.Today;
            son.Value = DateTime.Today;
            // F3 kısayolunu etkinleştir
            this.KeyPreview = true;
            this.KeyDown += Hareketler_KeyDown;
            // Tarih seçilince sorgula butonunu aktif yap
            ilk.ValueChanged += TarihSecildi;
            son.ValueChanged += TarihSecildi;
            // ComboBox seçim değişikliği durumunda sorgulama işlemini otomatik yap
            tursecenek.SelectedIndexChanged += TurSecenek_SelectedIndexChanged;
            // Başlangıçta grid'i doldur
            BugununHareketleriniGetir();
            // Başlangıçta hiçbir satırın seçili olmamasını sağla
            hareketgrid.ClearSelection();
            // "borctext" ve "alacaktext" müdahaleye kapalı
            borctext.ReadOnly = true;
            alacaktext.ReadOnly = true;
            // "tursecenek" ComboBox seçeneklerini ekle
            tursecenek.Items.Add("Borç Dekontu");
            tursecenek.Items.Add("Tahsilat Dekontu");
            tursecenek.Items.Add("Tümü"); // Tümü seçeneği eklenerek tüm veriler gösterilebilir
            tursecenek.SelectedIndex = 2; // Varsayılan olarak "Tümü" seçili olsun
            // PrintDocument tanımlama
            this.printDocument = new System.Drawing.Printing.PrintDocument();
            this.printDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument_PrintPage);
            // PrintPreviewDialog tanımlama
            this.printPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
            this.printPreviewDialog.Document = this.printDocument; // PrintDocument ile ilişkilendirme
            this.printPreviewDialog.StartPosition = FormStartPosition.CenterScreen;
            this.printPreviewDialog.Size = new Size(1200, 800); // Büyük ekran boyutu
            this.hrktyazdir.Click += new System.EventHandler(this.hrktyazdir_Click);

            // ESC tuşuyla formu kapatma
            this.KeyPreview = true;
            this.KeyDown += (s, e) => {
                if (e.KeyCode == Keys.Escape)
                    this.Close();
            };
        }

        // Form taşıma için olay
        public void PnlHeader_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        // ComboBox seçimi değiştiğinde verileri filtrele
        public void TurSecenek_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Tarihleri al
                DateTime ilkTarih = ilk.Value.Date;
                DateTime sonTarih = son.Value.Date;
                // Tür seçimini al
                string tur = tursecenek.SelectedItem?.ToString() == "Tümü" ? null : tursecenek.SelectedItem?.ToString();
                // Verileri getirme işlemi
                VerileriGetir(ilkTarih, sonTarih, tur, hareketunvan.Text.Trim());
            }
            catch (Exception ex)
            {
                // Hata durumunda detaylı bilgi göster
                MessageBox.Show($"Hata: {ex.Message}\n{ex.StackTrace}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Hareketgrid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // Satır renklendirme işlemini burada yap
            SatirlariRenklendir();
            hareketgrid.ClearSelection(); // Hiçbir satırın seçili olmamasını sağla
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            InitializeDataGridView();  // DataGridView başlatma işlemi
            // DataGridView'in düzgün bir şekilde yüklendiğinden emin olun
            if (hareketgrid != null)
            {
                DataGridViewStilDuzenle();  // Stil düzenlemesini yap
            }
            else
            {
                MessageBox.Show("DataGridView 'hareketgrid' yüklenemedi!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // İlk tarih ayarları
            ilk.Format = DateTimePickerFormat.Custom;
            ilk.CustomFormat = "yyyy-MM-dd";
            ilk.ShowUpDown = true;
            // Son tarih ayarları
            son.Format = DateTimePickerFormat.Custom;
            son.CustomFormat = "yyyy-MM-dd";
            son.ShowUpDown = true;
            hareketgrid.ClearSelection();
            SatirlariRenklendir();
        }

        public void TarihSecildi(object sender, EventArgs e)
        {
            // Tarih seçildiğinde sorgula butonunu aktif yap
            hrktsorgula.Enabled = true;
        }

        public void Hareketler_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
            {
                BugununHareketleriniGetir();
                e.Handled = true;
                hareketgrid.ClearSelection();
            }
        }

        public void BugununHareketleriniGetir()
        {
            DateTime bugun = DateTime.Today;
            // İlk ve Son tarihleri bugünkü tarih olarak ayarla
            ilk.Value = bugun;
            son.Value = bugun;
            // Verileri getir
            VerileriGetir(bugun, bugun, null, null);
            // Sorgula butonunu pasif yap
            hrktsorgula.Enabled = false;
            // Hiçbir satırın seçili olmamasını sağla
            hareketgrid.ClearSelection();
            // Satır renklendirme işlemini uygula
            SatirlariRenklendir();
            tursecenek.SelectedIndex = tursecenek.Items.IndexOf("Tümü"); // "Tümü" seçili yap
        }

        public void SatirlariRenklendir()
        {
            foreach (DataGridViewRow row in hareketgrid.Rows)
            {
                if (row.Cells["Tür"].Value?.ToString() == "Tahsilat Dekontu")
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        cell.Style.ForeColor = Color.Blue;
                    }
                }
                else
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        cell.Style.ForeColor = Color.Black; // Varsayılan yazı rengi
                    }
                }
            }
        }

        public void VerileriGetir(DateTime baslangic, DateTime bitis, string tur, string unvan)
        {
            hareketgrid.DataSource = null; // DataGridView'i temizle
            // SQL sorgusunu oluştur
            string query = @"SELECT DATE(ch.tarih) AS 'Tarih', c.Unvani AS 'Unvanı',
                            ch.tur AS 'Tür', ch.aciklama AS 'Açıklama',
                            IFNULL(ch.borc, 0) AS 'Borç', IFNULL(ch.tahsilat, 0) AS 'Tahsilat'
                     FROM cari_hareketleri ch
                     INNER JOIN Cari c ON ch.cari_kodu = c.CariKodu
                     WHERE DATE(ch.tarih) BETWEEN @baslangic AND @bitis";
            // Tür filtresini ekle
            if (!string.IsNullOrEmpty(tur))
                query += " AND ch.tur = @tur";
            // Unvan filtresini ekle
            if (!string.IsNullOrEmpty(unvan))
                query += " AND c.Unvani LIKE @unvan";
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Tarih parametrelerini formatlayarak geçir
                        cmd.Parameters.AddWithValue("@baslangic", baslangic.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@bitis", bitis.ToString("yyyy-MM-dd"));
                        if (!string.IsNullOrEmpty(tur))
                            cmd.Parameters.AddWithValue("@tur", tur);
                        if (!string.IsNullOrEmpty(unvan))
                            cmd.Parameters.AddWithValue("@unvan", "%" + unvan + "%");
                        using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            if (dt.Rows.Count == 0)
                            {
                                MessageBox.Show("Seçilen tarihler arasında veri bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                hareketgrid.DataSource = dt;
                                DataGridViewStilDuzenle(); // Stil düzenleme
                                ToplamlariHesapla(dt);    // Toplam borç ve alacakları hesapla
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Hata durumunda detaylı bilgi göster
                MessageBox.Show($"Hata: {ex.Message}\n{ex.StackTrace}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void DataGridViewStilDuzenle()
        {
            // Başlangıçta hiçbir satırın seçili olmamasını sağla
            hareketgrid.ClearSelection();
            // Sütun başlıklarını düzenle
            hareketgrid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            hareketgrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            hareketgrid.ColumnHeadersDefaultCellStyle.BackColor = Color.DodgerBlue;
            hareketgrid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            hareketgrid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            hareketgrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            hareketgrid.ColumnHeadersHeight = 40;

            // Satırların varsayılan stilini düzenle
            hareketgrid.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            hareketgrid.DefaultCellStyle.ForeColor = Color.Black;
            hareketgrid.DefaultCellStyle.BackColor = Color.White;
            hareketgrid.DefaultCellStyle.SelectionBackColor = Color.LightSkyBlue;
            hareketgrid.DefaultCellStyle.SelectionForeColor = Color.Black;

            // Satır ve hücre kenarlıklarını çiz
            hareketgrid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            hareketgrid.GridColor = Color.LightGray;

            // **Sütun genişliklerini manuel ayarla**
            hareketgrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            // Eğer sütunlar zaten tanımlıysa genişliklerini ayarla
            if (hareketgrid.Columns.Count > 0)
            {
                hareketgrid.Columns["Tarih"].Width = 103;       // Tarih sütunu genişliği
                hareketgrid.Columns["Tür"].Width = 120;        // Tür sütunu genişliği
                hareketgrid.Columns["Açıklama"].Width = 220;   // Açıklama sütunu genişliği
                hareketgrid.Columns["Borç"].Width = 90;       // Borç sütunu genişliği
                hareketgrid.Columns["Tahsilat"].Width = 90;   // Tahsilat sütunu genişliği
            }

            // Satır yüksekliklerini ayarla
            hareketgrid.RowTemplate.Height = 30;

            // **Manuel veri girişini engelle**
            hareketgrid.ReadOnly = true; // Kullanıcıların düzenleme yapmasını engelle
            hareketgrid.AllowUserToAddRows = false; // Yeni satır ekleme seçeneğini kapat
            hareketgrid.AllowUserToDeleteRows = false; // Kullanıcıların satır silmesini engelle
            hareketgrid.AllowUserToOrderColumns = false; // Kullanıcıların sütun sıralamasını engelle

            // Satır başlıklarını gizle
            hareketgrid.RowHeadersVisible = false;

            // Satır ve sütun boyutlarını değiştirmeyi engelle
            hareketgrid.AllowUserToResizeColumns = false;
            hareketgrid.AllowUserToResizeRows = false;

            // Sadece satır seçilsin
            hareketgrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Varsayılan görsel stil kapalı
            hareketgrid.EnableHeadersVisualStyles = false;

            // Sütunlar arası çizgi çekmek için
            hareketgrid.AdvancedCellBorderStyle.Left = DataGridViewAdvancedCellBorderStyle.Single;
            hareketgrid.AdvancedCellBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.Single;
            hareketgrid.AdvancedCellBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
            hareketgrid.AdvancedCellBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;
            hareketgrid.GridColor = Color.LightGray; // Çizgi rengini gri yap

            foreach (DataGridViewRow row in hareketgrid.Rows)
            {
                // "Tür" sütununda "Tahsilat Dekontu" varsa
                if (row.Cells["Tür"].Value != null && row.Cells["Tür"].Value.ToString() == "Tahsilat Dekontu")
                {
                    // Yazı rengini mavi yap
                    row.DefaultCellStyle.ForeColor = Color.Blue;
                    row.DefaultCellStyle.SelectionForeColor = Color.Blue; // Seçildiğinde de mavi
                }
                else
                {
                    // Diğer satırlar için varsayılan yazı rengi siyah
                    row.DefaultCellStyle.ForeColor = Color.Black;
                    row.DefaultCellStyle.SelectionForeColor = Color.Black; // Seçildiğinde de siyah
                }
            }
        }

        public void ToplamlariHesapla(DataTable dt)
        {
            // Toplam borç ve alacak hesapla
            decimal toplamBorc = 0;
            decimal toplamAlacak = 0;
            foreach (DataRow row in dt.Rows)
            {
                // Borç sütununu kontrol et ve dönüştür
                if (!row.IsNull("Borç"))
                {
                    toplamBorc += Convert.ToDecimal(row["Borç"]);
                }
                // Tahsilat sütununu kontrol et ve dönüştür
                if (!row.IsNull("Tahsilat"))
                {
                    toplamAlacak += Convert.ToDecimal(row["Tahsilat"]);
                }
            }
            // Toplamları ilgili textbox'lara yaz
            borctext.Text = toplamBorc.ToString("C2");
            alacaktext.Text = toplamAlacak.ToString("C2");
        }

        public void AddCustomButtonsToPrintPreview(Form previewForm)
        {
            // Eğer butonlar zaten eklenmişse tekrar ekleme
            if (previewForm.Controls.OfType<Button>().Any(b => b.Text == "Excel'e Aktar"))
                return;

            // Excel'e Aktar butonu
            Button btnExcelAktar = new Button
            {
                Text = "Excel'e Aktar",
                Size = new System.Drawing.Size(120, 30),
                Location = new System.Drawing.Point(120, 10), // Önizleme ekranındaki konumu
                BackColor = Color.DodgerBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnExcelAktar.FlatAppearance.BorderSize = 0;

            btnExcelAktar.Click += (s, e) =>
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel Dosyası (*.xlsx)|*.xlsx",
                    Title = "Excel Dosyası Kaydet"
                };
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                        Microsoft.Office.Interop.Excel.Workbook workbook = excel.Workbooks.Add(Type.Missing);
                        Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.ActiveSheet;
                        worksheet.Name = "Hareketler";

                        // Başlıkları yazdır
                        for (int i = 1; i < hareketgrid.Columns.Count + 1; i++)
                        {
                            worksheet.Cells[1, i] = hareketgrid.Columns[i - 1].HeaderText;
                        }

                        // Verileri yazdır
                        for (int i = 0; i < hareketgrid.Rows.Count; i++)
                        {
                            for (int j = 0; j < hareketgrid.Columns.Count; j++)
                            {
                                worksheet.Cells[i + 2, j + 1] = hareketgrid.Rows[i].Cells[j].Value?.ToString() ?? "";
                            }
                        }

                        workbook.SaveAs(saveFileDialog.FileName);
                        workbook.Close();
                        excel.Quit();
                        MessageBox.Show("Excel'e başarıyla aktarıldı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            };

            // Form kontrolüne Excel'e Aktar butonunu ekle
            previewForm.Controls.Add(btnExcelAktar);
        }

        public void DrawGridLines(Graphics g, int x, int y, int width, int height, Pen pen)
        {
            // Her satır için sütun çizgilerini çiz
            for (int i = 0; i < hareketgrid.Rows.Count + 1; i++) // +1 başlıklar için
            {
                int currentY = y + i * height;
                g.DrawLine(pen, x, currentY, x + width, currentY); // Yatay çizgi
            }

            // Sütun çizgileri
            int currentX = x;
            foreach (DataGridViewColumn column in hareketgrid.Columns)
            {
                currentX += column.Width;
                g.DrawLine(pen, currentX, y, currentX, y + height * hareketgrid.Rows.Count); // Dikey çizgi
            }
        }

        public void btnYazdir_Click(object sender, EventArgs e)
        {
            PrintDialog yaziciSecim = new PrintDialog();
            yaziciSecim.Document = printDocument;
            if (yaziciSecim.ShowDialog() == DialogResult.OK)
            {
                printDocument.PrinterSettings = yaziciSecim.PrinterSettings;
                printPreviewDialog.Document = printDocument;
                printPreviewDialog.ShowDialog();
            }
        }

        public void btnExcelAktar_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel Dosyası (*.xlsx)|*.xlsx",
                Title = "Excel Dosyası Kaydet"
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                    Microsoft.Office.Interop.Excel.Workbook workbook = excel.Workbooks.Add(Type.Missing);
                    Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.ActiveSheet;
                    worksheet.Name = "Hareketler";

                    // Başlıkları ekle
                    for (int i = 1; i < hareketgrid.Columns.Count + 1; i++)
                    {
                        worksheet.Cells[1, i] = hareketgrid.Columns[i - 1].HeaderText;
                    }

                    // Verileri ekle
                    for (int i = 0; i < hareketgrid.Rows.Count; i++)
                    {
                        for (int j = 0; j < hareketgrid.Columns.Count; j++)
                        {
                            worksheet.Cells[i + 2, j + 1] = hareketgrid.Rows[i].Cells[j].Value?.ToString() ?? "";
                        }
                    }

                    workbook.SaveAs(saveFileDialog.FileName);
                    workbook.Close();
                    excel.Quit();
                    MessageBox.Show("Excel'e başarıyla aktarıldı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            // Yazı tipleri
            Font baslikFontu = new Font("Segoe UI", 16, FontStyle.Bold);
            Font sutunFontu = new Font("Segoe UI", 12, FontStyle.Bold);
            Font veriFontu = new Font("Segoe UI", 10);
            Pen cizgiKalemi = new Pen(Color.LightGray, 1);

            // Kenar boşlukları ve başlangıç koordinatları
            float x = e.MarginBounds.Left;
            float y = e.MarginBounds.Top;

            // Sayfa genişliği ve sütun genişliklerini hesapla
            float[] sutunGenislikleri = { 90, 160, 130, 180, 80, 80 }; // Sütun genişlikleri

            // Sağ üst köşeye tarih ve saat
            string tarihSaat = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
            e.Graphics.DrawString(tarihSaat, veriFontu, Brushes.Black, e.MarginBounds.Right - 100, y);

            // Başlık
            string baslik = "Hareketler Raporu";
            SizeF baslikBoyutu = e.Graphics.MeasureString(baslik, baslikFontu);
            e.Graphics.DrawString(baslik, baslikFontu, Brushes.DodgerBlue, e.MarginBounds.Left + (e.MarginBounds.Width - baslikBoyutu.Width) / 2, y);
            y += baslikFontu.GetHeight(e.Graphics) + 20;

            // Sütun başlıkları
            x = e.MarginBounds.Left;
            string[] sutunlar = { "Tarih", "Unvan", "Tür", "Açıklama", "Borç", "Tahsilat" };

            // Başlık arka planını çiz
            RectangleF headerRect = new RectangleF(x, y, sutunGenislikleri.Sum(), sutunFontu.GetHeight(e.Graphics) + 10);
            e.Graphics.FillRectangle(new SolidBrush(Color.DodgerBlue), headerRect);

            for (int i = 0; i < sutunlar.Length; i++)
            {
                RectangleF cellRect = new RectangleF(x, y, sutunGenislikleri[i], sutunFontu.GetHeight(e.Graphics) + 10);
                e.Graphics.DrawString(sutunlar[i], sutunFontu, Brushes.White, x + 5, y + 5);
                e.Graphics.DrawRectangle(Pens.White, cellRect.X, cellRect.Y, cellRect.Width, cellRect.Height);
                x += sutunGenislikleri[i];
            }

            y += sutunFontu.GetHeight(e.Graphics) + 10;

            // Verileri yazdırma işlemi
            bool hasMorePages = false;
            while (_currentRow < hareketgrid.Rows.Count)
            {
                DataGridViewRow row = hareketgrid.Rows[_currentRow];
                if (row.IsNewRow)
                {
                    _currentRow++;
                    continue;
                }

                x = e.MarginBounds.Left;
                string tarih = row.Cells["Tarih"].Value?.ToString() ?? "";
                string unvan = row.Cells["Unvanı"].Value?.ToString() ?? "";
                string tur = row.Cells["Tür"].Value?.ToString() ?? "";
                string aciklama = row.Cells["Açıklama"].Value?.ToString() ?? "";
                string borc = row.Cells["Borç"].Value?.ToString() ?? "₺0,00";
                string tahsilat = row.Cells["Tahsilat"].Value?.ToString() ?? "₺0,00";

                // Arka plan rengini belirle
                Brush rowBrush = _currentRow % 2 == 0 ? Brushes.White : new SolidBrush(Color.FromArgb(245, 245, 245));

                // Arka planı çiz
                RectangleF rowRect = new RectangleF(e.MarginBounds.Left, y, sutunGenislikleri.Sum(), veriFontu.GetHeight(e.Graphics) + 10);
                e.Graphics.FillRectangle(rowBrush, rowRect);

                // Veriler
                e.Graphics.DrawString(tarih, veriFontu, Brushes.Black, x + 5, y + 5);
                x += sutunGenislikleri[0];

                e.Graphics.DrawString(unvan, veriFontu, Brushes.Black, x + 5, y + 5);
                x += sutunGenislikleri[1];

                // Tür kısmı "Borç Dekontu" ise kalın yazı
                Font turFontu = tur == "Borç Dekontu" ? new Font(veriFontu, FontStyle.Bold) : veriFontu;
                Brush turBrush = tur == "Tahsilat Dekontu" ? Brushes.Blue : Brushes.Black;
                e.Graphics.DrawString(tur, turFontu, turBrush, x + 5, y + 5);
                x += sutunGenislikleri[2];

                // Açıklama metnini sınırla ve "..." ile kısalt
                StringFormat stringFormat = new StringFormat
                {
                    Trimming = StringTrimming.EllipsisCharacter,
                    FormatFlags = StringFormatFlags.NoWrap
                };
                RectangleF aciklamaRect = new RectangleF(x + 5, y + 5, sutunGenislikleri[3] - 10, veriFontu.GetHeight(e.Graphics));
                e.Graphics.DrawString(aciklama, veriFontu, Brushes.Black, aciklamaRect, stringFormat);
                x += sutunGenislikleri[3];

                e.Graphics.DrawString(borc, veriFontu, Brushes.Red, x + 5, y + 5);
                x += sutunGenislikleri[4];

                e.Graphics.DrawString(tahsilat, veriFontu, Brushes.Blue, x + 5, y + 5);

                // Satır çerçevesini çiz
                e.Graphics.DrawRectangle(Pens.LightGray, rowRect.X, rowRect.Y, rowRect.Width, rowRect.Height);

                y += veriFontu.GetHeight(e.Graphics) + 10;
                _currentRow++;

                // Eğer sayfa sınırını aştıysak
                if (y > e.MarginBounds.Bottom)
                {
                    hasMorePages = true;
                    break;
                }
            }

            // Sayfalandırmayı kontrol et
            e.HasMorePages = hasMorePages;

            // Son sayfada toplamları yazdır
            if (!hasMorePages)
            {
                decimal toplamBorc = hareketgrid.Rows.Cast<DataGridViewRow>()
                                      .Where(r => !r.IsNewRow && r.Cells["Borç"].Value != null)
                                      .Sum(r => Convert.ToDecimal(r.Cells["Borç"].Value));

                decimal toplamTahsilat = hareketgrid.Rows.Cast<DataGridViewRow>()
                                          .Where(r => !r.IsNewRow && r.Cells["Tahsilat"].Value != null)
                                          .Sum(r => Convert.ToDecimal(r.Cells["Tahsilat"].Value));

                y += 20; // Biraz boşluk bırak

                // Toplam Kısmı
                Rectangle totalRect = new Rectangle((int)e.MarginBounds.Left, (int)y, (int)sutunGenislikleri.Sum(), 60);
                e.Graphics.FillRectangle(new SolidBrush(Color.LightBlue), totalRect);
                e.Graphics.DrawRectangle(Pens.DodgerBlue, totalRect);

                e.Graphics.DrawString($"Toplam Borç: {toplamBorc:C2}", new Font(veriFontu, FontStyle.Bold), Brushes.Red, e.MarginBounds.Left + 10, y + 10);
                e.Graphics.DrawString($"Toplam Tahsilat: {toplamTahsilat:C2}", new Font(veriFontu, FontStyle.Bold), Brushes.Blue, e.MarginBounds.Left + 10, y + 30);
            }
        }

        // Global değişken
        public int _currentRow = 0;

        public void btnyaziciSec_Click(object sender, EventArgs e)
        {
            try
            {
                if (printDialog.ShowDialog() == DialogResult.OK)
                {
                    printDocument.PrinterSettings = printDialog.PrinterSettings;
                    printDocument.Print();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Yazdırma işlemi sırasında bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // TextChanged event'ı ile hareketunvan TextBox'ındaki yazıya göre filtreleme yapılacak
        public void hareketunvan_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // Kullanıcı tarafından girilen unvanı al
                string unvan = hareketunvan.Text.Trim();

                // Tarihleri al
                DateTime ilkTarih = ilk.Value.Date;
                DateTime sonTarih = son.Value.Date;

                // Tür seçimini al
                string tur = tursecenek.SelectedItem?.ToString() == "Tümü" ? null : tursecenek.SelectedItem?.ToString();

                // Verileri getirme işlemi
                VerileriGetir(ilkTarih, sonTarih, tur, unvan);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}\n{ex.StackTrace}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // VirtualMode etkinleştir
        public void InitializeDataGridView()
        {
            hareketgrid.VirtualMode = true;  // VirtualMode etkinleştir
            hareketgrid.CellValueNeeded += new DataGridViewCellValueEventHandler(hareketgrid_CellValueNeeded);
        }

        // CellValueNeeded olayında sadece ekranda görünen satırlar yüklenir
        public void hareketgrid_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < hareketData.Count)  // Eğer satır indeks geçerliyse
            {
                var data = hareketData[e.RowIndex];  // Veriyi al
                switch (e.ColumnIndex)
                {
                    case 0: // Tarih
                        e.Value = data.Tarih;
                        break;
                    case 1: // Unvan
                        e.Value = data.Unvan;
                        break;
                    case 2: // Tür
                        e.Value = data.Tur;
                        break;
                    case 3: // Açıklama
                        e.Value = data.Aciklama;
                        break;
                    case 4: // Borç
                        e.Value = data.Borc;
                        break;
                    case 5: // Tahsilat
                        e.Value = data.Tahsilat;
                        break;
                }
            }
        }

        public async void VerileriGetirAsync(DateTime baslangic, DateTime bitis, string tur, string unvan)
        {
            try
            {
                // Asenkron sorgu çalıştır
                var dt = await Task.Run(() => VeriGetir(baslangic, bitis, tur, unvan));

                // DataGridView'yi güncelle
                hareketgrid.DataSource = dt;
                DataGridViewStilDuzenle();
                ToplamlariHesapla(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Veritabanından veriyi asenkron al
        public DataTable VeriGetir(DateTime baslangic, DateTime bitis, string tur, string unvan)
        {
            string query = @"SELECT DATE(ch.tarih) AS 'Tarih', c.Unvani AS 'Unvanı',
                            ch.tur AS 'Tür', ch.aciklama AS 'Açıklama',
                            IFNULL(ch.borc, 0) AS 'Borç', IFNULL(ch.tahsilat, 0) AS 'Tahsilat'
                     FROM cari_hareketleri ch
                     INNER JOIN Cari c ON ch.cari_kodu = c.CariKodu
                     WHERE DATE(ch.tarih) BETWEEN @baslangic AND @bitis";

            if (!string.IsNullOrEmpty(tur))
                query += " AND ch.tur = @tur";

            if (!string.IsNullOrEmpty(unvan))
                query += " AND c.Unvani LIKE @unvan";

            // Veritabanından veri çekme
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@baslangic", baslangic.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@bitis", bitis.ToString("yyyy-MM-dd"));

                    if (!string.IsNullOrEmpty(tur))
                        cmd.Parameters.AddWithValue("@tur", tur);

                    if (!string.IsNullOrEmpty(unvan))
                        cmd.Parameters.AddWithValue("@unvan", "%" + unvan + "%");

                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        // Hareket sınıfı
        public class Hareket
        {
            public DateTime Tarih { get; set; }
            public string Unvan { get; set; }
            public string Tur { get; set; }
            public string Aciklama { get; set; }
            public decimal Borc { get; set; }
            public decimal Tahsilat { get; set; }
        }

        public void InitializeContextMenu()
        {
            // Yeni bir ContextMenuStrip oluştur
            ContextMenuStrip contextMenu = new ContextMenuStrip();

            // Tarih Filtreleme menüleri
            ToolStripMenuItem son7GunItem = new ToolStripMenuItem("Son 7 Gün");
            ToolStripMenuItem son15GunItem = new ToolStripMenuItem("Son 15 Gün");
            ToolStripMenuItem buAyItem = new ToolStripMenuItem("Bu Ay");
            ToolStripMenuItem gecenAyItem = new ToolStripMenuItem("Geçen Ay");

            son7GunItem.Click += (sender, e) => FiltreleSon7Gun();
            son15GunItem.Click += (sender, e) => FiltreleSon15Gun();
            buAyItem.Click += (sender, e) => FiltreleBuAy();
            gecenAyItem.Click += (sender, e) => FiltreleGecenAy();

            contextMenu.Items.Add(son7GunItem);
            contextMenu.Items.Add(son15GunItem);
            contextMenu.Items.Add(buAyItem);
            contextMenu.Items.Add(gecenAyItem);

            // Export işlemleri
            ToolStripMenuItem excelExportItem = new ToolStripMenuItem("Excel'e Aktar");
            ToolStripMenuItem csvExportItem = new ToolStripMenuItem("CSV'ye Aktar");

            excelExportItem.Click += (sender, e) => ExcelAktar();
            csvExportItem.Click += (sender, e) => CsvAktar();

            contextMenu.Items.Add(new ToolStripSeparator());  // Menüye ayırıcı ekle
            contextMenu.Items.Add(excelExportItem);
            contextMenu.Items.Add(csvExportItem);

            // DataGridView'e context menu'yu ekle
            hareketgrid.ContextMenuStrip = contextMenu;
        }

        // Son 7 Gün filtrele
        public void FiltreleSon7Gun()
        {
            DateTime today = DateTime.Today;
            DateTime last7Days = today.AddDays(-7);

            // Tarih aralığını güncelle
            ilk.Value = last7Days;
            son.Value = today;

            // Verileri getirme
            VerileriGetirAsync(last7Days, today, null, null);  // Verileri getirme
        }

        // Son 15 Gün filtrele
        public void FiltreleSon15Gun()
        {
            DateTime today = DateTime.Today;
            DateTime last15Days = today.AddDays(-15);

            // Tarih aralığını güncelle
            ilk.Value = last15Days;
            son.Value = today;

            // Verileri getirme
            VerileriGetirAsync(last15Days, today, null, null);  // Verileri getirme
        }

        // Bu Ay filtrele
        public void FiltreleBuAy()
        {
            DateTime today = DateTime.Today;
            DateTime firstOfMonth = new DateTime(today.Year, today.Month, 1);
            DateTime lastOfMonth = firstOfMonth.AddMonths(1).AddDays(-1);

            // Tarih aralığını güncelle
            ilk.Value = firstOfMonth;
            son.Value = lastOfMonth;

            // Verileri getirme
            VerileriGetirAsync(firstOfMonth, lastOfMonth, null, null);  // Verileri getirme
        }

        // Geçen Ay filtrele
        public void FiltreleGecenAy()
        {
            DateTime today = DateTime.Today;
            DateTime firstOfLastMonth = new DateTime(today.Year, today.Month - 1, 1);
            DateTime lastOfLastMonth = firstOfLastMonth.AddMonths(1).AddDays(-1);

            // Tarih aralığını güncelle
            ilk.Value = firstOfLastMonth;
            son.Value = lastOfLastMonth;

            // Verileri getirme
            VerileriGetirAsync(firstOfLastMonth, lastOfLastMonth, null, null);  // Verileri getirme
        }

        public void ExcelAktar()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel Dosyası (*.xlsx)|*.xlsx",
                Title = "Excel Dosyası Kaydet"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                    Microsoft.Office.Interop.Excel.Workbook workbook = excel.Workbooks.Add(Type.Missing);
                    Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.ActiveSheet;
                    worksheet.Name = "Hareketler";

                    // Başlıkları yazdır
                    for (int i = 1; i < hareketgrid.Columns.Count + 1; i++)
                    {
                        worksheet.Cells[1, i] = hareketgrid.Columns[i - 1].HeaderText;
                    }

                    // Verileri yazdır
                    for (int i = 0; i < hareketgrid.Rows.Count; i++)
                    {
                        for (int j = 0; j < hareketgrid.Columns.Count; j++)
                        {
                            worksheet.Cells[i + 2, j + 1] = hareketgrid.Rows[i].Cells[j].Value?.ToString() ?? "";
                        }
                    }

                    workbook.SaveAs(saveFileDialog.FileName);
                    workbook.Close();
                    excel.Quit();
                    MessageBox.Show("Excel'e başarıyla aktarıldı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void CsvAktar()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "CSV Dosyası (*.csv)|*.csv",
                Title = "CSV Dosyası Kaydet"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (System.IO.StreamWriter sw = new System.IO.StreamWriter(saveFileDialog.FileName))
                    {
                        // Sütun başlıklarını yazdır
                        for (int i = 0; i < hareketgrid.Columns.Count; i++)
                        {
                            sw.Write(hareketgrid.Columns[i].HeaderText + ",");
                        }
                        sw.WriteLine();

                        // Verileri yazdır
                        foreach (DataGridViewRow row in hareketgrid.Rows)
                        {
                            for (int i = 0; i < hareketgrid.Columns.Count; i++)
                            {
                                sw.Write(row.Cells[i].Value?.ToString() ?? "" + ",");
                            }
                            sw.WriteLine();
                        }
                    }
                    MessageBox.Show("CSV dosyasına başarıyla aktarıldı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void hrktsorgula_Click(object sender, EventArgs e)
        {
            try
            {
                // Tarihleri al
                DateTime ilkTarih = ilk.Value.Date;
                DateTime sonTarih = son.Value.Date;

                // Tarih sırasını kontrol et
                if (ilkTarih > sonTarih)
                {
                    MessageBox.Show("İlk tarih, son tarihten büyük olamaz. Lütfen tarihleri kontrol edin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Tarih sırasını kontrol etmeden devam etmeyin
                }

                // Tür seçimini al
                string tur = tursecenek.SelectedItem?.ToString() == "Tümü" ? null : tursecenek.SelectedItem?.ToString();

                // Verileri getirme işlemi
                VerileriGetir(ilkTarih, sonTarih, tur, hareketunvan.Text.Trim());
            }
            catch (Exception ex)
            {
                // Detaylı hata mesajı
                MessageBox.Show($"Hata: {ex.Message}\n{ex.StackTrace}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void hrktyazdir_Click(object sender, EventArgs e)
        {
            try
            {
                if (hareketgrid.Rows.Count > 0)
                {
                    // Önizleme ekranı
                    printPreviewDialog.Document = printDocument;
                    printPreviewDialog.StartPosition = FormStartPosition.CenterScreen;
                    printPreviewDialog.Size = new Size(1200, 800); // Büyük ekran boyutu
                    printPreviewDialog.ShowDialog();

                    // Önizleme ekranına butonları ekle
                    Form previewForm = (Form)printPreviewDialog;
                    AddCustomButtonsToPrintPreview(previewForm);
                }
                else
                {
                    MessageBox.Show("Yazdırılacak bir veri yok!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void hrktformkapat_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        public void hrktdetay_Click(object sender, EventArgs e)
        {
            if (hareketgrid.SelectedRows.Count > 0)
            {
                var selectedRow = hareketgrid.SelectedRows[0];
                var tur = selectedRow.Cells["Tür"]?.Value?.ToString();
                var unvan = selectedRow.Cells["Unvanı"]?.Value?.ToString() ?? "";
                var borcHucresi = selectedRow.Cells["Borç"];
                var tahsilatHucresi = selectedRow.Cells["Tahsilat"];
                var aciklamaHucresi = selectedRow.Cells["Açıklama"];

                if (borcHucresi == null && tahsilatHucresi == null)
                {
                    MessageBox.Show("Seçili satırda Borç veya Tahsilat sütunu bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string borctutar = borcHucresi?.Value?.ToString() ?? "₺0,00";
                string tahsiltutar = tahsilatHucresi?.Value?.ToString() ?? "₺0,00";
                string aciklama = aciklamaHucresi?.Value?.ToString() ?? "";

                if (!decimal.TryParse(borctutar.Replace("₺", "").Replace(".", "").Replace(",", "."), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal parsedBorc))
                {
                    parsedBorc = 0;
                }

                if (!decimal.TryParse(tahsiltutar.Replace("₺", "").Replace(".", "").Replace(",", "."), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal parsedTahsilat))
                {
                    parsedTahsilat = 0;
                }

                if (tur == "Borç Dekontu")
                {
                    try
                    {
                        using (Borcekle borcFormu = new Borcekle(unvan))
                        {
                            borcFormu.textBox3.Text = parsedBorc.ToString("0.00 TL");
                            borcFormu.textBox2.Text = aciklama;
                            borcFormu.textBox3.ReadOnly = true;
                            borcFormu.textBox2.ReadOnly = true;

                            // Form başlığını değiştir
                            borcFormu.Text = "Borç Detayları Gör";

                            // label1 ayarlarını yap
                            var label1 = borcFormu.Controls.OfType<Label>().FirstOrDefault(lbl => lbl.Name == "label1");
                            if (label1 != null)
                            {
                                label1.Text = "Cari Harekete Ait Detaylar";
                                label1.Visible = true;
                            }

                            // Kaydet butonunu pasif yap
                            var kaydetButton = borcFormu.Controls.OfType<Button>().FirstOrDefault(btn => btn.Name == "kaydet");
                            if (kaydetButton != null)
                            {
                                kaydetButton.Enabled = false;
                            }

                            borcFormu.ShowDialog();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Borç Detayları Gör formu açılırken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (tur == "Tahsilat Dekontu")
                {
                    try
                    {
                        using (Tahsilat tahsilatFormu = new Tahsilat(unvan))
                        {
                            tahsilatFormu.textBox3.Text = parsedTahsilat.ToString("0.00 TL");
                            tahsilatFormu.textBox2.Text = aciklama;
                            tahsilatFormu.textBox3.ReadOnly = true;
                            tahsilatFormu.textBox2.ReadOnly = true;

                            // Form başlığını değiştir
                            tahsilatFormu.Text = "Tahsilat Detayları Gör";

                            // label1 ayarlarını yap
                            var label1 = tahsilatFormu.Controls.OfType<Label>().FirstOrDefault(lbl => lbl.Name == "label1");
                            if (label1 != null)
                            {
                                label1.Text = "Cari Harekete Ait Detaylar";
                                label1.Visible = true;
                            }

                            // Kaydet butonunu pasif yap
                            var kaydetButton = tahsilatFormu.Controls.OfType<Button>().FirstOrDefault(btn => btn.Name == "kaydet");
                            if (kaydetButton != null)
                            {
                                kaydetButton.Enabled = false;
                            }

                            tahsilatFormu.ShowDialog();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Tahsilat Detayları Gör formu açılırken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Seçili işlem türü için detay bilgisi açılamaz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Lütfen bir işlem seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}