namespace Veresiye2025
{
    partial class CariEmanet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CariEmanet));
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlList = new System.Windows.Forms.Panel();
            this.dgvEmanetler = new System.Windows.Forms.DataGridView();
            this.pnlFormContainer = new System.Windows.Forms.Panel();
            this.pnlForm = new System.Windows.Forms.Panel();
            this.btnIptal = new System.Windows.Forms.Button();
            this.btnKaydet = new System.Windows.Forms.Button();
            this.btnFotografEkle = new System.Windows.Forms.Button();
            this.picEmanet = new System.Windows.Forms.PictureBox();
            this.lblFotograf = new System.Windows.Forms.Label();
            this.txtAciklama = new System.Windows.Forms.TextBox();
            this.lblAciklama = new System.Windows.Forms.Label();
            this.cmbDurum = new System.Windows.Forms.ComboBox();
            this.lblDurum = new System.Windows.Forms.Label();
            this.dtpGeriAlmaTarihi = new System.Windows.Forms.DateTimePicker();
            this.lblGeriAlmaTarihi = new System.Windows.Forms.Label();
            this.dtpEmanetTarihi = new System.Windows.Forms.DateTimePicker();
            this.lblEmanetTarihi = new System.Windows.Forms.Label();
            this.txtEmanetAdi = new System.Windows.Forms.TextBox();
            this.lblEmanetAdi = new System.Windows.Forms.Label();
            this.formElipse = new System.Windows.Forms.Timer(this.components);
            this.pnlHeader.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmanetler)).BeginInit();
            this.pnlFormContainer.SuspendLayout();
            this.pnlForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picEmanet)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(141)))), ((int)(((byte)(243)))));
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(15, 15);
            this.pnlHeader.Margin = new System.Windows.Forms.Padding(4);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(860, 50);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(24, 9);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(352, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "EMANET YÖNETİMİ - Cari Adı";
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlMain.Controls.Add(this.pnlList);
            this.pnlMain.Controls.Add(this.pnlFormContainer);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(15, 65);
            this.pnlMain.Margin = new System.Windows.Forms.Padding(4);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(860, 515);
            this.pnlMain.TabIndex = 1;
            // 
            // pnlList
            // 
            this.pnlList.Controls.Add(this.dgvEmanetler);
            this.pnlList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlList.Location = new System.Drawing.Point(0, 300);
            this.pnlList.Margin = new System.Windows.Forms.Padding(4);
            this.pnlList.Name = "pnlList";
            this.pnlList.Padding = new System.Windows.Forms.Padding(20, 5, 20, 20);
            this.pnlList.Size = new System.Drawing.Size(860, 215);
            this.pnlList.TabIndex = 1;
            // 
            // dgvEmanetler
            // 
            this.dgvEmanetler.AllowUserToAddRows = false;
            this.dgvEmanetler.AllowUserToDeleteRows = false;
            this.dgvEmanetler.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvEmanetler.BackgroundColor = System.Drawing.Color.White;
            this.dgvEmanetler.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvEmanetler.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEmanetler.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEmanetler.GridColor = System.Drawing.Color.LightGray;
            this.dgvEmanetler.Location = new System.Drawing.Point(20, 5);
            this.dgvEmanetler.Margin = new System.Windows.Forms.Padding(4);
            this.dgvEmanetler.Name = "dgvEmanetler";
            this.dgvEmanetler.ReadOnly = true;
            this.dgvEmanetler.RowHeadersVisible = false;
            this.dgvEmanetler.RowHeadersWidth = 51;
            this.dgvEmanetler.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEmanetler.Size = new System.Drawing.Size(820, 190);
            this.dgvEmanetler.TabIndex = 0;
            this.dgvEmanetler.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEmanetler_CellDoubleClick);
            this.dgvEmanetler.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvEmanetler_CellFormatting);
            // 
            // pnlFormContainer
            // 
            this.pnlFormContainer.Controls.Add(this.pnlForm);
            this.pnlFormContainer.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFormContainer.Location = new System.Drawing.Point(0, 0);
            this.pnlFormContainer.Margin = new System.Windows.Forms.Padding(4);
            this.pnlFormContainer.Name = "pnlFormContainer";
            this.pnlFormContainer.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.pnlFormContainer.Size = new System.Drawing.Size(860, 300);
            this.pnlFormContainer.TabIndex = 0;
            // 
            // pnlForm
            // 
            this.pnlForm.BackColor = System.Drawing.Color.LightGray;
            this.pnlForm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlForm.Controls.Add(this.btnIptal);
            this.pnlForm.Controls.Add(this.btnKaydet);
            this.pnlForm.Controls.Add(this.btnFotografEkle);
            this.pnlForm.Controls.Add(this.picEmanet);
            this.pnlForm.Controls.Add(this.lblFotograf);
            this.pnlForm.Controls.Add(this.txtAciklama);
            this.pnlForm.Controls.Add(this.lblAciklama);
            this.pnlForm.Controls.Add(this.cmbDurum);
            this.pnlForm.Controls.Add(this.lblDurum);
            this.pnlForm.Controls.Add(this.dtpGeriAlmaTarihi);
            this.pnlForm.Controls.Add(this.lblGeriAlmaTarihi);
            this.pnlForm.Controls.Add(this.dtpEmanetTarihi);
            this.pnlForm.Controls.Add(this.lblEmanetTarihi);
            this.pnlForm.Controls.Add(this.txtEmanetAdi);
            this.pnlForm.Controls.Add(this.lblEmanetAdi);
            this.pnlForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlForm.Location = new System.Drawing.Point(20, 10);
            this.pnlForm.Margin = new System.Windows.Forms.Padding(4);
            this.pnlForm.Name = "pnlForm";
            this.pnlForm.Size = new System.Drawing.Size(820, 280);
            this.pnlForm.TabIndex = 0;
            // 
            // btnIptal
            // 
            this.btnIptal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnIptal.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnIptal.FlatAppearance.BorderSize = 0;
            this.btnIptal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIptal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnIptal.ForeColor = System.Drawing.Color.White;
            this.btnIptal.Location = new System.Drawing.Point(690, 202);
            this.btnIptal.Margin = new System.Windows.Forms.Padding(4);
            this.btnIptal.Name = "btnIptal";
            this.btnIptal.Size = new System.Drawing.Size(120, 43);
            this.btnIptal.TabIndex = 14;
            this.btnIptal.Text = "İPTAL";
            this.btnIptal.UseVisualStyleBackColor = false;
            this.btnIptal.Click += new System.EventHandler(this.btnIptal_Click);
            // 
            // btnKaydet
            // 
            this.btnKaydet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(141)))), ((int)(((byte)(243)))));
            this.btnKaydet.FlatAppearance.BorderSize = 0;
            this.btnKaydet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKaydet.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnKaydet.ForeColor = System.Drawing.Color.White;
            this.btnKaydet.Location = new System.Drawing.Point(560, 202);
            this.btnKaydet.Margin = new System.Windows.Forms.Padding(4);
            this.btnKaydet.Name = "btnKaydet";
            this.btnKaydet.Size = new System.Drawing.Size(120, 43);
            this.btnKaydet.TabIndex = 13;
            this.btnKaydet.Text = "KAYDET";
            this.btnKaydet.UseVisualStyleBackColor = false;
            this.btnKaydet.Click += new System.EventHandler(this.btnKaydet_Click);
            // 
            // btnFotografEkle
            // 
            this.btnFotografEkle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(141)))), ((int)(((byte)(243)))));
            this.btnFotografEkle.FlatAppearance.BorderSize = 0;
            this.btnFotografEkle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFotografEkle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnFotografEkle.ForeColor = System.Drawing.Color.White;
            this.btnFotografEkle.Location = new System.Drawing.Point(560, 135);
            this.btnFotografEkle.Margin = new System.Windows.Forms.Padding(4);
            this.btnFotografEkle.Name = "btnFotografEkle";
            this.btnFotografEkle.Size = new System.Drawing.Size(160, 37);
            this.btnFotografEkle.TabIndex = 12;
            this.btnFotografEkle.Text = "Fotoğraf Ekle";
            this.btnFotografEkle.UseVisualStyleBackColor = false;
            this.btnFotografEkle.Click += new System.EventHandler(this.btnFotografEkle_Click);
            // 
            // picEmanet
            // 
            this.picEmanet.BackColor = System.Drawing.Color.White;
            this.picEmanet.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picEmanet.Location = new System.Drawing.Point(560, 25);
            this.picEmanet.Margin = new System.Windows.Forms.Padding(4);
            this.picEmanet.Name = "picEmanet";
            this.picEmanet.Size = new System.Drawing.Size(159, 96);
            this.picEmanet.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picEmanet.TabIndex = 11;
            this.picEmanet.TabStop = false;
            // 
            // lblFotograf
            // 
            this.lblFotograf.AutoSize = true;
            this.lblFotograf.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblFotograf.ForeColor = System.Drawing.Color.Black;
            this.lblFotograf.Location = new System.Drawing.Point(477, 25);
            this.lblFotograf.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFotograf.Name = "lblFotograf";
            this.lblFotograf.Size = new System.Drawing.Size(75, 20);
            this.lblFotograf.TabIndex = 10;
            this.lblFotograf.Text = "Fotoğraf:";
            // 
            // txtAciklama
            // 
            this.txtAciklama.BackColor = System.Drawing.Color.White;
            this.txtAciklama.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAciklama.ForeColor = System.Drawing.Color.Black;
            this.txtAciklama.Location = new System.Drawing.Point(171, 172);
            this.txtAciklama.Margin = new System.Windows.Forms.Padding(4);
            this.txtAciklama.Multiline = true;
            this.txtAciklama.Name = "txtAciklama";
            this.txtAciklama.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtAciklama.Size = new System.Drawing.Size(277, 78);
            this.txtAciklama.TabIndex = 9;
            // 
            // lblAciklama
            // 
            this.lblAciklama.AutoSize = true;
            this.lblAciklama.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblAciklama.ForeColor = System.Drawing.Color.Black;
            this.lblAciklama.Location = new System.Drawing.Point(11, 172);
            this.lblAciklama.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAciklama.Name = "lblAciklama";
            this.lblAciklama.Size = new System.Drawing.Size(77, 20);
            this.lblAciklama.TabIndex = 8;
            this.lblAciklama.Text = "Açıklama:";
            // 
            // cmbDurum
            // 
            this.cmbDurum.BackColor = System.Drawing.Color.White;
            this.cmbDurum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDurum.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbDurum.ForeColor = System.Drawing.Color.Black;
            this.cmbDurum.FormattingEnabled = true;
            this.cmbDurum.Items.AddRange(new object[] {
            "Teslim Edilmedi",
            "Kısmen Teslim Edildi",
            "Teslim Edildi"});
            this.cmbDurum.Location = new System.Drawing.Point(171, 135);
            this.cmbDurum.Margin = new System.Windows.Forms.Padding(4);
            this.cmbDurum.Name = "cmbDurum";
            this.cmbDurum.Size = new System.Drawing.Size(277, 24);
            this.cmbDurum.TabIndex = 7;
            // 
            // lblDurum
            // 
            this.lblDurum.AutoSize = true;
            this.lblDurum.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblDurum.ForeColor = System.Drawing.Color.Black;
            this.lblDurum.Location = new System.Drawing.Point(11, 135);
            this.lblDurum.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDurum.Name = "lblDurum";
            this.lblDurum.Size = new System.Drawing.Size(62, 20);
            this.lblDurum.TabIndex = 6;
            this.lblDurum.Text = "Durum:";
            // 
            // dtpGeriAlmaTarihi
            // 
            this.dtpGeriAlmaTarihi.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpGeriAlmaTarihi.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpGeriAlmaTarihi.Location = new System.Drawing.Point(171, 98);
            this.dtpGeriAlmaTarihi.Margin = new System.Windows.Forms.Padding(4);
            this.dtpGeriAlmaTarihi.Name = "dtpGeriAlmaTarihi";
            this.dtpGeriAlmaTarihi.ShowCheckBox = true;
            this.dtpGeriAlmaTarihi.Size = new System.Drawing.Size(277, 22);
            this.dtpGeriAlmaTarihi.TabIndex = 5;
            // 
            // lblGeriAlmaTarihi
            // 
            this.lblGeriAlmaTarihi.AutoSize = true;
            this.lblGeriAlmaTarihi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblGeriAlmaTarihi.ForeColor = System.Drawing.Color.Black;
            this.lblGeriAlmaTarihi.Location = new System.Drawing.Point(11, 98);
            this.lblGeriAlmaTarihi.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGeriAlmaTarihi.Name = "lblGeriAlmaTarihi";
            this.lblGeriAlmaTarihi.Size = new System.Drawing.Size(126, 20);
            this.lblGeriAlmaTarihi.TabIndex = 4;
            this.lblGeriAlmaTarihi.Text = "Geri Alma Tarihi:";
            // 
            // dtpEmanetTarihi
            // 
            this.dtpEmanetTarihi.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpEmanetTarihi.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEmanetTarihi.Location = new System.Drawing.Point(171, 62);
            this.dtpEmanetTarihi.Margin = new System.Windows.Forms.Padding(4);
            this.dtpEmanetTarihi.Name = "dtpEmanetTarihi";
            this.dtpEmanetTarihi.Size = new System.Drawing.Size(277, 22);
            this.dtpEmanetTarihi.TabIndex = 3;
            // 
            // lblEmanetTarihi
            // 
            this.lblEmanetTarihi.AutoSize = true;
            this.lblEmanetTarihi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblEmanetTarihi.ForeColor = System.Drawing.Color.Black;
            this.lblEmanetTarihi.Location = new System.Drawing.Point(11, 62);
            this.lblEmanetTarihi.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEmanetTarihi.Name = "lblEmanetTarihi";
            this.lblEmanetTarihi.Size = new System.Drawing.Size(109, 20);
            this.lblEmanetTarihi.TabIndex = 2;
            this.lblEmanetTarihi.Text = "Emanet Tarihi:";
            // 
            // txtEmanetAdi
            // 
            this.txtEmanetAdi.BackColor = System.Drawing.Color.White;
            this.txtEmanetAdi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmanetAdi.ForeColor = System.Drawing.Color.Black;
            this.txtEmanetAdi.Location = new System.Drawing.Point(171, 25);
            this.txtEmanetAdi.Margin = new System.Windows.Forms.Padding(4);
            this.txtEmanetAdi.Name = "txtEmanetAdi";
            this.txtEmanetAdi.Size = new System.Drawing.Size(277, 22);
            this.txtEmanetAdi.TabIndex = 1;
            // 
            // lblEmanetAdi
            // 
            this.lblEmanetAdi.AutoSize = true;
            this.lblEmanetAdi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblEmanetAdi.ForeColor = System.Drawing.Color.Black;
            this.lblEmanetAdi.Location = new System.Drawing.Point(11, 25);
            this.lblEmanetAdi.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEmanetAdi.Name = "lblEmanetAdi";
            this.lblEmanetAdi.Size = new System.Drawing.Size(156, 20);
            this.lblEmanetAdi.TabIndex = 0;
            this.lblEmanetAdi.Text = "Emanet Adı / Tanımı:";
            // 
            // CariEmanet
            // 
            this.AcceptButton = this.btnKaydet;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.CancelButton = this.btnIptal;
            this.ClientSize = new System.Drawing.Size(890, 595);
            this.ControlBox = false;
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CariEmanet";
            this.Padding = new System.Windows.Forms.Padding(15);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Emanet Takip Sistemi";
            this.Load += new System.EventHandler(this.CariEmanet_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmanetler)).EndInit();
            this.pnlFormContainer.ResumeLayout(false);
            this.pnlForm.ResumeLayout(false);
            this.pnlForm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picEmanet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel pnlHeader;
        public System.Windows.Forms.Label lblTitle;
        public System.Windows.Forms.Panel pnlMain;
        public System.Windows.Forms.Panel pnlFormContainer;
        public System.Windows.Forms.Panel pnlForm;
        public System.Windows.Forms.TextBox txtEmanetAdi;
        public System.Windows.Forms.Label lblEmanetAdi;
        public System.Windows.Forms.DateTimePicker dtpEmanetTarihi;
        public System.Windows.Forms.Label lblEmanetTarihi;
        public System.Windows.Forms.DateTimePicker dtpGeriAlmaTarihi;
        public System.Windows.Forms.Label lblGeriAlmaTarihi;
        public System.Windows.Forms.ComboBox cmbDurum;
        public System.Windows.Forms.Label lblDurum;
        public System.Windows.Forms.TextBox txtAciklama;
        public System.Windows.Forms.Label lblAciklama;
        public System.Windows.Forms.PictureBox picEmanet;
        public System.Windows.Forms.Label lblFotograf;
        public System.Windows.Forms.Button btnFotografEkle;
        public System.Windows.Forms.Button btnKaydet;
        public System.Windows.Forms.Button btnIptal;
        public System.Windows.Forms.Panel pnlList;
        public System.Windows.Forms.DataGridView dgvEmanetler;
        public System.Windows.Forms.Timer formElipse;
    }
}