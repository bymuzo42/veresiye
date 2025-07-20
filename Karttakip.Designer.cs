namespace Veresiye2025
{
    partial class Karttakip
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        public System.ComponentModel.IContainer components = null;
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        public void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Karttakip));
            this.panelTitleBar = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnClose = new Guna.UI2.WinForms.Guna2Button();
            this.panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.lblBankaAdi = new System.Windows.Forms.Label();
            this.lblKartNo = new System.Windows.Forms.Label();
            this.lblSonKullanma = new System.Windows.Forms.Label();
            this.lblCVC = new System.Windows.Forms.Label();
            this.lblHesapKesim = new System.Windows.Forms.Label();
            this.lblLimit = new System.Windows.Forms.Label();
            this.lblAlarmGun = new System.Windows.Forms.Label();
            this.txtBankaAdi = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtKartNo = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtCVC = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtLimit = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtAlarmGun = new Guna.UI2.WinForms.Guna2TextBox();
            this.datePickerSonKullanma = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.datePickerHesapKesim = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnKaydet = new Guna.UI2.WinForms.Guna2Button();
            this.btnAlarm = new Guna.UI2.WinForms.Guna2Button();
            this.btnTumunuSil = new Guna.UI2.WinForms.Guna2Button();
            this.dataGridView1 = new Guna.UI2.WinForms.Guna2DataGridView();
            this.tooltip = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.guna2DragControl1 = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.guna2ShadowForm1 = new Guna.UI2.WinForms.Guna2ShadowForm(this.components);
            this.guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.panelTitleBar.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTitleBar
            // 
            this.panelTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.panelTitleBar.Controls.Add(this.lblTitle);
            this.panelTitleBar.Controls.Add(this.btnClose);
            this.panelTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitleBar.Location = new System.Drawing.Point(0, 0);
            this.panelTitleBar.Margin = new System.Windows.Forms.Padding(4);
            this.panelTitleBar.Name = "panelTitleBar";
            this.panelTitleBar.Size = new System.Drawing.Size(1200, 49);
            this.panelTitleBar.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(16, 11);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(171, 28);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Kredi Kartı Takip";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FillColor = System.Drawing.Color.Transparent;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(1147, 0);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(53, 49);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "X";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderRadius = 10;
            this.panel1.Controls.Add(this.lblBankaAdi);
            this.panel1.Controls.Add(this.lblKartNo);
            this.panel1.Controls.Add(this.lblSonKullanma);
            this.panel1.Controls.Add(this.lblCVC);
            this.panel1.Controls.Add(this.lblHesapKesim);
            this.panel1.Controls.Add(this.lblLimit);
            this.panel1.Controls.Add(this.lblAlarmGun);
            this.panel1.Controls.Add(this.txtBankaAdi);
            this.panel1.Controls.Add(this.txtKartNo);
            this.panel1.Controls.Add(this.txtCVC);
            this.panel1.Controls.Add(this.txtLimit);
            this.panel1.Controls.Add(this.txtAlarmGun);
            this.panel1.Controls.Add(this.datePickerSonKullanma);
            this.panel1.Controls.Add(this.datePickerHesapKesim);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.btnKaydet);
            this.panel1.Controls.Add(this.btnAlarm);
            this.panel1.Controls.Add(this.btnTumunuSil);
            this.panel1.Location = new System.Drawing.Point(16, 62);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1168, 369);
            this.panel1.TabIndex = 1;
            // 
            // lblBankaAdi
            // 
            this.lblBankaAdi.AutoSize = true;
            this.lblBankaAdi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblBankaAdi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblBankaAdi.Location = new System.Drawing.Point(27, 25);
            this.lblBankaAdi.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBankaAdi.Name = "lblBankaAdi";
            this.lblBankaAdi.Size = new System.Drawing.Size(84, 20);
            this.lblBankaAdi.TabIndex = 0;
            this.lblBankaAdi.Text = "Banka Adı:";
            // 
            // lblKartNo
            // 
            this.lblKartNo.AutoSize = true;
            this.lblKartNo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblKartNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblKartNo.Location = new System.Drawing.Point(27, 74);
            this.lblKartNo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblKartNo.Name = "lblKartNo";
            this.lblKartNo.Size = new System.Drawing.Size(115, 20);
            this.lblKartNo.TabIndex = 1;
            this.lblKartNo.Text = "Kart Numarası:";
            // 
            // lblSonKullanma
            // 
            this.lblSonKullanma.AutoSize = true;
            this.lblSonKullanma.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSonKullanma.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblSonKullanma.Location = new System.Drawing.Point(27, 123);
            this.lblSonKullanma.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSonKullanma.Name = "lblSonKullanma";
            this.lblSonKullanma.Size = new System.Drawing.Size(152, 20);
            this.lblSonKullanma.TabIndex = 2;
            this.lblSonKullanma.Text = "Son Kullanma Tarihi:";
            // 
            // lblCVC
            // 
            this.lblCVC.AutoSize = true;
            this.lblCVC.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCVC.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblCVC.Location = new System.Drawing.Point(27, 172);
            this.lblCVC.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCVC.Name = "lblCVC";
            this.lblCVC.Size = new System.Drawing.Size(41, 20);
            this.lblCVC.TabIndex = 3;
            this.lblCVC.Text = "CVC:";
            // 
            // lblHesapKesim
            // 
            this.lblHesapKesim.AutoSize = true;
            this.lblHesapKesim.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblHesapKesim.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblHesapKesim.Location = new System.Drawing.Point(27, 222);
            this.lblHesapKesim.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHesapKesim.Name = "lblHesapKesim";
            this.lblHesapKesim.Size = new System.Drawing.Size(146, 20);
            this.lblHesapKesim.TabIndex = 4;
            this.lblHesapKesim.Text = "Hesap Kesim Tarihi:";
            // 
            // lblLimit
            // 
            this.lblLimit.AutoSize = true;
            this.lblLimit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblLimit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblLimit.Location = new System.Drawing.Point(27, 271);
            this.lblLimit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLimit.Name = "lblLimit";
            this.lblLimit.Size = new System.Drawing.Size(49, 20);
            this.lblLimit.TabIndex = 5;
            this.lblLimit.Text = "Limit:";
            // 
            // lblAlarmGun
            // 
            this.lblAlarmGun.AutoSize = true;
            this.lblAlarmGun.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblAlarmGun.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblAlarmGun.Location = new System.Drawing.Point(27, 320);
            this.lblAlarmGun.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAlarmGun.Name = "lblAlarmGun";
            this.lblAlarmGun.Size = new System.Drawing.Size(162, 20);
            this.lblAlarmGun.TabIndex = 6;
            this.lblAlarmGun.Text = "Kaç Gün Uyarı Versin?";
            // 
            // txtBankaAdi
            // 
            this.txtBankaAdi.Animated = true;
            this.txtBankaAdi.BorderRadius = 5;
            this.txtBankaAdi.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtBankaAdi.DefaultText = "";
            this.txtBankaAdi.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtBankaAdi.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtBankaAdi.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtBankaAdi.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtBankaAdi.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.txtBankaAdi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtBankaAdi.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.txtBankaAdi.Location = new System.Drawing.Point(240, 18);
            this.txtBankaAdi.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtBankaAdi.Name = "txtBankaAdi";
            this.txtBankaAdi.PlaceholderText = "Banka adını girin";
            this.txtBankaAdi.SelectedText = "";
            this.txtBankaAdi.Size = new System.Drawing.Size(333, 44);
            this.txtBankaAdi.TabIndex = 7;
            // 
            // txtKartNo
            // 
            this.txtKartNo.Animated = true;
            this.txtKartNo.BorderRadius = 5;
            this.txtKartNo.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtKartNo.DefaultText = "";
            this.txtKartNo.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtKartNo.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtKartNo.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtKartNo.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtKartNo.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.txtKartNo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtKartNo.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.txtKartNo.Location = new System.Drawing.Point(240, 68);
            this.txtKartNo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtKartNo.Name = "txtKartNo";
            this.txtKartNo.PlaceholderText = "16 haneli kart numarası";
            this.txtKartNo.SelectedText = "";
            this.txtKartNo.Size = new System.Drawing.Size(333, 44);
            this.txtKartNo.TabIndex = 8;
            // 
            // txtCVC
            // 
            this.txtCVC.Animated = true;
            this.txtCVC.BorderRadius = 5;
            this.txtCVC.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtCVC.DefaultText = "";
            this.txtCVC.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtCVC.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtCVC.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtCVC.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtCVC.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.txtCVC.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtCVC.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.txtCVC.Location = new System.Drawing.Point(240, 166);
            this.txtCVC.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtCVC.MaxLength = 3;
            this.txtCVC.Name = "txtCVC";
            this.txtCVC.PlaceholderText = "3 haneli güvenlik kodu";
            this.txtCVC.SelectedText = "";
            this.txtCVC.Size = new System.Drawing.Size(333, 44);
            this.txtCVC.TabIndex = 10;
            // 
            // txtLimit
            // 
            this.txtLimit.Animated = true;
            this.txtLimit.BorderRadius = 5;
            this.txtLimit.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtLimit.DefaultText = "";
            this.txtLimit.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtLimit.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtLimit.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtLimit.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtLimit.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.txtLimit.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtLimit.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.txtLimit.Location = new System.Drawing.Point(240, 265);
            this.txtLimit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtLimit.Name = "txtLimit";
            this.txtLimit.PlaceholderText = "Kart limitini girin";
            this.txtLimit.SelectedText = "";
            this.txtLimit.Size = new System.Drawing.Size(333, 44);
            this.txtLimit.TabIndex = 12;
            // 
            // txtAlarmGun
            // 
            this.txtAlarmGun.Animated = true;
            this.txtAlarmGun.BorderRadius = 5;
            this.txtAlarmGun.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtAlarmGun.DefaultText = "";
            this.txtAlarmGun.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtAlarmGun.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtAlarmGun.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtAlarmGun.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtAlarmGun.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.txtAlarmGun.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtAlarmGun.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.txtAlarmGun.Location = new System.Drawing.Point(240, 314);
            this.txtAlarmGun.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtAlarmGun.MaxLength = 2;
            this.txtAlarmGun.Name = "txtAlarmGun";
            this.txtAlarmGun.PlaceholderText = "1-30 arası bir değer girin";
            this.txtAlarmGun.SelectedText = "";
            this.txtAlarmGun.Size = new System.Drawing.Size(333, 44);
            this.txtAlarmGun.TabIndex = 13;
            // 
            // datePickerSonKullanma
            // 
            this.datePickerSonKullanma.Animated = true;
            this.datePickerSonKullanma.BorderRadius = 5;
            this.datePickerSonKullanma.Checked = true;
            this.datePickerSonKullanma.FillColor = System.Drawing.Color.White;
            this.datePickerSonKullanma.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.datePickerSonKullanma.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datePickerSonKullanma.Location = new System.Drawing.Point(240, 117);
            this.datePickerSonKullanma.Margin = new System.Windows.Forms.Padding(4);
            this.datePickerSonKullanma.MaxDate = new System.DateTime(2123, 12, 31, 0, 0, 0, 0);
            this.datePickerSonKullanma.MinDate = new System.DateTime(2023, 1, 1, 0, 0, 0, 0);
            this.datePickerSonKullanma.Name = "datePickerSonKullanma";
            this.datePickerSonKullanma.Size = new System.Drawing.Size(333, 44);
            this.datePickerSonKullanma.TabIndex = 9;
            this.datePickerSonKullanma.Value = new System.DateTime(2025, 5, 19, 0, 0, 0, 0);
            // 
            // datePickerHesapKesim
            // 
            this.datePickerHesapKesim.Animated = true;
            this.datePickerHesapKesim.BorderRadius = 5;
            this.datePickerHesapKesim.Checked = true;
            this.datePickerHesapKesim.FillColor = System.Drawing.Color.White;
            this.datePickerHesapKesim.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.datePickerHesapKesim.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datePickerHesapKesim.Location = new System.Drawing.Point(240, 215);
            this.datePickerHesapKesim.Margin = new System.Windows.Forms.Padding(4);
            this.datePickerHesapKesim.MaxDate = new System.DateTime(2123, 12, 31, 0, 0, 0, 0);
            this.datePickerHesapKesim.MinDate = new System.DateTime(2023, 1, 1, 0, 0, 0, 0);
            this.datePickerHesapKesim.Name = "datePickerHesapKesim";
            this.datePickerHesapKesim.Size = new System.Drawing.Size(333, 44);
            this.datePickerHesapKesim.TabIndex = 11;
            this.datePickerHesapKesim.Value = new System.DateTime(2025, 5, 19, 0, 0, 0, 0);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Veresiye2025.Properties.Resources.poscihaz1;
            this.pictureBox1.Location = new System.Drawing.Point(685, 4);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(400, 301);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // btnKaydet
            // 
            this.btnKaydet.Animated = true;
            this.btnKaydet.BorderRadius = 10;
            this.btnKaydet.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnKaydet.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnKaydet.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnKaydet.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnKaydet.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnKaydet.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnKaydet.ForeColor = System.Drawing.Color.White;
            this.btnKaydet.Location = new System.Drawing.Point(966, 313);
            this.btnKaydet.Margin = new System.Windows.Forms.Padding(4);
            this.btnKaydet.Name = "btnKaydet";
            this.btnKaydet.Size = new System.Drawing.Size(175, 45);
            this.btnKaydet.TabIndex = 16;
            this.btnKaydet.Text = "Kaydet";
            // 
            // btnAlarm
            // 
            this.btnAlarm.Animated = true;
            this.btnAlarm.BorderRadius = 10;
            this.btnAlarm.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnAlarm.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnAlarm.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnAlarm.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnAlarm.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnAlarm.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAlarm.ForeColor = System.Drawing.Color.White;
            this.btnAlarm.Location = new System.Drawing.Point(785, 313);
            this.btnAlarm.Margin = new System.Windows.Forms.Padding(4);
            this.btnAlarm.Name = "btnAlarm";
            this.btnAlarm.Size = new System.Drawing.Size(175, 45);
            this.btnAlarm.TabIndex = 15;
            this.btnAlarm.Text = "Alarm Kur";
            this.btnAlarm.Click += new System.EventHandler(this.btnAlarm_Click_1);
            // 
            // btnTumunuSil
            // 
            this.btnTumunuSil.Animated = true;
            this.btnTumunuSil.BorderRadius = 10;
            this.btnTumunuSil.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnTumunuSil.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnTumunuSil.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnTumunuSil.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnTumunuSil.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnTumunuSil.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnTumunuSil.ForeColor = System.Drawing.Color.White;
            this.btnTumunuSil.Location = new System.Drawing.Point(600, 313);
            this.btnTumunuSil.Margin = new System.Windows.Forms.Padding(4);
            this.btnTumunuSil.Name = "btnTumunuSil";
            this.btnTumunuSil.Size = new System.Drawing.Size(175, 45);
            this.btnTumunuSil.TabIndex = 17;
            this.btnTumunuSil.Text = "Tümünü Sil";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(248)))), ((int)(((byte)(249)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeight = 40;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(237)))), ((int)(((byte)(200)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dataGridView1.Location = new System.Drawing.Point(16, 443);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 35;
            this.dataGridView1.Size = new System.Drawing.Size(1168, 231);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(248)))), ((int)(((byte)(249)))));
            this.dataGridView1.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dataGridView1.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dataGridView1.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dataGridView1.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dataGridView1.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dataGridView1.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dataGridView1.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.dataGridView1.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridView1.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.dataGridView1.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dataGridView1.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.ThemeStyle.HeaderStyle.Height = 40;
            this.dataGridView1.ThemeStyle.ReadOnly = true;
            this.dataGridView1.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dataGridView1.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dataGridView1.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dataGridView1.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dataGridView1.ThemeStyle.RowsStyle.Height = 35;
            this.dataGridView1.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(237)))), ((int)(((byte)(200)))));
            this.dataGridView1.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.Black;
            // 
            // contextMenu
            // 
            this.contextMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.BorderRadius = 20;
            this.guna2Elipse1.TargetControl = this;
            // 
            // guna2DragControl1
            // 
            this.guna2DragControl1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2DragControl1.TargetControl = this.panelTitleBar;
            this.guna2DragControl1.UseTransparentDrag = true;
            // 
            // guna2ShadowForm1
            // 
            this.guna2ShadowForm1.ShadowColor = System.Drawing.Color.DarkGray;
            this.guna2ShadowForm1.TargetForm = this;
            // 
            // guna2BorderlessForm1
            // 
            this.guna2BorderlessForm1.BorderRadius = 20;
            this.guna2BorderlessForm1.ContainerControl = this;
            this.guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // Karttakip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(1200, 689);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelTitleBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Karttakip";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kredi Kartı Takip";
            this.panelTitleBar.ResumeLayout(false);
            this.panelTitleBar.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        // Modern tasarım bileşenleri
        public Guna.UI2.WinForms.Guna2Panel panelTitleBar;
        public System.Windows.Forms.Label lblTitle;
        public Guna.UI2.WinForms.Guna2Button btnClose;
        public Guna.UI2.WinForms.Guna2Panel panel1;
        public Guna.UI2.WinForms.Guna2DataGridView dataGridView1;

        // Form köşelerini yuvarlatmak için yeni bileşenler
        public Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        public Guna.UI2.WinForms.Guna2DragControl guna2DragControl1;
        public Guna.UI2.WinForms.Guna2ShadowForm guna2ShadowForm1;
        public Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;

        // Form bileşenleri
        public System.Windows.Forms.Label lblBankaAdi;
        public System.Windows.Forms.Label lblKartNo;
        public System.Windows.Forms.Label lblSonKullanma;
        public System.Windows.Forms.Label lblCVC;
        public System.Windows.Forms.Label lblHesapKesim;
        public System.Windows.Forms.Label lblLimit;
        public System.Windows.Forms.Label lblAlarmGun;
        public Guna.UI2.WinForms.Guna2TextBox txtBankaAdi;
        public Guna.UI2.WinForms.Guna2TextBox txtKartNo;
        public Guna.UI2.WinForms.Guna2TextBox txtCVC;
        public Guna.UI2.WinForms.Guna2TextBox txtLimit;
        public Guna.UI2.WinForms.Guna2TextBox txtAlarmGun;
        public Guna.UI2.WinForms.Guna2DateTimePicker datePickerSonKullanma;
        public Guna.UI2.WinForms.Guna2DateTimePicker datePickerHesapKesim;
        public Guna.UI2.WinForms.Guna2Button btnKaydet;
        public Guna.UI2.WinForms.Guna2Button btnAlarm;
        public Guna.UI2.WinForms.Guna2Button btnTumunuSil;
        public System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.ToolTip tooltip;
        public System.Windows.Forms.ContextMenuStrip contextMenu;
    }
}