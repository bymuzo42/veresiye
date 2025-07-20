namespace Veresiye2025
{
    partial class Carihareketler
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
            // Sadece CariTema'yı çağır
            CariTema tema = new CariTema();
            tema.InitializeComponents(this);
        }

        #endregion

        // TÜM TANIMLAMALAR BURADA KALIYOR (Designer.cs'den taşındı)
        public System.Windows.Forms.Panel panelTop;
        public System.Windows.Forms.Panel menuContainer;
        public System.Windows.Forms.MenuStrip menuStrip1;
        public System.Windows.Forms.ToolStripMenuItem Firmalar;
        public System.Windows.Forms.ToolStripMenuItem firmaHesaplarıToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem yılSonuDevirİşlemleriToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem firmanıDüzenleToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem işlemlerToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem hareketlerF3ToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem hatırlatmaKurToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem yedekİşlemleriToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem yedekAlToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem araçlarToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem genelAyarlarToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem epostaAyarlarıToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem şifreDeğiştirToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem posTakipİşlemleriToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem posTakipToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem krediKartıTakipToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem yardımToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem hakkımızdaToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem güncellemeDenetleToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem çıkışToolStripMenuItem;
        public System.Windows.Forms.Panel windowControlsPanel;
        public Guna.UI2.WinForms.Guna2Button btnMinimize;
        public Guna.UI2.WinForms.Guna2Button btnClose;
        public System.Windows.Forms.Panel panelHeader;
        public System.Windows.Forms.Panel firmPanel;
        public Guna.UI2.WinForms.Guna2Button button3;
        public Guna.UI2.WinForms.Guna2Button oncekicari;
        public Guna.UI2.WinForms.Guna2Button sonrakicari;
        public System.Windows.Forms.Panel panelButtons;
        public System.Windows.Forms.Panel notekleContainer;
        public Guna.UI2.WinForms.Guna2Button notekle;
        public Guna.UI2.WinForms.Guna2Button analiz;
        public Guna.UI2.WinForms.Guna2Button Sorgulabtn;
        public Guna.UI2.WinForms.Guna2Button eposta;
        public Guna.UI2.WinForms.Guna2Button yazdir;
        public Guna.UI2.WinForms.Guna2Button Excelaktar;
        public System.Windows.Forms.Panel emanetContainer;
        public Guna.UI2.WinForms.Guna2Button emanet;
        public System.Windows.Forms.Panel panelContent;
        internal System.Windows.Forms.DataGridView dataGridView1;
        public System.Windows.Forms.DataGridViewTextBoxColumn id;
        public System.Windows.Forms.DataGridViewTextBoxColumn cari_kodu;
        public System.Windows.Forms.DataGridViewTextBoxColumn Tarih;
        public System.Windows.Forms.DataGridViewTextBoxColumn Tür;
        public System.Windows.Forms.DataGridViewTextBoxColumn Açıklama;
        public System.Windows.Forms.DataGridViewTextBoxColumn Borç;
        public System.Windows.Forms.DataGridViewTextBoxColumn Alacak;
        public System.Windows.Forms.DataGridViewTextBoxColumn Kalan;
        public System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        public System.Windows.Forms.ToolStripMenuItem borcekle;
        public System.Windows.Forms.ToolStripMenuItem tahsilatekle;
        public System.Windows.Forms.Panel panelFooter;
        public System.Windows.Forms.Panel bottomButtonsPanel;
        public Guna.UI2.WinForms.Guna2Button hrketekle;
        public Guna.UI2.WinForms.Guna2Button hrktdegistir;
        public Guna.UI2.WinForms.Guna2Button hrktsil;
        public Guna.UI2.WinForms.Guna2Button gecikmesorgula;
        public Guna.UI2.WinForms.Guna2Button hrktlergerigit;
        public Guna.UI2.WinForms.Guna2Button button13;
        public System.Windows.Forms.Panel summaryPanel;
        public Guna.UI2.WinForms.Guna2TextBox textBox3;
        public System.Windows.Forms.Label label3;
        public Guna.UI2.WinForms.Guna2TextBox textBox2;
        public System.Windows.Forms.Label label2;
        public Guna.UI2.WinForms.Guna2TextBox textBox1;
        public System.Windows.Forms.Label label1;
        public System.Drawing.Printing.PrintDocument printDocument;
        public System.Windows.Forms.PrintPreviewDialog printPreviewDialog;
        public System.Windows.Forms.PrintDialog printDialog;
        public System.Windows.Forms.DataVisualization.Charting.Chart chart1;
    }
}