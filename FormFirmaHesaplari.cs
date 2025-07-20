using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Veresiye2025
{
    public partial class FormFirmaHesaplari : Form
    {
        // API çağrıları için gerekli metotlar
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        public static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect,
            int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        // TextBox için placeholder metni ayarlamak için
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);
        public const int EM_SETCUEBANNER = 0x1501;

        // Tema ve form durumu değişkenleri
        public bool isDarkMode = false;
        public int selectedFirmaID = 0;

        public FormFirmaHesaplari()
        {
            InitializeComponent();
            this.Load += FormFirmaHesaplari_Load;
            this.ResizeRedraw = true;
        }

        public void FormFirmaHesaplari_Load(object sender, EventArgs e)
        {
            // Yuvarlatılmış köşeleri uygula
            ApplyRoundedCorners();

            // Placeholder metni ayarla
            SendMessage(txtSearch.Handle, EM_SETCUEBANNER, 0, "Firma adı, telefon veya vergi no ile arama yapın...");

            // Listeleme olayları ve seçim değişikliklerini izle
            listViewFirmalar.SelectedIndexChanged += (s, ev) => UpdateButtonStates();

            // Firmaları yükle
            LoadFirmaData();
        }

        public void ApplyRoundedCorners()
        {
            // Form köşelerini yuvarla (20px)
            Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

            // Panel köşelerini yuvarla
            mainPanel.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, mainPanel.Width, mainPanel.Height, 10, 10));
            searchPanel.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, searchPanel.Width, searchPanel.Height, 10, 10));
            buttonPanel.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, buttonPanel.Width, buttonPanel.Height, 10, 10));
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (this.Region != null)
            {
                this.Region.Dispose();
            }
            ApplyRoundedCorners();
        }

        public void titleBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, 0xA1, 0x2, 0);
            }
        }

        public void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadFirmaData(txtSearch.Text);
        }

        public void LoadFirmaData(string searchText = "")
        {
            listViewFirmalar.Items.Clear();

            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"SELECT ID, FirmaAdi, TelefonNo, FaxNo, Adres, Il, Ilce, VergiNo, 
                           VergiDairesi, FirmaButcesi, Ulkesi, PBirimi 
                           FROM Firmalar";

                    // Arama metni varsa, WHERE koşulu ekle
                    if (!string.IsNullOrEmpty(searchText))
                    {
                        query += @" WHERE FirmaAdi LIKE @SearchText 
                           OR TelefonNo LIKE @SearchText 
                           OR VergiNo LIKE @SearchText";
                    }

                    query += " ORDER BY FirmaAdi";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        if (!string.IsNullOrEmpty(searchText))
                        {
                            command.Parameters.AddWithValue("@SearchText", "%" + searchText + "%");
                        }

                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                System.Windows.Forms.ListViewItem item = new System.Windows.Forms.ListViewItem(reader["ID"].ToString());
                                item.SubItems.Add(reader["FirmaAdi"].ToString());
                                item.SubItems.Add(reader["TelefonNo"].ToString());
                                item.SubItems.Add(reader["VergiNo"].ToString());
                                item.SubItems.Add(reader["Il"].ToString());
                                item.SubItems.Add(reader["Ilce"].ToString());
                                item.SubItems.Add(reader["FirmaButcesi"].ToString());
                                item.SubItems.Add(reader["PBirimi"].ToString());

                                // Dictionary kullanmak yerine Tag özelliğini doğrudan Dictionary<string, string> olarak ayarlayalım
                                item.Tag = new Dictionary<string, object>
                        {
                            { "FaxNo", reader["FaxNo"].ToString() },
                            { "Adres", reader["Adres"].ToString() },
                            { "VergiDairesi", reader["VergiDairesi"].ToString() },
                            { "Ulkesi", reader["Ulkesi"].ToString() }
                        };

                                listViewFirmalar.Items.Add(item);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Firma verileri yüklenirken hata oluştu: " + ex.Message, "Hata",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            // Sütun genişliklerini ayarla
            listViewFirmalar.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            // Butonları uygun duruma getir
            UpdateButtonStates();
        }

        public void UpdateButtonStates()
        {
            bool firmaSelected = listViewFirmalar.SelectedItems.Count > 0;
            btnDuzenle.Enabled = firmaSelected;
            btnSil.Enabled = firmaSelected;

            // Buton renklerini güncelle
            if (!firmaSelected)
            {
                btnDuzenle.FillColor = Color.FromArgb(40, 167, 69);
                btnSil.FillColor = Color.FromArgb(220, 53, 69);
                btnDuzenle.ForeColor = Color.White;
                btnSil.ForeColor = Color.White;
            }
        }

        public void BtnYeniFirma_Click(object sender, EventArgs e)
        {
            Form3 firmaForm = new Form3(); // Yeni firma ekleme formu
            firmaForm.ShowDialog();

            // Firma listesini yenile
            LoadFirmaData(txtSearch.Text);
        }

        public void BtnDuzenle_Click(object sender, EventArgs e)
        {
            if (listViewFirmalar.SelectedItems.Count > 0)
            {
                selectedFirmaID = Convert.ToInt32(listViewFirmalar.SelectedItems[0].Text);
                DuzenleFirma(selectedFirmaID);
            }
            else
            {
                MessageBox.Show("Lütfen düzenlemek için bir firma seçin.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void ListViewFirmalar_DoubleClick(object sender, EventArgs e)
        {
            if (listViewFirmalar.SelectedItems.Count > 0)
            {
                selectedFirmaID = Convert.ToInt32(listViewFirmalar.SelectedItems[0].Text);
                DuzenleFirma(selectedFirmaID);
            }
        }

        public void DuzenleFirma(int firmaID)
        {
            Form3 firmaForm = new Form3(firmaID); // Düzenleme modu
            firmaForm.ShowDialog();

            // Firma listesini yenile
            LoadFirmaData(txtSearch.Text);
        }

        public void BtnSil_Click(object sender, EventArgs e)
        {
            if (listViewFirmalar.SelectedItems.Count > 0)
            {
                int firmaID = Convert.ToInt32(listViewFirmalar.SelectedItems[0].Text);
                string firmaAdi = listViewFirmalar.SelectedItems[0].SubItems[1].Text;

                DialogResult result = MessageBox.Show($"{firmaAdi} firmasını silmek istediğinize emin misiniz?",
                    "Firma Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (DeleteFirma(firmaID))
                    {
                        MessageBox.Show($"{firmaAdi} firması başarıyla silindi.", "Bilgi",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Firma listesini yenile
                        LoadFirmaData(txtSearch.Text);
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen silmek için bir firma seçin.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public bool DeleteFirma(int firmaID)
        {
            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Önce bu firmaya bağlı diğer kayıtları kontrol et
                    string checkQuery = "SELECT COUNT(*) FROM Musteriler WHERE FirmaID = @FirmaID";
                    using (SQLiteCommand checkCmd = new SQLiteCommand(checkQuery, connection))
                    {
                        checkCmd.Parameters.AddWithValue("@FirmaID", firmaID);
                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                        if (count > 0)
                        {
                            MessageBox.Show("Bu firmaya bağlı müşteriler bulunmaktadır. Önce bu müşterileri silmelisiniz.",
                                "Bağlı Kayıtlar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }

                    // Firma silinebilir
                    string deleteQuery = "DELETE FROM Firmalar WHERE ID = @FirmaID";
                    using (SQLiteCommand deleteCmd = new SQLiteCommand(deleteQuery, connection))
                    {
                        deleteCmd.Parameters.AddWithValue("@FirmaID", firmaID);
                        int rowsAffected = deleteCmd.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Firma silinirken hata oluştu: " + ex.Message, "Hata",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        public void BtnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // ESC tuşu ile formu kapat
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}