using System.Windows.Forms;
using System;

namespace Veresiye2025
{
    partial class UnifiedForm4
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UnifiedForm4));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelTop = new System.Windows.Forms.Panel();
            this.menuContainer = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.Firmalar = new System.Windows.Forms.ToolStripMenuItem();
            this.firmaHesaplarıToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.işlemlerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hareketlerF3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.yedekİşlemleriToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.araçlarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hesapMakinesiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.çıkışToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowControlsPanel = new System.Windows.Forms.Panel();
            this.btnMinimize = new Guna.UI2.WinForms.Guna2Button();
            this.btnClose = new Guna.UI2.WinForms.Guna2Button();
            this.panelFilters = new System.Windows.Forms.Panel();
            this.lblSorgu = new System.Windows.Forms.Label();
            this.cmbSorgu = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblHesap = new System.Windows.Forms.Label();
            this.cmbHesap = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblFiltre = new System.Windows.Forms.Label();
            this.cmbFiltre = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblUnvan = new System.Windows.Forms.Label();
            this.txtUnvan = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnTopla = new Guna.UI2.WinForms.Guna2Button();
            this.yazdir = new Guna.UI2.WinForms.Guna2Button();
            this.panelActions = new System.Windows.Forms.Panel();
            this.btnEkle = new Guna.UI2.WinForms.Guna2Button();
            this.btnDegistir = new Guna.UI2.WinForms.Guna2Button();
            this.btnSil = new Guna.UI2.WinForms.Guna2Button();
            this.alarmbildir = new Guna.UI2.WinForms.Guna2Button();
            this.btnCarininHareketleriniAc = new Guna.UI2.WinForms.Guna2Button();
            this.pnlYaklasanAlarmlar = new System.Windows.Forms.Panel();
            this.flpAlarmIcerik = new System.Windows.Forms.FlowLayoutPanel();
            this.lblAlarmBaslik = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panelContent = new System.Windows.Forms.Panel();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
            this.printDialog = new System.Windows.Forms.PrintDialog();
            this.guna2MessageDialog = new Guna.UI2.WinForms.Guna2MessageDialog();
            this.panelTop.SuspendLayout();
            this.menuContainer.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.windowControlsPanel.SuspendLayout();
            this.panelFilters.SuspendLayout();
            this.panelActions.SuspendLayout();
            this.pnlYaklasanAlarmlar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panelContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.MidnightBlue;
            this.panelTop.Controls.Add(this.menuContainer);
            this.panelTop.Controls.Add(this.windowControlsPanel);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(893, 40);
            this.panelTop.TabIndex = 0;
            this.panelTop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PanelTop_MouseDown);
            this.panelTop.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PanelTop_MouseMove);
            this.panelTop.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PanelTop_MouseUp);
            // 
            // menuContainer
            // 
            this.menuContainer.BackColor = System.Drawing.Color.MidnightBlue;
            this.menuContainer.Controls.Add(this.menuStrip1);
            this.menuContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuContainer.Location = new System.Drawing.Point(0, 0);
            this.menuContainer.Name = "menuContainer";
            this.menuContainer.Size = new System.Drawing.Size(739, 40);
            this.menuContainer.TabIndex = 40;
            // 
            // menuStrip1
            // 
            this.menuStrip1.AutoSize = false;
            this.menuStrip1.BackColor = System.Drawing.Color.MidnightBlue;
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Firmalar,
            this.işlemlerToolStripMenuItem,
            this.yedekİşlemleriToolStripMenuItem,
            this.araçlarToolStripMenuItem,
            this.hesapMakinesiToolStripMenuItem,
            this.çıkışToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(15, 2, 0, 2);
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(739, 40);
            this.menuStrip1.TabIndex = 17;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // Firmalar
            // 
            this.Firmalar.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.firmaHesaplarıToolStripMenuItem});
            this.Firmalar.ForeColor = System.Drawing.Color.DarkGray;
            this.Firmalar.Name = "Firmalar";
            this.Firmalar.Size = new System.Drawing.Size(80, 36);
            this.Firmalar.Text = "Firmalar";
            // 
            // firmaHesaplarıToolStripMenuItem
            // 
            this.firmaHesaplarıToolStripMenuItem.Name = "firmaHesaplarıToolStripMenuItem";
            this.firmaHesaplarıToolStripMenuItem.Size = new System.Drawing.Size(250, 26);
            this.firmaHesaplarıToolStripMenuItem.Text = "Firma Hesapları";
            // 
            // işlemlerToolStripMenuItem
            // 
            this.işlemlerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hareketlerF3ToolStripMenuItem});
            this.işlemlerToolStripMenuItem.ForeColor = System.Drawing.Color.DarkGray;
            this.işlemlerToolStripMenuItem.Name = "işlemlerToolStripMenuItem";
            this.işlemlerToolStripMenuItem.Size = new System.Drawing.Size(76, 36);
            this.işlemlerToolStripMenuItem.Text = "İşlemler";
            // 
            // hareketlerF3ToolStripMenuItem
            // 
            this.hareketlerF3ToolStripMenuItem.Name = "hareketlerF3ToolStripMenuItem";
            this.hareketlerF3ToolStripMenuItem.Size = new System.Drawing.Size(284, 26);
            this.hareketlerF3ToolStripMenuItem.Text = "Hareketler[F3]";
            // 
            // yedekİşlemleriToolStripMenuItem
            // 
            this.yedekİşlemleriToolStripMenuItem.ForeColor = System.Drawing.Color.DarkGray;
            this.yedekİşlemleriToolStripMenuItem.Name = "yedekİşlemleriToolStripMenuItem";
            this.yedekİşlemleriToolStripMenuItem.Size = new System.Drawing.Size(125, 36);
            this.yedekİşlemleriToolStripMenuItem.Text = "Yedek İşlemleri";
            // 
            // araçlarToolStripMenuItem
            // 
            this.araçlarToolStripMenuItem.ForeColor = System.Drawing.Color.DarkGray;
            this.araçlarToolStripMenuItem.Name = "araçlarToolStripMenuItem";
            this.araçlarToolStripMenuItem.Size = new System.Drawing.Size(72, 36);
            this.araçlarToolStripMenuItem.Text = "Araçlar";
            // 
            // hesapMakinesiToolStripMenuItem
            // 
            this.hesapMakinesiToolStripMenuItem.ForeColor = System.Drawing.Color.DarkGray;
            this.hesapMakinesiToolStripMenuItem.Name = "hesapMakinesiToolStripMenuItem";
            this.hesapMakinesiToolStripMenuItem.Size = new System.Drawing.Size(130, 36);
            this.hesapMakinesiToolStripMenuItem.Text = "Hesap Makinesi";
            // 
            // çıkışToolStripMenuItem
            // 
            this.çıkışToolStripMenuItem.ForeColor = System.Drawing.Color.Crimson;
            this.çıkışToolStripMenuItem.Name = "çıkışToolStripMenuItem";
            this.çıkışToolStripMenuItem.Size = new System.Drawing.Size(54, 36);
            this.çıkışToolStripMenuItem.Text = "Çıkış";
            // 
            // windowControlsPanel
            // 
            this.windowControlsPanel.Controls.Add(this.btnMinimize);
            this.windowControlsPanel.Controls.Add(this.btnClose);
            this.windowControlsPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.windowControlsPanel.Location = new System.Drawing.Point(739, 0);
            this.windowControlsPanel.Name = "windowControlsPanel";
            this.windowControlsPanel.Size = new System.Drawing.Size(154, 40);
            this.windowControlsPanel.TabIndex = 39;
            // 
            // btnMinimize
            // 
            this.btnMinimize.BorderRadius = 12;
            this.btnMinimize.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnMinimize.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnMinimize.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnMinimize.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnMinimize.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(162)))), ((int)(((byte)(243)))));
            this.btnMinimize.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnMinimize.ForeColor = System.Drawing.Color.White;
            this.btnMinimize.Location = new System.Drawing.Point(50, 2);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(35, 35);
            this.btnMinimize.TabIndex = 1;
            this.btnMinimize.Text = "_";
            this.btnMinimize.TextOffset = new System.Drawing.Point(0, -2);
            this.btnMinimize.Click += new System.EventHandler(this.BtnMinimize_Click);
            // 
            // btnClose
            // 
            this.btnClose.BorderRadius = 12;
            this.btnClose.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnClose.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnClose.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnClose.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnClose.FillColor = System.Drawing.Color.Crimson;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(92, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(35, 35);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "X";
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // panelFilters
            // 
            this.panelFilters.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panelFilters.Controls.Add(this.lblSorgu);
            this.panelFilters.Controls.Add(this.cmbSorgu);
            this.panelFilters.Controls.Add(this.lblHesap);
            this.panelFilters.Controls.Add(this.cmbHesap);
            this.panelFilters.Controls.Add(this.lblFiltre);
            this.panelFilters.Controls.Add(this.cmbFiltre);
            this.panelFilters.Controls.Add(this.lblUnvan);
            this.panelFilters.Controls.Add(this.txtUnvan);
            this.panelFilters.Controls.Add(this.btnTopla);
            this.panelFilters.Controls.Add(this.yazdir);
            this.panelFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilters.Location = new System.Drawing.Point(0, 40);
            this.panelFilters.Name = "panelFilters";
            this.panelFilters.Padding = new System.Windows.Forms.Padding(8);
            this.panelFilters.Size = new System.Drawing.Size(893, 88);
            this.panelFilters.TabIndex = 2;
            // 
            // lblSorgu
            // 
            this.lblSorgu.AutoSize = true;
            this.lblSorgu.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblSorgu.Location = new System.Drawing.Point(8, 12);
            this.lblSorgu.Name = "lblSorgu";
            this.lblSorgu.Size = new System.Drawing.Size(53, 19);
            this.lblSorgu.TabIndex = 10;
            this.lblSorgu.Text = "Sorgu:";
            // 
            // cmbSorgu
            // 
            this.cmbSorgu.BackColor = System.Drawing.Color.Transparent;
            this.cmbSorgu.BorderRadius = 6;
            this.cmbSorgu.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSorgu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSorgu.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbSorgu.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbSorgu.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cmbSorgu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cmbSorgu.ItemHeight = 20;
            this.cmbSorgu.Items.AddRange(new object[] {
            "Unvan İle Sorgu",
            "Kod İle Sorgu",
            "İl İle Sorgu"});
            this.cmbSorgu.Location = new System.Drawing.Point(67, 11);
            this.cmbSorgu.Name = "cmbSorgu";
            this.cmbSorgu.Size = new System.Drawing.Size(121, 26);
            this.cmbSorgu.TabIndex = 5;
            // 
            // lblHesap
            // 
            this.lblHesap.AutoSize = true;
            this.lblHesap.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblHesap.Location = new System.Drawing.Point(196, 12);
            this.lblHesap.Name = "lblHesap";
            this.lblHesap.Size = new System.Drawing.Size(55, 19);
            this.lblHesap.TabIndex = 12;
            this.lblHesap.Text = "Hesap:";
            // 
            // cmbHesap
            // 
            this.cmbHesap.BackColor = System.Drawing.Color.Transparent;
            this.cmbHesap.BorderRadius = 6;
            this.cmbHesap.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbHesap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHesap.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbHesap.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbHesap.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cmbHesap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cmbHesap.ItemHeight = 20;
            this.cmbHesap.Items.AddRange(new object[] {
            "Tümü",
            "Aktif Hesaplar",
            "Pasif Hesaplar"});
            this.cmbHesap.Location = new System.Drawing.Point(257, 12);
            this.cmbHesap.Name = "cmbHesap";
            this.cmbHesap.Size = new System.Drawing.Size(203, 26);
            this.cmbHesap.TabIndex = 7;
            // 
            // lblFiltre
            // 
            this.lblFiltre.AutoSize = true;
            this.lblFiltre.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblFiltre.Location = new System.Drawing.Point(488, 12);
            this.lblFiltre.Name = "lblFiltre";
            this.lblFiltre.Size = new System.Drawing.Size(47, 19);
            this.lblFiltre.TabIndex = 13;
            this.lblFiltre.Text = "Filtre:";
            // 
            // cmbFiltre
            // 
            this.cmbFiltre.BackColor = System.Drawing.Color.Transparent;
            this.cmbFiltre.BorderRadius = 6;
            this.cmbFiltre.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbFiltre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFiltre.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbFiltre.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbFiltre.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cmbFiltre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cmbFiltre.ItemHeight = 20;
            this.cmbFiltre.Items.AddRange(new object[] {
            "Tümü..",
            "Sadece Borçlular",
            "Sadece Alacaklılar"});
            this.cmbFiltre.Location = new System.Drawing.Point(536, 12);
            this.cmbFiltre.Name = "cmbFiltre";
            this.cmbFiltre.Size = new System.Drawing.Size(145, 26);
            this.cmbFiltre.TabIndex = 8;
            // 
            // lblUnvan
            // 
            this.lblUnvan.AutoSize = true;
            this.lblUnvan.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblUnvan.Location = new System.Drawing.Point(196, 57);
            this.lblUnvan.Name = "lblUnvan";
            this.lblUnvan.Size = new System.Drawing.Size(55, 19);
            this.lblUnvan.TabIndex = 14;
            this.lblUnvan.Text = "Unvan:";
            // 
            // txtUnvan
            // 
            this.txtUnvan.BorderRadius = 6;
            this.txtUnvan.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtUnvan.DefaultText = "";
            this.txtUnvan.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtUnvan.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtUnvan.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtUnvan.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtUnvan.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtUnvan.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.txtUnvan.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtUnvan.Location = new System.Drawing.Point(257, 44);
            this.txtUnvan.Name = "txtUnvan";
            this.txtUnvan.PlaceholderText = "Arama yapmak için yazın...";
            this.txtUnvan.SelectedText = "";
            this.txtUnvan.Size = new System.Drawing.Size(424, 35);
            this.txtUnvan.TabIndex = 9;
            // 
            // btnTopla
            // 
            this.btnTopla.Animated = true;
            this.btnTopla.AnimatedGIF = true;
            this.btnTopla.BorderRadius = 24;
            this.btnTopla.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTopla.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnTopla.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnTopla.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnTopla.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnTopla.FillColor = System.Drawing.Color.DodgerBlue;
            this.btnTopla.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnTopla.ForeColor = System.Drawing.Color.White;
            this.btnTopla.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnTopla.ImageSize = new System.Drawing.Size(30, 30);
            this.btnTopla.Location = new System.Drawing.Point(687, 11);
            this.btnTopla.Name = "btnTopla";
            this.btnTopla.Size = new System.Drawing.Size(92, 65);
            this.btnTopla.TabIndex = 27;
            this.btnTopla.Text = "Topla";
            this.btnTopla.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // yazdir
            // 
            this.yazdir.Animated = true;
            this.yazdir.AnimatedGIF = true;
            this.yazdir.BorderRadius = 24;
            this.yazdir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.yazdir.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.yazdir.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.yazdir.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.yazdir.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.yazdir.FillColor = System.Drawing.Color.DodgerBlue;
            this.yazdir.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.yazdir.ForeColor = System.Drawing.Color.White;
            this.yazdir.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.yazdir.ImageSize = new System.Drawing.Size(30, 30);
            this.yazdir.Location = new System.Drawing.Point(785, 12);
            this.yazdir.Name = "yazdir";
            this.yazdir.Size = new System.Drawing.Size(99, 64);
            this.yazdir.TabIndex = 28;
            this.yazdir.Text = "Yazdır";
            this.yazdir.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // panelActions
            // 
            this.panelActions.BackColor = System.Drawing.Color.MidnightBlue;
            this.panelActions.Controls.Add(this.btnEkle);
            this.panelActions.Controls.Add(this.btnDegistir);
            this.panelActions.Controls.Add(this.btnSil);
            this.panelActions.Controls.Add(this.alarmbildir);
            this.panelActions.Controls.Add(this.btnCarininHareketleriniAc);
            this.panelActions.Location = new System.Drawing.Point(0, 349);
            this.panelActions.Name = "panelActions";
            this.panelActions.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.panelActions.Size = new System.Drawing.Size(893, 65);
            this.panelActions.TabIndex = 3;
            // 
            // btnEkle
            // 
            this.btnEkle.Animated = true;
            this.btnEkle.AnimatedGIF = true;
            this.btnEkle.AutoRoundedCorners = true;
            this.btnEkle.BorderRadius = 24;
            this.btnEkle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEkle.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnEkle.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnEkle.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnEkle.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnEkle.FillColor = System.Drawing.Color.DodgerBlue;
            this.btnEkle.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.btnEkle.ForeColor = System.Drawing.Color.White;
            this.btnEkle.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnEkle.ImageSize = new System.Drawing.Size(30, 30);
            this.btnEkle.Location = new System.Drawing.Point(12, 8);
            this.btnEkle.Name = "btnEkle";
            this.btnEkle.Size = new System.Drawing.Size(130, 50);
            this.btnEkle.TabIndex = 22;
            this.btnEkle.Text = "Cari Ekle";
            this.btnEkle.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // btnDegistir
            // 
            this.btnDegistir.Animated = true;
            this.btnDegistir.AnimatedGIF = true;
            this.btnDegistir.AutoRoundedCorners = true;
            this.btnDegistir.BorderRadius = 24;
            this.btnDegistir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDegistir.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnDegistir.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnDegistir.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnDegistir.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnDegistir.FillColor = System.Drawing.Color.DodgerBlue;
            this.btnDegistir.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.btnDegistir.ForeColor = System.Drawing.Color.White;
            this.btnDegistir.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnDegistir.ImageSize = new System.Drawing.Size(30, 30);
            this.btnDegistir.Location = new System.Drawing.Point(148, 8);
            this.btnDegistir.Name = "btnDegistir";
            this.btnDegistir.Size = new System.Drawing.Size(130, 50);
            this.btnDegistir.TabIndex = 23;
            this.btnDegistir.Text = "Değiştir";
            this.btnDegistir.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // btnSil
            // 
            this.btnSil.Animated = true;
            this.btnSil.AnimatedGIF = true;
            this.btnSil.AutoRoundedCorners = true;
            this.btnSil.BorderRadius = 24;
            this.btnSil.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSil.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSil.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSil.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSil.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSil.FillColor = System.Drawing.Color.DodgerBlue;
            this.btnSil.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.btnSil.ForeColor = System.Drawing.Color.White;
            this.btnSil.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnSil.ImageSize = new System.Drawing.Size(30, 30);
            this.btnSil.Location = new System.Drawing.Point(284, 8);
            this.btnSil.Name = "btnSil";
            this.btnSil.Size = new System.Drawing.Size(130, 50);
            this.btnSil.TabIndex = 24;
            this.btnSil.Text = "Cari Sil";
            this.btnSil.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // alarmbildir
            // 
            this.alarmbildir.Animated = true;
            this.alarmbildir.AnimatedGIF = true;
            this.alarmbildir.AutoRoundedCorners = true;
            this.alarmbildir.BorderRadius = 24;
            this.alarmbildir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.alarmbildir.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.alarmbildir.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.alarmbildir.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.alarmbildir.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.alarmbildir.FillColor = System.Drawing.Color.DodgerBlue;
            this.alarmbildir.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.alarmbildir.ForeColor = System.Drawing.Color.White;
            this.alarmbildir.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.alarmbildir.ImageSize = new System.Drawing.Size(30, 30);
            this.alarmbildir.Location = new System.Drawing.Point(420, 8);
            this.alarmbildir.Name = "alarmbildir";
            this.alarmbildir.Size = new System.Drawing.Size(152, 50);
            this.alarmbildir.TabIndex = 26;
            this.alarmbildir.Text = "Alarm Kur";
            this.alarmbildir.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // btnCarininHareketleriniAc
            // 
            this.btnCarininHareketleriniAc.Animated = true;
            this.btnCarininHareketleriniAc.AnimatedGIF = true;
            this.btnCarininHareketleriniAc.AutoRoundedCorners = true;
            this.btnCarininHareketleriniAc.BorderRadius = 24;
            this.btnCarininHareketleriniAc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCarininHareketleriniAc.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCarininHareketleriniAc.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCarininHareketleriniAc.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCarininHareketleriniAc.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCarininHareketleriniAc.FillColor = System.Drawing.Color.DodgerBlue;
            this.btnCarininHareketleriniAc.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.btnCarininHareketleriniAc.ForeColor = System.Drawing.Color.White;
            this.btnCarininHareketleriniAc.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnCarininHareketleriniAc.ImageSize = new System.Drawing.Size(30, 30);
            this.btnCarininHareketleriniAc.Location = new System.Drawing.Point(622, 8);
            this.btnCarininHareketleriniAc.Name = "btnCarininHareketleriniAc";
            this.btnCarininHareketleriniAc.Size = new System.Drawing.Size(266, 50);
            this.btnCarininHareketleriniAc.TabIndex = 25;
            this.btnCarininHareketleriniAc.Text = "Carinin Hareketlerini Aç";
            // 
            // pnlYaklasanAlarmlar
            // 
            this.pnlYaklasanAlarmlar.BackColor = System.Drawing.Color.MidnightBlue;
            this.pnlYaklasanAlarmlar.Controls.Add(this.flpAlarmIcerik);
            this.pnlYaklasanAlarmlar.Controls.Add(this.lblAlarmBaslik);
            this.pnlYaklasanAlarmlar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlYaklasanAlarmlar.ForeColor = System.Drawing.Color.White;
            this.pnlYaklasanAlarmlar.Location = new System.Drawing.Point(0, 414);
            this.pnlYaklasanAlarmlar.Name = "pnlYaklasanAlarmlar";
            this.pnlYaklasanAlarmlar.Padding = new System.Windows.Forms.Padding(5);
            this.pnlYaklasanAlarmlar.Size = new System.Drawing.Size(893, 44);
            this.pnlYaklasanAlarmlar.TabIndex = 100;
            // 
            // flpAlarmIcerik
            // 
            this.flpAlarmIcerik.BackColor = System.Drawing.Color.MidnightBlue;
            this.flpAlarmIcerik.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flpAlarmIcerik.ForeColor = System.Drawing.Color.White;
            this.flpAlarmIcerik.Location = new System.Drawing.Point(199, -1);
            this.flpAlarmIcerik.Name = "flpAlarmIcerik";
            this.flpAlarmIcerik.Size = new System.Drawing.Size(689, 40);
            this.flpAlarmIcerik.TabIndex = 102;
            // 
            // lblAlarmBaslik
            // 
            this.lblAlarmBaslik.AutoSize = true;
            this.lblAlarmBaslik.BackColor = System.Drawing.Color.MidnightBlue;
            this.lblAlarmBaslik.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblAlarmBaslik.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblAlarmBaslik.ForeColor = System.Drawing.Color.White;
            this.lblAlarmBaslik.Location = new System.Drawing.Point(5, 5);
            this.lblAlarmBaslik.Name = "lblAlarmBaslik";
            this.lblAlarmBaslik.Padding = new System.Windows.Forms.Padding(5, 0, 10, 0);
            this.lblAlarmBaslik.Size = new System.Drawing.Size(194, 20);
            this.lblAlarmBaslik.TabIndex = 101;
            this.lblAlarmBaslik.Text = "YAKLAŞAN ALARMLAR:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(162)))), ((int)(((byte)(243)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(162)))), ((int)(((byte)(243)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.GridColor = System.Drawing.Color.Silver;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(893, 223);
            this.dataGridView1.TabIndex = 0;
            // 
            // panelContent
            // 
            this.panelContent.BackColor = System.Drawing.Color.MidnightBlue;
            this.panelContent.Controls.Add(this.dataGridView1);
            this.panelContent.Location = new System.Drawing.Point(0, 128);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(896, 286);
            this.panelContent.TabIndex = 4;
            // 
            // printDocument1
            // 
            // 
            // printPreviewDialog
            // 
            this.printPreviewDialog.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog.Enabled = true;
            this.printPreviewDialog.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog.Icon")));
            this.printPreviewDialog.Name = "printPreviewDialog";
            this.printPreviewDialog.Text = "Baskı önizleme";
            this.printPreviewDialog.Visible = false;
            // 
            // printDialog
            // 
            this.printDialog.UseEXDialog = true;
            // 
            // guna2MessageDialog
            // 
            this.guna2MessageDialog.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
            this.guna2MessageDialog.Caption = null;
            this.guna2MessageDialog.Icon = Guna.UI2.WinForms.MessageDialogIcon.Error;
            this.guna2MessageDialog.Parent = null;
            this.guna2MessageDialog.Style = Guna.UI2.WinForms.MessageDialogStyle.Light;
            this.guna2MessageDialog.Text = null;
            // 
            // UnifiedForm4
            // 
            this.BackColor = System.Drawing.Color.MidnightBlue;
            this.ClientSize = new System.Drawing.Size(893, 458);
            this.ControlBox = false;
            this.Controls.Add(this.panelActions);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelFilters);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.pnlYaklasanAlarmlar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UnifiedForm4";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ana Ekran";
            this.panelTop.ResumeLayout(false);
            this.menuContainer.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.windowControlsPanel.ResumeLayout(false);
            this.panelFilters.ResumeLayout(false);
            this.panelFilters.PerformLayout();
            this.panelActions.ResumeLayout(false);
            this.pnlYaklasanAlarmlar.ResumeLayout(false);
            this.pnlYaklasanAlarmlar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panelContent.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        #region Control Declarations
        // Ana Paneller
        public System.Windows.Forms.Panel panelTop;
        public System.Windows.Forms.Panel menuContainer;
        public System.Windows.Forms.Panel windowControlsPanel;
        public System.Windows.Forms.Panel panelFilters;
        public System.Windows.Forms.Panel panelActions;

        // Window Controls
        public Guna.UI2.WinForms.Guna2Button btnMinimize;
        public Guna.UI2.WinForms.Guna2Button btnClose;

        // Menu
        internal System.Windows.Forms.MenuStrip menuStrip1;
        public System.Windows.Forms.ToolStripMenuItem Firmalar;
        public System.Windows.Forms.ToolStripMenuItem firmaHesaplarıToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem işlemlerToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem hareketlerF3ToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem yedekİşlemleriToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem araçlarToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem hesapMakinesiToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem çıkışToolStripMenuItem;

        // Filter Controls
        public Guna.UI2.WinForms.Guna2ComboBox cmbSorgu;
        public Guna.UI2.WinForms.Guna2ComboBox cmbHesap;
        public Guna.UI2.WinForms.Guna2ComboBox cmbFiltre;
        public Guna.UI2.WinForms.Guna2TextBox txtUnvan;
        public System.Windows.Forms.Label lblSorgu;
        public System.Windows.Forms.Label lblHesap;
        public System.Windows.Forms.Label lblFiltre;
        internal System.Windows.Forms.Label lblUnvan;

        // Action Buttons
        public Guna.UI2.WinForms.Guna2Button btnEkle;
        public Guna.UI2.WinForms.Guna2Button btnDegistir;
        public Guna.UI2.WinForms.Guna2Button btnSil;
        public Guna.UI2.WinForms.Guna2Button btnCarininHareketleriniAc;
        public Guna.UI2.WinForms.Guna2Button alarmbildir;
        public Guna.UI2.WinForms.Guna2Button btnTopla;
        public Guna.UI2.WinForms.Guna2Button yazdir;

        // Print Controls
        public System.Drawing.Printing.PrintDocument printDocument1;
        public System.Windows.Forms.PrintPreviewDialog printPreviewDialog;
        public System.Windows.Forms.PrintDialog printDialog;
        public Guna.UI2.WinForms.Guna2MessageDialog guna2MessageDialog;

        // Alarm Panel
        public System.Windows.Forms.Panel pnlYaklasanAlarmlar;
        public System.Windows.Forms.FlowLayoutPanel flpAlarmIcerik;
        public System.Windows.Forms.Label lblAlarmBaslik;

        public DataGridView dataGridView1;
        public Panel panelContent;
        #endregion

        #region Event Handlers
        public void BtnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        public void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void PanelTop_MouseDown(object sender, MouseEventArgs e)
        {
            // Will be implemented in .cs file
        }

        public void PanelTop_MouseMove(object sender, MouseEventArgs e)
        {
            // Will be implemented in .cs file
        }

        public void PanelTop_MouseUp(object sender, MouseEventArgs e)
        {
            // Will be implemented in .cs file
        }
        #endregion
    }
}