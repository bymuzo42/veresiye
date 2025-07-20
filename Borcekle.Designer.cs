namespace Veresiye2025
{
    partial class Borcekle
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Borcekle));
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.btnClose = new Guna.UI2.WinForms.Guna2ControlBox();
            this.lblFormTitle = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox1 = new Guna.UI2.WinForms.Guna2TextBox();
            this.carihesap = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.saat = new System.Windows.Forms.Label();
            this.tarih = new System.Windows.Forms.Label();
            this.sqLiteCommand1 = new System.Data.SQLite.SQLiteCommand();
            this.panel2 = new System.Windows.Forms.Panel();
            this.vadegungir = new Guna.UI2.WinForms.Guna2TextBox();
            this.borcvadelabel = new System.Windows.Forms.Label();
            this.borcuekleyentextbox = new Guna.UI2.WinForms.Guna2TextBox();
            this.borcekleyen = new System.Windows.Forms.Label();
            this.textBox3 = new Guna.UI2.WinForms.Guna2TextBox();
            this.textBox2 = new Guna.UI2.WinForms.Guna2TextBox();
            this.tutar = new System.Windows.Forms.Label();
            this.aciklama = new System.Windows.Forms.Label();
            this.alarm = new Guna.UI2.WinForms.Guna2Button();
            this.kaydet = new Guna.UI2.WinForms.Guna2Button();
            this.vazgec = new Guna.UI2.WinForms.Guna2Button();
            this.pnlHeader.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.Crimson;
            this.pnlHeader.Controls.Add(this.btnClose);
            this.pnlHeader.Controls.Add(this.lblFormTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(461, 40);
            this.pnlHeader.TabIndex = 35;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.FillColor = System.Drawing.Color.Transparent;
            this.btnClose.HoverState.FillColor = System.Drawing.Color.Red;
            this.btnClose.IconColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(421, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(40, 40);
            this.btnClose.TabIndex = 37;
            this.btnClose.Click += new System.EventHandler(this.vazgec_Click);
            // 
            // lblFormTitle
            // 
            this.lblFormTitle.AutoSize = true;
            this.lblFormTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblFormTitle.ForeColor = System.Drawing.Color.White;
            this.lblFormTitle.Location = new System.Drawing.Point(12, 9);
            this.lblFormTitle.Name = "lblFormTitle";
            this.lblFormTitle.Size = new System.Drawing.Size(175, 28);
            this.lblFormTitle.TabIndex = 0;
            this.lblFormTitle.Text = "Hesaba Borç Ekle";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.carihesap);
            this.panel1.Controls.Add(this.dateTimePicker2);
            this.panel1.Controls.Add(this.dateTimePicker1);
            this.panel1.Controls.Add(this.saat);
            this.panel1.Controls.Add(this.tarih);
            this.panel1.Location = new System.Drawing.Point(12, 50);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(433, 89);
            this.panel1.TabIndex = 1;
            // 
            // textBox1
            // 
            this.textBox1.BorderRadius = 5;
            this.textBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox1.DefaultText = "";
            this.textBox1.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.textBox1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.textBox1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textBox1.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textBox1.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.textBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.textBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.textBox1.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.textBox1.Location = new System.Drawing.Point(156, 45);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.PlaceholderText = "";
            this.textBox1.ReadOnly = true;
            this.textBox1.SelectedText = "";
            this.textBox1.Size = new System.Drawing.Size(258, 36);
            this.textBox1.TabIndex = 5;
            // 
            // carihesap
            // 
            this.carihesap.AutoSize = true;
            this.carihesap.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.carihesap.Location = new System.Drawing.Point(3, 53);
            this.carihesap.Name = "carihesap";
            this.carihesap.Size = new System.Drawing.Size(87, 20);
            this.carihesap.TabIndex = 4;
            this.carihesap.Text = "Cari Hesap:";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.CalendarFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.dateTimePicker2.CalendarForeColor = System.Drawing.Color.Black;
            this.dateTimePicker2.CalendarMonthBackground = System.Drawing.Color.White;
            this.dateTimePicker2.CalendarTitleBackColor = System.Drawing.Color.DodgerBlue;
            this.dateTimePicker2.CalendarTitleForeColor = System.Drawing.Color.White;
            this.dateTimePicker2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePicker2.Location = new System.Drawing.Point(317, 12);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(103, 27);
            this.dateTimePicker2.TabIndex = 3;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CalendarFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.dateTimePicker1.CalendarForeColor = System.Drawing.Color.Black;
            this.dateTimePicker1.CalendarMonthBackground = System.Drawing.Color.White;
            this.dateTimePicker1.CalendarTitleBackColor = System.Drawing.Color.DodgerBlue;
            this.dateTimePicker1.CalendarTitleForeColor = System.Drawing.Color.White;
            this.dateTimePicker1.CustomFormat = "dd.MM.yyyy";
            this.dateTimePicker1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(67, 12);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(168, 27);
            this.dateTimePicker1.TabIndex = 2;
            // 
            // saat
            // 
            this.saat.AutoSize = true;
            this.saat.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.saat.Location = new System.Drawing.Point(258, 17);
            this.saat.Name = "saat";
            this.saat.Size = new System.Drawing.Size(43, 20);
            this.saat.TabIndex = 1;
            this.saat.Text = "Saat:";
            // 
            // tarih
            // 
            this.tarih.AutoSize = true;
            this.tarih.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.tarih.Location = new System.Drawing.Point(3, 17);
            this.tarih.Name = "tarih";
            this.tarih.Size = new System.Drawing.Size(48, 20);
            this.tarih.TabIndex = 0;
            this.tarih.Text = "Tarih:";
            // 
            // sqLiteCommand1
            // 
            this.sqLiteCommand1.CommandText = null;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.vadegungir);
            this.panel2.Controls.Add(this.borcvadelabel);
            this.panel2.Controls.Add(this.borcuekleyentextbox);
            this.panel2.Controls.Add(this.borcekleyen);
            this.panel2.Controls.Add(this.textBox3);
            this.panel2.Controls.Add(this.textBox2);
            this.panel2.Controls.Add(this.tutar);
            this.panel2.Controls.Add(this.aciklama);
            this.panel2.Location = new System.Drawing.Point(12, 145);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(433, 194);
            this.panel2.TabIndex = 2;
            // 
            // vadegungir
            // 
            this.vadegungir.BorderRadius = 5;
            this.vadegungir.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.vadegungir.DefaultText = "";
            this.vadegungir.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.vadegungir.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.vadegungir.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.vadegungir.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.vadegungir.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.vadegungir.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.vadegungir.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.vadegungir.Location = new System.Drawing.Point(156, 146);
            this.vadegungir.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.vadegungir.Name = "vadegungir";
            this.vadegungir.PlaceholderText = "";
            this.vadegungir.SelectedText = "";
            this.vadegungir.Size = new System.Drawing.Size(258, 36);
            this.vadegungir.TabIndex = 12;
            // 
            // borcvadelabel
            // 
            this.borcvadelabel.AutoSize = true;
            this.borcvadelabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.borcvadelabel.Location = new System.Drawing.Point(3, 155);
            this.borcvadelabel.Name = "borcvadelabel";
            this.borcvadelabel.Size = new System.Drawing.Size(127, 20);
            this.borcvadelabel.TabIndex = 11;
            this.borcvadelabel.Text = "Vadesi Kaç Gün ?";
            // 
            // borcuekleyentextbox
            // 
            this.borcuekleyentextbox.BorderRadius = 5;
            this.borcuekleyentextbox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.borcuekleyentextbox.DefaultText = "";
            this.borcuekleyentextbox.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.borcuekleyentextbox.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.borcuekleyentextbox.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.borcuekleyentextbox.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.borcuekleyentextbox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.borcuekleyentextbox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.borcuekleyentextbox.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.borcuekleyentextbox.Location = new System.Drawing.Point(156, 100);
            this.borcuekleyentextbox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.borcuekleyentextbox.Name = "borcuekleyentextbox";
            this.borcuekleyentextbox.PlaceholderText = "";
            this.borcuekleyentextbox.SelectedText = "";
            this.borcuekleyentextbox.Size = new System.Drawing.Size(258, 36);
            this.borcuekleyentextbox.TabIndex = 10;
            // 
            // borcekleyen
            // 
            this.borcekleyen.AutoSize = true;
            this.borcekleyen.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.borcekleyen.Location = new System.Drawing.Point(3, 108);
            this.borcekleyen.Name = "borcekleyen";
            this.borcekleyen.Size = new System.Drawing.Size(111, 20);
            this.borcekleyen.TabIndex = 9;
            this.borcekleyen.Text = "Borcu Ekleyen:";
            // 
            // textBox3
            // 
            this.textBox3.BorderRadius = 5;
            this.textBox3.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox3.DefaultText = "0.00";
            this.textBox3.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.textBox3.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.textBox3.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textBox3.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textBox3.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.textBox3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.textBox3.ForeColor = System.Drawing.Color.Maroon;
            this.textBox3.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.textBox3.Location = new System.Drawing.Point(156, 54);
            this.textBox3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox3.Name = "textBox3";
            this.textBox3.PlaceholderText = "";
            this.textBox3.SelectedText = "";
            this.textBox3.Size = new System.Drawing.Size(258, 36);
            this.textBox3.TabIndex = 8;
            this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox3.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            this.textBox3.Enter += new System.EventHandler(this.textBox3_Enter);
            this.textBox3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox3_KeyPress);
            this.textBox3.Leave += new System.EventHandler(this.textBox3_Leave);
            // 
            // textBox2
            // 
            this.textBox2.BorderRadius = 5;
            this.textBox2.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox2.DefaultText = "";
            this.textBox2.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.textBox2.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.textBox2.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textBox2.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textBox2.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.textBox2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBox2.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.textBox2.Location = new System.Drawing.Point(156, 8);
            this.textBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox2.Name = "textBox2";
            this.textBox2.PlaceholderText = "";
            this.textBox2.SelectedText = "";
            this.textBox2.Size = new System.Drawing.Size(258, 36);
            this.textBox2.TabIndex = 6;
            // 
            // tutar
            // 
            this.tutar.AutoSize = true;
            this.tutar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.tutar.Location = new System.Drawing.Point(3, 64);
            this.tutar.Name = "tutar";
            this.tutar.Size = new System.Drawing.Size(51, 20);
            this.tutar.TabIndex = 7;
            this.tutar.Text = "Tutar:";
            // 
            // aciklama
            // 
            this.aciklama.AutoSize = true;
            this.aciklama.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.aciklama.Location = new System.Drawing.Point(3, 23);
            this.aciklama.Name = "aciklama";
            this.aciklama.Size = new System.Drawing.Size(77, 20);
            this.aciklama.TabIndex = 6;
            this.aciklama.Text = "Açıklama:";
            // 
            // alarm
            // 
            this.alarm.BorderRadius = 5;
            this.alarm.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.alarm.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.alarm.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.alarm.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.alarm.FillColor = System.Drawing.Color.Crimson;
            this.alarm.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.alarm.ForeColor = System.Drawing.Color.White;
            this.alarm.Image = global::Veresiye2025.Properties.Resources.clock;
            this.alarm.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.alarm.Location = new System.Drawing.Point(303, 345);
            this.alarm.Name = "alarm";
            this.alarm.Size = new System.Drawing.Size(142, 45);
            this.alarm.TabIndex = 38;
            this.alarm.Text = "Alarm Kur";
            this.alarm.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.alarm.Click += new System.EventHandler(this.alarm_Click);
            // 
            // kaydet
            // 
            this.kaydet.BorderRadius = 5;
            this.kaydet.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.kaydet.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.kaydet.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.kaydet.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.kaydet.FillColor = System.Drawing.Color.Crimson;
            this.kaydet.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.kaydet.ForeColor = System.Drawing.Color.White;
            this.kaydet.Image = ((System.Drawing.Image)(resources.GetObject("kaydet.Image")));
            this.kaydet.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.kaydet.Location = new System.Drawing.Point(148, 345);
            this.kaydet.Name = "kaydet";
            this.kaydet.Size = new System.Drawing.Size(152, 45);
            this.kaydet.TabIndex = 39;
            this.kaydet.Text = "Kaydet";
            this.kaydet.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.kaydet.Click += new System.EventHandler(this.kaydet_Click);
            // 
            // vazgec
            // 
            this.vazgec.BorderRadius = 5;
            this.vazgec.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.vazgec.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.vazgec.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.vazgec.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.vazgec.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.vazgec.FillColor = System.Drawing.Color.Crimson;
            this.vazgec.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.vazgec.ForeColor = System.Drawing.Color.White;
            this.vazgec.Image = ((System.Drawing.Image)(resources.GetObject("vazgec.Image")));
            this.vazgec.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.vazgec.Location = new System.Drawing.Point(12, 345);
            this.vazgec.Name = "vazgec";
            this.vazgec.Size = new System.Drawing.Size(130, 45);
            this.vazgec.TabIndex = 40;
            this.vazgec.Text = "Vazgeç";
            this.vazgec.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.vazgec.Click += new System.EventHandler(this.vazgec_Click);
            // 
            // Borcekle
            // 
            this.AcceptButton = this.kaydet;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.vazgec;
            this.ClientSize = new System.Drawing.Size(461, 403);
            this.Controls.Add(this.vazgec);
            this.Controls.Add(this.kaydet);
            this.Controls.Add(this.alarm);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlHeader);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "Borcekle";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hesaba Borç Ekle";
            this.Load += new System.EventHandler(this.Borcekle_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Label saat;
        public System.Windows.Forms.Label tarih;
        public System.Data.SQLite.SQLiteCommand sqLiteCommand1;
        public System.Windows.Forms.Label carihesap;
        public System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Label tutar;
        public System.Windows.Forms.Label aciklama;
        public System.Windows.Forms.Label borcekleyen;
        public System.Windows.Forms.Label borcvadelabel;
        public System.Windows.Forms.DateTimePicker dateTimePicker1;
        public System.Windows.Forms.DateTimePicker dateTimePicker2;

        // Panel ve başlık eklemeleri
        public System.Windows.Forms.Panel pnlHeader;
        public Guna.UI2.WinForms.Guna2ControlBox btnClose;
        public System.Windows.Forms.Label lblFormTitle;

        // Modernize edilen kontrol referansları
        public Guna.UI2.WinForms.Guna2TextBox textBox1;
        public Guna.UI2.WinForms.Guna2TextBox textBox2;
        public Guna.UI2.WinForms.Guna2TextBox textBox3;
        public Guna.UI2.WinForms.Guna2TextBox borcuekleyentextbox;
        public Guna.UI2.WinForms.Guna2TextBox vadegungir;
        public Guna.UI2.WinForms.Guna2Button vazgec;
        public Guna.UI2.WinForms.Guna2Button kaydet;
        public Guna.UI2.WinForms.Guna2Button alarm;
    }
}