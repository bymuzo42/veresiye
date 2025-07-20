namespace Veresiye2025
{
    partial class ErtelemeFormu
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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.btnClose = new Guna.UI2.WinForms.Guna2Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.dateTimePicker = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.lblYeniTarih = new System.Windows.Forms.Label();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnIptal = new Guna.UI2.WinForms.Guna2Button();
            this.btnKaydet = new Guna.UI2.WinForms.Guna2Button();
            this.pnlHeader.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(141)))), ((int)(((byte)(243)))));
            this.pnlHeader.Controls.Add(this.btnClose);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(350, 40);
            this.pnlHeader.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.BorderRadius = 15;
            this.btnClose.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(17)))), ((int)(((byte)(35)))));
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(310, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(30, 30);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "X";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(12, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(110, 23);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Alarm Ertele";
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.White;
            this.pnlMain.Controls.Add(this.dateTimePicker);
            this.pnlMain.Controls.Add(this.lblYeniTarih);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 40);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(15);
            this.pnlMain.Size = new System.Drawing.Size(350, 110);
            this.pnlMain.TabIndex = 1;
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.Animated = true;
            this.dateTimePicker.BackColor = System.Drawing.Color.Transparent;
            this.dateTimePicker.BorderRadius = 5;
            this.dateTimePicker.Checked = true;
            this.dateTimePicker.CustomFormat = "dd.MM.yyyy HH:mm";
            this.dateTimePicker.Dock = System.Windows.Forms.DockStyle.Top;
            this.dateTimePicker.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker.Location = new System.Drawing.Point(15, 35);
            this.dateTimePicker.MaxDate = new System.DateTime(2100, 12, 31, 0, 0, 0, 0);
            this.dateTimePicker.MinDate = new System.DateTime(2023, 1, 1, 0, 0, 0, 0);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.ShowUpDown = true;
            this.dateTimePicker.Size = new System.Drawing.Size(320, 36);
            this.dateTimePicker.TabIndex = 1;
            this.dateTimePicker.Value = new System.DateTime(2023, 1, 1, 12, 0, 0, 0);
            // 
            // lblYeniTarih
            // 
            this.lblYeniTarih.AutoSize = true;
            this.lblYeniTarih.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblYeniTarih.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblYeniTarih.Location = new System.Drawing.Point(15, 15);
            this.lblYeniTarih.Name = "lblYeniTarih";
            this.lblYeniTarih.Size = new System.Drawing.Size(248, 20);
            this.lblYeniTarih.TabIndex = 0;
            this.lblYeniTarih.Text = "Alarmın erteleneceği tarih ve saat:";
            // 
            // pnlButtons
            // 
            this.pnlButtons.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlButtons.Controls.Add(this.btnIptal);
            this.pnlButtons.Controls.Add(this.btnKaydet);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.Location = new System.Drawing.Point(0, 150);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Padding = new System.Windows.Forms.Padding(10);
            this.pnlButtons.Size = new System.Drawing.Size(350, 60);
            this.pnlButtons.TabIndex = 2;
            // 
            // btnIptal
            // 
            this.btnIptal.Animated = true;
            this.btnIptal.BorderRadius = 5;
            this.btnIptal.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnIptal.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnIptal.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnIptal.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnIptal.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.btnIptal.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnIptal.ForeColor = System.Drawing.Color.White;
            this.btnIptal.Location = new System.Drawing.Point(95, 10);
            this.btnIptal.Name = "btnIptal";
            this.btnIptal.Size = new System.Drawing.Size(100, 40);
            this.btnIptal.TabIndex = 0;
            this.btnIptal.Text = "İptal";
            this.btnIptal.Click += new System.EventHandler(this.btnIptal_Click);
            // 
            // btnKaydet
            // 
            this.btnKaydet.Animated = true;
            this.btnKaydet.BorderRadius = 5;
            this.btnKaydet.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnKaydet.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnKaydet.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnKaydet.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnKaydet.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(141)))), ((int)(((byte)(243)))));
            this.btnKaydet.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnKaydet.ForeColor = System.Drawing.Color.White;
            this.btnKaydet.Location = new System.Drawing.Point(205, 10);
            this.btnKaydet.Name = "btnKaydet";
            this.btnKaydet.Size = new System.Drawing.Size(135, 40);
            this.btnKaydet.TabIndex = 1;
            this.btnKaydet.Text = "Kaydet";
            this.btnKaydet.Click += new System.EventHandler(this.btnKaydet_Click);
            // 
            // ErtelemeFormu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 210);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlButtons);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ErtelemeFormu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Alarm Ertele";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        public System.Windows.Forms.Panel pnlHeader;
        public System.Windows.Forms.Label lblTitle;
        public Guna.UI2.WinForms.Guna2Button btnClose;
        public System.Windows.Forms.Panel pnlMain;
        public System.Windows.Forms.Label lblYeniTarih;
        public Guna.UI2.WinForms.Guna2DateTimePicker dateTimePicker;
        public System.Windows.Forms.Panel pnlButtons;
        public Guna.UI2.WinForms.Guna2Button btnIptal;
        public Guna.UI2.WinForms.Guna2Button btnKaydet;
    }
}