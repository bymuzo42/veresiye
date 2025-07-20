namespace Veresiye2025
{
    partial class FormHataMesaji
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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.btnClose = new Guna.UI2.WinForms.Guna2Button();
            this.lblBaslik = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.lblOneri = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblMaxEklenebilir = new System.Windows.Forms.Label();
            this.lblEklenmekIstenen = new System.Windows.Forms.Label();
            this.lblMevcutBorc = new System.Windows.Forms.Label();
            this.lblCariLimit = new System.Windows.Forms.Label();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnTamam = new Guna.UI2.WinForms.Guna2Button();
            this.timerAnimation = new System.Windows.Forms.Timer(this.components);
            this.iconPictureBox = new System.Windows.Forms.PictureBox();
            this.pnlHeader.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(17)))), ((int)(((byte)(35)))));
            this.pnlHeader.Controls.Add(this.btnClose);
            this.pnlHeader.Controls.Add(this.lblBaslik);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(512, 50);
            this.pnlHeader.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.BorderRadius = 15;
            this.btnClose.FillColor = System.Drawing.Color.Transparent;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(463, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(46, 50);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "X";
            this.btnClose.UseTransparentBackground = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblBaslik
            // 
            this.lblBaslik.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBaslik.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblBaslik.ForeColor = System.Drawing.Color.White;
            this.lblBaslik.Location = new System.Drawing.Point(0, 0);
            this.lblBaslik.Name = "lblBaslik";
            this.lblBaslik.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblBaslik.Size = new System.Drawing.Size(512, 50);
            this.lblBaslik.TabIndex = 0;
            this.lblBaslik.Text = "CARİ LİMİT AŞILDI!";
            this.lblBaslik.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.White;
            this.pnlMain.Controls.Add(this.iconPictureBox);
            this.pnlMain.Controls.Add(this.lblOneri);
            this.pnlMain.Controls.Add(this.panel1);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 50);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(15);
            this.pnlMain.Size = new System.Drawing.Size(512, 235);
            this.pnlMain.TabIndex = 1;
            // 
            // lblOneri
            // 
            this.lblOneri.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOneri.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.lblOneri.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblOneri.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblOneri.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(17)))), ((int)(((byte)(35)))));
            this.lblOneri.Location = new System.Drawing.Point(18, 182);
            this.lblOneri.Name = "lblOneri";
            this.lblOneri.Padding = new System.Windows.Forms.Padding(5);
            this.lblOneri.Size = new System.Drawing.Size(476, 40);
            this.lblOneri.TabIndex = 1;
            this.lblOneri.Text = "En fazla 1.000,00 ₺ tutarında borç ekleyebilirsiniz.";
            this.lblOneri.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblMaxEklenebilir);
            this.panel1.Controls.Add(this.lblEklenmekIstenen);
            this.panel1.Controls.Add(this.lblMevcutBorc);
            this.panel1.Controls.Add(this.lblCariLimit);
            this.panel1.Location = new System.Drawing.Point(84, 15);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(410, 156);
            this.panel1.TabIndex = 0;
            // 
            // lblMaxEklenebilir
            // 
            this.lblMaxEklenebilir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMaxEklenebilir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblMaxEklenebilir.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMaxEklenebilir.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblMaxEklenebilir.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(141)))), ((int)(((byte)(243)))));
            this.lblMaxEklenebilir.Location = new System.Drawing.Point(10, 120);
            this.lblMaxEklenebilir.Name = "lblMaxEklenebilir";
            this.lblMaxEklenebilir.Size = new System.Drawing.Size(388, 28);
            this.lblMaxEklenebilir.TabIndex = 3;
            this.lblMaxEklenebilir.Text = "Eklenebilecek Maksimum Borç: 1.000,00 ₺";
            this.lblMaxEklenebilir.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEklenmekIstenen
            // 
            this.lblEklenmekIstenen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEklenmekIstenen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblEklenmekIstenen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblEklenmekIstenen.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblEklenmekIstenen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(153)))), ((int)(((byte)(78)))));
            this.lblEklenmekIstenen.Location = new System.Drawing.Point(10, 85);
            this.lblEklenmekIstenen.Name = "lblEklenmekIstenen";
            this.lblEklenmekIstenen.Size = new System.Drawing.Size(388, 28);
            this.lblEklenmekIstenen.TabIndex = 2;
            this.lblEklenmekIstenen.Text = "Eklenmek İstenen Borç: 1.500,00 ₺";
            this.lblEklenmekIstenen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMevcutBorc
            // 
            this.lblMevcutBorc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMevcutBorc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblMevcutBorc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMevcutBorc.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblMevcutBorc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(17)))), ((int)(((byte)(35)))));
            this.lblMevcutBorc.Location = new System.Drawing.Point(10, 50);
            this.lblMevcutBorc.Name = "lblMevcutBorc";
            this.lblMevcutBorc.Size = new System.Drawing.Size(388, 28);
            this.lblMevcutBorc.TabIndex = 1;
            this.lblMevcutBorc.Text = "Mevcut Borç: 4.000,00 ₺";
            this.lblMevcutBorc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCariLimit
            // 
            this.lblCariLimit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCariLimit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblCariLimit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCariLimit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblCariLimit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(141)))), ((int)(((byte)(243)))));
            this.lblCariLimit.Location = new System.Drawing.Point(10, 15);
            this.lblCariLimit.Name = "lblCariLimit";
            this.lblCariLimit.Size = new System.Drawing.Size(388, 28);
            this.lblCariLimit.TabIndex = 0;
            this.lblCariLimit.Text = "Cari Limit: 5.000,00 ₺";
            this.lblCariLimit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlButtons
            // 
            this.pnlButtons.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlButtons.Controls.Add(this.btnTamam);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.Location = new System.Drawing.Point(0, 285);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Padding = new System.Windows.Forms.Padding(10);
            this.pnlButtons.Size = new System.Drawing.Size(512, 65);
            this.pnlButtons.TabIndex = 2;
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
            this.btnTamam.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnTamam.ForeColor = System.Drawing.Color.White;
            this.btnTamam.Location = new System.Drawing.Point(203, 8);
            this.btnTamam.Name = "btnTamam";
            this.btnTamam.Size = new System.Drawing.Size(120, 45);
            this.btnTamam.TabIndex = 0;
            this.btnTamam.Text = "Tamam";
            this.btnTamam.Click += new System.EventHandler(this.btnTamam_Click);
            // 
            // timerAnimation
            // 
            this.timerAnimation.Interval = 500;
            this.timerAnimation.Tick += new System.EventHandler(this.timerAnimation_Tick);
            // 
            // iconPictureBox
            // 
            this.iconPictureBox.Image = global::Veresiye2025.Properties.Resources.warning2_32;
            this.iconPictureBox.Location = new System.Drawing.Point(12, 51);
            this.iconPictureBox.Name = "iconPictureBox";
            this.iconPictureBox.Size = new System.Drawing.Size(60, 63);
            this.iconPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.iconPictureBox.TabIndex = 2;
            this.iconPictureBox.TabStop = false;
            // 
            // FormHataMesaji
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 350);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlButtons);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormHataMesaji";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hata Mesajı";
            this.pnlHeader.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.pnlButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel pnlHeader;
        public System.Windows.Forms.Label lblBaslik;
        public Guna.UI2.WinForms.Guna2Button btnClose;
        public System.Windows.Forms.Panel pnlMain;
        public System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Label lblCariLimit;
        public System.Windows.Forms.Label lblMevcutBorc;
        public System.Windows.Forms.Label lblEklenmekIstenen;
        public System.Windows.Forms.Label lblMaxEklenebilir;
        public System.Windows.Forms.Panel pnlButtons;
        public Guna.UI2.WinForms.Guna2Button btnTamam;
        public System.Windows.Forms.Label lblOneri;
        public System.Windows.Forms.PictureBox iconPictureBox;
        public System.Windows.Forms.Timer timerAnimation;
    }
}