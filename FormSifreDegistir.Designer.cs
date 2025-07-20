namespace Veresiye2025
{
    partial class FormSifreDegistir
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSifreDegistir));
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.btnClose = new Guna.UI2.WinForms.Guna2Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.chkShowPassword = new Guna.UI2.WinForms.Guna2CheckBox();
            this.progressBarSifreGuclu = new Guna.UI2.WinForms.Guna2ProgressBar();
            this.lblSifreDurumu = new System.Windows.Forms.Label();
            this.lblYeniSifreTekrar = new System.Windows.Forms.Label();
            this.txtYeniSifreTekrar = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblYeniSifre = new System.Windows.Forms.Label();
            this.txtYeniSifre = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblMevcutSifre = new System.Windows.Forms.Label();
            this.txtMevcutSifre = new Guna.UI2.WinForms.Guna2TextBox();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnVazgec = new Guna.UI2.WinForms.Guna2Button();
            this.btnKaydet = new Guna.UI2.WinForms.Guna2Button();
            this.pnlHeader.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(141)))), ((int)(((byte)(243)))));
            this.pnlHeader.Controls.Add(this.btnClose);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(400, 50);
            this.pnlHeader.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.BorderRadius = 15;
            this.btnClose.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(17)))), ((int)(((byte)(35)))));
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(349, 6);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(40, 40);
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
            this.lblTitle.Size = new System.Drawing.Size(400, 50);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "ŞİFRE DEĞİŞTİR";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.White;
            this.pnlMain.Controls.Add(this.chkShowPassword);
            this.pnlMain.Controls.Add(this.progressBarSifreGuclu);
            this.pnlMain.Controls.Add(this.lblSifreDurumu);
            this.pnlMain.Controls.Add(this.lblYeniSifreTekrar);
            this.pnlMain.Controls.Add(this.txtYeniSifreTekrar);
            this.pnlMain.Controls.Add(this.lblYeniSifre);
            this.pnlMain.Controls.Add(this.txtYeniSifre);
            this.pnlMain.Controls.Add(this.lblMevcutSifre);
            this.pnlMain.Controls.Add(this.txtMevcutSifre);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 50);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(400, 330);
            this.pnlMain.TabIndex = 1;
            // 
            // chkShowPassword
            // 
            this.chkShowPassword.AutoSize = true;
            this.chkShowPassword.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(141)))), ((int)(((byte)(243)))));
            this.chkShowPassword.CheckedState.BorderRadius = 0;
            this.chkShowPassword.CheckedState.BorderThickness = 0;
            this.chkShowPassword.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(141)))), ((int)(((byte)(243)))));
            this.chkShowPassword.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkShowPassword.Location = new System.Drawing.Point(68, 222);
            this.chkShowPassword.Name = "chkShowPassword";
            this.chkShowPassword.Size = new System.Drawing.Size(119, 24);
            this.chkShowPassword.TabIndex = 3;
            this.chkShowPassword.Text = "Şifreyi Göster";
            this.chkShowPassword.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.chkShowPassword.UncheckedState.BorderRadius = 0;
            this.chkShowPassword.UncheckedState.BorderThickness = 0;
            this.chkShowPassword.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            // 
            // progressBarSifreGuclu
            // 
            this.progressBarSifreGuclu.BackColor = System.Drawing.Color.Transparent;
            this.progressBarSifreGuclu.BorderRadius = 5;
            this.progressBarSifreGuclu.FillColor = System.Drawing.Color.LightGray;
            this.progressBarSifreGuclu.Location = new System.Drawing.Point(68, 280);
            this.progressBarSifreGuclu.Name = "progressBarSifreGuclu";
            this.progressBarSifreGuclu.ProgressColor = System.Drawing.Color.Green;
            this.progressBarSifreGuclu.ProgressColor2 = System.Drawing.Color.Green;
            this.progressBarSifreGuclu.Size = new System.Drawing.Size(264, 10);
            this.progressBarSifreGuclu.TabIndex = 4;
            this.progressBarSifreGuclu.Text = "guna2ProgressBar1";
            this.progressBarSifreGuclu.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // lblSifreDurumu
            // 
            this.lblSifreDurumu.AutoSize = true;
            this.lblSifreDurumu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblSifreDurumu.Location = new System.Drawing.Point(68, 254);
            this.lblSifreDurumu.Name = "lblSifreDurumu";
            this.lblSifreDurumu.Size = new System.Drawing.Size(79, 20);
            this.lblSifreDurumu.TabIndex = 7;
            this.lblSifreDurumu.Text = "Şifre Gücü:";
            // 
            // lblYeniSifreTekrar
            // 
            this.lblYeniSifreTekrar.AutoSize = true;
            this.lblYeniSifreTekrar.Location = new System.Drawing.Point(66, 154);
            this.lblYeniSifreTekrar.Name = "lblYeniSifreTekrar";
            this.lblYeniSifreTekrar.Size = new System.Drawing.Size(110, 16);
            this.lblYeniSifreTekrar.TabIndex = 6;
            this.lblYeniSifreTekrar.Text = "Yeni Şifre Tekrar:";
            // 
            // txtYeniSifreTekrar
            // 
            this.txtYeniSifreTekrar.BorderRadius = 10;
            this.txtYeniSifreTekrar.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtYeniSifreTekrar.DefaultText = "";
            this.txtYeniSifreTekrar.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtYeniSifreTekrar.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtYeniSifreTekrar.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtYeniSifreTekrar.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtYeniSifreTekrar.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtYeniSifreTekrar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtYeniSifreTekrar.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtYeniSifreTekrar.Location = new System.Drawing.Point(68, 174);
            this.txtYeniSifreTekrar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtYeniSifreTekrar.Name = "txtYeniSifreTekrar";
            this.txtYeniSifreTekrar.PlaceholderText = "Yeni şifrenizi tekrar girin";
            this.txtYeniSifreTekrar.SelectedText = "";
            this.txtYeniSifreTekrar.Size = new System.Drawing.Size(264, 42);
            this.txtYeniSifreTekrar.TabIndex = 2;
            // 
            // lblYeniSifre
            // 
            this.lblYeniSifre.AutoSize = true;
            this.lblYeniSifre.Location = new System.Drawing.Point(66, 87);
            this.lblYeniSifre.Name = "lblYeniSifre";
            this.lblYeniSifre.Size = new System.Drawing.Size(67, 16);
            this.lblYeniSifre.TabIndex = 4;
            this.lblYeniSifre.Text = "Yeni Şifre:";
            // 
            // txtYeniSifre
            // 
            this.txtYeniSifre.BorderRadius = 10;
            this.txtYeniSifre.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtYeniSifre.DefaultText = "";
            this.txtYeniSifre.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtYeniSifre.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtYeniSifre.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtYeniSifre.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtYeniSifre.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtYeniSifre.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtYeniSifre.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtYeniSifre.Location = new System.Drawing.Point(68, 107);
            this.txtYeniSifre.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtYeniSifre.Name = "txtYeniSifre";
            this.txtYeniSifre.PlaceholderText = "Yeni şifrenizi girin";
            this.txtYeniSifre.SelectedText = "";
            this.txtYeniSifre.Size = new System.Drawing.Size(264, 42);
            this.txtYeniSifre.TabIndex = 1;
            // 
            // lblMevcutSifre
            // 
            this.lblMevcutSifre.AutoSize = true;
            this.lblMevcutSifre.Location = new System.Drawing.Point(66, 20);
            this.lblMevcutSifre.Name = "lblMevcutSifre";
            this.lblMevcutSifre.Size = new System.Drawing.Size(83, 16);
            this.lblMevcutSifre.TabIndex = 2;
            this.lblMevcutSifre.Text = "Mevcut Şifre:";
            // 
            // txtMevcutSifre
            // 
            this.txtMevcutSifre.BorderRadius = 10;
            this.txtMevcutSifre.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMevcutSifre.DefaultText = "";
            this.txtMevcutSifre.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtMevcutSifre.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtMevcutSifre.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMevcutSifre.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMevcutSifre.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtMevcutSifre.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtMevcutSifre.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtMevcutSifre.Location = new System.Drawing.Point(68, 40);
            this.txtMevcutSifre.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtMevcutSifre.Name = "txtMevcutSifre";
            this.txtMevcutSifre.PlaceholderText = "Mevcut şifrenizi girin";
            this.txtMevcutSifre.SelectedText = "";
            this.txtMevcutSifre.Size = new System.Drawing.Size(264, 42);
            this.txtMevcutSifre.TabIndex = 0;
            // 
            // pnlButtons
            // 
            this.pnlButtons.BackColor = System.Drawing.Color.LightGray;
            this.pnlButtons.Controls.Add(this.btnVazgec);
            this.pnlButtons.Controls.Add(this.btnKaydet);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.Location = new System.Drawing.Point(0, 380);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(400, 70);
            this.pnlButtons.TabIndex = 2;
            // 
            // btnVazgec
            // 
            this.btnVazgec.BorderRadius = 10;
            this.btnVazgec.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnVazgec.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnVazgec.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnVazgec.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnVazgec.FillColor = System.Drawing.Color.DimGray;
            this.btnVazgec.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnVazgec.ForeColor = System.Drawing.Color.White;
            this.btnVazgec.Location = new System.Drawing.Point(68, 15);
            this.btnVazgec.Name = "btnVazgec";
            this.btnVazgec.Size = new System.Drawing.Size(90, 40);
            this.btnVazgec.TabIndex = 0;
            this.btnVazgec.Text = "Vazgeç";
            this.btnVazgec.Click += new System.EventHandler(this.btnVazgec_Click);
            // 
            // btnKaydet
            // 
            this.btnKaydet.BorderRadius = 10;
            this.btnKaydet.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnKaydet.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnKaydet.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnKaydet.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnKaydet.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(141)))), ((int)(((byte)(243)))));
            this.btnKaydet.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnKaydet.ForeColor = System.Drawing.Color.White;
            this.btnKaydet.Location = new System.Drawing.Point(164, 15);
            this.btnKaydet.Name = "btnKaydet";
            this.btnKaydet.Size = new System.Drawing.Size(168, 40);
            this.btnKaydet.TabIndex = 1;
            this.btnKaydet.Text = "Şifreyi Değiştir";
            this.btnKaydet.Click += new System.EventHandler(this.btnKaydet_Click);
            // 
            // FormSifreDegistir
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 450);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlButtons);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSifreDegistir";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Şifre Değiştir";
            this.Load += new System.EventHandler(this.FormSifreDegistir_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel pnlHeader;
        public System.Windows.Forms.Label lblTitle;
        public Guna.UI2.WinForms.Guna2Button btnClose;
        public System.Windows.Forms.Panel pnlMain;
        public System.Windows.Forms.Panel pnlButtons;
        public Guna.UI2.WinForms.Guna2Button btnVazgec;
        public Guna.UI2.WinForms.Guna2Button btnKaydet;
        public Guna.UI2.WinForms.Guna2TextBox txtMevcutSifre;
        public System.Windows.Forms.Label lblMevcutSifre;
        public System.Windows.Forms.Label lblYeniSifreTekrar;
        public Guna.UI2.WinForms.Guna2TextBox txtYeniSifreTekrar;
        public System.Windows.Forms.Label lblYeniSifre;
        public Guna.UI2.WinForms.Guna2TextBox txtYeniSifre;
        public Guna.UI2.WinForms.Guna2CheckBox chkShowPassword;
        public Guna.UI2.WinForms.Guna2ProgressBar progressBarSifreGuclu;
        public System.Windows.Forms.Label lblSifreDurumu;
    }
}