using System.Drawing;
using System.Drawing.Drawing2D;

namespace Veresiye2025
{
    partial class Form1
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
            this.loginPanel = new System.Windows.Forms.Panel();
            this.txtKullaniciAdi = new Guna.UI2.WinForms.Guna2TextBox();  // ✅ Doğrudan tanımlama
            this.txtSifre = new Guna.UI2.WinForms.Guna2TextBox();        // ✅ Doğrudan tanımlama
            this.hatirla = new System.Windows.Forms.CheckBox();
            this.goster = new System.Windows.Forms.CheckBox();
            this.forgotPasswordLink = new System.Windows.Forms.LinkLabel();
            this.btnGirisYap = new System.Windows.Forms.Button();
            this.btnKayitOl = new System.Windows.Forms.Button();
            this.btnSifreDegistir = new System.Windows.Forms.Button();
            this.loginProgressBar = new System.Windows.Forms.ProgressBar();
            this.titleBar = new System.Windows.Forms.Panel();
            this.closeButton = new System.Windows.Forms.Button();
            this.minimizeButton = new System.Windows.Forms.Button();
            this.lblFormTitle = new System.Windows.Forms.Label();
            this.logoPictureBox = new System.Windows.Forms.PictureBox();
            this.btnTheme = new System.Windows.Forms.Button();
            this.languageButton = new System.Windows.Forms.Button();
            this.statusLabel = new System.Windows.Forms.Label();
            this.timerEngel = new System.Windows.Forms.Timer(this.components);
            this.contextMenuLanguage = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItemTurkce = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemEnglish = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.loginPanel.SuspendLayout();
            this.titleBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).BeginInit();
            this.contextMenuLanguage.SuspendLayout();
            this.SuspendLayout();
            // 
            // loginPanel
            // 
            this.loginPanel.BackColor = System.Drawing.Color.White;
            this.loginPanel.Controls.Add(this.txtKullaniciAdi);
            this.loginPanel.Controls.Add(this.txtSifre);
            this.loginPanel.Controls.Add(this.hatirla);
            this.loginPanel.Controls.Add(this.goster);
            this.loginPanel.Controls.Add(this.forgotPasswordLink);
            this.loginPanel.Controls.Add(this.btnGirisYap);
            this.loginPanel.Controls.Add(this.btnKayitOl);
            this.loginPanel.Controls.Add(this.btnSifreDegistir);
            this.loginPanel.Controls.Add(this.loginProgressBar);
            this.loginPanel.Location = new System.Drawing.Point(25, 150);
            this.loginPanel.Name = "loginPanel";
            this.loginPanel.Size = new System.Drawing.Size(400, 340);
            this.loginPanel.TabIndex = 4;
            // 
            // txtKullaniciAdi
            // 
            this.txtKullaniciAdi.BorderRadius = 5;
            this.txtKullaniciAdi.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtKullaniciAdi.DefaultText = "";
            this.txtKullaniciAdi.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtKullaniciAdi.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtKullaniciAdi.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtKullaniciAdi.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtKullaniciAdi.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtKullaniciAdi.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtKullaniciAdi.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtKullaniciAdi.Location = new System.Drawing.Point(40, 60);
            this.txtKullaniciAdi.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtKullaniciAdi.Name = "txtKullaniciAdi";
            this.txtKullaniciAdi.PlaceholderText = "Kullanıcı Adınız";
            this.txtKullaniciAdi.SelectedText = "";
            this.txtKullaniciAdi.Size = new System.Drawing.Size(320, 36);
            this.txtKullaniciAdi.TabIndex = 1;
            // 
            // txtSifre
            // 
            this.txtSifre.BorderRadius = 5;
            this.txtSifre.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSifre.DefaultText = "";
            this.txtSifre.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtSifre.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtSifre.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSifre.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSifre.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtSifre.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSifre.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtSifre.Location = new System.Drawing.Point(40, 120);
            this.txtSifre.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSifre.Name = "txtSifre";
            this.txtSifre.PasswordChar = '•';
            this.txtSifre.PlaceholderText = "Şifreniz";
            this.txtSifre.SelectedText = "";
            this.txtSifre.Size = new System.Drawing.Size(320, 36);
            this.txtSifre.TabIndex = 2;
            // 
            // hatirla
            // 
            this.hatirla.AutoSize = true;
            this.hatirla.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.hatirla.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.hatirla.Location = new System.Drawing.Point(40, 160);
            this.hatirla.Name = "hatirla";
            this.hatirla.Size = new System.Drawing.Size(109, 24);
            this.hatirla.TabIndex = 3;
            this.hatirla.Text = "Beni Hatırla";
            this.hatirla.UseVisualStyleBackColor = true;
            // 
            // goster
            // 
            this.goster.AutoSize = true;
            this.goster.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.goster.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.goster.Location = new System.Drawing.Point(156, 160);
            this.goster.Name = "goster";
            this.goster.Size = new System.Drawing.Size(119, 24);
            this.goster.TabIndex = 4;
            this.goster.Text = "Şifreyi Göster";
            this.goster.UseVisualStyleBackColor = true;
            this.goster.CheckedChanged += new System.EventHandler(this.goster_CheckedChanged);
            // 
            // forgotPasswordLink
            // 
            this.forgotPasswordLink.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(86)))), ((int)(((byte)(179)))));
            this.forgotPasswordLink.AutoSize = true;
            this.forgotPasswordLink.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.forgotPasswordLink.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.forgotPasswordLink.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.forgotPasswordLink.Location = new System.Drawing.Point(36, 197);
            this.forgotPasswordLink.Name = "forgotPasswordLink";
            this.forgotPasswordLink.Size = new System.Drawing.Size(117, 20);
            this.forgotPasswordLink.TabIndex = 5;
            this.forgotPasswordLink.TabStop = true;
            this.forgotPasswordLink.Text = "Şifremi Unuttum";
            this.forgotPasswordLink.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.forgotPasswordLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.forgotPasswordLink_LinkClicked);
            // 
            // btnGirisYap
            // 
            this.btnGirisYap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnGirisYap.FlatAppearance.BorderSize = 0;
            this.btnGirisYap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGirisYap.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnGirisYap.ForeColor = System.Drawing.Color.White;
            this.btnGirisYap.Location = new System.Drawing.Point(40, 230);
            this.btnGirisYap.Name = "btnGirisYap";
            this.btnGirisYap.Size = new System.Drawing.Size(145, 45);
            this.btnGirisYap.TabIndex = 6;
            this.btnGirisYap.Text = "Giriş Yap";
            this.btnGirisYap.UseVisualStyleBackColor = false;
            this.btnGirisYap.Click += new System.EventHandler(this.btnGirisYap_Click);
            // 
            // btnKayitOl
            // 
            this.btnKayitOl.BackColor = System.Drawing.Color.Transparent;
            this.btnKayitOl.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnKayitOl.FlatAppearance.BorderSize = 2;
            this.btnKayitOl.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(86)))), ((int)(((byte)(179)))));
            this.btnKayitOl.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.btnKayitOl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKayitOl.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnKayitOl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnKayitOl.Location = new System.Drawing.Point(215, 230);
            this.btnKayitOl.Name = "btnKayitOl";
            this.btnKayitOl.Size = new System.Drawing.Size(145, 45);
            this.btnKayitOl.TabIndex = 7;
            this.btnKayitOl.Text = "Kayıt Ol";
            this.btnKayitOl.UseVisualStyleBackColor = false;
            this.btnKayitOl.Click += new System.EventHandler(this.btnKayitOl_Click);
            // 
            // btnSifreDegistir
            // 
            this.btnSifreDegistir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnSifreDegistir.FlatAppearance.BorderSize = 0;
            this.btnSifreDegistir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSifreDegistir.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnSifreDegistir.ForeColor = System.Drawing.Color.White;
            this.btnSifreDegistir.Location = new System.Drawing.Point(40, 285);
            this.btnSifreDegistir.Name = "btnSifreDegistir";
            this.btnSifreDegistir.Size = new System.Drawing.Size(320, 35);
            this.btnSifreDegistir.TabIndex = 8;
            this.btnSifreDegistir.Text = "Şifre Değiştir";
            this.btnSifreDegistir.UseVisualStyleBackColor = false;
            this.btnSifreDegistir.Click += new System.EventHandler(this.btnSifreDegistir_Click);
            // 
            // loginProgressBar
            // 
            this.loginProgressBar.Location = new System.Drawing.Point(40, 325);
            this.loginProgressBar.Name = "loginProgressBar";
            this.loginProgressBar.Size = new System.Drawing.Size(320, 8);
            this.loginProgressBar.TabIndex = 9;
            this.loginProgressBar.Visible = false;
            // 
            // titleBar
            // 
            this.titleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.titleBar.Controls.Add(this.closeButton);
            this.titleBar.Controls.Add(this.minimizeButton);
            this.titleBar.Controls.Add(this.lblFormTitle);
            this.titleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.titleBar.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.titleBar.Location = new System.Drawing.Point(0, 0);
            this.titleBar.Name = "titleBar";
            this.titleBar.Size = new System.Drawing.Size(450, 40);
            this.titleBar.TabIndex = 0;
            this.titleBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.titleBar_MouseDown);
            this.titleBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.titleBar_MouseMove);
            this.titleBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.titleBar_MouseUp);
            // 
            // closeButton
            // 
            this.closeButton.BackColor = System.Drawing.Color.Transparent;
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeButton.FlatAppearance.BorderSize = 0;
            this.closeButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(17)))), ((int)(((byte)(35)))));
            this.closeButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(17)))), ((int)(((byte)(35)))));
            this.closeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.closeButton.ForeColor = System.Drawing.Color.White;
            this.closeButton.Location = new System.Drawing.Point(410, 0);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(40, 40);
            this.closeButton.TabIndex = 1;
            this.closeButton.Text = "✕";
            this.closeButton.UseVisualStyleBackColor = false;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // minimizeButton
            // 
            this.minimizeButton.BackColor = System.Drawing.Color.Transparent;
            this.minimizeButton.FlatAppearance.BorderSize = 0;
            this.minimizeButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.minimizeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.minimizeButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.minimizeButton.ForeColor = System.Drawing.Color.White;
            this.minimizeButton.Location = new System.Drawing.Point(370, 0);
            this.minimizeButton.Name = "minimizeButton";
            this.minimizeButton.Size = new System.Drawing.Size(40, 40);
            this.minimizeButton.TabIndex = 2;
            this.minimizeButton.Text = "−";
            this.minimizeButton.UseVisualStyleBackColor = false;
            this.minimizeButton.Click += new System.EventHandler(this.minimizeButton_Click);
            // 
            // lblFormTitle
            // 
            this.lblFormTitle.AutoSize = true;
            this.lblFormTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblFormTitle.ForeColor = System.Drawing.Color.White;
            this.lblFormTitle.Location = new System.Drawing.Point(12, 9);
            this.lblFormTitle.Name = "lblFormTitle";
            this.lblFormTitle.Size = new System.Drawing.Size(155, 25);
            this.lblFormTitle.TabIndex = 0;
            this.lblFormTitle.Text = "Veresiye2025 V.1";
            // 
            // logoPictureBox
            // 
            this.logoPictureBox.Image = global::Veresiye2025.Properties.Resources.restore;
            this.logoPictureBox.Location = new System.Drawing.Point(120, 46);
            this.logoPictureBox.Name = "logoPictureBox";
            this.logoPictureBox.Size = new System.Drawing.Size(216, 94);
            this.logoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.logoPictureBox.TabIndex = 3;
            this.logoPictureBox.TabStop = false;
            // 
            // btnTheme
            // 
            this.btnTheme.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.btnTheme.FlatAppearance.BorderSize = 0;
            this.btnTheme.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTheme.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnTheme.ForeColor = System.Drawing.Color.Black;
            this.btnTheme.Location = new System.Drawing.Point(19, 520);
            this.btnTheme.Name = "btnTheme";
            this.btnTheme.Size = new System.Drawing.Size(50, 40);
            this.btnTheme.TabIndex = 10;
            this.btnTheme.Text = "🌙";
            this.btnTheme.UseVisualStyleBackColor = false;
            this.btnTheme.Click += new System.EventHandler(this.btnTheme_Click);
            // 
            // languageButton
            // 
            this.languageButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.languageButton.FlatAppearance.BorderSize = 0;
            this.languageButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.languageButton.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.languageButton.ForeColor = System.Drawing.Color.Black;
            this.languageButton.Location = new System.Drawing.Point(86, 520);
            this.languageButton.Name = "languageButton";
            this.languageButton.Size = new System.Drawing.Size(50, 40);
            this.languageButton.TabIndex = 11;
            this.languageButton.Text = "🌐";
            this.languageButton.UseVisualStyleBackColor = false;
            this.languageButton.Click += new System.EventHandler(this.languageButton_Click);
            // 
            // statusLabel
            // 
            this.statusLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.statusLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.statusLabel.Location = new System.Drawing.Point(25, 501);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(400, 20);
            this.statusLabel.TabIndex = 9;
            this.statusLabel.Text = "Giriş yapmak için bilgilerinizi girin";
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timerEngel
            // 
            this.timerEngel.Interval = 1000;
            this.timerEngel.Tick += new System.EventHandler(this.timerEngel_Tick);
            // 
            // contextMenuLanguage
            // 
            this.contextMenuLanguage.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuLanguage.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemTurkce,
            this.menuItemEnglish});
            this.contextMenuLanguage.Name = "contextMenuLanguage";
            this.contextMenuLanguage.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.contextMenuLanguage.Size = new System.Drawing.Size(126, 52);
            // 
            // menuItemTurkce
            // 
            this.menuItemTurkce.Name = "menuItemTurkce";
            this.menuItemTurkce.Size = new System.Drawing.Size(125, 24);
            this.menuItemTurkce.Text = "Türkçe";
            this.menuItemTurkce.Click += new System.EventHandler(this.menuItemTurkce_Click);
            // 
            // menuItemEnglish
            // 
            this.menuItemEnglish.Name = "menuItemEnglish";
            this.menuItemEnglish.Size = new System.Drawing.Size(125, 24);
            this.menuItemEnglish.Text = "English";
            this.menuItemEnglish.Click += new System.EventHandler(this.menuItemEnglish_Click);
            // 
            // Form1
            // 
            this.AcceptButton = this.btnGirisYap;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.CancelButton = this.closeButton;
            this.ClientSize = new System.Drawing.Size(450, 580);
            this.Controls.Add(this.titleBar);
            this.Controls.Add(this.logoPictureBox);
            this.Controls.Add(this.loginPanel);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.btnTheme);
            this.Controls.Add(this.languageButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Veresiye2025 V.1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.loginPanel.ResumeLayout(false);
            this.loginPanel.PerformLayout();
            this.titleBar.ResumeLayout(false);
            this.titleBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).EndInit();
            this.contextMenuLanguage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel titleBar;
        public System.Windows.Forms.Label lblFormTitle;
        public System.Windows.Forms.Button closeButton;
        public System.Windows.Forms.Button minimizeButton;
        public System.Windows.Forms.Panel loginPanel;
        public System.Windows.Forms.PictureBox logoPictureBox;
        public Guna.UI2.WinForms.Guna2TextBox txtKullaniciAdi;    // ✅ Doğru tanım
        public Guna.UI2.WinForms.Guna2TextBox txtSifre;          // ✅ Doğru tanım
        public System.Windows.Forms.CheckBox hatirla;
        public System.Windows.Forms.CheckBox goster;
        public System.Windows.Forms.LinkLabel forgotPasswordLink;
        public System.Windows.Forms.Button btnGirisYap;
        public System.Windows.Forms.Button btnKayitOl;
        public System.Windows.Forms.Button btnSifreDegistir;
        public System.Windows.Forms.ProgressBar loginProgressBar;
        public System.Windows.Forms.Label statusLabel;
        public System.Windows.Forms.Button btnTheme;
        public System.Windows.Forms.Button languageButton;
        public System.Windows.Forms.Timer timerEngel;
        public System.Windows.Forms.ContextMenuStrip contextMenuLanguage;
        public System.Windows.Forms.ToolStripMenuItem menuItemTurkce;
        public System.Windows.Forms.ToolStripMenuItem menuItemEnglish;
        public System.Windows.Forms.ToolTip toolTip;
    }
}