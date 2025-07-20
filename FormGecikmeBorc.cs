using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace Veresiye2025
{
    public partial class FormGecikmeBorc : Form
    {
        public string cariUnvan; // Cari unvan bilgisini saklayacağız

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

        public FormGecikmeBorc(string bakiye, string gecikmisBorc, string vadeGun, string faturaTarihi, string cariAdi)
        {
            InitializeComponent();

            // Cari unvan değişkenini alıyoruz
            this.cariUnvan = cariAdi;

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

            // Başlık ve içeriği ayarla
            lblTitle.Text = "GECİKMİŞ BORÇ BİLGİSİ";
            lblCariUnvan.Text = cariAdi;
            lblBakiye.Text = bakiye;
            lblGecikmisBorc.Text = gecikmisBorc;
            lblVade.Text = vadeGun + " Gün";
            lblFaturaTarihi.Text = faturaTarihi;

            // Yanıp sönen uyarı için zamanlayıcı başlat
            timerBlink.Start();
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

        // Tahsilat Yap butonuna tıklanınca
        public void btnTahsilat_Click(object sender, EventArgs e)
        {
            // Formu gizle, kapanmasın
            this.Hide();

            // Tahsilat Formunu aç
            Tahsilat tahsilatFormu = new Tahsilat(cariUnvan);
            tahsilatFormu.ShowDialog(); // Kullanıcı tahsilat yapana kadar bekler.

            // Tahsilat tamamlandıktan sonra bu formu tamamen kapat
            this.Close();

            // Carihareketler formunu bul ve DataGridView'i yenileyip son satıra odaklan
            if (Application.OpenForms["Carihareketler"] is Carihareketler cariForm)
            {
                cariForm.RefreshDataGridAndFocusLastRow();
            }
        }

        // Tamam butonuna tıklanınca
        public void btnTamam_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Kapat butonuna tıklanınca
        public void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Yanıp sönen uyarı efekti için
        public void timerBlink_Tick(object sender, EventArgs e)
        {
            lblGecikmisBorc.ForeColor = lblGecikmisBorc.ForeColor == Color.Red ?
                Color.DarkRed : Color.Red;

            warningIconPictureBox.Visible = !warningIconPictureBox.Visible;
        }

        // Yardım butonu tıklandı
        public void btnInfo_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Gecikmiş borçlar, vade tarihi geçmiş ve ödenmemiş faturalarınızı gösterir.\n\n" +
                "Tahsilat yaparak borcunuzu azaltabilirsiniz.\n\n" +
                "Vade gün sayısı, faturanın vade tarihinden bu yana geçen gün sayısını gösterir.",
                "Gecikmiş Borç Bilgisi",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
    }
}