using System.Windows.Forms;
using System;
using System.Drawing;
using Guna.UI2.WinForms;
using System.IO;
using System.Linq;
namespace Veresiye2025
{
    partial class BackupRestoreForm
    {
        public System.Windows.Forms.Label lblBackupInfo;
        public System.Windows.Forms.Label lblRestoreInfo;
        public Guna.UI2.WinForms.Guna2CheckBox chkEnableAutoBackup;
        public MetroFramework.Controls.MetroProgressBar progressBarRestore;
        public System.Windows.Forms.Label lblLastBackupDate;
        public System.Windows.Forms.Label lblLastRestoreInfo;
        public System.Windows.Forms.Label lblLastAutoBackupInfo;
        public Guna.UI2.WinForms.Guna2Panel panelTitleBar;
        public Guna.UI2.WinForms.Guna2Button btnClose;
        public System.Windows.Forms.Label lblTitle;
        public Guna.UI2.WinForms.Guna2TextBox txtBackupPath;
        public Guna.UI2.WinForms.Guna2TextBox txtRestoreFile;
        public Guna.UI2.WinForms.Guna2TextBox txtAutoBackupPath;
        public Guna.UI2.WinForms.Guna2RadioButton radioDaily;
        public Guna.UI2.WinForms.Guna2RadioButton radioWeekly;
        public Guna.UI2.WinForms.Guna2RadioButton radioMonthly;
        public Guna.UI2.WinForms.Guna2RadioButton radioCustom;
        public Guna.UI2.WinForms.Guna2NumericUpDown nudBackupInterval;
        public Guna.UI2.WinForms.Guna2Panel guna2not;
        public Guna.UI2.WinForms.Guna2Button yedekalgozat;
        public Guna.UI2.WinForms.Guna2Button yedekle;
        public Guna.UI2.WinForms.Guna2Button yedekgoster;
        public Guna.UI2.WinForms.Guna2Panel geriyuklenott;
        public Guna.UI2.WinForms.Guna2Button gozat;
        public Guna.UI2.WinForms.Guna2Button geriyukle;
        public Guna.UI2.WinForms.Guna2Panel otonotpanel;
        public Guna.UI2.WinForms.Guna2Button otoayarkaydet;
        public Guna.UI2.WinForms.Guna2Button otogozat;
        public System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.PictureBox pictureBox2;
        public System.Windows.Forms.PictureBox pictureBox3;
        public System.Windows.Forms.FolderBrowserDialog FolderBrowserDialog;
        public System.Windows.Forms.OpenFileDialog openFileDialog;
        public Guna.UI2.WinForms.Guna2TabControl tabControl;
        public System.Windows.Forms.TabPage tabBackup;
        public System.Windows.Forms.TabPage tabRestore;
        public System.Windows.Forms.TabPage tabAutoBackup;
        public System.Windows.Forms.TabPage tabCloudBackup;
        public Guna.UI2.WinForms.Guna2HtmlLabel infoBackupLabel;
        public Guna.UI2.WinForms.Guna2HtmlLabel infoRestoreLabel;
        public Guna.UI2.WinForms.Guna2HtmlLabel infoAutoBackupLabel;
        public System.Windows.Forms.Label lblEstimatedSize;
        public Guna.UI2.WinForms.Guna2Button btnCalculateSize;
        public Guna.UI2.WinForms.Guna2Button btnShowCalendar;
        public Guna.UI2.WinForms.Guna2DateTimePicker datePicker;
        public Guna.UI2.WinForms.Guna2Panel cloudLeftPanel;
        public Guna.UI2.WinForms.Guna2Panel cloudRightPanel;
        public Guna.UI2.WinForms.Guna2ComboBox cmbCloudService;
        public Guna.UI2.WinForms.Guna2TextBox txtCloudFolder;
        public Guna.UI2.WinForms.Guna2Button btnCloudConnect;
        public Guna.UI2.WinForms.Guna2Button btnCloudBackupNow;
        public System.Windows.Forms.Label lblCloudStatus;
        public Guna.UI2.WinForms.Guna2TextBox txtFTPServer;
        public Guna.UI2.WinForms.Guna2TextBox txtFTPUsername;
        public Guna.UI2.WinForms.Guna2TextBox txtFTPPassword;
        public Guna.UI2.WinForms.Guna2Button btnFTPConnect;
        public Guna.UI2.WinForms.Guna2Button btninfo2;
        // Yedekleme Durumu Özeti paneli
        //public System.Windows.Forms.Panel panelYedeklemeDurumu;
        public System.Windows.Forms.Label lblYedeklemeDurumuBaslik;
        public System.Windows.Forms.Label lblSonYedekBaslik;
        public System.Windows.Forms.Label lblSonYedekDeger;
        public System.Windows.Forms.Label lblToplamYedekBaslik;
        public System.Windows.Forms.Label lblToplamYedekDeger;
        public System.Windows.Forms.Label lblSonrakiOtoBaslik;
        public System.Windows.Forms.Label lblSonrakiOtoDeger;
        public System.Windows.Forms.Label lblTavsiyeBaslik;
        public System.Windows.Forms.Label lblTavsiyeDeger;
        public Guna.UI2.WinForms.Guna2Panel panelYedeklemeDurumu;

        public void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BackupRestoreForm));
            this.panelTitleBar = new Guna.UI2.WinForms.Guna2Panel();
            this.btnClose = new Guna.UI2.WinForms.Guna2Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.tabControl = new Guna.UI2.WinForms.Guna2TabControl();
            this.tabBackup = new System.Windows.Forms.TabPage();
            this.datePicker = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.lblBackupInfo = new System.Windows.Forms.Label();
            this.txtBackupPath = new Guna.UI2.WinForms.Guna2TextBox();
            this.yedekalgozat = new Guna.UI2.WinForms.Guna2Button();
            this.yedekle = new Guna.UI2.WinForms.Guna2Button();
            this.lblLastBackupDate = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.yedekgoster = new Guna.UI2.WinForms.Guna2Button();
            this.guna2not = new Guna.UI2.WinForms.Guna2Panel();
            this.infoBackupLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.btnShowCalendar = new Guna.UI2.WinForms.Guna2Button();
            this.btnCalculateSize = new Guna.UI2.WinForms.Guna2Button();
            this.lblEstimatedSize = new System.Windows.Forms.Label();
            this.tabRestore = new System.Windows.Forms.TabPage();
            this.lblRestoreInfo = new System.Windows.Forms.Label();
            this.txtRestoreFile = new Guna.UI2.WinForms.Guna2TextBox();
            this.gozat = new Guna.UI2.WinForms.Guna2Button();
            this.geriyukle = new Guna.UI2.WinForms.Guna2Button();
            this.lblLastRestoreInfo = new System.Windows.Forms.Label();
            this.progressBarRestore = new MetroFramework.Controls.MetroProgressBar();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.geriyuklenott = new Guna.UI2.WinForms.Guna2Panel();
            this.infoRestoreLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.tabAutoBackup = new System.Windows.Forms.TabPage();
            this.chkEnableAutoBackup = new Guna.UI2.WinForms.Guna2CheckBox();
            this.radioDaily = new Guna.UI2.WinForms.Guna2RadioButton();
            this.radioWeekly = new Guna.UI2.WinForms.Guna2RadioButton();
            this.radioMonthly = new Guna.UI2.WinForms.Guna2RadioButton();
            this.radioCustom = new Guna.UI2.WinForms.Guna2RadioButton();
            this.nudBackupInterval = new Guna.UI2.WinForms.Guna2NumericUpDown();
            this.txtAutoBackupPath = new Guna.UI2.WinForms.Guna2TextBox();
            this.otogozat = new Guna.UI2.WinForms.Guna2Button();
            this.otoayarkaydet = new Guna.UI2.WinForms.Guna2Button();
            this.lblLastAutoBackupInfo = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.otonotpanel = new Guna.UI2.WinForms.Guna2Panel();
            this.infoAutoBackupLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.tabCloudBackup = new System.Windows.Forms.TabPage();
            this.cloudLeftPanel = new Guna.UI2.WinForms.Guna2Panel();
            this.btninfo2 = new Guna.UI2.WinForms.Guna2Button();
            this.lblCloudStatus = new System.Windows.Forms.Label();
            this.btnCloudBackupNow = new Guna.UI2.WinForms.Guna2Button();
            this.btnCloudConnect = new Guna.UI2.WinForms.Guna2Button();
            this.txtCloudFolder = new Guna.UI2.WinForms.Guna2TextBox();
            this.cmbCloudService = new Guna.UI2.WinForms.Guna2ComboBox();
            this.cloudRightPanel = new Guna.UI2.WinForms.Guna2Panel();
            this.btnFTPConnect = new Guna.UI2.WinForms.Guna2Button();
            this.txtFTPPassword = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtFTPUsername = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtFTPServer = new Guna.UI2.WinForms.Guna2TextBox();
            this.FolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.panelYedeklemeDurumu = new Guna.UI2.WinForms.Guna2Panel();
            this.lblYedeklemeDurumuBaslik = new System.Windows.Forms.Label();
            this.lblSonYedekBaslik = new System.Windows.Forms.Label();
            this.lblSonYedekDeger = new System.Windows.Forms.Label();
            this.lblToplamYedekBaslik = new System.Windows.Forms.Label();
            this.lblToplamYedekDeger = new System.Windows.Forms.Label();
            this.lblSonrakiOtoBaslik = new System.Windows.Forms.Label();
            this.lblSonrakiOtoDeger = new System.Windows.Forms.Label();
            this.lblTavsiyeBaslik = new System.Windows.Forms.Label();
            this.lblTavsiyeDeger = new System.Windows.Forms.Label();
            this.gerinoktasi = new Guna.UI2.WinForms.Guna2Button();
            this.panelTitleBar.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabBackup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.guna2not.SuspendLayout();
            this.tabRestore.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.geriyuklenott.SuspendLayout();
            this.tabAutoBackup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBackupInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.otonotpanel.SuspendLayout();
            this.tabCloudBackup.SuspendLayout();
            this.cloudLeftPanel.SuspendLayout();
            this.cloudRightPanel.SuspendLayout();
            this.panelYedeklemeDurumu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTitleBar
            // 
            this.panelTitleBar.Controls.Add(this.btnClose);
            this.panelTitleBar.Controls.Add(this.lblTitle);
            this.panelTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitleBar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.panelTitleBar.Location = new System.Drawing.Point(0, 0);
            this.panelTitleBar.Name = "panelTitleBar";
            this.panelTitleBar.Size = new System.Drawing.Size(950, 40);
            this.panelTitleBar.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Animated = true;
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.BorderRadius = 10;
            this.btnClose.FillColor = System.Drawing.Color.Transparent;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(920, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(30, 40);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "X";
            this.btnClose.UseTransparentBackground = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click_1);
            this.btnClose.MouseEnter += new System.EventHandler(this.btnClose_MouseEnter);
            this.btnClose.MouseLeave += new System.EventHandler(this.btnClose_MouseLeave);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(12, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(351, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Veritabanı Yedekleme ve Geri Yükleme";
            // 
            // tabControl
            // 
            this.tabControl.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabControl.Controls.Add(this.tabBackup);
            this.tabControl.Controls.Add(this.tabRestore);
            this.tabControl.Controls.Add(this.tabAutoBackup);
            this.tabControl.Controls.Add(this.tabCloudBackup);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.ItemSize = new System.Drawing.Size(180, 40);
            this.tabControl.Location = new System.Drawing.Point(0, 40);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(950, 465);
            this.tabControl.TabButtonHoverState.BorderColor = System.Drawing.Color.Empty;
            this.tabControl.TabButtonHoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(150)))), ((int)(((byte)(255)))));
            this.tabControl.TabButtonHoverState.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.tabControl.TabButtonHoverState.ForeColor = System.Drawing.Color.White;
            this.tabControl.TabButtonHoverState.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(52)))), ((int)(((byte)(70)))));
            this.tabControl.TabButtonIdleState.BorderColor = System.Drawing.Color.Empty;
            this.tabControl.TabButtonIdleState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.tabControl.TabButtonIdleState.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.tabControl.TabButtonIdleState.ForeColor = System.Drawing.Color.White;
            this.tabControl.TabButtonIdleState.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(42)))), ((int)(((byte)(57)))));
            this.tabControl.TabButtonSelectedState.BorderColor = System.Drawing.Color.Empty;
            this.tabControl.TabButtonSelectedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(230)))));
            this.tabControl.TabButtonSelectedState.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.tabControl.TabButtonSelectedState.ForeColor = System.Drawing.Color.White;
            this.tabControl.TabButtonSelectedState.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(132)))), ((int)(((byte)(255)))));
            this.tabControl.TabButtonSize = new System.Drawing.Size(180, 40);
            this.tabControl.TabIndex = 0;
            this.tabControl.TabMenuBackColor = System.Drawing.Color.Transparent;
            // 
            // tabBackup
            // 
            this.tabBackup.BackColor = System.Drawing.Color.White;
            this.tabBackup.Controls.Add(this.datePicker);
            this.tabBackup.Controls.Add(this.lblBackupInfo);
            this.tabBackup.Controls.Add(this.txtBackupPath);
            this.tabBackup.Controls.Add(this.yedekalgozat);
            this.tabBackup.Controls.Add(this.yedekle);
            this.tabBackup.Controls.Add(this.lblLastBackupDate);
            this.tabBackup.Controls.Add(this.pictureBox2);
            this.tabBackup.Controls.Add(this.yedekgoster);
            this.tabBackup.Controls.Add(this.guna2not);
            this.tabBackup.Controls.Add(this.btnShowCalendar);
            this.tabBackup.Controls.Add(this.btnCalculateSize);
            this.tabBackup.Controls.Add(this.lblEstimatedSize);
            this.tabBackup.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.tabBackup.Location = new System.Drawing.Point(184, 4);
            this.tabBackup.Name = "tabBackup";
            this.tabBackup.Size = new System.Drawing.Size(762, 457);
            this.tabBackup.TabIndex = 0;
            this.tabBackup.Text = "Yedek Al";
            // 
            // datePicker
            // 
            this.datePicker.BorderRadius = 5;
            this.datePicker.Checked = true;
            this.datePicker.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.datePicker.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.datePicker.Location = new System.Drawing.Point(26, 199);
            this.datePicker.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.datePicker.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.datePicker.Name = "datePicker";
            this.datePicker.Size = new System.Drawing.Size(300, 36);
            this.datePicker.TabIndex = 0;
            this.datePicker.Value = new System.DateTime(2025, 5, 13, 22, 41, 0, 391);
            this.datePicker.ValueChanged += new System.EventHandler(this.datePicker_ValueChanged);
            // 
            // lblBackupInfo
            // 
            this.lblBackupInfo.AutoSize = true;
            this.lblBackupInfo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblBackupInfo.Location = new System.Drawing.Point(20, 20);
            this.lblBackupInfo.Name = "lblBackupInfo";
            this.lblBackupInfo.Size = new System.Drawing.Size(307, 23);
            this.lblBackupInfo.TabIndex = 0;
            this.lblBackupInfo.Text = "Yedek almak için hedef klasörü seçin.";
            // 
            // txtBackupPath
            // 
            this.txtBackupPath.BorderRadius = 10;
            this.txtBackupPath.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtBackupPath.DefaultText = "";
            this.txtBackupPath.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtBackupPath.Location = new System.Drawing.Point(20, 50);
            this.txtBackupPath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtBackupPath.Name = "txtBackupPath";
            this.txtBackupPath.PlaceholderText = "Yedekleme yolunu seçin...";
            this.txtBackupPath.SelectedText = "";
            this.txtBackupPath.Size = new System.Drawing.Size(512, 36);
            this.txtBackupPath.TabIndex = 1;
            // 
            // yedekalgozat
            // 
            this.yedekalgozat.BorderRadius = 10;
            this.yedekalgozat.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.yedekalgozat.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.yedekalgozat.ForeColor = System.Drawing.Color.White;
            this.yedekalgozat.Image = global::Veresiye2025.Properties.Resources.sorguhareket;
            this.yedekalgozat.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.yedekalgozat.ImageOffset = new System.Drawing.Point(-5, 0);
            this.yedekalgozat.ImageSize = new System.Drawing.Size(24, 24);
            this.yedekalgozat.Location = new System.Drawing.Point(20, 100);
            this.yedekalgozat.Name = "yedekalgozat";
            this.yedekalgozat.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.yedekalgozat.Size = new System.Drawing.Size(131, 40);
            this.yedekalgozat.TabIndex = 2;
            this.yedekalgozat.Text = "Gözat";
            this.yedekalgozat.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.yedekalgozat.TextOffset = new System.Drawing.Point(-10, 0);
            this.yedekalgozat.Click += new System.EventHandler(this.yedekalgozat_Click);
            // 
            // yedekle
            // 
            this.yedekle.BorderRadius = 10;
            this.yedekle.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.yedekle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.yedekle.ForeColor = System.Drawing.Color.White;
            this.yedekle.Image = global::Veresiye2025.Properties.Resources.save_icon;
            this.yedekle.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.yedekle.ImageOffset = new System.Drawing.Point(-5, 0);
            this.yedekle.ImageSize = new System.Drawing.Size(24, 24);
            this.yedekle.Location = new System.Drawing.Point(181, 100);
            this.yedekle.Name = "yedekle";
            this.yedekle.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.yedekle.Size = new System.Drawing.Size(145, 40);
            this.yedekle.TabIndex = 3;
            this.yedekle.Text = "Yedekle";
            this.yedekle.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.yedekle.TextOffset = new System.Drawing.Point(-10, 0);
            this.yedekle.Click += new System.EventHandler(this.yedekle_Click);
            // 
            // lblLastBackupDate
            // 
            this.lblLastBackupDate.AutoSize = true;
            this.lblLastBackupDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblLastBackupDate.ForeColor = System.Drawing.Color.Red;
            this.lblLastBackupDate.Location = new System.Drawing.Point(20, 150);
            this.lblLastBackupDate.Name = "lblLastBackupDate";
            this.lblLastBackupDate.Size = new System.Drawing.Size(227, 20);
            this.lblLastBackupDate.TabIndex = 4;
            this.lblLastBackupDate.Text = "Son Yedek Alma: Henüz alınmadı";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Veresiye2025.Properties.Resources.backup;
            this.pictureBox2.Location = new System.Drawing.Point(560, 6);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(180, 180);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // yedekgoster
            // 
            this.yedekgoster.BorderRadius = 10;
            this.yedekgoster.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.yedekgoster.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.yedekgoster.ForeColor = System.Drawing.Color.White;
            this.yedekgoster.Image = global::Veresiye2025.Properties.Resources.klasor;
            this.yedekgoster.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.yedekgoster.ImageOffset = new System.Drawing.Point(-5, 0);
            this.yedekgoster.ImageSize = new System.Drawing.Size(24, 24);
            this.yedekgoster.Location = new System.Drawing.Point(560, 195);
            this.yedekgoster.Name = "yedekgoster";
            this.yedekgoster.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.yedekgoster.Size = new System.Drawing.Size(180, 40);
            this.yedekgoster.TabIndex = 6;
            this.yedekgoster.Text = "Yedek Göster";
            this.yedekgoster.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.yedekgoster.TextOffset = new System.Drawing.Point(-10, 0);
            this.yedekgoster.Click += new System.EventHandler(this.yedekgoster_Click);
            // 
            // guna2not
            // 
            this.guna2not.BackColor = System.Drawing.Color.Transparent;
            this.guna2not.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.guna2not.BorderRadius = 15;
            this.guna2not.BorderThickness = 1;
            this.guna2not.Controls.Add(this.infoBackupLabel);
            this.guna2not.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.guna2not.Location = new System.Drawing.Point(20, 260);
            this.guna2not.Name = "guna2not";
            this.guna2not.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.guna2not.ShadowDecoration.Depth = 5;
            this.guna2not.ShadowDecoration.Enabled = true;
            this.guna2not.Size = new System.Drawing.Size(720, 185);
            this.guna2not.TabIndex = 7;
            // 
            // infoBackupLabel
            // 
            this.infoBackupLabel.BackColor = System.Drawing.Color.Transparent;
            this.infoBackupLabel.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.infoBackupLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.infoBackupLabel.Location = new System.Drawing.Point(15, 14);
            this.infoBackupLabel.Name = "infoBackupLabel";
            this.infoBackupLabel.Size = new System.Drawing.Size(646, 127);
            this.infoBackupLabel.TabIndex = 0;
            this.infoBackupLabel.Text = resources.GetString("infoBackupLabel.Text");
            // 
            // btnShowCalendar
            // 
            this.btnShowCalendar.BorderRadius = 10;
            this.btnShowCalendar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnShowCalendar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnShowCalendar.ForeColor = System.Drawing.Color.White;
            this.btnShowCalendar.Image = global::Veresiye2025.Properties.Resources._return;
            this.btnShowCalendar.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnShowCalendar.ImageOffset = new System.Drawing.Point(5, 0);
            this.btnShowCalendar.Location = new System.Drawing.Point(352, 195);
            this.btnShowCalendar.Name = "btnShowCalendar";
            this.btnShowCalendar.Size = new System.Drawing.Size(180, 40);
            this.btnShowCalendar.TabIndex = 8;
            this.btnShowCalendar.Text = "Yedekleme Takvimi";
            this.btnShowCalendar.TextOffset = new System.Drawing.Point(10, 0);
            this.btnShowCalendar.Click += new System.EventHandler(this.btnShowCalendar_Click);
            // 
            // btnCalculateSize
            // 
            this.btnCalculateSize.BorderRadius = 10;
            this.btnCalculateSize.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnCalculateSize.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCalculateSize.ForeColor = System.Drawing.Color.White;
            this.btnCalculateSize.Image = global::Veresiye2025.Properties.Resources.analiz;
            this.btnCalculateSize.Location = new System.Drawing.Point(352, 100);
            this.btnCalculateSize.Name = "btnCalculateSize";
            this.btnCalculateSize.Size = new System.Drawing.Size(180, 40);
            this.btnCalculateSize.TabIndex = 9;
            this.btnCalculateSize.Text = "Boyutu Hesapla";
            this.btnCalculateSize.Click += new System.EventHandler(this.btnCalculateSize_Click);
            // 
            // lblEstimatedSize
            // 
            this.lblEstimatedSize.AutoSize = true;
            this.lblEstimatedSize.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblEstimatedSize.Location = new System.Drawing.Point(20, 176);
            this.lblEstimatedSize.Name = "lblEstimatedSize";
            this.lblEstimatedSize.Size = new System.Drawing.Size(248, 20);
            this.lblEstimatedSize.TabIndex = 10;
            this.lblEstimatedSize.Text = "Tahmini Boyut: Henüz hesaplanmadı";
            // 
            // tabRestore
            // 
            this.tabRestore.BackColor = System.Drawing.Color.White;
            this.tabRestore.Controls.Add(this.gerinoktasi);
            this.tabRestore.Controls.Add(this.lblRestoreInfo);
            this.tabRestore.Controls.Add(this.txtRestoreFile);
            this.tabRestore.Controls.Add(this.gozat);
            this.tabRestore.Controls.Add(this.geriyukle);
            this.tabRestore.Controls.Add(this.lblLastRestoreInfo);
            this.tabRestore.Controls.Add(this.progressBarRestore);
            this.tabRestore.Controls.Add(this.pictureBox1);
            this.tabRestore.Controls.Add(this.geriyuklenott);
            this.tabRestore.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.tabRestore.Location = new System.Drawing.Point(184, 4);
            this.tabRestore.Name = "tabRestore";
            this.tabRestore.Size = new System.Drawing.Size(762, 457);
            this.tabRestore.TabIndex = 1;
            this.tabRestore.Text = "Geri Yükle";
            // 
            // lblRestoreInfo
            // 
            this.lblRestoreInfo.AutoSize = true;
            this.lblRestoreInfo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblRestoreInfo.Location = new System.Drawing.Point(20, 20);
            this.lblRestoreInfo.Name = "lblRestoreInfo";
            this.lblRestoreInfo.Size = new System.Drawing.Size(259, 23);
            this.lblRestoreInfo.TabIndex = 0;
            this.lblRestoreInfo.Text = "Geri yüklemek için dosya seçin.";
            // 
            // txtRestoreFile
            // 
            this.txtRestoreFile.BorderRadius = 10;
            this.txtRestoreFile.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtRestoreFile.DefaultText = "";
            this.txtRestoreFile.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtRestoreFile.Location = new System.Drawing.Point(20, 50);
            this.txtRestoreFile.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtRestoreFile.Name = "txtRestoreFile";
            this.txtRestoreFile.PlaceholderText = "Dosya yolunu seçin...";
            this.txtRestoreFile.SelectedText = "";
            this.txtRestoreFile.Size = new System.Drawing.Size(526, 36);
            this.txtRestoreFile.TabIndex = 1;
            // 
            // gozat
            // 
            this.gozat.BorderRadius = 10;
            this.gozat.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.gozat.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.gozat.ForeColor = System.Drawing.Color.White;
            this.gozat.Image = global::Veresiye2025.Properties.Resources.sorguhareket;
            this.gozat.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.gozat.ImageOffset = new System.Drawing.Point(-5, 0);
            this.gozat.ImageSize = new System.Drawing.Size(24, 24);
            this.gozat.Location = new System.Drawing.Point(20, 100);
            this.gozat.Name = "gozat";
            this.gozat.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.gozat.Size = new System.Drawing.Size(120, 40);
            this.gozat.TabIndex = 2;
            this.gozat.Text = "Gözat";
            this.gozat.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.gozat.TextOffset = new System.Drawing.Point(-10, 0);
            this.gozat.Click += new System.EventHandler(this.gozat_Click);
            // 
            // geriyukle
            // 
            this.geriyukle.BorderRadius = 10;
            this.geriyukle.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.geriyukle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.geriyukle.ForeColor = System.Drawing.Color.White;
            this.geriyukle.Image = global::Veresiye2025.Properties.Resources._return;
            this.geriyukle.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.geriyukle.ImageOffset = new System.Drawing.Point(-5, 0);
            this.geriyukle.ImageSize = new System.Drawing.Size(24, 24);
            this.geriyukle.Location = new System.Drawing.Point(150, 100);
            this.geriyukle.Name = "geriyukle";
            this.geriyukle.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.geriyukle.Size = new System.Drawing.Size(160, 40);
            this.geriyukle.TabIndex = 3;
            this.geriyukle.Text = "Geri Yükle";
            this.geriyukle.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.geriyukle.TextOffset = new System.Drawing.Point(-10, 0);
            this.geriyukle.Click += new System.EventHandler(this.geriyukle_Click);
            // 
            // lblLastRestoreInfo
            // 
            this.lblLastRestoreInfo.AutoSize = true;
            this.lblLastRestoreInfo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblLastRestoreInfo.ForeColor = System.Drawing.Color.Red;
            this.lblLastRestoreInfo.Location = new System.Drawing.Point(20, 150);
            this.lblLastRestoreInfo.Name = "lblLastRestoreInfo";
            this.lblLastRestoreInfo.Size = new System.Drawing.Size(244, 20);
            this.lblLastRestoreInfo.TabIndex = 4;
            this.lblLastRestoreInfo.Text = "Son Geri Yükleme: Henüz yapılmadı";
            // 
            // progressBarRestore
            // 
            this.progressBarRestore.Location = new System.Drawing.Point(20, 190);
            this.progressBarRestore.Name = "progressBarRestore";
            this.progressBarRestore.Size = new System.Drawing.Size(420, 30);
            this.progressBarRestore.Style = MetroFramework.MetroColorStyle.Blue;
            this.progressBarRestore.TabIndex = 5;
            this.progressBarRestore.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Veresiye2025.Properties.Resources.restore;
            this.pictureBox1.Location = new System.Drawing.Point(560, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(180, 180);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // geriyuklenott
            // 
            this.geriyuklenott.BackColor = System.Drawing.Color.Transparent;
            this.geriyuklenott.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.geriyuklenott.BorderRadius = 15;
            this.geriyuklenott.BorderThickness = 1;
            this.geriyuklenott.Controls.Add(this.infoRestoreLabel);
            this.geriyuklenott.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.geriyuklenott.Location = new System.Drawing.Point(20, 260);
            this.geriyuklenott.Name = "geriyuklenott";
            this.geriyuklenott.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.geriyuklenott.ShadowDecoration.Depth = 5;
            this.geriyuklenott.ShadowDecoration.Enabled = true;
            this.geriyuklenott.Size = new System.Drawing.Size(720, 185);
            this.geriyuklenott.TabIndex = 7;
            // 
            // infoRestoreLabel
            // 
            this.infoRestoreLabel.BackColor = System.Drawing.Color.Transparent;
            this.infoRestoreLabel.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.infoRestoreLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.infoRestoreLabel.Location = new System.Drawing.Point(15, 15);
            this.infoRestoreLabel.Name = "infoRestoreLabel";
            this.infoRestoreLabel.Size = new System.Drawing.Size(599, 148);
            this.infoRestoreLabel.TabIndex = 0;
            this.infoRestoreLabel.Text = resources.GetString("infoRestoreLabel.Text");
            // 
            // tabAutoBackup
            // 
            this.tabAutoBackup.BackColor = System.Drawing.Color.White;
            this.tabAutoBackup.Controls.Add(this.chkEnableAutoBackup);
            this.tabAutoBackup.Controls.Add(this.radioDaily);
            this.tabAutoBackup.Controls.Add(this.radioWeekly);
            this.tabAutoBackup.Controls.Add(this.radioMonthly);
            this.tabAutoBackup.Controls.Add(this.radioCustom);
            this.tabAutoBackup.Controls.Add(this.nudBackupInterval);
            this.tabAutoBackup.Controls.Add(this.txtAutoBackupPath);
            this.tabAutoBackup.Controls.Add(this.otogozat);
            this.tabAutoBackup.Controls.Add(this.otoayarkaydet);
            this.tabAutoBackup.Controls.Add(this.lblLastAutoBackupInfo);
            this.tabAutoBackup.Controls.Add(this.pictureBox3);
            this.tabAutoBackup.Controls.Add(this.otonotpanel);
            this.tabAutoBackup.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.tabAutoBackup.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabAutoBackup.Location = new System.Drawing.Point(184, 4);
            this.tabAutoBackup.Name = "tabAutoBackup";
            this.tabAutoBackup.Size = new System.Drawing.Size(762, 457);
            this.tabAutoBackup.TabIndex = 2;
            this.tabAutoBackup.Text = "Oto Yedek Ayarları";
            // 
            // chkEnableAutoBackup
            // 
            this.chkEnableAutoBackup.BackColor = System.Drawing.Color.Transparent;
            this.chkEnableAutoBackup.CheckedState.BorderRadius = 0;
            this.chkEnableAutoBackup.CheckedState.BorderThickness = 0;
            this.chkEnableAutoBackup.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.chkEnableAutoBackup.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.chkEnableAutoBackup.Location = new System.Drawing.Point(20, 20);
            this.chkEnableAutoBackup.Name = "chkEnableAutoBackup";
            this.chkEnableAutoBackup.Size = new System.Drawing.Size(400, 30);
            this.chkEnableAutoBackup.TabIndex = 0;
            this.chkEnableAutoBackup.Text = "Otomatik Yedeklemeyi Aktif Et";
            this.chkEnableAutoBackup.UncheckedState.BorderRadius = 0;
            this.chkEnableAutoBackup.UncheckedState.BorderThickness = 0;
            this.chkEnableAutoBackup.UseVisualStyleBackColor = false;
            // 
            // radioDaily
            // 
            this.radioDaily.BackColor = System.Drawing.Color.Transparent;
            this.radioDaily.CheckedState.BorderThickness = 0;
            this.radioDaily.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.radioDaily.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.radioDaily.Location = new System.Drawing.Point(50, 60);
            this.radioDaily.Name = "radioDaily";
            this.radioDaily.Size = new System.Drawing.Size(80, 30);
            this.radioDaily.TabIndex = 1;
            this.radioDaily.Text = "Günlük";
            this.radioDaily.UncheckedState.BorderThickness = 0;
            this.radioDaily.UseVisualStyleBackColor = false;
            // 
            // radioWeekly
            // 
            this.radioWeekly.BackColor = System.Drawing.Color.Transparent;
            this.radioWeekly.CheckedState.BorderThickness = 0;
            this.radioWeekly.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.radioWeekly.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.radioWeekly.Location = new System.Drawing.Point(140, 60);
            this.radioWeekly.Name = "radioWeekly";
            this.radioWeekly.Size = new System.Drawing.Size(80, 30);
            this.radioWeekly.TabIndex = 2;
            this.radioWeekly.Text = "Haftalık";
            this.radioWeekly.UncheckedState.BorderThickness = 0;
            this.radioWeekly.UseVisualStyleBackColor = false;
            // 
            // radioMonthly
            // 
            this.radioMonthly.BackColor = System.Drawing.Color.Transparent;
            this.radioMonthly.CheckedState.BorderThickness = 0;
            this.radioMonthly.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.radioMonthly.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.radioMonthly.Location = new System.Drawing.Point(230, 60);
            this.radioMonthly.Name = "radioMonthly";
            this.radioMonthly.Size = new System.Drawing.Size(80, 30);
            this.radioMonthly.TabIndex = 3;
            this.radioMonthly.Text = "Aylık";
            this.radioMonthly.UncheckedState.BorderThickness = 0;
            this.radioMonthly.UseVisualStyleBackColor = false;
            // 
            // radioCustom
            // 
            this.radioCustom.BackColor = System.Drawing.Color.Transparent;
            this.radioCustom.CheckedState.BorderThickness = 0;
            this.radioCustom.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.radioCustom.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.radioCustom.Location = new System.Drawing.Point(50, 100);
            this.radioCustom.Name = "radioCustom";
            this.radioCustom.Size = new System.Drawing.Size(200, 30);
            this.radioCustom.TabIndex = 4;
            this.radioCustom.Text = "Yedekleme Aralığı (Dakika)";
            this.radioCustom.UncheckedState.BorderThickness = 0;
            this.radioCustom.UseVisualStyleBackColor = false;
            // 
            // nudBackupInterval
            // 
            this.nudBackupInterval.BackColor = System.Drawing.Color.Transparent;
            this.nudBackupInterval.BorderRadius = 8;
            this.nudBackupInterval.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.nudBackupInterval.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.nudBackupInterval.Location = new System.Drawing.Point(366, 102);
            this.nudBackupInterval.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.nudBackupInterval.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.nudBackupInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudBackupInterval.Name = "nudBackupInterval";
            this.nudBackupInterval.Size = new System.Drawing.Size(74, 30);
            this.nudBackupInterval.TabIndex = 5;
            this.nudBackupInterval.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // txtAutoBackupPath
            // 
            this.txtAutoBackupPath.BorderRadius = 10;
            this.txtAutoBackupPath.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtAutoBackupPath.DefaultText = "";
            this.txtAutoBackupPath.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtAutoBackupPath.Location = new System.Drawing.Point(20, 140);
            this.txtAutoBackupPath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtAutoBackupPath.Name = "txtAutoBackupPath";
            this.txtAutoBackupPath.PlaceholderText = "Otomatik yedekleme yolunu seçin...";
            this.txtAutoBackupPath.SelectedText = "";
            this.txtAutoBackupPath.Size = new System.Drawing.Size(420, 36);
            this.txtAutoBackupPath.TabIndex = 6;
            // 
            // otogozat
            // 
            this.otogozat.BorderRadius = 10;
            this.otogozat.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.otogozat.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.otogozat.ForeColor = System.Drawing.Color.White;
            this.otogozat.Image = global::Veresiye2025.Properties.Resources.sorguhareket;
            this.otogozat.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.otogozat.ImageOffset = new System.Drawing.Point(-5, 0);
            this.otogozat.ImageSize = new System.Drawing.Size(24, 24);
            this.otogozat.Location = new System.Drawing.Point(20, 190);
            this.otogozat.Name = "otogozat";
            this.otogozat.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.otogozat.Size = new System.Drawing.Size(120, 40);
            this.otogozat.TabIndex = 7;
            this.otogozat.Text = "Gözat";
            this.otogozat.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.otogozat.TextOffset = new System.Drawing.Point(-10, 0);
            this.otogozat.Click += new System.EventHandler(this.otogozat_Click);
            // 
            // otoayarkaydet
            // 
            this.otoayarkaydet.BorderRadius = 10;
            this.otoayarkaydet.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.otoayarkaydet.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.otoayarkaydet.ForeColor = System.Drawing.Color.White;
            this.otoayarkaydet.Image = global::Veresiye2025.Properties.Resources.open;
            this.otoayarkaydet.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.otoayarkaydet.ImageOffset = new System.Drawing.Point(-5, 0);
            this.otoayarkaydet.ImageSize = new System.Drawing.Size(24, 24);
            this.otoayarkaydet.Location = new System.Drawing.Point(150, 190);
            this.otoayarkaydet.Name = "otoayarkaydet";
            this.otoayarkaydet.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.otoayarkaydet.Size = new System.Drawing.Size(160, 40);
            this.otoayarkaydet.TabIndex = 8;
            this.otoayarkaydet.Text = "Ayarı Kaydet";
            this.otoayarkaydet.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.otoayarkaydet.TextOffset = new System.Drawing.Point(-10, 0);
            this.otoayarkaydet.Click += new System.EventHandler(this.otoayarkaydet_Click);
            // 
            // lblLastAutoBackupInfo
            // 
            this.lblLastAutoBackupInfo.AutoSize = true;
            this.lblLastAutoBackupInfo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblLastAutoBackupInfo.ForeColor = System.Drawing.Color.Red;
            this.lblLastAutoBackupInfo.Location = new System.Drawing.Point(20, 240);
            this.lblLastAutoBackupInfo.Name = "lblLastAutoBackupInfo";
            this.lblLastAutoBackupInfo.Size = new System.Drawing.Size(225, 20);
            this.lblLastAutoBackupInfo.TabIndex = 9;
            this.lblLastAutoBackupInfo.Text = "Son Oto Yedek: Henüz yapılmadı";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::Veresiye2025.Properties.Resources.otoyedek;
            this.pictureBox3.Location = new System.Drawing.Point(560, 6);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(180, 180);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 10;
            this.pictureBox3.TabStop = false;
            // 
            // otonotpanel
            // 
            this.otonotpanel.BackColor = System.Drawing.Color.Transparent;
            this.otonotpanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.otonotpanel.BorderRadius = 15;
            this.otonotpanel.BorderThickness = 1;
            this.otonotpanel.Controls.Add(this.infoAutoBackupLabel);
            this.otonotpanel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.otonotpanel.Location = new System.Drawing.Point(20, 260);
            this.otonotpanel.Name = "otonotpanel";
            this.otonotpanel.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.otonotpanel.ShadowDecoration.Depth = 5;
            this.otonotpanel.ShadowDecoration.Enabled = true;
            this.otonotpanel.Size = new System.Drawing.Size(720, 185);
            this.otonotpanel.TabIndex = 11;
            // 
            // infoAutoBackupLabel
            // 
            this.infoAutoBackupLabel.BackColor = System.Drawing.Color.Transparent;
            this.infoAutoBackupLabel.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.infoAutoBackupLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.infoAutoBackupLabel.Location = new System.Drawing.Point(15, 15);
            this.infoAutoBackupLabel.Name = "infoAutoBackupLabel";
            this.infoAutoBackupLabel.Size = new System.Drawing.Size(682, 148);
            this.infoAutoBackupLabel.TabIndex = 0;
            this.infoAutoBackupLabel.Text = resources.GetString("infoAutoBackupLabel.Text");
            // 
            // tabCloudBackup
            // 
            this.tabCloudBackup.BackColor = System.Drawing.Color.White;
            this.tabCloudBackup.Controls.Add(this.cloudLeftPanel);
            this.tabCloudBackup.Controls.Add(this.cloudRightPanel);
            this.tabCloudBackup.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.tabCloudBackup.Location = new System.Drawing.Point(184, 4);
            this.tabCloudBackup.Name = "tabCloudBackup";
            this.tabCloudBackup.Size = new System.Drawing.Size(762, 457);
            this.tabCloudBackup.TabIndex = 3;
            this.tabCloudBackup.Text = "Bulut Yedekleme";
            // 
            // cloudLeftPanel
            // 
            this.cloudLeftPanel.BorderColor = System.Drawing.Color.LightGray;
            this.cloudLeftPanel.BorderRadius = 10;
            this.cloudLeftPanel.BorderThickness = 1;
            this.cloudLeftPanel.Controls.Add(this.btninfo2);
            this.cloudLeftPanel.Controls.Add(this.lblCloudStatus);
            this.cloudLeftPanel.Controls.Add(this.btnCloudBackupNow);
            this.cloudLeftPanel.Controls.Add(this.btnCloudConnect);
            this.cloudLeftPanel.Controls.Add(this.txtCloudFolder);
            this.cloudLeftPanel.Controls.Add(this.cmbCloudService);
            this.cloudLeftPanel.FillColor = System.Drawing.Color.White;
            this.cloudLeftPanel.Location = new System.Drawing.Point(20, 20);
            this.cloudLeftPanel.Name = "cloudLeftPanel";
            this.cloudLeftPanel.Size = new System.Drawing.Size(340, 400);
            this.cloudLeftPanel.TabIndex = 0;
            // 
            // btninfo2
            // 
            this.btninfo2.BorderRadius = 5;
            this.btninfo2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btninfo2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btninfo2.ForeColor = System.Drawing.Color.White;
            this.btninfo2.Location = new System.Drawing.Point(185, 195);
            this.btninfo2.Name = "btninfo2";
            this.btninfo2.Size = new System.Drawing.Size(40, 40);
            this.btninfo2.TabIndex = 5;
            this.btninfo2.Text = "?";
            this.btninfo2.Click += new System.EventHandler(this.btninfo2_Click);
            // 
            // lblCloudStatus
            // 
            this.lblCloudStatus.AutoSize = true;
            this.lblCloudStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCloudStatus.ForeColor = System.Drawing.Color.Red;
            this.lblCloudStatus.Location = new System.Drawing.Point(15, 238);
            this.lblCloudStatus.Name = "lblCloudStatus";
            this.lblCloudStatus.Size = new System.Drawing.Size(133, 20);
            this.lblCloudStatus.TabIndex = 4;
            this.lblCloudStatus.Text = "Durum: Bağlı değil";
            // 
            // btnCloudBackupNow
            // 
            this.btnCloudBackupNow.BorderRadius = 5;
            this.btnCloudBackupNow.Enabled = false;
            this.btnCloudBackupNow.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnCloudBackupNow.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCloudBackupNow.ForeColor = System.Drawing.Color.DimGray;
            this.btnCloudBackupNow.Location = new System.Drawing.Point(15, 195);
            this.btnCloudBackupNow.Name = "btnCloudBackupNow";
            this.btnCloudBackupNow.Size = new System.Drawing.Size(150, 40);
            this.btnCloudBackupNow.TabIndex = 3;
            this.btnCloudBackupNow.Text = "Şimdi Yedekle";
            this.btnCloudBackupNow.Click += new System.EventHandler(this.btnCloudBackupNow_Click);
            // 
            // btnCloudConnect
            // 
            this.btnCloudConnect.BorderRadius = 5;
            this.btnCloudConnect.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnCloudConnect.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCloudConnect.ForeColor = System.Drawing.Color.White;
            this.btnCloudConnect.Location = new System.Drawing.Point(15, 143);
            this.btnCloudConnect.Name = "btnCloudConnect";
            this.btnCloudConnect.Size = new System.Drawing.Size(150, 40);
            this.btnCloudConnect.TabIndex = 2;
            this.btnCloudConnect.Text = "Hesaba Bağlan";
            this.btnCloudConnect.Click += new System.EventHandler(this.btnCloudConnect_Click);
            // 
            // txtCloudFolder
            // 
            this.txtCloudFolder.BorderRadius = 5;
            this.txtCloudFolder.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtCloudFolder.DefaultText = "";
            this.txtCloudFolder.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtCloudFolder.Location = new System.Drawing.Point(15, 93);
            this.txtCloudFolder.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtCloudFolder.Name = "txtCloudFolder";
            this.txtCloudFolder.PlaceholderText = "Klasör adı (isteğe bağlı)";
            this.txtCloudFolder.SelectedText = "";
            this.txtCloudFolder.Size = new System.Drawing.Size(310, 36);
            this.txtCloudFolder.TabIndex = 1;
            // 
            // cmbCloudService
            // 
            this.cmbCloudService.BackColor = System.Drawing.Color.Transparent;
            this.cmbCloudService.BorderRadius = 5;
            this.cmbCloudService.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbCloudService.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCloudService.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbCloudService.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbCloudService.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbCloudService.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cmbCloudService.ItemHeight = 30;
            this.cmbCloudService.Items.AddRange(new object[] {
            "Google Drive",
            "Dropbox",
            "OneDrive"});
            this.cmbCloudService.Location = new System.Drawing.Point(15, 18);
            this.cmbCloudService.Name = "cmbCloudService";
            this.cmbCloudService.Size = new System.Drawing.Size(310, 36);
            this.cmbCloudService.StartIndex = 0;
            this.cmbCloudService.TabIndex = 0;
            // 
            // cloudRightPanel
            // 
            this.cloudRightPanel.BorderColor = System.Drawing.Color.LightGray;
            this.cloudRightPanel.BorderRadius = 10;
            this.cloudRightPanel.BorderThickness = 1;
            this.cloudRightPanel.Controls.Add(this.btnFTPConnect);
            this.cloudRightPanel.Controls.Add(this.txtFTPPassword);
            this.cloudRightPanel.Controls.Add(this.txtFTPUsername);
            this.cloudRightPanel.Controls.Add(this.txtFTPServer);
            this.cloudRightPanel.FillColor = System.Drawing.Color.White;
            this.cloudRightPanel.Location = new System.Drawing.Point(380, 20);
            this.cloudRightPanel.Name = "cloudRightPanel";
            this.cloudRightPanel.Size = new System.Drawing.Size(340, 400);
            this.cloudRightPanel.TabIndex = 1;
            // 
            // btnFTPConnect
            // 
            this.btnFTPConnect.BorderRadius = 5;
            this.btnFTPConnect.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnFTPConnect.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnFTPConnect.ForeColor = System.Drawing.Color.White;
            this.btnFTPConnect.Location = new System.Drawing.Point(16, 218);
            this.btnFTPConnect.Name = "btnFTPConnect";
            this.btnFTPConnect.Size = new System.Drawing.Size(150, 36);
            this.btnFTPConnect.TabIndex = 3;
            this.btnFTPConnect.Text = "FTP\'ye Bağlan";
            this.btnFTPConnect.Click += new System.EventHandler(this.btnFTPConnect_Click);
            // 
            // txtFTPPassword
            // 
            this.txtFTPPassword.BorderRadius = 5;
            this.txtFTPPassword.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtFTPPassword.DefaultText = "";
            this.txtFTPPassword.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtFTPPassword.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtFTPPassword.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtFTPPassword.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtFTPPassword.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtFTPPassword.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtFTPPassword.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtFTPPassword.Location = new System.Drawing.Point(16, 168);
            this.txtFTPPassword.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtFTPPassword.Name = "txtFTPPassword";
            this.txtFTPPassword.PasswordChar = '•';
            this.txtFTPPassword.PlaceholderText = "Şifre";
            this.txtFTPPassword.SelectedText = "";
            this.txtFTPPassword.Size = new System.Drawing.Size(310, 36);
            this.txtFTPPassword.TabIndex = 2;
            // 
            // txtFTPUsername
            // 
            this.txtFTPUsername.BorderRadius = 5;
            this.txtFTPUsername.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtFTPUsername.DefaultText = "";
            this.txtFTPUsername.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtFTPUsername.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtFTPUsername.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtFTPUsername.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtFTPUsername.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtFTPUsername.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtFTPUsername.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtFTPUsername.Location = new System.Drawing.Point(16, 93);
            this.txtFTPUsername.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtFTPUsername.Name = "txtFTPUsername";
            this.txtFTPUsername.PlaceholderText = "Kullanıcı Adı";
            this.txtFTPUsername.SelectedText = "";
            this.txtFTPUsername.Size = new System.Drawing.Size(310, 36);
            this.txtFTPUsername.TabIndex = 1;
            // 
            // txtFTPServer
            // 
            this.txtFTPServer.BorderRadius = 5;
            this.txtFTPServer.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtFTPServer.DefaultText = "";
            this.txtFTPServer.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtFTPServer.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtFTPServer.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtFTPServer.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtFTPServer.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtFTPServer.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtFTPServer.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtFTPServer.Location = new System.Drawing.Point(16, 18);
            this.txtFTPServer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtFTPServer.Name = "txtFTPServer";
            this.txtFTPServer.PlaceholderText = "ftp://orneksite.com";
            this.txtFTPServer.SelectedText = "";
            this.txtFTPServer.Size = new System.Drawing.Size(310, 36);
            this.txtFTPServer.TabIndex = 0;
            // 
            // panelYedeklemeDurumu
            // 
            this.panelYedeklemeDurumu.BackColor = System.Drawing.Color.Transparent;
            this.panelYedeklemeDurumu.BorderColor = System.Drawing.Color.Transparent;
            this.panelYedeklemeDurumu.BorderRadius = 15;
            this.panelYedeklemeDurumu.BorderThickness = 1;
            this.panelYedeklemeDurumu.Controls.Add(this.lblYedeklemeDurumuBaslik);
            this.panelYedeklemeDurumu.Controls.Add(this.lblSonYedekBaslik);
            this.panelYedeklemeDurumu.Controls.Add(this.lblSonYedekDeger);
            this.panelYedeklemeDurumu.Controls.Add(this.lblToplamYedekBaslik);
            this.panelYedeklemeDurumu.Controls.Add(this.lblToplamYedekDeger);
            this.panelYedeklemeDurumu.Controls.Add(this.lblSonrakiOtoBaslik);
            this.panelYedeklemeDurumu.Controls.Add(this.lblSonrakiOtoDeger);
            this.panelYedeklemeDurumu.Controls.Add(this.lblTavsiyeBaslik);
            this.panelYedeklemeDurumu.Controls.Add(this.lblTavsiyeDeger);
            this.panelYedeklemeDurumu.FillColor = System.Drawing.Color.White;
            this.panelYedeklemeDurumu.Location = new System.Drawing.Point(0, 194);
            this.panelYedeklemeDurumu.Name = "panelYedeklemeDurumu";
            this.panelYedeklemeDurumu.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.panelYedeklemeDurumu.ShadowDecoration.Depth = 2;
            this.panelYedeklemeDurumu.ShadowDecoration.Enabled = true;
            this.panelYedeklemeDurumu.Size = new System.Drawing.Size(180, 310);
            this.panelYedeklemeDurumu.TabIndex = 20;
            // 
            // lblYedeklemeDurumuBaslik
            // 
            this.lblYedeklemeDurumuBaslik.BackColor = System.Drawing.Color.Red;
            this.lblYedeklemeDurumuBaslik.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblYedeklemeDurumuBaslik.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblYedeklemeDurumuBaslik.ForeColor = System.Drawing.Color.White;
            this.lblYedeklemeDurumuBaslik.Location = new System.Drawing.Point(0, 0);
            this.lblYedeklemeDurumuBaslik.Name = "lblYedeklemeDurumuBaslik";
            this.lblYedeklemeDurumuBaslik.Size = new System.Drawing.Size(180, 22);
            this.lblYedeklemeDurumuBaslik.TabIndex = 0;
            this.lblYedeklemeDurumuBaslik.Text = "YEDEKLEME BİLGİSİ";
            this.lblYedeklemeDurumuBaslik.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSonYedekBaslik
            // 
            this.lblSonYedekBaslik.AutoSize = true;
            this.lblSonYedekBaslik.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblSonYedekBaslik.Location = new System.Drawing.Point(6, 28);
            this.lblSonYedekBaslik.Name = "lblSonYedekBaslik";
            this.lblSonYedekBaslik.Size = new System.Drawing.Size(136, 25);
            this.lblSonYedekBaslik.TabIndex = 1;
            this.lblSonYedekBaslik.Text = "✅ Son Yedek:";
            // 
            // lblSonYedekDeger
            // 
            this.lblSonYedekDeger.AutoSize = true;
            this.lblSonYedekDeger.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblSonYedekDeger.Location = new System.Drawing.Point(13, 65);
            this.lblSonYedekDeger.Name = "lblSonYedekDeger";
            this.lblSonYedekDeger.Size = new System.Drawing.Size(129, 23);
            this.lblSonYedekDeger.TabIndex = 2;
            this.lblSonYedekDeger.Text = "Henüz alınmadı";
            // 
            // lblToplamYedekBaslik
            // 
            this.lblToplamYedekBaslik.AutoSize = true;
            this.lblToplamYedekBaslik.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblToplamYedekBaslik.Location = new System.Drawing.Point(7, 100);
            this.lblToplamYedekBaslik.Name = "lblToplamYedekBaslik";
            this.lblToplamYedekBaslik.Size = new System.Drawing.Size(164, 25);
            this.lblToplamYedekBaslik.TabIndex = 3;
            this.lblToplamYedekBaslik.Text = "📊 Toplam Yedek:";
            // 
            // lblToplamYedekDeger
            // 
            this.lblToplamYedekDeger.AutoSize = true;
            this.lblToplamYedekDeger.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblToplamYedekDeger.Location = new System.Drawing.Point(13, 138);
            this.lblToplamYedekDeger.Name = "lblToplamYedekDeger";
            this.lblToplamYedekDeger.Size = new System.Drawing.Size(122, 23);
            this.lblToplamYedekDeger.TabIndex = 4;
            this.lblToplamYedekDeger.Text = "0 yedek (0 MB)";
            // 
            // lblSonrakiOtoBaslik
            // 
            this.lblSonrakiOtoBaslik.AutoSize = true;
            this.lblSonrakiOtoBaslik.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblSonrakiOtoBaslik.Location = new System.Drawing.Point(8, 179);
            this.lblSonrakiOtoBaslik.Name = "lblSonrakiOtoBaslik";
            this.lblSonrakiOtoBaslik.Size = new System.Drawing.Size(149, 25);
            this.lblSonrakiOtoBaslik.TabIndex = 5;
            this.lblSonrakiOtoBaslik.Text = "⏰ Sonraki Oto:";
            // 
            // lblSonrakiOtoDeger
            // 
            this.lblSonrakiOtoDeger.AutoSize = true;
            this.lblSonrakiOtoDeger.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblSonrakiOtoDeger.Location = new System.Drawing.Point(13, 214);
            this.lblSonrakiOtoDeger.Name = "lblSonrakiOtoDeger";
            this.lblSonrakiOtoDeger.Size = new System.Drawing.Size(105, 23);
            this.lblSonrakiOtoDeger.TabIndex = 6;
            this.lblSonrakiOtoDeger.Text = "Ayarlanmadı";
            // 
            // lblTavsiyeBaslik
            // 
            this.lblTavsiyeBaslik.AutoSize = true;
            this.lblTavsiyeBaslik.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblTavsiyeBaslik.Location = new System.Drawing.Point(12, 242);
            this.lblTavsiyeBaslik.Name = "lblTavsiyeBaslik";
            this.lblTavsiyeBaslik.Size = new System.Drawing.Size(110, 25);
            this.lblTavsiyeBaslik.TabIndex = 7;
            this.lblTavsiyeBaslik.Text = "⚠️ Tavsiye:";
            // 
            // lblTavsiyeDeger
            // 
            this.lblTavsiyeDeger.AutoSize = true;
            this.lblTavsiyeDeger.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblTavsiyeDeger.Location = new System.Drawing.Point(13, 275);
            this.lblTavsiyeDeger.Name = "lblTavsiyeDeger";
            this.lblTavsiyeDeger.Size = new System.Drawing.Size(148, 23);
            this.lblTavsiyeDeger.TabIndex = 8;
            this.lblTavsiyeDeger.Text = "Düzenli yedek alın";
            // 
            // gerinoktasi
            // 
            this.gerinoktasi.BorderRadius = 10;
            this.gerinoktasi.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.gerinoktasi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.gerinoktasi.ForeColor = System.Drawing.Color.White;
            this.gerinoktasi.Image = global::Veresiye2025.Properties.Resources._return;
            this.gerinoktasi.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.gerinoktasi.ImageOffset = new System.Drawing.Point(-5, 0);
            this.gerinoktasi.ImageSize = new System.Drawing.Size(24, 24);
            this.gerinoktasi.Location = new System.Drawing.Point(316, 100);
            this.gerinoktasi.Name = "gerinoktasi";
            this.gerinoktasi.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.gerinoktasi.Size = new System.Drawing.Size(229, 40);
            this.gerinoktasi.TabIndex = 8;
            this.gerinoktasi.Text = "Geri Yükleme Noktası";
            this.gerinoktasi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.gerinoktasi.TextOffset = new System.Drawing.Point(-10, 0);
            this.gerinoktasi.Click += new System.EventHandler(this.gerinoktasi_Click);
            // 
            // BackupRestoreForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(950, 505);
            this.Controls.Add(this.panelYedeklemeDurumu);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.panelTitleBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BackupRestoreForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.panelTitleBar.ResumeLayout(false);
            this.panelTitleBar.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabBackup.ResumeLayout(false);
            this.tabBackup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.guna2not.ResumeLayout(false);
            this.guna2not.PerformLayout();
            this.tabRestore.ResumeLayout(false);
            this.tabRestore.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.geriyuklenott.ResumeLayout(false);
            this.geriyuklenott.PerformLayout();
            this.tabAutoBackup.ResumeLayout(false);
            this.tabAutoBackup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBackupInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.otonotpanel.ResumeLayout(false);
            this.otonotpanel.PerformLayout();
            this.tabCloudBackup.ResumeLayout(false);
            this.cloudLeftPanel.ResumeLayout(false);
            this.cloudLeftPanel.PerformLayout();
            this.cloudRightPanel.ResumeLayout(false);
            this.panelYedeklemeDurumu.ResumeLayout(false);
            this.panelYedeklemeDurumu.PerformLayout();
            this.ResumeLayout(false);

        }

        public void InitializeYedeklemeDurumuPanel()
        {
            // Bu metot artık InitializeComponent içinde gerçekleştiriliyor
            // Yedekleme durumu panelindeki bilgileri güncelliyoruz
            UpdateYedeklemeDurumuPanel();
        }

        public Guna2Button gerinoktasi;
    }
}