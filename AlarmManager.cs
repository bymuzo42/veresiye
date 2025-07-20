using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace Veresiye2025
{
    public static class AlarmManager
    {
        // Alarm listesi formuna referans
        public static Form _alarmListeForm;

        // Alarm listesini gösteren ana metot
        public static void GosterAlarmListesi()
        {
            try
            {
                // Mevcut formu kontrol et
                if (_alarmListeForm != null && !_alarmListeForm.IsDisposed)
                {
                    // Form açıksa, formun durumunu normal yap, öne getir ve verilerini yenile
                    _alarmListeForm.WindowState = FormWindowState.Normal;
                    _alarmListeForm.BringToFront();
                    YenileAlarmVerileri();
                    return;
                }

                // Form açık değilse, yeni bir form oluştur
                // TabControl ve TabPage'leri içeren görünüm tasarla
                _alarmListeForm = new Form
                {
                    Text = "Alarm Listesi",
                    Size = new Size(700, 518),
                    StartPosition = FormStartPosition.CenterScreen,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    MaximizeBox = false,
                    MinimizeBox = false,
                    BackColor = Color.FromArgb(240, 240, 240),
                    ShowInTaskbar = false,
                    ShowIcon = false
                };

                // Form kapandığında referansı temizle
                _alarmListeForm.FormClosed += (s, e) => {
                    _alarmListeForm = null;
                };

                // TabControl oluştur
                TabControl tabControl = new TabControl
                {
                    Location = new Point(10, 10),
                    Size = new Size(675, 425),
                    Font = new Font("Segoe UI", 9.5F, FontStyle.Regular)
                };

                // Sekmeleri oluştur
                TabPage tabAktif = new TabPage
                {
                    Text = "Aktif Alarmlar",
                    BackColor = Color.White
                };
                TabPage tabErtelenen = new TabPage
                {
                    Text = "Ertelenenler",
                    BackColor = Color.White
                };
                TabPage tabTamamlanan = new TabPage
                {
                    Text = "Tamamlananlar",
                    BackColor = Color.White
                };

                // DataGridView'ları oluştur ve ekle
                Guna2DataGridView dgvAktif = OlusturDataGridView();
                dgvAktif.Dock = DockStyle.Fill;
                tabAktif.Controls.Add(dgvAktif);

                Guna2DataGridView dgvErtelenen = OlusturDataGridView();
                dgvErtelenen.Dock = DockStyle.Fill;
                tabErtelenen.Controls.Add(dgvErtelenen);

                Guna2DataGridView dgvTamamlanan = OlusturDataGridView();
                dgvTamamlanan.Dock = DockStyle.Fill;
                tabTamamlanan.Controls.Add(dgvTamamlanan);

                // Sağ tık menülerini ekle
                EkleSagTikMenusu(dgvAktif, "Aktif", dgvAktif, dgvErtelenen, dgvTamamlanan);
                EkleSagTikMenusu(dgvErtelenen, "Ertelenen", dgvAktif, dgvErtelenen, dgvTamamlanan);
                EkleSagTikMenusu(dgvTamamlanan, "Tamamlanan", dgvAktif, dgvErtelenen, dgvTamamlanan);

                // Sekmeleri TabControl'e ekle
                tabControl.Controls.Add(tabAktif);
                tabControl.Controls.Add(tabErtelenen);
                tabControl.Controls.Add(tabTamamlanan);

                // TabControl'ü forma ekle
                _alarmListeForm.Controls.Add(tabControl);

                // Butonları oluştur ve ekle
                int buttonY = 435;
                int buttonWidth = 120;
                int buttonSpacing = 15;

                Guna2Button btnTamamlandi = new Guna2Button
                {
                    Text = "Tamamlandı",
                    Size = new Size(buttonWidth, 40),
                    Location = new Point(10, buttonY),
                    BorderRadius = 10,
                    FillColor = Color.FromArgb(40, 167, 69),
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    ForeColor = Color.White
                };

                Guna2Button btnErtele = new Guna2Button
                {
                    Text = "Ertele",
                    Size = new Size(buttonWidth, 40),
                    Location = new Point(10 + buttonWidth + buttonSpacing, buttonY),
                    BorderRadius = 10,
                    FillColor = Color.FromArgb(0, 123, 255),
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    ForeColor = Color.White
                };

                Guna2Button btnSil = new Guna2Button
                {
                    Text = "Sil",
                    Size = new Size(buttonWidth, 40),
                    Location = new Point(10 + (buttonWidth + buttonSpacing) * 2, buttonY),
                    BorderRadius = 10,
                    FillColor = Color.FromArgb(220, 53, 69),
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    ForeColor = Color.White
                };

                Guna2Button btnYeni = new Guna2Button
                {
                    Text = "Yeni Alarm",
                    Size = new Size(buttonWidth, 40),
                    Location = new Point(10 + (buttonWidth + buttonSpacing) * 3, buttonY),
                    BorderRadius = 10,
                    FillColor = Color.FromArgb(255, 153, 0),
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    ForeColor = Color.White
                };

                Guna2Button btnKapat = new Guna2Button
                {
                    Text = "Kapat",
                    Size = new Size(buttonWidth, 40),
                    Location = new Point(10 + (buttonWidth + buttonSpacing) * 4, buttonY),
                    BorderRadius = 10,
                    FillColor = Color.FromArgb(108, 117, 125),
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    ForeColor = Color.White
                };

                // Buton olaylarını ekle
                btnTamamlandi.Click += (s, e) => {
                    if (tabControl.SelectedIndex == 0 && dgvAktif.SelectedRows.Count > 0)
                    {
                        int id = Convert.ToInt32(dgvAktif.SelectedRows[0].Cells["ID"].Value);
                        GuncelleAlarmDurumu(id, "Tamamlandı");
                        YenileAlarmVerileri();
                    }
                    else if (tabControl.SelectedIndex == 1 && dgvErtelenen.SelectedRows.Count > 0)
                    {
                        int id = Convert.ToInt32(dgvErtelenen.SelectedRows[0].Cells["ID"].Value);
                        GuncelleAlarmDurumu(id, "Tamamlandı");
                        YenileAlarmVerileri();
                    }
                };

                btnErtele.Click += (s, e) => {
                    Guna2DataGridView currentGrid = null;
                    if (tabControl.SelectedIndex == 0)
                        currentGrid = dgvAktif;
                    else if (tabControl.SelectedIndex == 1)
                        currentGrid = dgvErtelenen;

                    if (currentGrid != null && currentGrid.SelectedRows.Count > 0)
                    {
                        int id = Convert.ToInt32(currentGrid.SelectedRows[0].Cells["ID"].Value);
                        GosterErtelemeFormu(id);
                        YenileAlarmVerileri();
                    }
                };

                btnSil.Click += (s, e) => {
                    Guna2DataGridView currentGrid = null;
                    if (tabControl.SelectedIndex == 0)
                        currentGrid = dgvAktif;
                    else if (tabControl.SelectedIndex == 1)
                        currentGrid = dgvErtelenen;
                    else if (tabControl.SelectedIndex == 2)
                        currentGrid = dgvTamamlanan;

                    if (currentGrid != null && currentGrid.SelectedRows.Count > 0)
                    {
                        int id = Convert.ToInt32(currentGrid.SelectedRows[0].Cells["ID"].Value);
                        if (MessageBox.Show("Seçili alarmı silmek istediğinizden emin misiniz?", "Onay",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            if (SilAlarm(id))
                            {
                                YenileAlarmVerileri();
                            }
                        }
                    }
                };

                btnYeni.Click += (s, e) => {
                    try
                    {
                        _alarmListeForm.Hide();
                        Alarmkur yeniAlarmForm = new Alarmkur();
                        yeniAlarmForm.ShowInTaskbar = false;
                        yeniAlarmForm.ShowIcon = false;
                        if (yeniAlarmForm.ShowDialog() == DialogResult.OK)
                        {
                            YenileAlarmVerileri();
                        }
                        _alarmListeForm.Show();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Yeni alarm oluşturulurken hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                };

                btnKapat.Click += (s, e) => {
                    _alarmListeForm.Close();
                };

                // Butonları forma ekle
                _alarmListeForm.Controls.Add(btnTamamlandi);
                _alarmListeForm.Controls.Add(btnErtele);
                _alarmListeForm.Controls.Add(btnSil);
                _alarmListeForm.Controls.Add(btnYeni);
                _alarmListeForm.Controls.Add(btnKapat);

                // Verileri yükle
                YukleAktifAlarmlar(dgvAktif);
                YukleErtelenenAlarmlar(dgvErtelenen);
                YukleTamamlananAlarmlar(dgvTamamlanan);

                // Formu göster
                _alarmListeForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Alarm listesi gösterilirken bir hata oluştu: {ex.Message}",
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Alarm verilerini yenileme
        public static void YenileAlarmVerileri()
        {
            try
            {
                if (_alarmListeForm == null || _alarmListeForm.IsDisposed)
                    return;

                // TabControl kontrolünü bul
                TabControl tabControl = null;
                foreach (Control control in _alarmListeForm.Controls)
                {
                    if (control is TabControl)
                    {
                        tabControl = (TabControl)control;
                        break;
                    }
                }

                if (tabControl == null)
                    return;

                // Her bir sekmedeki DataGridView'ları bul ve verileri yenile
                foreach (TabPage tabPage in tabControl.TabPages)
                {
                    foreach (Control control in tabPage.Controls)
                    {
                        if (control is Guna2DataGridView dgv)
                        {
                            if (tabPage.Text == "Aktif Alarmlar")
                                YukleAktifAlarmlar(dgv);
                            else if (tabPage.Text == "Ertelenenler")
                                YukleErtelenenAlarmlar(dgv);
                            else if (tabPage.Text == "Tamamlananlar")
                                YukleTamamlananAlarmlar(dgv);
                        }
                    }
                }

                // Form4'ü güncelle
                Form4 form4 = Application.OpenForms["Form4"] as Form4;
                if (form4 != null)
                {
                    form4.KontrolEtAlarmlari();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Alarm verileri yenilenirken hata: {ex.Message}");
            }
        }

        // DataGridView oluşturma
        public static Guna2DataGridView OlusturDataGridView()
        {
            Guna2DataGridView dgv = new Guna2DataGridView
            {
                Dock = DockStyle.Fill,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None,
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None
            };

            // Başlık stilini manuel ayarla
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(26, 115, 232);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgv.DefaultCellStyle.BackColor = Color.White;
            dgv.DefaultCellStyle.ForeColor = Color.DimGray;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 240, 240);
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;

            // DataGridView sütunları
            dgv.Columns.Add("ID", "ID");
            dgv.Columns.Add("CariUnvan", "Cari");
            dgv.Columns.Add("AlarmTarihi", "Tarih/Saat");
            dgv.Columns.Add("OdemeTuru", "Ödeme Türü");
            dgv.Columns.Add("Aciklama", "Açıklama");
            dgv.Columns.Add("ErtelemeSayisi", "Erteleme");
            dgv.Columns.Add("OnemDerecesi", "Önem");

            // ID sütununu gizle
            dgv.Columns["ID"].Visible = false;

            // Sütun genişlikleri
            dgv.Columns["CariUnvan"].Width = 160;
            dgv.Columns["AlarmTarihi"].Width = 120;
            dgv.Columns["OdemeTuru"].Width = 100;
            dgv.Columns["Aciklama"].Width = 200;
            dgv.Columns["ErtelemeSayisi"].Width = 80;
            dgv.Columns["OnemDerecesi"].Width = 80;

            return dgv;
        }

        // Sağ tık menüsü ekleme
        public static void EkleSagTikMenusu(Guna2DataGridView dgv, string kategoriAdi,
                                    Guna2DataGridView aktifAlarmlar,
                                    Guna2DataGridView ertelenenAlarmlar,
                                    Guna2DataGridView tamamlananAlarmlar)
        {
            // ContextMenuStrip oluştur
            ContextMenuStrip sagTikMenu = new ContextMenuStrip();

            // Tamamlandı menü öğesi
            ToolStripMenuItem tamamlandiMenuItem = new ToolStripMenuItem("Tamamlandı Olarak İşaretle");
            tamamlandiMenuItem.Click += (s, e) => {
                if (dgv.SelectedRows.Count > 0)
                {
                    int id = Convert.ToInt32(dgv.SelectedRows[0].Cells["ID"].Value);
                    GuncelleAlarmDurumu(id, "Tamamlandı");
                    YenileAlarmVerileri();
                }
            };
            sagTikMenu.Items.Add(tamamlandiMenuItem);

            // Ertele menü öğesi
            ToolStripMenuItem erteleMenuItem = new ToolStripMenuItem("Ertele");
            erteleMenuItem.Click += (s, e) => {
                if (dgv.SelectedRows.Count > 0)
                {
                    int id = Convert.ToInt32(dgv.SelectedRows[0].Cells["ID"].Value);
                    GosterErtelemeFormu(id);
                    YenileAlarmVerileri();
                }
            };
            sagTikMenu.Items.Add(erteleMenuItem);

            // Ayırıcı çizgi
            sagTikMenu.Items.Add(new ToolStripSeparator());

            // Sil menü öğesi
            ToolStripMenuItem silMenuItem = new ToolStripMenuItem("Sil");
            silMenuItem.Click += (s, e) => {
                if (dgv.SelectedRows.Count > 0)
                {
                    int id = Convert.ToInt32(dgv.SelectedRows[0].Cells["ID"].Value);
                    if (MessageBox.Show("Seçili alarmı silmek istediğinizden emin misiniz?", "Onay",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        SilAlarm(id);
                        YenileAlarmVerileri();
                    }
                }
            };
            sagTikMenu.Items.Add(silMenuItem);

            // Tümünü sil menü öğesi
            ToolStripMenuItem tumunuSilMenuItem = new ToolStripMenuItem("Tümünü Sil");
            tumunuSilMenuItem.Click += (s, e) => {
                if (dgv.Rows.Count == 0)
                {
                    MessageBox.Show($"Silinecek {kategoriAdi} bulunamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                string mesaj = $"Bu sekmedeki tüm {kategoriAdi} alarmlar silinecek. Bu işlem geri alınamaz. Devam etmek istiyor musunuz?";
                DialogResult result = MessageBox.Show(mesaj, "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    // Silinecek alarm ID'lerini topla
                    List<int> silinecekIDler = new List<int>();
                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        int id = Convert.ToInt32(row.Cells["ID"].Value);
                        silinecekIDler.Add(id);
                    }

                    if (TumAlarmlariSil(silinecekIDler))
                    {
                        YenileAlarmVerileri();
                    }
                }
            };
            sagTikMenu.Items.Add(tumunuSilMenuItem);

            // ContextMenuStrip'i DataGridView'e ata
            dgv.ContextMenuStrip = sagTikMenu;

            // Tamamlananlar sekmesi için işlevsiz yapılacak öğeler
            if (kategoriAdi == "Tamamlanan")
            {
                tamamlandiMenuItem.Enabled = false; // Tamamlandı menüsünü devre dışı bırak
                erteleMenuItem.Enabled = false; // Ertele menüsünü devre dışı bırak
            }

            // Menü gösterilmeden önce etkinleştirme/devre dışı bırakma
            sagTikMenu.Opening += (s, e) => {
                if (dgv.SelectedRows.Count > 0)
                {
                    // Seçili satırın durumunu kontrol et
                    string durum = "";
                    // Tamamlananlar sekmesindeki tüm alarmlar zaten tamamlanmış durumda
                    if (kategoriAdi != "Tamamlanan")
                    {
                        // Sadece Aktif ve Ertelenenler sekmeleri için durum kontrolü yap
                        if (dgv.Columns.Contains("Durum"))
                        {
                            // Normal DataGridView için Durum sütunu varsa
                            durum = dgv.SelectedRows[0].Cells["Durum"].Value?.ToString() ?? "";
                        }
                        else
                        {
                            // Durum sütunu yoksa (bizim oluşturduğumuz DataGridView'lerde)
                            // Örneğin satır stilinden anlayabiliriz (eğer strikethrough varsa tamamlanmış demektir)
                            Font font = dgv.SelectedRows[0].DefaultCellStyle.Font;
                            if (font != null && font.Strikeout)
                            {
                                durum = "Tamamlandı";
                            }
                        }
                        // Durum tamamlandıysa ilgili menü öğelerini devre dışı bırak
                        if (durum == "Tamamlandı")
                        {
                            tamamlandiMenuItem.Enabled = false;
                            erteleMenuItem.Enabled = false;
                        }
                        else
                        {
                            tamamlandiMenuItem.Enabled = true;
                            erteleMenuItem.Enabled = true;
                        }
                    }
                }
                else
                {
                    // Satır seçili değilse sadece "Tümünü Sil" etkin olmalı
                    tamamlandiMenuItem.Enabled = false;
                    erteleMenuItem.Enabled = false;
                    silMenuItem.Enabled = false;
                    tumunuSilMenuItem.Enabled = dgv.Rows.Count > 0;
                }
            };
        }

        // Aktif alarmları yükle
        public static void YukleAktifAlarmlar(Guna2DataGridView dgv)
        {
            dgv.Rows.Clear();
            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = @"SELECT id, cari_kodu, cari_unvan, alarm_tarihi, mesaj, odeme_turu,
                        onem_derecesi, IFNULL(erteleme_sayisi, 0) as erteleme_sayisi
                       FROM Alarmlar
                       WHERE durum = 'Bekliyor'
                       ORDER BY datetime(alarm_tarihi) ASC, onem_derecesi DESC";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DateTime alarmTarihi = Convert.ToDateTime(reader["alarm_tarihi"]);
                            int ertelemeSayisi = Convert.ToInt32(reader["erteleme_sayisi"]);
                            string onemDerecesi = reader["onem_derecesi"].ToString();
                            int rowIndex = dgv.Rows.Add(
                                reader["id"],
                                reader["cari_unvan"].ToString(),
                                alarmTarihi.ToString("dd.MM.yyyy HH:mm"),
                                reader["odeme_turu"].ToString(),
                                reader["mesaj"].ToString(),
                                ertelemeSayisi > 0 ? ertelemeSayisi.ToString() + " kez" : "-",
                                onemDerecesi
                            );
                            // Bugün olan alarmlar için stil
                            if (alarmTarihi.Date == DateTime.Today)
                            {
                                dgv.Rows[rowIndex].DefaultCellStyle.ForeColor = Color.Crimson;
                                dgv.Rows[rowIndex].DefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                            }
                            // Geçmiş alarmlar için stil
                            else if (alarmTarihi.Date < DateTime.Today)
                            {
                                dgv.Rows[rowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 235, 235);
                            }
                            // Önem derecesine göre arka plan rengi
                            if (onemDerecesi == "Yüksek")
                            {
                                dgv.Rows[rowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 246, 222);
                            }
                            else if (onemDerecesi == "Kritik")
                            {
                                dgv.Rows[rowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 228, 225);
                            }
                            // Erteleme sayısına göre stil
                            if (ertelemeSayisi > 0)
                            {
                                dgv.Rows[rowIndex].Cells["ErtelemeSayisi"].Style.ForeColor = Color.FromArgb(255, 128, 0);
                                dgv.Rows[rowIndex].Cells["ErtelemeSayisi"].Style.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                            }
                        }
                    }
                }
            }
        }

        // Ertelenen alarmları yükle
        public static void YukleErtelenenAlarmlar(Guna2DataGridView dgv)
        {
            dgv.Rows.Clear();
            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = @"SELECT id, cari_kodu, cari_unvan, alarm_tarihi, mesaj, odeme_turu,
                        onem_derecesi, IFNULL(erteleme_sayisi, 0) as erteleme_sayisi
                       FROM Alarmlar
                       WHERE durum = 'Bekliyor' AND IFNULL(erteleme_sayisi, 0) > 0
                       ORDER BY erteleme_sayisi DESC, datetime(alarm_tarihi) ASC";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DateTime alarmTarihi = Convert.ToDateTime(reader["alarm_tarihi"]);
                            int ertelemeSayisi = Convert.ToInt32(reader["erteleme_sayisi"]);
                            string onemDerecesi = reader["onem_derecesi"].ToString();
                            int rowIndex = dgv.Rows.Add(
                                reader["id"],
                                reader["cari_unvan"].ToString(),
                                alarmTarihi.ToString("dd.MM.yyyy HH:mm"),
                                reader["odeme_turu"].ToString(),
                                reader["mesaj"].ToString(),
                                ertelemeSayisi.ToString() + " kez",
                                onemDerecesi
                            );
                            // Erteleme sayısına göre stil
                            if (ertelemeSayisi >= 3)
                            {
                                dgv.Rows[rowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 230, 230);
                                dgv.Rows[rowIndex].Cells["ErtelemeSayisi"].Style.ForeColor = Color.Red;
                            }
                            else if (ertelemeSayisi >= 2)
                            {
                                dgv.Rows[rowIndex].Cells["ErtelemeSayisi"].Style.ForeColor = Color.OrangeRed;
                            }
                            else
                            {
                                dgv.Rows[rowIndex].Cells["ErtelemeSayisi"].Style.ForeColor = Color.Orange;
                            }
                            dgv.Rows[rowIndex].Cells["ErtelemeSayisi"].Style.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                        }
                    }
                }
            }
        }
        // Tamamlanan alarmları yükle
        public static void YukleTamamlananAlarmlar(Guna2DataGridView dgv)
        {
            dgv.Rows.Clear();
            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = @"SELECT id, cari_kodu, cari_unvan, alarm_tarihi, mesaj, odeme_turu,
                onem_derecesi, IFNULL(erteleme_sayisi, 0) as erteleme_sayisi
               FROM Alarmlar
               WHERE durum = 'Tamamlandı'
               ORDER BY datetime(alarm_tarihi) DESC";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DateTime alarmTarihi = Convert.ToDateTime(reader["alarm_tarihi"]);
                            int ertelemeSayisi = Convert.ToInt32(reader["erteleme_sayisi"]);
                            int rowIndex = dgv.Rows.Add(
                                reader["id"],
                                reader["cari_unvan"].ToString(),
                                alarmTarihi.ToString("dd.MM.yyyy HH:mm"),
                                reader["odeme_turu"].ToString(),
                                reader["mesaj"].ToString(),
                                ertelemeSayisi > 0 ? ertelemeSayisi.ToString() + " kez" : "-",
                                reader["onem_derecesi"].ToString()
                            );
                            // Tamamlanmış alarmlar için stil
                            dgv.Rows[rowIndex].DefaultCellStyle.ForeColor = Color.Gray;
                            dgv.Rows[rowIndex].DefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Strikeout);
                        }
                    }
                }
            }
        }

        // Erteleme formu göster
        public static void GosterErtelemeFormu(int alarmId)
        {
            Form ertelemeForm = new Form
            {
                Text = "Alarmı Ertele",
                Size = new Size(300, 200),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                BackColor = Color.FromArgb(45, 45, 48),
                ForeColor = Color.White,
                ShowIcon = false,
                ShowInTaskbar = false
            };

            Label lblAciklama = new Label
            {
                Text = "Erteleme süresini seçin:",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 20)
            };

            ComboBox cmbSure = new ComboBox
            {
                Size = new Size(260, 30),
                Location = new Point(20, 50),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10)
            };
            cmbSure.Items.Add("15 dakika");
            cmbSure.Items.Add("30 dakika");
            cmbSure.Items.Add("1 saat");
            cmbSure.Items.Add("2 saat");
            cmbSure.Items.Add("4 saat");
            cmbSure.Items.Add("Yarın aynı saatte");
            cmbSure.SelectedIndex = 0; // Varsayılan 15 dakika

            Button btnTamam = new Button
            {
                Text = "Tamam",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Size = new Size(100, 35),
                Location = new Point(20, 100),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };

            Button btnVazgec = new Button
            {
                Text = "Vazgeç",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Size = new Size(100, 35),
                Location = new Point(180, 100),
                BackColor = Color.FromArgb(100, 100, 100),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };

            btnTamam.Click += (s, e) =>
            {
                int selectedIndex = cmbSure.SelectedIndex;
                DateTime yeniTarih = DateTime.Now;
                switch (selectedIndex)
                {
                    case 0: // 15 dakika
                        yeniTarih = DateTime.Now.AddMinutes(15);
                        break;
                    case 1: // 30 dakika
                        yeniTarih = DateTime.Now.AddMinutes(30);
                        break;
                    case 2: // 1 saat
                        yeniTarih = DateTime.Now.AddHours(1);
                        break;
                    case 3: // 2 saat
                        yeniTarih = DateTime.Now.AddHours(2);
                        break;
                    case 4: // 4 saat
                        yeniTarih = DateTime.Now.AddHours(4);
                        break;
                    case 5: // Yarın aynı saatte
                        yeniTarih = DateTime.Now.AddDays(1);
                        break;
                    default:
                        yeniTarih = DateTime.Now.AddMinutes(15);
                        break;
                }
                int ertelemeSayisi = GetAlarmErtelemeSayisi(alarmId) + 1;
                if (ErteleAlarm(alarmId, yeniTarih, ertelemeSayisi))
                {
                    ertelemeForm.Close();
                }
            };

            btnVazgec.Click += (s, e) =>
            {
                ertelemeForm.Close();
            };

            ertelemeForm.Controls.Add(lblAciklama);
            ertelemeForm.Controls.Add(cmbSure);
            ertelemeForm.Controls.Add(btnTamam);
            ertelemeForm.Controls.Add(btnVazgec);
            ertelemeForm.ShowDialog(_alarmListeForm);
        }

        // Alarm erteleme sayısını al
        public static int GetAlarmErtelemeSayisi(int alarmId)
        {
            int ertelemeSayisi = 0;
            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT IFNULL(erteleme_sayisi, 0) FROM Alarmlar WHERE id = @id";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", alarmId);
                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        ertelemeSayisi = Convert.ToInt32(result);
                    }
                }
            }
            return ertelemeSayisi;
        }

        // Alarmı ertele
        public static bool ErteleAlarm(int alarmId, DateTime yeniTarih, int ertelemeSayisi)
        {
            try
            {
                string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = @"UPDATE Alarmlar
                   SET alarm_tarihi = @yeniTarih,
                       bildirildi = 0,
                       erteleme_sayisi = @ertelemeSayisi,
                       son_erteleme_tarihi = @sonErtelemeTarihi
                   WHERE id = @id";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@yeniTarih", yeniTarih.ToString("yyyy-MM-dd HH:mm:ss"));
                        command.Parameters.AddWithValue("@ertelemeSayisi", ertelemeSayisi);
                        command.Parameters.AddWithValue("@sonErtelemeTarihi", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        command.Parameters.AddWithValue("@id", alarmId);
                        int result = command.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show($"Alarm {yeniTarih.ToString("dd.MM.yyyy HH:mm")} tarihine ertelendi.",
                                           "Alarm Ertelendi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return true;
                        }
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Alarm ertelenirken bir hata oluştu: {ex.Message}", "Hata",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Alarm durumunu güncelle
        public static bool GuncelleAlarmDurumu(int alarmId, string durum)
        {
            try
            {
                string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE Alarmlar SET durum = @durum WHERE id = @id";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@durum", durum);
                        command.Parameters.AddWithValue("@id", alarmId);
                        int result = command.ExecuteNonQuery();
                        if (result > 0)
                        {
                            if (durum == "Tamamlandı")
                            {
                                MessageBox.Show("Alarm tamamlandı olarak işaretlendi.", "Bilgi",
                                               MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }

                            // Form4'ü güncelle (eğer açıksa)
                            Form4 form4 = Application.OpenForms["Form4"] as Form4;
                            if (form4 != null)
                            {
                                form4.KontrolEtAlarmlari();
                            }

                            return true;
                        }
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Alarm durumu güncellenirken bir hata oluştu: {ex.Message}", "Hata",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Alarm silme
        public static bool SilAlarm(int alarmId)
        {
            try
            {
                string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM Alarmlar WHERE id = @id";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", alarmId);
                        int result = command.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Alarm başarıyla silindi.", "Bilgi",
                                           MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Form4'ü güncelle (eğer açıksa)
                            Form4 form4 = Application.OpenForms["Form4"] as Form4;
                            if (form4 != null)
                            {
                                form4.KontrolEtAlarmlari();
                            }

                            return true;
                        }
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Alarm silinirken bir hata oluştu: {ex.Message}", "Hata",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Birden fazla alarmı sil
        public static bool TumAlarmlariSil(List<int> alarmIDler)
        {
            if (alarmIDler.Count == 0)
                return false;

            int silinenSayisi = 0;
            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    using (SQLiteTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            string idListesi = string.Join(",", alarmIDler);
                            string query = $"DELETE FROM Alarmlar WHERE id IN ({idListesi})";
                            using (SQLiteCommand command = new SQLiteCommand(query, connection, transaction))
                            {
                                silinenSayisi = command.ExecuteNonQuery();
                            }
                            transaction.Commit();

                            if (silinenSayisi > 0)
                            {
                                MessageBox.Show($"{silinenSayisi} alarm başarıyla silindi.", "Bilgi",
                                               MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // Form4'ü güncelle (eğer açıksa)
                                Form4 form4 = Application.OpenForms["Form4"] as Form4;
                                if (form4 != null)
                                {
                                    form4.KontrolEtAlarmlari();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show($"Silme işlemi sırasında bir hata oluştu: {ex.Message}", "Hata",
                                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Veritabanı bağlantısı sırasında bir hata oluştu: {ex.Message}", "Hata",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return silinenSayisi > 0;
        }

        // Bildirim gösterme
        public static void GosterBildirim(string baslik, string mesaj, ToolTipIcon ikonTipi = ToolTipIcon.Info)
        {
            NotifyIcon notifyIcon = new NotifyIcon
            {
                Icon = SystemIcons.Information,
                Visible = true,
                BalloonTipTitle = baslik,
                BalloonTipText = mesaj,
                BalloonTipIcon = ikonTipi
            };

            // Bildirime tıklama olayı - Alarm listesini göster
            notifyIcon.BalloonTipClicked += (sender, e) => {
                try
                {
                    // Ana thread üzerinde çalıştır
                    if (Application.OpenForms.Count > 0)
                    {
                        Form anaForm = Application.OpenForms[0]; // Ana form
                        anaForm.BeginInvoke(new Action(() => {
                            try
                            {
                                // Alarm listesini göster
                                GosterAlarmListesi();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Bildirim tıklamasında hata: {ex.Message}");
                            }
                        }));
                    }
                    else
                    {
                        // Ana form yoksa, direkt çağrı yap
                        GosterAlarmListesi();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Bildirim işlenirken bir hata oluştu: {ex.Message}", "Hata",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            notifyIcon.ShowBalloonTip(3000);

            // 5 saniye sonra gizle
            System.Threading.Tasks.Task.Delay(5000).ContinueWith(t => {
                try
                {
                    if (notifyIcon != null && notifyIcon.Visible)
                    {
                        notifyIcon.Visible = false;
                        notifyIcon.Dispose();
                    }
                }
                catch { /* Dispose sırasında hata olursa görmezden gel */ }
            });
        }

        // Form4 için yardımcı metot - Aktif alarm sayısını alma
        public static int GetAktifAlarmSayisi()
        {
            int alarmSayisi = 0;

            try
            {
                string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM Alarmlar WHERE durum = 'Bekliyor'";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        object result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            alarmSayisi = Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Aktif alarm sayısı alınırken hata: {ex.Message}");
            }

            return alarmSayisi;
        }

        // Bugün ve geçmiş alarmları kontrol eden metot
        public static void KontrolEtGecmisAlarmlari()
        {
            try
            {
                string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Bugünün başlangıcını ve bitişini hesapla
                    DateTime bugun = DateTime.Today;
                    DateTime bugunBitis = bugun.AddDays(1).AddMilliseconds(-1);

                    // Bugünkü ve geçmiş alarm sayısını kontrol et (bildirilmemiş ve bekleyen)
                    string query = @"SELECT COUNT(*) FROM Alarmlar 
                           WHERE durum = 'Bekliyor' 
                           AND bildirildi = 0
                           AND datetime(alarm_tarihi) <= @simdi";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@simdi", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        int alarmSayisi = Convert.ToInt32(command.ExecuteScalar());

                        if (alarmSayisi > 0)
                        {
                            // Bildirimi göster
                            string mesaj = alarmSayisi == 1
                                ? "1 adet alarm zamanı geldi veya geçti!"
                                : $"{alarmSayisi} adet alarm zamanı geldi veya geçti!";

                            GosterBildirim("Alarm Hatırlatması", mesaj, ToolTipIcon.Warning);

                            // Bildirilen alarmları işaretle
                            string updateQuery = @"UPDATE Alarmlar 
                                          SET bildirildi = 1 
                                          WHERE durum = 'Bekliyor' 
                                          AND bildirildi = 0
                                          AND datetime(alarm_tarihi) <= @simdi";

                            using (SQLiteCommand updateCommand = new SQLiteCommand(updateQuery, connection))
                            {
                                updateCommand.Parameters.AddWithValue("@simdi", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                                updateCommand.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Alarm kontrolü sırasında hata: {ex.Message}");
            }
        }
    }
}
