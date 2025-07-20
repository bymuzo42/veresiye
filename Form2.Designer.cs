using System.ComponentModel;

namespace Veresiye2025
{
    partial class Form2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.formPanel = new System.Windows.Forms.Panel();
            this.statusPanel = new System.Windows.Forms.Panel();
            this.lblPasswordStrength = new System.Windows.Forms.Label();
            this.statusLabel = new System.Windows.Forms.Label();
            this.txtAdSoyad = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtKullaniciAdi = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtSifre = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtEposta = new Guna.UI2.WinForms.Guna2TextBox();
            this.labelAdSoyad = new System.Windows.Forms.Label();
            this.labelKullaniciAdi = new System.Windows.Forms.Label();
            this.labelSifre = new System.Windows.Forms.Label();
            this.labelEposta = new System.Windows.Forms.Label();
            this.kayitolkaydet = new Guna.UI2.WinForms.Guna2Button();
            this.vazgec = new Guna.UI2.WinForms.Guna2Button();
            this.titleBar = new System.Windows.Forms.Panel();
            this.closeButton = new Guna.UI2.WinForms.Guna2ControlBox();
            this.lblFormTitle = new System.Windows.Forms.Label();
            this.formTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.formPanel.SuspendLayout();
            this.statusPanel.SuspendLayout();
            this.titleBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // formPanel
            // 
            this.formPanel.BackColor = System.Drawing.Color.White;
            this.formPanel.Controls.Add(this.statusPanel);
            this.formPanel.Controls.Add(this.txtAdSoyad);
            this.formPanel.Controls.Add(this.txtKullaniciAdi);
            this.formPanel.Controls.Add(this.txtSifre);
            this.formPanel.Controls.Add(this.txtEposta);
            this.formPanel.Controls.Add(this.labelAdSoyad);
            this.formPanel.Controls.Add(this.labelKullaniciAdi);
            this.formPanel.Controls.Add(this.labelSifre);
            this.formPanel.Controls.Add(this.labelEposta);
            this.formPanel.Controls.Add(this.kayitolkaydet);
            this.formPanel.Controls.Add(this.vazgec);
            this.formPanel.Location = new System.Drawing.Point(20, 60);
            this.formPanel.Name = "formPanel";
            this.formPanel.Size = new System.Drawing.Size(400, 320);
            this.formPanel.TabIndex = 1;
            // 
            // statusPanel
            // 
            this.statusPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.statusPanel.Controls.Add(this.lblPasswordStrength);
            this.statusPanel.Controls.Add(this.statusLabel);
            this.statusPanel.Location = new System.Drawing.Point(20, 190);
            this.statusPanel.Name = "statusPanel";
            this.statusPanel.Padding = new System.Windows.Forms.Padding(5);
            this.statusPanel.Size = new System.Drawing.Size(360, 50);
            this.statusPanel.TabIndex = 10;
            // 
            // lblPasswordStrength
            // 
            this.lblPasswordStrength.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblPasswordStrength.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblPasswordStrength.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.lblPasswordStrength.Location = new System.Drawing.Point(5, 25);
            this.lblPasswordStrength.Name = "lblPasswordStrength";
            this.lblPasswordStrength.Size = new System.Drawing.Size(350, 20);
            this.lblPasswordStrength.TabIndex = 1;
            this.lblPasswordStrength.Text = "";
            this.lblPasswordStrength.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // statusLabel
            // 
            this.statusLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.statusLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.statusLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.statusLabel.Location = new System.Drawing.Point(5, 5);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(350, 20);
            this.statusLabel.TabIndex = 0;
            this.statusLabel.Text = "Tüm bilgileri eksiksiz doldurun";
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtAdSoyad
            // 
            this.txtAdSoyad.BorderRadius = 5;
            this.txtAdSoyad.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtAdSoyad.DefaultText = "";
            this.txtAdSoyad.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtAdSoyad.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtAdSoyad.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtAdSoyad.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtAdSoyad.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtAdSoyad.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtAdSoyad.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtAdSoyad.Location = new System.Drawing.Point(150, 15);
            this.txtAdSoyad.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtAdSoyad.Name = "txtAdSoyad";
            this.txtAdSoyad.PlaceholderText = "Adınız ve soyadınız";
            this.txtAdSoyad.SelectedText = "";
            this.txtAdSoyad.Size = new System.Drawing.Size(230, 36);
            this.txtAdSoyad.TabIndex = 0;
            this.formTooltip.SetToolTip(this.txtAdSoyad, "Sadece harf ve boşluk karakteri girebilirsiniz");
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
            this.txtKullaniciAdi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtKullaniciAdi.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtKullaniciAdi.Location = new System.Drawing.Point(150, 60);
            this.txtKullaniciAdi.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtKullaniciAdi.Name = "txtKullaniciAdi";
            this.txtKullaniciAdi.PlaceholderText = "En az 3 karakter";
            this.txtKullaniciAdi.SelectedText = "";
            this.txtKullaniciAdi.Size = new System.Drawing.Size(230, 36);
            this.txtKullaniciAdi.TabIndex = 1;
            this.formTooltip.SetToolTip(this.txtKullaniciAdi, "Harf, rakam ve _ karakteri kullanabilirsiniz. En az 3 karakter");
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
            this.txtSifre.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSifre.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtSifre.Location = new System.Drawing.Point(150, 105);
            this.txtSifre.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSifre.Name = "txtSifre";
            this.txtSifre.PasswordChar = '•';
            this.txtSifre.PlaceholderText = "En az 6 karakter, 1 sayı";
            this.txtSifre.SelectedText = "";
            this.txtSifre.Size = new System.Drawing.Size(230, 36);
            this.txtSifre.TabIndex = 2;
            this.formTooltip.SetToolTip(this.txtSifre, "En az 6 karakter ve en az 1 sayı içermelidir");
            // 
            // txtEposta
            // 
            this.txtEposta.BorderRadius = 5;
            this.txtEposta.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtEposta.DefaultText = "";
            this.txtEposta.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtEposta.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtEposta.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtEposta.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtEposta.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtEposta.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtEposta.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtEposta.Location = new System.Drawing.Point(150, 150);
            this.txtEposta.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtEposta.Name = "txtEposta";
            this.txtEposta.PlaceholderText = "ornek@email.com (opsiyonel)";
            this.txtEposta.SelectedText = "";
            this.txtEposta.Size = new System.Drawing.Size(230, 36);
            this.txtEposta.TabIndex = 3;
            this.formTooltip.SetToolTip(this.txtEposta, "Geçerli bir e-posta adresi giriniz (opsiyonel)");
            // 
            // labelAdSoyad
            // 
            this.labelAdSoyad.AutoSize = true;
            this.labelAdSoyad.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.labelAdSoyad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.labelAdSoyad.Location = new System.Drawing.Point(20, 22);
            this.labelAdSoyad.Name = "labelAdSoyad";
            this.labelAdSoyad.Size = new System.Drawing.Size(96, 23);
            this.labelAdSoyad.TabIndex = 4;
            this.labelAdSoyad.Text = "Ad Soyad:*";
            // 
            // labelKullaniciAdi
            // 
            this.labelKullaniciAdi.AutoSize = true;
            this.labelKullaniciAdi.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.labelKullaniciAdi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.labelKullaniciAdi.Location = new System.Drawing.Point(20, 67);
            this.labelKullaniciAdi.Name = "labelKullaniciAdi";
            this.labelKullaniciAdi.Size = new System.Drawing.Size(116, 23);
            this.labelKullaniciAdi.TabIndex = 5;
            this.labelKullaniciAdi.Text = "Kullanıcı Adı:*";
            // 
            // labelSifre
            // 
            this.labelSifre.AutoSize = true;
            this.labelSifre.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.labelSifre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.labelSifre.Location = new System.Drawing.Point(20, 112);
            this.labelSifre.Name = "labelSifre";
            this.labelSifre.Size = new System.Drawing.Size(57, 23);
            this.labelSifre.TabIndex = 6;
            this.labelSifre.Text = "Şifre:*";
            // 
            // labelEposta
            // 
            this.labelEposta.AutoSize = true;
            this.labelEposta.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.labelEposta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.labelEposta.Location = new System.Drawing.Point(20, 157);
            this.labelEposta.Name = "labelEposta";
            this.labelEposta.Size = new System.Drawing.Size(72, 23);
            this.labelEposta.TabIndex = 7;
            this.labelEposta.Text = "E-posta:";
            // 
            // kayitolkaydet
            // 
            this.kayitolkaydet.BorderRadius = 5;
            this.kayitolkaydet.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.kayitolkaydet.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.kayitolkaydet.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.kayitolkaydet.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.kayitolkaydet.FillColor = System.Drawing.Color.DodgerBlue;
            this.kayitolkaydet.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.kayitolkaydet.ForeColor = System.Drawing.Color.White;
            this.kayitolkaydet.Image = global::Veresiye2025.Properties.Resources.save_icon;
            this.kayitolkaydet.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.kayitolkaydet.Location = new System.Drawing.Point(237, 260);
            this.kayitolkaydet.Name = "kayitolkaydet";
            this.kayitolkaydet.Size = new System.Drawing.Size(144, 45);
            this.kayitolkaydet.TabIndex = 4;
            this.kayitolkaydet.Text = "Kaydet";
            this.kayitolkaydet.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.kayitolkaydet.Click += new System.EventHandler(this.kayitolkaydet_Click);
            // 
            // vazgec
            // 
            this.vazgec.BorderRadius = 5;
            this.vazgec.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.vazgec.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.vazgec.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.vazgec.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.vazgec.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.vazgec.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.vazgec.ForeColor = System.Drawing.Color.White;
            this.vazgec.Image = global::Veresiye2025.Properties.Resources.kapat1;
            this.vazgec.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.vazgec.Location = new System.Drawing.Point(20, 260);
            this.vazgec.Name = "vazgec";
            this.vazgec.Size = new System.Drawing.Size(144, 45);
            this.vazgec.TabIndex = 5;
            this.vazgec.Text = "Vazgeç";
            this.vazgec.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.vazgec.Click += new System.EventHandler(this.vazgec_Click);
            // 
            // titleBar
            // 
            this.titleBar.BackColor = System.Drawing.Color.DodgerBlue;
            this.titleBar.Controls.Add(this.closeButton);
            this.titleBar.Controls.Add(this.lblFormTitle);
            this.titleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.titleBar.Location = new System.Drawing.Point(0, 0);
            this.titleBar.Name = "titleBar";
            this.titleBar.Size = new System.Drawing.Size(440, 40);
            this.titleBar.TabIndex = 0;
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.BackColor = System.Drawing.Color.Transparent;
            this.closeButton.FillColor = System.Drawing.Color.Transparent;
            this.closeButton.HoverState.FillColor = System.Drawing.Color.Red;
            this.closeButton.IconColor = System.Drawing.Color.White;
            this.closeButton.Location = new System.Drawing.Point(384, 0);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(40, 40);
            this.closeButton.TabIndex = 1;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // lblFormTitle
            // 
            this.lblFormTitle.AutoSize = true;
            this.lblFormTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblFormTitle.ForeColor = System.Drawing.Color.White;
            this.lblFormTitle.Location = new System.Drawing.Point(12, 9);
            this.lblFormTitle.Name = "lblFormTitle";
            this.lblFormTitle.Size = new System.Drawing.Size(80, 25);
            this.lblFormTitle.TabIndex = 0;
            this.lblFormTitle.Text = "Kayıt Ol";
            // 
            // formTooltip
            // 
            this.formTooltip.AutoPopDelay = 5000;
            this.formTooltip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.formTooltip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.formTooltip.InitialDelay = 100;
            this.formTooltip.IsBalloon = true;
            this.formTooltip.ReshowDelay = 100;
            this.formTooltip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.formTooltip.ToolTipTitle = "Bilgi";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(440, 400);
            this.Controls.Add(this.formPanel);
            this.Controls.Add(this.titleBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kayıt Ol";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.formPanel.ResumeLayout(false);
            this.formPanel.PerformLayout();
            this.statusPanel.ResumeLayout(false);
            this.titleBar.ResumeLayout(false);
            this.titleBar.PerformLayout();
            this.ResumeLayout(false);
        }
        #endregion

        public System.Windows.Forms.Panel titleBar;
        public System.Windows.Forms.Label lblFormTitle;
        public Guna.UI2.WinForms.Guna2ControlBox closeButton;
        public System.Windows.Forms.Panel formPanel;
        public System.Windows.Forms.Panel statusPanel;
        public System.Windows.Forms.Label statusLabel;
        public System.Windows.Forms.Label lblPasswordStrength;

        public Guna.UI2.WinForms.Guna2TextBox txtAdSoyad;
        public Guna.UI2.WinForms.Guna2TextBox txtKullaniciAdi;
        public Guna.UI2.WinForms.Guna2TextBox txtSifre;
        public Guna.UI2.WinForms.Guna2TextBox txtEposta;

        public System.Windows.Forms.Label labelAdSoyad;
        public System.Windows.Forms.Label labelKullaniciAdi;
        public System.Windows.Forms.Label labelSifre;
        public System.Windows.Forms.Label labelEposta;

        public Guna.UI2.WinForms.Guna2Button kayitolkaydet;
        public Guna.UI2.WinForms.Guna2Button vazgec;
        public System.Windows.Forms.ToolTip formTooltip;
    }
}