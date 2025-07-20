using System.Drawing;
using System.Windows.Forms;
namespace Veresiye2025
{
    
    
    partial class FormGenelAyarlar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGenelAyarlar));
            this.genelayarlar = new System.Windows.Forms.TabControl();
            this.tabgenelayar = new System.Windows.Forms.TabPage();
            this.ayarlarkaydet = new Guna.UI2.WinForms.Guna2Button();
            this.ayarlarvazgec = new Guna.UI2.WinForms.Guna2Button();
            this.eposta = new System.Windows.Forms.TabPage();
            this.epostagunapanel = new Guna.UI2.WinForms.Guna2Panel();
            this.epostagonder = new Guna.UI2.WinForms.Guna2Button();
            this.epostakaydet = new Guna.UI2.WinForms.Guna2Button();
            this.txtSmtpHost = new System.Windows.Forms.TextBox();
            this.txtSmtpPort = new System.Windows.Forms.TextBox();
            this.txtEmailUser = new System.Windows.Forms.TextBox();
            this.txtEmailPassword = new System.Windows.Forms.TextBox();
            this.smsayar = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.temizle = new Guna.UI2.WinForms.Guna2Button();
            this.lstCariLimitAsanlar = new System.Windows.Forms.ListBox();
            this.btnSmsGonder = new Guna.UI2.WinForms.Guna2Button();
            this.btnListedenCikar = new Guna.UI2.WinForms.Guna2Button();
            this.btnCariLimitKontrol = new Guna.UI2.WinForms.Guna2Button();
            this.cmbCariSec = new System.Windows.Forms.ComboBox();
            this.btnCariEkle = new Guna.UI2.WinForms.Guna2Button();
            this.tema = new System.Windows.Forms.TabPage();
            this.dilayar = new System.Windows.Forms.TabPage();
            this.guna2ImageButton1 = new Guna.UI2.WinForms.Guna2ImageButton();
            this.guna2ImageButton2 = new Guna.UI2.WinForms.Guna2ImageButton();
            this.dilikaydet = new Guna.UI2.WinForms.Guna2Button();
            this.dilsec = new Guna.UI2.WinForms.Guna2ComboBox();
            this.kullanici = new System.Windows.Forms.TabPage();
            this.Alarm = new System.Windows.Forms.TabPage();
            this.vade = new System.Windows.Forms.TabPage();
            this.pnlBanner = new System.Windows.Forms.Panel();
            this.lblBanner = new System.Windows.Forms.Label();
            this.genelayarlar.SuspendLayout();
            this.tabgenelayar.SuspendLayout();
            this.eposta.SuspendLayout();
            this.smsayar.SuspendLayout();
            this.dilayar.SuspendLayout();
            this.pnlBanner.SuspendLayout();
            this.SuspendLayout();
            // 
            // genelayarlar
            // 
            this.genelayarlar.Controls.Add(this.tabgenelayar);
            this.genelayarlar.Controls.Add(this.eposta);
            this.genelayarlar.Controls.Add(this.smsayar);
            this.genelayarlar.Controls.Add(this.tema);
            this.genelayarlar.Controls.Add(this.dilayar);
            this.genelayarlar.Controls.Add(this.kullanici);
            this.genelayarlar.Controls.Add(this.Alarm);
            this.genelayarlar.Controls.Add(this.vade);
            this.genelayarlar.Location = new System.Drawing.Point(1, 3);
            this.genelayarlar.Name = "genelayarlar";
            this.genelayarlar.SelectedIndex = 0;
            this.genelayarlar.Size = new System.Drawing.Size(800, 371);
            this.genelayarlar.TabIndex = 0;
            // 
            // tabgenelayar
            // 
            this.tabgenelayar.BackColor = System.Drawing.SystemColors.Control;
            this.tabgenelayar.Controls.Add(this.ayarlarkaydet);
            this.tabgenelayar.Controls.Add(this.ayarlarvazgec);
            this.tabgenelayar.Location = new System.Drawing.Point(4, 25);
            this.tabgenelayar.Name = "tabgenelayar";
            this.tabgenelayar.Padding = new System.Windows.Forms.Padding(3);
            this.tabgenelayar.Size = new System.Drawing.Size(792, 342);
            this.tabgenelayar.TabIndex = 0;
            this.tabgenelayar.Text = "Genel Ayarlar";
            // 
            // ayarlarkaydet
            // 
            this.ayarlarkaydet.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.ayarlarkaydet.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.ayarlarkaydet.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.ayarlarkaydet.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.ayarlarkaydet.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ayarlarkaydet.ForeColor = System.Drawing.Color.White;
            this.ayarlarkaydet.Location = new System.Drawing.Point(412, 246);
            this.ayarlarkaydet.Name = "ayarlarkaydet";
            this.ayarlarkaydet.Size = new System.Drawing.Size(130, 60);
            this.ayarlarkaydet.TabIndex = 4;
            this.ayarlarkaydet.Text = "Kaydet";
            // 
            // ayarlarvazgec
            // 
            this.ayarlarvazgec.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.ayarlarvazgec.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.ayarlarvazgec.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.ayarlarvazgec.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.ayarlarvazgec.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ayarlarvazgec.ForeColor = System.Drawing.Color.White;
            this.ayarlarvazgec.Location = new System.Drawing.Point(247, 246);
            this.ayarlarvazgec.Name = "ayarlarvazgec";
            this.ayarlarvazgec.Size = new System.Drawing.Size(130, 60);
            this.ayarlarvazgec.TabIndex = 3;
            this.ayarlarvazgec.Text = "Vazgeç";
            // 
            // eposta
            // 
            this.eposta.BackColor = System.Drawing.SystemColors.Control;
            this.eposta.Controls.Add(this.epostagunapanel);
            this.eposta.Controls.Add(this.epostagonder);
            this.eposta.Controls.Add(this.epostakaydet);
            this.eposta.Controls.Add(this.txtSmtpHost);
            this.eposta.Controls.Add(this.txtSmtpPort);
            this.eposta.Controls.Add(this.txtEmailUser);
            this.eposta.Controls.Add(this.txtEmailPassword);
            this.eposta.Location = new System.Drawing.Point(4, 25);
            this.eposta.Name = "eposta";
            this.eposta.Padding = new System.Windows.Forms.Padding(3);
            this.eposta.Size = new System.Drawing.Size(792, 342);
            this.eposta.TabIndex = 1;
            this.eposta.Text = "E-Posta Ayarları";
            // 
            // epostagunapanel
            // 
            this.epostagunapanel.Location = new System.Drawing.Point(307, 6);
            this.epostagunapanel.Name = "epostagunapanel";
            this.epostagunapanel.Size = new System.Drawing.Size(478, 330);
            this.epostagunapanel.TabIndex = 1;
            // 
            // epostagonder
            // 
            this.epostagonder.Animated = true;
            this.epostagonder.AnimatedGIF = true;
            this.epostagonder.AutoRoundedCorners = true;
            this.epostagonder.BackColor = System.Drawing.Color.Transparent;
            this.epostagonder.BorderRadius = 20;
            this.epostagonder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.epostagonder.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.epostagonder.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.epostagonder.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.epostagonder.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.epostagonder.FillColor = System.Drawing.Color.DodgerBlue;
            this.epostagonder.FocusedColor = System.Drawing.Color.White;
            this.epostagonder.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.epostagonder.ForeColor = System.Drawing.Color.White;
            this.epostagonder.Image = global::Veresiye2025.Properties.Resources.next_icon2;
            this.epostagonder.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.epostagonder.ImageSize = new System.Drawing.Size(30, 30);
            this.epostagonder.IndicateFocus = true;
            this.epostagonder.Location = new System.Drawing.Point(157, 189);
            this.epostagonder.Name = "epostagonder";
            this.epostagonder.Size = new System.Drawing.Size(144, 43);
            this.epostagonder.TabIndex = 28;
            this.epostagonder.Text = "Gönder";
            this.epostagonder.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.epostagonder.TextFormatNoPrefix = true;
            this.epostagonder.Click += new System.EventHandler(this.epostagonder_Click);
            // 
            // epostakaydet
            // 
            this.epostakaydet.Animated = true;
            this.epostakaydet.AnimatedGIF = true;
            this.epostakaydet.AutoRoundedCorners = true;
            this.epostakaydet.BackColor = System.Drawing.Color.Transparent;
            this.epostakaydet.BorderRadius = 20;
            this.epostakaydet.Cursor = System.Windows.Forms.Cursors.Hand;
            this.epostakaydet.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.epostakaydet.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.epostakaydet.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.epostakaydet.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.epostakaydet.FillColor = System.Drawing.Color.DodgerBlue;
            this.epostakaydet.FocusedColor = System.Drawing.Color.White;
            this.epostakaydet.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.epostakaydet.ForeColor = System.Drawing.Color.White;
            this.epostakaydet.Image = global::Veresiye2025.Properties.Resources.save_icon;
            this.epostakaydet.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.epostakaydet.ImageSize = new System.Drawing.Size(30, 30);
            this.epostakaydet.IndicateFocus = true;
            this.epostakaydet.Location = new System.Drawing.Point(7, 189);
            this.epostakaydet.Name = "epostakaydet";
            this.epostakaydet.Size = new System.Drawing.Size(144, 43);
            this.epostakaydet.TabIndex = 27;
            this.epostakaydet.Text = "Kaydet";
            this.epostakaydet.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.epostakaydet.TextFormatNoPrefix = true;
            this.epostakaydet.Click += new System.EventHandler(this.epostakaydet_Click);
            // 
            // txtSmtpHost
            // 
            this.txtSmtpHost.ForeColor = System.Drawing.Color.Gray;
            this.txtSmtpHost.Location = new System.Drawing.Point(7, 21);
            this.txtSmtpHost.Name = "txtSmtpHost";
            this.txtSmtpHost.Size = new System.Drawing.Size(250, 22);
            this.txtSmtpHost.TabIndex = 1;
            this.txtSmtpHost.Text = "SMTP Sunucusu";
            this.txtSmtpHost.Enter += new System.EventHandler(this.TextBox_Enter);
            this.txtSmtpHost.Leave += new System.EventHandler(this.TextBox_Leave);
            // 
            // txtSmtpPort
            // 
            this.txtSmtpPort.ForeColor = System.Drawing.Color.Gray;
            this.txtSmtpPort.Location = new System.Drawing.Point(7, 62);
            this.txtSmtpPort.Name = "txtSmtpPort";
            this.txtSmtpPort.Size = new System.Drawing.Size(100, 22);
            this.txtSmtpPort.TabIndex = 2;
            this.txtSmtpPort.Text = "Port";
            this.txtSmtpPort.Enter += new System.EventHandler(this.TextBox_Enter);
            this.txtSmtpPort.Leave += new System.EventHandler(this.TextBox_Leave);
            // 
            // txtEmailUser
            // 
            this.txtEmailUser.ForeColor = System.Drawing.Color.Gray;
            this.txtEmailUser.Location = new System.Drawing.Point(7, 102);
            this.txtEmailUser.Name = "txtEmailUser";
            this.txtEmailUser.Size = new System.Drawing.Size(250, 22);
            this.txtEmailUser.TabIndex = 3;
            this.txtEmailUser.Text = "E-Posta Adresi";
            this.txtEmailUser.Enter += new System.EventHandler(this.TextBox_Enter);
            this.txtEmailUser.Leave += new System.EventHandler(this.TextBox_Leave);
            // 
            // txtEmailPassword
            // 
            this.txtEmailPassword.ForeColor = System.Drawing.Color.Gray;
            this.txtEmailPassword.Location = new System.Drawing.Point(7, 142);
            this.txtEmailPassword.Name = "txtEmailPassword";
            this.txtEmailPassword.Size = new System.Drawing.Size(250, 22);
            this.txtEmailPassword.TabIndex = 4;
            this.txtEmailPassword.Text = "Şifre";
            this.txtEmailPassword.Enter += new System.EventHandler(this.PasswordBox_Enter);
            this.txtEmailPassword.Leave += new System.EventHandler(this.PasswordBox_Leave);
            // 
            // smsayar
            // 
            this.smsayar.BackColor = System.Drawing.SystemColors.Control;
            this.smsayar.Controls.Add(this.label2);
            this.smsayar.Controls.Add(this.label1);
            this.smsayar.Controls.Add(this.temizle);
            this.smsayar.Controls.Add(this.lstCariLimitAsanlar);
            this.smsayar.Controls.Add(this.btnSmsGonder);
            this.smsayar.Controls.Add(this.btnListedenCikar);
            this.smsayar.Controls.Add(this.btnCariLimitKontrol);
            this.smsayar.Controls.Add(this.cmbCariSec);
            this.smsayar.Controls.Add(this.btnCariEkle);
            this.smsayar.Location = new System.Drawing.Point(4, 25);
            this.smsayar.Name = "smsayar";
            this.smsayar.Padding = new System.Windows.Forms.Padding(3);
            this.smsayar.Size = new System.Drawing.Size(792, 342);
            this.smsayar.TabIndex = 2;
            this.smsayar.Text = "Sms Ayarları";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(623, 293);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 23);
            this.label2.TabIndex = 11;
            this.label2.Text = "Manuel Cari Seç";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(6, 319);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(649, 23);
            this.label1.TabIndex = 10;
            this.label1.Text = "Not:Bu Bölümde Sadece Telefon Numarası Olan Cari Kartlar Listelenmektedir.!!!";
            // 
            // temizle
            // 
            this.temizle.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.temizle.ForeColor = System.Drawing.Color.White;
            this.temizle.Location = new System.Drawing.Point(623, 174);
            this.temizle.Name = "temizle";
            this.temizle.Size = new System.Drawing.Size(162, 50);
            this.temizle.TabIndex = 8;
            this.temizle.Text = "Tümünü Sil";
            this.temizle.Click += new System.EventHandler(this.temizle_Click);
            // 
            // lstCariLimitAsanlar
            // 
            this.lstCariLimitAsanlar.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lstCariLimitAsanlar.ItemHeight = 23;
            this.lstCariLimitAsanlar.Location = new System.Drawing.Point(3, 3);
            this.lstCariLimitAsanlar.Name = "lstCariLimitAsanlar";
            this.lstCariLimitAsanlar.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstCariLimitAsanlar.Size = new System.Drawing.Size(614, 257);
            this.lstCariLimitAsanlar.TabIndex = 0;
            // 
            // btnSmsGonder
            // 
            this.btnSmsGonder.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnSmsGonder.ForeColor = System.Drawing.Color.White;
            this.btnSmsGonder.Location = new System.Drawing.Point(623, 6);
            this.btnSmsGonder.Name = "btnSmsGonder";
            this.btnSmsGonder.Size = new System.Drawing.Size(162, 50);
            this.btnSmsGonder.TabIndex = 1;
            this.btnSmsGonder.Text = "SMS Gönder";
            this.btnSmsGonder.Click += new System.EventHandler(this.btnSmsGonder_Click);
            // 
            // btnListedenCikar
            // 
            this.btnListedenCikar.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnListedenCikar.ForeColor = System.Drawing.Color.White;
            this.btnListedenCikar.Location = new System.Drawing.Point(623, 62);
            this.btnListedenCikar.Name = "btnListedenCikar";
            this.btnListedenCikar.Size = new System.Drawing.Size(162, 50);
            this.btnListedenCikar.TabIndex = 3;
            this.btnListedenCikar.Text = "Listeden Çıkar";
            this.btnListedenCikar.Click += new System.EventHandler(this.btnListedenCikar_Click);
            // 
            // btnCariLimitKontrol
            // 
            this.btnCariLimitKontrol.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnCariLimitKontrol.ForeColor = System.Drawing.Color.White;
            this.btnCariLimitKontrol.Location = new System.Drawing.Point(623, 118);
            this.btnCariLimitKontrol.Name = "btnCariLimitKontrol";
            this.btnCariLimitKontrol.Size = new System.Drawing.Size(162, 50);
            this.btnCariLimitKontrol.TabIndex = 5;
            this.btnCariLimitKontrol.Text = "Limit Aşanları Listele";
            this.btnCariLimitKontrol.Click += new System.EventHandler(this.btnCariLimitKontrol_Click);
            // 
            // cmbCariSec
            // 
            this.cmbCariSec.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCariSec.Location = new System.Drawing.Point(6, 285);
            this.cmbCariSec.Name = "cmbCariSec";
            this.cmbCariSec.Size = new System.Drawing.Size(611, 24);
            this.cmbCariSec.TabIndex = 9;
            // 
            // btnCariEkle
            // 
            this.btnCariEkle.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnCariEkle.ForeColor = System.Drawing.Color.White;
            this.btnCariEkle.Location = new System.Drawing.Point(623, 230);
            this.btnCariEkle.Name = "btnCariEkle";
            this.btnCariEkle.Size = new System.Drawing.Size(162, 50);
            this.btnCariEkle.TabIndex = 7;
            this.btnCariEkle.Text = "Seçili Cariyi Ekle";
            this.btnCariEkle.Click += new System.EventHandler(this.btnCariEkle_Click);
            // 
            // tema
            // 
            this.tema.BackColor = System.Drawing.SystemColors.Control;
            this.tema.Location = new System.Drawing.Point(4, 25);
            this.tema.Name = "tema";
            this.tema.Padding = new System.Windows.Forms.Padding(3);
            this.tema.Size = new System.Drawing.Size(792, 342);
            this.tema.TabIndex = 3;
            this.tema.Text = "Tema Ayarları";
            // 
            // dilayar
            // 
            this.dilayar.BackColor = System.Drawing.SystemColors.Control;
            this.dilayar.Controls.Add(this.guna2ImageButton1);
            this.dilayar.Controls.Add(this.guna2ImageButton2);
            this.dilayar.Controls.Add(this.dilikaydet);
            this.dilayar.Controls.Add(this.dilsec);
            this.dilayar.Location = new System.Drawing.Point(4, 25);
            this.dilayar.Name = "dilayar";
            this.dilayar.Padding = new System.Windows.Forms.Padding(3);
            this.dilayar.Size = new System.Drawing.Size(792, 342);
            this.dilayar.TabIndex = 4;
            this.dilayar.Text = "Dil";
            // 
            // guna2ImageButton1
            // 
            this.guna2ImageButton1.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.guna2ImageButton1.HoverState.ImageSize = new System.Drawing.Size(64, 64);
            this.guna2ImageButton1.Image = ((System.Drawing.Image)(resources.GetObject("guna2ImageButton1.Image")));
            this.guna2ImageButton1.ImageOffset = new System.Drawing.Point(0, 0);
            this.guna2ImageButton1.ImageRotate = 0F;
            this.guna2ImageButton1.Location = new System.Drawing.Point(186, 146);
            this.guna2ImageButton1.Name = "guna2ImageButton1";
            this.guna2ImageButton1.PressedState.ImageSize = new System.Drawing.Size(64, 64);
            this.guna2ImageButton1.Size = new System.Drawing.Size(125, 97);
            this.guna2ImageButton1.TabIndex = 4;
            this.guna2ImageButton1.Click += new System.EventHandler(this.guna2ImageButton1_Click);
            // 
            // guna2ImageButton2
            // 
            this.guna2ImageButton2.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.guna2ImageButton2.HoverState.ImageSize = new System.Drawing.Size(64, 64);
            this.guna2ImageButton2.Image = ((System.Drawing.Image)(resources.GetObject("guna2ImageButton2.Image")));
            this.guna2ImageButton2.ImageOffset = new System.Drawing.Point(0, 0);
            this.guna2ImageButton2.ImageRotate = 0F;
            this.guna2ImageButton2.Location = new System.Drawing.Point(39, 146);
            this.guna2ImageButton2.Name = "guna2ImageButton2";
            this.guna2ImageButton2.PressedState.ImageSize = new System.Drawing.Size(64, 64);
            this.guna2ImageButton2.Size = new System.Drawing.Size(125, 97);
            this.guna2ImageButton2.TabIndex = 3;
            // 
            // dilikaydet
            // 
            this.dilikaydet.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.dilikaydet.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.dilikaydet.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.dilikaydet.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.dilikaydet.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dilikaydet.ForeColor = System.Drawing.Color.White;
            this.dilikaydet.Location = new System.Drawing.Point(39, 101);
            this.dilikaydet.Name = "dilikaydet";
            this.dilikaydet.Size = new System.Drawing.Size(272, 39);
            this.dilikaydet.TabIndex = 1;
            this.dilikaydet.Text = "Kaydet";
            this.dilikaydet.Click += new System.EventHandler(this.dilikaydet_Click);
            // 
            // dilsec
            // 
            this.dilsec.BackColor = System.Drawing.Color.Transparent;
            this.dilsec.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.dilsec.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dilsec.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.dilsec.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.dilsec.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dilsec.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.dilsec.ItemHeight = 30;
            this.dilsec.Items.AddRange(new object[] {
            "tr",
            "en"});
            this.dilsec.Location = new System.Drawing.Point(39, 33);
            this.dilsec.Name = "dilsec";
            this.dilsec.Size = new System.Drawing.Size(272, 36);
            this.dilsec.StartIndex = 0;
            this.dilsec.TabIndex = 0;
            // 
            // kullanici
            // 
            this.kullanici.BackColor = System.Drawing.SystemColors.Control;
            this.kullanici.Location = new System.Drawing.Point(4, 25);
            this.kullanici.Name = "kullanici";
            this.kullanici.Padding = new System.Windows.Forms.Padding(3);
            this.kullanici.Size = new System.Drawing.Size(792, 342);
            this.kullanici.TabIndex = 5;
            this.kullanici.Text = "Kullanıcı Ayarları";
            // 
            // Alarm
            // 
            this.Alarm.BackColor = System.Drawing.SystemColors.Control;
            this.Alarm.Location = new System.Drawing.Point(4, 25);
            this.Alarm.Name = "Alarm";
            this.Alarm.Padding = new System.Windows.Forms.Padding(3);
            this.Alarm.Size = new System.Drawing.Size(792, 342);
            this.Alarm.TabIndex = 6;
            this.Alarm.Text = "Alarm Ayarları";
            // 
            // vade
            // 
            this.vade.BackColor = System.Drawing.SystemColors.Control;
            this.vade.Location = new System.Drawing.Point(4, 25);
            this.vade.Name = "vade";
            this.vade.Padding = new System.Windows.Forms.Padding(3);
            this.vade.Size = new System.Drawing.Size(792, 342);
            this.vade.TabIndex = 7;
            this.vade.Text = "Vade Ayarları";
            // 
            // pnlBanner
            // 
            this.pnlBanner.BackColor = System.Drawing.Color.Yellow;
            this.pnlBanner.Controls.Add(this.lblBanner);
            this.pnlBanner.Location = new System.Drawing.Point(44, 380);
            this.pnlBanner.Name = "pnlBanner";
            this.pnlBanner.Size = new System.Drawing.Size(720, 90);
            this.pnlBanner.TabIndex = 1;
            // 
            // lblBanner
            // 
            this.lblBanner.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.lblBanner.ForeColor = System.Drawing.Color.Red;
            this.lblBanner.Location = new System.Drawing.Point(0, 0);
            this.lblBanner.Name = "lblBanner";
            this.lblBanner.Size = new System.Drawing.Size(720, 90);
            this.lblBanner.TabIndex = 0;
            this.lblBanner.Text = "📢 Büyük Kampanya! %50\'ye varan indirimler için tıklayın!";
            this.lblBanner.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblBanner.Click += new System.EventHandler(this.lblBanner_Click);
            // 
            // FormGenelAyarlar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 478);
            this.Controls.Add(this.genelayarlar);
            this.Controls.Add(this.pnlBanner);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormGenelAyarlar";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Genel Ayarlar";
            this.genelayarlar.ResumeLayout(false);
            this.tabgenelayar.ResumeLayout(false);
            this.eposta.ResumeLayout(false);
            this.eposta.PerformLayout();
            this.smsayar.ResumeLayout(false);
            this.smsayar.PerformLayout();
            this.dilayar.ResumeLayout(false);
            this.pnlBanner.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.TabControl genelayarlar;
        public System.Windows.Forms.TabPage tabgenelayar;
        public System.Windows.Forms.TabPage eposta;
        public System.Windows.Forms.TabPage smsayar;
        public System.Windows.Forms.TabPage tema;
        public System.Windows.Forms.TabPage dilayar;
        public System.Windows.Forms.TabPage kullanici;
        public System.Windows.Forms.TabPage Alarm;
        public System.Windows.Forms.TabPage vade;
        public Guna.UI2.WinForms.Guna2Button ayarlarkaydet;
        public Guna.UI2.WinForms.Guna2Button ayarlarvazgec;

        public System.Windows.Forms.TextBox txtSmtpHost;
        public System.Windows.Forms.TextBox txtSmtpPort;
        public System.Windows.Forms.TextBox txtEmailUser;
        public System.Windows.Forms.TextBox txtEmailPassword;
        public System.Windows.Forms.Panel pnlBanner;
        public System.Windows.Forms.Label lblBanner;

        // **Sms Ayarları Sekmesine Eklenecek Bileşenler**
        public System.Windows.Forms.ListBox lstCariLimitAsanlar;
        public Guna.UI2.WinForms.Guna2Button btnSmsGonder;
        public Guna.UI2.WinForms.Guna2Button btnListedenCikar;
        public Guna.UI2.WinForms.Guna2Button btnCariLimitKontrol;


        // **Sms Ayarları Sekmesine Eklenecek Bileşenler**
        public System.Windows.Forms.ComboBox cmbCariSec;
        public Guna.UI2.WinForms.Guna2Button btnCariEkle;
        public Guna.UI2.WinForms.Guna2Button temizle;
        public Label label1;
        public Label label2;
        public Guna.UI2.WinForms.Guna2Button dilikaydet;
        public Guna.UI2.WinForms.Guna2ComboBox dilsec;
        public Guna.UI2.WinForms.Guna2ImageButton guna2ImageButton1;
        public Guna.UI2.WinForms.Guna2ImageButton guna2ImageButton2;
        public Guna.UI2.WinForms.Guna2Button epostakaydet;
        public Guna.UI2.WinForms.Guna2Button epostagonder;
        public Guna.UI2.WinForms.Guna2Panel epostagunapanel;
    }
}