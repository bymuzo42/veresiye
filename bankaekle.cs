using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;
using System.Drawing;
using System.Globalization;

namespace Veresiye2025
{
    public partial class bankaekle : Form
    {
        public int seciliBankaID = -1; // Düzenleme için seçili ID'yi takip edecek
        public bool isDragging = false;
        public Point dragStartPoint;

        public bankaekle()
        {
            InitializeComponent();
            // Form hareketi için olaylar
            panelTitleBar.MouseDown += PanelTitleBar_MouseDown;
            panelTitleBar.MouseMove += PanelTitleBar_MouseMove;
            panelTitleBar.MouseUp += PanelTitleBar_MouseUp;
            lblTitle.MouseDown += PanelTitleBar_MouseDown;
            lblTitle.MouseMove += PanelTitleBar_MouseMove;
            lblTitle.MouseUp += PanelTitleBar_MouseUp;

            // Buton hover efektleri
            btnClose.MouseEnter += btnClose_MouseEnter;
            btnClose.MouseLeave += btnClose_MouseLeave;

            // ESC tuşu ile formu kapatma
            this.KeyDown += new KeyEventHandler(bankaekle_KeyDown);
            this.KeyPreview = true;
        }

        public void bankaekle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();  // ESC tuşuna basıldığında formu kapat
            }
        }

        public void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void btnClose_MouseEnter(object sender, EventArgs e)
        {
            btnClose.FillColor = Color.FromArgb(220, 53, 69); // Kırmızı hover
        }

        public void btnClose_MouseLeave(object sender, EventArgs e)
        {
            btnClose.FillColor = Color.Transparent;
        }

        public void PanelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            dragStartPoint = new Point(e.X, e.Y);
        }

        public void PanelTitleBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentPosition = PointToScreen(new Point(e.X, e.Y));
                Location = new Point(currentPosition.X - dragStartPoint.X, currentPosition.Y - dragStartPoint.Y);
            }
        }

        public void PanelTitleBar_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        public void bankaekle_Load(object sender, EventArgs e)
        {
            // Form açıldığında bankaları listele
            BankalariYukle();

            // DataGridView görünümünü ayarla
            DataGridViewAyarla();

            // RichTextBox'u salt okunur yap
            rtbNotlar.ReadOnly = true;
            rtbNotlar.TabStop = false;
            rtbNotlar.BackColor = Color.White;

            // Bilgi metin kutusuna hoş geldiniz mesajı ekle
            rtbNotlar.Clear();
            rtbNotlar.SelectionAlignment = HorizontalAlignment.Center;
            rtbNotlar.SelectionFont = new Font("Segoe UI", 12, FontStyle.Bold);
            rtbNotlar.SelectionColor = Color.FromArgb(0, 123, 255);
            rtbNotlar.AppendText("🏦 BANKA YÖNETİMİ 🏦\n\n");
            rtbNotlar.SelectionFont = new Font("Segoe UI", 9.5f);
            rtbNotlar.SelectionColor = Color.Black;
            rtbNotlar.AppendText("• Yeni banka eklemek için formu doldurup 'Kaydet' butonuna tıklayın.\n\n");
            rtbNotlar.AppendText("• Mevcut kayıtları görmek için aşağıdaki tabloyu kullanabilirsiniz.\n\n");
            rtbNotlar.AppendText("• Düzenlemek istediğiniz kaydı seçip 'Düzenle' butonuna tıklayın.\n\n");
            rtbNotlar.AppendText("• Silmek istediğiniz kaydı seçip 'Sil' butonuna tıklayın.\n\n");
            rtbNotlar.AppendText("• Ondalıklı değerlerde lütfen nokta kullanın (örn: 2.5).\n\n");

            // "Postakip" formunda cihaz ve banka sayısını güncelle
            if (this.Owner is postakip postakipForm)
            {
                postakipForm.CihazSayisiniGuncelle();
                postakipForm.BankaSayisiniGuncelle();
                postakipForm.Invalidate();
                postakipForm.Refresh();
            }
        }

        public void DataGridViewAyarla()
        {
            try
            {
                // Guna2DataGridView için stil ayarları
                dgvBankalar.BorderStyle = BorderStyle.Fixed3D;
                dgvBankalar.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                dgvBankalar.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(0, 123, 255);
                dgvBankalar.ThemeStyle.HeaderStyle.ForeColor = Color.White;

                // Başlıklara tıklamayı ve sıralamayı tamamen etkisiz hale getirme
                foreach (DataGridViewColumn column in dgvBankalar.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                // Başlık stillerini ayarla
                dgvBankalar.EnableHeadersVisualStyles = false;
                dgvBankalar.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgvBankalar.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 123, 255);
                dgvBankalar.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            catch (Exception ex)
            {
                Console.WriteLine("DataGridView ayarlanırken hata: " + ex.Message);
            }
        }

        public void bankakaydet_Click(object sender, EventArgs e)
        {
            bool islemBasarili = false;

            try
            {
                string bankaAdi = txtBankaAdi.Text.Trim();
                string cihazAdi = txtCihazAdi.Text.Trim();
                int valor;
                decimal kesintiOrani, erkenBozumOrani;

                // Boş alan kontrolü
                if (string.IsNullOrWhiteSpace(bankaAdi) || string.IsNullOrWhiteSpace(cihazAdi))
                {
                    MessageBox.Show("Banka adı ve cihaz adı alanları boş bırakılamaz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Valör kontrolü - sadece sayı olmalı
                if (!int.TryParse(txtValor.Text, out valor))
                {
                    MessageBox.Show("Valör değeri geçerli bir sayı olmalıdır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Kesinti oranı kontrolü - sadece noktalı değer
                if (!decimal.TryParse(txtKesintiOrani.Text, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out kesintiOrani))
                {
                    MessageBox.Show("Kesinti oranı geçerli bir sayı olmalıdır. Lütfen ondalık ayırıcı olarak nokta (.) kullanın.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Erken bozum oranı kontrolü - sadece noktalı değer
                if (!decimal.TryParse(txtErkenBozumOrani.Text, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out erkenBozumOrani))
                {
                    MessageBox.Show("Erken bozum oranı geçerli bir sayı olmalıdır. Lütfen ondalık ayırıcı olarak nokta (.) kullanın.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Değerleri formatlayalım - Veritabanına % değil, ondalık olarak kaydedeceğiz
                kesintiOrani /= 100;
                erkenBozumOrani /= 100;

                if (seciliBankaID == -1)
                {
                    KaydetBanka(bankaAdi, valor, kesintiOrani, erkenBozumOrani, cihazAdi);
                }
                else
                {
                    GuncelleBanka(seciliBankaID, bankaAdi, valor, kesintiOrani, erkenBozumOrani, cihazAdi);
                    seciliBankaID = -1;
                }

                islemBasarili = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("İşlem sırasında bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (islemBasarili)
            {
                // Postakip Formunu Bul ve Label'ları Güncelle
                foreach (Form frm in Application.OpenForms)
                {
                    if (frm is postakip postakipForm)
                    {
                        postakipForm.CihazSayisiniGuncelle();
                        postakipForm.BankaSayisiniGuncelle();
                        postakipForm.BankalariYukle();
                        break;
                    }
                }

                // Listeyi güncelle
                BankalariYukle();

                // Formu temizle
                TemizleForm();
            }
        }

        public void RichTextBoxGuncelle(string bankaAdi, string valor, string kesinti, string erkenBozum, string cihazAdi)
        {
            rtbNotlar.Clear();
            rtbNotlar.SelectionAlignment = HorizontalAlignment.Left;

            // Başlık
            rtbNotlar.SelectionFont = new Font("Segoe UI", 14, FontStyle.Bold);
            rtbNotlar.SelectionColor = Color.FromArgb(0, 123, 255);
            rtbNotlar.AppendText("🔹 SEÇİLİ BANKA BİLGİLERİ 🔹\n\n");

            // Banka Adı
            rtbNotlar.SelectionFont = new Font("Segoe UI", 11, FontStyle.Bold);
            rtbNotlar.SelectionColor = Color.Black;
            rtbNotlar.AppendText("🏦 Banka Adı: ");
            rtbNotlar.SelectionFont = new Font("Segoe UI", 11, FontStyle.Regular);
            rtbNotlar.SelectionColor = Color.DarkGreen;
            rtbNotlar.AppendText($"{bankaAdi}\n\n");

            // Valör
            rtbNotlar.SelectionFont = new Font("Segoe UI", 11, FontStyle.Bold);
            rtbNotlar.SelectionColor = Color.Black;
            rtbNotlar.AppendText("📅 Valör: ");
            rtbNotlar.SelectionFont = new Font("Segoe UI", 11, FontStyle.Regular);
            rtbNotlar.SelectionColor = Color.DarkMagenta;
            rtbNotlar.AppendText($"{valor} Gün\n\n");

            // Kesinti Oranı
            rtbNotlar.SelectionFont = new Font("Segoe UI", 11, FontStyle.Bold);
            rtbNotlar.SelectionColor = Color.Black;
            rtbNotlar.AppendText("💰 Kesinti Oranı: ");
            rtbNotlar.SelectionFont = new Font("Segoe UI", 11, FontStyle.Regular);
            rtbNotlar.SelectionColor = Color.Red;
            rtbNotlar.AppendText($"%{kesinti}\n\n");

            // Erken Bozum Oranı
            rtbNotlar.SelectionFont = new Font("Segoe UI", 11, FontStyle.Bold);
            rtbNotlar.SelectionColor = Color.Black;
            rtbNotlar.AppendText("⚡ Erken Bozum Oranı: ");
            rtbNotlar.SelectionFont = new Font("Segoe UI", 11, FontStyle.Regular);
            rtbNotlar.SelectionColor = Color.DarkRed;
            rtbNotlar.AppendText($"%{erkenBozum}\n\n");

            // Cihaz Adı
            rtbNotlar.SelectionFont = new Font("Segoe UI", 11, FontStyle.Bold);
            rtbNotlar.SelectionColor = Color.Black;
            rtbNotlar.AppendText("📱 Cihaz Adı: ");
            rtbNotlar.SelectionFont = new Font("Segoe UI", 11, FontStyle.Regular);
            rtbNotlar.SelectionColor = Color.DarkOrange;
            rtbNotlar.AppendText($"{cihazAdi}\n");
        }

        public void KaydetBanka(string bankaAdi, int valor, decimal kesintiOrani, decimal erkenBozumOrani, string cihazAdi)
        {
            string connectionString = "Data Source=veresiye.db;Version=3;";
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Banka (BankaAdi, Valor, KesintiOrani, ErkenBozumOrani, CihazAdi) VALUES (@BankaAdi, @Valor, @KesintiOrani, @ErkenBozumOrani, @CihazAdi)";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@BankaAdi", bankaAdi);
                    cmd.Parameters.AddWithValue("@Valor", valor);
                    cmd.Parameters.AddWithValue("@KesintiOrani", kesintiOrani);
                    cmd.Parameters.AddWithValue("@ErkenBozumOrani", erkenBozumOrani);
                    cmd.Parameters.AddWithValue("@CihazAdi", cihazAdi);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Banka başarıyla kaydedildi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        public void GuncelleBanka(int bankaID, string bankaAdi, int valor, decimal kesintiOrani, decimal erkenBozumOrani, string cihazAdi)
        {
            string connectionString = "Data Source=veresiye.db;Version=3;";
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE Banka SET BankaAdi=@BankaAdi, Valor=@Valor, KesintiOrani=@KesintiOrani, ErkenBozumOrani=@ErkenBozumOrani, CihazAdi=@CihazAdi WHERE BankaID=@BankaID";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@BankaAdi", bankaAdi);
                    cmd.Parameters.AddWithValue("@Valor", valor);
                    cmd.Parameters.AddWithValue("@KesintiOrani", kesintiOrani);
                    cmd.Parameters.AddWithValue("@ErkenBozumOrani", erkenBozumOrani);
                    cmd.Parameters.AddWithValue("@CihazAdi", cihazAdi);
                    cmd.Parameters.AddWithValue("@BankaID", bankaID);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Banka bilgileri güncellendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        public void bankasil_Click(object sender, EventArgs e)
        {
            if (dgvBankalar.CurrentRow == null)
            {
                MessageBox.Show("Lütfen silmek için bir banka seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int bankaID = Convert.ToInt32(dgvBankalar.CurrentRow.Cells["BankaID"].Value);
            string bankaAdi = dgvBankalar.CurrentRow.Cells["BankaAdi"].Value.ToString();

            DialogResult sonuc = MessageBox.Show(
                $"'{bankaAdi}' bankasını silmek istediğinizden emin misiniz?",
                "Onay",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (sonuc == DialogResult.Yes)
            {
                string connectionString = "Data Source=veresiye.db;Version=3;";
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        string query = "DELETE FROM Banka WHERE BankaID=@BankaID";
                        using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@BankaID", bankaID);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Banka başarıyla silindi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Silme hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                // Silme işleminden sonra DataGridView güncellenmeli
                BankalariYukle();

                // Postakip Formunu Güncelle
                foreach (Form frm in Application.OpenForms)
                {
                    if (frm is postakip postakipForm)
                    {
                        postakipForm.CihazSayisiniGuncelle();
                        postakipForm.BankaSayisiniGuncelle();
                        postakipForm.BankalariYukle();
                        break;
                    }
                }

                // Form alanlarını temizle
                TemizleForm();
            }
        }

        public void bankaduzenle_Click(object sender, EventArgs e)
        {
            if (dgvBankalar.CurrentRow == null)
            {
                MessageBox.Show("Lütfen düzenlemek için bir banka seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Seçili satırdan verileri al
            seciliBankaID = Convert.ToInt32(dgvBankalar.CurrentRow.Cells["BankaID"].Value);
            string bankaAdi = dgvBankalar.CurrentRow.Cells["BankaAdi"].Value.ToString();
            string valor = dgvBankalar.CurrentRow.Cells["Valor"].Value.ToString();
            string kesinti = dgvBankalar.CurrentRow.Cells["KesintiOrani"].Value.ToString();
            string erkenBozum = dgvBankalar.CurrentRow.Cells["ErkenBozumOrani"].Value.ToString();
            string cihazAdi = dgvBankalar.CurrentRow.Cells["CihazAdi"].Value.ToString();

            // TextBox'lara verileri aktar
            txtBankaAdi.Text = bankaAdi;
            txtValor.Text = valor;
            txtKesintiOrani.Text = kesinti;
            txtErkenBozumOrani.Text = erkenBozum;
            txtCihazAdi.Text = cihazAdi;

            // RichTextBox bilgilerini güncelle
            RichTextBoxGuncelle(bankaAdi, valor, kesinti, erkenBozum, cihazAdi);

            // Kaydet butonunun metnini değiştir
            btnKaydet.Text = "Güncelle";
        }

        internal void BankalariYukle()
        {
            string connectionString = "Data Source=veresiye.db;Version=3;";
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    // SQL sorgusunu düzelttim - Yüzdelik değerleri 100 ile çarparak göster
                    string query = "SELECT BankaID, BankaAdi, Valor, (KesintiOrani*100) AS KesintiOrani, (ErkenBozumOrani*100) AS ErkenBozumOrani, CihazAdi FROM Banka";
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvBankalar.DataSource = dt;

                        // DataGridView görünümünü özelleştir
                        if (dgvBankalar.Columns.Count > 0)
                        {
                            // Sütun başlıklarını özelleştir
                            dgvBankalar.Columns["BankaID"].HeaderText = "ID";
                            dgvBankalar.Columns["BankaAdi"].HeaderText = "Banka Adı";
                            dgvBankalar.Columns["Valor"].HeaderText = "Valör (Gün)";
                            dgvBankalar.Columns["KesintiOrani"].HeaderText = "Kesinti Oranı (%)";
                            dgvBankalar.Columns["ErkenBozumOrani"].HeaderText = "Erken Bozum Oranı (%)";
                            dgvBankalar.Columns["CihazAdi"].HeaderText = "Cihaz Adı";

                            // BankaID sütununu gizle
                            dgvBankalar.Columns["BankaID"].Visible = false;

                            // Sayısal sütunlar için hizalama ayarla
                            dgvBankalar.Columns["Valor"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            dgvBankalar.Columns["KesintiOrani"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            dgvBankalar.Columns["ErkenBozumOrani"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                            // Sayısal değerler için format belirle
                            dgvBankalar.Columns["KesintiOrani"].DefaultCellStyle.Format = "0.##";
                            dgvBankalar.Columns["ErkenBozumOrani"].DefaultCellStyle.Format = "0.##";
                        }

                        // DataGridView stillerini ayarla
                        DataGridViewAyarla();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veri yüklenirken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void TemizleForm()
        {
            txtBankaAdi.Clear();
            txtValor.Clear();
            txtKesintiOrani.Clear();
            txtErkenBozumOrani.Clear();
            txtCihazAdi.Clear();
            seciliBankaID = -1;
            btnKaydet.Text = "Kaydet";

            // RichTextBox'u varsayılan bilgi mesajıyla güncelle
            rtbNotlar.Clear();
            rtbNotlar.SelectionAlignment = HorizontalAlignment.Center;
            rtbNotlar.SelectionFont = new Font("Segoe UI", 12, FontStyle.Bold);
            rtbNotlar.SelectionColor = Color.FromArgb(0, 123, 255);
            rtbNotlar.AppendText("🏦 BANKA YÖNETİMİ 🏦\n\n");
            rtbNotlar.SelectionFont = new Font("Segoe UI", 9.5f);
            rtbNotlar.SelectionColor = Color.Black;
            rtbNotlar.AppendText("• Yeni banka eklemek için formu doldurup 'Kaydet' butonuna tıklayın.\n\n");
            rtbNotlar.AppendText("• Mevcut kayıtları görmek için aşağıdaki tabloyu kullanabilirsiniz.\n\n");
            rtbNotlar.AppendText("• Düzenlemek istediğiniz kaydı seçip 'Düzenle' butonuna tıklayın.\n\n");
            rtbNotlar.AppendText("• Silmek istediğiniz kaydı seçip 'Sil' butonuna tıklayın.\n\n");
            rtbNotlar.AppendText("• Ondalıklı değerlerde lütfen nokta kullanın (örn: 2.5).\n\n");
        }

        public void temizle_Click(object sender, EventArgs e)
        {
            TemizleForm();
        }
    }
}