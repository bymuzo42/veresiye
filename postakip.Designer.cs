namespace Veresiye2025
{
    partial class postakip
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.formBorder = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.panelTitleBar = new Guna.UI2.WinForms.Guna2Panel();
            this.btnClose = new Guna.UI2.WinForms.Guna2Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelButtons = new Guna.UI2.WinForms.Guna2Panel();
            this.tumunusil = new Guna.UI2.WinForms.Guna2Button();
            this.excelaktar = new Guna.UI2.WinForms.Guna2Button();
            this.hareketduzenle = new Guna.UI2.WinForms.Guna2Button();
            this.hareketsil = new Guna.UI2.WinForms.Guna2Button();
            this.gunsonuekle = new Guna.UI2.WinForms.Guna2Button();
            this.bankaekle = new Guna.UI2.WinForms.Guna2Button();
            this.panelFilters = new Guna.UI2.WinForms.Guna2Panel();
            this.groupBox4 = new Guna.UI2.WinForms.Guna2GroupBox();
            this.filtrele = new Guna.UI2.WinForms.Guna2Button();
            this.endDatePicker = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.startDatePicker = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.bankaComboBox = new Guna.UI2.WinForms.Guna2ComboBox();
            this.panelInfo = new Guna.UI2.WinForms.Guna2Panel();
            this.groupBox3 = new Guna.UI2.WinForms.Guna2GroupBox();
            this.lblToplamCiro = new System.Windows.Forms.Label();
            this.lblToplamBloke = new System.Windows.Forms.Label();
            this.groupBox2 = new Guna.UI2.WinForms.Guna2GroupBox();
            this.lblYarinGecen = new System.Windows.Forms.Label();
            this.lblBugunGecen = new System.Windows.Forms.Label();
            this.groupBox1 = new Guna.UI2.WinForms.Guna2GroupBox();
            this.eklicihazgoster2 = new System.Windows.Forms.Label();
            this.kayitlibanka = new System.Windows.Forms.Label();
            this.panelGridView = new Guna.UI2.WinForms.Guna2Panel();
            this.dataGridView1 = new Guna.UI2.WinForms.Guna2DataGridView();
            this.panelPagination = new Guna.UI2.WinForms.Guna2Panel();
            this.hangisayfa = new System.Windows.Forms.Label();
            this.sonrakisayfa = new Guna.UI2.WinForms.Guna2Button();
            this.oncekisayfa = new Guna.UI2.WinForms.Guna2Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panelTitleBar.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.panelFilters.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.panelInfo.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panelGridView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panelPagination.SuspendLayout();
            this.SuspendLayout();
            // 
            // formBorder
            // 
            this.formBorder.BorderRadius = 20;
            this.formBorder.TargetControl = this;
            // 
            // panelTitleBar
            // 
            this.panelTitleBar.BorderRadius = 5;
            this.panelTitleBar.Controls.Add(this.btnClose);
            this.panelTitleBar.Controls.Add(this.lblTitle);
            this.panelTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitleBar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.panelTitleBar.Location = new System.Drawing.Point(10, 10);
            this.panelTitleBar.Name = "panelTitleBar";
            this.panelTitleBar.Size = new System.Drawing.Size(1180, 40);
            this.panelTitleBar.TabIndex = 0;
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
            this.btnClose.Location = new System.Drawing.Point(1140, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(40, 40);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "X";
            this.btnClose.UseTransparentBackground = true;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(12, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(233, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Günlük POS Takip Ekranı";
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.tumunusil);
            this.panelButtons.Controls.Add(this.excelaktar);
            this.panelButtons.Controls.Add(this.hareketduzenle);
            this.panelButtons.Controls.Add(this.hareketsil);
            this.panelButtons.Controls.Add(this.gunsonuekle);
            this.panelButtons.Controls.Add(this.bankaekle);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelButtons.Location = new System.Drawing.Point(10, 50);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(1180, 60);
            this.panelButtons.TabIndex = 1;
            // 
            // tumunusil
            // 
            this.tumunusil.Animated = true;
            this.tumunusil.BorderRadius = 10;
            this.tumunusil.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.tumunusil.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.tumunusil.ForeColor = System.Drawing.Color.White;
            this.tumunusil.Location = new System.Drawing.Point(900, 10);
            this.tumunusil.Name = "tumunusil";
            this.tumunusil.Size = new System.Drawing.Size(160, 40);
            this.tumunusil.TabIndex = 5;
            this.tumunusil.Text = "Tümünü Temizle";
            // 
            // excelaktar
            // 
            this.excelaktar.Animated = true;
            this.excelaktar.BorderRadius = 10;
            this.excelaktar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.excelaktar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.excelaktar.ForeColor = System.Drawing.Color.White;
            this.excelaktar.Location = new System.Drawing.Point(720, 10);
            this.excelaktar.Name = "excelaktar";
            this.excelaktar.Size = new System.Drawing.Size(160, 40);
            this.excelaktar.TabIndex = 4;
            this.excelaktar.Text = "Excel\'e Aktar";
            // 
            // hareketduzenle
            // 
            this.hareketduzenle.Animated = true;
            this.hareketduzenle.BorderRadius = 10;
            this.hareketduzenle.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.hareketduzenle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.hareketduzenle.ForeColor = System.Drawing.Color.White;
            this.hareketduzenle.Location = new System.Drawing.Point(540, 10);
            this.hareketduzenle.Name = "hareketduzenle";
            this.hareketduzenle.Size = new System.Drawing.Size(160, 40);
            this.hareketduzenle.TabIndex = 3;
            this.hareketduzenle.Text = "Düzenle";
            // 
            // hareketsil
            // 
            this.hareketsil.Animated = true;
            this.hareketsil.BorderRadius = 10;
            this.hareketsil.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.hareketsil.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.hareketsil.ForeColor = System.Drawing.Color.White;
            this.hareketsil.Location = new System.Drawing.Point(360, 10);
            this.hareketsil.Name = "hareketsil";
            this.hareketsil.Size = new System.Drawing.Size(160, 40);
            this.hareketsil.TabIndex = 2;
            this.hareketsil.Text = "Hareketi Sil";
            // 
            // gunsonuekle
            // 
            this.gunsonuekle.Animated = true;
            this.gunsonuekle.BorderRadius = 10;
            this.gunsonuekle.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.gunsonuekle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.gunsonuekle.ForeColor = System.Drawing.Color.White;
            this.gunsonuekle.Location = new System.Drawing.Point(180, 10);
            this.gunsonuekle.Name = "gunsonuekle";
            this.gunsonuekle.Size = new System.Drawing.Size(160, 40);
            this.gunsonuekle.TabIndex = 1;
            this.gunsonuekle.Text = "Günsonu Ekle";
            // 
            // bankaekle
            // 
            this.bankaekle.Animated = true;
            this.bankaekle.BorderRadius = 10;
            this.bankaekle.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.bankaekle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.bankaekle.ForeColor = System.Drawing.Color.White;
            this.bankaekle.Location = new System.Drawing.Point(0, 10);
            this.bankaekle.Name = "bankaekle";
            this.bankaekle.Size = new System.Drawing.Size(160, 40);
            this.bankaekle.TabIndex = 0;
            this.bankaekle.Text = "Banka Ekle";
            // 
            // panelFilters
            // 
            this.panelFilters.Controls.Add(this.groupBox4);
            this.panelFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilters.Location = new System.Drawing.Point(10, 110);
            this.panelFilters.Name = "panelFilters";
            this.panelFilters.Size = new System.Drawing.Size(1180, 100);
            this.panelFilters.TabIndex = 2;
            // 
            // groupBox4
            // 
            this.groupBox4.BorderRadius = 15;
            this.groupBox4.Controls.Add(this.filtrele);
            this.groupBox4.Controls.Add(this.endDatePicker);
            this.groupBox4.Controls.Add(this.startDatePicker);
            this.groupBox4.Controls.Add(this.bankaComboBox);
            this.groupBox4.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.groupBox4.ForeColor = System.Drawing.Color.White;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(1180, 100);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.Text = "Banka Adına Göre Filtreleme";
            // 
            // filtrele
            // 
            this.filtrele.Animated = true;
            this.filtrele.BorderRadius = 10;
            this.filtrele.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.filtrele.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.filtrele.ForeColor = System.Drawing.Color.White;
            this.filtrele.Location = new System.Drawing.Point(900, 55);
            this.filtrele.Name = "filtrele";
            this.filtrele.Size = new System.Drawing.Size(160, 36);
            this.filtrele.TabIndex = 3;
            this.filtrele.Text = "Filtrele";
            // 
            // endDatePicker
            // 
            this.endDatePicker.BorderRadius = 10;
            this.endDatePicker.Checked = true;
            this.endDatePicker.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.endDatePicker.ForeColor = System.Drawing.Color.Black;
            this.endDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.endDatePicker.Location = new System.Drawing.Point(580, 55);
            this.endDatePicker.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.endDatePicker.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.endDatePicker.Name = "endDatePicker";
            this.endDatePicker.Size = new System.Drawing.Size(300, 36);
            this.endDatePicker.TabIndex = 2;
            this.endDatePicker.Value = new System.DateTime(2023, 5, 18, 0, 0, 0, 0);
            // 
            // startDatePicker
            // 
            this.startDatePicker.BorderRadius = 10;
            this.startDatePicker.Checked = true;
            this.startDatePicker.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.startDatePicker.ForeColor = System.Drawing.Color.Black;
            this.startDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.startDatePicker.Location = new System.Drawing.Point(240, 55);
            this.startDatePicker.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.startDatePicker.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.startDatePicker.Name = "startDatePicker";
            this.startDatePicker.Size = new System.Drawing.Size(320, 36);
            this.startDatePicker.TabIndex = 1;
            this.startDatePicker.Value = new System.DateTime(2023, 5, 18, 0, 0, 0, 0);
            // 
            // bankaComboBox
            // 
            this.bankaComboBox.BackColor = System.Drawing.Color.Transparent;
            this.bankaComboBox.BorderRadius = 10;
            this.bankaComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.bankaComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.bankaComboBox.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.bankaComboBox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.bankaComboBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.bankaComboBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.bankaComboBox.ItemHeight = 30;
            this.bankaComboBox.Location = new System.Drawing.Point(12, 55);
            this.bankaComboBox.Name = "bankaComboBox";
            this.bankaComboBox.Size = new System.Drawing.Size(200, 36);
            this.bankaComboBox.TabIndex = 0;
            // 
            // panelInfo
            // 
            this.panelInfo.Controls.Add(this.groupBox3);
            this.panelInfo.Controls.Add(this.groupBox2);
            this.panelInfo.Controls.Add(this.groupBox1);
            this.panelInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelInfo.Location = new System.Drawing.Point(10, 210);
            this.panelInfo.Name = "panelInfo";
            this.panelInfo.Size = new System.Drawing.Size(1180, 120);
            this.panelInfo.TabIndex = 3;
            // 
            // groupBox3
            // 
            this.groupBox3.BorderRadius = 15;
            this.groupBox3.Controls.Add(this.lblToplamCiro);
            this.groupBox3.Controls.Add(this.lblToplamBloke);
            this.groupBox3.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.groupBox3.ForeColor = System.Drawing.Color.White;
            this.groupBox3.Location = new System.Drawing.Point(820, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(360, 120);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.Text = "Bloke ve Ciro Hesap";
            // 
            // lblToplamCiro
            // 
            this.lblToplamCiro.AutoSize = true;
            this.lblToplamCiro.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblToplamCiro.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.lblToplamCiro.Location = new System.Drawing.Point(14, 84);
            this.lblToplamCiro.Name = "lblToplamCiro";
            this.lblToplamCiro.Size = new System.Drawing.Size(99, 20);
            this.lblToplamCiro.TabIndex = 1;
            this.lblToplamCiro.Text = "Toplam Ciro: ";
            // 
            // lblToplamBloke
            // 
            this.lblToplamBloke.AutoSize = true;
            this.lblToplamBloke.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblToplamBloke.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.lblToplamBloke.Location = new System.Drawing.Point(14, 54);
            this.lblToplamBloke.Name = "lblToplamBloke";
            this.lblToplamBloke.Size = new System.Drawing.Size(109, 20);
            this.lblToplamBloke.TabIndex = 0;
            this.lblToplamBloke.Text = "Toplam Bloke: ";
            // 
            // groupBox2
            // 
            this.groupBox2.BorderRadius = 15;
            this.groupBox2.Controls.Add(this.lblYarinGecen);
            this.groupBox2.Controls.Add(this.lblBugunGecen);
            this.groupBox2.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(366, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(448, 120);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.Text = "Hesaba Geçecekler";
            // 
            // lblYarinGecen
            // 
            this.lblYarinGecen.AutoSize = true;
            this.lblYarinGecen.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblYarinGecen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.lblYarinGecen.Location = new System.Drawing.Point(14, 84);
            this.lblYarinGecen.Name = "lblYarinGecen";
            this.lblYarinGecen.Size = new System.Drawing.Size(113, 20);
            this.lblYarinGecen.TabIndex = 1;
            this.lblYarinGecen.Text = "Yarın Geçecek: ";
            // 
            // lblBugunGecen
            // 
            this.lblBugunGecen.AutoSize = true;
            this.lblBugunGecen.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblBugunGecen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.lblBugunGecen.Location = new System.Drawing.Point(14, 54);
            this.lblBugunGecen.Name = "lblBugunGecen";
            this.lblBugunGecen.Size = new System.Drawing.Size(108, 20);
            this.lblBugunGecen.TabIndex = 0;
            this.lblBugunGecen.Text = "Bugün Geçen: ";
            // 
            // groupBox1
            // 
            this.groupBox1.BorderRadius = 15;
            this.groupBox1.Controls.Add(this.eklicihazgoster2);
            this.groupBox1.Controls.Add(this.kayitlibanka);
            this.groupBox1.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(360, 120);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.Text = "Kayıtlı Banka ve Cihazlar";
            // 
            // eklicihazgoster2
            // 
            this.eklicihazgoster2.AutoSize = true;
            this.eklicihazgoster2.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.eklicihazgoster2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.eklicihazgoster2.Location = new System.Drawing.Point(14, 84);
            this.eklicihazgoster2.Name = "eklicihazgoster2";
            this.eklicihazgoster2.Size = new System.Drawing.Size(104, 20);
            this.eklicihazgoster2.TabIndex = 1;
            this.eklicihazgoster2.Text = "Cihaz Sayısı: 0";
            // 
            // kayitlibanka
            // 
            this.kayitlibanka.AutoSize = true;
            this.kayitlibanka.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.kayitlibanka.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.kayitlibanka.Location = new System.Drawing.Point(14, 54);
            this.kayitlibanka.Name = "kayitlibanka";
            this.kayitlibanka.Size = new System.Drawing.Size(109, 20);
            this.kayitlibanka.TabIndex = 0;
            this.kayitlibanka.Text = "Banka Sayısı: 0";
            // 
            // panelGridView
            // 
            this.panelGridView.Controls.Add(this.dataGridView1);
            this.panelGridView.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelGridView.Location = new System.Drawing.Point(10, 330);
            this.panelGridView.Name = "panelGridView";
            this.panelGridView.Size = new System.Drawing.Size(1180, 300);
            this.panelGridView.TabIndex = 4;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeight = 30;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1180, 300);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dataGridView1.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dataGridView1.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dataGridView1.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dataGridView1.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dataGridView1.ThemeStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.dataGridView1.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dataGridView1.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.dataGridView1.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridView1.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.dataGridView1.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dataGridView1.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.ThemeStyle.HeaderStyle.Height = 30;
            this.dataGridView1.ThemeStyle.ReadOnly = true;
            this.dataGridView1.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dataGridView1.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dataGridView1.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dataGridView1.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dataGridView1.ThemeStyle.RowsStyle.Height = 24;
            this.dataGridView1.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dataGridView1.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            // 
            // panelPagination
            // 
            this.panelPagination.Controls.Add(this.hangisayfa);
            this.panelPagination.Controls.Add(this.sonrakisayfa);
            this.panelPagination.Controls.Add(this.oncekisayfa);
            this.panelPagination.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPagination.Location = new System.Drawing.Point(10, 630);
            this.panelPagination.Name = "panelPagination";
            this.panelPagination.Size = new System.Drawing.Size(1180, 98);
            this.panelPagination.TabIndex = 5;
            // 
            // hangisayfa
            // 
            this.hangisayfa.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.hangisayfa.AutoSize = true;
            this.hangisayfa.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.hangisayfa.Location = new System.Drawing.Point(536, 41);
            this.hangisayfa.Name = "hangisayfa";
            this.hangisayfa.Size = new System.Drawing.Size(84, 20);
            this.hangisayfa.TabIndex = 2;
            this.hangisayfa.Text = "Sayfa 1 / 1";
            this.hangisayfa.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // sonrakisayfa
            // 
            this.sonrakisayfa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sonrakisayfa.Animated = true;
            this.sonrakisayfa.BorderRadius = 10;
            this.sonrakisayfa.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.sonrakisayfa.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.sonrakisayfa.ForeColor = System.Drawing.Color.White;
            this.sonrakisayfa.Location = new System.Drawing.Point(1000, 5);
            this.sonrakisayfa.Name = "sonrakisayfa";
            this.sonrakisayfa.Size = new System.Drawing.Size(160, 40);
            this.sonrakisayfa.TabIndex = 1;
            this.sonrakisayfa.Text = "Sonraki Sayfa";
            // 
            // oncekisayfa
            // 
            this.oncekisayfa.Animated = true;
            this.oncekisayfa.BorderRadius = 10;
            this.oncekisayfa.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.oncekisayfa.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.oncekisayfa.ForeColor = System.Drawing.Color.White;
            this.oncekisayfa.Location = new System.Drawing.Point(20, 5);
            this.oncekisayfa.Name = "oncekisayfa";
            this.oncekisayfa.Size = new System.Drawing.Size(160, 40);
            this.oncekisayfa.TabIndex = 0;
            this.oncekisayfa.Text = "Önceki Sayfa";
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 6000;
            this.toolTip1.InitialDelay = 500;
            this.toolTip1.ReshowDelay = 100;
            // 
            // postakip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1200, 738);
            this.Controls.Add(this.panelPagination);
            this.Controls.Add(this.panelGridView);
            this.Controls.Add(this.panelInfo);
            this.Controls.Add(this.panelFilters);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.panelTitleBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "postakip";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Günlük Pos Takip Ekranı";
            this.panelTitleBar.ResumeLayout(false);
            this.panelTitleBar.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            this.panelFilters.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.panelInfo.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panelGridView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panelPagination.ResumeLayout(false);
            this.panelPagination.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion
        public Guna.UI2.WinForms.Guna2Elipse formBorder;
        public Guna.UI2.WinForms.Guna2Panel panelTitleBar;
        public Guna.UI2.WinForms.Guna2Button btnClose;
        public System.Windows.Forms.Label lblTitle;
        public Guna.UI2.WinForms.Guna2Panel panelButtons;
        public Guna.UI2.WinForms.Guna2Button bankaekle;
        public Guna.UI2.WinForms.Guna2Button gunsonuekle;
        public Guna.UI2.WinForms.Guna2Button hareketsil;
        public Guna.UI2.WinForms.Guna2Button hareketduzenle;
        public Guna.UI2.WinForms.Guna2Button excelaktar;
        public Guna.UI2.WinForms.Guna2Button tumunusil;
        public Guna.UI2.WinForms.Guna2Panel panelFilters;
        public Guna.UI2.WinForms.Guna2GroupBox groupBox4;
        public Guna.UI2.WinForms.Guna2ComboBox bankaComboBox;
        public Guna.UI2.WinForms.Guna2DateTimePicker startDatePicker;
        public Guna.UI2.WinForms.Guna2DateTimePicker endDatePicker;
        public Guna.UI2.WinForms.Guna2Button filtrele;
        public Guna.UI2.WinForms.Guna2Panel panelInfo;
        public Guna.UI2.WinForms.Guna2GroupBox groupBox1;
        public System.Windows.Forms.Label kayitlibanka;
        public System.Windows.Forms.Label eklicihazgoster2;
        public Guna.UI2.WinForms.Guna2GroupBox groupBox2;
        public System.Windows.Forms.Label lblBugunGecen;
        public System.Windows.Forms.Label lblYarinGecen;
        public Guna.UI2.WinForms.Guna2GroupBox groupBox3;
        public System.Windows.Forms.Label lblToplamBloke;
        public System.Windows.Forms.Label lblToplamCiro;
        public Guna.UI2.WinForms.Guna2Panel panelGridView;
        public Guna.UI2.WinForms.Guna2DataGridView dataGridView1;
        public Guna.UI2.WinForms.Guna2Panel panelPagination;
        public Guna.UI2.WinForms.Guna2Button oncekisayfa;
        public Guna.UI2.WinForms.Guna2Button sonrakisayfa;
        public System.Windows.Forms.Label hangisayfa;
        public System.Windows.Forms.ToolTip toolTip1;
    }
}