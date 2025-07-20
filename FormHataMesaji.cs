using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using FontAwesome.Sharp;
using Guna.UI2.WinForms;

namespace Veresiye2025
{
    public partial class FormHataMesaji : Form
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

        public FormHataMesaji(string baslik, decimal cariLimit, decimal mevcutBorc, decimal eklenmekIstenen, decimal maxEklenebilir)
        {
            InitializeComponent();

            // Form köşelerini yuvarlatma
            this.FormBorderStyle = FormBorderStyle.None;
            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 20, 20));

            // Form taşıma için olay ekle
            pnlHeader.MouseDown += PnlHeader_MouseDown;
            lblBaslik.MouseDown += PnlHeader_MouseDown;

            // Form boyutu değiştiğinde köşeleri güncelle
            this.Resize += (s, e) => {
                this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 20, 20));
            };

            // Değerleri doldur
            lblBaslik.Text = baslik;
            lblCariLimit.Text = $"Cari Limit: {cariLimit:C2}";
            lblMevcutBorc.Text = $"Mevcut Borç: {mevcutBorc:C2}";
            lblEklenmekIstenen.Text = $"Eklenmek İstenen Borç: {eklenmekIstenen:C2}";
            lblMaxEklenebilir.Text = $"Eklenebilecek Maksimum Borç: {maxEklenebilir:C2}";

            // Öneri mesajını oluştur
            string oneriMesaji = maxEklenebilir <= 0
                ? "Limit dolmuştur. Ödeme yapılmadan yeni borç eklenemez."
                : $"En fazla {maxEklenebilir:C2} tutarında borç ekleyebilirsiniz.";

            lblOneri.Text = oneriMesaji;

            // Animasyon efekti
            timerAnimation.Start();
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

        public void btnTamam_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void timerAnimation_Tick(object sender, EventArgs e)
        {
            // Uyarı ikonunu animasyonlu göster
            iconPictureBox.Visible = !iconPictureBox.Visible;

            // 3 saniye sonra animasyonu durdur
            if (timerCounter >= 6)
            {
                timerAnimation.Stop();
                iconPictureBox.Visible = true;
            }

            timerCounter++;
        }

        public int timerCounter = 0;
    }
}