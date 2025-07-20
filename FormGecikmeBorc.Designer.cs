namespace Veresiye2025
{
    partial class FormGecikmeBorc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGecikmeBorc));
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.btnInfo = new Guna.UI2.WinForms.Guna2Button();
            this.btnClose = new Guna.UI2.WinForms.Guna2Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.warningIconPictureBox = new System.Windows.Forms.PictureBox();
            this.pnlCariBaslik = new System.Windows.Forms.Panel();
            this.lblCariUnvan = new System.Windows.Forms.Label();
            this.lblCariBaslik = new System.Windows.Forms.Label();
            this.pnlBilgiler = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblFaturaTarihi = new System.Windows.Forms.Label();
            this.lblFaturaTarihiBaslik = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblVade = new System.Windows.Forms.Label();
            this.lblVadeBaslik = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblGecikmisBorc = new System.Windows.Forms.Label();
            this.lblGecikmisBaslik = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblBakiye = new System.Windows.Forms.Label();
            this.lblBakiyeBaslik = new System.Windows.Forms.Label();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnTahsilat = new Guna.UI2.WinForms.Guna2Button();
            this.btnTamam = new Guna.UI2.WinForms.Guna2Button();
            this.timerBlink = new System.Windows.Forms.Timer(this.components);
            this.pnlHeader.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.warningIconPictureBox)).BeginInit();
            this.pnlCariBaslik.SuspendLayout();
            this.pnlBilgiler.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.pnlHeader.Controls.Add(this.btnInfo);
            this.pnlHeader.Controls.Add(this.btnClose);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(500, 50);
            this.pnlHeader.TabIndex = 0;
            // 
            // btnInfo
            // 
            this.btnInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInfo.BackColor = System.Drawing.Color.Transparent;
            this.btnInfo.BorderRadius = 15;
            this.btnInfo.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(141)))), ((int)(((byte)(243)))));
            this.btnInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnInfo.ForeColor = System.Drawing.Color.White;
            this.btnInfo.Location = new System.Drawing.Point(392, 6);
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.Size = new System.Drawing.Size(50, 40);
            this.btnInfo.TabIndex = 2;
            this.btnInfo.Text = "?";
            this.btnInfo.UseTransparentBackground = true;
            this.btnInfo.Click += new System.EventHandler(this.btnInfo_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.BorderRadius = 15;
            this.btnClose.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(17)))), ((int)(((byte)(35)))));
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(445, 6);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(50, 40);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "X";
            this.btnClose.UseTransparentBackground = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblTitle.Size = new System.Drawing.Size(500, 50);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "GECİKMİŞ BORÇ BİLGİSİ";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.White;
            this.pnlMain.Controls.Add(this.warningIconPictureBox);
            this.pnlMain.Controls.Add(this.pnlCariBaslik);
            this.pnlMain.Controls.Add(this.pnlBilgiler);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 50);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(15);
            this.pnlMain.Size = new System.Drawing.Size(500, 275);
            this.pnlMain.TabIndex = 1;
            // 
            // warningIconPictureBox
            // 
            this.warningIconPictureBox.Image = global::Veresiye2025.Properties.Resources.warning;
            this.warningIconPictureBox.Location = new System.Drawing.Point(18, 18);
            this.warningIconPictureBox.Name = "warningIconPictureBox";
            this.warningIconPictureBox.Size = new System.Drawing.Size(40, 40);
            this.warningIconPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.warningIconPictureBox.TabIndex = 2;
            this.warningIconPictureBox.TabStop = false;
            // 
            // pnlCariBaslik
            // 
            this.pnlCariBaslik.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlCariBaslik.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.pnlCariBaslik.Controls.Add(this.lblCariUnvan);
            this.pnlCariBaslik.Controls.Add(this.lblCariBaslik);
            this.pnlCariBaslik.Location = new System.Drawing.Point(70, 18);
            this.pnlCariBaslik.Name = "pnlCariBaslik";
            this.pnlCariBaslik.Size = new System.Drawing.Size(415, 40);
            this.pnlCariBaslik.TabIndex = 1;
            // 
            // lblCariUnvan
            // 
            this.lblCariUnvan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCariUnvan.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblCariUnvan.ForeColor = System.Drawing.Color.White;
            this.lblCariUnvan.Location = new System.Drawing.Point(103, 0);
            this.lblCariUnvan.Name = "lblCariUnvan";
            this.lblCariUnvan.Size = new System.Drawing.Size(312, 40);
            this.lblCariUnvan.TabIndex = 1;
            this.lblCariUnvan.Text = "MÜŞTERİ UNVANI";
            this.lblCariUnvan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCariBaslik
            // 
            this.lblCariBaslik.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblCariBaslik.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblCariBaslik.ForeColor = System.Drawing.Color.White;
            this.lblCariBaslik.Location = new System.Drawing.Point(0, 0);
            this.lblCariBaslik.Name = "lblCariBaslik";
            this.lblCariBaslik.Size = new System.Drawing.Size(103, 40);
            this.lblCariBaslik.TabIndex = 0;
            this.lblCariBaslik.Text = "Cari Hesap:";
            this.lblCariBaslik.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlBilgiler
            // 
            this.pnlBilgiler.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlBilgiler.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.pnlBilgiler.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBilgiler.Controls.Add(this.panel4);
            this.pnlBilgiler.Controls.Add(this.panel3);
            this.pnlBilgiler.Controls.Add(this.panel2);
            this.pnlBilgiler.Controls.Add(this.panel1);
            this.pnlBilgiler.Location = new System.Drawing.Point(15, 73);
            this.pnlBilgiler.Name = "pnlBilgiler";
            this.pnlBilgiler.Padding = new System.Windows.Forms.Padding(10);
            this.pnlBilgiler.Size = new System.Drawing.Size(470, 187);
            this.pnlBilgiler.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.lblFaturaTarihi);
            this.panel4.Controls.Add(this.lblFaturaTarihiBaslik);
            this.panel4.Location = new System.Drawing.Point(10, 136);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(445, 36);
            this.panel4.TabIndex = 3;
            // 
            // lblFaturaTarihi
            // 
            this.lblFaturaTarihi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFaturaTarihi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblFaturaTarihi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblFaturaTarihi.Location = new System.Drawing.Point(250, 0);
            this.lblFaturaTarihi.Name = "lblFaturaTarihi";
            this.lblFaturaTarihi.Size = new System.Drawing.Size(193, 34);
            this.lblFaturaTarihi.TabIndex = 1;
            this.lblFaturaTarihi.Text = "01.01.2023";
            this.lblFaturaTarihi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFaturaTarihiBaslik
            // 
            this.lblFaturaTarihiBaslik.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblFaturaTarihiBaslik.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblFaturaTarihiBaslik.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblFaturaTarihiBaslik.Location = new System.Drawing.Point(0, 0);
            this.lblFaturaTarihiBaslik.Name = "lblFaturaTarihiBaslik";
            this.lblFaturaTarihiBaslik.Size = new System.Drawing.Size(250, 34);
            this.lblFaturaTarihiBaslik.TabIndex = 0;
            this.lblFaturaTarihiBaslik.Text = "Gecikmeye Düşen Fatura Tarihi:";
            this.lblFaturaTarihiBaslik.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.lblVade);
            this.panel3.Controls.Add(this.lblVadeBaslik);
            this.panel3.Location = new System.Drawing.Point(10, 94);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(445, 36);
            this.panel3.TabIndex = 2;
            // 
            // lblVade
            // 
            this.lblVade.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVade.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblVade.ForeColor = System.Drawing.Color.Blue;
            this.lblVade.Location = new System.Drawing.Point(250, 0);
            this.lblVade.Name = "lblVade";
            this.lblVade.Size = new System.Drawing.Size(193, 34);
            this.lblVade.TabIndex = 1;
            this.lblVade.Text = "30 Gün";
            this.lblVade.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblVadeBaslik
            // 
            this.lblVadeBaslik.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblVadeBaslik.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblVadeBaslik.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblVadeBaslik.Location = new System.Drawing.Point(0, 0);
            this.lblVadeBaslik.Name = "lblVadeBaslik";
            this.lblVadeBaslik.Size = new System.Drawing.Size(250, 34);
            this.lblVadeBaslik.TabIndex = 0;
            this.lblVadeBaslik.Text = "Vade Gün Sayısı:";
            this.lblVadeBaslik.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.lblGecikmisBorc);
            this.panel2.Controls.Add(this.lblGecikmisBaslik);
            this.panel2.Location = new System.Drawing.Point(10, 52);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(445, 36);
            this.panel2.TabIndex = 1;
            // 
            // lblGecikmisBorc
            // 
            this.lblGecikmisBorc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblGecikmisBorc.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblGecikmisBorc.ForeColor = System.Drawing.Color.Red;
            this.lblGecikmisBorc.Location = new System.Drawing.Point(250, 0);
            this.lblGecikmisBorc.Name = "lblGecikmisBorc";
            this.lblGecikmisBorc.Size = new System.Drawing.Size(193, 34);
            this.lblGecikmisBorc.TabIndex = 1;
            this.lblGecikmisBorc.Text = "1.000,00 ₺";
            this.lblGecikmisBorc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblGecikmisBaslik
            // 
            this.lblGecikmisBaslik.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblGecikmisBaslik.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblGecikmisBaslik.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblGecikmisBaslik.Location = new System.Drawing.Point(0, 0);
            this.lblGecikmisBaslik.Name = "lblGecikmisBaslik";
            this.lblGecikmisBaslik.Size = new System.Drawing.Size(250, 34);
            this.lblGecikmisBaslik.TabIndex = 0;
            this.lblGecikmisBaslik.Text = "Gecikmiş Borç:";
            this.lblGecikmisBaslik.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblBakiye);
            this.panel1.Controls.Add(this.lblBakiyeBaslik);
            this.panel1.Location = new System.Drawing.Point(10, 10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(445, 36);
            this.panel1.TabIndex = 0;
            // 
            // lblBakiye
            // 
            this.lblBakiye.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBakiye.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblBakiye.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(141)))), ((int)(((byte)(243)))));
            this.lblBakiye.Location = new System.Drawing.Point(250, 0);
            this.lblBakiye.Name = "lblBakiye";
            this.lblBakiye.Size = new System.Drawing.Size(193, 34);
            this.lblBakiye.TabIndex = 1;
            this.lblBakiye.Text = "5.000,00 ₺";
            this.lblBakiye.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblBakiyeBaslik
            // 
            this.lblBakiyeBaslik.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblBakiyeBaslik.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblBakiyeBaslik.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblBakiyeBaslik.Location = new System.Drawing.Point(0, 0);
            this.lblBakiyeBaslik.Name = "lblBakiyeBaslik";
            this.lblBakiyeBaslik.Size = new System.Drawing.Size(250, 34);
            this.lblBakiyeBaslik.TabIndex = 0;
            this.lblBakiyeBaslik.Text = "Güncel Bakiye:";
            this.lblBakiyeBaslik.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlButtons
            // 
            this.pnlButtons.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlButtons.Controls.Add(this.btnTahsilat);
            this.pnlButtons.Controls.Add(this.btnTamam);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.Location = new System.Drawing.Point(0, 325);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Padding = new System.Windows.Forms.Padding(10);
            this.pnlButtons.Size = new System.Drawing.Size(500, 65);
            this.pnlButtons.TabIndex = 2;
            // 
            // btnTahsilat
            // 
            this.btnTahsilat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTahsilat.Animated = true;
            this.btnTahsilat.BorderRadius = 5;
            this.btnTahsilat.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnTahsilat.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnTahsilat.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnTahsilat.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnTahsilat.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(153)))), ((int)(((byte)(78)))));
            this.btnTahsilat.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnTahsilat.ForeColor = System.Drawing.Color.White;
            this.btnTahsilat.Location = new System.Drawing.Point(319, 10);
            this.btnTahsilat.Name = "btnTahsilat";
            this.btnTahsilat.Size = new System.Drawing.Size(171, 45);
            this.btnTahsilat.TabIndex = 1;
            this.btnTahsilat.Text = "Tahsilat Yap";
            this.btnTahsilat.Click += new System.EventHandler(this.btnTahsilat_Click);
            // 
            // btnTamam
            // 
            this.btnTamam.Animated = true;
            this.btnTamam.BorderRadius = 5;
            this.btnTamam.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnTamam.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnTamam.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnTamam.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnTamam.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(141)))), ((int)(((byte)(243)))));
            this.btnTamam.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnTamam.ForeColor = System.Drawing.Color.White;
            this.btnTamam.Location = new System.Drawing.Point(10, 10);
            this.btnTamam.Name = "btnTamam";
            this.btnTamam.Size = new System.Drawing.Size(171, 45);
            this.btnTamam.TabIndex = 0;
            this.btnTamam.Text = "Tamam";
            this.btnTamam.Click += new System.EventHandler(this.btnTamam_Click);
            // 
            // timerBlink
            // 
            this.timerBlink.Interval = 500;
            this.timerBlink.Tick += new System.EventHandler(this.timerBlink_Tick);
            // 
            // FormGecikmeBorc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 390);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlButtons);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormGecikmeBorc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gecikmiş Borç Ekranı";
            this.pnlHeader.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.warningIconPictureBox)).EndInit();
            this.pnlCariBaslik.ResumeLayout(false);
            this.pnlBilgiler.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.pnlButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel pnlHeader;
        public System.Windows.Forms.Label lblTitle;
        public Guna.UI2.WinForms.Guna2Button btnClose;
        public Guna.UI2.WinForms.Guna2Button btnInfo;
        public System.Windows.Forms.Panel pnlMain;
        public System.Windows.Forms.Panel pnlButtons;
        public Guna.UI2.WinForms.Guna2Button btnTahsilat;
        public Guna.UI2.WinForms.Guna2Button btnTamam;
        public System.Windows.Forms.Panel pnlBilgiler;
        public System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Label lblBakiye;
        public System.Windows.Forms.Label lblBakiyeBaslik;
        public System.Windows.Forms.Panel panel4;
        public System.Windows.Forms.Label lblFaturaTarihi;
        public System.Windows.Forms.Label lblFaturaTarihiBaslik;
        public System.Windows.Forms.Panel panel3;
        public System.Windows.Forms.Label lblVade;
        public System.Windows.Forms.Label lblVadeBaslik;
        public System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Label lblGecikmisBorc;
        public System.Windows.Forms.Label lblGecikmisBaslik;
        public System.Windows.Forms.Panel pnlCariBaslik;
        public System.Windows.Forms.Label lblCariUnvan;
        public System.Windows.Forms.Label lblCariBaslik;
        public System.Windows.Forms.PictureBox warningIconPictureBox;
        public System.Windows.Forms.Timer timerBlink;
    }
}