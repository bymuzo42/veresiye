namespace Veresiye2025
{
    partial class Form3
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form3));
            this.titleBar = new System.Windows.Forms.Panel();
            this.closeButton = new System.Windows.Forms.Button();
            this.lblFormTitle = new System.Windows.Forms.Label();
            this.formPanel = new System.Windows.Forms.Panel();
            this.txtFirmaAdi = new System.Windows.Forms.TextBox();
            this.txtTelefonNo = new System.Windows.Forms.TextBox();
            this.txtFaxNo = new System.Windows.Forms.TextBox();
            this.txtAdres = new System.Windows.Forms.TextBox();
            this.txtIl = new System.Windows.Forms.TextBox();
            this.txtIlce = new System.Windows.Forms.TextBox();
            this.txtVergiNo = new System.Windows.Forms.TextBox();
            this.txtVergiDairesi = new System.Windows.Forms.TextBox();
            this.txtFirmaButcesi = new System.Windows.Forms.TextBox();
            this.txtUlkesi = new System.Windows.Forms.TextBox();
            this.cmbParaBirimi = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.btnfirmakayit = new Guna.UI2.WinForms.Guna2Button();
            this.btnVazgec = new Guna.UI2.WinForms.Guna2Button();
            this.formTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.statusPanel = new System.Windows.Forms.Panel();
            this.statusLabel = new System.Windows.Forms.Label();
            this.titleBar.SuspendLayout();
            this.formPanel.SuspendLayout();
            this.statusPanel.SuspendLayout();
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
            this.titleBar.Size = new System.Drawing.Size(470, 40);
            this.titleBar.TabIndex = 0;
            this.titleBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.titleBar_MouseDown);
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.BackColor = System.Drawing.Color.Transparent;
            this.closeButton.FlatAppearance.BorderSize = 0;
            this.closeButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(17)))), ((int)(((byte)(35)))));
            this.closeButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(17)))), ((int)(((byte)(35)))));
            this.closeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.closeButton.ForeColor = System.Drawing.Color.White;
            this.closeButton.Location = new System.Drawing.Point(430, 0);
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
            this.lblFormTitle.Size = new System.Drawing.Size(110, 25);
            this.lblFormTitle.TabIndex = 0;
            this.lblFormTitle.Text = "Firma Kayıt";
            // 
            // formPanel
            // 
            this.formPanel.AutoScroll = true;
            this.formPanel.BackColor = System.Drawing.Color.White;
            this.formPanel.Controls.Add(this.statusPanel);
            this.formPanel.Controls.Add(this.txtFirmaAdi);
            this.formPanel.Controls.Add(this.txtTelefonNo);
            this.formPanel.Controls.Add(this.txtFaxNo);
            this.formPanel.Controls.Add(this.txtAdres);
            this.formPanel.Controls.Add(this.txtIl);
            this.formPanel.Controls.Add(this.txtIlce);
            this.formPanel.Controls.Add(this.txtVergiNo);
            this.formPanel.Controls.Add(this.txtVergiDairesi);
            this.formPanel.Controls.Add(this.txtFirmaButcesi);
            this.formPanel.Controls.Add(this.txtUlkesi);
            this.formPanel.Controls.Add(this.cmbParaBirimi);
            this.formPanel.Controls.Add(this.label1);
            this.formPanel.Controls.Add(this.label2);
            this.formPanel.Controls.Add(this.label3);
            this.formPanel.Controls.Add(this.label4);
            this.formPanel.Controls.Add(this.label5);
            this.formPanel.Controls.Add(this.label6);
            this.formPanel.Controls.Add(this.label7);
            this.formPanel.Controls.Add(this.label8);
            this.formPanel.Controls.Add(this.label9);
            this.formPanel.Controls.Add(this.label10);
            this.formPanel.Controls.Add(this.label11);
            this.formPanel.Controls.Add(this.btnfirmakayit);
            this.formPanel.Controls.Add(this.btnVazgec);
            this.formPanel.Location = new System.Drawing.Point(20, 60);
            this.formPanel.Name = "formPanel";
            this.formPanel.Size = new System.Drawing.Size(430, 596);
            this.formPanel.TabIndex = 1;
            // 
            // statusPanel
            // 
            this.statusPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.statusPanel.Controls.Add(this.statusLabel);
            this.statusPanel.Location = new System.Drawing.Point(20, 486);
            this.statusPanel.Name = "statusPanel";
            this.statusPanel.Padding = new System.Windows.Forms.Padding(5);
            this.statusPanel.Size = new System.Drawing.Size(380, 35);
            this.statusPanel.TabIndex = 24;
            this.statusPanel.Visible = false;
            // 
            // statusLabel
            // 
            this.statusLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.statusLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.statusLabel.Location = new System.Drawing.Point(5, 5);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(370, 25);
            this.statusLabel.TabIndex = 0;
            this.statusLabel.Text = "Tüm bilgileri eksiksiz doldurunuz";
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtFirmaAdi
            // 
            this.txtFirmaAdi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFirmaAdi.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtFirmaAdi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.txtFirmaAdi.Location = new System.Drawing.Point(150, 20);
            this.txtFirmaAdi.Name = "txtFirmaAdi";
            this.txtFirmaAdi.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.txtFirmaAdi.Size = new System.Drawing.Size(250, 30);
            this.txtFirmaAdi.TabIndex = 0;
            this.formTooltip.SetToolTip(this.txtFirmaAdi, "Firma adını giriniz");
            // 
            // txtTelefonNo
            // 
            this.txtTelefonNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTelefonNo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTelefonNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.txtTelefonNo.Location = new System.Drawing.Point(150, 60);
            this.txtTelefonNo.Name = "txtTelefonNo";
            this.txtTelefonNo.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.txtTelefonNo.Size = new System.Drawing.Size(250, 30);
            this.txtTelefonNo.TabIndex = 1;
            this.formTooltip.SetToolTip(this.txtTelefonNo, "Telefon numarasını giriniz");
            // 
            // txtFaxNo
            // 
            this.txtFaxNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFaxNo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtFaxNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.txtFaxNo.Location = new System.Drawing.Point(150, 100);
            this.txtFaxNo.Name = "txtFaxNo";
            this.txtFaxNo.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.txtFaxNo.Size = new System.Drawing.Size(250, 30);
            this.txtFaxNo.TabIndex = 2;
            this.formTooltip.SetToolTip(this.txtFaxNo, "Fax numarasını giriniz");
            // 
            // txtAdres
            // 
            this.txtAdres.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAdres.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtAdres.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.txtAdres.Location = new System.Drawing.Point(150, 140);
            this.txtAdres.Multiline = true;
            this.txtAdres.Name = "txtAdres";
            this.txtAdres.Padding = new System.Windows.Forms.Padding(5, 5, 0, 0);
            this.txtAdres.Size = new System.Drawing.Size(250, 60);
            this.txtAdres.TabIndex = 3;
            this.formTooltip.SetToolTip(this.txtAdres, "Firma adresini giriniz");
            // 
            // txtIl
            // 
            this.txtIl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtIl.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtIl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.txtIl.Location = new System.Drawing.Point(150, 210);
            this.txtIl.Name = "txtIl";
            this.txtIl.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.txtIl.Size = new System.Drawing.Size(250, 30);
            this.txtIl.TabIndex = 4;
            this.formTooltip.SetToolTip(this.txtIl, "Firmanın bulunduğu ili giriniz");
            // 
            // txtIlce
            // 
            this.txtIlce.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtIlce.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtIlce.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.txtIlce.Location = new System.Drawing.Point(150, 250);
            this.txtIlce.Name = "txtIlce";
            this.txtIlce.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.txtIlce.Size = new System.Drawing.Size(250, 30);
            this.txtIlce.TabIndex = 5;
            this.formTooltip.SetToolTip(this.txtIlce, "Firmanın bulunduğu ilçeyi giriniz");
            // 
            // txtVergiNo
            // 
            this.txtVergiNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVergiNo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtVergiNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.txtVergiNo.Location = new System.Drawing.Point(150, 290);
            this.txtVergiNo.MaxLength = 11;
            this.txtVergiNo.Name = "txtVergiNo";
            this.txtVergiNo.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.txtVergiNo.Size = new System.Drawing.Size(250, 30);
            this.txtVergiNo.TabIndex = 6;
            this.formTooltip.SetToolTip(this.txtVergiNo, "Vergi numarasını 10-11 haneli olarak giriniz");
            // 
            // txtVergiDairesi
            // 
            this.txtVergiDairesi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVergiDairesi.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtVergiDairesi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.txtVergiDairesi.Location = new System.Drawing.Point(150, 330);
            this.txtVergiDairesi.Name = "txtVergiDairesi";
            this.txtVergiDairesi.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.txtVergiDairesi.Size = new System.Drawing.Size(250, 30);
            this.txtVergiDairesi.TabIndex = 7;
            this.formTooltip.SetToolTip(this.txtVergiDairesi, "Vergi dairesini giriniz");
            // 
            // txtFirmaButcesi
            // 
            this.txtFirmaButcesi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFirmaButcesi.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtFirmaButcesi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.txtFirmaButcesi.Location = new System.Drawing.Point(150, 370);
            this.txtFirmaButcesi.Name = "txtFirmaButcesi";
            this.txtFirmaButcesi.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.txtFirmaButcesi.Size = new System.Drawing.Size(250, 30);
            this.txtFirmaButcesi.TabIndex = 8;
            this.formTooltip.SetToolTip(this.txtFirmaButcesi, "Firma bütçesini rakamlarla giriniz");
            // 
            // txtUlkesi
            // 
            this.txtUlkesi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUlkesi.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtUlkesi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.txtUlkesi.Location = new System.Drawing.Point(150, 410);
            this.txtUlkesi.Name = "txtUlkesi";
            this.txtUlkesi.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.txtUlkesi.Size = new System.Drawing.Size(250, 30);
            this.txtUlkesi.TabIndex = 9;
            this.formTooltip.SetToolTip(this.txtUlkesi, "Firmanın bulunduğu ülkeyi giriniz");
            // 
            // cmbParaBirimi
            // 
            this.cmbParaBirimi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.cmbParaBirimi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbParaBirimi.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbParaBirimi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.cmbParaBirimi.FormattingEnabled = true;
            this.cmbParaBirimi.Items.AddRange(new object[] {
            "USD",
            "EUR",
            "TRY",
            "GBP"});
            this.cmbParaBirimi.Location = new System.Drawing.Point(150, 450);
            this.cmbParaBirimi.Name = "cmbParaBirimi";
            this.cmbParaBirimi.Size = new System.Drawing.Size(250, 31);
            this.cmbParaBirimi.TabIndex = 10;
            this.formTooltip.SetToolTip(this.cmbParaBirimi, "Para birimini seçiniz");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.label1.Location = new System.Drawing.Point(20, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 23);
            this.label1.TabIndex = 12;
            this.label1.Text = "Firma Adı:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.label2.Location = new System.Drawing.Point(20, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 23);
            this.label2.TabIndex = 13;
            this.label2.Text = "Telefon No:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.label3.Location = new System.Drawing.Point(20, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 23);
            this.label3.TabIndex = 14;
            this.label3.Text = "Fax No:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.label4.Location = new System.Drawing.Point(20, 142);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 23);
            this.label4.TabIndex = 15;
            this.label4.Text = "Adres:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.label5.Location = new System.Drawing.Point(20, 212);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 23);
            this.label5.TabIndex = 16;
            this.label5.Text = "İl:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.label6.Location = new System.Drawing.Point(20, 252);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 23);
            this.label6.TabIndex = 17;
            this.label6.Text = "İlçe:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.label7.Location = new System.Drawing.Point(20, 292);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 23);
            this.label7.TabIndex = 18;
            this.label7.Text = "Vergi No:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.label8.Location = new System.Drawing.Point(20, 332);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(109, 23);
            this.label8.TabIndex = 19;
            this.label8.Text = "Vergi Dairesi:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.label9.Location = new System.Drawing.Point(20, 372);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(116, 23);
            this.label9.TabIndex = 20;
            this.label9.Text = "Firma Bütçesi:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.label10.Location = new System.Drawing.Point(20, 412);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 23);
            this.label10.TabIndex = 21;
            this.label10.Text = "Ülkesi:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.label11.Location = new System.Drawing.Point(20, 452);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(96, 23);
            this.label11.TabIndex = 22;
            this.label11.Text = "Para Birimi:";
            // 
            // btnfirmakayit
            // 
            this.btnfirmakayit.Animated = true;
            this.btnfirmakayit.AnimatedGIF = true;
            this.btnfirmakayit.AutoRoundedCorners = true;
            this.btnfirmakayit.BackColor = System.Drawing.Color.Transparent;
            this.btnfirmakayit.BorderRadius = 20;
            this.btnfirmakayit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnfirmakayit.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnfirmakayit.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnfirmakayit.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnfirmakayit.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnfirmakayit.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnfirmakayit.FocusedColor = System.Drawing.Color.White;
            this.btnfirmakayit.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnfirmakayit.ForeColor = System.Drawing.Color.White;
            this.btnfirmakayit.Image = global::Veresiye2025.Properties.Resources.save_icon;
            this.btnfirmakayit.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnfirmakayit.ImageSize = new System.Drawing.Size(25, 25);
            this.btnfirmakayit.IndicateFocus = true;
            this.btnfirmakayit.Location = new System.Drawing.Point(256, 530);
            this.btnfirmakayit.Name = "btnfirmakayit";
            this.btnfirmakayit.Size = new System.Drawing.Size(144, 43);
            this.btnfirmakayit.TabIndex = 12;
            this.btnfirmakayit.Text = "Kaydet";
            this.btnfirmakayit.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnfirmakayit.TextFormatNoPrefix = true;
            this.btnfirmakayit.Click += new System.EventHandler(this.btnfirmakayit_Click);
            // 
            // btnVazgec
            // 
            this.btnVazgec.Animated = true;
            this.btnVazgec.AnimatedGIF = true;
            this.btnVazgec.AutoRoundedCorners = true;
            this.btnVazgec.BackColor = System.Drawing.Color.Transparent;
            this.btnVazgec.BorderRadius = 20;
            this.btnVazgec.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVazgec.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnVazgec.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnVazgec.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnVazgec.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnVazgec.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnVazgec.FocusedColor = System.Drawing.Color.White;
            this.btnVazgec.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnVazgec.ForeColor = System.Drawing.Color.White;
            this.btnVazgec.Image = global::Veresiye2025.Properties.Resources.kapat1;
            this.btnVazgec.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnVazgec.ImageSize = new System.Drawing.Size(25, 25);
            this.btnVazgec.IndicateFocus = true;
            this.btnVazgec.Location = new System.Drawing.Point(20, 530);
            this.btnVazgec.Name = "btnVazgec";
            this.btnVazgec.Size = new System.Drawing.Size(144, 43);
            this.btnVazgec.TabIndex = 13;
            this.btnVazgec.Text = "Vazgeç";
            this.btnVazgec.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnVazgec.TextFormatNoPrefix = true;
            this.btnVazgec.Click += new System.EventHandler(this.btnVazgec_Click);
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
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(470, 668);
            this.Controls.Add(this.formPanel);
            this.Controls.Add(this.titleBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form3";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Firma Kayıt";
            this.Load += new System.EventHandler(this.Form3_Load);
            this.titleBar.ResumeLayout(false);
            this.titleBar.PerformLayout();
            this.formPanel.ResumeLayout(false);
            this.formPanel.PerformLayout();
            this.statusPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        public System.Windows.Forms.Panel titleBar;
        public System.Windows.Forms.Label lblFormTitle;
        public System.Windows.Forms.Button closeButton;
        public System.Windows.Forms.Panel formPanel;
        public System.Windows.Forms.TextBox txtFirmaAdi;
        public System.Windows.Forms.TextBox txtTelefonNo;
        public System.Windows.Forms.TextBox txtFaxNo;
        public System.Windows.Forms.TextBox txtAdres;
        public System.Windows.Forms.TextBox txtIl;
        public System.Windows.Forms.TextBox txtIlce;
        public System.Windows.Forms.TextBox txtVergiNo;
        public System.Windows.Forms.TextBox txtVergiDairesi;
        public System.Windows.Forms.TextBox txtFirmaButcesi;
        public System.Windows.Forms.TextBox txtUlkesi;
        public System.Windows.Forms.ComboBox cmbParaBirimi;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.Label label5;
        public System.Windows.Forms.Label label6;
        public System.Windows.Forms.Label label7;
        public System.Windows.Forms.Label label8;
        public System.Windows.Forms.Label label9;
        public System.Windows.Forms.Label label10;
        public System.Windows.Forms.Label label11;
        public Guna.UI2.WinForms.Guna2Button btnfirmakayit;
        public Guna.UI2.WinForms.Guna2Button btnVazgec;
        public System.Windows.Forms.ToolTip formTooltip;
        public System.Windows.Forms.Panel statusPanel;
        public System.Windows.Forms.Label statusLabel;
    }
}