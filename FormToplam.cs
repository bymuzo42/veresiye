using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace Veresiye2025
{
    public partial class FormToplam : Form
    {
        // Form köşelerini yuvarlatmak için
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        public static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );

        // Form taşıma için
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        public FormToplam(decimal aktifBorclu, decimal aktifAlacakli, decimal pasifBorclu, decimal pasifAlacakli, decimal vadesiGecenToplam)
        {
            InitializeComponent();

            // Form köşelerini yuvarlatma
            this.FormBorderStyle = FormBorderStyle.None;
            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 20, 20));

            // Form taşıma için olay ekle
            pnlHeader.MouseDown += PnlHeader_MouseDown;
            lblTitle.MouseDown += PnlHeader_MouseDown;

            // Form boyutu değiştiğinde köşeleri güncelle
            this.Resize += (s, e) => {
                this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 20, 20));
            };

            this.KeyDown += FormToplam_KeyDown; // KeyDown olayını bağlıyoruz

            // Tüm değerleri göster
            SetupValues(aktifBorclu, aktifAlacakli, pasifBorclu, pasifAlacakli, vadesiGecenToplam);
        }

        // Form başlığı üzerinde mouse down olduğunda formu taşı
        public void PnlHeader_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        public void SetupValues(decimal aktifBorclu, decimal aktifAlacakli, decimal pasifBorclu, decimal pasifAlacakli, decimal vadesiGecenToplam)
        {
            // Değerleri formata uygun şekilde göster
            lblAktifBorclu.Text = aktifBorclu.ToString("C2");
            lblAktifAlacakli.Text = aktifAlacakli.ToString("C2");
            lblPasifBorclu.Text = pasifBorclu.ToString("C2");
            lblPasifAlacakli.Text = pasifAlacakli.ToString("C2");

            // Genel toplamları hesapla
            decimal genelBorclu = aktifBorclu + pasifBorclu;
            decimal genelAlacakli = aktifAlacakli + pasifAlacakli;
            lblGenelBorclu.Text = genelBorclu.ToString("C2");
            lblGenelAlacakli.Text = genelAlacakli.ToString("C2");

            // Vadesi geçen borç toplamı
            lblVadesiGecenToplam.Text = vadesiGecenToplam > 0 ? vadesiGecenToplam.ToString("C2") : "₺0,00";

            // Panel ve grup açıklamaları güncelle
            SetupPanelColors(aktifBorclu, aktifAlacakli, genelBorclu, genelAlacakli, vadesiGecenToplam);
        }

        // Panel ve grup renklerini duruma göre ayarla
        public void SetupPanelColors(decimal aktifBorclu, decimal aktifAlacakli, decimal genelBorclu, decimal genelAlacakli, decimal vadesiGecenToplam)
        {
            // Aktif panel arkaplan rengi
            if (aktifBorclu > aktifAlacakli)
                groupAktif.ForeColor = Color.Green;
            else if (aktifBorclu < aktifAlacakli)
                groupAktif.ForeColor = Color.Red;

            // Genel durum paneli arkaplan rengi
            if (genelBorclu > genelAlacakli)
            {
                groupGenel.ForeColor = Color.Green;
                lblGeneralStatus.Text = "Genel Durum: Borçlu";
                lblGeneralStatus.ForeColor = Color.Green;
            }
            else if (genelBorclu < genelAlacakli)
            {
                groupGenel.ForeColor = Color.Red;
                lblGeneralStatus.Text = "Genel Durum: Alacaklı";
                lblGeneralStatus.ForeColor = Color.Red;
            }
            else
            {
                lblGeneralStatus.Text = "Genel Durum: Dengede";
                lblGeneralStatus.ForeColor = Color.Blue;
            }

            // Vadesi geçen tutar uyarısı
            if (vadesiGecenToplam > 0)
            {
                lblVadesiGecenUyari.Visible = true;
                lblVadesiGecenToplam.ForeColor = Color.DarkRed;
                lblVadesiGecenToplamLabel.ForeColor = Color.DarkRed;
            }
            else
            {
                lblVadesiGecenUyari.Visible = false;
            }
        }

        public void FormToplam_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();  // ESC tuşuna basıldığında formu kapat
            }
        }

        public void btnHeaderClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void btnExport_Click(object sender, EventArgs e)
        {
            // Dışa aktarma işlemleri burada olabilir
            MessageBox.Show("Veriler dışarı aktarılıyor...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}