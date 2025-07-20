using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using Guna.UI2.WinForms;

namespace Veresiye2025
{
    public class CariTema
    {
        public void InitializeComponents(Carihareketler form)
        {
            // Components kontrolü
            if (form.components == null)
            {
                form.components = new System.ComponentModel.Container();
            }

            // Kontrol oluşturma
            CreateControls(form);

            // Özellik ayarlama
            SetProperties(form);

            // Layout ayarlama
            SetupLayout(form);

            // Event handler bağlama
            SetupEventHandlers(form);
        }

        private void CreateControls(Carihareketler form)
        {
            // Ana paneller
            form.panelTop = new System.Windows.Forms.Panel();
            form.menuContainer = new System.Windows.Forms.Panel();
            form.windowControlsPanel = new System.Windows.Forms.Panel();
            form.panelHeader = new System.Windows.Forms.Panel();
            form.firmPanel = new System.Windows.Forms.Panel();
            form.panelButtons = new System.Windows.Forms.Panel();
            form.notekleContainer = new System.Windows.Forms.Panel();
            form.emanetContainer = new System.Windows.Forms.Panel();
            form.panelContent = new System.Windows.Forms.Panel();
            form.panelFooter = new System.Windows.Forms.Panel();
            form.bottomButtonsPanel = new System.Windows.Forms.Panel();
            form.summaryPanel = new System.Windows.Forms.Panel();

            // MenuStrip ve items
            form.menuStrip1 = new System.Windows.Forms.MenuStrip();
            form.Firmalar = new System.Windows.Forms.ToolStripMenuItem();
            form.firmaHesaplarıToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            form.yılSonuDevirİşlemleriToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            form.firmanıDüzenleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            form.işlemlerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            form.hareketlerF3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            form.hatırlatmaKurToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            form.yedekİşlemleriToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            form.yedekAlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            form.araçlarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            form.genelAyarlarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            form.epostaAyarlarıToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            form.şifreDeğiştirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            form.posTakipİşlemleriToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            form.posTakipToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            form.krediKartıTakipToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            form.yardımToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            form.hakkımızdaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            form.güncellemeDenetleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            form.çıkışToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();

            // Butonlar
            form.btnMinimize = new Guna.UI2.WinForms.Guna2Button();
            form.btnClose = new Guna.UI2.WinForms.Guna2Button();
            form.button3 = new Guna.UI2.WinForms.Guna2Button();
            form.oncekicari = new Guna.UI2.WinForms.Guna2Button();
            form.sonrakicari = new Guna.UI2.WinForms.Guna2Button();
            form.notekle = new Guna.UI2.WinForms.Guna2Button();
            form.analiz = new Guna.UI2.WinForms.Guna2Button();
            form.Sorgulabtn = new Guna.UI2.WinForms.Guna2Button();
            form.eposta = new Guna.UI2.WinForms.Guna2Button();
            form.yazdir = new Guna.UI2.WinForms.Guna2Button();
            form.Excelaktar = new Guna.UI2.WinForms.Guna2Button();
            form.emanet = new Guna.UI2.WinForms.Guna2Button();
            form.hrketekle = new Guna.UI2.WinForms.Guna2Button();
            form.hrktdegistir = new Guna.UI2.WinForms.Guna2Button();
            form.hrktsil = new Guna.UI2.WinForms.Guna2Button();
            form.gecikmesorgula = new Guna.UI2.WinForms.Guna2Button();
            form.hrktlergerigit = new Guna.UI2.WinForms.Guna2Button();
            form.button13 = new Guna.UI2.WinForms.Guna2Button();

            // DataGridView ve sütunları
            form.dataGridView1 = new System.Windows.Forms.DataGridView();
            form.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            form.cari_kodu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            form.Tarih = new System.Windows.Forms.DataGridViewTextBoxColumn();
            form.Tür = new System.Windows.Forms.DataGridViewTextBoxColumn();
            form.Açıklama = new System.Windows.Forms.DataGridViewTextBoxColumn();
            form.Borç = new System.Windows.Forms.DataGridViewTextBoxColumn();
            form.Alacak = new System.Windows.Forms.DataGridViewTextBoxColumn();
            form.Kalan = new System.Windows.Forms.DataGridViewTextBoxColumn();

            // ContextMenuStrip
            form.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip();
            form.borcekle = new System.Windows.Forms.ToolStripMenuItem();
            form.tahsilatekle = new System.Windows.Forms.ToolStripMenuItem();

            // TextBox'lar ve Label'lar
            form.textBox1 = new Guna.UI2.WinForms.Guna2TextBox();
            form.textBox2 = new Guna.UI2.WinForms.Guna2TextBox();
            form.textBox3 = new Guna.UI2.WinForms.Guna2TextBox();
            form.label1 = new System.Windows.Forms.Label();
            form.label2 = new System.Windows.Forms.Label();
            form.label3 = new System.Windows.Forms.Label();

            // Print components
            form.printDocument = new System.Drawing.Printing.PrintDocument();
            form.printPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
            form.printDialog = new System.Windows.Forms.PrintDialog();

            // Chart
            form.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
        }

        private void SetProperties(Carihareketler form)
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Carihareketler));

            // Form ayarları
            form.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            form.BackColor = System.Drawing.Color.White;
            form.ClientSize = new System.Drawing.Size(890, 518);
            form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            form.Name = "Carihareketler";
            form.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            form.Text = "Cari Hareketleri";

            // Panel ayarları
            SetupPanelProperties(form);

            // Menu ayarları  
            SetupMenuProperties(form);

            // Button ayarları
            SetupButtonProperties(form);

            // DataGridView ayarları
            SetupDataGridViewProperties(form);

            // Footer ayarları
            SetupFooterProperties(form);

            // Print ve Chart ayarları
            SetupOtherProperties(form, resources);
        }

        private void SetupPanelProperties(Carihareketler form)
        {
            // panelTop
            form.panelTop.BackColor = System.Drawing.Color.MidnightBlue;
            form.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            form.panelTop.Location = new System.Drawing.Point(0, 0);
            form.panelTop.Name = "panelTop";
            form.panelTop.Size = new System.Drawing.Size(890, 52);
            form.panelTop.TabIndex = 0;

            // menuContainer
            form.menuContainer.BackColor = System.Drawing.Color.FromArgb(28, 141, 243);
            form.menuContainer.Location = new System.Drawing.Point(15, 0);
            form.menuContainer.Name = "menuContainer";
            form.menuContainer.Size = new System.Drawing.Size(755, 52);
            form.menuContainer.TabIndex = 40;

            // windowControlsPanel
            form.windowControlsPanel.Dock = System.Windows.Forms.DockStyle.Right;
            form.windowControlsPanel.Location = new System.Drawing.Point(785, 0);
            form.windowControlsPanel.Name = "windowControlsPanel";
            form.windowControlsPanel.Size = new System.Drawing.Size(105, 52);
            form.windowControlsPanel.TabIndex = 39;

            // panelHeader
            form.panelHeader.BackColor = System.Drawing.Color.DodgerBlue;
            form.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            form.panelHeader.Location = new System.Drawing.Point(0, 52);
            form.panelHeader.Name = "panelHeader";
            form.panelHeader.Padding = new System.Windows.Forms.Padding(15);
            form.panelHeader.Size = new System.Drawing.Size(890, 84);
            form.panelHeader.TabIndex = 1;

            // firmPanel
            form.firmPanel.BackColor = System.Drawing.Color.FromArgb(67, 162, 243);
            form.firmPanel.Location = new System.Drawing.Point(81, 10);
            form.firmPanel.Name = "firmPanel";
            form.firmPanel.Size = new System.Drawing.Size(726, 60);
            form.firmPanel.TabIndex = 47;

            // panelButtons
            form.panelButtons.BackColor = System.Drawing.Color.MidnightBlue;
            form.panelButtons.Dock = System.Windows.Forms.DockStyle.Top;
            form.panelButtons.Location = new System.Drawing.Point(0, 136);
            form.panelButtons.Name = "panelButtons";
            form.panelButtons.Padding = new System.Windows.Forms.Padding(15);
            form.panelButtons.Size = new System.Drawing.Size(890, 65);
            form.panelButtons.TabIndex = 2;

            // Button containers
            form.notekleContainer.Location = new System.Drawing.Point(15, 9);
            form.notekleContainer.Name = "notekleContainer";
            form.notekleContainer.Size = new System.Drawing.Size(115, 45);
            form.notekleContainer.TabIndex = 55;

            form.emanetContainer.Location = new System.Drawing.Point(765, 9);
            form.emanetContainer.Name = "emanetContainer";
            form.emanetContainer.Size = new System.Drawing.Size(110, 45);
            form.emanetContainer.TabIndex = 56;

            // panelContent
            form.panelContent.BackColor = System.Drawing.Color.White;
            form.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            form.panelContent.Location = new System.Drawing.Point(0, 201);
            form.panelContent.Name = "panelContent";
            form.panelContent.Size = new System.Drawing.Size(890, 239);
            form.panelContent.TabIndex = 3;

            // panelFooter
            form.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            form.panelFooter.Location = new System.Drawing.Point(0, 440);
            form.panelFooter.Name = "panelFooter";
            form.panelFooter.Size = new System.Drawing.Size(890, 78);
            form.panelFooter.TabIndex = 4;

            // bottomButtonsPanel
            form.bottomButtonsPanel.BackColor = System.Drawing.Color.MidnightBlue;
            form.bottomButtonsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            form.bottomButtonsPanel.Location = new System.Drawing.Point(0, 28);
            form.bottomButtonsPanel.Name = "bottomButtonsPanel";
            form.bottomButtonsPanel.Padding = new System.Windows.Forms.Padding(15, 3, 15, 3);
            form.bottomButtonsPanel.Size = new System.Drawing.Size(890, 50);
            form.bottomButtonsPanel.TabIndex = 37;

            // summaryPanel
            form.summaryPanel.Dock = System.Windows.Forms.DockStyle.Top;
            form.summaryPanel.Location = new System.Drawing.Point(0, 0);
            form.summaryPanel.Name = "summaryPanel";
            form.summaryPanel.Size = new System.Drawing.Size(890, 30);
            form.summaryPanel.TabIndex = 36;
        }

        private void SetupMenuProperties(Carihareketler form)
        {
            // menuStrip1
            form.menuStrip1.AutoSize = false;
            form.menuStrip1.BackColor = System.Drawing.Color.MidnightBlue;
            form.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            form.menuStrip1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            form.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            form.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                form.Firmalar,
                form.işlemlerToolStripMenuItem,
                form.yedekİşlemleriToolStripMenuItem,
                form.araçlarToolStripMenuItem,
                form.posTakipİşlemleriToolStripMenuItem,
                form.yardımToolStripMenuItem,
                form.çıkışToolStripMenuItem
            });
            form.menuStrip1.Location = new System.Drawing.Point(0, 0);
            form.menuStrip1.Name = "menuStrip1";
            form.menuStrip1.Padding = new System.Windows.Forms.Padding(10, 2, 0, 2);
            form.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            form.menuStrip1.Size = new System.Drawing.Size(755, 52);
            form.menuStrip1.TabIndex = 38;
            form.menuStrip1.Text = "menuStrip1";

            // Menu items setup
            SetupMenuItems(form);
        }

        private void SetupMenuItems(Carihareketler form)
        {
            // Firmalar menu
            form.Firmalar.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                form.firmaHesaplarıToolStripMenuItem,
                form.yılSonuDevirİşlemleriToolStripMenuItem,
                form.firmanıDüzenleToolStripMenuItem
            });
            form.Firmalar.ForeColor = System.Drawing.Color.DarkGray;
            form.Firmalar.Name = "Firmalar";
            form.Firmalar.Size = new System.Drawing.Size(80, 48);
            form.Firmalar.Text = "Firmalar";

            form.firmaHesaplarıToolStripMenuItem.Name = "firmaHesaplarıToolStripMenuItem";
            form.firmaHesaplarıToolStripMenuItem.Size = new System.Drawing.Size(250, 26);
            form.firmaHesaplarıToolStripMenuItem.Text = "Firma Hesapları";

            form.yılSonuDevirİşlemleriToolStripMenuItem.Name = "yılSonuDevirİşlemleriToolStripMenuItem";
            form.yılSonuDevirİşlemleriToolStripMenuItem.Size = new System.Drawing.Size(250, 26);
            form.yılSonuDevirİşlemleriToolStripMenuItem.Text = "Yıl Sonu Devir İşlemleri";

            form.firmanıDüzenleToolStripMenuItem.Name = "firmanıDüzenleToolStripMenuItem";
            form.firmanıDüzenleToolStripMenuItem.Size = new System.Drawing.Size(250, 26);
            form.firmanıDüzenleToolStripMenuItem.Text = "Firmanı Düzenle";

            // İşlemler menu
            form.işlemlerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                form.hareketlerF3ToolStripMenuItem,
                form.hatırlatmaKurToolStripMenuItem
            });
            form.işlemlerToolStripMenuItem.ForeColor = System.Drawing.Color.DarkGray;
            form.işlemlerToolStripMenuItem.Name = "işlemlerToolStripMenuItem";
            form.işlemlerToolStripMenuItem.Size = new System.Drawing.Size(76, 48);
            form.işlemlerToolStripMenuItem.Text = "İşlemler";

            form.hareketlerF3ToolStripMenuItem.Name = "hareketlerF3ToolStripMenuItem";
            form.hareketlerF3ToolStripMenuItem.Size = new System.Drawing.Size(192, 26);
            form.hareketlerF3ToolStripMenuItem.Text = "Hareketler[F3]";

            form.hatırlatmaKurToolStripMenuItem.Name = "hatırlatmaKurToolStripMenuItem";
            form.hatırlatmaKurToolStripMenuItem.Size = new System.Drawing.Size(192, 26);
            form.hatırlatmaKurToolStripMenuItem.Text = "Hatırlatma Kur";

            // Yedek İşlemleri menu
            form.yedekİşlemleriToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                form.yedekAlToolStripMenuItem
            });
            form.yedekİşlemleriToolStripMenuItem.ForeColor = System.Drawing.Color.DarkGray;
            form.yedekİşlemleriToolStripMenuItem.Name = "yedekİşlemleriToolStripMenuItem";
            form.yedekİşlemleriToolStripMenuItem.Size = new System.Drawing.Size(125, 48);
            form.yedekİşlemleriToolStripMenuItem.Text = "Yedek İşlemleri";

            form.yedekAlToolStripMenuItem.Name = "yedekAlToolStripMenuItem";
            form.yedekAlToolStripMenuItem.Size = new System.Drawing.Size(227, 26);
            form.yedekAlToolStripMenuItem.Text = "Yedekleme İşlemleri";

            // Araçlar menu
            form.araçlarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                form.genelAyarlarToolStripMenuItem,
                form.epostaAyarlarıToolStripMenuItem,
                form.şifreDeğiştirToolStripMenuItem
            });
            form.araçlarToolStripMenuItem.ForeColor = System.Drawing.Color.DarkGray;
            form.araçlarToolStripMenuItem.Name = "araçlarToolStripMenuItem";
            form.araçlarToolStripMenuItem.Size = new System.Drawing.Size(72, 48);
            form.araçlarToolStripMenuItem.Text = "Araçlar";

            form.genelAyarlarToolStripMenuItem.Name = "genelAyarlarToolStripMenuItem";
            form.genelAyarlarToolStripMenuItem.Size = new System.Drawing.Size(195, 26);
            form.genelAyarlarToolStripMenuItem.Text = "Genel Ayarlar";

            form.epostaAyarlarıToolStripMenuItem.Name = "epostaAyarlarıToolStripMenuItem";
            form.epostaAyarlarıToolStripMenuItem.Size = new System.Drawing.Size(195, 26);
            form.epostaAyarlarıToolStripMenuItem.Text = "Eposta Ayarları";

            form.şifreDeğiştirToolStripMenuItem.Name = "şifreDeğiştirToolStripMenuItem";
            form.şifreDeğiştirToolStripMenuItem.Size = new System.Drawing.Size(195, 26);
            form.şifreDeğiştirToolStripMenuItem.Text = "Şifre Değiştir";

            // POS Takip menu
            form.posTakipİşlemleriToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                form.posTakipToolStripMenuItem,
                form.krediKartıTakipToolStripMenuItem
            });
            form.posTakipİşlemleriToolStripMenuItem.ForeColor = System.Drawing.Color.DarkGray;
            form.posTakipİşlemleriToolStripMenuItem.Name = "posTakipİşlemleriToolStripMenuItem";
            form.posTakipİşlemleriToolStripMenuItem.Size = new System.Drawing.Size(147, 48);
            form.posTakipİşlemleriToolStripMenuItem.Text = "Pos Takip İşlemleri";

            form.posTakipToolStripMenuItem.Name = "posTakipToolStripMenuItem";
            form.posTakipToolStripMenuItem.Size = new System.Drawing.Size(204, 26);
            form.posTakipToolStripMenuItem.Text = "Pos Takip";

            form.krediKartıTakipToolStripMenuItem.Name = "krediKartıTakipToolStripMenuItem";
            form.krediKartıTakipToolStripMenuItem.Size = new System.Drawing.Size(204, 26);
            form.krediKartıTakipToolStripMenuItem.Text = "Kredi Kartı Takip";

            // Yardım menu
            form.yardımToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                form.hakkımızdaToolStripMenuItem,
                form.güncellemeDenetleToolStripMenuItem
            });
            form.yardımToolStripMenuItem.ForeColor = System.Drawing.Color.DarkGray;
            form.yardımToolStripMenuItem.Name = "yardımToolStripMenuItem";
            form.yardımToolStripMenuItem.Size = new System.Drawing.Size(71, 48);
            form.yardımToolStripMenuItem.Text = "Yardım";

            form.hakkımızdaToolStripMenuItem.Name = "hakkımızdaToolStripMenuItem";
            form.hakkımızdaToolStripMenuItem.Size = new System.Drawing.Size(229, 26);
            form.hakkımızdaToolStripMenuItem.Text = "Hakkımızda";

            form.güncellemeDenetleToolStripMenuItem.Name = "güncellemeDenetleToolStripMenuItem";
            form.güncellemeDenetleToolStripMenuItem.Size = new System.Drawing.Size(229, 26);
            form.güncellemeDenetleToolStripMenuItem.Text = "Güncelleme Denetle";

            // Çıkış
            form.çıkışToolStripMenuItem.ForeColor = System.Drawing.Color.Crimson;
            form.çıkışToolStripMenuItem.Name = "çıkışToolStripMenuItem";
            form.çıkışToolStripMenuItem.Size = new System.Drawing.Size(54, 48);
            form.çıkışToolStripMenuItem.Text = "Çıkış";
        }

        private void SetupButtonProperties(Carihareketler form)
        {
            // Window control buttons
            form.btnMinimize.BorderRadius = 18;
            form.btnMinimize.FillColor = System.Drawing.Color.FromArgb(67, 162, 243);
            form.btnMinimize.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            form.btnMinimize.ForeColor = System.Drawing.Color.White;
            form.btnMinimize.Location = new System.Drawing.Point(12, 8);
            form.btnMinimize.Name = "btnMinimize";
            form.btnMinimize.Size = new System.Drawing.Size(36, 36);
            form.btnMinimize.TabIndex = 1;
            form.btnMinimize.Text = "_";
            form.btnMinimize.TextOffset = new System.Drawing.Point(0, -2);

            form.btnClose.BorderRadius = 18;
            form.btnClose.FillColor = System.Drawing.Color.Crimson;
            form.btnClose.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            form.btnClose.ForeColor = System.Drawing.Color.White;
            form.btnClose.Location = new System.Drawing.Point(57, 8);
            form.btnClose.Name = "btnClose";
            form.btnClose.Size = new System.Drawing.Size(36, 36);
            form.btnClose.TabIndex = 2;
            form.btnClose.Text = "X";

            // Header buttons
            form.button3.BorderRadius = 8;
            form.button3.FillColor = System.Drawing.Color.FromArgb(67, 162, 243);
            form.button3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            form.button3.ForeColor = System.Drawing.Color.White;
            form.button3.Location = new System.Drawing.Point(0, 0);
            form.button3.Name = "button3";
            form.button3.Size = new System.Drawing.Size(726, 60);
            form.button3.TabIndex = 46;
            form.button3.Text = "TEST FİRMA-4";
            form.button3.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;

            form.oncekicari.BorderRadius = 8;
            form.oncekicari.Cursor = System.Windows.Forms.Cursors.Hand;
            form.oncekicari.FillColor = System.Drawing.Color.FromArgb(67, 162, 243);
            form.oncekicari.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            form.oncekicari.ForeColor = System.Drawing.Color.LawnGreen;
            form.oncekicari.Location = new System.Drawing.Point(15, 10);
            form.oncekicari.Name = "oncekicari";
            form.oncekicari.Size = new System.Drawing.Size(60, 60);
            form.oncekicari.TabIndex = 44;
            form.oncekicari.Text = "◀";

            form.sonrakicari.BorderRadius = 8;
            form.sonrakicari.Cursor = System.Windows.Forms.Cursors.Hand;
            form.sonrakicari.FillColor = System.Drawing.Color.FromArgb(67, 162, 243);
            form.sonrakicari.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            form.sonrakicari.ForeColor = System.Drawing.Color.LawnGreen;
            form.sonrakicari.Location = new System.Drawing.Point(818, 10);
            form.sonrakicari.Name = "sonrakicari";
            form.sonrakicari.Size = new System.Drawing.Size(60, 60);
            form.sonrakicari.TabIndex = 45;
            form.sonrakicari.Text = "▶";

            // Action buttons WITH ICONS - Güvenli şekilde
            SetupActionButtonWithIcon(form.notekle, "Notlar", 0, 0, 115, 45, true, "notebook");
            SetupActionButtonWithIcon(form.analiz, "İstatistik", 140, 9, 120, 45, false, "grafik");
            SetupActionButtonWithIcon(form.Sorgulabtn, "Sorgula", 270, 9, 120, 45, false, "aramayap_32");
            SetupActionButtonWithIcon(form.eposta, "E-Posta", 400, 9, 120, 45, false, "eposta");
            SetupActionButtonWithIcon(form.yazdir, "Yazdır", 530, 9, 115, 45, false, "yazici");
            SetupActionButtonWithIcon(form.Excelaktar, "Excel", 655, 9, 100, 45, false, "excelaktar");
            SetupActionButtonWithIcon(form.emanet, "Emanet", 0, 0, 110, 45, true, "emanet");

            // Bottom buttons WITH ICONS - Güvenli şekilde
            SetupBottomButtonWithIcon(form.hrketekle, "Ekle", 15, 3, 95, 42, "add");
            SetupBottomButtonWithIcon(form.hrktdegistir, "Değiştir", 125, 3, 120, 42, "edit");
            SetupBottomButtonWithIcon(form.hrktsil, "Sil", 260, 3, 95, 42, "delete");
            SetupBottomButtonWithIcon(form.gecikmesorgula, "Gecikmiş Sorgula", 554, 3, 185, 42, "warning2_32");
            SetupBottomButtonWithIcon(form.hrktlergerigit, "Geri", 780, 3, 95, 42, "_return");

            // button13 (disabled)
            form.button13.BorderRadius = 12;
            form.button13.Cursor = System.Windows.Forms.Cursors.Default;
            form.button13.Enabled = false;
            form.button13.FillColor = System.Drawing.Color.FromArgb(67, 162, 243);
            form.button13.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            form.button13.ForeColor = System.Drawing.Color.White;
            form.button13.Location = new System.Drawing.Point(370, 3);
            form.button13.Name = "button13";
            form.button13.Size = new System.Drawing.Size(161, 42);
            form.button13.TabIndex = 35;
            form.button13.Text = "İşlem Sayısı =";
        }

        private void SetupActionButtonWithIcon(Guna2Button button, string text, int x, int y, int width, int height, bool dock, string iconName)
        {
            button.BorderRadius = 12;
            button.Cursor = System.Windows.Forms.Cursors.Hand;
            button.FillColor = System.Drawing.Color.FromArgb(67, 162, 243);
            button.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            button.ForeColor = System.Drawing.Color.White;
            button.Name = text.ToLower().Replace(" ", "").Replace("-", "");
            button.Size = new System.Drawing.Size(width, height);
            button.TabIndex = 47;
            button.Text = text;
            button.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            button.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;

            // ICON YÜKLEME - Güvenli şekilde
            try
            {
                var resourceManager = global::Veresiye2025.Properties.Resources.ResourceManager;
                var icon = resourceManager.GetObject(iconName) as System.Drawing.Image;
                if (icon != null)
                {
                    button.Image = icon;
                }
            }
            catch
            {
                // Icon yoksa boş bırak
                button.Image = null;
            }

            if (dock)
            {
                button.Dock = System.Windows.Forms.DockStyle.Fill;
                button.Location = new System.Drawing.Point(0, 0);
            }
            else
            {
                button.Location = new System.Drawing.Point(x, y);
            }
        }

        private void SetupBottomButtonWithIcon(Guna2Button button, string text, int x, int y, int width, int height, string iconName)
        {
            button.BorderRadius = 12;
            button.Cursor = System.Windows.Forms.Cursors.Hand;
            button.FillColor = System.Drawing.Color.FromArgb(67, 162, 243);
            button.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            button.ForeColor = System.Drawing.Color.White;
            button.Location = new System.Drawing.Point(x, y);
            button.Name = text.ToLower().Replace(" ", "");
            button.Size = new System.Drawing.Size(width, height);
            button.Text = text;
            button.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            button.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;

            // ICON YÜKLEME - Güvenli şekilde
            try
            {
                var resourceManager = global::Veresiye2025.Properties.Resources.ResourceManager;
                var icon = resourceManager.GetObject(iconName) as System.Drawing.Image;
                if (icon != null)
                {
                    button.Image = icon;
                }
            }
            catch
            {
                // Icon yoksa boş bırak
                button.Image = null;
            }
        }

        private void SetupDataGridViewProperties(Carihareketler form)
        {
            // ÖNCE AutoGenerate'i kapat ve temizle
            form.dataGridView1.AutoGenerateColumns = false;
            form.dataGridView1.Columns.Clear();
            form.dataGridView1.DataSource = null; // DataSource'u temizle

            // ==== MODERN GÖRÜNÜM AYARLARI ====
            form.dataGridView1.BackgroundColor = Color.White;
            form.dataGridView1.BorderStyle = BorderStyle.None;
            form.dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            form.dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(67, 162, 243);
            form.dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;
            form.dataGridView1.DefaultCellStyle.BackColor = Color.White;
            form.dataGridView1.DefaultCellStyle.ForeColor = Color.FromArgb(64, 64, 64);
            form.dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            form.dataGridView1.RowTemplate.Height = 35;

            // Header stili
            form.dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(28, 141, 243);
            form.dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            form.dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            form.dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            form.dataGridView1.ColumnHeadersHeight = 40;
            form.dataGridView1.EnableHeadersVisualStyles = false;

            // Diğer ayarlar
            form.dataGridView1.RowHeadersVisible = false;
            form.dataGridView1.AllowUserToAddRows = false;
            form.dataGridView1.AllowUserToDeleteRows = false;
            form.dataGridView1.AllowUserToResizeRows = false;
            form.dataGridView1.ReadOnly = true;
            form.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            form.dataGridView1.MultiSelect = false;
            form.dataGridView1.ShowCellToolTips = true;
            form.dataGridView1.GridColor = Color.FromArgb(230, 230, 230);

            // ==== BOŞLUK PROBLEMİNİ ÇÖZMEK İÇİN ====
            form.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            form.dataGridView1.Dock = DockStyle.Fill;

            // Sütunları orijinal şekilde oluştur
            SetupDataGridViewColumns(form);

            // ContextMenuStrip
            SetupContextMenu(form);
        }

        private void SetupDataGridViewColumns(Carihareketler form)
        {
            // ==== SÜTUNLARI OLUŞTUR (ORIJINAL GİBİ) ====

            // ID Sütunu (Gizli)
            form.id = new DataGridViewTextBoxColumn
            {
                Name = "id",
                HeaderText = "ID",
                Visible = false
            };
            form.dataGridView1.Columns.Add(form.id);

            // Cari Kodu (Gizli)
            form.cari_kodu = new DataGridViewTextBoxColumn
            {
                Name = "cari_kodu",
                HeaderText = "Cari Kodu",
                Visible = false
            };
            form.dataGridView1.Columns.Add(form.cari_kodu);

            // Tarih Sütunu
            form.Tarih = new DataGridViewTextBoxColumn
            {
                Name = "Tarih",
                HeaderText = "📅 Tarih",
                FillWeight = 15F, // %15 genişlik
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "dd.MM.yyyy",
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 9F, FontStyle.Regular)
                }
            };
            form.dataGridView1.Columns.Add(form.Tarih);

            // Tür Sütunu
            form.Tür = new DataGridViewTextBoxColumn
            {
                Name = "Tür",
                HeaderText = "🏷️ Tür",
                FillWeight = 20F, // %20 genişlik
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold)
                }
            };
            form.dataGridView1.Columns.Add(form.Tür);

            // Açıklama Sütunu
            form.Açıklama = new DataGridViewTextBoxColumn
            {
                Name = "Açıklama",
                HeaderText = "📝 Açıklama",
                FillWeight = 30F, // %30 genişlik
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleLeft,
                    Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                    WrapMode = DataGridViewTriState.True
                }
            };
            form.dataGridView1.Columns.Add(form.Açıklama);

            // Borç Sütunu
            form.Borç = new DataGridViewTextBoxColumn
            {
                Name = "Borç",
                HeaderText = "💰 Borç",
                FillWeight = 12F, // %12 genişlik
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "C2",
                    Alignment = DataGridViewContentAlignment.MiddleRight,
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    ForeColor = Color.Crimson
                }
            };
            form.dataGridView1.Columns.Add(form.Borç);

            // Tahsilat/Alacak Sütunu
            form.Alacak = new DataGridViewTextBoxColumn
            {
                Name = "Tahsilat", // Orijinalinizde "Alacak" olarak
                HeaderText = "💵 Tahsilat",
                FillWeight = 12F, // %12 genişlik
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "C2",
                    Alignment = DataGridViewContentAlignment.MiddleRight,
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    ForeColor = Color.ForestGreen
                }
            };
            form.dataGridView1.Columns.Add(form.Alacak);

            // Kalan Sütunu
            form.Kalan = new DataGridViewTextBoxColumn
            {
                Name = "Kalan",
                HeaderText = "⚖️ Kalan",
                FillWeight = 11F, // %11 genişlik
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "C2",
                    Alignment = DataGridViewContentAlignment.MiddleRight,
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    ForeColor = Color.DarkBlue
                }
            };
            form.dataGridView1.Columns.Add(form.Kalan);
        }



        private void SetupContextMenu(Carihareketler form)
        {
            form.contextMenuStrip1.BackColor = System.Drawing.Color.FromArgb(28, 141, 243);
            form.contextMenuStrip1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            form.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            form.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                form.borcekle,
                form.tahsilatekle
            });
            form.contextMenuStrip1.Name = "contextMenuStrip1";
            form.contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            form.contextMenuStrip1.Size = new System.Drawing.Size(228, 56);

            // Context menu items WITH ICONS
            form.borcekle.BackColor = System.Drawing.Color.FromArgb(67, 162, 243);
            form.borcekle.ForeColor = System.Drawing.Color.White;
            form.borcekle.Name = "borcekle";
            form.borcekle.ShortcutKeys = System.Windows.Forms.Keys.F5;
            form.borcekle.Size = new System.Drawing.Size(227, 26);
            form.borcekle.Text = "Borç Ekle [F5]";

            form.tahsilatekle.BackColor = System.Drawing.Color.FromArgb(67, 162, 243);
            form.tahsilatekle.ForeColor = System.Drawing.Color.White;
            form.tahsilatekle.Name = "tahsilatekle";
            form.tahsilatekle.ShortcutKeys = System.Windows.Forms.Keys.F6;
            form.tahsilatekle.Size = new System.Drawing.Size(227, 26);
            form.tahsilatekle.Text = "Tahsilat Ekle [F6]";

            // Context menu iconları ekle
            try
            {
                var resourceManager = global::Veresiye2025.Properties.Resources.ResourceManager;
                var addIcon = resourceManager.GetObject("add") as System.Drawing.Image;
                if (addIcon != null)
                {
                    form.borcekle.Image = addIcon;
                    form.tahsilatekle.Image = addIcon;
                }
            }
            catch
            {
                // Icon yoksa boş bırak
            }

            form.dataGridView1.ContextMenuStrip = form.contextMenuStrip1;
        }

        private void SetupFooterProperties(Carihareketler form)
        {
            // Summary labels
            form.label1.AutoSize = true;
            form.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            form.label1.Location = new System.Drawing.Point(17, 5);
            form.label1.Name = "label1";
            form.label1.Size = new System.Drawing.Size(101, 20);
            form.label1.TabIndex = 29;
            form.label1.Text = "Toplam Borç:";

            form.label2.AutoSize = true;
            form.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            form.label2.Location = new System.Drawing.Point(335, 5);
            form.label2.Name = "label2";
            form.label2.Size = new System.Drawing.Size(116, 20);
            form.label2.TabIndex = 31;
            form.label2.Text = "Toplam Alacak:";

            form.label3.AutoSize = true;
            form.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            form.label3.ForeColor = System.Drawing.Color.Red;
            form.label3.Location = new System.Drawing.Point(629, 5);
            form.label3.Name = "label3";
            form.label3.Size = new System.Drawing.Size(59, 20);
            form.label3.TabIndex = 33;
            form.label3.Text = "Bakiye:";

            // Summary textboxes
            SetupSummaryTextBox(form.textBox1, 140, 2, 180, 26, 30);
            SetupSummaryTextBox(form.textBox2, 448, 2, 170, 26, 32);
            SetupSummaryTextBox(form.textBox3, 693, 2, 180, 26, 34);
        }

        private void SetupSummaryTextBox(Guna2TextBox textBox, int x, int y, int width, int height, int tabIndex)
        {
            textBox.BorderRadius = 8;
            textBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            textBox.ForeColor = System.Drawing.Color.Black;
            textBox.Location = new System.Drawing.Point(x, y);
            textBox.ReadOnly = true;
            textBox.Size = new System.Drawing.Size(width, height);
            textBox.TabIndex = tabIndex;
        }

        private void SetupOtherProperties(Carihareketler form, ComponentResourceManager resources)
        {
            // Print components
            form.printPreviewDialog.AutoScrollMargin = new System.Drawing.Size(0, 0);
            form.printPreviewDialog.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            form.printPreviewDialog.ClientSize = new System.Drawing.Size(400, 300);
            form.printPreviewDialog.Enabled = true;
            form.printPreviewDialog.Name = "printPreviewDialog";
            form.printPreviewDialog.Text = "Baskı önizleme";
            form.printPreviewDialog.Visible = false;

            form.printDialog.AllowCurrentPage = true;
            form.printDialog.AllowSelection = true;
            form.printDialog.AllowSomePages = true;
            form.printDialog.PrintToFile = true;
            form.printDialog.UseEXDialog = true;

            // Chart
            var chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            var legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            var series1 = new System.Windows.Forms.DataVisualization.Charting.Series();

            chartArea1.Name = "ChartArea1";
            form.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            form.chart1.Legends.Add(legend1);
            form.chart1.Location = new System.Drawing.Point(0, 0);
            form.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            form.chart1.Series.Add(series1);
            form.chart1.Size = new System.Drawing.Size(300, 300);
            form.chart1.TabIndex = 0;
            form.chart1.Text = "chart1";
        }

        private void SetupLayout(Carihareketler form)
        {
            form.SuspendLayout();

            // Panel hierarchies
            form.panelTop.Controls.Add(form.menuContainer);
            form.panelTop.Controls.Add(form.windowControlsPanel);
            form.menuContainer.Controls.Add(form.menuStrip1);
            form.windowControlsPanel.Controls.Add(form.btnMinimize);
            form.windowControlsPanel.Controls.Add(form.btnClose);

            form.panelHeader.Controls.Add(form.firmPanel);
            form.panelHeader.Controls.Add(form.oncekicari);
            form.panelHeader.Controls.Add(form.sonrakicari);
            form.firmPanel.Controls.Add(form.button3);

            form.panelButtons.Controls.Add(form.notekleContainer);
            form.panelButtons.Controls.Add(form.analiz);
            form.panelButtons.Controls.Add(form.Sorgulabtn);
            form.panelButtons.Controls.Add(form.eposta);
            form.panelButtons.Controls.Add(form.yazdir);
            form.panelButtons.Controls.Add(form.Excelaktar);
            form.panelButtons.Controls.Add(form.emanetContainer);
            form.notekleContainer.Controls.Add(form.notekle);
            form.emanetContainer.Controls.Add(form.emanet);

            form.panelContent.Controls.Add(form.dataGridView1);

            form.panelFooter.Controls.Add(form.bottomButtonsPanel);
            form.panelFooter.Controls.Add(form.summaryPanel);
            form.bottomButtonsPanel.Controls.Add(form.hrketekle);
            form.bottomButtonsPanel.Controls.Add(form.hrktdegistir);
            form.bottomButtonsPanel.Controls.Add(form.hrktsil);
            form.bottomButtonsPanel.Controls.Add(form.gecikmesorgula);
            form.bottomButtonsPanel.Controls.Add(form.hrktlergerigit);
            form.bottomButtonsPanel.Controls.Add(form.button13);

            form.summaryPanel.Controls.Add(form.textBox3);
            form.summaryPanel.Controls.Add(form.label3);
            form.summaryPanel.Controls.Add(form.textBox2);
            form.summaryPanel.Controls.Add(form.label2);
            form.summaryPanel.Controls.Add(form.textBox1);
            form.summaryPanel.Controls.Add(form.label1);

            // Add main panels to form
            form.Controls.Add(form.panelContent);
            form.Controls.Add(form.panelFooter);
            form.Controls.Add(form.panelButtons);
            form.Controls.Add(form.panelHeader);
            form.Controls.Add(form.panelTop);

            form.ResumeLayout(false);
        }

        private void SetupEventHandlers(Carihareketler form)
        {
            // Window control events
            if (form.btnMinimize != null)
                form.btnMinimize.Click += (s, e) => form.WindowState = FormWindowState.Minimized;
            if (form.btnClose != null)
                form.btnClose.Click += (s, e) => form.Close();

            // Menu events - Form'daki metodları çağır
            if (form.hareketlerF3ToolStripMenuItem != null)
                form.hareketlerF3ToolStripMenuItem.Click += form.hareketlerF3ToolStripMenuItem_Click_1;
            if (form.yedekAlToolStripMenuItem != null)
                form.yedekAlToolStripMenuItem.Click += form.yedekAlToolStripMenuItem_Click;
            if (form.genelAyarlarToolStripMenuItem != null)
                form.genelAyarlarToolStripMenuItem.Click += form.genelAyarlarToolStripMenuItem_Click;
            if (form.şifreDeğiştirToolStripMenuItem != null)
                form.şifreDeğiştirToolStripMenuItem.Click += form.şifreDeğiştirToolStripMenuItem_Click;
            if (form.posTakipToolStripMenuItem != null)
                form.posTakipToolStripMenuItem.Click += form.posTakipToolStripMenuItem_Click;
            if (form.krediKartıTakipToolStripMenuItem != null)
                form.krediKartıTakipToolStripMenuItem.Click += form.krediKartıTakipToolStripMenuItem_Click;
            if (form.hakkımızdaToolStripMenuItem != null)
                form.hakkımızdaToolStripMenuItem.Click += form.hakkımızdaToolStripMenuItem_Click;
            if (form.güncellemeDenetleToolStripMenuItem != null)
                form.güncellemeDenetleToolStripMenuItem.Click += form.güncellemeDenetleToolStripMenuItem_Click;
            if (form.çıkışToolStripMenuItem != null)
                form.çıkışToolStripMenuItem.Click += form.çıkışToolStripMenuItem_Click;

            // Navigation events
            if (form.button3 != null)
                form.button3.Click += form.button3_Click_1;
            if (form.oncekicari != null)
                form.oncekicari.Click += form.oncekicari_Click;
            if (form.sonrakicari != null)
                form.sonrakicari.Click += form.sonrakicari_Click;

            // Action button events
            if (form.notekle != null)
                form.notekle.Click += form.notekle_Click;
            if (form.analiz != null)
                form.analiz.Click += form.analiz_Click;
            if (form.Sorgulabtn != null)
                form.Sorgulabtn.Click += form.Sorgulabtn_Click;
            if (form.yazdir != null)
                form.yazdir.Click += form.yazdir_Click;
            if (form.Excelaktar != null)
                form.Excelaktar.Click += form.Excelaktar_Click;
            if (form.emanet != null)
                form.emanet.Click += form.emanet_Click;

            // Bottom panel events
            if (form.hrketekle != null)
                form.hrketekle.Click += form.hrketekle_Click;
            if (form.hrktdegistir != null)
                form.hrktdegistir.Click += form.hrktdegistir_Click;
            if (form.hrktsil != null)
                form.hrktsil.Click += form.hrktsil_Click;
            if (form.gecikmesorgula != null)
                form.gecikmesorgula.Click += form.gecikmesorgula_Click;
            if (form.hrktlergerigit != null)
                form.hrktlergerigit.Click += form.hrktlergerigit_Click;

            // DataGridView events
            if (form.dataGridView1 != null)
            {

                // Bu satırları kontrol edin - metod adları doğru mu?
                form.dataGridView1.CellFormatting += form.DataGridView1_CellFormatting_Modern;
                form.dataGridView1.CellMouseEnter += form.DataGridView1_CellMouseEnter_Modern;
                form.dataGridView1.CellMouseLeave += form.DataGridView1_CellMouseLeave_Modern;
                form.dataGridView1.CellPainting += form.DataGridView1_CellPainting;
                form.dataGridView1.CellClick += form.DataGridView1_CellClick;
            }

            // Context menu events
            if (form.contextMenuStrip1 != null)
                form.contextMenuStrip1.Opening += form.ContextMenuStrip1_Opening;

            // Print events
            if (form.printDocument != null)
                form.printDocument.PrintPage += form.printDocument_PrintPage;
            // DataGridView events
            if (form.dataGridView1 != null)
            {

                // Orijinal kodunuzdaki event'leri de ekleyin
                form.dataGridView1.CellFormatting += form.DataGridView1_CellFormatting_Modern;
                form.dataGridView1.CellMouseEnter += form.DataGridView1_CellMouseEnter_Modern;
                form.dataGridView1.CellMouseLeave += form.DataGridView1_CellMouseLeave_Modern;
                form.dataGridView1.CellPainting += form.DataGridView1_CellPainting;
                form.dataGridView1.CellClick += form.DataGridView1_CellClick;
            }
        }
    }
}