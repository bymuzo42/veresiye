namespace Veresiye2025
{
    partial class GunSonuForm
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

        public void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GunSonuForm));
            this.panelTitleBar = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnClose = new Guna.UI2.WinForms.Guna2Button();
            this.panelContent = new Guna.UI2.WinForms.Guna2Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.CihazAdiLabel = new System.Windows.Forms.Label();
            this.BankaAdiLabel = new System.Windows.Forms.Label();
            this.ValörLabel = new System.Windows.Forms.Label();
            this.TutarLabel = new System.Windows.Forms.Label();
            this.sayac = new System.Windows.Forms.Label();
            this.tarih = new System.Windows.Forms.Label();
            this.CihazAdiComboBox = new Guna.UI2.WinForms.Guna2ComboBox();
            this.BankaAdiComboBox = new Guna.UI2.WinForms.Guna2ComboBox();
            this.ValörTextBox = new Guna.UI2.WinForms.Guna2TextBox();
            this.TutarTextBox = new Guna.UI2.WinForms.Guna2TextBox();
            this.SayaçTextBox = new Guna.UI2.WinForms.Guna2TextBox();
            this.IslemTarihiPicker = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.BankaAdiTextBox = new Guna.UI2.WinForms.Guna2TextBox();
            this.CihazAdiTextBox = new Guna.UI2.WinForms.Guna2TextBox();
            this.panelButtons = new Guna.UI2.WinForms.Guna2Panel();
            this.btntemizle = new Guna.UI2.WinForms.Guna2Button();
            this.btnkaydet = new Guna.UI2.WinForms.Guna2Button();
            this.panelNote = new Guna.UI2.WinForms.Guna2Panel();
            this.not = new System.Windows.Forms.Label();
            this.formBorder = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.panelTitleBar.SuspendLayout();
            this.panelContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelButtons.SuspendLayout();
            this.panelNote.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTitleBar
            // 
            this.panelTitleBar.BorderRadius = 5;
            this.panelTitleBar.Controls.Add(this.lblTitle);
            this.panelTitleBar.Controls.Add(this.btnClose);
            this.panelTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitleBar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.panelTitleBar.Location = new System.Drawing.Point(10, 10);
            this.panelTitleBar.Name = "panelTitleBar";
            this.panelTitleBar.Size = new System.Drawing.Size(600, 40);
            this.panelTitleBar.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(12, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(135, 25);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Günsonu Ekle";
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
            this.btnClose.Location = new System.Drawing.Point(560, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(40, 40);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "X";
            this.btnClose.UseTransparentBackground = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panelContent
            // 
            this.panelContent.Controls.Add(this.pictureBox1);
            this.panelContent.Controls.Add(this.CihazAdiLabel);
            this.panelContent.Controls.Add(this.BankaAdiLabel);
            this.panelContent.Controls.Add(this.ValörLabel);
            this.panelContent.Controls.Add(this.TutarLabel);
            this.panelContent.Controls.Add(this.sayac);
            this.panelContent.Controls.Add(this.tarih);
            this.panelContent.Controls.Add(this.CihazAdiComboBox);
            this.panelContent.Controls.Add(this.BankaAdiComboBox);
            this.panelContent.Controls.Add(this.ValörTextBox);
            this.panelContent.Controls.Add(this.TutarTextBox);
            this.panelContent.Controls.Add(this.SayaçTextBox);
            this.panelContent.Controls.Add(this.IslemTarihiPicker);
            this.panelContent.Controls.Add(this.BankaAdiTextBox);
            this.panelContent.Controls.Add(this.CihazAdiTextBox);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelContent.Location = new System.Drawing.Point(10, 50);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(600, 320);
            this.panelContent.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::Veresiye2025.Properties.Resources.poscihaz;
            this.pictureBox1.Location = new System.Drawing.Point(390, 30);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(180, 240);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // CihazAdiLabel
            // 
            this.CihazAdiLabel.AutoSize = true;
            this.CihazAdiLabel.BackColor = System.Drawing.Color.Transparent;
            this.CihazAdiLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.CihazAdiLabel.Location = new System.Drawing.Point(15, 100);
            this.CihazAdiLabel.Name = "CihazAdiLabel";
            this.CihazAdiLabel.Size = new System.Drawing.Size(86, 23);
            this.CihazAdiLabel.TabIndex = 5;
            this.CihazAdiLabel.Text = "Cihaz Adı:";
            // 
            // BankaAdiLabel
            // 
            this.BankaAdiLabel.AutoSize = true;
            this.BankaAdiLabel.BackColor = System.Drawing.Color.Transparent;
            this.BankaAdiLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.BankaAdiLabel.Location = new System.Drawing.Point(15, 55);
            this.BankaAdiLabel.Name = "BankaAdiLabel";
            this.BankaAdiLabel.Size = new System.Drawing.Size(91, 23);
            this.BankaAdiLabel.TabIndex = 6;
            this.BankaAdiLabel.Text = "Banka Adı:";
            // 
            // ValörLabel
            // 
            this.ValörLabel.AutoSize = true;
            this.ValörLabel.BackColor = System.Drawing.Color.Transparent;
            this.ValörLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.ValörLabel.Location = new System.Drawing.Point(15, 145);
            this.ValörLabel.Name = "ValörLabel";
            this.ValörLabel.Size = new System.Drawing.Size(54, 23);
            this.ValörLabel.TabIndex = 7;
            this.ValörLabel.Text = "Valör:";
            // 
            // TutarLabel
            // 
            this.TutarLabel.AutoSize = true;
            this.TutarLabel.BackColor = System.Drawing.Color.Transparent;
            this.TutarLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.TutarLabel.Location = new System.Drawing.Point(15, 190);
            this.TutarLabel.Name = "TutarLabel";
            this.TutarLabel.Size = new System.Drawing.Size(54, 23);
            this.TutarLabel.TabIndex = 8;
            this.TutarLabel.Text = "Tutar:";
            // 
            // sayac
            // 
            this.sayac.AutoSize = true;
            this.sayac.BackColor = System.Drawing.Color.Transparent;
            this.sayac.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.sayac.Location = new System.Drawing.Point(15, 15);
            this.sayac.Name = "sayac";
            this.sayac.Size = new System.Drawing.Size(58, 23);
            this.sayac.TabIndex = 9;
            this.sayac.Text = "Sayaç:";
            // 
            // tarih
            // 
            this.tarih.AutoSize = true;
            this.tarih.BackColor = System.Drawing.Color.Transparent;
            this.tarih.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.tarih.Location = new System.Drawing.Point(15, 240);
            this.tarih.Name = "tarih";
            this.tarih.Size = new System.Drawing.Size(50, 23);
            this.tarih.TabIndex = 10;
            this.tarih.Text = "Tarih:";
            // 
            // CihazAdiComboBox
            // 
            this.CihazAdiComboBox.BackColor = System.Drawing.Color.Transparent;
            this.CihazAdiComboBox.BorderRadius = 10;
            this.CihazAdiComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.CihazAdiComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CihazAdiComboBox.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.CihazAdiComboBox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.CihazAdiComboBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.CihazAdiComboBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.CihazAdiComboBox.ItemHeight = 30;
            this.CihazAdiComboBox.Location = new System.Drawing.Point(120, 90);
            this.CihazAdiComboBox.Name = "CihazAdiComboBox";
            this.CihazAdiComboBox.Size = new System.Drawing.Size(240, 36);
            this.CihazAdiComboBox.TabIndex = 1;
            // 
            // BankaAdiComboBox
            // 
            this.BankaAdiComboBox.BackColor = System.Drawing.Color.Transparent;
            this.BankaAdiComboBox.BorderRadius = 10;
            this.BankaAdiComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.BankaAdiComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.BankaAdiComboBox.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.BankaAdiComboBox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.BankaAdiComboBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.BankaAdiComboBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.BankaAdiComboBox.ItemHeight = 30;
            this.BankaAdiComboBox.Location = new System.Drawing.Point(120, 45);
            this.BankaAdiComboBox.Name = "BankaAdiComboBox";
            this.BankaAdiComboBox.Size = new System.Drawing.Size(240, 36);
            this.BankaAdiComboBox.TabIndex = 0;
            this.BankaAdiComboBox.SelectedIndexChanged += new System.EventHandler(this.BankaAdiComboBox_SelectedIndexChanged);
            // 
            // ValörTextBox
            // 
            this.ValörTextBox.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ValörTextBox.BorderRadius = 10;
            this.ValörTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ValörTextBox.DefaultText = "";
            this.ValörTextBox.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.ValörTextBox.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.ValörTextBox.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.ValörTextBox.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.ValörTextBox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.ValörTextBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ValörTextBox.ForeColor = System.Drawing.Color.Black;
            this.ValörTextBox.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.ValörTextBox.Location = new System.Drawing.Point(120, 135);
            this.ValörTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ValörTextBox.Name = "ValörTextBox";
            this.ValörTextBox.PlaceholderText = "";
            this.ValörTextBox.ReadOnly = true;
            this.ValörTextBox.SelectedText = "";
            this.ValörTextBox.Size = new System.Drawing.Size(240, 36);
            this.ValörTextBox.TabIndex = 2;
            // 
            // TutarTextBox
            // 
            this.TutarTextBox.BorderRadius = 10;
            this.TutarTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TutarTextBox.DefaultText = "";
            this.TutarTextBox.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.TutarTextBox.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.TutarTextBox.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.TutarTextBox.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.TutarTextBox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.TutarTextBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.TutarTextBox.ForeColor = System.Drawing.Color.Black;
            this.TutarTextBox.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.TutarTextBox.Location = new System.Drawing.Point(120, 180);
            this.TutarTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TutarTextBox.Name = "TutarTextBox";
            this.TutarTextBox.PlaceholderText = "Tutar girin (₺)...";
            this.TutarTextBox.SelectedText = "";
            this.TutarTextBox.Size = new System.Drawing.Size(240, 36);
            this.TutarTextBox.TabIndex = 3;
            // 
            // SayaçTextBox
            // 
            this.SayaçTextBox.BorderRadius = 10;
            this.SayaçTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.SayaçTextBox.DefaultText = "";
            this.SayaçTextBox.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.SayaçTextBox.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.SayaçTextBox.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.SayaçTextBox.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.SayaçTextBox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.SayaçTextBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.SayaçTextBox.ForeColor = System.Drawing.Color.Black;
            this.SayaçTextBox.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.SayaçTextBox.Location = new System.Drawing.Point(120, 5);
            this.SayaçTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.SayaçTextBox.Name = "SayaçTextBox";
            this.SayaçTextBox.PlaceholderText = "";
            this.SayaçTextBox.ReadOnly = true;
            this.SayaçTextBox.SelectedText = "";
            this.SayaçTextBox.Size = new System.Drawing.Size(240, 36);
            this.SayaçTextBox.TabIndex = 11;
            // 
            // IslemTarihiPicker
            // 
            this.IslemTarihiPicker.BorderRadius = 10;
            this.IslemTarihiPicker.Checked = true;
            this.IslemTarihiPicker.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            this.IslemTarihiPicker.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.IslemTarihiPicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.IslemTarihiPicker.Location = new System.Drawing.Point(120, 230);
            this.IslemTarihiPicker.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.IslemTarihiPicker.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.IslemTarihiPicker.Name = "IslemTarihiPicker";
            this.IslemTarihiPicker.Size = new System.Drawing.Size(240, 36);
            this.IslemTarihiPicker.TabIndex = 4;
            this.IslemTarihiPicker.Value = new System.DateTime(2025, 5, 17, 0, 0, 0, 0);
            // 
            // BankaAdiTextBox
            // 
            this.BankaAdiTextBox.BorderRadius = 10;
            this.BankaAdiTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.BankaAdiTextBox.DefaultText = "";
            this.BankaAdiTextBox.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.BankaAdiTextBox.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.BankaAdiTextBox.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.BankaAdiTextBox.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.BankaAdiTextBox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.BankaAdiTextBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.BankaAdiTextBox.ForeColor = System.Drawing.Color.Black;
            this.BankaAdiTextBox.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.BankaAdiTextBox.Location = new System.Drawing.Point(120, 45);
            this.BankaAdiTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BankaAdiTextBox.Name = "BankaAdiTextBox";
            this.BankaAdiTextBox.PlaceholderText = "";
            this.BankaAdiTextBox.ReadOnly = true;
            this.BankaAdiTextBox.SelectedText = "";
            this.BankaAdiTextBox.Size = new System.Drawing.Size(240, 36);
            this.BankaAdiTextBox.TabIndex = 12;
            this.BankaAdiTextBox.Visible = false;
            // 
            // CihazAdiTextBox
            // 
            this.CihazAdiTextBox.BorderRadius = 10;
            this.CihazAdiTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.CihazAdiTextBox.DefaultText = "";
            this.CihazAdiTextBox.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.CihazAdiTextBox.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.CihazAdiTextBox.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.CihazAdiTextBox.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.CihazAdiTextBox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.CihazAdiTextBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.CihazAdiTextBox.ForeColor = System.Drawing.Color.Black;
            this.CihazAdiTextBox.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.CihazAdiTextBox.Location = new System.Drawing.Point(120, 90);
            this.CihazAdiTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CihazAdiTextBox.Name = "CihazAdiTextBox";
            this.CihazAdiTextBox.PlaceholderText = "";
            this.CihazAdiTextBox.ReadOnly = true;
            this.CihazAdiTextBox.SelectedText = "";
            this.CihazAdiTextBox.Size = new System.Drawing.Size(240, 36);
            this.CihazAdiTextBox.TabIndex = 13;
            this.CihazAdiTextBox.Visible = false;
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.btntemizle);
            this.panelButtons.Controls.Add(this.btnkaydet);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelButtons.Location = new System.Drawing.Point(10, 370);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(600, 60);
            this.panelButtons.TabIndex = 2;
            // 
            // btntemizle
            // 
            this.btntemizle.Animated = true;
            this.btntemizle.BorderRadius = 10;
            this.btntemizle.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btntemizle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btntemizle.ForeColor = System.Drawing.Color.White;
            this.btntemizle.Image = global::Veresiye2025.Properties.Resources.delete;
            this.btntemizle.Location = new System.Drawing.Point(215, 12);
            this.btntemizle.Name = "btntemizle";
            this.btntemizle.Size = new System.Drawing.Size(140, 40);
            this.btntemizle.TabIndex = 6;
            this.btntemizle.Text = "Temizle";
            this.btntemizle.Click += new System.EventHandler(this.btntemizle_Click);
            // 
            // btnkaydet
            // 
            this.btnkaydet.Animated = true;
            this.btnkaydet.BorderRadius = 10;
            this.btnkaydet.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnkaydet.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnkaydet.ForeColor = System.Drawing.Color.White;
            this.btnkaydet.Image = global::Veresiye2025.Properties.Resources.open;
            this.btnkaydet.Location = new System.Drawing.Point(65, 12);
            this.btnkaydet.Name = "btnkaydet";
            this.btnkaydet.Size = new System.Drawing.Size(140, 40);
            this.btnkaydet.TabIndex = 5;
            this.btnkaydet.Text = "Kaydet";
            this.btnkaydet.Click += new System.EventHandler(this.btnkaydet_Click);
            // 
            // panelNote
            // 
            this.panelNote.Controls.Add(this.not);
            this.panelNote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelNote.Location = new System.Drawing.Point(10, 430);
            this.panelNote.Name = "panelNote";
            this.panelNote.Size = new System.Drawing.Size(600, 250);
            this.panelNote.TabIndex = 3;
            // 
            // not
            // 
            this.not.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.not.Location = new System.Drawing.Point(15, 15);
            this.not.Name = "not";
            this.not.Size = new System.Drawing.Size(570, 225);
            this.not.TabIndex = 0;
            this.not.Text = resources.GetString("not.Text");
            // 
            // formBorder
            // 
            this.formBorder.BorderRadius = 20;
            this.formBorder.TargetControl = this;
            // 
            // GunSonuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(620, 690);
            this.Controls.Add(this.panelNote);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelTitleBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GunSonuForm";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Günsonu Ekle";
            this.Load += new System.EventHandler(this.GunSonuForm_Load);
            this.panelTitleBar.ResumeLayout(false);
            this.panelTitleBar.PerformLayout();
            this.panelContent.ResumeLayout(false);
            this.panelContent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelButtons.ResumeLayout(false);
            this.panelNote.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        public Guna.UI2.WinForms.Guna2Panel panelTitleBar;
        public Guna.UI2.WinForms.Guna2Button btnClose;
        public System.Windows.Forms.Label lblTitle;
        public Guna.UI2.WinForms.Guna2Panel panelContent;
        public Guna.UI2.WinForms.Guna2Panel panelButtons;
        public Guna.UI2.WinForms.Guna2Panel panelNote;
        public Guna.UI2.WinForms.Guna2Elipse formBorder;
        public System.Windows.Forms.ToolTip toolTip;
        public System.Windows.Forms.Label CihazAdiLabel;
        public System.Windows.Forms.Label BankaAdiLabel;
        public System.Windows.Forms.Label ValörLabel;
        public System.Windows.Forms.Label TutarLabel;
        public System.Windows.Forms.Label sayac;
        public System.Windows.Forms.Label tarih;
        public System.Windows.Forms.Label not;
        public System.Windows.Forms.PictureBox pictureBox1;
        internal Guna.UI2.WinForms.Guna2ComboBox CihazAdiComboBox;
        internal Guna.UI2.WinForms.Guna2ComboBox BankaAdiComboBox;
        internal Guna.UI2.WinForms.Guna2TextBox ValörTextBox;
        internal Guna.UI2.WinForms.Guna2TextBox TutarTextBox;
        internal Guna.UI2.WinForms.Guna2TextBox SayaçTextBox;
        internal Guna.UI2.WinForms.Guna2DateTimePicker IslemTarihiPicker;
        internal Guna.UI2.WinForms.Guna2TextBox BankaAdiTextBox;
        internal Guna.UI2.WinForms.Guna2TextBox CihazAdiTextBox;
        public Guna.UI2.WinForms.Guna2Button btntemizle;
        public Guna.UI2.WinForms.Guna2Button btnkaydet;
    }
}