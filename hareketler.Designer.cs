namespace Veresiye2025
{
    partial class Hareketler
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Hareketler));
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.btnClose = new Guna.UI2.WinForms.Guna2ControlBox();
            this.lblFormTitle = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.son = new System.Windows.Forms.DateTimePicker();
            this.ilk = new System.Windows.Forms.DateTimePicker();
            this.sontarih = new System.Windows.Forms.Label();
            this.ilktarih = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.hareketunvan = new Guna.UI2.WinForms.Guna2TextBox();
            this.tursecenek = new Guna.UI2.WinForms.Guna2ComboBox();
            this.Unvani = new System.Windows.Forms.Label();
            this.Tür = new System.Windows.Forms.Label();
            this.hareketgrid = new System.Windows.Forms.DataGridView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.alacaktext = new Guna.UI2.WinForms.Guna2TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.borctext = new Guna.UI2.WinForms.Guna2TextBox();
            this.hrktsorgula = new Guna.UI2.WinForms.Guna2Button();
            this.hrktyazdir = new Guna.UI2.WinForms.Guna2Button();
            this.hrktformkapat = new Guna.UI2.WinForms.Guna2Button();
            this.hrktdetay = new Guna.UI2.WinForms.Guna2Button();
            this.printDocument = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
            this.printDialog = new System.Windows.Forms.PrintDialog();
            this.pnlHeader.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hareketgrid)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.DodgerBlue;
            this.pnlHeader.Controls.Add(this.btnClose);
            this.pnlHeader.Controls.Add(this.lblFormTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1004, 40);
            this.pnlHeader.TabIndex = 35;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.FillColor = System.Drawing.Color.Transparent;
            this.btnClose.HoverState.FillColor = System.Drawing.Color.Red; // Mouse üzerine gelince kırmızı arkaplan
            this.btnClose.IconColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(964, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(40, 40);
            this.btnClose.TabIndex = 37;
            // 
            // lblFormTitle
            // 
            this.lblFormTitle.AutoSize = true;
            this.lblFormTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblFormTitle.ForeColor = System.Drawing.Color.White;
            this.lblFormTitle.Location = new System.Drawing.Point(12, 9);
            this.lblFormTitle.Name = "lblFormTitle";
            this.lblFormTitle.Size = new System.Drawing.Size(155, 28);
            this.lblFormTitle.TabIndex = 0;
            this.lblFormTitle.Text = "Cari Hareketler";
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.White;
            this.pnlMain.Controls.Add(this.groupBox1);
            this.pnlMain.Controls.Add(this.groupBox2);
            this.pnlMain.Controls.Add(this.hareketgrid);
            this.pnlMain.Controls.Add(this.groupBox4);
            this.pnlMain.Controls.Add(this.hrktsorgula);
            this.pnlMain.Controls.Add(this.hrktyazdir);
            this.pnlMain.Controls.Add(this.hrktformkapat);
            this.pnlMain.Controls.Add(this.hrktdetay);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 40);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1004, 513);
            this.pnlMain.TabIndex = 36;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.son);
            this.groupBox1.Controls.Add(this.ilk);
            this.groupBox1.Controls.Add(this.sontarih);
            this.groupBox1.Controls.Add(this.ilktarih);
            this.groupBox1.Location = new System.Drawing.Point(12, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(316, 88);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // son
            // 
            this.son.CalendarFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.son.CalendarForeColor = System.Drawing.Color.Black;
            this.son.CalendarMonthBackground = System.Drawing.Color.White;
            this.son.CalendarTitleBackColor = System.Drawing.Color.DodgerBlue;
            this.son.CalendarTitleForeColor = System.Drawing.Color.White;
            this.son.CustomFormat = "dd.MM.yyyy";
            this.son.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.son.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.son.Location = new System.Drawing.Point(109, 51);
            this.son.Name = "son";
            this.son.Size = new System.Drawing.Size(190, 27);
            this.son.TabIndex = 5;
            this.son.Value = new System.DateTime(2025, 5, 21, 0, 0, 0, 0);
            // 
            // ilk
            // 
            this.ilk.CalendarFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.ilk.CalendarForeColor = System.Drawing.Color.Black;
            this.ilk.CalendarMonthBackground = System.Drawing.Color.White;
            this.ilk.CalendarTitleBackColor = System.Drawing.Color.DodgerBlue;
            this.ilk.CalendarTitleForeColor = System.Drawing.Color.White;
            this.ilk.CustomFormat = "dd.MM.yyyy";
            this.ilk.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.ilk.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ilk.Location = new System.Drawing.Point(109, 16);
            this.ilk.Name = "ilk";
            this.ilk.Size = new System.Drawing.Size(190, 27);
            this.ilk.TabIndex = 4;
            this.ilk.Value = new System.DateTime(2025, 5, 21, 0, 0, 0, 0);
            // 
            // sontarih
            // 
            this.sontarih.AutoSize = true;
            this.sontarih.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.sontarih.Location = new System.Drawing.Point(6, 53);
            this.sontarih.Name = "sontarih";
            this.sontarih.Size = new System.Drawing.Size(82, 20);
            this.sontarih.TabIndex = 1;
            this.sontarih.Text = "Son Tarih :";
            // 
            // ilktarih
            // 
            this.ilktarih.AutoSize = true;
            this.ilktarih.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.ilktarih.Location = new System.Drawing.Point(6, 15);
            this.ilktarih.Name = "ilktarih";
            this.ilktarih.Size = new System.Drawing.Size(73, 20);
            this.ilktarih.TabIndex = 0;
            this.ilktarih.Text = "İlk Tarih :";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.hareketunvan);
            this.groupBox2.Controls.Add(this.tursecenek);
            this.groupBox2.Controls.Add(this.Unvani);
            this.groupBox2.Controls.Add(this.Tür);
            this.groupBox2.Location = new System.Drawing.Point(334, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(363, 88);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // hareketunvan
            // 
            this.hareketunvan.Animated = true;
            this.hareketunvan.BackColor = System.Drawing.Color.Transparent;
            this.hareketunvan.BorderRadius = 5;
            this.hareketunvan.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.hareketunvan.DefaultText = "";
            this.hareketunvan.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.hareketunvan.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.hareketunvan.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.hareketunvan.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.hareketunvan.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.hareketunvan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.hareketunvan.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.hareketunvan.Location = new System.Drawing.Point(86, 46);
            this.hareketunvan.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.hareketunvan.Name = "hareketunvan";
            this.hareketunvan.PlaceholderText = "Unvan Giriniz";
            this.hareketunvan.SelectedText = "";
            this.hareketunvan.Size = new System.Drawing.Size(233, 36);
            this.hareketunvan.TabIndex = 21;
            this.hareketunvan.TextChanged += new System.EventHandler(this.hareketunvan_TextChanged);
            // 
            // tursecenek
            // 
            this.tursecenek.BackColor = System.Drawing.Color.Transparent;
            this.tursecenek.BorderRadius = 5;
            this.tursecenek.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.tursecenek.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tursecenek.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tursecenek.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tursecenek.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tursecenek.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.tursecenek.ItemHeight = 30;
            this.tursecenek.Location = new System.Drawing.Point(86, 7);
            this.tursecenek.Name = "tursecenek";
            this.tursecenek.Size = new System.Drawing.Size(233, 36);
            this.tursecenek.TabIndex = 20;
            // 
            // Unvani
            // 
            this.Unvani.AutoSize = true;
            this.Unvani.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Unvani.Location = new System.Drawing.Point(6, 53);
            this.Unvani.Name = "Unvani";
            this.Unvani.Size = new System.Drawing.Size(62, 20);
            this.Unvani.TabIndex = 2;
            this.Unvani.Text = "Ünvan :";
            // 
            // Tür
            // 
            this.Tür.AutoSize = true;
            this.Tür.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Tür.Location = new System.Drawing.Point(6, 15);
            this.Tür.Name = "Tür";
            this.Tür.Size = new System.Drawing.Size(49, 20);
            this.Tür.TabIndex = 2;
            this.Tür.Text = "Türü :";
            // 
            // hareketgrid
            // 
            this.hareketgrid.BackgroundColor = System.Drawing.Color.White;
            this.hareketgrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.hareketgrid.GridColor = System.Drawing.Color.White;
            this.hareketgrid.Location = new System.Drawing.Point(12, 98);
            this.hareketgrid.Name = "hareketgrid";
            this.hareketgrid.RowHeadersWidth = 51;
            this.hareketgrid.RowTemplate.Height = 24;
            this.hareketgrid.Size = new System.Drawing.Size(975, 350);
            this.hareketgrid.TabIndex = 3;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.alacaktext);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.borctext);
            this.groupBox4.Location = new System.Drawing.Point(143, 454);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(686, 53);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            // 
            // alacaktext
            // 
            this.alacaktext.Animated = true;
            this.alacaktext.BackColor = System.Drawing.Color.Transparent;
            this.alacaktext.BorderRadius = 5;
            this.alacaktext.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.alacaktext.DefaultText = "";
            this.alacaktext.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.alacaktext.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.alacaktext.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.alacaktext.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.alacaktext.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.alacaktext.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.alacaktext.ForeColor = System.Drawing.Color.Blue;
            this.alacaktext.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.alacaktext.Location = new System.Drawing.Point(413, 12);
            this.alacaktext.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.alacaktext.Name = "alacaktext";
            this.alacaktext.PlaceholderText = "";
            this.alacaktext.ReadOnly = true;
            this.alacaktext.SelectedText = "";
            this.alacaktext.Size = new System.Drawing.Size(228, 36);
            this.alacaktext.TabIndex = 22;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(329, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 20);
            this.label2.TabIndex = 8;
            this.label2.Text = "T.Alacak :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(3, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "T.Borç :";
            // 
            // borctext
            // 
            this.borctext.Animated = true;
            this.borctext.BackColor = System.Drawing.Color.Transparent;
            this.borctext.BorderRadius = 5;
            this.borctext.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.borctext.DefaultText = "";
            this.borctext.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.borctext.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.borctext.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.borctext.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.borctext.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.borctext.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.borctext.ForeColor = System.Drawing.Color.Red;
            this.borctext.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.borctext.Location = new System.Drawing.Point(76, 12);
            this.borctext.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.borctext.Name = "borctext";
            this.borctext.PlaceholderText = "";
            this.borctext.ReadOnly = true;
            this.borctext.SelectedText = "";
            this.borctext.Size = new System.Drawing.Size(232, 36);
            this.borctext.TabIndex = 9;
            // 
            // hrktsorgula
            // 
            this.hrktsorgula.Animated = true;
            this.hrktsorgula.AnimatedGIF = true;
            this.hrktsorgula.BackColor = System.Drawing.Color.Transparent;
            this.hrktsorgula.BorderRadius = 5;
            this.hrktsorgula.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hrktsorgula.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.hrktsorgula.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.hrktsorgula.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.hrktsorgula.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.hrktsorgula.FillColor = System.Drawing.Color.DodgerBlue;
            this.hrktsorgula.FocusedColor = System.Drawing.Color.White;
            this.hrktsorgula.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.hrktsorgula.ForeColor = System.Drawing.Color.White;
            this.hrktsorgula.Image = global::Veresiye2025.Properties.Resources.sorguhareket;
            this.hrktsorgula.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.hrktsorgula.ImageSize = new System.Drawing.Size(30, 30);
            this.hrktsorgula.IndicateFocus = true;
            this.hrktsorgula.Location = new System.Drawing.Point(703, 12);
            this.hrktsorgula.Name = "hrktsorgula";
            this.hrktsorgula.Size = new System.Drawing.Size(138, 81);
            this.hrktsorgula.TabIndex = 31;
            this.hrktsorgula.Text = "Sorgula";
            this.hrktsorgula.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.hrktsorgula.TextFormatNoPrefix = true;
            this.hrktsorgula.Click += new System.EventHandler(this.hrktsorgula_Click);
            // 
            // hrktyazdir
            // 
            this.hrktyazdir.Animated = true;
            this.hrktyazdir.AnimatedGIF = true;
            this.hrktyazdir.BackColor = System.Drawing.Color.Transparent;
            this.hrktyazdir.BorderRadius = 5;
            this.hrktyazdir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hrktyazdir.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.hrktyazdir.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.hrktyazdir.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.hrktyazdir.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.hrktyazdir.FillColor = System.Drawing.Color.DodgerBlue;
            this.hrktyazdir.FocusedColor = System.Drawing.Color.White;
            this.hrktyazdir.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.hrktyazdir.ForeColor = System.Drawing.Color.White;
            this.hrktyazdir.Image = global::Veresiye2025.Properties.Resources.printhareket;
            this.hrktyazdir.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.hrktyazdir.ImageSize = new System.Drawing.Size(30, 30);
            this.hrktyazdir.IndicateFocus = true;
            this.hrktyazdir.Location = new System.Drawing.Point(848, 12);
            this.hrktyazdir.Name = "hrktyazdir";
            this.hrktyazdir.Size = new System.Drawing.Size(138, 81);
            this.hrktyazdir.TabIndex = 32;
            this.hrktyazdir.Text = "Yazdır";
            this.hrktyazdir.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.hrktyazdir.TextFormatNoPrefix = true;
            this.hrktyazdir.Click += new System.EventHandler(this.hrktyazdir_Click);
            // 
            // hrktformkapat
            // 
            this.hrktformkapat.Animated = true;
            this.hrktformkapat.AnimatedGIF = true;
            this.hrktformkapat.BackColor = System.Drawing.Color.Transparent;
            this.hrktformkapat.BorderRadius = 5;
            this.hrktformkapat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hrktformkapat.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.hrktformkapat.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.hrktformkapat.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.hrktformkapat.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.hrktformkapat.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.hrktformkapat.FillColor = System.Drawing.Color.DodgerBlue;
            this.hrktformkapat.FocusedColor = System.Drawing.Color.White;
            this.hrktformkapat.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.hrktformkapat.ForeColor = System.Drawing.Color.White;
            this.hrktformkapat.Image = global::Veresiye2025.Properties.Resources.kapat;
            this.hrktformkapat.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.hrktformkapat.ImageSize = new System.Drawing.Size(30, 30);
            this.hrktformkapat.IndicateFocus = true;
            this.hrktformkapat.Location = new System.Drawing.Point(12, 453);
            this.hrktformkapat.Name = "hrktformkapat";
            this.hrktformkapat.Size = new System.Drawing.Size(125, 55);
            this.hrktformkapat.TabIndex = 33;
            this.hrktformkapat.Text = "Kapat";
            this.hrktformkapat.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.hrktformkapat.TextFormatNoPrefix = true;
            this.hrktformkapat.Click += new System.EventHandler(this.hrktformkapat_Click_1);
            // 
            // hrktdetay
            // 
            this.hrktdetay.Animated = true;
            this.hrktdetay.AnimatedGIF = true;
            this.hrktdetay.BackColor = System.Drawing.Color.Transparent;
            this.hrktdetay.BorderRadius = 5;
            this.hrktdetay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hrktdetay.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.hrktdetay.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.hrktdetay.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.hrktdetay.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.hrktdetay.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.hrktdetay.FillColor = System.Drawing.Color.DodgerBlue;
            this.hrktdetay.FocusedColor = System.Drawing.Color.White;
            this.hrktdetay.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.hrktdetay.ForeColor = System.Drawing.Color.White;
            this.hrktdetay.Image = global::Veresiye2025.Properties.Resources._return;
            this.hrktdetay.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.hrktdetay.ImageSize = new System.Drawing.Size(30, 30);
            this.hrktdetay.IndicateFocus = true;
            this.hrktdetay.Location = new System.Drawing.Point(835, 453);
            this.hrktdetay.Name = "hrktdetay";
            this.hrktdetay.Size = new System.Drawing.Size(152, 55);
            this.hrktdetay.TabIndex = 34;
            this.hrktdetay.Text = "İşlem Detayı";
            this.hrktdetay.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.hrktdetay.TextFormatNoPrefix = true;
            this.hrktdetay.Click += new System.EventHandler(this.hrktdetay_Click);
            // 
            // printPreviewDialog
            // 
            this.printPreviewDialog.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog.Enabled = true;
            this.printPreviewDialog.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog.Icon")));
            this.printPreviewDialog.Name = "printPreviewDialog";
            this.printPreviewDialog.Text = "Baskı önizleme";
            this.printPreviewDialog.Visible = false;
            // 
            // printDialog
            // 
            this.printDialog.UseEXDialog = true;
            // 
            // Hareketler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.hrktformkapat;
            this.ClientSize = new System.Drawing.Size(1004, 553);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Hareketler";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cari Hareketler";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hareketgrid)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        public System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.Label sontarih;
        public System.Windows.Forms.Label ilktarih;
        public System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.Label Unvani;
        public System.Windows.Forms.Label Tür;
        public System.Windows.Forms.DataGridView hareketgrid;
        public System.Windows.Forms.GroupBox groupBox4;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label1;
        public System.Drawing.Printing.PrintDocument printDocument;
        public System.Windows.Forms.PrintPreviewDialog printPreviewDialog;
        public System.Windows.Forms.PrintDialog printDialog;
        public Guna.UI2.WinForms.Guna2Button hrktsorgula;
        public Guna.UI2.WinForms.Guna2Button hrktyazdir;
        public Guna.UI2.WinForms.Guna2Button hrktformkapat;
        public Guna.UI2.WinForms.Guna2Button hrktdetay;
        public System.Windows.Forms.Panel pnlHeader;
        public System.Windows.Forms.Label lblFormTitle;
        public System.Windows.Forms.Panel pnlMain;
        public Guna.UI2.WinForms.Guna2ControlBox btnClose;
        public System.Windows.Forms.DateTimePicker son;
        public System.Windows.Forms.DateTimePicker ilk;
        public Guna.UI2.WinForms.Guna2ComboBox tursecenek;
        public Guna.UI2.WinForms.Guna2TextBox hareketunvan;
        public Guna.UI2.WinForms.Guna2TextBox borctext;
        public Guna.UI2.WinForms.Guna2TextBox alacaktext;
    }
}