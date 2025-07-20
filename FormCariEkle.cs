using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using Guna.UI2.WinForms;

namespace Veresiye2025
{
    public partial class FormCariEkle : Form
    {
        public string cariKodu;
        public bool isEditMode = false; // Düzenleme modunu takip etmek için
        public bool isDarkMode = false; // Tema durumu
        public bool isDragging = false; // Form taşıma için
        public Point dragStartPoint; // Form taşıma için başlangıç noktası

        // Varsayılan constructor: Yeni cari eklemek veya düzenleme için
        public FormCariEkle(string cariKodu = "", string unvan = "")
        {
            InitializeComponent();

            // Modern stil uygula
            ApplyModernStyle();

            // Hesap türlerini ComboBox'a ekle
            cmbHesap.Items.Clear(); // Önce temizleyelim
            cmbHesap.Items.Add("Aktif Hesap");
            cmbHesap.Items.Add("Pasif Hesap");

            // Varsayılan olarak "Aktif Hesap" seçili
            cmbHesap.SelectedIndex = 0;

            // Cari kodunu kontrol et ve düzenleme modunu ayarla
            if (!string.IsNullOrEmpty(cariKodu))
            {
                this.cariKodu = cariKodu;
                isEditMode = true;
                LoadCariDetails(); // Cari detaylarını yükle
                lblFormTitle.Text = "Cari Düzenle"; // Başlık metnini güncelle
            }
            else
            {
                LoadCariKodu(); // Yeni cari kodu oluştur
                lblFormTitle.Text = "Yeni Cari Ekle"; // Başlık metnini güncelle
            }

            // txtUnvani'ye dışarıdan gelen unvanı aktar
            if (!string.IsNullOrEmpty(unvan))
            {
                txtUnvani.Text = unvan;
            }

            // FormClosing olayını bağla
            this.FormClosing += new FormClosingEventHandler(this.FormCariEkle_FormClosing);
        }

        // Modern stil uygulama
        public void ApplyModernStyle()
        {
            // Form ayarları
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(245, 247, 250);

            // Yuvarlatılmış köşeler
            ApplyRoundedCorners();

            // Tab kontrol görsel ayarları (gruplandırma için)
            tabControl.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControl.DrawItem += TabControl_DrawItem;

            // Form boyutunu ve konumunu ayarla
            this.Size = new Size(722, 508);

            // TextBox stillerini düzenle
            foreach (TabPage tabPage in tabControl.TabPages)
            {
                foreach (Control control in tabPage.Controls)
                {
                    if (control is TextBox textBox)
                    {
                        textBox.BorderStyle = BorderStyle.FixedSingle;
                        textBox.Font = new Font("Segoe UI", 9.5F);
                        textBox.BackColor = Color.FromArgb(248, 249, 250);
                    }
                    else if (control is Label label)
                    {
                        label.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
                        label.ForeColor = Color.FromArgb(60, 60, 60);
                    }
                    else if (control is ComboBox comboBox)
                    {
                        comboBox.FlatStyle = FlatStyle.Flat;
                        comboBox.Font = new Font("Segoe UI", 9.5F);
                        comboBox.BackColor = Color.FromArgb(248, 249, 250);

                        // ComboBox DropDownStyle'ı ayarla
                        comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                    }
                }
            }

            // Hesap durumu renk kontrolü - cmbHesap değiştiğinde rengi güncelle
            cmbHesap.SelectedIndexChanged += (s, e) =>
            {
                UpdateHesapStatusColor();
            };

            // İlerleme çubuğu görsel ayarları
            progressBar.Style = ProgressBarStyle.Continuous;
            progressBar.Height = 5;
            progressBar.Visible = false;
        }

        // Hesap durumu renk güncellemesi
        public void UpdateHesapStatusColor()
        {
            if (cmbHesap.SelectedItem != null)
            {
                string selectedStatus = cmbHesap.SelectedItem.ToString();
                if (selectedStatus == "Pasif Hesap")
                {
                    cmbHesap.ForeColor = Color.Red;
                }
                else
                {
                    cmbHesap.ForeColor = Color.FromArgb(0, 123, 255); // veya Color.Green
                }
            }
        }

        // Yuvarlatılmış köşeler uygula
        public void ApplyRoundedCorners()
        {
            // Form köşelerini yuvarla
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

        // Tab kontrolü özel çizim
        public void TabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            TabPage tabPage = tabControl.TabPages[e.Index];
            Rectangle tabBounds = tabControl.GetTabRect(e.Index);

            // Tab arkaplan rengi
            Brush backBrush = isDarkMode
                ? new SolidBrush(Color.FromArgb(45, 45, 45))
                : new SolidBrush(Color.FromArgb(240, 240, 240));

            // Seçili tab arkaplan rengi
            Brush selectedBrush = isDarkMode
                ? new SolidBrush(Color.FromArgb(75, 110, 175))
                : new SolidBrush(Color.FromArgb(0, 123, 255));

            // Tab metin rengi
            Brush textBrush = isDarkMode
                ? new SolidBrush(Color.White)
                : new SolidBrush(Color.FromArgb(50, 50, 50));

            // Seçili tab metin rengi
            Brush selectedTextBrush = new SolidBrush(Color.White);

            // Arkaplanı çiz
            g.FillRectangle(e.State == DrawItemState.Selected ? selectedBrush : backBrush, tabBounds);

            // Tab metni çiz
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            g.DrawString(tabPage.Text, new Font("Segoe UI", 9, FontStyle.Bold),
                         e.State == DrawItemState.Selected ? selectedTextBrush : textBrush,
                         tabBounds, stringFormat);
        }

        // Form yeniden boyutlandırıldığında bölgeyi güncelle
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ApplyRoundedCorners();
        }

        // Yeni bir cari kodu oluşturmak için metod
        public void LoadCariKodu()
        {
            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT CariKodu FROM Cari ORDER BY CariKodu ASC";
                using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        List<int> mevcutCariKodlar = new List<int>();
                        while (reader.Read())
                        {
                            if (int.TryParse(reader["CariKodu"].ToString(), out int cariKod))
                            {
                                mevcutCariKodlar.Add(cariKod);
                            }
                        }
                        int yeniCariKod = 1;
                        for (int i = 1; i <= mevcutCariKodlar.Count + 1; i++)
                        {
                            if (!mevcutCariKodlar.Contains(i))
                            {
                                yeniCariKod = i;
                                break;
                            }
                        }
                        txtCariKodu.Text = yeniCariKod.ToString("D5");
                    }
                }
            }
            txtCariKodu.ReadOnly = true;
        }

        // Düzenleme modundaysa cari bilgilerini yükleyen metod
        public void LoadCariDetails()
        {
            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Cari WHERE CariKodu = @CariKodu";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CariKodu", cariKodu);
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Tüm bilgileri doldur
                                txtCariKodu.Text = reader["CariKodu"].ToString();
                                txtUnvani.Text = reader["Unvani"].ToString();
                                txtYetkili.Text = reader["Yetkili"].ToString();
                                txtTelefon.Text = reader["Telefon"].ToString();
                                txtGsm.Text = reader["Gsm"].ToString();
                                txtFaks.Text = reader["Faks"].ToString();
                                txtAdres.Text = reader["Adres"].ToString();
                                txtIl.Text = reader["Il"].ToString();
                                txtIlce.Text = reader["Ilce"].ToString();
                                txtVergiDairesi.Text = reader["VergiDairesi"].ToString();
                                txtVergiNo.Text = reader["VergiNo"].ToString();
                                txtEposta.Text = reader["Eposta"].ToString();
                                txtWeb.Text = reader["Web"].ToString();
                                txtCariLimit.Text = reader["CariLimit"].ToString();

                                // ComboBox'tan değeri seç
                                string hesapDurumu = reader["Hesap"].ToString();
                                for (int i = 0; i < cmbHesap.Items.Count; i++)
                                {
                                    if (cmbHesap.Items[i].ToString() == hesapDurumu)
                                    {
                                        cmbHesap.SelectedIndex = i;
                                        break;
                                    }
                                }

                                // Hesap durumuna göre ComboBox rengini ayarla
                                UpdateHesapStatusColor();

                                // Vade Günü
                                if (int.TryParse(reader["VadeGunu"].ToString(), out int vadeGunu))
                                {
                                    vadegun.Value = vadeGunu;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Cari bilgisi bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Cari bilgileri yüklenirken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            txtCariKodu.ReadOnly = true; // Cari kodunun düzenlenememesi için
        }

        // Parametreleri eklemek için yardımcı metod
        public void AddParameters(SQLiteCommand command)
        {
            command.Parameters.AddWithValue("@CariKodu", txtCariKodu.Text);
            command.Parameters.AddWithValue("@Unvani", txtUnvani.Text);
            command.Parameters.AddWithValue("@Yetkili", txtYetkili.Text);
            command.Parameters.AddWithValue("@Telefon", txtTelefon.Text);
            command.Parameters.AddWithValue("@Gsm", txtGsm.Text);
            command.Parameters.AddWithValue("@Faks", txtFaks.Text);
            command.Parameters.AddWithValue("@Adres", txtAdres.Text);
            command.Parameters.AddWithValue("@Il", txtIl.Text);
            command.Parameters.AddWithValue("@Ilce", txtIlce.Text);
            command.Parameters.AddWithValue("@VergiDairesi", txtVergiDairesi.Text);
            command.Parameters.AddWithValue("@VergiNo", txtVergiNo.Text);
            command.Parameters.AddWithValue("@Eposta", txtEposta.Text);
            command.Parameters.AddWithValue("@Web", txtWeb.Text);
            command.Parameters.AddWithValue("@CariLimit", txtCariLimit.Text);
            command.Parameters.AddWithValue("@Hesap", cmbHesap.SelectedItem?.ToString());
            command.Parameters.AddWithValue("@VadeGunu", (int)vadegun.Value);
            command.Parameters.AddWithValue("@VadeTarihi", DateTime.Today.AddDays((int)vadegun.Value));
        }

        // Vazgeç butonuna basıldığında
        public void vazgec_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // Form yüklendiğinde
        public void FormCariEkle_Load(object sender, EventArgs e)
        {
            // Tab sayfalarını seçili yap
            tabControl.SelectedIndex = 0;

            // Hesap durumu renk ayarını başlangıçta uygula
            UpdateHesapStatusColor();
        }

        // Form kapatılırken
        public void FormCariEkle_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult != DialogResult.OK && this.DialogResult != DialogResult.Cancel)
            {
                e.Cancel = true;
                MessageBox.Show("Lütfen işlemi tamamlayınız veya iptal ediniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Kaydet butonuna basıldığında
        public void btnKaydet_Click(object sender, EventArgs e)
        {
            // Zorunlu alanları kontrol et
            if (string.IsNullOrWhiteSpace(txtUnvani.Text))
            {
                MessageBox.Show("Lütfen unvanı giriniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl.SelectedIndex = 0; // Temel bilgiler tabına git
                txtUnvani.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCariLimit.Text) || !decimal.TryParse(txtCariLimit.Text, out decimal cariLimit) || cariLimit <= 0)
            {
                MessageBox.Show("Lütfen geçerli bir Cari Limit giriniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl.SelectedIndex = 2; // Hesap Ayarları tabına git
                txtCariLimit.Focus();
                return;
            }

            progressBar.Visible = true;
            progressBar.Value = 20;

            // Kullanıcının seçtiği hesap durumunu sakla
            string selectedHesapDurumu = cmbHesap.SelectedItem?.ToString() ?? "Aktif Hesap";

            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    progressBar.Value = 40;

                    if (isEditMode)
                    {
                        string updateQuery = @"UPDATE Cari SET Unvani = @Unvani, Yetkili = @Yetkili, Telefon = @Telefon, Gsm = @Gsm,
                    Faks = @Faks, Adres = @Adres, Il = @Il, Ilce = @Ilce, VergiDairesi = @VergiDairesi, VergiNo = @VergiNo,
                    Eposta = @Eposta, Web = @Web, CariLimit = @CariLimit, Hesap = @Hesap, VadeGunu = @VadeGunu, VadeTarihi = @VadeTarihi
                    WHERE CariKodu = @CariKodu";
                        using (SQLiteCommand command = new SQLiteCommand(updateQuery, connection))
                        {
                            AddParameters(command);
                            // Hesap durumunu elle ayarla
                            command.Parameters["@Hesap"].Value = selectedHesapDurumu;
                            command.ExecuteNonQuery();
                            progressBar.Value = 80;

                            // ÖNEMLİ: GuncelleBakiye yerine sadece bakiyeyi hesaplayalım ama hesap durumunu değiştirmeyelim
                            HesaplaBakiye(txtCariKodu.Text, connection, false);

                            MessageBox.Show("Cari başarıyla güncellendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        string insertQuery = @"INSERT INTO Cari (CariKodu, Unvani, Yetkili, Telefon, Gsm, Faks, Adres, Il, Ilce, VergiDairesi,
                    VergiNo, Eposta, Web, CariLimit, Hesap, Tarih, VadeGunu, VadeTarihi, Bakiye)
                    VALUES (@CariKodu, @Unvani, @Yetkili, @Telefon, @Gsm, @Faks, @Adres, @Il, @Ilce, @VergiDairesi, @VergiNo,
                    @Eposta, @Web, @CariLimit, @Hesap, @Tarih, @VadeGunu, @VadeTarihi, 0)";
                        using (SQLiteCommand command = new SQLiteCommand(insertQuery, connection))
                        {
                            AddParameters(command);
                            // Hesap durumunu elle ayarla
                            command.Parameters["@Hesap"].Value = selectedHesapDurumu;
                            command.Parameters.AddWithValue("@Tarih", DateTime.Now);
                            command.Parameters.AddWithValue("@Bakiye", 0); // Yeni cari için başlangıç bakiyesi 0
                            command.ExecuteNonQuery();
                            progressBar.Value = 80;

                            MessageBox.Show("Cari başarıyla eklendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }

                    // Form4'teki DataGridView'i güncelle
                    if (Application.OpenForms["Form4"] is Form4 form4)
                    {
                        form4.RefreshDataGridView();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("İşlem sırasında hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    progressBar.Value = 100;
                    System.Threading.Thread.Sleep(300); // Görsel geri bildirim için
                    progressBar.Visible = false;
                }
            }

            this.DialogResult = DialogResult.OK; // Formu kapat
        }

        // Bakiyeyi hesaplayan ama hesap durumunu değiştirmeyen yeni metod
        public void HesaplaBakiye(string cariKodu, SQLiteConnection connection, bool updateHesapDurumu = true)
        {
            try
            {
                // ✅ Cari hareketlerinden toplam borç ve tahsilatı al
                decimal toplamBorc = 0;
                decimal toplamTahsilat = 0;
                string query = "SELECT COALESCE(SUM(Borc), 0) AS ToplamBorc, COALESCE(SUM(Tahsilat), 0) AS ToplamTahsilat FROM cari_hareketleri WHERE Cari_Kodu = @CariKodu";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CariKodu", cariKodu);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            decimal.TryParse(reader["ToplamBorc"]?.ToString(), out toplamBorc);
                            decimal.TryParse(reader["ToplamTahsilat"]?.ToString(), out toplamTahsilat);
                        }
                    }
                }

                // ✅ Yeni bakiye hesapla
                decimal yeniBakiye = toplamBorc - toplamTahsilat;

                // ✅ Cari tablosundaki `Bakiye` sütununu güncelle
                string updateQuery = "UPDATE Cari SET Bakiye = @YeniBakiye WHERE CariKodu = @CariKodu";
                using (SQLiteCommand updateCommand = new SQLiteCommand(updateQuery, connection))
                {
                    updateCommand.Parameters.AddWithValue("@YeniBakiye", yeniBakiye);
                    updateCommand.Parameters.AddWithValue("@CariKodu", cariKodu);
                    updateCommand.ExecuteNonQuery();
                }

                // Eğer hesap durumu güncellemesi isteniyorsa
                if (updateHesapDurumu)
                {
                    PasifHesapKontrol(cariKodu);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bakiye hesaplanırken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        

        internal void PasifHesapYap(string cariKodu)
        {
            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE Cari SET Hesap = 'Pasif Hesap' WHERE CariKodu = @CariKodu";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CariKodu", cariKodu);
                        command.ExecuteNonQuery();
                    }

                    // Güncel bakiyeyi al ve ekrana yansıt
                    decimal guncelBakiye = 0;
                    using (SQLiteCommand command = new SQLiteCommand("SELECT Bakiye FROM Cari WHERE CariKodu = @CariKodu", connection))
                    {
                        command.Parameters.AddWithValue("@CariKodu", cariKodu);
                        object result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            guncelBakiye = Convert.ToDecimal(result);
                        }
                    }

                    // Hesap durumunu ComboBox'ta güncelle
                    if (this.cariKodu == cariKodu && cmbHesap != null)
                    {
                        for (int i = 0; i < cmbHesap.Items.Count; i++)
                        {
                            if (cmbHesap.Items[i].ToString() == "Pasif Hesap")
                            {
                                cmbHesap.SelectedIndex = i;
                                break;
                            }
                        }
                        // Rengi güncelle
                        UpdateHesapStatusColor();
                    }

                    // Form4'teki DataGridView'i güncelle
                    if (Application.OpenForms["Form4"] is Form4 form4)
                    {
                        form4.RefreshDataGridView();
                    }

                    // Modern mesaj kutusu
                    MessageBox.Show($"Cari Kodu: {cariKodu} - Pasif Hesap Yapıldı!\nGüncel Bakiye: {guncelBakiye:C2}",
                                    "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("İşlem sırasında hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        internal void PasifHesapKontrol(string cariKodu)
        {
            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    // ✅ Hesap, Bakiye ve CariLimit'i tek sorguda al
                    string mevcutDurum = "";
                    decimal bakiye = 0;
                    decimal cariLimiti = 0;
                    string query = "SELECT Hesap, Bakiye, CariLimit FROM Cari WHERE CariKodu = @CariKodu";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CariKodu", cariKodu);
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                mevcutDurum = reader["Hesap"]?.ToString() ?? "";
                                decimal.TryParse(reader["Bakiye"]?.ToString(), out bakiye);
                                decimal.TryParse(reader["CariLimit"]?.ToString(), out cariLimiti);
                            }
                        }
                    }

                    // ✅ Yeni hesap durumunu belirle
                    string yeniDurum = bakiye < cariLimiti ? "Aktif Hesap" : "Pasif Hesap";

                    // ✅ Eğer hesap durumu değiştiyse, veritabanını güncelle ve mesaj göster
                    if (mevcutDurum != yeniDurum)
                    {
                        using (SQLiteCommand command = new SQLiteCommand("UPDATE Cari SET Hesap = @YeniDurum WHERE CariKodu = @CariKodu", connection))
                        {
                            command.Parameters.AddWithValue("@YeniDurum", yeniDurum);
                            command.Parameters.AddWithValue("@CariKodu", cariKodu);
                            command.ExecuteNonQuery();
                        }

                        // ✅ Eğer Carihareketler formu açıksa, `UpdateFirmaAdi()` çağır
                        if (Application.OpenForms["Carihareketler"] is Carihareketler cariForm)
                        {
                            cariForm.UpdateFirmaAdi();
                        }

                        // ✅ `cmbHesap` null değilse güncelle
                        if (this.cariKodu == cariKodu && cmbHesap != null)
                        {
                            // ComboBox'tan değeri seç
                            for (int i = 0; i < cmbHesap.Items.Count; i++)
                            {
                                if (cmbHesap.Items[i].ToString() == yeniDurum)
                                {
                                    cmbHesap.SelectedIndex = i;
                                    break;
                                }
                            }

                            // Rengi güncelle
                            UpdateHesapStatusColor();
                        }

                        // ✅ Form4'teki DataGridView'i güncelle
                        if (Application.OpenForms["Form4"] is Form4 form4)
                        {
                            form4.RefreshDataGridView();
                        }

                        // ✅ Yalnızca hesap durumu değiştiyse mesaj göster
                        MessageBox.Show($"📢 Cari Kodu: {cariKodu}\n" +
                                        $"🔵 Yeni Hesap Durumu: {yeniDurum}\n" +
                                        $"🔴 Bakiye: {bakiye:C2}\n" +
                                        $"🟢 Limit: {cariLimiti:C2}",
                                        "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("İşlem sırasında hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        internal void GuncelleBakiye(string cariKodu)
        {
            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    // ✅ Cari hareketlerinden toplam borç ve tahsilatı al
                    decimal toplamBorc = 0;
                    decimal toplamTahsilat = 0;
                    string query = "SELECT COALESCE(SUM(Borc), 0) AS ToplamBorc, COALESCE(SUM(Tahsilat), 0) AS ToplamTahsilat FROM cari_hareketleri WHERE Cari_Kodu = @CariKodu";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CariKodu", cariKodu);
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                decimal.TryParse(reader["ToplamBorc"]?.ToString(), out toplamBorc);
                                decimal.TryParse(reader["ToplamTahsilat"]?.ToString(), out toplamTahsilat);
                            }
                        }
                    }

                    // ✅ Yeni bakiye hesapla
                    decimal yeniBakiye = toplamBorc - toplamTahsilat;

                    // ✅ Cari tablosundaki `Bakiye` sütununu güncelle
                    string updateQuery = "UPDATE Cari SET Bakiye = @YeniBakiye WHERE CariKodu = @CariKodu";
                    using (SQLiteCommand updateCommand = new SQLiteCommand(updateQuery, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@YeniBakiye", yeniBakiye);
                        updateCommand.Parameters.AddWithValue("@CariKodu", cariKodu);
                        updateCommand.ExecuteNonQuery();
                    }

                    // ✅ Bakiye güncellendikten sonra hesap durumunu kontrol et
                    PasifHesapKontrol(cariKodu);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bakiye güncellenirken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Başlık çubuğu fare olayları (form taşıma)
        public void titleBar_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            dragStartPoint = new Point(e.X, e.Y);
        }

        public void titleBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point diff = new Point(e.X - dragStartPoint.X, e.Y - dragStartPoint.Y);
                this.Location = new Point(this.Location.X + diff.X, this.Location.Y + diff.Y);
            }
        }

        public void titleBar_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        // Kapat butonu olayı
        public void closeButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}