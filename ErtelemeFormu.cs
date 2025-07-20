using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace Veresiye2025
{
    public partial class ErtelemeFormu : Form
    {
        public DateTime SecilenTarih { get;  set; } // Seçilen tarih bu alanda tutulur

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

        public ErtelemeFormu(DateTime mevcutTarih)
        {
            InitializeComponent();

            // Form köşelerini yuvarlatma
            this.FormBorderStyle = FormBorderStyle.None;
            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 20, 20));

            // Varsayılan olarak mevcut alarm tarihini göster
            dateTimePicker.Value = mevcutTarih;

            // Form taşıma için olay ekle
            pnlHeader.MouseDown += PnlHeader_MouseDown;
            lblTitle.MouseDown += PnlHeader_MouseDown;

            // Form boyutu değiştiğinde köşeleri güncelle
            this.Resize += (s, e) => {
                this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 20, 20));
            };

            // Arka plan rengini ve diğer görsel özellikleri ayarla
            this.BackColor = Color.White;
            pnlHeader.BackColor = Color.FromArgb(28, 141, 243);
            lblTitle.ForeColor = Color.White;
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

        public void btnKaydet_Click(object sender, EventArgs e)
        {
            // Kullanıcının seçtiği tarihi alın
            SecilenTarih = dateTimePicker.Value;

            // DialogResult.OK ile geri dön, bu sayede işlemin başarılı olduğunu anlarız
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public void btnIptal_Click(object sender, EventArgs e)
        {
            // İptal ile kapat
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public void btnClose_Click(object sender, EventArgs e)
        {
            // Formu kapat
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}