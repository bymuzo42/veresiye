using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Veresiye2025
{
    partial class StatisticsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        public System.ComponentModel.IContainer components = null;

        // Form için yuvarlak köşeler ve gölge
        public const int WM_NCHITTEST = 0x84;
        public const int HTCLIENT = 0x1;
        public const int HTCAPTION = 0x2;
        public bool m_aeroEnabled;
        public const int CS_DROPSHADOW = 0x00020000;
        public const int WM_NCPAINT = 0x0085;
        public const int WM_ACTIVATEAPP = 0x001C;

        [DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);
        [DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);
        [DllImport("dwmapi.dll")]
        public static extern int DwmIsCompositionEnabled(ref int pfEnabled);
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        public static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
         );

        public struct MARGINS
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                m_aeroEnabled = CheckAeroEnabled();
                CreateParams cp = base.CreateParams;
                if (!m_aeroEnabled)
                    cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }

        public bool CheckAeroEnabled()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                int enabled = 0;
                DwmIsCompositionEnabled(ref enabled);
                return (enabled == 1) ? true : false;
            }
            return false;
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCPAINT:
                    if (m_aeroEnabled)
                    {
                        var v = 2;
                        DwmSetWindowAttribute(this.Handle, 2, ref v, 4);
                        MARGINS margins = new MARGINS()
                        {
                            bottomHeight = 1,
                            leftWidth = 0,
                            rightWidth = 0,
                            topHeight = 0
                        };
                        DwmExtendFrameIntoClientArea(this.Handle, ref margins);
                    }
                    break;
                default:
                    break;
            }
            base.WndProc(ref m);

            if (m.Msg == WM_NCHITTEST && (int)m.Result == HTCLIENT)
            {
                m.Result = (IntPtr)HTCAPTION;
            }
        }

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
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.chkShowPopup = new System.Windows.Forms.CheckBox();
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelTargetInput = new System.Windows.Forms.Panel();
            this.btnSaveTarget = new System.Windows.Forms.Button();
            this.txtTarget = new System.Windows.Forms.TextBox();
            this.lblTarget = new System.Windows.Forms.Label();
            this.btnDetailedReport = new System.Windows.Forms.Button();
            this.btnExportPdf = new System.Windows.Forms.Button();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.flowLayoutPanelSummary = new System.Windows.Forms.FlowLayoutPanel();
            this.panelMonthlyTotal = new System.Windows.Forms.Panel();
            this.panelMTColorBar = new System.Windows.Forms.Panel();
            this.panelMTContent = new System.Windows.Forms.Panel();
            this.lblMTUnit = new System.Windows.Forms.Label();
            this.lblMTValue = new System.Windows.Forms.Label();
            this.lblMTTitle = new System.Windows.Forms.Label();
            this.panelTodayIncoming = new System.Windows.Forms.Panel();
            this.panelTIColorBar = new System.Windows.Forms.Panel();
            this.panelTIContent = new System.Windows.Forms.Panel();
            this.lblTIUnit = new System.Windows.Forms.Label();
            this.lblTIValue = new System.Windows.Forms.Label();
            this.lblTITitle = new System.Windows.Forms.Label();
            this.panelBankBlock = new System.Windows.Forms.Panel();
            this.panelBBColorBar = new System.Windows.Forms.Panel();
            this.panelBBContent = new System.Windows.Forms.Panel();
            this.lblBBUnit = new System.Windows.Forms.Label();
            this.lblBBValue = new System.Windows.Forms.Label();
            this.lblBBTitle = new System.Windows.Forms.Label();
            this.panelValor = new System.Windows.Forms.Panel();
            this.panelVColorBar = new System.Windows.Forms.Panel();
            this.panelVContent = new System.Windows.Forms.Panel();
            this.lblVUnit = new System.Windows.Forms.Label();
            this.lblVValue = new System.Windows.Forms.Label();
            this.lblVTitle = new System.Windows.Forms.Label();
            this.panelMostUsedBank = new System.Windows.Forms.Panel();
            this.panelMUBColorBar = new System.Windows.Forms.Panel();
            this.panelMUBContent = new System.Windows.Forms.Panel();
            this.lblMUBValue = new System.Windows.Forms.Label();
            this.lblMUBTitle = new System.Windows.Forms.Label();
            this.panelMostActiveDevice = new System.Windows.Forms.Panel();
            this.panelMADColorBar = new System.Windows.Forms.Panel();
            this.panelMADContent = new System.Windows.Forms.Panel();
            this.lblMADValue = new System.Windows.Forms.Label();
            this.lblMADTitle = new System.Windows.Forms.Label();
            this.panelTarget = new System.Windows.Forms.Panel();
            this.panelTColorBar = new System.Windows.Forms.Panel();
            this.panelTContent = new System.Windows.Forms.Panel();
            this.lblTUnit = new System.Windows.Forms.Label();
            this.lblTValue = new System.Windows.Forms.Label();
            this.lblTTitle = new System.Windows.Forms.Label();
            this.tabControlStats = new System.Windows.Forms.TabControl();
            this.tabPageCharts = new System.Windows.Forms.TabPage();
            this.chartMonthly = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartBankDist = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabPageBanks = new System.Windows.Forms.TabPage();
            this.tabPageDevices = new System.Windows.Forms.TabPage();
            this.tabPageTargets = new System.Windows.Forms.TabPage();
            this.panelHeader.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.panelTargetInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.flowLayoutPanelSummary.SuspendLayout();
            this.panelMonthlyTotal.SuspendLayout();
            this.panelMTContent.SuspendLayout();
            this.panelTodayIncoming.SuspendLayout();
            this.panelTIContent.SuspendLayout();
            this.panelBankBlock.SuspendLayout();
            this.panelBBContent.SuspendLayout();
            this.panelValor.SuspendLayout();
            this.panelVContent.SuspendLayout();
            this.panelMostUsedBank.SuspendLayout();
            this.panelMUBContent.SuspendLayout();
            this.panelMostActiveDevice.SuspendLayout();
            this.panelMADContent.SuspendLayout();
            this.panelTarget.SuspendLayout();
            this.panelTContent.SuspendLayout();
            this.tabControlStats.SuspendLayout();
            this.tabPageCharts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartMonthly)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartBankDist)).BeginInit();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.panelHeader.Controls.Add(this.btnClose);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(4);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1200, 62);
            this.panelHeader.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(1140, 6);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(53, 49);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "X";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(27, 18);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(265, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Aylık POS İstatistikleri";
            // 
            // chkShowPopup
            // 
            this.chkShowPopup.AutoSize = true;
            this.chkShowPopup.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkShowPopup.Location = new System.Drawing.Point(27, 68);
            this.chkShowPopup.Margin = new System.Windows.Forms.Padding(4);
            this.chkShowPopup.Name = "chkShowPopup";
            this.chkShowPopup.Size = new System.Drawing.Size(319, 24);
            this.chkShowPopup.TabIndex = 1;
            this.chkShowPopup.Text = "Uygulama başlangıcında istatistikleri göster";
            this.chkShowPopup.UseVisualStyleBackColor = true;
            this.chkShowPopup.CheckedChanged += new System.EventHandler(this.chkShowPopup_CheckedChanged);
            // 
            // panelMain
            // 
            this.panelMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelMain.BackColor = System.Drawing.Color.White;
            this.panelMain.Controls.Add(this.panelTargetInput);
            this.panelMain.Controls.Add(this.btnDetailedReport);
            this.panelMain.Controls.Add(this.btnExportPdf);
            this.panelMain.Controls.Add(this.btnExportExcel);
            this.panelMain.Controls.Add(this.splitContainerMain);
            this.panelMain.Location = new System.Drawing.Point(0, 98);
            this.panelMain.Margin = new System.Windows.Forms.Padding(4);
            this.panelMain.Name = "panelMain";
            this.panelMain.Padding = new System.Windows.Forms.Padding(27, 25, 27, 25);
            this.panelMain.Size = new System.Drawing.Size(1200, 640);
            this.panelMain.TabIndex = 2;
            // 
            // panelTargetInput
            // 
            this.panelTargetInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panelTargetInput.Controls.Add(this.btnSaveTarget);
            this.panelTargetInput.Controls.Add(this.txtTarget);
            this.panelTargetInput.Controls.Add(this.lblTarget);
            this.panelTargetInput.Location = new System.Drawing.Point(27, 554);
            this.panelTargetInput.Margin = new System.Windows.Forms.Padding(4);
            this.panelTargetInput.Name = "panelTargetInput";
            this.panelTargetInput.Size = new System.Drawing.Size(427, 49);
            this.panelTargetInput.TabIndex = 4;
            // 
            // btnSaveTarget
            // 
            this.btnSaveTarget.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnSaveTarget.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveTarget.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSaveTarget.ForeColor = System.Drawing.Color.White;
            this.btnSaveTarget.Location = new System.Drawing.Point(307, 6);
            this.btnSaveTarget.Margin = new System.Windows.Forms.Padding(4);
            this.btnSaveTarget.Name = "btnSaveTarget";
            this.btnSaveTarget.Size = new System.Drawing.Size(107, 37);
            this.btnSaveTarget.TabIndex = 2;
            this.btnSaveTarget.Text = "Kaydet";
            this.btnSaveTarget.UseVisualStyleBackColor = false;
            this.btnSaveTarget.Click += new System.EventHandler(this.btnSaveTarget_Click);
            // 
            // txtTarget
            // 
            this.txtTarget.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTarget.Location = new System.Drawing.Point(133, 9);
            this.txtTarget.Margin = new System.Windows.Forms.Padding(4);
            this.txtTarget.Name = "txtTarget";
            this.txtTarget.Size = new System.Drawing.Size(159, 27);
            this.txtTarget.TabIndex = 1;
            this.txtTarget.Text = "100,000.00";
            // 
            // lblTarget
            // 
            this.lblTarget.AutoSize = true;
            this.lblTarget.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTarget.Location = new System.Drawing.Point(0, 12);
            this.lblTarget.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTarget.Name = "lblTarget";
            this.lblTarget.Size = new System.Drawing.Size(111, 20);
            this.lblTarget.TabIndex = 0;
            this.lblTarget.Text = "Aylık Hedef (₺):";
            // 
            // btnDetailedReport
            // 
            this.btnDetailedReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDetailedReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnDetailedReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDetailedReport.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnDetailedReport.ForeColor = System.Drawing.Color.White;
            this.btnDetailedReport.Location = new System.Drawing.Point(547, 554);
            this.btnDetailedReport.Margin = new System.Windows.Forms.Padding(4);
            this.btnDetailedReport.Name = "btnDetailedReport";
            this.btnDetailedReport.Size = new System.Drawing.Size(200, 49);
            this.btnDetailedReport.TabIndex = 3;
            this.btnDetailedReport.Text = "Detaylı Rapor";
            this.btnDetailedReport.UseVisualStyleBackColor = false;
            this.btnDetailedReport.Click += new System.EventHandler(this.btnDetailedReport_Click);
            // 
            // btnExportPdf
            // 
            this.btnExportPdf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportPdf.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnExportPdf.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportPdf.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnExportPdf.ForeColor = System.Drawing.Color.White;
            this.btnExportPdf.Location = new System.Drawing.Point(973, 554);
            this.btnExportPdf.Margin = new System.Windows.Forms.Padding(4);
            this.btnExportPdf.Name = "btnExportPdf";
            this.btnExportPdf.Size = new System.Drawing.Size(200, 49);
            this.btnExportPdf.TabIndex = 2;
            this.btnExportPdf.Text = "PDF\'e Aktar";
            this.btnExportPdf.UseVisualStyleBackColor = false;
            this.btnExportPdf.Click += new System.EventHandler(this.btnExportPdf_Click);
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnExportExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportExcel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnExportExcel.ForeColor = System.Drawing.Color.White;
            this.btnExportExcel.Location = new System.Drawing.Point(760, 554);
            this.btnExportExcel.Margin = new System.Windows.Forms.Padding(4);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(200, 49);
            this.btnExportExcel.TabIndex = 1;
            this.btnExportExcel.Text = "Excel\'e Aktar";
            this.btnExportExcel.UseVisualStyleBackColor = false;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerMain.Location = new System.Drawing.Point(27, 25);
            this.splitContainerMain.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.flowLayoutPanelSummary);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.tabControlStats);
            this.splitContainerMain.Size = new System.Drawing.Size(1147, 517);
            this.splitContainerMain.SplitterDistance = 466;
            this.splitContainerMain.SplitterWidth = 5;
            this.splitContainerMain.TabIndex = 0;
            // 
            // flowLayoutPanelSummary
            // 
            this.flowLayoutPanelSummary.AutoScroll = true;
            this.flowLayoutPanelSummary.Controls.Add(this.panelMonthlyTotal);
            this.flowLayoutPanelSummary.Controls.Add(this.panelTodayIncoming);
            this.flowLayoutPanelSummary.Controls.Add(this.panelBankBlock);
            this.flowLayoutPanelSummary.Controls.Add(this.panelValor);
            this.flowLayoutPanelSummary.Controls.Add(this.panelMostUsedBank);
            this.flowLayoutPanelSummary.Controls.Add(this.panelMostActiveDevice);
            this.flowLayoutPanelSummary.Controls.Add(this.panelTarget);
            this.flowLayoutPanelSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelSummary.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelSummary.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanelSummary.Margin = new System.Windows.Forms.Padding(4);
            this.flowLayoutPanelSummary.Name = "flowLayoutPanelSummary";
            this.flowLayoutPanelSummary.Size = new System.Drawing.Size(466, 517);
            this.flowLayoutPanelSummary.TabIndex = 0;
            this.flowLayoutPanelSummary.WrapContents = false;
            // 
            // panelMonthlyTotal
            // 
            this.panelMonthlyTotal.BackColor = System.Drawing.Color.White;
            this.panelMonthlyTotal.Controls.Add(this.panelMTColorBar);
            this.panelMonthlyTotal.Controls.Add(this.panelMTContent);
            this.panelMonthlyTotal.Location = new System.Drawing.Point(4, 4);
            this.panelMonthlyTotal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 12);
            this.panelMonthlyTotal.Name = "panelMonthlyTotal";
            this.panelMonthlyTotal.Size = new System.Drawing.Size(426, 110);
            this.panelMonthlyTotal.TabIndex = 0;
            // 
            // panelMTColorBar
            // 
            this.panelMTColorBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.panelMTColorBar.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMTColorBar.Location = new System.Drawing.Point(0, 0);
            this.panelMTColorBar.Margin = new System.Windows.Forms.Padding(4);
            this.panelMTColorBar.Name = "panelMTColorBar";
            this.panelMTColorBar.Size = new System.Drawing.Size(7, 110);
            this.panelMTColorBar.TabIndex = 0;
            // 
            // panelMTContent
            // 
            this.panelMTContent.Controls.Add(this.lblMTUnit);
            this.panelMTContent.Controls.Add(this.lblMTValue);
            this.panelMTContent.Controls.Add(this.lblMTTitle);
            this.panelMTContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMTContent.Location = new System.Drawing.Point(0, 0);
            this.panelMTContent.Margin = new System.Windows.Forms.Padding(4);
            this.panelMTContent.Name = "panelMTContent";
            this.panelMTContent.Padding = new System.Windows.Forms.Padding(13, 6, 7, 6);
            this.panelMTContent.Size = new System.Drawing.Size(426, 110);
            this.panelMTContent.TabIndex = 1;
            // 
            // lblMTUnit
            // 
            this.lblMTUnit.AutoSize = true;
            this.lblMTUnit.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblMTUnit.ForeColor = System.Drawing.Color.Gray;
            this.lblMTUnit.Location = new System.Drawing.Point(340, 52);
            this.lblMTUnit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMTUnit.Name = "lblMTUnit";
            this.lblMTUnit.Size = new System.Drawing.Size(19, 23);
            this.lblMTUnit.TabIndex = 2;
            this.lblMTUnit.Text = "₺";
            // 
            // lblMTValue
            // 
            this.lblMTValue.AutoSize = true;
            this.lblMTValue.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblMTValue.ForeColor = System.Drawing.Color.Black;
            this.lblMTValue.Location = new System.Drawing.Point(17, 37);
            this.lblMTValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMTValue.Name = "lblMTValue";
            this.lblMTValue.Size = new System.Drawing.Size(77, 41);
            this.lblMTValue.TabIndex = 1;
            this.lblMTValue.Tag = "value";
            this.lblMTValue.Text = "0.00";
            // 
            // lblMTTitle
            // 
            this.lblMTTitle.AutoSize = true;
            this.lblMTTitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblMTTitle.ForeColor = System.Drawing.Color.Gray;
            this.lblMTTitle.Location = new System.Drawing.Point(17, 6);
            this.lblMTTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMTTitle.Name = "lblMTTitle";
            this.lblMTTitle.Size = new System.Drawing.Size(105, 23);
            this.lblMTTitle.TabIndex = 0;
            this.lblMTTitle.Text = "Aylık Toplam";
            // 
            // panelTodayIncoming
            // 
            this.panelTodayIncoming.BackColor = System.Drawing.Color.White;
            this.panelTodayIncoming.Controls.Add(this.panelTIColorBar);
            this.panelTodayIncoming.Controls.Add(this.panelTIContent);
            this.panelTodayIncoming.Location = new System.Drawing.Point(4, 130);
            this.panelTodayIncoming.Margin = new System.Windows.Forms.Padding(4, 4, 4, 12);
            this.panelTodayIncoming.Name = "panelTodayIncoming";
            this.panelTodayIncoming.Size = new System.Drawing.Size(426, 110);
            this.panelTodayIncoming.TabIndex = 1;
            // 
            // panelTIColorBar
            // 
            this.panelTIColorBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.panelTIColorBar.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelTIColorBar.Location = new System.Drawing.Point(0, 0);
            this.panelTIColorBar.Margin = new System.Windows.Forms.Padding(4);
            this.panelTIColorBar.Name = "panelTIColorBar";
            this.panelTIColorBar.Size = new System.Drawing.Size(7, 110);
            this.panelTIColorBar.TabIndex = 0;
            // 
            // panelTIContent
            // 
            this.panelTIContent.Controls.Add(this.lblTIUnit);
            this.panelTIContent.Controls.Add(this.lblTIValue);
            this.panelTIContent.Controls.Add(this.lblTITitle);
            this.panelTIContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTIContent.Location = new System.Drawing.Point(0, 0);
            this.panelTIContent.Margin = new System.Windows.Forms.Padding(4);
            this.panelTIContent.Name = "panelTIContent";
            this.panelTIContent.Padding = new System.Windows.Forms.Padding(13, 6, 7, 6);
            this.panelTIContent.Size = new System.Drawing.Size(426, 110);
            this.panelTIContent.TabIndex = 1;
            // 
            // lblTIUnit
            // 
            this.lblTIUnit.AutoSize = true;
            this.lblTIUnit.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTIUnit.ForeColor = System.Drawing.Color.Gray;
            this.lblTIUnit.Location = new System.Drawing.Point(340, 52);
            this.lblTIUnit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTIUnit.Name = "lblTIUnit";
            this.lblTIUnit.Size = new System.Drawing.Size(19, 23);
            this.lblTIUnit.TabIndex = 2;
            this.lblTIUnit.Text = "₺";
            // 
            // lblTIValue
            // 
            this.lblTIValue.AutoSize = true;
            this.lblTIValue.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTIValue.ForeColor = System.Drawing.Color.Black;
            this.lblTIValue.Location = new System.Drawing.Point(17, 37);
            this.lblTIValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTIValue.Name = "lblTIValue";
            this.lblTIValue.Size = new System.Drawing.Size(77, 41);
            this.lblTIValue.TabIndex = 1;
            this.lblTIValue.Tag = "value";
            this.lblTIValue.Text = "0.00";
            // 
            // lblTITitle
            // 
            this.lblTITitle.AutoSize = true;
            this.lblTITitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTITitle.ForeColor = System.Drawing.Color.Gray;
            this.lblTITitle.Location = new System.Drawing.Point(17, 6);
            this.lblTITitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTITitle.Name = "lblTITitle";
            this.lblTITitle.Size = new System.Drawing.Size(189, 23);
            this.lblTITitle.TabIndex = 0;
            this.lblTITitle.Text = "Bugün Hesaba Geçecek";
            // 
            // panelBankBlock
            // 
            this.panelBankBlock.BackColor = System.Drawing.Color.White;
            this.panelBankBlock.Controls.Add(this.panelBBColorBar);
            this.panelBankBlock.Controls.Add(this.panelBBContent);
            this.panelBankBlock.Location = new System.Drawing.Point(4, 256);
            this.panelBankBlock.Margin = new System.Windows.Forms.Padding(4, 4, 4, 12);
            this.panelBankBlock.Name = "panelBankBlock";
            this.panelBankBlock.Size = new System.Drawing.Size(426, 110);
            this.panelBankBlock.TabIndex = 2;
            // 
            // panelBBColorBar
            // 
            this.panelBBColorBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.panelBBColorBar.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelBBColorBar.Location = new System.Drawing.Point(0, 0);
            this.panelBBColorBar.Margin = new System.Windows.Forms.Padding(4);
            this.panelBBColorBar.Name = "panelBBColorBar";
            this.panelBBColorBar.Size = new System.Drawing.Size(7, 110);
            this.panelBBColorBar.TabIndex = 0;
            // 
            // panelBBContent
            // 
            this.panelBBContent.Controls.Add(this.lblBBUnit);
            this.panelBBContent.Controls.Add(this.lblBBValue);
            this.panelBBContent.Controls.Add(this.lblBBTitle);
            this.panelBBContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBBContent.Location = new System.Drawing.Point(0, 0);
            this.panelBBContent.Margin = new System.Windows.Forms.Padding(4);
            this.panelBBContent.Name = "panelBBContent";
            this.panelBBContent.Padding = new System.Windows.Forms.Padding(13, 6, 7, 6);
            this.panelBBContent.Size = new System.Drawing.Size(426, 110);
            this.panelBBContent.TabIndex = 1;
            // 
            // lblBBUnit
            // 
            this.lblBBUnit.AutoSize = true;
            this.lblBBUnit.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblBBUnit.ForeColor = System.Drawing.Color.Gray;
            this.lblBBUnit.Location = new System.Drawing.Point(340, 52);
            this.lblBBUnit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBBUnit.Name = "lblBBUnit";
            this.lblBBUnit.Size = new System.Drawing.Size(19, 23);
            this.lblBBUnit.TabIndex = 2;
            this.lblBBUnit.Text = "₺";
            // 
            // lblBBValue
            // 
            this.lblBBValue.AutoSize = true;
            this.lblBBValue.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblBBValue.ForeColor = System.Drawing.Color.Black;
            this.lblBBValue.Location = new System.Drawing.Point(17, 37);
            this.lblBBValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBBValue.Name = "lblBBValue";
            this.lblBBValue.Size = new System.Drawing.Size(77, 41);
            this.lblBBValue.TabIndex = 1;
            this.lblBBValue.Tag = "value";
            this.lblBBValue.Text = "0.00";
            // 
            // lblBBTitle
            // 
            this.lblBBTitle.AutoSize = true;
            this.lblBBTitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblBBTitle.ForeColor = System.Drawing.Color.Gray;
            this.lblBBTitle.Location = new System.Drawing.Point(17, 6);
            this.lblBBTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBBTitle.Name = "lblBBTitle";
            this.lblBBTitle.Size = new System.Drawing.Size(173, 23);
            this.lblBBTitle.TabIndex = 0;
            this.lblBBTitle.Text = "Toplam Banka Blokesi";
            // 
            // panelValor
            // 
            this.panelValor.BackColor = System.Drawing.Color.White;
            this.panelValor.Controls.Add(this.panelVColorBar);
            this.panelValor.Controls.Add(this.panelVContent);
            this.panelValor.Location = new System.Drawing.Point(4, 382);
            this.panelValor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 12);
            this.panelValor.Name = "panelValor";
            this.panelValor.Size = new System.Drawing.Size(426, 110);
            this.panelValor.TabIndex = 3;
            // 
            // panelVColorBar
            // 
            this.panelVColorBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(162)))), ((int)(((byte)(184)))));
            this.panelVColorBar.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelVColorBar.Location = new System.Drawing.Point(0, 0);
            this.panelVColorBar.Margin = new System.Windows.Forms.Padding(4);
            this.panelVColorBar.Name = "panelVColorBar";
            this.panelVColorBar.Size = new System.Drawing.Size(7, 110);
            this.panelVColorBar.TabIndex = 0;
            // 
            // panelVContent
            // 
            this.panelVContent.Controls.Add(this.lblVUnit);
            this.panelVContent.Controls.Add(this.lblVValue);
            this.panelVContent.Controls.Add(this.lblVTitle);
            this.panelVContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelVContent.Location = new System.Drawing.Point(0, 0);
            this.panelVContent.Margin = new System.Windows.Forms.Padding(4);
            this.panelVContent.Name = "panelVContent";
            this.panelVContent.Padding = new System.Windows.Forms.Padding(13, 6, 7, 6);
            this.panelVContent.Size = new System.Drawing.Size(426, 110);
            this.panelVContent.TabIndex = 1;
            // 
            // lblVUnit
            // 
            this.lblVUnit.AutoSize = true;
            this.lblVUnit.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblVUnit.ForeColor = System.Drawing.Color.Gray;
            this.lblVUnit.Location = new System.Drawing.Point(340, 52);
            this.lblVUnit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblVUnit.Name = "lblVUnit";
            this.lblVUnit.Size = new System.Drawing.Size(40, 23);
            this.lblVUnit.TabIndex = 2;
            this.lblVUnit.Text = "gün";
            // 
            // lblVValue
            // 
            this.lblVValue.AutoSize = true;
            this.lblVValue.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblVValue.ForeColor = System.Drawing.Color.Black;
            this.lblVValue.Location = new System.Drawing.Point(17, 37);
            this.lblVValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblVValue.Name = "lblVValue";
            this.lblVValue.Size = new System.Drawing.Size(60, 41);
            this.lblVValue.TabIndex = 1;
            this.lblVValue.Tag = "value";
            this.lblVValue.Text = "0.0";
            // 
            // lblVTitle
            // 
            this.lblVTitle.AutoSize = true;
            this.lblVTitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblVTitle.ForeColor = System.Drawing.Color.Gray;
            this.lblVTitle.Location = new System.Drawing.Point(17, 6);
            this.lblVTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblVTitle.Name = "lblVTitle";
            this.lblVTitle.Size = new System.Drawing.Size(125, 23);
            this.lblVTitle.TabIndex = 0;
            this.lblVTitle.Text = "Ortalama Valör";
            // 
            // panelMostUsedBank
            // 
            this.panelMostUsedBank.BackColor = System.Drawing.Color.White;
            this.panelMostUsedBank.Controls.Add(this.panelMUBColorBar);
            this.panelMostUsedBank.Controls.Add(this.panelMUBContent);
            this.panelMostUsedBank.Location = new System.Drawing.Point(4, 508);
            this.panelMostUsedBank.Margin = new System.Windows.Forms.Padding(4, 4, 4, 12);
            this.panelMostUsedBank.Name = "panelMostUsedBank";
            this.panelMostUsedBank.Size = new System.Drawing.Size(426, 110);
            this.panelMostUsedBank.TabIndex = 4;
            // 
            // panelMUBColorBar
            // 
            this.panelMUBColorBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(66)))), ((int)(((byte)(193)))));
            this.panelMUBColorBar.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMUBColorBar.Location = new System.Drawing.Point(0, 0);
            this.panelMUBColorBar.Margin = new System.Windows.Forms.Padding(4);
            this.panelMUBColorBar.Name = "panelMUBColorBar";
            this.panelMUBColorBar.Size = new System.Drawing.Size(7, 110);
            this.panelMUBColorBar.TabIndex = 0;
            // 
            // panelMUBContent
            // 
            this.panelMUBContent.Controls.Add(this.lblMUBValue);
            this.panelMUBContent.Controls.Add(this.lblMUBTitle);
            this.panelMUBContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMUBContent.Location = new System.Drawing.Point(0, 0);
            this.panelMUBContent.Margin = new System.Windows.Forms.Padding(4);
            this.panelMUBContent.Name = "panelMUBContent";
            this.panelMUBContent.Padding = new System.Windows.Forms.Padding(13, 6, 7, 6);
            this.panelMUBContent.Size = new System.Drawing.Size(426, 110);
            this.panelMUBContent.TabIndex = 1;
            // 
            // lblMUBValue
            // 
            this.lblMUBValue.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblMUBValue.ForeColor = System.Drawing.Color.Black;
            this.lblMUBValue.Location = new System.Drawing.Point(17, 37);
            this.lblMUBValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMUBValue.Name = "lblMUBValue";
            this.lblMUBValue.Size = new System.Drawing.Size(396, 65);
            this.lblMUBValue.TabIndex = 1;
            this.lblMUBValue.Tag = "value";
            this.lblMUBValue.Text = "-";
            // 
            // lblMUBTitle
            // 
            this.lblMUBTitle.AutoSize = true;
            this.lblMUBTitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblMUBTitle.ForeColor = System.Drawing.Color.Gray;
            this.lblMUBTitle.Location = new System.Drawing.Point(17, 6);
            this.lblMUBTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMUBTitle.Name = "lblMUBTitle";
            this.lblMUBTitle.Size = new System.Drawing.Size(193, 23);
            this.lblMUBTitle.TabIndex = 0;
            this.lblMUBTitle.Text = "En Çok Kullanılan Banka";
            // 
            // panelMostActiveDevice
            // 
            this.panelMostActiveDevice.BackColor = System.Drawing.Color.White;
            this.panelMostActiveDevice.Controls.Add(this.panelMADColorBar);
            this.panelMostActiveDevice.Controls.Add(this.panelMADContent);
            this.panelMostActiveDevice.Location = new System.Drawing.Point(4, 634);
            this.panelMostActiveDevice.Margin = new System.Windows.Forms.Padding(4, 4, 4, 12);
            this.panelMostActiveDevice.Name = "panelMostActiveDevice";
            this.panelMostActiveDevice.Size = new System.Drawing.Size(426, 110);
            this.panelMostActiveDevice.TabIndex = 5;
            // 
            // panelMADColorBar
            // 
            this.panelMADColorBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(162)))), ((int)(((byte)(184)))));
            this.panelMADColorBar.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMADColorBar.Location = new System.Drawing.Point(0, 0);
            this.panelMADColorBar.Margin = new System.Windows.Forms.Padding(4);
            this.panelMADColorBar.Name = "panelMADColorBar";
            this.panelMADColorBar.Size = new System.Drawing.Size(7, 110);
            this.panelMADColorBar.TabIndex = 0;
            // 
            // panelMADContent
            // 
            this.panelMADContent.Controls.Add(this.lblMADValue);
            this.panelMADContent.Controls.Add(this.lblMADTitle);
            this.panelMADContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMADContent.Location = new System.Drawing.Point(0, 0);
            this.panelMADContent.Margin = new System.Windows.Forms.Padding(4);
            this.panelMADContent.Name = "panelMADContent";
            this.panelMADContent.Padding = new System.Windows.Forms.Padding(13, 6, 7, 6);
            this.panelMADContent.Size = new System.Drawing.Size(426, 110);
            this.panelMADContent.TabIndex = 1;
            // 
            // lblMADValue
            // 
            this.lblMADValue.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblMADValue.ForeColor = System.Drawing.Color.Black;
            this.lblMADValue.Location = new System.Drawing.Point(17, 37);
            this.lblMADValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMADValue.Name = "lblMADValue";
            this.lblMADValue.Size = new System.Drawing.Size(396, 65);
            this.lblMADValue.TabIndex = 1;
            this.lblMADValue.Tag = "value";
            this.lblMADValue.Text = "-";
            // 
            // lblMADTitle
            // 
            this.lblMADTitle.AutoSize = true;
            this.lblMADTitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblMADTitle.ForeColor = System.Drawing.Color.Gray;
            this.lblMADTitle.Location = new System.Drawing.Point(17, 6);
            this.lblMADTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMADTitle.Name = "lblMADTitle";
            this.lblMADTitle.Size = new System.Drawing.Size(115, 23);
            this.lblMADTitle.TabIndex = 0;
            this.lblMADTitle.Text = "En Aktif Cihaz";
            // 
            // panelTarget
            // 
            this.panelTarget.BackColor = System.Drawing.Color.White;
            this.panelTarget.Controls.Add(this.panelTColorBar);
            this.panelTarget.Controls.Add(this.panelTContent);
            this.panelTarget.Location = new System.Drawing.Point(4, 760);
            this.panelTarget.Margin = new System.Windows.Forms.Padding(4, 4, 4, 12);
            this.panelTarget.Name = "panelTarget";
            this.panelTarget.Size = new System.Drawing.Size(426, 110);
            this.panelTarget.TabIndex = 6;
            // 
            // panelTColorBar
            // 
            this.panelTColorBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.panelTColorBar.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelTColorBar.Location = new System.Drawing.Point(0, 0);
            this.panelTColorBar.Margin = new System.Windows.Forms.Padding(4);
            this.panelTColorBar.Name = "panelTColorBar";
            this.panelTColorBar.Size = new System.Drawing.Size(7, 110);
            this.panelTColorBar.TabIndex = 0;
            // 
            // panelTContent
            // 
            this.panelTContent.Controls.Add(this.lblTUnit);
            this.panelTContent.Controls.Add(this.lblTValue);
            this.panelTContent.Controls.Add(this.lblTTitle);
            this.panelTContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTContent.Location = new System.Drawing.Point(0, 0);
            this.panelTContent.Margin = new System.Windows.Forms.Padding(4);
            this.panelTContent.Name = "panelTContent";
            this.panelTContent.Padding = new System.Windows.Forms.Padding(13, 6, 7, 6);
            this.panelTContent.Size = new System.Drawing.Size(426, 110);
            this.panelTContent.TabIndex = 1;
            // 
            // lblTUnit
            // 
            this.lblTUnit.AutoSize = true;
            this.lblTUnit.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTUnit.ForeColor = System.Drawing.Color.Gray;
            this.lblTUnit.Location = new System.Drawing.Point(340, 52);
            this.lblTUnit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTUnit.Name = "lblTUnit";
            this.lblTUnit.Size = new System.Drawing.Size(24, 23);
            this.lblTUnit.TabIndex = 2;
            this.lblTUnit.Text = "%";
            // 
            // lblTValue
            // 
            this.lblTValue.AutoSize = true;
            this.lblTValue.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTValue.ForeColor = System.Drawing.Color.Black;
            this.lblTValue.Location = new System.Drawing.Point(17, 37);
            this.lblTValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTValue.Name = "lblTValue";
            this.lblTValue.Size = new System.Drawing.Size(60, 41);
            this.lblTValue.TabIndex = 1;
            this.lblTValue.Tag = "value";
            this.lblTValue.Text = "0.0";
            // 
            // lblTTitle
            // 
            this.lblTTitle.AutoSize = true;
            this.lblTTitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTTitle.ForeColor = System.Drawing.Color.Gray;
            this.lblTTitle.Location = new System.Drawing.Point(17, 6);
            this.lblTTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTTitle.Name = "lblTTitle";
            this.lblTTitle.Size = new System.Drawing.Size(156, 23);
            this.lblTTitle.TabIndex = 0;
            this.lblTTitle.Text = "Hedef Gerçekleşme";
            // 
            // tabControlStats
            // 
            this.tabControlStats.Controls.Add(this.tabPageCharts);
            this.tabControlStats.Controls.Add(this.tabPageBanks);
            this.tabControlStats.Controls.Add(this.tabPageDevices);
            this.tabControlStats.Controls.Add(this.tabPageTargets);
            this.tabControlStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlStats.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tabControlStats.Location = new System.Drawing.Point(0, 0);
            this.tabControlStats.Margin = new System.Windows.Forms.Padding(4);
            this.tabControlStats.Name = "tabControlStats";
            this.tabControlStats.SelectedIndex = 0;
            this.tabControlStats.Size = new System.Drawing.Size(676, 517);
            this.tabControlStats.TabIndex = 0;
            // 
            // tabPageCharts
            // 
            this.tabPageCharts.Controls.Add(this.chartMonthly);
            this.tabPageCharts.Controls.Add(this.chartBankDist);
            this.tabPageCharts.Location = new System.Drawing.Point(4, 29);
            this.tabPageCharts.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageCharts.Name = "tabPageCharts";
            this.tabPageCharts.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageCharts.Size = new System.Drawing.Size(668, 484);
            this.tabPageCharts.TabIndex = 0;
            this.tabPageCharts.Text = "Grafikler";
            this.tabPageCharts.UseVisualStyleBackColor = true;
            // 
            // chartMonthly
            // 
            chartArea1.Name = "ChartArea1";
            this.chartMonthly.ChartAreas.Add(chartArea1);
            this.chartMonthly.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartMonthly.Location = new System.Drawing.Point(4, 312);
            this.chartMonthly.Margin = new System.Windows.Forms.Padding(4);
            this.chartMonthly.Name = "chartMonthly";
            series1.ChartArea = "ChartArea1";
            series1.IsValueShownAsLabel = true;
            series1.Name = "Series1";
            this.chartMonthly.Series.Add(series1);
            this.chartMonthly.Size = new System.Drawing.Size(660, 168);
            this.chartMonthly.TabIndex = 1;
            this.chartMonthly.Text = "Aylık Karşılaştırma";
            title1.Name = "Title1";
            title1.Text = "Aylık Karşılaştırma";
            this.chartMonthly.Titles.Add(title1);
            // 
            // chartBankDist
            // 
            chartArea2.Name = "ChartArea1";
            this.chartBankDist.ChartAreas.Add(chartArea2);
            this.chartBankDist.Dock = System.Windows.Forms.DockStyle.Top;
            this.chartBankDist.Location = new System.Drawing.Point(4, 4);
            this.chartBankDist.Margin = new System.Windows.Forms.Padding(4);
            this.chartBankDist.Name = "chartBankDist";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series2.IsValueShownAsLabel = true;
            series2.Name = "Series1";
            this.chartBankDist.Series.Add(series2);
            this.chartBankDist.Size = new System.Drawing.Size(660, 308);
            this.chartBankDist.TabIndex = 0;
            this.chartBankDist.Text = "Banka Dağılımı";
            title2.Name = "Title1";
            title2.Text = "Banka Dağılımı";
            this.chartBankDist.Titles.Add(title2);
            // 
            // tabPageBanks
            // 
            this.tabPageBanks.Location = new System.Drawing.Point(4, 29);
            this.tabPageBanks.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageBanks.Name = "tabPageBanks";
            this.tabPageBanks.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageBanks.Size = new System.Drawing.Size(668, 484);
            this.tabPageBanks.TabIndex = 1;
            this.tabPageBanks.Text = "Bankalar";
            this.tabPageBanks.UseVisualStyleBackColor = true;
            // 
            // tabPageDevices
            // 
            this.tabPageDevices.Location = new System.Drawing.Point(4, 29);
            this.tabPageDevices.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageDevices.Name = "tabPageDevices";
            this.tabPageDevices.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageDevices.Size = new System.Drawing.Size(668, 484);
            this.tabPageDevices.TabIndex = 2;
            this.tabPageDevices.Text = "Cihazlar";
            this.tabPageDevices.UseVisualStyleBackColor = true;
            // 
            // tabPageTargets
            // 
            this.tabPageTargets.Location = new System.Drawing.Point(4, 29);
            this.tabPageTargets.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageTargets.Name = "tabPageTargets";
            this.tabPageTargets.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageTargets.Size = new System.Drawing.Size(668, 484);
            this.tabPageTargets.TabIndex = 3;
            this.tabPageTargets.Text = "Hedefler";
            this.tabPageTargets.UseVisualStyleBackColor = true;
            // 
            // StatisticsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(1200, 738);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.chkShowPopup);
            this.Controls.Add(this.panelHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StatisticsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Aylık POS İstatistikleri";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StatisticsForm_FormClosing);
            this.Load += new System.EventHandler(this.StatisticsForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.StatisticsForm_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.StatisticsForm_KeyDown);
            this.Resize += new System.EventHandler(this.StatisticsForm_Resize);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.panelTargetInput.ResumeLayout(false);
            this.panelTargetInput.PerformLayout();
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.flowLayoutPanelSummary.ResumeLayout(false);
            this.panelMonthlyTotal.ResumeLayout(false);
            this.panelMTContent.ResumeLayout(false);
            this.panelMTContent.PerformLayout();
            this.panelTodayIncoming.ResumeLayout(false);
            this.panelTIContent.ResumeLayout(false);
            this.panelTIContent.PerformLayout();
            this.panelBankBlock.ResumeLayout(false);
            this.panelBBContent.ResumeLayout(false);
            this.panelBBContent.PerformLayout();
            this.panelValor.ResumeLayout(false);
            this.panelVContent.ResumeLayout(false);
            this.panelVContent.PerformLayout();
            this.panelMostUsedBank.ResumeLayout(false);
            this.panelMUBContent.ResumeLayout(false);
            this.panelMUBContent.PerformLayout();
            this.panelMostActiveDevice.ResumeLayout(false);
            this.panelMADContent.ResumeLayout(false);
            this.panelMADContent.PerformLayout();
            this.panelTarget.ResumeLayout(false);
            this.panelTContent.ResumeLayout(false);
            this.panelTContent.PerformLayout();
            this.tabControlStats.ResumeLayout(false);
            this.tabPageCharts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartMonthly)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartBankDist)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        public System.Windows.Forms.Panel panelHeader;
        public System.Windows.Forms.Label lblTitle;
        public System.Windows.Forms.Button btnClose;
        public System.Windows.Forms.CheckBox chkShowPopup;
        public System.Windows.Forms.Panel panelMain;
        public System.Windows.Forms.SplitContainer splitContainerMain;
        public System.Windows.Forms.FlowLayoutPanel flowLayoutPanelSummary;
        public System.Windows.Forms.Panel panelMonthlyTotal;
        public System.Windows.Forms.Panel panelMTColorBar;
        public System.Windows.Forms.Panel panelMTContent;
        public System.Windows.Forms.Label lblMTTitle;
        public System.Windows.Forms.Label lblMTValue;
        public System.Windows.Forms.Label lblMTUnit;
        public System.Windows.Forms.Panel panelTodayIncoming;
        public System.Windows.Forms.Panel panelTIColorBar;
        public System.Windows.Forms.Panel panelTIContent;
        public System.Windows.Forms.Label lblTIUnit;
        public System.Windows.Forms.Label lblTIValue;
        public System.Windows.Forms.Label lblTITitle;
        public System.Windows.Forms.Panel panelBankBlock;
        public System.Windows.Forms.Panel panelBBColorBar;
        public System.Windows.Forms.Panel panelBBContent;
        public System.Windows.Forms.Label lblBBUnit;
        public System.Windows.Forms.Label lblBBValue;
        public System.Windows.Forms.Label lblBBTitle;
        public System.Windows.Forms.Panel panelValor;
        public System.Windows.Forms.Panel panelVColorBar;
        public System.Windows.Forms.Panel panelVContent;
        public System.Windows.Forms.Label lblVUnit;
        public System.Windows.Forms.Label lblVValue;
        public System.Windows.Forms.Label lblVTitle;
        public System.Windows.Forms.Panel panelMostUsedBank;
        public System.Windows.Forms.Panel panelMUBColorBar;
        public System.Windows.Forms.Panel panelMUBContent;
        public System.Windows.Forms.Label lblMUBValue;
        public System.Windows.Forms.Label lblMUBTitle;
        public System.Windows.Forms.Panel panelMostActiveDevice;
        public System.Windows.Forms.Panel panelMADColorBar;
        public System.Windows.Forms.Panel panelMADContent;
        public System.Windows.Forms.Label lblMADValue;
        public System.Windows.Forms.Label lblMADTitle;
        public System.Windows.Forms.Panel panelTarget;
        public System.Windows.Forms.Panel panelTColorBar;
        public System.Windows.Forms.Panel panelTContent;
        public System.Windows.Forms.Label lblTUnit;
        public System.Windows.Forms.Label lblTValue;
        public System.Windows.Forms.Label lblTTitle;
        public System.Windows.Forms.TabControl tabControlStats;
        public System.Windows.Forms.TabPage tabPageCharts;
        public System.Windows.Forms.TabPage tabPageBanks;
        public System.Windows.Forms.TabPage tabPageDevices;
        public System.Windows.Forms.TabPage tabPageTargets;
        public System.Windows.Forms.DataVisualization.Charting.Chart chartBankDist;
        public System.Windows.Forms.DataVisualization.Charting.Chart chartMonthly;
        public System.Windows.Forms.Button btnExportExcel;
        public System.Windows.Forms.Button btnExportPdf;
        public System.Windows.Forms.Button btnDetailedReport;
        public System.Windows.Forms.Panel panelTargetInput;
        public System.Windows.Forms.Button btnSaveTarget;
        public System.Windows.Forms.TextBox txtTarget;
        public System.Windows.Forms.Label lblTarget;
    }
}