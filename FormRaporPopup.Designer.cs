namespace Veresiye2025
{
    partial class FormRaporPopup
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
            this.panelHeader = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelCards = new System.Windows.Forms.Panel();
            this.panelCard6 = new System.Windows.Forms.Panel();
            this.btnCard6 = new System.Windows.Forms.Button();
            this.lblCard6Desc = new System.Windows.Forms.Label();
            this.lblCard6Title = new System.Windows.Forms.Label();
            this.panelCard5 = new System.Windows.Forms.Panel();
            this.btnCard5 = new System.Windows.Forms.Button();
            this.lblCard5Desc = new System.Windows.Forms.Label();
            this.lblCard5Title = new System.Windows.Forms.Label();
            this.panelCard4 = new System.Windows.Forms.Panel();
            this.btnCard4 = new System.Windows.Forms.Button();
            this.lblCard4Desc = new System.Windows.Forms.Label();
            this.lblCard4Title = new System.Windows.Forms.Label();
            this.panelCard3 = new System.Windows.Forms.Panel();
            this.btnCard3 = new System.Windows.Forms.Button();
            this.lblCard3Desc = new System.Windows.Forms.Label();
            this.lblCard3Title = new System.Windows.Forms.Label();
            this.panelCard2 = new System.Windows.Forms.Panel();
            this.btnCard2 = new System.Windows.Forms.Button();
            this.lblCard2Desc = new System.Windows.Forms.Label();
            this.lblCard2Title = new System.Windows.Forms.Label();
            this.panelCard1 = new System.Windows.Forms.Panel();
            this.btnCard1 = new System.Windows.Forms.Button();
            this.lblCard1Desc = new System.Windows.Forms.Label();
            this.lblCard1Title = new System.Windows.Forms.Label();
            this.panelStats = new System.Windows.Forms.Panel();
            this.lblStat4Label = new System.Windows.Forms.Label();
            this.lblStat4Value = new System.Windows.Forms.Label();
            this.lblStat4Icon = new System.Windows.Forms.Label();
            this.lblStat3Label = new System.Windows.Forms.Label();
            this.lblStat3Value = new System.Windows.Forms.Label();
            this.lblStat3Icon = new System.Windows.Forms.Label();
            this.lblStat2Label = new System.Windows.Forms.Label();
            this.lblStat2Value = new System.Windows.Forms.Label();
            this.lblStat2Icon = new System.Windows.Forms.Label();
            this.lblStat1Label = new System.Windows.Forms.Label();
            this.lblStat1Value = new System.Windows.Forms.Label();
            this.lblStat1Icon = new System.Windows.Forms.Label();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.lblFooter = new System.Windows.Forms.Label();
            this.panelHeader.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.panelCards.SuspendLayout();
            this.panelCard6.SuspendLayout();
            this.panelCard5.SuspendLayout();
            this.panelCard4.SuspendLayout();
            this.panelCard3.SuspendLayout();
            this.panelCard2.SuspendLayout();
            this.panelCard1.SuspendLayout();
            this.panelStats.SuspendLayout();
            this.panelFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.MidnightBlue;
            this.panelHeader.Controls.Add(this.btnClose);
            this.panelHeader.Controls.Add(this.lblSubtitle);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(900, 80);
            this.panelHeader.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(844, 20);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(40, 40);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "✕";
            this.btnClose.UseVisualStyleBackColor = false;
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
            this.lblSubtitle.Location = new System.Drawing.Point(30, 50);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(333, 23);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "İstediğiniz rapor türünü seçin ve oluşturun";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(30, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(312, 41);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "📊 RAPOR OLUŞTUR";
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.White;
            this.panelMain.Controls.Add(this.panelCards);
            this.panelMain.Controls.Add(this.panelStats);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 80);
            this.panelMain.Name = "panelMain";
            this.panelMain.Padding = new System.Windows.Forms.Padding(20);
            this.panelMain.Size = new System.Drawing.Size(900, 463);
            this.panelMain.TabIndex = 1;
            // 
            // panelCards
            // 
            this.panelCards.BackColor = System.Drawing.Color.Transparent;
            this.panelCards.Controls.Add(this.panelCard6);
            this.panelCards.Controls.Add(this.panelCard5);
            this.panelCards.Controls.Add(this.panelCard4);
            this.panelCards.Controls.Add(this.panelCard3);
            this.panelCards.Controls.Add(this.panelCard2);
            this.panelCards.Controls.Add(this.panelCard1);
            this.panelCards.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCards.Location = new System.Drawing.Point(20, 100);
            this.panelCards.Name = "panelCards";
            this.panelCards.Size = new System.Drawing.Size(860, 343);
            this.panelCards.TabIndex = 1;
            // 
            // panelCard6
            // 
            this.panelCard6.BackColor = System.Drawing.Color.White;
            this.panelCard6.Controls.Add(this.btnCard6);
            this.panelCard6.Controls.Add(this.lblCard6Desc);
            this.panelCard6.Controls.Add(this.lblCard6Title);
            this.panelCard6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelCard6.Location = new System.Drawing.Point(220, 175);
            this.panelCard6.Name = "panelCard6";
            this.panelCard6.Size = new System.Drawing.Size(200, 120);
            this.panelCard6.TabIndex = 5;
            // 
            // btnCard6
            // 
            this.btnCard6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnCard6.FlatAppearance.BorderSize = 0;
            this.btnCard6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCard6.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.btnCard6.ForeColor = System.Drawing.Color.White;
            this.btnCard6.Location = new System.Drawing.Point(105, 85);
            this.btnCard6.Name = "btnCard6";
            this.btnCard6.Size = new System.Drawing.Size(80, 25);
            this.btnCard6.TabIndex = 2;
            this.btnCard6.Text = "Oluştur";
            this.btnCard6.UseVisualStyleBackColor = false;
            // 
            // lblCard6Desc
            // 
            this.lblCard6Desc.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblCard6Desc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.lblCard6Desc.Location = new System.Drawing.Point(10, 45);
            this.lblCard6Desc.Name = "lblCard6Desc";
            this.lblCard6Desc.Size = new System.Drawing.Size(180, 40);
            this.lblCard6Desc.TabIndex = 1;
            this.lblCard6Desc.Text = "Görsel grafikler\nKısa özet raporu";
            // 
            // lblCard6Title
            // 
            this.lblCard6Title.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCard6Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(27)))), ((int)(((byte)(62)))));
            this.lblCard6Title.Location = new System.Drawing.Point(10, 15);
            this.lblCard6Title.Name = "lblCard6Title";
            this.lblCard6Title.Size = new System.Drawing.Size(180, 25);
            this.lblCard6Title.TabIndex = 0;
            this.lblCard6Title.Text = "📊 Dashboard";
            this.lblCard6Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelCard5
            // 
            this.panelCard5.BackColor = System.Drawing.Color.White;
            this.panelCard5.Controls.Add(this.btnCard5);
            this.panelCard5.Controls.Add(this.lblCard5Desc);
            this.panelCard5.Controls.Add(this.lblCard5Title);
            this.panelCard5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelCard5.Location = new System.Drawing.Point(10, 175);
            this.panelCard5.Name = "panelCard5";
            this.panelCard5.Size = new System.Drawing.Size(200, 120);
            this.panelCard5.TabIndex = 4;
            // 
            // btnCard5
            // 
            this.btnCard5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(133)))), ((int)(((byte)(27)))));
            this.btnCard5.FlatAppearance.BorderSize = 0;
            this.btnCard5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCard5.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.btnCard5.ForeColor = System.Drawing.Color.White;
            this.btnCard5.Location = new System.Drawing.Point(105, 85);
            this.btnCard5.Name = "btnCard5";
            this.btnCard5.Size = new System.Drawing.Size(80, 25);
            this.btnCard5.TabIndex = 2;
            this.btnCard5.Text = "Oluştur";
            this.btnCard5.UseVisualStyleBackColor = false;
            // 
            // lblCard5Desc
            // 
            this.lblCard5Desc.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblCard5Desc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.lblCard5Desc.Location = new System.Drawing.Point(10, 45);
            this.lblCard5Desc.Name = "lblCard5Desc";
            this.lblCard5Desc.Size = new System.Drawing.Size(180, 40);
            this.lblCard5Desc.TabIndex = 1;
            this.lblCard5Desc.Text = "Tüm müşteri detayları\nExcel formatında";
            // 
            // lblCard5Title
            // 
            this.lblCard5Title.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCard5Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(27)))), ((int)(((byte)(62)))));
            this.lblCard5Title.Location = new System.Drawing.Point(10, 15);
            this.lblCard5Title.Name = "lblCard5Title";
            this.lblCard5Title.Size = new System.Drawing.Size(180, 25);
            this.lblCard5Title.TabIndex = 0;
            this.lblCard5Title.Text = "📋 Detaylı Liste";
            this.lblCard5Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelCard4
            // 
            this.panelCard4.BackColor = System.Drawing.Color.White;
            this.panelCard4.Controls.Add(this.btnCard4);
            this.panelCard4.Controls.Add(this.lblCard4Desc);
            this.panelCard4.Controls.Add(this.lblCard4Title);
            this.panelCard4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelCard4.Location = new System.Drawing.Point(650, 20);
            this.panelCard4.Name = "panelCard4";
            this.panelCard4.Size = new System.Drawing.Size(200, 120);
            this.panelCard4.TabIndex = 3;
            // 
            // btnCard4
            // 
            this.btnCard4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(162)))), ((int)(((byte)(184)))));
            this.btnCard4.FlatAppearance.BorderSize = 0;
            this.btnCard4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCard4.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.btnCard4.ForeColor = System.Drawing.Color.White;
            this.btnCard4.Location = new System.Drawing.Point(105, 85);
            this.btnCard4.Name = "btnCard4";
            this.btnCard4.Size = new System.Drawing.Size(80, 25);
            this.btnCard4.TabIndex = 2;
            this.btnCard4.Text = "Oluştur";
            this.btnCard4.UseVisualStyleBackColor = false;
            // 
            // lblCard4Desc
            // 
            this.lblCard4Desc.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblCard4Desc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.lblCard4Desc.Location = new System.Drawing.Point(10, 45);
            this.lblCard4Desc.Name = "lblCard4Desc";
            this.lblCard4Desc.Size = new System.Drawing.Size(180, 40);
            this.lblCard4Desc.TabIndex = 1;
            this.lblCard4Desc.Text = "Borç grupları analizi\nMüşteri profilleme";
            // 
            // lblCard4Title
            // 
            this.lblCard4Title.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCard4Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(27)))), ((int)(((byte)(62)))));
            this.lblCard4Title.Location = new System.Drawing.Point(10, 15);
            this.lblCard4Title.Name = "lblCard4Title";
            this.lblCard4Title.Size = new System.Drawing.Size(180, 25);
            this.lblCard4Title.TabIndex = 0;
            this.lblCard4Title.Text = "👥 Müşteri Segmentasyon";
            this.lblCard4Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelCard3
            // 
            this.panelCard3.BackColor = System.Drawing.Color.White;
            this.panelCard3.Controls.Add(this.btnCard3);
            this.panelCard3.Controls.Add(this.lblCard3Desc);
            this.panelCard3.Controls.Add(this.lblCard3Title);
            this.panelCard3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelCard3.Location = new System.Drawing.Point(430, 20);
            this.panelCard3.Name = "panelCard3";
            this.panelCard3.Size = new System.Drawing.Size(200, 120);
            this.panelCard3.TabIndex = 2;
            // 
            // btnCard3
            // 
            this.btnCard3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(81)))), ((int)(((byte)(255)))));
            this.btnCard3.FlatAppearance.BorderSize = 0;
            this.btnCard3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCard3.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.btnCard3.ForeColor = System.Drawing.Color.White;
            this.btnCard3.Location = new System.Drawing.Point(105, 85);
            this.btnCard3.Name = "btnCard3";
            this.btnCard3.Size = new System.Drawing.Size(80, 25);
            this.btnCard3.TabIndex = 2;
            this.btnCard3.Text = "Oluştur";
            this.btnCard3.UseVisualStyleBackColor = false;
            // 
            // lblCard3Desc
            // 
            this.lblCard3Desc.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblCard3Desc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.lblCard3Desc.Location = new System.Drawing.Point(10, 45);
            this.lblCard3Desc.Name = "lblCard3Desc";
            this.lblCard3Desc.Size = new System.Drawing.Size(180, 40);
            this.lblCard3Desc.TabIndex = 1;
            this.lblCard3Desc.Text = "Hedef vs gerçekleşen\n6 aylık trend analizi";
            // 
            // lblCard3Title
            // 
            this.lblCard3Title.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCard3Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(27)))), ((int)(((byte)(62)))));
            this.lblCard3Title.Location = new System.Drawing.Point(10, 15);
            this.lblCard3Title.Name = "lblCard3Title";
            this.lblCard3Title.Size = new System.Drawing.Size(180, 25);
            this.lblCard3Title.TabIndex = 0;
            this.lblCard3Title.Text = "💰 Tahsilat Performans";
            this.lblCard3Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelCard2
            // 
            this.panelCard2.BackColor = System.Drawing.Color.White;
            this.panelCard2.Controls.Add(this.btnCard2);
            this.panelCard2.Controls.Add(this.lblCard2Desc);
            this.panelCard2.Controls.Add(this.lblCard2Title);
            this.panelCard2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelCard2.Location = new System.Drawing.Point(220, 20);
            this.panelCard2.Name = "panelCard2";
            this.panelCard2.Size = new System.Drawing.Size(200, 120);
            this.panelCard2.TabIndex = 1;
            // 
            // btnCard2
            // 
            this.btnCard2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnCard2.FlatAppearance.BorderSize = 0;
            this.btnCard2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCard2.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.btnCard2.ForeColor = System.Drawing.Color.White;
            this.btnCard2.Location = new System.Drawing.Point(105, 85);
            this.btnCard2.Name = "btnCard2";
            this.btnCard2.Size = new System.Drawing.Size(80, 25);
            this.btnCard2.TabIndex = 2;
            this.btnCard2.Text = "Oluştur";
            this.btnCard2.UseVisualStyleBackColor = false;
            // 
            // lblCard2Desc
            // 
            this.lblCard2Desc.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblCard2Desc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.lblCard2Desc.Location = new System.Drawing.Point(10, 45);
            this.lblCard2Desc.Name = "lblCard2Desc";
            this.lblCard2Desc.Size = new System.Drawing.Size(180, 40);
            this.lblCard2Desc.TabIndex = 1;
            this.lblCard2Desc.Text = "Müşteri risk skorları\nTahsilat şansı analizi";
            // 
            // lblCard2Title
            // 
            this.lblCard2Title.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCard2Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(27)))), ((int)(((byte)(62)))));
            this.lblCard2Title.Location = new System.Drawing.Point(10, 15);
            this.lblCard2Title.Name = "lblCard2Title";
            this.lblCard2Title.Size = new System.Drawing.Size(180, 25);
            this.lblCard2Title.TabIndex = 0;
            this.lblCard2Title.Text = "🎯 Risk Analizi";
            this.lblCard2Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelCard1
            // 
            this.panelCard1.BackColor = System.Drawing.Color.White;
            this.panelCard1.Controls.Add(this.btnCard1);
            this.panelCard1.Controls.Add(this.lblCard1Desc);
            this.panelCard1.Controls.Add(this.lblCard1Title);
            this.panelCard1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelCard1.Location = new System.Drawing.Point(10, 20);
            this.panelCard1.Name = "panelCard1";
            this.panelCard1.Size = new System.Drawing.Size(200, 120);
            this.panelCard1.TabIndex = 0;
            // 
            // btnCard1
            // 
            this.btnCard1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnCard1.FlatAppearance.BorderSize = 0;
            this.btnCard1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCard1.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.btnCard1.ForeColor = System.Drawing.Color.White;
            this.btnCard1.Location = new System.Drawing.Point(105, 85);
            this.btnCard1.Name = "btnCard1";
            this.btnCard1.Size = new System.Drawing.Size(80, 25);
            this.btnCard1.TabIndex = 2;
            this.btnCard1.Text = "Oluştur";
            this.btnCard1.UseVisualStyleBackColor = false;
            // 
            // lblCard1Desc
            // 
            this.lblCard1Desc.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblCard1Desc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.lblCard1Desc.Location = new System.Drawing.Point(10, 45);
            this.lblCard1Desc.Name = "lblCard1Desc";
            this.lblCard1Desc.Size = new System.Drawing.Size(180, 40);
            this.lblCard1Desc.TabIndex = 1;
            this.lblCard1Desc.Text = "Son 30 günün detaylı analizi\nRisk dağılımı ve trend analizi";
            // 
            // lblCard1Title
            // 
            this.lblCard1Title.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCard1Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(27)))), ((int)(((byte)(62)))));
            this.lblCard1Title.Location = new System.Drawing.Point(10, 15);
            this.lblCard1Title.Name = "lblCard1Title";
            this.lblCard1Title.Size = new System.Drawing.Size(180, 25);
            this.lblCard1Title.TabIndex = 0;
            this.lblCard1Title.Text = "📈 Aylık Takip";
            this.lblCard1Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelStats
            // 
            this.panelStats.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.panelStats.Controls.Add(this.lblStat4Label);
            this.panelStats.Controls.Add(this.lblStat4Value);
            this.panelStats.Controls.Add(this.lblStat4Icon);
            this.panelStats.Controls.Add(this.lblStat3Label);
            this.panelStats.Controls.Add(this.lblStat3Value);
            this.panelStats.Controls.Add(this.lblStat3Icon);
            this.panelStats.Controls.Add(this.lblStat1Value);
            this.panelStats.Controls.Add(this.lblStat2Label);
            this.panelStats.Controls.Add(this.lblStat2Value);
            this.panelStats.Controls.Add(this.lblStat2Icon);
            this.panelStats.Controls.Add(this.lblStat1Label);
            this.panelStats.Controls.Add(this.lblStat1Icon);
            this.panelStats.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelStats.Location = new System.Drawing.Point(20, 20);
            this.panelStats.Name = "panelStats";
            this.panelStats.Size = new System.Drawing.Size(860, 80);
            this.panelStats.TabIndex = 0;
            // 
            // lblStat4Label
            // 
            this.lblStat4Label.Font = new System.Drawing.Font("Segoe UI Semibold", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblStat4Label.ForeColor = System.Drawing.Color.Crimson;
            this.lblStat4Label.Location = new System.Drawing.Point(675, 47);
            this.lblStat4Label.Name = "lblStat4Label";
            this.lblStat4Label.Size = new System.Drawing.Size(100, 15);
            this.lblStat4Label.TabIndex = 11;
            this.lblStat4Label.Text = "Yüksek Risk";
            // 
            // lblStat4Value
            // 
            this.lblStat4Value.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblStat4Value.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(27)))), ((int)(((byte)(62)))));
            this.lblStat4Value.Location = new System.Drawing.Point(675, 12);
            this.lblStat4Value.Name = "lblStat4Value";
            this.lblStat4Value.Size = new System.Drawing.Size(100, 35);
            this.lblStat4Value.TabIndex = 10;
            this.lblStat4Value.Text = "0";
            this.lblStat4Value.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStat4Icon
            // 
            this.lblStat4Icon.Font = new System.Drawing.Font("Segoe UI Emoji", 16F);
            this.lblStat4Icon.Location = new System.Drawing.Point(615, 12);
            this.lblStat4Icon.Name = "lblStat4Icon";
            this.lblStat4Icon.Size = new System.Drawing.Size(50, 35);
            this.lblStat4Icon.TabIndex = 9;
            this.lblStat4Icon.Text = "📊";
            // 
            // lblStat3Label
            // 
            this.lblStat3Label.Font = new System.Drawing.Font("Segoe UI Semibold", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblStat3Label.ForeColor = System.Drawing.Color.Orange;
            this.lblStat3Label.Location = new System.Drawing.Point(455, 47);
            this.lblStat3Label.Name = "lblStat3Label";
            this.lblStat3Label.Size = new System.Drawing.Size(100, 15);
            this.lblStat3Label.TabIndex = 8;
            this.lblStat3Label.Text = "Kritik Durum";
            // 
            // lblStat3Value
            // 
            this.lblStat3Value.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblStat3Value.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(27)))), ((int)(((byte)(62)))));
            this.lblStat3Value.Location = new System.Drawing.Point(455, 12);
            this.lblStat3Value.Name = "lblStat3Value";
            this.lblStat3Value.Size = new System.Drawing.Size(100, 35);
            this.lblStat3Value.TabIndex = 7;
            this.lblStat3Value.Text = "0";
            this.lblStat3Value.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStat3Icon
            // 
            this.lblStat3Icon.Font = new System.Drawing.Font("Segoe UI Emoji", 16F);
            this.lblStat3Icon.Location = new System.Drawing.Point(397, 12);
            this.lblStat3Icon.Name = "lblStat3Icon";
            this.lblStat3Icon.Size = new System.Drawing.Size(50, 35);
            this.lblStat3Icon.TabIndex = 6;
            this.lblStat3Icon.Text = "⚠️";
            // 
            // lblStat2Label
            // 
            this.lblStat2Label.Font = new System.Drawing.Font("Segoe UI Semibold", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblStat2Label.ForeColor = System.Drawing.Color.Coral;
            this.lblStat2Label.Location = new System.Drawing.Point(255, 47);
            this.lblStat2Label.Name = "lblStat2Label";
            this.lblStat2Label.Size = new System.Drawing.Size(100, 15);
            this.lblStat2Label.TabIndex = 5;
            this.lblStat2Label.Text = "Toplam Borç";
            // 
            // lblStat2Value
            // 
            this.lblStat2Value.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblStat2Value.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(27)))), ((int)(((byte)(62)))));
            this.lblStat2Value.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblStat2Value.Location = new System.Drawing.Point(260, 12);
            this.lblStat2Value.Name = "lblStat2Value";
            this.lblStat2Value.Size = new System.Drawing.Size(100, 35);
            this.lblStat2Value.TabIndex = 4;
            this.lblStat2Value.Text = "₺0";
            this.lblStat2Value.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStat2Icon
            // 
            this.lblStat2Icon.Font = new System.Drawing.Font("Segoe UI Emoji", 16F);
            this.lblStat2Icon.Location = new System.Drawing.Point(200, 12);
            this.lblStat2Icon.Name = "lblStat2Icon";
            this.lblStat2Icon.Size = new System.Drawing.Size(50, 35);
            this.lblStat2Icon.TabIndex = 3;
            this.lblStat2Icon.Text = "💰";
            // 
            // lblStat1Label
            // 
            this.lblStat1Label.Font = new System.Drawing.Font("Segoe UI Semibold", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblStat1Label.ForeColor = System.Drawing.Color.Black;
            this.lblStat1Label.Location = new System.Drawing.Point(55, 47);
            this.lblStat1Label.Name = "lblStat1Label";
            this.lblStat1Label.Size = new System.Drawing.Size(100, 15);
            this.lblStat1Label.TabIndex = 2;
            this.lblStat1Label.Text = "Toplam Borçlu";
            // 
            // lblStat1Value
            // 
            this.lblStat1Value.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblStat1Value.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(27)))), ((int)(((byte)(62)))));
            this.lblStat1Value.Location = new System.Drawing.Point(64, 12);
            this.lblStat1Value.Name = "lblStat1Value";
            this.lblStat1Value.Size = new System.Drawing.Size(100, 35);
            this.lblStat1Value.TabIndex = 1;
            this.lblStat1Value.Text = "0";
            this.lblStat1Value.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStat1Icon
            // 
            this.lblStat1Icon.Font = new System.Drawing.Font("Segoe UI Emoji", 16F);
            this.lblStat1Icon.Location = new System.Drawing.Point(11, 12);
            this.lblStat1Icon.Name = "lblStat1Icon";
            this.lblStat1Icon.Size = new System.Drawing.Size(50, 35);
            this.lblStat1Icon.TabIndex = 0;
            this.lblStat1Icon.Text = "👥";
            // 
            // panelFooter
            // 
            this.panelFooter.BackColor = System.Drawing.Color.MidnightBlue;
            this.panelFooter.Controls.Add(this.lblFooter);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 543);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(900, 57);
            this.panelFooter.TabIndex = 2;
            // 
            // lblFooter
            // 
            this.lblFooter.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblFooter.ForeColor = System.Drawing.Color.White;
            this.lblFooter.Location = new System.Drawing.Point(20, 18);
            this.lblFooter.Name = "lblFooter";
            this.lblFooter.Size = new System.Drawing.Size(600, 20);
            this.lblFooter.TabIndex = 0;
            this.lblFooter.Text = "Footer text here";
            // 
            // FormRaporPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelFooter);
            this.Controls.Add(this.panelHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormRaporPopup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Rapor Oluştur";
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.panelCards.ResumeLayout(false);
            this.panelCard6.ResumeLayout(false);
            this.panelCard5.ResumeLayout(false);
            this.panelCard4.ResumeLayout(false);
            this.panelCard3.ResumeLayout(false);
            this.panelCard2.ResumeLayout(false);
            this.panelCard1.ResumeLayout(false);
            this.panelStats.ResumeLayout(false);
            this.panelFooter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panelHeader;
        public System.Windows.Forms.Button btnClose;
        public System.Windows.Forms.Label lblSubtitle;
        public System.Windows.Forms.Label lblTitle;
        public System.Windows.Forms.Panel panelMain;
        public System.Windows.Forms.Panel panelCards;
        public System.Windows.Forms.Panel panelCard6;
        public System.Windows.Forms.Button btnCard6;
        public System.Windows.Forms.Label lblCard6Desc;
        public System.Windows.Forms.Label lblCard6Title;
        public System.Windows.Forms.Panel panelCard5;
        public System.Windows.Forms.Button btnCard5;
        public System.Windows.Forms.Label lblCard5Desc;
        public System.Windows.Forms.Label lblCard5Title;
        public System.Windows.Forms.Panel panelCard4;
        public System.Windows.Forms.Button btnCard4;
        public System.Windows.Forms.Label lblCard4Desc;
        public System.Windows.Forms.Label lblCard4Title;
        public System.Windows.Forms.Panel panelCard3;
        public System.Windows.Forms.Button btnCard3;
        public System.Windows.Forms.Label lblCard3Desc;
        public System.Windows.Forms.Label lblCard3Title;
        public System.Windows.Forms.Panel panelCard2;
        public System.Windows.Forms.Button btnCard2;
        public System.Windows.Forms.Label lblCard2Desc;
        public System.Windows.Forms.Label lblCard2Title;
        public System.Windows.Forms.Panel panelCard1;
        public System.Windows.Forms.Button btnCard1;
        public System.Windows.Forms.Label lblCard1Desc;
        public System.Windows.Forms.Label lblCard1Title;
        public System.Windows.Forms.Panel panelStats;
        public System.Windows.Forms.Label lblStat4Label;
        public System.Windows.Forms.Label lblStat4Value;
        public System.Windows.Forms.Label lblStat4Icon;
        public System.Windows.Forms.Label lblStat3Label;
        public System.Windows.Forms.Label lblStat3Value;
        public System.Windows.Forms.Label lblStat3Icon;
        public System.Windows.Forms.Label lblStat2Label;
        public System.Windows.Forms.Label lblStat2Value;
        public System.Windows.Forms.Label lblStat2Icon;
        public System.Windows.Forms.Label lblStat1Label;
        public System.Windows.Forms.Label lblStat1Value;
        public System.Windows.Forms.Label lblStat1Icon;
        public System.Windows.Forms.Panel panelFooter;
        public System.Windows.Forms.Label lblFooter;
    }
}