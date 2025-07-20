namespace Veresiye2025
{
    partial class FormIstatistik
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormIstatistik));
            this.pnlUst = new System.Windows.Forms.Panel();
            this.lblOzet = new System.Windows.Forms.Label();
            this.cmbTarihAraligi = new System.Windows.Forms.ComboBox();
            this.pnlSol = new System.Windows.Forms.Panel();
            this.dgvIstatistik = new System.Windows.Forms.DataGridView();
            this.Ay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ToplamBorc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ToplamTahsilat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AylikCiro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CiroDegisimi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CiroDurumu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlSag = new System.Windows.Forms.Panel();
            this.chartCiro = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.splitterYatay = new System.Windows.Forms.Splitter();
            this.yazdirrapor = new Guna.UI2.WinForms.Guna2Button();
            this.pnlUst.SuspendLayout();
            this.pnlSol.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIstatistik)).BeginInit();
            this.pnlSag.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartCiro)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlUst
            // 
            this.pnlUst.BackColor = System.Drawing.Color.LightGray;
            this.pnlUst.Controls.Add(this.yazdirrapor);
            this.pnlUst.Controls.Add(this.lblOzet);
            this.pnlUst.Controls.Add(this.cmbTarihAraligi);
            this.pnlUst.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlUst.Location = new System.Drawing.Point(0, 0);
            this.pnlUst.Name = "pnlUst";
            this.pnlUst.Size = new System.Drawing.Size(908, 50);
            this.pnlUst.TabIndex = 0;
            // 
            // lblOzet
            // 
            this.lblOzet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOzet.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblOzet.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(57)))), ((int)(((byte)(53)))));
            this.lblOzet.Location = new System.Drawing.Point(448, 15);
            this.lblOzet.Name = "lblOzet";
            this.lblOzet.Size = new System.Drawing.Size(448, 25);
            this.lblOzet.TabIndex = 2;
            this.lblOzet.Text = "Özet Bilgi";
            this.lblOzet.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbTarihAraligi
            // 
            this.cmbTarihAraligi.BackColor = System.Drawing.Color.LightGray;
            this.cmbTarihAraligi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTarihAraligi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbTarihAraligi.ForeColor = System.Drawing.Color.White;
            this.cmbTarihAraligi.FormattingEnabled = true;
            this.cmbTarihAraligi.Items.AddRange(new object[] {
            "Son 3 Ay",
            "Son 6 Ay",
            "Son 12 Ay",
            "Tüm Zamanlar"});
            this.cmbTarihAraligi.Location = new System.Drawing.Point(15, 15);
            this.cmbTarihAraligi.Name = "cmbTarihAraligi";
            this.cmbTarihAraligi.Size = new System.Drawing.Size(195, 28);
            this.cmbTarihAraligi.TabIndex = 0;
            this.cmbTarihAraligi.SelectedIndexChanged += new System.EventHandler(this.cmbTarihAraligi_SelectedIndexChanged);
            // 
            // pnlSol
            // 
            this.pnlSol.Controls.Add(this.dgvIstatistik);
            this.pnlSol.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSol.Location = new System.Drawing.Point(0, 50);
            this.pnlSol.Name = "pnlSol";
            this.pnlSol.Padding = new System.Windows.Forms.Padding(10);
            this.pnlSol.Size = new System.Drawing.Size(510, 468);
            this.pnlSol.TabIndex = 1;
            // 
            // dgvIstatistik
            // 
            this.dgvIstatistik.AllowUserToAddRows = false;
            this.dgvIstatistik.AllowUserToDeleteRows = false;
            this.dgvIstatistik.AllowUserToResizeColumns = false;
            this.dgvIstatistik.AllowUserToResizeRows = false;
            this.dgvIstatistik.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvIstatistik.BackgroundColor = System.Drawing.Color.White;
            this.dgvIstatistik.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvIstatistik.ColumnHeadersHeight = 40;
            this.dgvIstatistik.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvIstatistik.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Ay,
            this.ToplamBorc,
            this.ToplamTahsilat,
            this.AylikCiro,
            this.CiroDegisimi,
            this.CiroDurumu});
            this.dgvIstatistik.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvIstatistik.EnableHeadersVisualStyles = false;
            this.dgvIstatistik.Location = new System.Drawing.Point(10, 10);
            this.dgvIstatistik.Name = "dgvIstatistik";
            this.dgvIstatistik.ReadOnly = true;
            this.dgvIstatistik.RowHeadersVisible = false;
            this.dgvIstatistik.RowHeadersWidth = 51;
            this.dgvIstatistik.RowTemplate.Height = 35;
            this.dgvIstatistik.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvIstatistik.Size = new System.Drawing.Size(490, 448);
            this.dgvIstatistik.TabIndex = 0;
            // 
            // Ay
            // 
            this.Ay.HeaderText = "Ay";
            this.Ay.MinimumWidth = 6;
            this.Ay.Name = "Ay";
            this.Ay.ReadOnly = true;
            this.Ay.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ToplamBorc
            // 
            this.ToplamBorc.HeaderText = "Toplam Borç (₺)";
            this.ToplamBorc.MinimumWidth = 6;
            this.ToplamBorc.Name = "ToplamBorc";
            this.ToplamBorc.ReadOnly = true;
            this.ToplamBorc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ToplamTahsilat
            // 
            this.ToplamTahsilat.HeaderText = "Toplam Tahsilat (₺)";
            this.ToplamTahsilat.MinimumWidth = 6;
            this.ToplamTahsilat.Name = "ToplamTahsilat";
            this.ToplamTahsilat.ReadOnly = true;
            this.ToplamTahsilat.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // AylikCiro
            // 
            this.AylikCiro.HeaderText = "Aylık Ciro (₺)";
            this.AylikCiro.MinimumWidth = 6;
            this.AylikCiro.Name = "AylikCiro";
            this.AylikCiro.ReadOnly = true;
            this.AylikCiro.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // CiroDegisimi
            // 
            this.CiroDegisimi.HeaderText = "Ciro Değişimi (₺)";
            this.CiroDegisimi.MinimumWidth = 6;
            this.CiroDegisimi.Name = "CiroDegisimi";
            this.CiroDegisimi.ReadOnly = true;
            this.CiroDegisimi.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // CiroDurumu
            // 
            this.CiroDurumu.HeaderText = "Ciro Durumu";
            this.CiroDurumu.MinimumWidth = 6;
            this.CiroDurumu.Name = "CiroDurumu";
            this.CiroDurumu.ReadOnly = true;
            this.CiroDurumu.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // pnlSag
            // 
            this.pnlSag.Controls.Add(this.chartCiro);
            this.pnlSag.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSag.Location = new System.Drawing.Point(510, 50);
            this.pnlSag.Name = "pnlSag";
            this.pnlSag.Padding = new System.Windows.Forms.Padding(10);
            this.pnlSag.Size = new System.Drawing.Size(398, 468);
            this.pnlSag.TabIndex = 2;
            // 
            // chartCiro
            // 
            chartArea1.Name = "MainArea";
            this.chartCiro.ChartAreas.Add(chartArea1);
            this.chartCiro.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend";
            this.chartCiro.Legends.Add(legend1);
            this.chartCiro.Location = new System.Drawing.Point(10, 10);
            this.chartCiro.Name = "chartCiro";
            series1.ChartArea = "MainArea";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            series1.Legend = "Legend";
            series1.Name = "Ciro";
            series2.ChartArea = "MainArea";
            series2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(57)))), ((int)(((byte)(53)))));
            series2.Legend = "Legend";
            series2.Name = "Borç";
            series3.ChartArea = "MainArea";
            series3.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            series3.Legend = "Legend";
            series3.Name = "Tahsilat";
            this.chartCiro.Series.Add(series1);
            this.chartCiro.Series.Add(series2);
            this.chartCiro.Series.Add(series3);
            this.chartCiro.Size = new System.Drawing.Size(378, 448);
            this.chartCiro.TabIndex = 0;
            this.chartCiro.Text = "Ciro Grafiği";
            // 
            // splitterYatay
            // 
            this.splitterYatay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.splitterYatay.Location = new System.Drawing.Point(510, 50);
            this.splitterYatay.Name = "splitterYatay";
            this.splitterYatay.Size = new System.Drawing.Size(3, 468);
            this.splitterYatay.TabIndex = 3;
            this.splitterYatay.TabStop = false;
            // 
            // yazdirrapor
            // 
            this.yazdirrapor.Animated = true;
            this.yazdirrapor.AnimatedGIF = true;
            this.yazdirrapor.AutoRoundedCorners = true;
            this.yazdirrapor.BackColor = System.Drawing.Color.LightGray;
            this.yazdirrapor.BorderRadius = 16;
            this.yazdirrapor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.yazdirrapor.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.yazdirrapor.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.yazdirrapor.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.yazdirrapor.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.yazdirrapor.FillColor = System.Drawing.Color.DodgerBlue;
            this.yazdirrapor.FocusedColor = System.Drawing.Color.White;
            this.yazdirrapor.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.yazdirrapor.ForeColor = System.Drawing.Color.White;
            this.yazdirrapor.Image = global::Veresiye2025.Properties.Resources.yazici;
            this.yazdirrapor.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.yazdirrapor.ImageSize = new System.Drawing.Size(30, 30);
            this.yazdirrapor.IndicateFocus = true;
            this.yazdirrapor.Location = new System.Drawing.Point(239, 10);
            this.yazdirrapor.Name = "yazdirrapor";
            this.yazdirrapor.Size = new System.Drawing.Size(125, 34);
            this.yazdirrapor.TabIndex = 52;
            this.yazdirrapor.Text = "Yazdır";
            this.yazdirrapor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.yazdirrapor.Click += new System.EventHandler(this.yazdirrapor_Click);
            // 
            // FormIstatistik
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(908, 518);
            this.Controls.Add(this.splitterYatay);
            this.Controls.Add(this.pnlSag);
            this.Controls.Add(this.pnlSol);
            this.Controls.Add(this.pnlUst);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "FormIstatistik";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cari İstatistik";
            this.pnlUst.ResumeLayout(false);
            this.pnlSol.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvIstatistik)).EndInit();
            this.pnlSag.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartCiro)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion
        public System.Windows.Forms.Panel pnlUst;
        public System.Windows.Forms.Panel pnlSol;
        public System.Windows.Forms.Panel pnlSag;
        public System.Windows.Forms.ComboBox cmbTarihAraligi;
        public System.Windows.Forms.Label lblOzet;
        public System.Windows.Forms.DataGridView dgvIstatistik;
        public System.Windows.Forms.DataGridViewTextBoxColumn Ay;
        public System.Windows.Forms.DataGridViewTextBoxColumn ToplamBorc;
        public System.Windows.Forms.DataGridViewTextBoxColumn ToplamTahsilat;
        public System.Windows.Forms.DataGridViewTextBoxColumn AylikCiro;
        public System.Windows.Forms.DataGridViewTextBoxColumn CiroDegisimi;
        public System.Windows.Forms.DataGridViewTextBoxColumn CiroDurumu;
        public System.Windows.Forms.DataVisualization.Charting.Chart chartCiro;
        public System.Windows.Forms.Splitter splitterYatay;
        public Guna.UI2.WinForms.Guna2Button yazdirrapor;
    }
}