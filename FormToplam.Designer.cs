namespace Veresiye2025
{
    partial class FormToplam
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormToplam));
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.btnHeaderClose = new Guna.UI2.WinForms.Guna2Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.lblGeneralStatus = new System.Windows.Forms.Label();
            this.lblVadesiGecenUyari = new System.Windows.Forms.Label();
            this.panelToplamlar = new System.Windows.Forms.Panel();
            this.groupGenel = new System.Windows.Forms.GroupBox();
            this.lblGenelBorcluLabel = new System.Windows.Forms.Label();
            this.lblGenelBorclu = new System.Windows.Forms.Label();
            this.lblGenelAlacakliLabel = new System.Windows.Forms.Label();
            this.lblGenelAlacakli = new System.Windows.Forms.Label();
            this.lblVadesiGecenToplamLabel = new System.Windows.Forms.Label();
            this.lblVadesiGecenToplam = new System.Windows.Forms.Label();
            this.groupPasif = new System.Windows.Forms.GroupBox();
            this.lblPasifBorcluLabel = new System.Windows.Forms.Label();
            this.lblPasifBorclu = new System.Windows.Forms.Label();
            this.lblPasifAlacakliLabel = new System.Windows.Forms.Label();
            this.lblPasifAlacakli = new System.Windows.Forms.Label();
            this.groupAktif = new System.Windows.Forms.GroupBox();
            this.lblAktifBorcluLabel = new System.Windows.Forms.Label();
            this.lblAktifBorclu = new System.Windows.Forms.Label();
            this.lblAktifAlacakliLabel = new System.Windows.Forms.Label();
            this.lblAktifAlacakli = new System.Windows.Forms.Label();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnExport = new Guna.UI2.WinForms.Guna2Button();
            this.btnKapat = new Guna.UI2.WinForms.Guna2Button();
            this.pnlHeader.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.panelToplamlar.SuspendLayout();
            this.groupGenel.SuspendLayout();
            this.groupPasif.SuspendLayout();
            this.groupAktif.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(141)))), ((int)(((byte)(243)))));
            this.pnlHeader.Controls.Add(this.btnHeaderClose);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(400, 50);
            this.pnlHeader.TabIndex = 0;
            // 
            // btnHeaderClose
            // 
            this.btnHeaderClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHeaderClose.BackColor = System.Drawing.Color.Transparent;
            this.btnHeaderClose.BorderRadius = 15;
            this.btnHeaderClose.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(17)))), ((int)(((byte)(35)))));
            this.btnHeaderClose.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnHeaderClose.ForeColor = System.Drawing.Color.White;
            this.btnHeaderClose.Location = new System.Drawing.Point(352, 5);
            this.btnHeaderClose.Name = "btnHeaderClose";
            this.btnHeaderClose.Size = new System.Drawing.Size(40, 40);
            this.btnHeaderClose.TabIndex = 1;
            this.btnHeaderClose.Text = "X";
            this.btnHeaderClose.UseTransparentBackground = true;
            this.btnHeaderClose.Click += new System.EventHandler(this.btnHeaderClose_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblTitle.Size = new System.Drawing.Size(400, 50);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "BORÇ / ALACAK TOPLAMLAR";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.White;
            this.pnlMain.Controls.Add(this.lblGeneralStatus);
            this.pnlMain.Controls.Add(this.lblVadesiGecenUyari);
            this.pnlMain.Controls.Add(this.panelToplamlar);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 50);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(15);
            this.pnlMain.Size = new System.Drawing.Size(400, 408);
            this.pnlMain.TabIndex = 1;
            // 
            // lblGeneralStatus
            // 
            this.lblGeneralStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGeneralStatus.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblGeneralStatus.ForeColor = System.Drawing.Color.Blue;
            this.lblGeneralStatus.Location = new System.Drawing.Point(15, 15);
            this.lblGeneralStatus.Name = "lblGeneralStatus";
            this.lblGeneralStatus.Size = new System.Drawing.Size(370, 23);
            this.lblGeneralStatus.TabIndex = 2;
            this.lblGeneralStatus.Text = "Genel Durum: Dengede";
            this.lblGeneralStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblVadesiGecenUyari
            // 
            this.lblVadesiGecenUyari.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblVadesiGecenUyari.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.lblVadesiGecenUyari.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblVadesiGecenUyari.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblVadesiGecenUyari.ForeColor = System.Drawing.Color.Red;
            this.lblVadesiGecenUyari.Location = new System.Drawing.Point(12, 370);
            this.lblVadesiGecenUyari.Name = "lblVadesiGecenUyari";
            this.lblVadesiGecenUyari.Size = new System.Drawing.Size(370, 23);
            this.lblVadesiGecenUyari.TabIndex = 1;
            this.lblVadesiGecenUyari.Text = "Dikkat! Vadesi geçmiş borçlar mevcut!";
            this.lblVadesiGecenUyari.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblVadesiGecenUyari.Visible = false;
            // 
            // panelToplamlar
            // 
            this.panelToplamlar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelToplamlar.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelToplamlar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelToplamlar.Controls.Add(this.groupGenel);
            this.panelToplamlar.Controls.Add(this.groupPasif);
            this.panelToplamlar.Controls.Add(this.groupAktif);
            this.panelToplamlar.Location = new System.Drawing.Point(15, 45);
            this.panelToplamlar.Name = "panelToplamlar";
            this.panelToplamlar.Padding = new System.Windows.Forms.Padding(10);
            this.panelToplamlar.Size = new System.Drawing.Size(370, 322);
            this.panelToplamlar.TabIndex = 0;
            // 
            // groupGenel
            // 
            this.groupGenel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupGenel.Controls.Add(this.lblGenelBorcluLabel);
            this.groupGenel.Controls.Add(this.lblGenelBorclu);
            this.groupGenel.Controls.Add(this.lblGenelAlacakliLabel);
            this.groupGenel.Controls.Add(this.lblGenelAlacakli);
            this.groupGenel.Controls.Add(this.lblVadesiGecenToplamLabel);
            this.groupGenel.Controls.Add(this.lblVadesiGecenToplam);
            this.groupGenel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupGenel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(141)))), ((int)(((byte)(243)))));
            this.groupGenel.Location = new System.Drawing.Point(10, 215);
            this.groupGenel.Name = "groupGenel";
            this.groupGenel.Size = new System.Drawing.Size(345, 85);
            this.groupGenel.TabIndex = 2;
            this.groupGenel.TabStop = false;
            this.groupGenel.Text = "Genel Toplamlar";
            // 
            // lblGenelBorcluLabel
            // 
            this.lblGenelBorcluLabel.AutoSize = true;
            this.lblGenelBorcluLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblGenelBorcluLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblGenelBorcluLabel.Location = new System.Drawing.Point(6, 25);
            this.lblGenelBorcluLabel.Name = "lblGenelBorcluLabel";
            this.lblGenelBorcluLabel.Size = new System.Drawing.Size(118, 20);
            this.lblGenelBorcluLabel.TabIndex = 0;
            this.lblGenelBorcluLabel.Text = "Borçlu Toplamı:";
            // 
            // lblGenelBorclu
            // 
            this.lblGenelBorclu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGenelBorclu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblGenelBorclu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblGenelBorclu.Location = new System.Drawing.Point(239, 25);
            this.lblGenelBorclu.Name = "lblGenelBorclu";
            this.lblGenelBorclu.Size = new System.Drawing.Size(100, 20);
            this.lblGenelBorclu.TabIndex = 1;
            this.lblGenelBorclu.Text = "₺0,00";
            this.lblGenelBorclu.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblGenelAlacakliLabel
            // 
            this.lblGenelAlacakliLabel.AutoSize = true;
            this.lblGenelAlacakliLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblGenelAlacakliLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblGenelAlacakliLabel.Location = new System.Drawing.Point(6, 45);
            this.lblGenelAlacakliLabel.Name = "lblGenelAlacakliLabel";
            this.lblGenelAlacakliLabel.Size = new System.Drawing.Size(127, 20);
            this.lblGenelAlacakliLabel.TabIndex = 2;
            this.lblGenelAlacakliLabel.Text = "Alacaklı Toplamı:";
            // 
            // lblGenelAlacakli
            // 
            this.lblGenelAlacakli.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGenelAlacakli.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblGenelAlacakli.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblGenelAlacakli.Location = new System.Drawing.Point(239, 45);
            this.lblGenelAlacakli.Name = "lblGenelAlacakli";
            this.lblGenelAlacakli.Size = new System.Drawing.Size(100, 20);
            this.lblGenelAlacakli.TabIndex = 3;
            this.lblGenelAlacakli.Text = "₺0,00";
            this.lblGenelAlacakli.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblVadesiGecenToplamLabel
            // 
            this.lblVadesiGecenToplamLabel.AutoSize = true;
            this.lblVadesiGecenToplamLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblVadesiGecenToplamLabel.ForeColor = System.Drawing.Color.Red;
            this.lblVadesiGecenToplamLabel.Location = new System.Drawing.Point(6, 65);
            this.lblVadesiGecenToplamLabel.Name = "lblVadesiGecenToplamLabel";
            this.lblVadesiGecenToplamLabel.Size = new System.Drawing.Size(161, 20);
            this.lblVadesiGecenToplamLabel.TabIndex = 4;
            this.lblVadesiGecenToplamLabel.Text = "Vadesi Geçen Toplam:";
            // 
            // lblVadesiGecenToplam
            // 
            this.lblVadesiGecenToplam.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblVadesiGecenToplam.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblVadesiGecenToplam.ForeColor = System.Drawing.Color.Red;
            this.lblVadesiGecenToplam.Location = new System.Drawing.Point(239, 65);
            this.lblVadesiGecenToplam.Name = "lblVadesiGecenToplam";
            this.lblVadesiGecenToplam.Size = new System.Drawing.Size(100, 20);
            this.lblVadesiGecenToplam.TabIndex = 5;
            this.lblVadesiGecenToplam.Text = "₺0,00";
            this.lblVadesiGecenToplam.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupPasif
            // 
            this.groupPasif.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupPasif.Controls.Add(this.lblPasifBorcluLabel);
            this.groupPasif.Controls.Add(this.lblPasifBorclu);
            this.groupPasif.Controls.Add(this.lblPasifAlacakliLabel);
            this.groupPasif.Controls.Add(this.lblPasifAlacakli);
            this.groupPasif.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupPasif.ForeColor = System.Drawing.Color.Red;
            this.groupPasif.Location = new System.Drawing.Point(10, 107);
            this.groupPasif.Name = "groupPasif";
            this.groupPasif.Size = new System.Drawing.Size(345, 70);
            this.groupPasif.TabIndex = 1;
            this.groupPasif.TabStop = false;
            this.groupPasif.Text = "Pasif Toplamlar";
            // 
            // lblPasifBorcluLabel
            // 
            this.lblPasifBorcluLabel.AutoSize = true;
            this.lblPasifBorcluLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblPasifBorcluLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblPasifBorcluLabel.Location = new System.Drawing.Point(6, 25);
            this.lblPasifBorcluLabel.Name = "lblPasifBorcluLabel";
            this.lblPasifBorcluLabel.Size = new System.Drawing.Size(112, 20);
            this.lblPasifBorcluLabel.TabIndex = 0;
            this.lblPasifBorcluLabel.Text = "Borçlu Toplamı:";
            // 
            // lblPasifBorclu
            // 
            this.lblPasifBorclu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPasifBorclu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblPasifBorclu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblPasifBorclu.Location = new System.Drawing.Point(239, 25);
            this.lblPasifBorclu.Name = "lblPasifBorclu";
            this.lblPasifBorclu.Size = new System.Drawing.Size(100, 20);
            this.lblPasifBorclu.TabIndex = 1;
            this.lblPasifBorclu.Text = "₺0,00";
            this.lblPasifBorclu.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPasifAlacakliLabel
            // 
            this.lblPasifAlacakliLabel.AutoSize = true;
            this.lblPasifAlacakliLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblPasifAlacakliLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblPasifAlacakliLabel.Location = new System.Drawing.Point(6, 45);
            this.lblPasifAlacakliLabel.Name = "lblPasifAlacakliLabel";
            this.lblPasifAlacakliLabel.Size = new System.Drawing.Size(122, 20);
            this.lblPasifAlacakliLabel.TabIndex = 2;
            this.lblPasifAlacakliLabel.Text = "Alacaklı Toplamı:";
            // 
            // lblPasifAlacakli
            // 
            this.lblPasifAlacakli.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPasifAlacakli.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblPasifAlacakli.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblPasifAlacakli.Location = new System.Drawing.Point(239, 45);
            this.lblPasifAlacakli.Name = "lblPasifAlacakli";
            this.lblPasifAlacakli.Size = new System.Drawing.Size(100, 20);
            this.lblPasifAlacakli.TabIndex = 3;
            this.lblPasifAlacakli.Text = "₺0,00";
            this.lblPasifAlacakli.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupAktif
            // 
            this.groupAktif.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupAktif.Controls.Add(this.lblAktifBorcluLabel);
            this.groupAktif.Controls.Add(this.lblAktifBorclu);
            this.groupAktif.Controls.Add(this.lblAktifAlacakliLabel);
            this.groupAktif.Controls.Add(this.lblAktifAlacakli);
            this.groupAktif.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupAktif.ForeColor = System.Drawing.Color.Green;
            this.groupAktif.Location = new System.Drawing.Point(10, 9);
            this.groupAktif.Name = "groupAktif";
            this.groupAktif.Size = new System.Drawing.Size(345, 70);
            this.groupAktif.TabIndex = 0;
            this.groupAktif.TabStop = false;
            this.groupAktif.Text = "Aktif Toplamlar";
            // 
            // lblAktifBorcluLabel
            // 
            this.lblAktifBorcluLabel.AutoSize = true;
            this.lblAktifBorcluLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblAktifBorcluLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblAktifBorcluLabel.Location = new System.Drawing.Point(6, 25);
            this.lblAktifBorcluLabel.Name = "lblAktifBorcluLabel";
            this.lblAktifBorcluLabel.Size = new System.Drawing.Size(112, 20);
            this.lblAktifBorcluLabel.TabIndex = 0;
            this.lblAktifBorcluLabel.Text = "Borçlu Toplamı:";
            // 
            // lblAktifBorclu
            // 
            this.lblAktifBorclu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAktifBorclu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblAktifBorclu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblAktifBorclu.Location = new System.Drawing.Point(239, 25);
            this.lblAktifBorclu.Name = "lblAktifBorclu";
            this.lblAktifBorclu.Size = new System.Drawing.Size(100, 20);
            this.lblAktifBorclu.TabIndex = 1;
            this.lblAktifBorclu.Text = "₺0,00";
            this.lblAktifBorclu.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblAktifAlacakliLabel
            // 
            this.lblAktifAlacakliLabel.AutoSize = true;
            this.lblAktifAlacakliLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblAktifAlacakliLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblAktifAlacakliLabel.Location = new System.Drawing.Point(6, 45);
            this.lblAktifAlacakliLabel.Name = "lblAktifAlacakliLabel";
            this.lblAktifAlacakliLabel.Size = new System.Drawing.Size(122, 20);
            this.lblAktifAlacakliLabel.TabIndex = 2;
            this.lblAktifAlacakliLabel.Text = "Alacaklı Toplamı:";
            // 
            // lblAktifAlacakli
            // 
            this.lblAktifAlacakli.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAktifAlacakli.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblAktifAlacakli.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblAktifAlacakli.Location = new System.Drawing.Point(239, 45);
            this.lblAktifAlacakli.Name = "lblAktifAlacakli";
            this.lblAktifAlacakli.Size = new System.Drawing.Size(100, 20);
            this.lblAktifAlacakli.TabIndex = 3;
            this.lblAktifAlacakli.Text = "₺0,00";
            this.lblAktifAlacakli.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlButtons
            // 
            this.pnlButtons.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlButtons.Controls.Add(this.btnExport);
            this.pnlButtons.Controls.Add(this.btnKapat);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.Location = new System.Drawing.Point(0, 458);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(400, 60);
            this.pnlButtons.TabIndex = 2;
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.BorderRadius = 5;
            this.btnExport.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnExport.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnExport.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnExport.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnExport.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(138)))), ((int)(((byte)(0)))));
            this.btnExport.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnExport.ForeColor = System.Drawing.Color.White;
            this.btnExport.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnExport.Location = new System.Drawing.Point(264, 10);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(120, 40);
            this.btnExport.TabIndex = 1;
            this.btnExport.Text = "Rapor Al";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnKapat
            // 
            this.btnKapat.BorderRadius = 5;
            this.btnKapat.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnKapat.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnKapat.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnKapat.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnKapat.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(141)))), ((int)(((byte)(243)))));
            this.btnKapat.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnKapat.ForeColor = System.Drawing.Color.White;
            this.btnKapat.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnKapat.Location = new System.Drawing.Point(15, 10);
            this.btnKapat.Name = "btnKapat";
            this.btnKapat.Size = new System.Drawing.Size(120, 40);
            this.btnKapat.TabIndex = 0;
            this.btnKapat.Text = "Kapat";
            this.btnKapat.Click += new System.EventHandler(this.btnKapat_Click);
            // 
            // FormToplam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 518);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlButtons);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormToplam";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Liste Toplamları";
            this.pnlHeader.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.panelToplamlar.ResumeLayout(false);
            this.groupGenel.ResumeLayout(false);
            this.groupGenel.PerformLayout();
            this.groupPasif.ResumeLayout(false);
            this.groupPasif.PerformLayout();
            this.groupAktif.ResumeLayout(false);
            this.groupAktif.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel pnlHeader;
        public System.Windows.Forms.Label lblTitle;
        public Guna.UI2.WinForms.Guna2Button btnHeaderClose;
        public System.Windows.Forms.Panel pnlMain;
        public System.Windows.Forms.Panel pnlButtons;
        public Guna.UI2.WinForms.Guna2Button btnKapat;
        public Guna.UI2.WinForms.Guna2Button btnExport;
        public System.Windows.Forms.Panel panelToplamlar;
        public System.Windows.Forms.GroupBox groupAktif;
        public System.Windows.Forms.Label lblAktifBorcluLabel;
        public System.Windows.Forms.Label lblAktifBorclu;
        public System.Windows.Forms.Label lblAktifAlacakliLabel;
        public System.Windows.Forms.Label lblAktifAlacakli;
        public System.Windows.Forms.GroupBox groupPasif;
        public System.Windows.Forms.Label lblPasifBorcluLabel;
        public System.Windows.Forms.Label lblPasifBorclu;
        public System.Windows.Forms.Label lblPasifAlacakliLabel;
        public System.Windows.Forms.Label lblPasifAlacakli;
        public System.Windows.Forms.GroupBox groupGenel;
        public System.Windows.Forms.Label lblGenelBorcluLabel;
        public System.Windows.Forms.Label lblGenelBorclu;
        public System.Windows.Forms.Label lblGenelAlacakliLabel;
        public System.Windows.Forms.Label lblGenelAlacakli;
        public System.Windows.Forms.Label lblVadesiGecenToplamLabel;
        public System.Windows.Forms.Label lblVadesiGecenToplam;
        public System.Windows.Forms.Label lblVadesiGecenUyari;
        public System.Windows.Forms.Label lblGeneralStatus;
    }
}