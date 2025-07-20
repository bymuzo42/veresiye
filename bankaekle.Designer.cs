namespace Veresiye2025
{
    partial class bankaekle
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.formBorder = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.panelTitleBar = new Guna.UI2.WinForms.Guna2Panel();
            this.btnClose = new Guna.UI2.WinForms.Guna2Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelMain = new Guna.UI2.WinForms.Guna2Panel();
            this.panelLeft = new Guna.UI2.WinForms.Guna2Panel();
            this.lblBankaAdi = new System.Windows.Forms.Label();
            this.txtBankaAdi = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblCihazAdi = new System.Windows.Forms.Label();
            this.txtCihazAdi = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblValor = new System.Windows.Forms.Label();
            this.txtValor = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblKesintiOrani = new System.Windows.Forms.Label();
            this.txtKesintiOrani = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblErkenBozumOrani = new System.Windows.Forms.Label();
            this.txtErkenBozumOrani = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnKaydet = new Guna.UI2.WinForms.Guna2Button();
            this.btnTemizle = new Guna.UI2.WinForms.Guna2Button();
            this.panelRight = new Guna.UI2.WinForms.Guna2Panel();
            this.rtbNotlar = new System.Windows.Forms.RichTextBox();
            this.panelBottom = new Guna.UI2.WinForms.Guna2Panel();
            this.dgvBankalar = new Guna.UI2.WinForms.Guna2DataGridView();
            this.btnSil = new Guna.UI2.WinForms.Guna2Button();
            this.btnDuzenle = new Guna.UI2.WinForms.Guna2Button();
            this.panelTitleBar.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.panelRight.SuspendLayout();
            this.panelBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBankalar)).BeginInit();
            this.SuspendLayout();
            //
            // formBorder
            //
            this.formBorder.BorderRadius = 20;
            this.formBorder.TargetControl = this;
            //
            // panelTitleBar
            //
            this.panelTitleBar.BorderRadius = 5;
            this.panelTitleBar.Controls.Add(this.btnClose);
            this.panelTitleBar.Controls.Add(this.lblTitle);
            this.panelTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitleBar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.panelTitleBar.Location = new System.Drawing.Point(10, 10);
            this.panelTitleBar.Name = "panelTitleBar";
            this.panelTitleBar.Size = new System.Drawing.Size(1080, 40);
            this.panelTitleBar.TabIndex = 1;
            //
            // btnClose
            //
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Animated = true;
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.BorderRadius = 10;
            this.btnClose.FillColor = System.Drawing.Color.Transparent;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(1040, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(40, 40);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "X";
            this.btnClose.UseTransparentBackground = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.btnClose.MouseEnter += new System.EventHandler(this.btnClose_MouseEnter);
            this.btnClose.MouseLeave += new System.EventHandler(this.btnClose_MouseLeave);
            //
            // lblTitle
            //
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(12, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(146, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Banka İşlemleri";
            //
            // panelMain
            //
            this.panelMain.Controls.Add(this.panelLeft);
            this.panelMain.Controls.Add(this.panelRight);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMain.Location = new System.Drawing.Point(10, 50);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1080, 304);
            this.panelMain.TabIndex = 2;
            //
            // panelLeft
            //
            this.panelLeft.BorderRadius = 20;
            this.panelLeft.Controls.Add(this.lblBankaAdi);
            this.panelLeft.Controls.Add(this.txtBankaAdi);
            this.panelLeft.Controls.Add(this.lblCihazAdi);
            this.panelLeft.Controls.Add(this.txtCihazAdi);
            this.panelLeft.Controls.Add(this.lblValor);
            this.panelLeft.Controls.Add(this.txtValor);
            this.panelLeft.Controls.Add(this.lblKesintiOrani);
            this.panelLeft.Controls.Add(this.txtKesintiOrani);
            this.panelLeft.Controls.Add(this.lblErkenBozumOrani);
            this.panelLeft.Controls.Add(this.txtErkenBozumOrani);
            this.panelLeft.Controls.Add(this.btnKaydet);
            this.panelLeft.Controls.Add(this.btnTemizle);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(540, 304);
            this.panelLeft.TabIndex = 3;
            //
            // lblBankaAdi
            //
            this.lblBankaAdi.AutoSize = true;
            this.lblBankaAdi.BackColor = System.Drawing.Color.Transparent;
            this.lblBankaAdi.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblBankaAdi.Location = new System.Drawing.Point(20, 20);
            this.lblBankaAdi.Name = "lblBankaAdi";
            this.lblBankaAdi.Size = new System.Drawing.Size(91, 23);
            this.lblBankaAdi.TabIndex = 0;
            this.lblBankaAdi.Text = "Banka Adı:";
            //
            // txtBankaAdi
            //
            this.txtBankaAdi.BorderRadius = 10;
            this.txtBankaAdi.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtBankaAdi.DefaultText = "";
            this.txtBankaAdi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtBankaAdi.Location = new System.Drawing.Point(223, 15);
            this.txtBankaAdi.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtBankaAdi.Name = "txtBankaAdi";
            this.txtBankaAdi.PlaceholderText = "Banka adını yazın...";
            this.txtBankaAdi.SelectedText = "";
            this.txtBankaAdi.Size = new System.Drawing.Size(260, 36);
            this.txtBankaAdi.TabIndex = 1;
            //
            // lblCihazAdi
            //
            this.lblCihazAdi.AutoSize = true;
            this.lblCihazAdi.BackColor = System.Drawing.Color.Transparent;
            this.lblCihazAdi.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblCihazAdi.Location = new System.Drawing.Point(20, 65);
            this.lblCihazAdi.Name = "lblCihazAdi";
            this.lblCihazAdi.Size = new System.Drawing.Size(86, 23);
            this.lblCihazAdi.TabIndex = 2;
            this.lblCihazAdi.Text = "Cihaz Adı:";
            //
            // txtCihazAdi
            //
            this.txtCihazAdi.BorderRadius = 10;
            this.txtCihazAdi.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtCihazAdi.DefaultText = "";
            this.txtCihazAdi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtCihazAdi.Location = new System.Drawing.Point(223, 60);
            this.txtCihazAdi.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtCihazAdi.Name = "txtCihazAdi";
            this.txtCihazAdi.PlaceholderText = "Cihaz adını yazın...";
            this.txtCihazAdi.SelectedText = "";
            this.txtCihazAdi.Size = new System.Drawing.Size(260, 36);
            this.txtCihazAdi.TabIndex = 3;
            //
            // lblValor
            //
            this.lblValor.AutoSize = true;
            this.lblValor.BackColor = System.Drawing.Color.Transparent;
            this.lblValor.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblValor.Location = new System.Drawing.Point(20, 110);
            this.lblValor.Name = "lblValor";
            this.lblValor.Size = new System.Drawing.Size(102, 23);
            this.lblValor.TabIndex = 4;
            this.lblValor.Text = "Valör (Gün):";
            //
            // txtValor
            //
            this.txtValor.BorderRadius = 10;
            this.txtValor.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtValor.DefaultText = "";
            this.txtValor.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtValor.Location = new System.Drawing.Point(223, 105);
            this.txtValor.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtValor.Name = "txtValor";
            this.txtValor.PlaceholderText = "Valör gün sayısı...";
            this.txtValor.SelectedText = "";
            this.txtValor.Size = new System.Drawing.Size(260, 36);
            this.txtValor.TabIndex = 5;
            //
            // lblKesintiOrani
            //
            this.lblKesintiOrani.AutoSize = true;
            this.lblKesintiOrani.BackColor = System.Drawing.Color.Transparent;
            this.lblKesintiOrani.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblKesintiOrani.Location = new System.Drawing.Point(20, 155);
            this.lblKesintiOrani.Name = "lblKesintiOrani";
            this.lblKesintiOrani.Size = new System.Drawing.Size(142, 23);
            this.lblKesintiOrani.TabIndex = 6;
            this.lblKesintiOrani.Text = "Kesinti Oranı (%):";
            //
            // txtKesintiOrani
            //
            this.txtKesintiOrani.BorderRadius = 10;
            this.txtKesintiOrani.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtKesintiOrani.DefaultText = "";
            this.txtKesintiOrani.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtKesintiOrani.Location = new System.Drawing.Point(223, 150);
            this.txtKesintiOrani.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtKesintiOrani.Name = "txtKesintiOrani";
            this.txtKesintiOrani.PlaceholderText = "Örn: 1.5";
            this.txtKesintiOrani.SelectedText = "";
            this.txtKesintiOrani.Size = new System.Drawing.Size(260, 36);
            this.txtKesintiOrani.TabIndex = 7;
            //
            // lblErkenBozumOrani
            //
            this.lblErkenBozumOrani.AutoSize = true;
            this.lblErkenBozumOrani.BackColor = System.Drawing.Color.Transparent;
            this.lblErkenBozumOrani.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblErkenBozumOrani.Location = new System.Drawing.Point(20, 200);
            this.lblErkenBozumOrani.Name = "lblErkenBozumOrani";
            this.lblErkenBozumOrani.Size = new System.Drawing.Size(193, 23);
            this.lblErkenBozumOrani.TabIndex = 8;
            this.lblErkenBozumOrani.Text = "Erken Bozum Oranı (%):";
            //
            // txtErkenBozumOrani
            //
            this.txtErkenBozumOrani.BorderRadius = 10;
            this.txtErkenBozumOrani.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtErkenBozumOrani.DefaultText = "";
            this.txtErkenBozumOrani.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtErkenBozumOrani.Location = new System.Drawing.Point(223, 195);
            this.txtErkenBozumOrani.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtErkenBozumOrani.Name = "txtErkenBozumOrani";
            this.txtErkenBozumOrani.PlaceholderText = "Örn: 2.5";
            this.txtErkenBozumOrani.SelectedText = "";
            this.txtErkenBozumOrani.Size = new System.Drawing.Size(260, 36);
            this.txtErkenBozumOrani.TabIndex = 9;
            //
            // btnKaydet
            //
            this.btnKaydet.BorderRadius = 10;
            this.btnKaydet.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnKaydet.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnKaydet.ForeColor = System.Drawing.Color.White;
            this.btnKaydet.Location = new System.Drawing.Point(223, 245);
            this.btnKaydet.Name = "btnKaydet";
            this.btnKaydet.Size = new System.Drawing.Size(117, 45);
            this.btnKaydet.TabIndex = 10;
            this.btnKaydet.Text = "Kaydet";
            this.btnKaydet.Click += new System.EventHandler(this.bankakaydet_Click);
            //
            // btnTemizle
            //
            this.btnTemizle.BorderRadius = 10;
            this.btnTemizle.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnTemizle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnTemizle.ForeColor = System.Drawing.Color.White;
            this.btnTemizle.Location = new System.Drawing.Point(366, 245);
            this.btnTemizle.Name = "btnTemizle";
            this.btnTemizle.Size = new System.Drawing.Size(117, 45);
            this.btnTemizle.TabIndex = 11;
            this.btnTemizle.Text = "Temizle";
            this.btnTemizle.Click += new System.EventHandler(this.temizle_Click);
            //
            // panelRight
            //
            this.panelRight.BorderRadius = 10;
            this.panelRight.Controls.Add(this.rtbNotlar);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelRight.Location = new System.Drawing.Point(540, 0);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(540, 304);
            this.panelRight.TabIndex = 4;
            //
            // rtbNotlar
            //
            this.rtbNotlar.BackColor = System.Drawing.Color.White;
            this.rtbNotlar.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbNotlar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.rtbNotlar.Location = new System.Drawing.Point(15, 15);
            this.rtbNotlar.Name = "rtbNotlar";
            this.rtbNotlar.ReadOnly = true;
            this.rtbNotlar.Size = new System.Drawing.Size(510, 275);
            this.rtbNotlar.TabIndex = 1;
            this.rtbNotlar.Text = "";
            //
            // panelBottom
            //
            this.panelBottom.Controls.Add(this.dgvBankalar);
            this.panelBottom.Controls.Add(this.btnSil);
            this.panelBottom.Controls.Add(this.btnDuzenle);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBottom.Location = new System.Drawing.Point(10, 354);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(1080, 326);
            this.panelBottom.TabIndex = 5;
            //
            // dgvBankalar
            //
            this.dgvBankalar.AllowUserToAddRows = false;
            this.dgvBankalar.AllowUserToDeleteRows = false;
            this.dgvBankalar.AllowUserToResizeColumns = false;
            this.dgvBankalar.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvBankalar.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvBankalar.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.dgvBankalar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvBankalar.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvBankalar.ColumnHeadersHeight = 30;
            this.dgvBankalar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvBankalar.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvBankalar.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvBankalar.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvBankalar.Location = new System.Drawing.Point(0, 0);
            this.dgvBankalar.MultiSelect = false;
            this.dgvBankalar.Name = "dgvBankalar";
            this.dgvBankalar.ReadOnly = true;
            this.dgvBankalar.RowHeadersVisible = false;
            this.dgvBankalar.RowHeadersWidth = 51;
            this.dgvBankalar.RowTemplate.Height = 24;
            this.dgvBankalar.Size = new System.Drawing.Size(1080, 260);
            this.dgvBankalar.TabIndex = 0;
            this.dgvBankalar.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvBankalar.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvBankalar.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvBankalar.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvBankalar.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvBankalar.ThemeStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.dgvBankalar.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvBankalar.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.dgvBankalar.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvBankalar.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.dgvBankalar.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvBankalar.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvBankalar.ThemeStyle.HeaderStyle.Height = 30;
            this.dgvBankalar.ThemeStyle.ReadOnly = true;
            this.dgvBankalar.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvBankalar.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvBankalar.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dgvBankalar.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvBankalar.ThemeStyle.RowsStyle.Height = 24;
            this.dgvBankalar.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvBankalar.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            //
            // btnSil
            //
            this.btnSil.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSil.BorderRadius = 10;
            this.btnSil.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnSil.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSil.ForeColor = System.Drawing.Color.White;
            this.btnSil.Location = new System.Drawing.Point(20, 270);
            this.btnSil.Name = "btnSil";
            this.btnSil.Size = new System.Drawing.Size(180, 45);
            this.btnSil.TabIndex = 1;
            this.btnSil.Text = "Seçili Kaydı Sil";
            this.btnSil.Click += new System.EventHandler(this.bankasil_Click);
            //
            // btnDuzenle
            //
            this.btnDuzenle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDuzenle.BorderRadius = 10;
            this.btnDuzenle.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnDuzenle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnDuzenle.ForeColor = System.Drawing.Color.White;
            this.btnDuzenle.Location = new System.Drawing.Point(880, 270);
            this.btnDuzenle.Name = "btnDuzenle";
            this.btnDuzenle.Size = new System.Drawing.Size(180, 45);
            this.btnDuzenle.TabIndex = 2;
            this.btnDuzenle.Text = "Seçili Kaydı Düzenle";
            this.btnDuzenle.Click += new System.EventHandler(this.bankaduzenle_Click);
            //
            // bankaekle
            //
            this.AcceptButton = this.btnKaydet;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1100, 690);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelTitleBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "bankaekle";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Banka Ekle";
            this.Load += new System.EventHandler(this.bankaekle_Load);
            this.panelTitleBar.ResumeLayout(false);
            this.panelTitleBar.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.panelLeft.ResumeLayout(false);
            this.panelLeft.PerformLayout();
            this.panelRight.ResumeLayout(false);
            this.panelBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBankalar)).EndInit();
            this.ResumeLayout(false);
        }
        #endregion
        public Guna.UI2.WinForms.Guna2Elipse formBorder;
        public Guna.UI2.WinForms.Guna2Panel panelTitleBar;
        public Guna.UI2.WinForms.Guna2Button btnClose;
        public System.Windows.Forms.Label lblTitle;
        public Guna.UI2.WinForms.Guna2Panel panelMain;
        public Guna.UI2.WinForms.Guna2Panel panelLeft;
        public System.Windows.Forms.Label lblBankaAdi;
        public Guna.UI2.WinForms.Guna2TextBox txtBankaAdi;
        public System.Windows.Forms.Label lblCihazAdi;
        public Guna.UI2.WinForms.Guna2TextBox txtCihazAdi;
        public System.Windows.Forms.Label lblValor;
        public Guna.UI2.WinForms.Guna2TextBox txtValor;
        public System.Windows.Forms.Label lblKesintiOrani;
        public Guna.UI2.WinForms.Guna2TextBox txtKesintiOrani;
        public System.Windows.Forms.Label lblErkenBozumOrani;
        public Guna.UI2.WinForms.Guna2TextBox txtErkenBozumOrani;
        public Guna.UI2.WinForms.Guna2Button btnKaydet;
        public Guna.UI2.WinForms.Guna2Button btnTemizle;
        public Guna.UI2.WinForms.Guna2Panel panelRight;
        public System.Windows.Forms.RichTextBox rtbNotlar;
        public Guna.UI2.WinForms.Guna2Panel panelBottom;
        public Guna.UI2.WinForms.Guna2DataGridView dgvBankalar;
        public Guna.UI2.WinForms.Guna2Button btnSil;
        public Guna.UI2.WinForms.Guna2Button btnDuzenle;
    }
}