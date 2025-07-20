using System.Drawing;

namespace Veresiye2025
{
    partial class FormOdemeYapmayanlar
    {
        public System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        public void InitializeComponent()
        {
            this.panelHeader = new System.Windows.Forms.Panel();
            this.guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            this.lblBaslik = new System.Windows.Forms.Label();
            this.lblAltBaslik = new System.Windows.Forms.Label();
            this.panelStats = new System.Windows.Forms.Panel();
            this.tableLayoutPanelStats = new System.Windows.Forms.TableLayoutPanel();
            this.panelStat1 = new System.Windows.Forms.Panel();
            this.lblToplamBorclu = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panelStat2 = new System.Windows.Forms.Panel();
            this.lblToplamBorc = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panelStat3 = new System.Windows.Forms.Panel();
            this.lblVadesiGecen = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panelStat4 = new System.Windows.Forms.Panel();
            this.lblKritikDurum = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panelToolbar = new System.Windows.Forms.Panel();
            this.tableLayoutPanelToolbar = new System.Windows.Forms.TableLayoutPanel();
            this.panelFiltre = new System.Windows.Forms.Panel();
            this.cmbFiltre = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panelSiralama = new System.Windows.Forms.Panel();
            this.cmbSiralama = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panelArama = new System.Windows.Forms.Panel();
            this.btnAra = new System.Windows.Forms.Button();
            this.txtArama = new System.Windows.Forms.TextBox();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnYenile = new System.Windows.Forms.Button();
            this.panelMain = new System.Windows.Forms.Panel();
            this.dgvBorclular = new System.Windows.Forms.DataGridView();
            this.panelProgress = new System.Windows.Forms.Panel();
            this.lblTahsilatYuzde = new System.Windows.Forms.Label();
            this.progressBarTahsilat = new System.Windows.Forms.ProgressBar();
            this.lblTahsilatBaslik = new System.Windows.Forms.Label();
            this.panelBottomActions = new System.Windows.Forms.Panel();
            this.tableLayoutPanelActions = new System.Windows.Forms.TableLayoutPanel();
            this.btnTumunuPasifYap = new System.Windows.Forms.Button();
            this.btnTopluEmail = new System.Windows.Forms.Button();
            this.btnRaporOlustur = new System.Windows.Forms.Button();
            this.btnExcelAktar = new System.Windows.Forms.Button();
            this.lblTopluIslemler = new System.Windows.Forms.Label();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblDurum = new System.Windows.Forms.Label();
            this.lblToplamKayit = new System.Windows.Forms.Label();
            this.panelHeader.SuspendLayout();
            this.panelStats.SuspendLayout();
            this.tableLayoutPanelStats.SuspendLayout();
            this.panelStat1.SuspendLayout();
            this.panelStat2.SuspendLayout();
            this.panelStat3.SuspendLayout();
            this.panelStat4.SuspendLayout();
            this.panelToolbar.SuspendLayout();
            this.tableLayoutPanelToolbar.SuspendLayout();
            this.panelFiltre.SuspendLayout();
            this.panelSiralama.SuspendLayout();
            this.panelArama.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBorclular)).BeginInit();
            this.panelProgress.SuspendLayout();
            this.panelBottomActions.SuspendLayout();
            this.tableLayoutPanelActions.SuspendLayout();
            this.panelFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.MidnightBlue;
            this.panelHeader.Controls.Add(this.guna2Button1);
            this.panelHeader.Controls.Add(this.lblBaslik);
            this.panelHeader.Controls.Add(this.lblAltBaslik);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(2, 2);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Padding = new System.Windows.Forms.Padding(25, 15, 25, 15);
            this.panelHeader.Size = new System.Drawing.Size(916, 80);
            this.panelHeader.TabIndex = 0;
            // 
            // guna2Button1
            // 
            this.guna2Button1.BorderRadius = 12;
            this.guna2Button1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button1.FillColor = System.Drawing.Color.Crimson;
            this.guna2Button1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.guna2Button1.ForeColor = System.Drawing.Color.White;
            this.guna2Button1.Location = new System.Drawing.Point(855, 18);
            this.guna2Button1.Name = "guna2Button1";
            this.guna2Button1.Size = new System.Drawing.Size(48, 42);
            this.guna2Button1.TabIndex = 4;
            this.guna2Button1.Text = "X";
            this.guna2Button1.Click += new System.EventHandler(this.guna2Button1_Click);
            // 
            // lblBaslik
            // 
            this.lblBaslik.AutoSize = true;
            this.lblBaslik.BackColor = System.Drawing.Color.Transparent;
            this.lblBaslik.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblBaslik.ForeColor = System.Drawing.Color.White;
            this.lblBaslik.Location = new System.Drawing.Point(25, 6);
            this.lblBaslik.Name = "lblBaslik";
            this.lblBaslik.Size = new System.Drawing.Size(439, 41);
            this.lblBaslik.TabIndex = 0;
            this.lblBaslik.Text = "Ödeme Yapmayanlar Yönetimi";
            // 
            // lblAltBaslik
            // 
            this.lblAltBaslik.AutoSize = true;
            this.lblAltBaslik.BackColor = System.Drawing.Color.Transparent;
            this.lblAltBaslik.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblAltBaslik.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.lblAltBaslik.Location = new System.Drawing.Point(28, 52);
            this.lblAltBaslik.Name = "lblAltBaslik";
            this.lblAltBaslik.Size = new System.Drawing.Size(396, 23);
            this.lblAltBaslik.TabIndex = 1;
            this.lblAltBaslik.Text = "Son 30 gün içinde ödeme yapmayan müşteri takibi";
            // 
            // panelStats
            // 
            this.panelStats.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.panelStats.Controls.Add(this.tableLayoutPanelStats);
            this.panelStats.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelStats.Location = new System.Drawing.Point(2, 82);
            this.panelStats.Name = "panelStats";
            this.panelStats.Padding = new System.Windows.Forms.Padding(20, 15, 20, 15);
            this.panelStats.Size = new System.Drawing.Size(916, 90);
            this.panelStats.TabIndex = 1;
            // 
            // tableLayoutPanelStats
            // 
            this.tableLayoutPanelStats.ColumnCount = 4;
            this.tableLayoutPanelStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelStats.Controls.Add(this.panelStat1, 0, 0);
            this.tableLayoutPanelStats.Controls.Add(this.panelStat2, 1, 0);
            this.tableLayoutPanelStats.Controls.Add(this.panelStat3, 2, 0);
            this.tableLayoutPanelStats.Controls.Add(this.panelStat4, 3, 0);
            this.tableLayoutPanelStats.Location = new System.Drawing.Point(3, -2);
            this.tableLayoutPanelStats.Name = "tableLayoutPanelStats";
            this.tableLayoutPanelStats.RowCount = 1;
            this.tableLayoutPanelStats.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelStats.Size = new System.Drawing.Size(910, 86);
            this.tableLayoutPanelStats.TabIndex = 0;
            // 
            // panelStat1
            // 
            this.panelStat1.BackColor = System.Drawing.Color.White;
            this.panelStat1.Controls.Add(this.lblToplamBorclu);
            this.panelStat1.Controls.Add(this.label1);
            this.panelStat1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelStat1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelStat1.Location = new System.Drawing.Point(8, 5);
            this.panelStat1.Margin = new System.Windows.Forms.Padding(8, 5, 8, 5);
            this.panelStat1.Name = "panelStat1";
            this.panelStat1.Padding = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.panelStat1.Size = new System.Drawing.Size(211, 76);
            this.panelStat1.TabIndex = 0;
            // 
            // lblToplamBorclu
            // 
            this.lblToplamBorclu.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblToplamBorclu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.lblToplamBorclu.Location = new System.Drawing.Point(18, 38);
            this.lblToplamBorclu.Name = "lblToplamBorclu";
            this.lblToplamBorclu.Size = new System.Drawing.Size(173, 33);
            this.lblToplamBorclu.TabIndex = 0;
            this.lblToplamBorclu.Text = "4";
            this.lblToplamBorclu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.label1.Location = new System.Drawing.Point(15, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(181, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Toplam Borçlu";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelStat2
            // 
            this.panelStat2.BackColor = System.Drawing.Color.White;
            this.panelStat2.Controls.Add(this.lblToplamBorc);
            this.panelStat2.Controls.Add(this.label3);
            this.panelStat2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelStat2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelStat2.Location = new System.Drawing.Point(235, 5);
            this.panelStat2.Margin = new System.Windows.Forms.Padding(8, 5, 8, 5);
            this.panelStat2.Name = "panelStat2";
            this.panelStat2.Padding = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.panelStat2.Size = new System.Drawing.Size(211, 76);
            this.panelStat2.TabIndex = 1;
            // 
            // lblToplamBorc
            // 
            this.lblToplamBorc.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblToplamBorc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(133)))), ((int)(((byte)(27)))));
            this.lblToplamBorc.Location = new System.Drawing.Point(13, 35);
            this.lblToplamBorc.Name = "lblToplamBorc";
            this.lblToplamBorc.Size = new System.Drawing.Size(173, 36);
            this.lblToplamBorc.TabIndex = 0;
            this.lblToplamBorc.Text = "₺15,155";
            this.lblToplamBorc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.label3.Location = new System.Drawing.Point(15, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(181, 25);
            this.label3.TabIndex = 1;
            this.label3.Text = "Toplam Borç";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelStat3
            // 
            this.panelStat3.BackColor = System.Drawing.Color.White;
            this.panelStat3.Controls.Add(this.lblVadesiGecen);
            this.panelStat3.Controls.Add(this.label5);
            this.panelStat3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelStat3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelStat3.Location = new System.Drawing.Point(462, 5);
            this.panelStat3.Margin = new System.Windows.Forms.Padding(8, 5, 8, 5);
            this.panelStat3.Name = "panelStat3";
            this.panelStat3.Padding = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.panelStat3.Size = new System.Drawing.Size(211, 76);
            this.panelStat3.TabIndex = 2;
            // 
            // lblVadesiGecen
            // 
            this.lblVadesiGecen.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblVadesiGecen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.lblVadesiGecen.Location = new System.Drawing.Point(13, 35);
            this.lblVadesiGecen.Name = "lblVadesiGecen";
            this.lblVadesiGecen.Size = new System.Drawing.Size(173, 36);
            this.lblVadesiGecen.TabIndex = 0;
            this.lblVadesiGecen.Text = "₺15,155";
            this.lblVadesiGecen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.label5.Location = new System.Drawing.Point(15, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(181, 25);
            this.label5.TabIndex = 1;
            this.label5.Text = "Vadesi Geçen";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelStat4
            // 
            this.panelStat4.BackColor = System.Drawing.Color.White;
            this.panelStat4.Controls.Add(this.lblKritikDurum);
            this.panelStat4.Controls.Add(this.label7);
            this.panelStat4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelStat4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelStat4.Location = new System.Drawing.Point(689, 5);
            this.panelStat4.Margin = new System.Windows.Forms.Padding(8, 5, 8, 5);
            this.panelStat4.Name = "panelStat4";
            this.panelStat4.Padding = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.panelStat4.Size = new System.Drawing.Size(213, 76);
            this.panelStat4.TabIndex = 3;
            // 
            // lblKritikDurum
            // 
            this.lblKritikDurum.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblKritikDurum.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(27)))), ((int)(((byte)(62)))));
            this.lblKritikDurum.Location = new System.Drawing.Point(18, 35);
            this.lblKritikDurum.Name = "lblKritikDurum";
            this.lblKritikDurum.Size = new System.Drawing.Size(173, 36);
            this.lblKritikDurum.TabIndex = 0;
            this.lblKritikDurum.Text = "3";
            this.lblKritikDurum.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.Dock = System.Windows.Forms.DockStyle.Top;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.label7.Location = new System.Drawing.Point(15, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(183, 23);
            this.label7.TabIndex = 1;
            this.label7.Text = "Kritik Durum";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelToolbar
            // 
            this.panelToolbar.BackColor = System.Drawing.Color.White;
            this.panelToolbar.Controls.Add(this.tableLayoutPanelToolbar);
            this.panelToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelToolbar.Location = new System.Drawing.Point(2, 172);
            this.panelToolbar.Name = "panelToolbar";
            this.panelToolbar.Padding = new System.Windows.Forms.Padding(25, 12, 25, 12);
            this.panelToolbar.Size = new System.Drawing.Size(916, 60);
            this.panelToolbar.TabIndex = 2;
            // 
            // tableLayoutPanelToolbar
            // 
            this.tableLayoutPanelToolbar.ColumnCount = 4;
            this.tableLayoutPanelToolbar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelToolbar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelToolbar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.29792F));
            this.tableLayoutPanelToolbar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.81755F));
            this.tableLayoutPanelToolbar.Controls.Add(this.panelFiltre, 0, 0);
            this.tableLayoutPanelToolbar.Controls.Add(this.panelSiralama, 1, 0);
            this.tableLayoutPanelToolbar.Controls.Add(this.panelArama, 2, 0);
            this.tableLayoutPanelToolbar.Controls.Add(this.panelButtons, 3, 0);
            this.tableLayoutPanelToolbar.Location = new System.Drawing.Point(25, 12);
            this.tableLayoutPanelToolbar.Name = "tableLayoutPanelToolbar";
            this.tableLayoutPanelToolbar.RowCount = 1;
            this.tableLayoutPanelToolbar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelToolbar.Size = new System.Drawing.Size(880, 36);
            this.tableLayoutPanelToolbar.TabIndex = 0;
            // 
            // panelFiltre
            // 
            this.panelFiltre.Controls.Add(this.cmbFiltre);
            this.panelFiltre.Controls.Add(this.label2);
            this.panelFiltre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFiltre.Location = new System.Drawing.Point(3, 3);
            this.panelFiltre.Name = "panelFiltre";
            this.panelFiltre.Size = new System.Drawing.Size(213, 30);
            this.panelFiltre.TabIndex = 0;
            // 
            // cmbFiltre
            // 
            this.cmbFiltre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbFiltre.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.cmbFiltre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFiltre.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbFiltre.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbFiltre.FormattingEnabled = true;
            this.cmbFiltre.Location = new System.Drawing.Point(63, 3);
            this.cmbFiltre.Name = "cmbFiltre";
            this.cmbFiltre.Size = new System.Drawing.Size(155, 28);
            this.cmbFiltre.TabIndex = 1;
            this.cmbFiltre.SelectedIndexChanged += new System.EventHandler(this.cmbFiltre_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(80)))), ((int)(((byte)(87)))));
            this.label2.Location = new System.Drawing.Point(0, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 23);
            this.label2.TabIndex = 0;
            this.label2.Text = "Filtre:";
            // 
            // panelSiralama
            // 
            this.panelSiralama.Controls.Add(this.cmbSiralama);
            this.panelSiralama.Controls.Add(this.label4);
            this.panelSiralama.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSiralama.Location = new System.Drawing.Point(222, 3);
            this.panelSiralama.Name = "panelSiralama";
            this.panelSiralama.Size = new System.Drawing.Size(213, 30);
            this.panelSiralama.TabIndex = 1;
            // 
            // cmbSiralama
            // 
            this.cmbSiralama.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSiralama.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.cmbSiralama.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSiralama.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSiralama.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbSiralama.FormattingEnabled = true;
            this.cmbSiralama.Location = new System.Drawing.Point(80, 3);
            this.cmbSiralama.Name = "cmbSiralama";
            this.cmbSiralama.Size = new System.Drawing.Size(130, 28);
            this.cmbSiralama.TabIndex = 1;
            this.cmbSiralama.SelectedIndexChanged += new System.EventHandler(this.cmbSiralama_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(80)))), ((int)(((byte)(87)))));
            this.label4.Location = new System.Drawing.Point(0, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 23);
            this.label4.TabIndex = 0;
            this.label4.Text = "Sıralama:";
            // 
            // panelArama
            // 
            this.panelArama.Controls.Add(this.btnAra);
            this.panelArama.Controls.Add(this.txtArama);
            this.panelArama.Location = new System.Drawing.Point(441, 3);
            this.panelArama.Name = "panelArama";
            this.panelArama.Size = new System.Drawing.Size(316, 30);
            this.panelArama.TabIndex = 2;
            // 
            // btnAra
            // 
            this.btnAra.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAra.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(81)))), ((int)(((byte)(255)))));
            this.btnAra.FlatAppearance.BorderSize = 0;
            this.btnAra.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAra.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAra.ForeColor = System.Drawing.Color.White;
            this.btnAra.Location = new System.Drawing.Point(247, 0);
            this.btnAra.Name = "btnAra";
            this.btnAra.Size = new System.Drawing.Size(69, 30);
            this.btnAra.TabIndex = 1;
            this.btnAra.Text = "🔍 Ara";
            this.btnAra.UseVisualStyleBackColor = false;
            // 
            // txtArama
            // 
            this.txtArama.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtArama.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.txtArama.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtArama.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtArama.Location = new System.Drawing.Point(8, 6);
            this.txtArama.Multiline = true;
            this.txtArama.Name = "txtArama";
            this.txtArama.Size = new System.Drawing.Size(230, 24);
            this.txtArama.TabIndex = 0;
            this.txtArama.Text = "Müşteri adı, cari kod veya telefon ara...";
            this.txtArama.TextChanged += new System.EventHandler(this.txtArama_TextChanged);
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.btnYenile);
            this.panelButtons.Location = new System.Drawing.Point(768, 3);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(91, 30);
            this.panelButtons.TabIndex = 3;
            // 
            // btnYenile
            // 
            this.btnYenile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnYenile.FlatAppearance.BorderSize = 0;
            this.btnYenile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnYenile.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnYenile.ForeColor = System.Drawing.Color.White;
            this.btnYenile.Location = new System.Drawing.Point(0, 1);
            this.btnYenile.Name = "btnYenile";
            this.btnYenile.Size = new System.Drawing.Size(84, 30);
            this.btnYenile.TabIndex = 0;
            this.btnYenile.Text = "🔄 Yenile";
            this.btnYenile.UseVisualStyleBackColor = false;
            this.btnYenile.Click += new System.EventHandler(this.btnYenile_Click);
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.panelMain.Controls.Add(this.dgvBorclular);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(2, 232);
            this.panelMain.Name = "panelMain";
            this.panelMain.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.panelMain.Size = new System.Drawing.Size(916, 246);
            this.panelMain.TabIndex = 3;
            // 
            // dgvBorclular
            // 
            this.dgvBorclular.AllowUserToAddRows = false;
            this.dgvBorclular.AllowUserToDeleteRows = false;
            this.dgvBorclular.AllowUserToResizeColumns = false;
            this.dgvBorclular.AllowUserToResizeRows = false;
            this.dgvBorclular.BackgroundColor = System.Drawing.Color.White;
            this.dgvBorclular.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvBorclular.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvBorclular.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvBorclular.ColumnHeadersHeight = 40;
            this.dgvBorclular.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvBorclular.EnableHeadersVisualStyles = false;
            this.dgvBorclular.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.dgvBorclular.Location = new System.Drawing.Point(20, 10);
            this.dgvBorclular.MultiSelect = false;
            this.dgvBorclular.Name = "dgvBorclular";
            this.dgvBorclular.ReadOnly = true;
            this.dgvBorclular.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvBorclular.RowHeadersVisible = false;
            this.dgvBorclular.RowHeadersWidth = 51;
            this.dgvBorclular.RowTemplate.Height = 40;
            this.dgvBorclular.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvBorclular.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBorclular.Size = new System.Drawing.Size(876, 226);
            this.dgvBorclular.TabIndex = 0;
            this.dgvBorclular.DoubleClick += new System.EventHandler(this.dgvBorclular_DoubleClick);
            // 
            // panelProgress
            // 
            this.panelProgress.BackColor = System.Drawing.Color.White;
            this.panelProgress.Controls.Add(this.lblTahsilatYuzde);
            this.panelProgress.Controls.Add(this.progressBarTahsilat);
            this.panelProgress.Controls.Add(this.lblTahsilatBaslik);
            this.panelProgress.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelProgress.Location = new System.Drawing.Point(2, 478);
            this.panelProgress.Name = "panelProgress";
            this.panelProgress.Padding = new System.Windows.Forms.Padding(25, 8, 25, 8);
            this.panelProgress.Size = new System.Drawing.Size(916, 45);
            this.panelProgress.TabIndex = 4;
            // 
            // lblTahsilatYuzde
            // 
            this.lblTahsilatYuzde.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTahsilatYuzde.AutoSize = true;
            this.lblTahsilatYuzde.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTahsilatYuzde.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(80)))), ((int)(((byte)(87)))));
            this.lblTahsilatYuzde.Location = new System.Drawing.Point(705, 13);
            this.lblTahsilatYuzde.Name = "lblTahsilatYuzde";
            this.lblTahsilatYuzde.Size = new System.Drawing.Size(147, 20);
            this.lblTahsilatYuzde.TabIndex = 2;
            this.lblTahsilatYuzde.Text = "%0 (₺0 / ₺100,000)";
            // 
            // progressBarTahsilat
            // 
            this.progressBarTahsilat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarTahsilat.Location = new System.Drawing.Point(211, 6);
            this.progressBarTahsilat.Name = "progressBarTahsilat";
            this.progressBarTahsilat.Size = new System.Drawing.Size(488, 45);
            this.progressBarTahsilat.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBarTahsilat.TabIndex = 1;
            // 
            // lblTahsilatBaslik
            // 
            this.lblTahsilatBaslik.AutoSize = true;
            this.lblTahsilatBaslik.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTahsilatBaslik.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(80)))), ((int)(((byte)(87)))));
            this.lblTahsilatBaslik.Location = new System.Drawing.Point(25, 14);
            this.lblTahsilatBaslik.Name = "lblTahsilatBaslik";
            this.lblTahsilatBaslik.Size = new System.Drawing.Size(177, 23);
            this.lblTahsilatBaslik.TabIndex = 0;
            this.lblTahsilatBaslik.Text = "Bu Ay Tahsilat Oranı:";
            // 
            // panelBottomActions
            // 
            this.panelBottomActions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.panelBottomActions.Controls.Add(this.tableLayoutPanelActions);
            this.panelBottomActions.Controls.Add(this.lblTopluIslemler);
            this.panelBottomActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottomActions.Location = new System.Drawing.Point(2, 523);
            this.panelBottomActions.Name = "panelBottomActions";
            this.panelBottomActions.Padding = new System.Windows.Forms.Padding(25, 8, 25, 8);
            this.panelBottomActions.Size = new System.Drawing.Size(916, 70);
            this.panelBottomActions.TabIndex = 5;
            // 
            // tableLayoutPanelActions
            // 
            this.tableLayoutPanelActions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanelActions.ColumnCount = 4;
            this.tableLayoutPanelActions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelActions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelActions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelActions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelActions.Controls.Add(this.btnTumunuPasifYap, 0, 0);
            this.tableLayoutPanelActions.Controls.Add(this.btnTopluEmail, 1, 0);
            this.tableLayoutPanelActions.Controls.Add(this.btnRaporOlustur, 2, 0);
            this.tableLayoutPanelActions.Controls.Add(this.btnExcelAktar, 3, 0);
            this.tableLayoutPanelActions.Location = new System.Drawing.Point(25, 30);
            this.tableLayoutPanelActions.Name = "tableLayoutPanelActions";
            this.tableLayoutPanelActions.RowCount = 1;
            this.tableLayoutPanelActions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelActions.Size = new System.Drawing.Size(866, 38);
            this.tableLayoutPanelActions.TabIndex = 1;
            // 
            // btnTumunuPasifYap
            // 
            this.btnTumunuPasifYap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnTumunuPasifYap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTumunuPasifYap.FlatAppearance.BorderSize = 0;
            this.btnTumunuPasifYap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTumunuPasifYap.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnTumunuPasifYap.ForeColor = System.Drawing.Color.White;
            this.btnTumunuPasifYap.Location = new System.Drawing.Point(8, 5);
            this.btnTumunuPasifYap.Margin = new System.Windows.Forms.Padding(8, 5, 8, 5);
            this.btnTumunuPasifYap.Name = "btnTumunuPasifYap";
            this.btnTumunuPasifYap.Size = new System.Drawing.Size(200, 28);
            this.btnTumunuPasifYap.TabIndex = 0;
            this.btnTumunuPasifYap.Text = "Pasif Yap";
            this.btnTumunuPasifYap.UseVisualStyleBackColor = false;
            this.btnTumunuPasifYap.Click += new System.EventHandler(this.btnTumunuPasifYap_Click);
            // 
            // btnTopluEmail
            // 
            this.btnTopluEmail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.btnTopluEmail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTopluEmail.FlatAppearance.BorderSize = 0;
            this.btnTopluEmail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTopluEmail.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnTopluEmail.ForeColor = System.Drawing.Color.Black;
            this.btnTopluEmail.Location = new System.Drawing.Point(224, 5);
            this.btnTopluEmail.Margin = new System.Windows.Forms.Padding(8, 5, 8, 5);
            this.btnTopluEmail.Name = "btnTopluEmail";
            this.btnTopluEmail.Size = new System.Drawing.Size(200, 28);
            this.btnTopluEmail.TabIndex = 1;
            this.btnTopluEmail.Text = "E-posta Gönder";
            this.btnTopluEmail.UseVisualStyleBackColor = false;
            // 
            // btnRaporOlustur
            // 
            this.btnRaporOlustur.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(162)))), ((int)(((byte)(184)))));
            this.btnRaporOlustur.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRaporOlustur.FlatAppearance.BorderSize = 0;
            this.btnRaporOlustur.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRaporOlustur.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnRaporOlustur.ForeColor = System.Drawing.Color.White;
            this.btnRaporOlustur.Location = new System.Drawing.Point(440, 5);
            this.btnRaporOlustur.Margin = new System.Windows.Forms.Padding(8, 5, 8, 5);
            this.btnRaporOlustur.Name = "btnRaporOlustur";
            this.btnRaporOlustur.Size = new System.Drawing.Size(200, 28);
            this.btnRaporOlustur.TabIndex = 2;
            this.btnRaporOlustur.Text = "Rapor Oluştur";
            this.btnRaporOlustur.UseVisualStyleBackColor = false;
            this.btnRaporOlustur.Click += new System.EventHandler(this.btnRaporOlustur_Click);
            // 
            // btnExcelAktar
            // 
            this.btnExcelAktar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnExcelAktar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExcelAktar.FlatAppearance.BorderSize = 0;
            this.btnExcelAktar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExcelAktar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnExcelAktar.ForeColor = System.Drawing.Color.White;
            this.btnExcelAktar.Location = new System.Drawing.Point(656, 5);
            this.btnExcelAktar.Margin = new System.Windows.Forms.Padding(8, 5, 8, 5);
            this.btnExcelAktar.Name = "btnExcelAktar";
            this.btnExcelAktar.Size = new System.Drawing.Size(202, 28);
            this.btnExcelAktar.TabIndex = 3;
            this.btnExcelAktar.Text = "Excel\'e Aktar";
            this.btnExcelAktar.UseVisualStyleBackColor = false;
            this.btnExcelAktar.Click += new System.EventHandler(this.btnExcelAktar_Click);
            // 
            // lblTopluIslemler
            // 
            this.lblTopluIslemler.AutoSize = true;
            this.lblTopluIslemler.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTopluIslemler.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(80)))), ((int)(((byte)(87)))));
            this.lblTopluIslemler.Location = new System.Drawing.Point(25, 8);
            this.lblTopluIslemler.Name = "lblTopluIslemler";
            this.lblTopluIslemler.Size = new System.Drawing.Size(136, 25);
            this.lblTopluIslemler.TabIndex = 0;
            this.lblTopluIslemler.Text = "Toplu İşlemler";
            // 
            // panelFooter
            // 
            this.panelFooter.BackColor = System.Drawing.Color.MidnightBlue;
            this.panelFooter.Controls.Add(this.progressBar);
            this.panelFooter.Controls.Add(this.lblDurum);
            this.panelFooter.Controls.Add(this.lblToplamKayit);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(2, 593);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Padding = new System.Windows.Forms.Padding(25, 8, 25, 8);
            this.panelFooter.Size = new System.Drawing.Size(916, 35);
            this.panelFooter.TabIndex = 6;
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(510, 10);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(150, 15);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar.TabIndex = 2;
            this.progressBar.Visible = false;
            // 
            // lblDurum
            // 
            this.lblDurum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDurum.AutoSize = true;
            this.lblDurum.BackColor = System.Drawing.Color.Transparent;
            this.lblDurum.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDurum.ForeColor = System.Drawing.Color.White;
            this.lblDurum.Location = new System.Drawing.Point(670, 10);
            this.lblDurum.Name = "lblDurum";
            this.lblDurum.Size = new System.Drawing.Size(233, 20);
            this.lblDurum.TabIndex = 1;
            this.lblDurum.Text = "Son güncelleme: 14:50:31 - 4 kayıt";
            // 
            // lblToplamKayit
            // 
            this.lblToplamKayit.AutoSize = true;
            this.lblToplamKayit.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblToplamKayit.ForeColor = System.Drawing.Color.White;
            this.lblToplamKayit.Location = new System.Drawing.Point(25, 8);
            this.lblToplamKayit.Name = "lblToplamKayit";
            this.lblToplamKayit.Size = new System.Drawing.Size(222, 23);
            this.lblToplamKayit.TabIndex = 0;
            this.lblToplamKayit.Text = "Toplam 4 kayıt listeleniyor";
            // 
            // FormOdemeYapmayanlar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(27)))), ((int)(((byte)(62)))));
            this.ClientSize = new System.Drawing.Size(920, 630);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelProgress);
            this.Controls.Add(this.panelBottomActions);
            this.Controls.Add(this.panelFooter);
            this.Controls.Add(this.panelToolbar);
            this.Controls.Add(this.panelStats);
            this.Controls.Add(this.panelHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormOdemeYapmayanlar";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ödeme Yapmayanlar Yönetimi";
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelStats.ResumeLayout(false);
            this.tableLayoutPanelStats.ResumeLayout(false);
            this.panelStat1.ResumeLayout(false);
            this.panelStat2.ResumeLayout(false);
            this.panelStat3.ResumeLayout(false);
            this.panelStat4.ResumeLayout(false);
            this.panelToolbar.ResumeLayout(false);
            this.tableLayoutPanelToolbar.ResumeLayout(false);
            this.panelFiltre.ResumeLayout(false);
            this.panelFiltre.PerformLayout();
            this.panelSiralama.ResumeLayout(false);
            this.panelSiralama.PerformLayout();
            this.panelArama.ResumeLayout(false);
            this.panelArama.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBorclular)).EndInit();
            this.panelProgress.ResumeLayout(false);
            this.panelProgress.PerformLayout();
            this.panelBottomActions.ResumeLayout(false);
            this.panelBottomActions.PerformLayout();
            this.tableLayoutPanelActions.ResumeLayout(false);
            this.panelFooter.ResumeLayout(false);
            this.panelFooter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panelHeader;
        public System.Windows.Forms.Label lblBaslik;
        public System.Windows.Forms.Label lblAltBaslik;
        public System.Windows.Forms.Panel panelStats;
        public System.Windows.Forms.TableLayoutPanel tableLayoutPanelStats;
        public System.Windows.Forms.Panel panelStat1;
        public System.Windows.Forms.Label lblToplamBorclu;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Panel panelStat2;
        public System.Windows.Forms.Label lblToplamBorc;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.Panel panelStat3;
        public System.Windows.Forms.Label lblVadesiGecen;
        public System.Windows.Forms.Label label5;
        public System.Windows.Forms.Panel panelStat4;
        public System.Windows.Forms.Label lblKritikDurum;
        public System.Windows.Forms.Label label7;
        public System.Windows.Forms.Panel panelToolbar;
        public System.Windows.Forms.TableLayoutPanel tableLayoutPanelToolbar;
        public System.Windows.Forms.Panel panelFiltre;
        public System.Windows.Forms.ComboBox cmbFiltre;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Panel panelSiralama;
        public System.Windows.Forms.ComboBox cmbSiralama;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.Panel panelArama;
        public System.Windows.Forms.Button btnAra;
        public System.Windows.Forms.TextBox txtArama;
        public System.Windows.Forms.Panel panelButtons;
        public System.Windows.Forms.Button btnYenile;
        public System.Windows.Forms.Panel panelMain;
        public System.Windows.Forms.DataGridView dgvBorclular;
        public System.Windows.Forms.Panel panelProgress;
        public System.Windows.Forms.Label lblTahsilatYuzde;
        public System.Windows.Forms.ProgressBar progressBarTahsilat;
        public System.Windows.Forms.Label lblTahsilatBaslik;
        public System.Windows.Forms.Panel panelBottomActions;
        public System.Windows.Forms.TableLayoutPanel tableLayoutPanelActions;
        public System.Windows.Forms.Button btnTumunuPasifYap;
        public System.Windows.Forms.Button btnTopluEmail;
        public System.Windows.Forms.Button btnRaporOlustur;
        public System.Windows.Forms.Button btnExcelAktar;
        public System.Windows.Forms.Label lblTopluIslemler;
        public System.Windows.Forms.Panel panelFooter;
        public System.Windows.Forms.ProgressBar progressBar;
        public System.Windows.Forms.Label lblDurum;
        public System.Windows.Forms.Label lblToplamKayit;
        public Guna.UI2.WinForms.Guna2Button guna2Button1;
    }
}