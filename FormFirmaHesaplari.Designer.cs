namespace Veresiye2025
{
    partial class FormFirmaHesaplari
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
            this.titleBar = new System.Windows.Forms.Panel();
            this.closeButton = new System.Windows.Forms.Button();
            this.lblFormTitle = new System.Windows.Forms.Label();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.listViewFirmalar = new System.Windows.Forms.ListView();
            this.columnID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnFirmaAdi = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnTelefon = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnVergiNo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnIl = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnIlce = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnButce = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnParaBirimi = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.searchPanel = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.buttonPanel = new System.Windows.Forms.Panel();
            this.btnDuzenle = new Guna.UI2.WinForms.Guna2Button();
            this.btnSil = new Guna.UI2.WinForms.Guna2Button();
            this.btnKapat = new Guna.UI2.WinForms.Guna2Button();
            this.btnYeniFirma = new Guna.UI2.WinForms.Guna2Button();
            this.titleBar.SuspendLayout();
            this.mainPanel.SuspendLayout();
            this.searchPanel.SuspendLayout();
            this.buttonPanel.SuspendLayout();
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
            this.titleBar.Size = new System.Drawing.Size(900, 40);
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
            this.closeButton.Location = new System.Drawing.Point(860, 0);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(40, 40);
            this.closeButton.TabIndex = 1;
            this.closeButton.TabStop = false;
            this.closeButton.Text = "✕";
            this.closeButton.UseVisualStyleBackColor = false;
            this.closeButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // lblFormTitle
            // 
            this.lblFormTitle.AutoSize = true;
            this.lblFormTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblFormTitle.ForeColor = System.Drawing.Color.White;
            this.lblFormTitle.Location = new System.Drawing.Point(12, 9);
            this.lblFormTitle.Name = "lblFormTitle";
            this.lblFormTitle.Size = new System.Drawing.Size(146, 25);
            this.lblFormTitle.TabIndex = 0;
            this.lblFormTitle.Text = "Firma Hesapları";
            // 
            // mainPanel
            // 
            this.mainPanel.BackColor = System.Drawing.Color.White;
            this.mainPanel.Controls.Add(this.listViewFirmalar);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 100);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Padding = new System.Windows.Forms.Padding(15);
            this.mainPanel.Size = new System.Drawing.Size(900, 430);
            this.mainPanel.TabIndex = 1;
            // 
            // listViewFirmalar
            // 
            this.listViewFirmalar.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewFirmalar.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnID,
            this.columnFirmaAdi,
            this.columnTelefon,
            this.columnVergiNo,
            this.columnIl,
            this.columnIlce,
            this.columnButce,
            this.columnParaBirimi});
            this.listViewFirmalar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewFirmalar.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.listViewFirmalar.FullRowSelect = true;
            this.listViewFirmalar.GridLines = true;
            this.listViewFirmalar.HideSelection = false;
            this.listViewFirmalar.Location = new System.Drawing.Point(15, 15);
            this.listViewFirmalar.Name = "listViewFirmalar";
            this.listViewFirmalar.Size = new System.Drawing.Size(870, 400);
            this.listViewFirmalar.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listViewFirmalar.TabIndex = 0;
            this.listViewFirmalar.UseCompatibleStateImageBehavior = false;
            this.listViewFirmalar.View = System.Windows.Forms.View.Details;
            this.listViewFirmalar.DoubleClick += new System.EventHandler(this.ListViewFirmalar_DoubleClick);
            // 
            // columnID
            // 
            this.columnID.Text = "ID";
            this.columnID.Width = 50;
            // 
            // columnFirmaAdi
            // 
            this.columnFirmaAdi.Text = "Firma Adı";
            this.columnFirmaAdi.Width = 220;
            // 
            // columnTelefon
            // 
            this.columnTelefon.Text = "Telefon";
            this.columnTelefon.Width = 120;
            // 
            // columnVergiNo
            // 
            this.columnVergiNo.Text = "Vergi No";
            this.columnVergiNo.Width = 100;
            // 
            // columnIl
            // 
            this.columnIl.Text = "İl";
            this.columnIl.Width = 100;
            // 
            // columnIlce
            // 
            this.columnIlce.Text = "İlçe";
            this.columnIlce.Width = 100;
            // 
            // columnButce
            // 
            this.columnButce.Text = "Bütçe";
            this.columnButce.Width = 100;
            // 
            // columnParaBirimi
            // 
            this.columnParaBirimi.Text = "Para Birimi";
            this.columnParaBirimi.Width = 80;
            // 
            // searchPanel
            // 
            this.searchPanel.BackColor = System.Drawing.Color.White;
            this.searchPanel.Controls.Add(this.txtSearch);
            this.searchPanel.Controls.Add(this.lblSearch);
            this.searchPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchPanel.Location = new System.Drawing.Point(0, 40);
            this.searchPanel.Name = "searchPanel";
            this.searchPanel.Padding = new System.Windows.Forms.Padding(15, 15, 15, 10);
            this.searchPanel.Size = new System.Drawing.Size(900, 60);
            this.searchPanel.TabIndex = 2;
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearch.Location = new System.Drawing.Point(610, 15);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(275, 30);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.TextChanged += new System.EventHandler(this.TxtSearch_TextChanged);
            // 
            // lblSearch
            // 
            this.lblSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblSearch.Location = new System.Drawing.Point(559, 18);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(40, 23);
            this.lblSearch.TabIndex = 1;
            this.lblSearch.Text = "Ara:";
            // 
            // buttonPanel
            // 
            this.buttonPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.buttonPanel.Controls.Add(this.btnYeniFirma);
            this.buttonPanel.Controls.Add(this.btnDuzenle);
            this.buttonPanel.Controls.Add(this.btnSil);
            this.buttonPanel.Controls.Add(this.btnKapat);
            this.buttonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonPanel.Location = new System.Drawing.Point(0, 530);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Padding = new System.Windows.Forms.Padding(15);
            this.buttonPanel.Size = new System.Drawing.Size(900, 70);
            this.buttonPanel.TabIndex = 3;
            // 
            // btnDuzenle
            // 
            this.btnDuzenle.Animated = true;
            this.btnDuzenle.BorderRadius = 20;
            this.btnDuzenle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDuzenle.Enabled = false;
            this.btnDuzenle.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnDuzenle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDuzenle.ForeColor = System.Drawing.Color.White;
            this.btnDuzenle.Image = global::Veresiye2025.Properties.Resources.edit;
            this.btnDuzenle.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnDuzenle.ImageSize = new System.Drawing.Size(25, 25);
            this.btnDuzenle.Location = new System.Drawing.Point(181, 15);
            this.btnDuzenle.Name = "btnDuzenle";
            this.btnDuzenle.Size = new System.Drawing.Size(160, 40);
            this.btnDuzenle.TabIndex = 1;
            this.btnDuzenle.Text = "Düzenle";
            this.btnDuzenle.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnDuzenle.Click += new System.EventHandler(this.BtnDuzenle_Click);
            // 
            // btnSil
            // 
            this.btnSil.Animated = true;
            this.btnSil.BorderRadius = 20;
            this.btnSil.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSil.Enabled = false;
            this.btnSil.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnSil.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSil.ForeColor = System.Drawing.Color.White;
            this.btnSil.Image = global::Veresiye2025.Properties.Resources.delete;
            this.btnSil.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnSil.ImageSize = new System.Drawing.Size(25, 25);
            this.btnSil.Location = new System.Drawing.Point(347, 15);
            this.btnSil.Name = "btnSil";
            this.btnSil.Size = new System.Drawing.Size(150, 40);
            this.btnSil.TabIndex = 2;
            this.btnSil.Text = "Sil";
            this.btnSil.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnSil.Click += new System.EventHandler(this.BtnSil_Click);
            // 
            // btnKapat
            // 
            this.btnKapat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnKapat.Animated = true;
            this.btnKapat.BorderRadius = 20;
            this.btnKapat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKapat.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnKapat.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnKapat.ForeColor = System.Drawing.Color.White;
            this.btnKapat.Image = global::Veresiye2025.Properties.Resources.kapat;
            this.btnKapat.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnKapat.ImageSize = new System.Drawing.Size(25, 25);
            this.btnKapat.Location = new System.Drawing.Point(735, 15);
            this.btnKapat.Name = "btnKapat";
            this.btnKapat.Size = new System.Drawing.Size(150, 40);
            this.btnKapat.TabIndex = 3;
            this.btnKapat.Text = "Kapat";
            this.btnKapat.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnKapat.Click += new System.EventHandler(this.BtnKapat_Click);
            // 
            // btnYeniFirma
            // 
            this.btnYeniFirma.Animated = true;
            this.btnYeniFirma.BorderRadius = 20;
            this.btnYeniFirma.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnYeniFirma.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnYeniFirma.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnYeniFirma.ForeColor = System.Drawing.Color.White;
            this.btnYeniFirma.Image = global::Veresiye2025.Properties.Resources.add;
            this.btnYeniFirma.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnYeniFirma.ImageSize = new System.Drawing.Size(25, 25);
            this.btnYeniFirma.Location = new System.Drawing.Point(15, 15);
            this.btnYeniFirma.Name = "btnYeniFirma";
            this.btnYeniFirma.Size = new System.Drawing.Size(160, 40);
            this.btnYeniFirma.TabIndex = 0;
            this.btnYeniFirma.Text = "Yeni Firma";
            this.btnYeniFirma.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnYeniFirma.Click += new System.EventHandler(this.BtnYeniFirma_Click);
            // 
            // FormFirmaHesaplari
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.buttonPanel);
            this.Controls.Add(this.searchPanel);
            this.Controls.Add(this.titleBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormFirmaHesaplari";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Firma Hesapları";
            this.Load += new System.EventHandler(this.FormFirmaHesaplari_Load);
            this.titleBar.ResumeLayout(false);
            this.titleBar.PerformLayout();
            this.mainPanel.ResumeLayout(false);
            this.searchPanel.ResumeLayout(false);
            this.searchPanel.PerformLayout();
            this.buttonPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel titleBar;
        public System.Windows.Forms.Label lblFormTitle;
        public System.Windows.Forms.Button closeButton;
        public System.Windows.Forms.Panel mainPanel;
        public System.Windows.Forms.ListView listViewFirmalar;
        public System.Windows.Forms.ColumnHeader columnID;
        public System.Windows.Forms.ColumnHeader columnFirmaAdi;
        public System.Windows.Forms.ColumnHeader columnTelefon;
        public System.Windows.Forms.ColumnHeader columnVergiNo;
        public System.Windows.Forms.ColumnHeader columnIl;
        public System.Windows.Forms.ColumnHeader columnIlce;
        public System.Windows.Forms.ColumnHeader columnButce;
        public System.Windows.Forms.ColumnHeader columnParaBirimi;
        public System.Windows.Forms.Panel searchPanel;
        public System.Windows.Forms.TextBox txtSearch;
        public System.Windows.Forms.Label lblSearch;
        public System.Windows.Forms.Panel buttonPanel;
        public Guna.UI2.WinForms.Guna2Button btnYeniFirma;
        public Guna.UI2.WinForms.Guna2Button btnDuzenle;
        public Guna.UI2.WinForms.Guna2Button btnSil;
        public Guna.UI2.WinForms.Guna2Button btnKapat;
    }
}