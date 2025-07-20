namespace Veresiye2025
{
    partial class FormFirstSetup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFirstSetup));
            this.titleBar = new System.Windows.Forms.Panel();
            this.closeButton = new System.Windows.Forms.Button();
            this.lblFormTitle = new System.Windows.Forms.Label();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.welcomePanel = new System.Windows.Forms.Panel();
            this.logoPanel = new System.Windows.Forms.Panel();
            this.logoPictureBox = new System.Windows.Forms.PictureBox();
            this.welcomeLabel = new System.Windows.Forms.Label();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.stepLabel = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.btnNext = new Guna.UI2.WinForms.Guna2Button();
            this.btnBack = new Guna.UI2.WinForms.Guna2Button();
            this.btnCancel = new Guna.UI2.WinForms.Guna2Button();
            this.setupTimer = new System.Windows.Forms.Timer(this.components);
            this.titleBar.SuspendLayout();
            this.mainPanel.SuspendLayout();
            this.welcomePanel.SuspendLayout();
            this.logoPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).BeginInit();
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
            this.titleBar.Size = new System.Drawing.Size(600, 50);
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
            this.closeButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.closeButton.ForeColor = System.Drawing.Color.White;
            this.closeButton.Location = new System.Drawing.Point(550, 0);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(50, 50);
            this.closeButton.TabIndex = 1;
            this.closeButton.Text = "✕";
            this.closeButton.UseVisualStyleBackColor = false;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // lblFormTitle
            // 
            this.lblFormTitle.AutoSize = true;
            this.lblFormTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            this.lblFormTitle.ForeColor = System.Drawing.Color.White;
            this.lblFormTitle.Location = new System.Drawing.Point(15, 12);
            this.lblFormTitle.Name = "lblFormTitle";
            this.lblFormTitle.Size = new System.Drawing.Size(290, 32);
            this.lblFormTitle.TabIndex = 0;
            this.lblFormTitle.Text = "🚀 Veresiye2025 - Kurulum";
            // 
            // mainPanel
            // 
            this.mainPanel.BackColor = System.Drawing.Color.White;
            this.mainPanel.Controls.Add(this.welcomePanel);
            this.mainPanel.Location = new System.Drawing.Point(20, 70);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(560, 400);
            this.mainPanel.TabIndex = 1;
            // 
            // welcomePanel
            // 
            this.welcomePanel.Controls.Add(this.logoPanel);
            this.welcomePanel.Controls.Add(this.welcomeLabel);
            this.welcomePanel.Controls.Add(this.descriptionLabel);
            this.welcomePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.welcomePanel.Location = new System.Drawing.Point(0, 0);
            this.welcomePanel.Name = "welcomePanel";
            this.welcomePanel.Padding = new System.Windows.Forms.Padding(40, 20, 40, 20);
            this.welcomePanel.Size = new System.Drawing.Size(560, 400);
            this.welcomePanel.TabIndex = 0;
            // 
            // logoPanel
            // 
            this.logoPanel.Controls.Add(this.logoPictureBox);
            this.logoPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.logoPanel.Location = new System.Drawing.Point(40, 20);
            this.logoPanel.Name = "logoPanel";
            this.logoPanel.Size = new System.Drawing.Size(480, 120);
            this.logoPanel.TabIndex = 0;
            // 
            // logoPictureBox
            // 
            this.logoPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logoPictureBox.Image = global::Veresiye2025.Properties.Resources.restore;
            this.logoPictureBox.Location = new System.Drawing.Point(0, 0);
            this.logoPictureBox.Name = "logoPictureBox";
            this.logoPictureBox.Size = new System.Drawing.Size(480, 120);
            this.logoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.logoPictureBox.TabIndex = 0;
            this.logoPictureBox.TabStop = false;
            // 
            // welcomeLabel
            // 
            this.welcomeLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.welcomeLabel.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.welcomeLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.welcomeLabel.Location = new System.Drawing.Point(40, 140);
            this.welcomeLabel.Name = "welcomeLabel";
            this.welcomeLabel.Size = new System.Drawing.Size(480, 50);
            this.welcomeLabel.TabIndex = 1;
            this.welcomeLabel.Text = "Hoş Geldiniz!";
            this.welcomeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.descriptionLabel.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.descriptionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.descriptionLabel.Location = new System.Drawing.Point(40, 190);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(480, 190);
            this.descriptionLabel.TabIndex = 2;
            this.descriptionLabel.Text = "Veresiye2025 programını kullanmaya başlamak için kısa bir kurulum yapmanız gereki" +
    "yor.\r\n\r\n📋 Bu kurulumda:\r\n• Yönetici hesabınızı oluşturacaksınız\r\n• Firma bilgiler" +
    "inizi gireceksiniz\r\n• Programa başlayacaksınız\r\n\r\nKurulum yaklaşık 2-3 dakika sü" +
    "recektir.";
            this.descriptionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // stepLabel
            // 
            this.stepLabel.AutoSize = true;
            this.stepLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.stepLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.stepLabel.Location = new System.Drawing.Point(20, 480);
            this.stepLabel.Name = "stepLabel";
            this.stepLabel.Size = new System.Drawing.Size(124, 23);
            this.stepLabel.TabIndex = 2;
            this.stepLabel.Text = "Adım 1/3: Başlangıç";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(20, 510);
            this.progressBar.Maximum = 3;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(560, 8);
            this.progressBar.TabIndex = 3;
            this.progressBar.Value = 1;
            // 
            // btnNext
            // 
            this.btnNext.Animated = true;
            this.btnNext.BorderRadius = 8;
            this.btnNext.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnNext.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnNext.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnNext.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnNext.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnNext.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnNext.ForeColor = System.Drawing.Color.White;
            this.btnNext.Image = global::Veresiye2025.Properties.Resources.save_icon;
            this.btnNext.ImageAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.btnNext.Location = new System.Drawing.Point(480, 530);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(100, 40);
            this.btnNext.TabIndex = 6;
            this.btnNext.Text = "İleri";
            this.btnNext.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnBack
            // 
            this.btnBack.Animated = true;
            this.btnBack.BorderRadius = 8;
            this.btnBack.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnBack.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnBack.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnBack.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnBack.Enabled = false;
            this.btnBack.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.Location = new System.Drawing.Point(370, 530);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(100, 40);
            this.btnBack.TabIndex = 5;
            this.btnBack.Text = "Geri";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Animated = true;
            this.btnCancel.BorderRadius = 8;
            this.btnCancel.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCancel.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCancel.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCancel.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCancel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(20, 530);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 40);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "İptal";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // setupTimer
            // 
            this.setupTimer.Interval = 50;
            this.setupTimer.Tick += new System.EventHandler(this.setupTimer_Tick);
            // 
            // FormFirstSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(600, 580);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.stepLabel);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.titleBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormFirstSetup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Veresiye2025 - İlk Kurulum";
            this.Load += new System.EventHandler(this.FormFirstSetup_Load);
            this.titleBar.ResumeLayout(false);
            this.titleBar.PerformLayout();
            this.mainPanel.ResumeLayout(false);
            this.welcomePanel.ResumeLayout(false);
            this.logoPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        public System.Windows.Forms.Panel titleBar;
        public System.Windows.Forms.Label lblFormTitle;
        public System.Windows.Forms.Button closeButton;
        public System.Windows.Forms.Panel mainPanel;
        public System.Windows.Forms.Panel welcomePanel;
        public System.Windows.Forms.Panel logoPanel;
        public System.Windows.Forms.PictureBox logoPictureBox;
        public System.Windows.Forms.Label welcomeLabel;
        public System.Windows.Forms.Label descriptionLabel;
        public System.Windows.Forms.Label stepLabel;
        public System.Windows.Forms.ProgressBar progressBar;
        public Guna.UI2.WinForms.Guna2Button btnNext;
        public Guna.UI2.WinForms.Guna2Button btnBack;
        public Guna.UI2.WinForms.Guna2Button btnCancel;
        public System.Windows.Forms.Timer setupTimer;
    }
}