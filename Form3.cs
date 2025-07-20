using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Veresiye2025
{
    public partial class Form3 : Form
    {
        public int firmaID; // Düzenlenecek firmanın ID'si
        public bool isEditMode; // Düzenleme modunda mı kontrolü
        public bool isDarkMode = false; // Tema durumu

        // Form taşıma için gerekli API çağrıları
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        // Yuvarlatılmış köşeler için gerekli API çağrısı
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        public static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect,
            int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        // Yeni yapıcı: firmaID'yi alarak formun düzenleme modunda açılmasını sağlar
        public Form3(int firmaID = 0)
        {
            InitializeComponent();
            this.firmaID = firmaID;
            isEditMode = firmaID != 0; // firmaID sıfır değilse düzenleme modundadır

            // Form yüklendiğinde yuvarlatılmış köşeleri uygulamak için
            this.Load += Form3_Load;
            this.ResizeRedraw = true;
        }

        // Form yüklendiğinde çalışacak olay işleyicisi
        public void Form3_Load(object sender, EventArgs e)
        {
            // Yuvarlatılmış köşeleri uygula
            ApplyRoundedCorners();

            // Text girişleri için doğrulama eventlerini ekle
            ConfigureValidations();

            // İl-İlçe otomatik tamamlama
            ConfigureAutoComplete();

            // Form başlığını ayarla
            lblFormTitle.Text = isEditMode ? "Firma Düzenle" : "Firma Kayıt";

            // Tema uygula
            ApplyTheme(isDarkMode);

            // Para birimi combobox'ını doldur ve varsayılan TRY seç
            if (!isEditMode)
            {
                cmbParaBirimi.SelectedIndex = 2; // TRY seçili
                statusPanel.Visible = true;
                statusLabel.Text = "Yeni firma kaydı için bilgileri giriniz";
            }

            if (isEditMode)
            {
                // firmaID mevcutsa, firma bilgilerini veritabanından yükle
                LoadFirmaBilgileri();
                // Düzenleme modunda kaydetme butonu metnini güncelle
                btnfirmakayit.Text = "Güncelle";
                statusPanel.Visible = true;
                statusLabel.Text = "Firma bilgilerini güncelleyebilirsiniz";
            }
        }

        public void ConfigureValidations()
        {
            // Vergi No alanı için sadece rakam girişine izin ver
            txtVergiNo.KeyPress += (s, evt) => {
                if (!char.IsControl(evt.KeyChar) && !char.IsDigit(evt.KeyChar))
                {
                    evt.Handled = true;
                }
            };

            // Firma bütçesi için sadece rakam ve virgül girişine izin ver
            txtFirmaButcesi.KeyPress += (s, evt) => {
                if (!char.IsControl(evt.KeyChar) && !char.IsDigit(evt.KeyChar) && evt.KeyChar != ',' && evt.KeyChar != '.')
                {
                    evt.Handled = true;
                }
            };

            // Telefon numarası için sadece rakam, + ve - girişine izin ver
            txtTelefonNo.KeyPress += (s, evt) => {
                if (!char.IsControl(evt.KeyChar) && !char.IsDigit(evt.KeyChar) && evt.KeyChar != '+' && evt.KeyChar != '-' && evt.KeyChar != ' ')
                {
                    evt.Handled = true;
                }
            };

            // Zorunlu alanların rengini değiştirme
            txtFirmaAdi.TextChanged += (s, evt) => {
                if (string.IsNullOrWhiteSpace(txtFirmaAdi.Text))
                {
                    txtFirmaAdi.BackColor = Color.FromArgb(255, 236, 236);
                }
                else
                {
                    txtFirmaAdi.BackColor = Color.FromArgb(248, 249, 250);
                }
            };

            txtTelefonNo.TextChanged += (s, evt) => {
                if (string.IsNullOrWhiteSpace(txtTelefonNo.Text))
                {
                    txtTelefonNo.BackColor = Color.FromArgb(255, 236, 236);
                }
                else
                {
                    txtTelefonNo.BackColor = Color.FromArgb(248, 249, 250);
                }
            };
        }

        public void ConfigureAutoComplete()
        {
            // İl için otomatik tamamlama
            AutoCompleteStringCollection ilCollection = new AutoCompleteStringCollection();
            ilCollection.AddRange(new string[] {
                "Adana", "Adıyaman", "Afyonkarahisar", "Ağrı", "Amasya", "Ankara", "Antalya", "Artvin",
                "Aydın", "Balıkesir", "Bilecik", "Bingöl", "Bitlis", "Bolu", "Burdur", "Bursa", "Çanakkale",
                "Çankırı", "Çorum", "Denizli", "Diyarbakır", "Edirne", "Elazığ", "Erzincan", "Erzurum",
                "Eskişehir", "Gaziantep", "Giresun", "Gümüşhane", "Hakkari", "Hatay", "Isparta", "Mersin",
                "İstanbul", "İzmir", "Kars", "Kastamonu", "Kayseri", "Kırklareli", "Kırşehir", "Kocaeli",
                "Konya", "Kütahya", "Malatya", "Manisa", "Kahramanmaraş", "Mardin", "Muğla", "Muş", "Nevşehir",
                "Niğde", "Ordu", "Rize", "Sakarya", "Samsun", "Siirt", "Sinop", "Sivas", "Tekirdağ", "Tokat",
                "Trabzon", "Tunceli", "Şanlıurfa", "Uşak", "Van", "Yozgat", "Zonguldak", "Aksaray", "Bayburt",
                "Karaman", "Kırıkkale", "Batman", "Şırnak", "Bartın", "Ardahan", "Iğdır", "Yalova", "Karabük",
                "Kilis", "Osmaniye", "Düzce"
            });
            txtIl.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtIl.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtIl.AutoCompleteCustomSource = ilCollection;

            // İlçe için İstanbul'un ilçelerini örnek olarak ekleyelim
            txtIl.TextChanged += (s, evt) => {
                AutoCompleteStringCollection ilceCollection = new AutoCompleteStringCollection();

                if (txtIl.Text.ToLower() == "istanbul")
                {
                    ilceCollection.AddRange(new string[] {
                        "Adalar", "Arnavutköy", "Ataşehir", "Avcılar", "Bağcılar", "Bahçelievler", "Bakırköy",
                        "Başakşehir", "Bayrampaşa", "Beşiktaş", "Beykoz", "Beylikdüzü", "Beyoğlu", "Büyükçekmece",
                        "Çatalca", "Çekmeköy", "Esenler", "Esenyurt", "Eyüpsultan", "Fatih", "Gaziosmanpaşa",
                        "Güngören", "Kadıköy", "Kağıthane", "Kartal", "Küçükçekmece", "Maltepe", "Pendik",
                        "Sancaktepe", "Sarıyer", "Silivri", "Sultanbeyli", "Sultangazi", "Şile", "Şişli",
                        "Tuzla", "Ümraniye", "Üsküdar", "Zeytinburnu"
                    });
                }
                else if (txtIl.Text.ToLower() == "ankara")
                {
                    ilceCollection.AddRange(new string[] {
                        "Akyurt", "Altındağ", "Ayaş", "Balâ", "Beypazarı", "Çamlıdere", "Çankaya", "Çubuk",
                        "Elmadağ", "Etimesgut", "Evren", "Gölbaşı", "Güdül", "Haymana", "Kalecik", "Kahramankazan",
                        "Keçiören", "Kızılcahamam", "Mamak", "Nallıhan", "Polatlı", "Pursaklar", "Sincan",
                        "Şereflikoçhisar", "Yenimahalle"
                    });
                }

                txtIlce.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtIlce.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtIlce.AutoCompleteCustomSource = ilceCollection;
            };

            // Ülke için otomatik tamamlama
            AutoCompleteStringCollection ulkeCollection = new AutoCompleteStringCollection();
            ulkeCollection.AddRange(new string[] {
                "Türkiye", "Almanya", "Fransa", "İngiltere", "İtalya", "İspanya", "Rusya", "ABD",
                "Kanada", "Japonya", "Çin", "Hindistan", "Brezilya", "Meksika", "Güney Kore"
            });
            txtUlkesi.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtUlkesi.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtUlkesi.AutoCompleteCustomSource = ulkeCollection;
        }

        public void ApplyRoundedCorners()
        {
            // Form köşelerini yuvarla (20px radius)
            Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

            // Panel köşelerini yuvarla (10px radius)
            formPanel.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, formPanel.Width, formPanel.Height, 10, 10));

            // Status panel köşelerini yuvarla (5px radius)
            statusPanel.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, statusPanel.Width, statusPanel.Height, 5, 5));
        }

        // Form yeniden boyutlandırıldığında bölgeyi güncelle
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (this.Region != null)
            {
                this.Region.Dispose();
            }
            ApplyRoundedCorners();
        }

        public void ApplyTheme(bool darkMode)
        {
            if (darkMode)
            {
                // Ana form
                this.BackColor = Color.FromArgb(18, 18, 18);

                // Form paneli
                formPanel.BackColor = Color.FromArgb(30, 30, 30);

                // Başlık çubuğu
                titleBar.BackColor = Color.FromArgb(24, 24, 24);
                lblFormTitle.ForeColor = Color.White;
                closeButton.ForeColor = Color.White;

                // Status panel
                statusPanel.BackColor = Color.FromArgb(40, 40, 40);
                statusLabel.ForeColor = Color.LightGray;

                // TextBox'lar ve diğer kontroller
                foreach (Control control in formPanel.Controls)
                {
                    if (control is TextBox textBox)
                    {
                        textBox.BackColor = Color.FromArgb(45, 45, 45);
                        textBox.ForeColor = Color.White;
                        textBox.BorderStyle = BorderStyle.FixedSingle;
                    }
                    else if (control is ComboBox comboBox)
                    {
                        comboBox.BackColor = Color.FromArgb(45, 45, 45);
                        comboBox.ForeColor = Color.White;
                    }
                    else if (control is Label label)
                    {
                        label.ForeColor = Color.LightGray;
                    }
                }

                // Butonlar
                btnfirmakayit.FillColor = Color.FromArgb(75, 110, 175);
                btnVazgec.FillColor = Color.FromArgb(60, 65, 70);
            }
            else
            {
                // Ana form
                this.BackColor = Color.FromArgb(245, 247, 250);

                // Form paneli
                formPanel.BackColor = Color.White;

                // Başlık çubuğu
                titleBar.BackColor = Color.FromArgb(0, 123, 255);
                lblFormTitle.ForeColor = Color.White;
                closeButton.ForeColor = Color.White;

                // Status panel
                statusPanel.BackColor = Color.FromArgb(245, 247, 250);
                statusLabel.ForeColor = Color.FromArgb(100, 100, 100);

                // TextBox'lar ve diğer kontroller
                foreach (Control control in formPanel.Controls)
                {
                    if (control is TextBox textBox)
                    {
                        textBox.BackColor = Color.FromArgb(248, 249, 250);
                        textBox.ForeColor = Color.Black;
                        // Yuvarlatılmış köşeler için daha modern sınır stili
                        textBox.BorderStyle = BorderStyle.FixedSingle;
                    }
                    else if (control is ComboBox comboBox)
                    {
                        comboBox.BackColor = Color.FromArgb(248, 249, 250);
                        comboBox.ForeColor = Color.Black;
                    }
                    else if (control is Label label)
                    {
                        label.ForeColor = Color.FromArgb(60, 60, 60);
                    }
                }

                // Butonlar
                btnfirmakayit.FillColor = Color.FromArgb(0, 123, 255);
                btnVazgec.FillColor = Color.FromArgb(108, 117, 125);
            }

            // Zorunlu alanları vurgula
            HighlightRequiredFields();
        }

        // Zorunlu alanları vurgulama
        public void HighlightRequiredFields()
        {
            // Firma adı ve telefon no boşsa kırmızımsı arka plan ile vurgula
            if (string.IsNullOrWhiteSpace(txtFirmaAdi.Text))
            {
                txtFirmaAdi.BackColor = Color.FromArgb(255, 236, 236);
            }

            if (string.IsNullOrWhiteSpace(txtTelefonNo.Text))
            {
                txtTelefonNo.BackColor = Color.FromArgb(255, 236, 236);
            }
        }

        // Firma bilgilerini veritabanından yükler
        public void LoadFirmaBilgileri()
        {
            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT FirmaAdi, TelefonNo, FaxNo, Adres, Il, Ilce, VergiNo, VergiDairesi, FirmaButcesi, Ulkesi, PBirimi FROM Firmalar WHERE ID = @ID";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ID", firmaID);
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // TextBox'ları doldur
                                txtFirmaAdi.Text = reader["FirmaAdi"].ToString();
                                txtTelefonNo.Text = reader["TelefonNo"].ToString();
                                txtFaxNo.Text = reader["FaxNo"].ToString();
                                txtAdres.Text = reader["Adres"].ToString();
                                txtIl.Text = reader["Il"].ToString();
                                txtIlce.Text = reader["Ilce"].ToString();
                                txtVergiNo.Text = reader["VergiNo"].ToString();
                                txtVergiDairesi.Text = reader["VergiDairesi"].ToString();
                                txtFirmaButcesi.Text = reader["FirmaButcesi"].ToString();
                                txtUlkesi.Text = reader["Ulkesi"].ToString();

                                // Para birimini seç
                                string paraBirimi = reader["PBirimi"].ToString();
                                for (int i = 0; i < cmbParaBirimi.Items.Count; i++)
                                {
                                    if (cmbParaBirimi.Items[i].ToString() == paraBirimi)
                                    {
                                        cmbParaBirimi.SelectedIndex = i;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Firma bilgileri yüklenirken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Vergi numarasının doğruluğunu kontrol et
        public bool IsValidVergiNo(string vergiNo)
        {
            // Boşsa geçerli kabul et (zorunlu alan değil)
            if (string.IsNullOrEmpty(vergiNo))
                return true;

            // 10 veya 11 haneli olmalı
            if (vergiNo.Length != 10 && vergiNo.Length != 11)
                return false;

            // Sadece rakam içermeli
            foreach (char c in vergiNo)
            {
                if (!char.IsDigit(c))
                    return false;
            }

            return true;
        }

        // Firma kaydetme veya güncelleme işlemi
        public void btnfirmakayit_Click(object sender, EventArgs e)
        {
            string firmaAdi = txtFirmaAdi.Text.Trim();
            string telefonNo = txtTelefonNo.Text.Trim();
            string faxNo = txtFaxNo.Text.Trim();
            string adres = txtAdres.Text.Trim();
            string il = txtIl.Text.Trim();
            string ilce = txtIlce.Text.Trim();
            string vergiNo = txtVergiNo.Text.Trim();
            string vergiDairesi = txtVergiDairesi.Text.Trim();
            string firmaButcesi = txtFirmaButcesi.Text.Trim();
            string ulkesi = txtUlkesi.Text.Trim();
            string paraBirimi = cmbParaBirimi.SelectedItem?.ToString();

            // Giriş doğrulama
            List<string> errors = new List<string>();

            if (string.IsNullOrEmpty(firmaAdi))
            {
                errors.Add("Firma adı boş olamaz!");
                txtFirmaAdi.BackColor = Color.FromArgb(255, 236, 236);
                txtFirmaAdi.Focus();
            }

            if (string.IsNullOrEmpty(telefonNo))
            {
                errors.Add("Telefon numarası boş olamaz!");
                txtTelefonNo.BackColor = Color.FromArgb(255, 236, 236);
                if (errors.Count == 1) // Başka hata yoksa focus buraya
                    txtTelefonNo.Focus();
            }

            if (!IsValidVergiNo(vergiNo))
            {
                errors.Add("Vergi numarası 10 veya 11 haneli olmalıdır!");
                txtVergiNo.BackColor = Color.FromArgb(255, 236, 236);
                if (errors.Count == 1) // Başka hata yoksa focus buraya
                    txtVergiNo.Focus();
            }

            // Hata varsa kullanıcıya bildir ve işlemi durdur
            if (errors.Count > 0)
            {
                MessageBox.Show(string.Join("\n", errors), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Status mesajını güncelle
            statusPanel.Visible = true;
            statusLabel.Text = "İşlem yapılıyor...";
            Application.DoEvents(); // UI'ın güncellenmesini sağlar

            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    if (isEditMode)
                    {
                        // Düzenleme modundaysa, firma bilgilerini güncelle
                        string updateQuery = @"UPDATE Firmalar SET FirmaAdi = @FirmaAdi, TelefonNo = @TelefonNo, FaxNo = @FaxNo, Adres = @Adres,
                                               Il = @Il, Ilce = @Ilce, VergiNo = @VergiNo, VergiDairesi = @VergiDairesi, FirmaButcesi = @FirmaButcesi,
                                               Ulkesi = @Ulkesi, PBirimi = @PBirimi WHERE ID = @ID";
                        using (SQLiteCommand updateCmd = new SQLiteCommand(updateQuery, connection))
                        {
                            updateCmd.Parameters.AddWithValue("@FirmaAdi", firmaAdi);
                            updateCmd.Parameters.AddWithValue("@TelefonNo", telefonNo);
                            updateCmd.Parameters.AddWithValue("@FaxNo", faxNo);
                            updateCmd.Parameters.AddWithValue("@Adres", adres);
                            updateCmd.Parameters.AddWithValue("@Il", il);
                            updateCmd.Parameters.AddWithValue("@Ilce", ilce);
                            updateCmd.Parameters.AddWithValue("@VergiNo", vergiNo);
                            updateCmd.Parameters.AddWithValue("@VergiDairesi", vergiDairesi);
                            updateCmd.Parameters.AddWithValue("@FirmaButcesi", firmaButcesi);
                            updateCmd.Parameters.AddWithValue("@Ulkesi", ulkesi);
                            updateCmd.Parameters.AddWithValue("@PBirimi", paraBirimi);
                            updateCmd.Parameters.AddWithValue("@ID", firmaID);
                            updateCmd.ExecuteNonQuery();
                            MessageBox.Show("Firma bilgileri başarıyla güncellendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        // Yeni firma kaydetme işlemi
                        string insertQuery = @"INSERT INTO Firmalar (FirmaAdi, TelefonNo, FaxNo, Adres, Il, Ilce, VergiNo, VergiDairesi, FirmaButcesi, Ulkesi, PBirimi)
                                               VALUES (@FirmaAdi, @TelefonNo, @FaxNo, @Adres, @Il, @Ilce, @VergiNo, @VergiDairesi, @FirmaButcesi, @Ulkesi, @PBirimi)";
                        using (SQLiteCommand insertCmd = new SQLiteCommand(insertQuery, connection))
                        {
                            insertCmd.Parameters.AddWithValue("@FirmaAdi", firmaAdi);
                            insertCmd.Parameters.AddWithValue("@TelefonNo", telefonNo);
                            insertCmd.Parameters.AddWithValue("@FaxNo", faxNo);
                            insertCmd.Parameters.AddWithValue("@Adres", adres);
                            insertCmd.Parameters.AddWithValue("@Il", il);
                            insertCmd.Parameters.AddWithValue("@Ilce", ilce);
                            insertCmd.Parameters.AddWithValue("@VergiNo", vergiNo);
                            insertCmd.Parameters.AddWithValue("@VergiDairesi", vergiDairesi);
                            insertCmd.Parameters.AddWithValue("@FirmaButcesi", firmaButcesi);
                            insertCmd.Parameters.AddWithValue("@Ulkesi", ulkesi);
                            insertCmd.Parameters.AddWithValue("@PBirimi", paraBirimi);
                            insertCmd.ExecuteNonQuery();
                            MessageBox.Show("Firma kaydı başarılı!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    // Formu kapatma işlemi
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("İşlem sırasında bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    // Hata durumunda status mesajını güncelle
                    statusLabel.Text = "Hata oluştu!";
                }
            }
        }

        // Vazgeç butonu
        public void btnVazgec_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Kapat butonu
        public void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Form taşıma işlemleri
        public void titleBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, 0xA1, 0x2, 0);
            }
        }

        // ESC tuşu ile formu kapat
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            else if (keyData == Keys.Enter && !txtAdres.Focused) // Adres alanında enter tuşu çalışmasın (çok satırlı)
            {
                // Bir sonraki kontrole geç
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}