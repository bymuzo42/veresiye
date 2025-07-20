using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Guna.UI2.WinForms;

namespace Veresiye2025
{
    public class InputBoxForm : Form
    {
        // Form taşıma için gerekli DLL import
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        public extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        public extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        // Yuvarlatılmış köşeler için API
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        public static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );

        public Panel pnlHeader;
        public Label lblTitle;
        public Guna2ControlBox btnClose;
        public Guna2TextBox inputTextBox;
        public Guna2Button okButton;
        public Guna2Button cancelButton;
        public Label lblPrompt;

        public string UserInput { get; set; }

        public InputBoxForm(string title, string prompt)
        {
            // Form temel özellikleri
            this.Text = title;
            this.Size = new Size(400, 200);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

            // Header panel oluşturma
            pnlHeader = new Panel()
            {
                Dock = DockStyle.Top,
                Height = 40,
                BackColor = Color.DodgerBlue
            };

            // Form başlığı
            lblTitle = new Label()
            {
                Text = title,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(12, 9)
            };

            // Kapatma butonu
            btnClose = new Guna2ControlBox()
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                FillColor = Color.Transparent,
                IconColor = Color.White,
                HoverState = { FillColor = Color.Red },
                Size = new Size(40, 40),
                Location = new Point(Width - 40, 0)
            };

            // Form içeriği
            lblPrompt = new Label()
            {
                Text = prompt,
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                Location = new Point(20, 60),
                AutoSize = true
            };

            // Giriş kutusu
            inputTextBox = new Guna2TextBox()
            {
                BorderRadius = 5,
                Font = new Font("Segoe UI", 10),
                Location = new Point(20, 90),
                Size = new Size(Width - 40, 36),
                PlaceholderText = "Buraya giriniz..."
            };

            // Butonlar
            okButton = new Guna2Button()
            {
                Text = "Tamam",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                FillColor = Color.DodgerBlue,
                ForeColor = Color.White,
                BorderRadius = 5,
                Size = new Size(100, 36),
                Location = new Point(Width - 240, 140)
            };

            cancelButton = new Guna2Button()
            {
                Text = "İptal",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                FillColor = Color.DodgerBlue,
                ForeColor = Color.White,
                BorderRadius = 5,
                Size = new Size(100, 36),
                Location = new Point(Width - 120, 140)
            };

            // Panel için olay tanımla (form taşıma için)
            pnlHeader.MouseDown += (sender, e) => {
                ReleaseCapture();
                SendMessage(this.Handle, 0x112, 0xf012, 0);
            };

            lblTitle.MouseDown += (sender, e) => {
                ReleaseCapture();
                SendMessage(this.Handle, 0x112, 0xf012, 0);
            };

            // Buton olayları
            okButton.Click += (sender, e) => {
                this.UserInput = inputTextBox.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            };

            cancelButton.Click += (sender, e) => {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            };

            // ESC ile kapatma
            this.KeyPreview = true;
            this.KeyDown += (s, e) => {
                if (e.KeyCode == Keys.Escape)
                {
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                }
            };

            // Kontrolleri forma ekleme
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Controls.Add(btnClose);

            this.Controls.Add(pnlHeader);
            this.Controls.Add(lblPrompt);
            this.Controls.Add(inputTextBox);
            this.Controls.Add(okButton);
            this.Controls.Add(cancelButton);

            // Form yüklendiğinde input kutusu odaklanır
            this.Load += (s, e) => inputTextBox.Focus();
        }
    }
}