// CleanUnifiedForm4.cs - Resources hatası olmayan versiyon
using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace Veresiye2025
{
    public partial class CleanUnifiedForm4 : Form
    {
        private string _firmaAdi;

        public CleanUnifiedForm4(string firmaAdi)
        {
            _firmaAdi = firmaAdi;
            InitializeCleanComponents();
        }

        private void InitializeCleanComponents()
        {
            // Temel form ayarları
            this.Text = $"Ana Ekran - {_firmaAdi}";
            this.Size = new Size(1200, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(245, 246, 250);
            this.Font = new Font("Segoe UI", 9F);
            this.MinimumSize = new Size(800, 600);

            // Ana panel
            Panel mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(20)
            };

            // Başlık
            Label lblTitle = new Label
            {
                Text = "Unified Form Test - Başarılı!",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(67, 162, 243),
                Location = new Point(50, 50),
                Size = new Size(500, 40)
            };

            // Alt başlık
            Label lblSubtitle = new Label
            {
                Text = $"Firma: {_firmaAdi}",
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.Gray,
                Location = new Point(50, 100),
                Size = new Size(400, 30)
            };

            // Test butonu
            Guna2Button testButton = new Guna2Button
            {
                Text = "✅ Form Başarıyla Açıldı!",
                Size = new Size(200, 50),
                Location = new Point(50, 150),
                FillColor = Color.FromArgb(67, 162, 243),
                ForeColor = Color.White,
                BorderRadius = 10,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            testButton.Click += (s, e) => {
                MessageBox.Show("UnifiedForm4 başarıyla çalışıyor!\n\nResources hatası çözüldü.",
                    "Başarılı Test", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };

            // Kapat butonu
            Guna2Button closeButton = new Guna2Button
            {
                Text = "Kapat",
                Size = new Size(100, 40),
                Location = new Point(270, 150),
                FillColor = Color.Gray,
                ForeColor = Color.White,
                BorderRadius = 10,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };

            closeButton.Click += (s, e) => this.Close();

            // Kontrolleri ekle
            mainPanel.Controls.Add(lblTitle);
            mainPanel.Controls.Add(lblSubtitle);
            mainPanel.Controls.Add(testButton);
            mainPanel.Controls.Add(closeButton);

            this.Controls.Add(mainPanel);
        }

        // Form closing event
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
        }
    }
}