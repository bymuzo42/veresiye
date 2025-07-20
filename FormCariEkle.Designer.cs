using System.Drawing;

namespace Veresiye2025
{
    partial class FormCariEkle
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCariEkle));
            this.titleBar = new System.Windows.Forms.Panel();
            this.closeButton = new System.Windows.Forms.Button();
            this.lblFormTitle = new System.Windows.Forms.Label();
            this.leftPanel = new System.Windows.Forms.Panel();
            this.rightPanel = new System.Windows.Forms.Panel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lblCariKodu = new System.Windows.Forms.Label();
            this.txtCariKodu = new System.Windows.Forms.TextBox();
            this.lblUnvani = new System.Windows.Forms.Label();
            this.txtUnvani = new System.Windows.Forms.TextBox();
            this.lblYetkili = new System.Windows.Forms.Label();
            this.txtYetkili = new System.Windows.Forms.TextBox();
            this.lblTelefon = new System.Windows.Forms.Label();
            this.txtTelefon = new System.Windows.Forms.TextBox();
            this.lblGsm = new System.Windows.Forms.Label();
            this.txtGsm = new System.Windows.Forms.TextBox();
            this.lblFaks = new System.Windows.Forms.Label();
            this.txtFaks = new System.Windows.Forms.TextBox();
            this.lblEposta = new System.Windows.Forms.Label();
            this.txtEposta = new System.Windows.Forms.TextBox();
            this.lblTarih = new System.Windows.Forms.Label();
            this.dtpTarih = new System.Windows.Forms.DateTimePicker();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lblAdres = new System.Windows.Forms.Label();
            this.txtAdres = new System.Windows.Forms.TextBox();
            this.lblIl = new System.Windows.Forms.Label();
            this.txtIl = new System.Windows.Forms.TextBox();
            this.lblIlce = new System.Windows.Forms.Label();
            this.txtIlce = new System.Windows.Forms.TextBox();
            this.lblVergiDairesi = new System.Windows.Forms.Label();
            this.txtVergiDairesi = new System.Windows.Forms.TextBox();
            this.lblVergiNo = new System.Windows.Forms.Label();
            this.txtVergiNo = new System.Windows.Forms.TextBox();
            this.lblWeb = new System.Windows.Forms.Label();
            this.txtWeb = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.lblCariLimit = new System.Windows.Forms.Label();
            this.txtCariLimit = new System.Windows.Forms.TextBox();
            this.lblHesap = new System.Windows.Forms.Label();
            this.cmbHesap = new System.Windows.Forms.ComboBox();
            this.vade = new System.Windows.Forms.Label();
            this.vadegun = new System.Windows.Forms.NumericUpDown();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.btnKaydet = new Guna.UI2.WinForms.Guna2Button();
            this.vazgec = new Guna.UI2.WinForms.Guna2Button();
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.titleBar.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vadegun)).BeginInit();
            this.SuspendLayout();
            // 
            // titleBar
            // 
            this.titleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.titleBar.Controls.Add(this.closeButton);
            this.titleBar.Controls.Add(this.lblFormTitle);
            this.titleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.titleBar.Location = new System.Drawing.Point(0, 0);
            this.titleBar.Name = "titleBar";
            this.titleBar.Size = new System.Drawing.Size(722, 40);
            this.titleBar.TabIndex = 0;
            this.titleBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.titleBar_MouseDown);
            this.titleBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.titleBar_MouseMove);
            this.titleBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.titleBar_MouseUp);
            // 
            // closeButton
            // 
            this.closeButton.BackColor = System.Drawing.Color.Transparent;
            this.closeButton.FlatAppearance.BorderSize = 0;
            this.closeButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(17)))), ((int)(((byte)(35)))));
            this.closeButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(17)))), ((int)(((byte)(35)))));
            this.closeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.closeButton.ForeColor = System.Drawing.Color.White;
            this.closeButton.Location = new System.Drawing.Point(680, 0);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(40, 40);
            this.closeButton.TabIndex = 1;
            this.closeButton.Text = "✕";
            this.closeButton.UseVisualStyleBackColor = false;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // lblFormTitle
            // 
            this.lblFormTitle.AutoSize = true;
            this.lblFormTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblFormTitle.ForeColor = System.Drawing.Color.White;
            this.lblFormTitle.Location = new System.Drawing.Point(12, 9);
            this.lblFormTitle.Name = "lblFormTitle";
            this.lblFormTitle.Size = new System.Drawing.Size(128, 25);
            this.lblFormTitle.TabIndex = 0;
            this.lblFormTitle.Text = "Cari Kart Ekle";
            // 
            // leftPanel
            // 
            this.leftPanel.Location = new System.Drawing.Point(0, 0);
            this.leftPanel.Name = "leftPanel";
            this.leftPanel.Size = new System.Drawing.Size(200, 100);
            this.leftPanel.TabIndex = 0;
            // 
            // rightPanel
            // 
            this.rightPanel.Location = new System.Drawing.Point(0, 0);
            this.rightPanel.Name = "rightPanel";
            this.rightPanel.Size = new System.Drawing.Size(200, 100);
            this.rightPanel.TabIndex = 0;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Controls.Add(this.tabPage3);
            this.tabControl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl.ItemSize = new System.Drawing.Size(180, 35);
            this.tabControl.Location = new System.Drawing.Point(20, 48);
            this.tabControl.Name = "tabControl";
            this.tabControl.Padding = new System.Drawing.Point(16, 3);
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(680, 384);
            this.tabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.Controls.Add(this.lblCariKodu);
            this.tabPage1.Controls.Add(this.txtCariKodu);
            this.tabPage1.Controls.Add(this.lblUnvani);
            this.tabPage1.Controls.Add(this.txtUnvani);
            this.tabPage1.Controls.Add(this.lblYetkili);
            this.tabPage1.Controls.Add(this.txtYetkili);
            this.tabPage1.Controls.Add(this.lblTelefon);
            this.tabPage1.Controls.Add(this.txtTelefon);
            this.tabPage1.Controls.Add(this.lblGsm);
            this.tabPage1.Controls.Add(this.txtGsm);
            this.tabPage1.Controls.Add(this.lblFaks);
            this.tabPage1.Controls.Add(this.txtFaks);
            this.tabPage1.Controls.Add(this.lblEposta);
            this.tabPage1.Controls.Add(this.txtEposta);
            this.tabPage1.Controls.Add(this.lblTarih);
            this.tabPage1.Controls.Add(this.dtpTarih);
            this.tabPage1.Location = new System.Drawing.Point(4, 39);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(672, 341);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Temel Bilgiler";
            // 
            // lblCariKodu
            // 
            this.lblCariKodu.AutoSize = true;
            this.lblCariKodu.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblCariKodu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblCariKodu.Location = new System.Drawing.Point(30, 22);
            this.lblCariKodu.Name = "lblCariKodu";
            this.lblCariKodu.Size = new System.Drawing.Size(89, 23);
            this.lblCariKodu.TabIndex = 0;
            this.lblCariKodu.Text = "Cari Kodu:";
            // 
            // txtCariKodu
            // 
            this.txtCariKodu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCariKodu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCariKodu.Location = new System.Drawing.Point(160, 20);
            this.txtCariKodu.Name = "txtCariKodu";
            this.txtCariKodu.Size = new System.Drawing.Size(200, 30);
            this.txtCariKodu.TabIndex = 0;
            // 
            // lblUnvani
            // 
            this.lblUnvani.AutoSize = true;
            this.lblUnvani.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblUnvani.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblUnvani.Location = new System.Drawing.Point(30, 62);
            this.lblUnvani.Name = "lblUnvani";
            this.lblUnvani.Size = new System.Drawing.Size(68, 23);
            this.lblUnvani.TabIndex = 0;
            this.lblUnvani.Text = "Unvanı:";
            // 
            // txtUnvani
            // 
            this.txtUnvani.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUnvani.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtUnvani.Location = new System.Drawing.Point(160, 60);
            this.txtUnvani.Name = "txtUnvani";
            this.txtUnvani.Size = new System.Drawing.Size(200, 30);
            this.txtUnvani.TabIndex = 1;
            // 
            // lblYetkili
            // 
            this.lblYetkili.AutoSize = true;
            this.lblYetkili.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblYetkili.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblYetkili.Location = new System.Drawing.Point(30, 102);
            this.lblYetkili.Name = "lblYetkili";
            this.lblYetkili.Size = new System.Drawing.Size(58, 23);
            this.lblYetkili.TabIndex = 0;
            this.lblYetkili.Text = "Yetkili:";
            // 
            // txtYetkili
            // 
            this.txtYetkili.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtYetkili.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtYetkili.Location = new System.Drawing.Point(160, 100);
            this.txtYetkili.Name = "txtYetkili";
            this.txtYetkili.Size = new System.Drawing.Size(200, 30);
            this.txtYetkili.TabIndex = 2;
            // 
            // lblTelefon
            // 
            this.lblTelefon.AutoSize = true;
            this.lblTelefon.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblTelefon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblTelefon.Location = new System.Drawing.Point(30, 142);
            this.lblTelefon.Name = "lblTelefon";
            this.lblTelefon.Size = new System.Drawing.Size(69, 23);
            this.lblTelefon.TabIndex = 0;
            this.lblTelefon.Text = "Telefon:";
            // 
            // txtTelefon
            // 
            this.txtTelefon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTelefon.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTelefon.Location = new System.Drawing.Point(160, 140);
            this.txtTelefon.Name = "txtTelefon";
            this.txtTelefon.Size = new System.Drawing.Size(200, 30);
            this.txtTelefon.TabIndex = 3;
            // 
            // lblGsm
            // 
            this.lblGsm.AutoSize = true;
            this.lblGsm.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblGsm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblGsm.Location = new System.Drawing.Point(30, 182);
            this.lblGsm.Name = "lblGsm";
            this.lblGsm.Size = new System.Drawing.Size(48, 23);
            this.lblGsm.TabIndex = 0;
            this.lblGsm.Text = "Gsm:";
            // 
            // txtGsm
            // 
            this.txtGsm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGsm.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtGsm.Location = new System.Drawing.Point(160, 180);
            this.txtGsm.Name = "txtGsm";
            this.txtGsm.Size = new System.Drawing.Size(200, 30);
            this.txtGsm.TabIndex = 4;
            // 
            // lblFaks
            // 
            this.lblFaks.AutoSize = true;
            this.lblFaks.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblFaks.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblFaks.Location = new System.Drawing.Point(30, 222);
            this.lblFaks.Name = "lblFaks";
            this.lblFaks.Size = new System.Drawing.Size(47, 23);
            this.lblFaks.TabIndex = 0;
            this.lblFaks.Text = "Faks:";
            // 
            // txtFaks
            // 
            this.txtFaks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFaks.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtFaks.Location = new System.Drawing.Point(160, 220);
            this.txtFaks.Name = "txtFaks";
            this.txtFaks.Size = new System.Drawing.Size(200, 30);
            this.txtFaks.TabIndex = 5;
            // 
            // lblEposta
            // 
            this.lblEposta.AutoSize = true;
            this.lblEposta.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblEposta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblEposta.Location = new System.Drawing.Point(30, 262);
            this.lblEposta.Name = "lblEposta";
            this.lblEposta.Size = new System.Drawing.Size(71, 23);
            this.lblEposta.TabIndex = 0;
            this.lblEposta.Text = "E-Posta:";
            // 
            // txtEposta
            // 
            this.txtEposta.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEposta.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtEposta.Location = new System.Drawing.Point(160, 260);
            this.txtEposta.Name = "txtEposta";
            this.txtEposta.Size = new System.Drawing.Size(200, 30);
            this.txtEposta.TabIndex = 6;
            // 
            // lblTarih
            // 
            this.lblTarih.AutoSize = true;
            this.lblTarih.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblTarih.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblTarih.Location = new System.Drawing.Point(30, 302);
            this.lblTarih.Name = "lblTarih";
            this.lblTarih.Size = new System.Drawing.Size(50, 23);
            this.lblTarih.TabIndex = 0;
            this.lblTarih.Text = "Tarih:";
            // 
            // dtpTarih
            // 
            this.dtpTarih.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpTarih.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTarih.Location = new System.Drawing.Point(160, 300);
            this.dtpTarih.Name = "dtpTarih";
            this.dtpTarih.Size = new System.Drawing.Size(200, 30);
            this.dtpTarih.TabIndex = 7;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.White;
            this.tabPage2.Controls.Add(this.lblAdres);
            this.tabPage2.Controls.Add(this.txtAdres);
            this.tabPage2.Controls.Add(this.lblIl);
            this.tabPage2.Controls.Add(this.txtIl);
            this.tabPage2.Controls.Add(this.lblIlce);
            this.tabPage2.Controls.Add(this.txtIlce);
            this.tabPage2.Controls.Add(this.lblVergiDairesi);
            this.tabPage2.Controls.Add(this.txtVergiDairesi);
            this.tabPage2.Controls.Add(this.lblVergiNo);
            this.tabPage2.Controls.Add(this.txtVergiNo);
            this.tabPage2.Controls.Add(this.lblWeb);
            this.tabPage2.Controls.Add(this.txtWeb);
            this.tabPage2.Location = new System.Drawing.Point(4, 39);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(672, 341);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Detay Bilgiler";
            // 
            // lblAdres
            // 
            this.lblAdres.AutoSize = true;
            this.lblAdres.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblAdres.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblAdres.Location = new System.Drawing.Point(30, 22);
            this.lblAdres.Name = "lblAdres";
            this.lblAdres.Size = new System.Drawing.Size(57, 23);
            this.lblAdres.TabIndex = 0;
            this.lblAdres.Text = "Adres:";
            // 
            // txtAdres
            // 
            this.txtAdres.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAdres.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtAdres.Location = new System.Drawing.Point(160, 20);
            this.txtAdres.Multiline = true;
            this.txtAdres.Name = "txtAdres";
            this.txtAdres.Size = new System.Drawing.Size(300, 60);
            this.txtAdres.TabIndex = 0;
            // 
            // lblIl
            // 
            this.lblIl.AutoSize = true;
            this.lblIl.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblIl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblIl.Location = new System.Drawing.Point(30, 92);
            this.lblIl.Name = "lblIl";
            this.lblIl.Size = new System.Drawing.Size(23, 23);
            this.lblIl.TabIndex = 0;
            this.lblIl.Text = "İl:";
            // 
            // txtIl
            // 
            this.txtIl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtIl.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtIl.Location = new System.Drawing.Point(160, 90);
            this.txtIl.Name = "txtIl";
            this.txtIl.Size = new System.Drawing.Size(200, 30);
            this.txtIl.TabIndex = 1;
            // 
            // lblIlce
            // 
            this.lblIlce.AutoSize = true;
            this.lblIlce.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblIlce.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblIlce.Location = new System.Drawing.Point(30, 132);
            this.lblIlce.Name = "lblIlce";
            this.lblIlce.Size = new System.Drawing.Size(40, 23);
            this.lblIlce.TabIndex = 0;
            this.lblIlce.Text = "İlçe:";
            // 
            // txtIlce
            // 
            this.txtIlce.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtIlce.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtIlce.Location = new System.Drawing.Point(160, 130);
            this.txtIlce.Name = "txtIlce";
            this.txtIlce.Size = new System.Drawing.Size(200, 30);
            this.txtIlce.TabIndex = 2;
            // 
            // lblVergiDairesi
            // 
            this.lblVergiDairesi.AutoSize = true;
            this.lblVergiDairesi.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblVergiDairesi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblVergiDairesi.Location = new System.Drawing.Point(30, 172);
            this.lblVergiDairesi.Name = "lblVergiDairesi";
            this.lblVergiDairesi.Size = new System.Drawing.Size(109, 23);
            this.lblVergiDairesi.TabIndex = 0;
            this.lblVergiDairesi.Text = "Vergi Dairesi:";
            // 
            // txtVergiDairesi
            // 
            this.txtVergiDairesi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVergiDairesi.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtVergiDairesi.Location = new System.Drawing.Point(160, 170);
            this.txtVergiDairesi.Name = "txtVergiDairesi";
            this.txtVergiDairesi.Size = new System.Drawing.Size(200, 30);
            this.txtVergiDairesi.TabIndex = 3;
            // 
            // lblVergiNo
            // 
            this.lblVergiNo.AutoSize = true;
            this.lblVergiNo.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblVergiNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblVergiNo.Location = new System.Drawing.Point(30, 212);
            this.lblVergiNo.Name = "lblVergiNo";
            this.lblVergiNo.Size = new System.Drawing.Size(81, 23);
            this.lblVergiNo.TabIndex = 0;
            this.lblVergiNo.Text = "Vergi No:";
            // 
            // txtVergiNo
            // 
            this.txtVergiNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVergiNo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtVergiNo.Location = new System.Drawing.Point(160, 210);
            this.txtVergiNo.Name = "txtVergiNo";
            this.txtVergiNo.Size = new System.Drawing.Size(200, 30);
            this.txtVergiNo.TabIndex = 4;
            // 
            // lblWeb
            // 
            this.lblWeb.AutoSize = true;
            this.lblWeb.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblWeb.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblWeb.Location = new System.Drawing.Point(30, 252);
            this.lblWeb.Name = "lblWeb";
            this.lblWeb.Size = new System.Drawing.Size(49, 23);
            this.lblWeb.TabIndex = 0;
            this.lblWeb.Text = "Web:";
            // 
            // txtWeb
            // 
            this.txtWeb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtWeb.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtWeb.Location = new System.Drawing.Point(160, 250);
            this.txtWeb.Name = "txtWeb";
            this.txtWeb.Size = new System.Drawing.Size(200, 30);
            this.txtWeb.TabIndex = 5;
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.White;
            this.tabPage3.Controls.Add(this.lblCariLimit);
            this.tabPage3.Controls.Add(this.txtCariLimit);
            this.tabPage3.Controls.Add(this.lblHesap);
            this.tabPage3.Controls.Add(this.cmbHesap);
            this.tabPage3.Controls.Add(this.vade);
            this.tabPage3.Controls.Add(this.vadegun);
            this.tabPage3.Location = new System.Drawing.Point(4, 39);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(672, 341);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Hesap Ayarları";
            // 
            // lblCariLimit
            // 
            this.lblCariLimit.AutoSize = true;
            this.lblCariLimit.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblCariLimit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblCariLimit.Location = new System.Drawing.Point(30, 22);
            this.lblCariLimit.Name = "lblCariLimit";
            this.lblCariLimit.Size = new System.Drawing.Size(86, 23);
            this.lblCariLimit.TabIndex = 0;
            this.lblCariLimit.Text = "Cari Limit:";
            // 
            // txtCariLimit
            // 
            this.txtCariLimit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCariLimit.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCariLimit.Location = new System.Drawing.Point(160, 20);
            this.txtCariLimit.Name = "txtCariLimit";
            this.txtCariLimit.Size = new System.Drawing.Size(200, 30);
            this.txtCariLimit.TabIndex = 0;
            // 
            // lblHesap
            // 
            this.lblHesap.AutoSize = true;
            this.lblHesap.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblHesap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblHesap.Location = new System.Drawing.Point(30, 62);
            this.lblHesap.Name = "lblHesap";
            this.lblHesap.Size = new System.Drawing.Size(62, 23);
            this.lblHesap.TabIndex = 0;
            this.lblHesap.Text = "Hesap:";
            // 
            // cmbHesap
            // 
            this.cmbHesap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbHesap.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbHesap.FormattingEnabled = true;
            this.cmbHesap.Location = new System.Drawing.Point(160, 60);
            this.cmbHesap.Name = "cmbHesap";
            this.cmbHesap.Size = new System.Drawing.Size(200, 31);
            this.cmbHesap.TabIndex = 1;
            // 
            // vade
            // 
            this.vade.AutoSize = true;
            this.vade.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.vade.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.vade.Location = new System.Drawing.Point(30, 102);
            this.vade.Name = "vade";
            this.vade.Size = new System.Drawing.Size(99, 23);
            this.vade.TabIndex = 0;
            this.vade.Text = "Vade Günü:";
            // 
            // vadegun
            // 
            this.vadegun.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.vadegun.Location = new System.Drawing.Point(160, 100);
            this.vadegun.Maximum = new decimal(new int[] {
            365,
            0,
            0,
            0});
            this.vadegun.Name = "vadegun";
            this.vadegun.Size = new System.Drawing.Size(200, 30);
            this.vadegun.TabIndex = 2;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(20, 560);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(680, 5);
            this.progressBar.TabIndex = 2;
            this.progressBar.Visible = false;
            // 
            // btnKaydet
            // 
            this.btnKaydet.Animated = true;
            this.btnKaydet.AnimatedGIF = true;
            this.btnKaydet.AutoRoundedCorners = true;
            this.btnKaydet.BackColor = System.Drawing.Color.Transparent;
            this.btnKaydet.BorderRadius = 20;
            this.btnKaydet.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKaydet.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnKaydet.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnKaydet.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnKaydet.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnKaydet.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnKaydet.FocusedColor = System.Drawing.Color.White;
            this.btnKaydet.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnKaydet.ForeColor = System.Drawing.Color.White;
            this.btnKaydet.Image = global::Veresiye2025.Properties.Resources.open;
            this.btnKaydet.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnKaydet.ImageSize = new System.Drawing.Size(25, 25);
            this.btnKaydet.IndicateFocus = true;
            this.btnKaydet.Location = new System.Drawing.Point(450, 442);
            this.btnKaydet.Name = "btnKaydet";
            this.btnKaydet.Size = new System.Drawing.Size(120, 43);
            this.btnKaydet.TabIndex = 3;
            this.btnKaydet.Text = "Kaydet";
            this.btnKaydet.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.btnKaydet.TextFormatNoPrefix = true;
            this.btnKaydet.Click += new System.EventHandler(this.btnKaydet_Click);
            // 
            // vazgec
            // 
            this.vazgec.Animated = true;
            this.vazgec.AnimatedGIF = true;
            this.vazgec.AutoRoundedCorners = true;
            this.vazgec.BackColor = System.Drawing.Color.Transparent;
            this.vazgec.BorderRadius = 20;
            this.vazgec.Cursor = System.Windows.Forms.Cursors.Hand;
            this.vazgec.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.vazgec.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.vazgec.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.vazgec.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.vazgec.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.vazgec.FocusedColor = System.Drawing.Color.White;
            this.vazgec.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.vazgec.ForeColor = System.Drawing.Color.White;
            this.vazgec.Image = global::Veresiye2025.Properties.Resources.kapat1;
            this.vazgec.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.vazgec.ImageSize = new System.Drawing.Size(25, 25);
            this.vazgec.IndicateFocus = true;
            this.vazgec.Location = new System.Drawing.Point(573, 442);
            this.vazgec.Name = "vazgec";
            this.vazgec.Size = new System.Drawing.Size(120, 43);
            this.vazgec.TabIndex = 4;
            this.vazgec.Text = "Vazgeç";
            this.vazgec.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.vazgec.TextFormatNoPrefix = true;
            this.vazgec.Click += new System.EventHandler(this.vazgec_Click);
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(20, 445);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(291, 48);
            this.guna2HtmlLabel1.TabIndex = 5;
            this.guna2HtmlLabel1.Text = "<font color=\'#666\'><b>Not:</b> <font color=\'red\'>Tüm alanları dikkatlice doldurun" +
    ".<br>Zorunlu alanlar: Unvan, Cari Limit</font>";
            // 
            // FormCariEkle
            // 
            this.ClientSize = new System.Drawing.Size(722, 508);
            this.Controls.Add(this.titleBar);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.btnKaydet);
            this.Controls.Add(this.vazgec);
            this.Controls.Add(this.guna2HtmlLabel1);
            this.Controls.Add(this.progressBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormCariEkle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cari Kart Ekle";
            this.Load += new System.EventHandler(this.FormCariEkle_Load);
            this.titleBar.ResumeLayout(false);
            this.titleBar.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vadegun)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Panel titleBar;
        public System.Windows.Forms.Label lblFormTitle;
        public System.Windows.Forms.Button closeButton;
        public System.Windows.Forms.Panel leftPanel;
        public System.Windows.Forms.Panel rightPanel;
        public System.Windows.Forms.TabControl tabControl;
        public System.Windows.Forms.TabPage tabPage1;
        public System.Windows.Forms.TabPage tabPage2;
        public System.Windows.Forms.TabPage tabPage3;
        public System.Windows.Forms.ProgressBar progressBar;
        public System.Windows.Forms.TextBox txtCariKodu;
        public System.Windows.Forms.TextBox txtUnvani;
        public System.Windows.Forms.TextBox txtYetkili;
        public System.Windows.Forms.TextBox txtTelefon;
        public System.Windows.Forms.TextBox txtGsm;
        public System.Windows.Forms.TextBox txtFaks;
        public System.Windows.Forms.TextBox txtAdres;
        public System.Windows.Forms.TextBox txtIl;
        public System.Windows.Forms.TextBox txtIlce;
        public System.Windows.Forms.TextBox txtVergiDairesi;
        public System.Windows.Forms.TextBox txtVergiNo;
        public System.Windows.Forms.TextBox txtEposta;
        public System.Windows.Forms.TextBox txtWeb;
        public System.Windows.Forms.TextBox txtCariLimit;
        public System.Windows.Forms.ComboBox cmbHesap;
        public System.Windows.Forms.DateTimePicker dtpTarih;
        public System.Windows.Forms.NumericUpDown vadegun;
        public System.Windows.Forms.Label lblCariKodu;
        public System.Windows.Forms.Label lblUnvani;
        public System.Windows.Forms.Label lblYetkili;
        public System.Windows.Forms.Label lblTelefon;
        public System.Windows.Forms.Label lblGsm;
        public System.Windows.Forms.Label lblFaks;
        public System.Windows.Forms.Label lblAdres;
        public System.Windows.Forms.Label lblIl;
        public System.Windows.Forms.Label lblIlce;
        public System.Windows.Forms.Label lblVergiDairesi;
        public System.Windows.Forms.Label lblVergiNo;
        public System.Windows.Forms.Label lblEposta;
        public System.Windows.Forms.Label lblWeb;
        public System.Windows.Forms.Label lblCariLimit;
        public System.Windows.Forms.Label lblHesap;
        public System.Windows.Forms.Label lblTarih;
        public System.Windows.Forms.Label vade;
        public Guna.UI2.WinForms.Guna2Button btnKaydet;
        public Guna.UI2.WinForms.Guna2Button vazgec;
        public Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
    }
}