using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
namespace Veresiye2025
{
    public partial class HesapMakinesi : Form
    {
        // Hesaplama ile ilgili değişkenler
        public double hafiza = 0;
        public double oncekiSayi = 0;
        public double guncelSayi = 0;
        public string islem = "";
        public bool islemSonrasiTemizle = true;
        public bool esittirSonrasi = false;
        // Geçmiş ile ilgili değişkenler
        public List<string> hesaplamaGecmisi = new List<string>();
        public bool gecmisPaneliAcik = false;
        // Hafıza etiketi - dinamik olarak tanımlanacak
        public Guna.UI2.WinForms.Guna2HtmlLabel hafizaLabel;
        // Form boyutları
        public const int TAM_GENISLIK = 800;  // Geçmiş paneli açıkken genişlik
        public const int NORMAL_GENISLIK = 580; // Geçmiş paneli kapalıyken genişlik
        public const int FORM_YUKSEKLIK = 518; // Sabit yükseklik
        public const int GECMIS_PANEL_GENISLIK = 250; // Geçmiş panelinin genişliği

        public HesapMakinesi()
        {
            InitializeComponent();
            KeyPreview = true;
            this.KeyDown += HesapMakinesi_KeyDown;
            // Form boyutunu ayarla
            this.Size = new Size(NORMAL_GENISLIK, FORM_YUKSEKLIK);
            gecmisPanel.Visible = false;
            gecmisPaneliAcik = false;
            gecmisToggle.Checked = false;
            // Geçmiş panelinin konumunu ayarla
            gecmisPanel.Location = new Point(NORMAL_GENISLIK, 12);
            // Hafıza etiketi oluştur ve form'a ekle
            hafizaLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            hafizaLabel.BackColor = System.Drawing.Color.Transparent;
            hafizaLabel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            hafizaLabel.Location = new System.Drawing.Point(12, 110);
            hafizaLabel.Name = "hafizaLabel";
            hafizaLabel.Size = new System.Drawing.Size(100, 20);
            hafizaLabel.Text = "M: 0";
            hafizaLabel.Visible = false;
            this.Controls.Add(hafizaLabel);

            
        }

        public void HesapMakinesi_Load(object sender, EventArgs e)
        {
            // Form yüklendiğinde yapılacak işlemler
            this.Focus();
            this.ActiveControl = null;
            // Hafıza etiketini başlangıçta gizle
            hafizaLabel.Visible = false;
        }

        public void HesapMakinesi_KeyDown(object sender, KeyEventArgs e)
        {
            // Burası klavye olayları için gerekli işlemler içindir
            // Ancak işlemi ProcessCmdKey metodunda yapıyoruz
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Ctrl+C (Kopyala)
            if (keyData == (Keys.Control | Keys.C))
            {
                kopyalaButton.PerformClick();
                return true;
            }
            // Ctrl+V (Yapıştır)
            else if (keyData == (Keys.Control | Keys.V))
            {
                yapistirButton.PerformClick();
                return true;
            }
            // Sayısal tuşlar (0-9)
            if (keyData >= Keys.D0 && keyData <= Keys.D9)
            {
                int sayi = keyData - Keys.D0; // 0-9 arası sayıyı al
                SimulateButtonClick(sayi.ToString());
                return true; // Tuşu işlediğimizi belirtmek için true döndürüyoruz
            }
            // Numpad tuşları (0-9)
            else if (keyData >= Keys.NumPad0 && keyData <= Keys.NumPad9)
            {
                int sayi = keyData - Keys.NumPad0; // 0-9 arası sayıyı al
                SimulateButtonClick(sayi.ToString());
                return true;
            }
            // İşlem tuşları
            else if (keyData == Keys.Add) // +
            {
                SimulateButtonClick("+");
                return true;
            }
            else if (keyData == Keys.Subtract) // -
            {
                SimulateButtonClick("-");
                return true;
            }
            else if (keyData == Keys.Multiply) // *
            {
                SimulateButtonClick("×");
                return true;
            }
            else if (keyData == Keys.Divide) // /
            {
                SimulateButtonClick("÷");
                return true;
            }
            else if (keyData == Keys.Return || keyData == Keys.Enter) // Enter - == operatörü düzeltildi
            {
                SimulateButtonClick("=");
                return true;
            }
            else if (keyData == Keys.Back) // Backspace
            {
                SimulateButtonClick("⌫");
                return true;
            }
            else if (keyData == Keys.Decimal || keyData == Keys.OemPeriod) // Nokta - == operatörü düzeltildi
            {
                SimulateButtonClick(".");
                return true;
            }
            else if (keyData == Keys.Escape) // ESC
            {
                SimulateButtonClick("C");
                return true;
            }
            // F12 tuşu - formu kapatır
            else if (keyData == Keys.F12)
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        // Tuş simülasyonu yapan metot - tüm formdaki butonları tarar
        public void SimulateButtonClick(string tusDegeri)
        {
            // Önce buttonPanel içerisindeki butonları tarayalım
            foreach (Control control in this.Controls)
            {
                // Panel'i bulduk
                if (control is Guna2Panel panel)
                {
                    foreach (Control panelControl in panel.Controls)
                    {
                        if (panelControl is Guna2Button btn && btn.Text == tusDegeri)
                        {
                            btn.PerformClick();
                            return;
                        }
                    }
                }
                // Tüm form yüzeyindeki butonları da tarayalım
                else if (control is Guna2Button btn && btn.Text == tusDegeri)
                {
                    btn.PerformClick();
                    return;
                }
            }
        }

        // Sayı dönüşümü için geliştirilen yeni metot
        public double ParseSayi(string metin)
        {
            // Hem nokta hem de virgül olarak yazılmış ondalık sayıları kabul et
            string normalizedText = metin.Replace(',', '.');
            if (double.TryParse(normalizedText, System.Globalization.NumberStyles.Any,
                                System.Globalization.CultureInfo.InvariantCulture, out double result))
            {
                return result;
            }
            throw new FormatException("Geçersiz sayı formatı");
        }

        public void Tus_Click(object sender, EventArgs e)
        {
            Guna2Button tus = sender as Guna2Button;
            string tusMetni = tus.Text;
            try
            {
                // Sayı tuşları
                if (char.IsDigit(tusMetni[0]) || tusMetni == "." || tusMetni == "00") // == operatörü düzeltildi
                {
                    if (islemSonrasiTemizle)
                    {
                        sonucEkrani.Text = tusMetni == "." ? "0." : tusMetni;
                        islemSonrasiTemizle = false;
                    }
                    else
                    {
                        if (sonucEkrani.Text == "0" && tusMetni != ".")
                        {
                            sonucEkrani.Text = tusMetni;
                        }
                        else if (tusMetni == "." && sonucEkrani.Text.Contains("."))
                        {
                            // Zaten nokta var, bir şey yapma
                        }
                        else if (tusMetni == "00" && sonucEkrani.Text == "0") // == operatörü düzeltildi
                        {
                            // Zaten 0 var, bir şey yapma
                        }
                        else
                        {
                            sonucEkrani.Text += tusMetni;
                        }
                    }
                    if (esittirSonrasi)
                    {
                        islemGecmisiLabel.Text = "";
                        esittirSonrasi = false;
                    }
                }
                // Temel işlemler
                else if (tusMetni == "+" || tusMetni == "-" || tusMetni == "×" || tusMetni == "÷") // == operatörü düzeltildi
                {
                    if (!string.IsNullOrEmpty(islem) && !islemSonrasiTemizle)
                    {
                        // Önceki işlemi tamamla
                        Hesapla();
                    }
                    oncekiSayi = ParseSayi(sonucEkrani.Text); // ParseSayi metodu kullanılıyor
                    islem = tusMetni;
                    islemGecmisiLabel.Text = FormatSayi(oncekiSayi) + " " + islem;
                    islemSonrasiTemizle = true;
                    esittirSonrasi = false;
                }
                // Eşittir işlemi
                else if (tusMetni == "=")
                {
                    Hesapla();
                    islem = "";
                    islemSonrasiTemizle = true;
                    esittirSonrasi = true;
                }
                // Temizleme işlemleri
                else if (tusMetni == "C")
                {
                    sonucEkrani.Text = "0";
                    islemGecmisiLabel.Text = "";
                    oncekiSayi = 0;
                    islem = "";
                    islemSonrasiTemizle = true;
                }
                else if (tusMetni == "CE")
                {
                    sonucEkrani.Text = "0";
                    islemSonrasiTemizle = true;
                }
                else if (tusMetni == "⌫")
                {
                    if (sonucEkrani.Text.Length > 1)
                    {
                        sonucEkrani.Text = sonucEkrani.Text.Substring(0, sonucEkrani.Text.Length - 1);
                    }
                    else
                    {
                        sonucEkrani.Text = "0";
                        islemSonrasiTemizle = true;
                    }
                }
                // İşaret değiştir
                else if (tusMetni == "±")
                {
                    if (sonucEkrani.Text != "0")
                    {
                        if (sonucEkrani.Text.StartsWith("-"))
                            sonucEkrani.Text = sonucEkrani.Text.Substring(1);
                        else
                            sonucEkrani.Text = "-" + sonucEkrani.Text;
                    }
                }
                // Yüzde işlemi
                else if (tusMetni == "%")
                {
                    if (!string.IsNullOrEmpty(islem))
                    {
                        double sayi = ParseSayi(sonucEkrani.Text);
                        double yuzde = oncekiSayi * sayi / 100;
                        sonucEkrani.Text = FormatSayi(yuzde);
                    }
                    else
                    {
                        double sayi = ParseSayi(sonucEkrani.Text);
                        double yuzde = sayi / 100;
                        sonucEkrani.Text = FormatSayi(yuzde);
                    }
                    islemSonrasiTemizle = true;
                }
                // Karekök
                else if (tusMetni == "√")
                {
                    double sayi = ParseSayi(sonucEkrani.Text);
                    if (sayi < 0)
                    {
                        sonucEkrani.Text = "Hata";
                    }
                    else
                    {
                        double sonuc = Math.Sqrt(sayi);
                        sonucEkrani.Text = FormatSayi(sonuc);
                    }
                    islemSonrasiTemizle = true;
                }
                // Kare alma
                else if (tusMetni == "x²")
                {
                    double sayi = ParseSayi(sonucEkrani.Text);
                    double sonuc = sayi * sayi;
                    sonucEkrani.Text = FormatSayi(sonuc);
                    islemSonrasiTemizle = true;
                }
                // Tersini alma
                else if (tusMetni == "1/x")
                {
                    double sayi = ParseSayi(sonucEkrani.Text);
                    if (sayi == 0)
                    {
                        sonucEkrani.Text = "Sıfıra bölünemez";
                    }
                    else
                    {
                        double sonuc = 1 / sayi;
                        sonucEkrani.Text = FormatSayi(sonuc);
                    }
                    islemSonrasiTemizle = true;
                }
                // Hafıza işlemleri
                else if (tusMetni == "MC")
                {
                    hafiza = 0;
                    hafizaLabel.Visible = false;
                }
                else if (tusMetni == "MR")
                {
                    sonucEkrani.Text = FormatSayi(hafiza);
                    islemSonrasiTemizle = true;
                }
                else if (tusMetni == "M+")
                {
                    hafiza += ParseSayi(sonucEkrani.Text);
                    hafizaLabel.Text = "M: " + FormatSayi(hafiza);
                    hafizaLabel.Visible = true;
                    islemSonrasiTemizle = true;
                }
                else if (tusMetni == "M-")
                {
                    hafiza -= ParseSayi(sonucEkrani.Text);
                    hafizaLabel.Text = "M: " + FormatSayi(hafiza);
                    hafizaLabel.Visible = true;
                    islemSonrasiTemizle = true;
                }
                // KDV İşlemleri - Güncel oranlarla
                else if (tusMetni == "KDV1")
                {
                    double sayi = ParseSayi(sonucEkrani.Text);
                    double kdvTutari = sayi * 0.01;
                    double sonuc = sayi + kdvTutari;
                    islemGecmisiLabel.Text = FormatSayi(sayi) + " + %1 KDV = " + FormatSayi(sonuc);
                    sonucEkrani.Text = FormatSayi(sonuc);
                    islemSonrasiTemizle = true;
                }
                else if (tusMetni == "KDV10")
                {
                    double sayi = ParseSayi(sonucEkrani.Text);
                    double kdvTutari = sayi * 0.10;
                    double sonuc = sayi + kdvTutari;
                    islemGecmisiLabel.Text = FormatSayi(sayi) + " + %10 KDV = " + FormatSayi(sonuc);
                    sonucEkrani.Text = FormatSayi(sonuc);
                    islemSonrasiTemizle = true;
                }
                else if (tusMetni == "KDV20")
                {
                    double sayi = ParseSayi(sonucEkrani.Text);
                    double kdvTutari = sayi * 0.20;
                    double sonuc = sayi + kdvTutari;
                    islemGecmisiLabel.Text = FormatSayi(sayi) + " + %20 KDV = " + FormatSayi(sonuc);
                    sonucEkrani.Text = FormatSayi(sonuc);
                    islemSonrasiTemizle = true;
                }
                // KDV Çıkarma İşlemleri
                else if (tusMetni == "KDV-1")
                {
                    double sayi = ParseSayi(sonucEkrani.Text);
                    double kdvsiz = sayi / 1.01;
                    islemGecmisiLabel.Text = FormatSayi(sayi) + " (%1 KDV'li) - KDV = " + FormatSayi(kdvsiz);
                    sonucEkrani.Text = FormatSayi(kdvsiz);
                    islemSonrasiTemizle = true;
                }
                else if (tusMetni == "KDV-10")
                {
                    double sayi = ParseSayi(sonucEkrani.Text);
                    double kdvsiz = sayi / 1.10;
                    islemGecmisiLabel.Text = FormatSayi(sayi) + " (%10 KDV'li) - KDV = " + FormatSayi(kdvsiz);
                    sonucEkrani.Text = FormatSayi(kdvsiz);
                    islemSonrasiTemizle = true;
                }
                else if (tusMetni == "KDV-20")
                {
                    double sayi = ParseSayi(sonucEkrani.Text);
                    double kdvsiz = sayi / 1.20;
                    islemGecmisiLabel.Text = FormatSayi(sayi) + " (%20 KDV'li) - KDV = " + FormatSayi(kdvsiz);
                    sonucEkrani.Text = FormatSayi(kdvsiz);
                    islemSonrasiTemizle = true;
                }
                // İskonto İşlemleri
                else if (tusMetni == "İSK+")
                {
                    double sayi = ParseSayi(sonucEkrani.Text);
                    OzelYuzdeForm form = new OzelYuzdeForm("İskonto Ekle (%)", 10);
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        double iskontoOrani = form.Yuzde;
                        double iskontoTutari = sayi * iskontoOrani / 100;
                        double sonuc = sayi + iskontoTutari;
                        islemGecmisiLabel.Text = FormatSayi(sayi) + " + %" + iskontoOrani + " = " + FormatSayi(sonuc);
                        sonucEkrani.Text = FormatSayi(sonuc);
                    }
                    islemSonrasiTemizle = true;
                }
                else if (tusMetni == "İSK-")
                {
                    double sayi = ParseSayi(sonucEkrani.Text);
                    OzelYuzdeForm form = new OzelYuzdeForm("İskonto Düş (%)", 10);
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        double iskontoOrani = form.Yuzde;
                        double iskontoTutari = sayi * iskontoOrani / 100;
                        double sonuc = sayi - iskontoTutari;
                        islemGecmisiLabel.Text = FormatSayi(sayi) + " - %" + iskontoOrani + " = " + FormatSayi(sonuc);
                        sonucEkrani.Text = FormatSayi(sonuc);
                    }
                    islemSonrasiTemizle = true;
                }
            }
            catch (Exception ex)
            {
                sonucEkrani.Text = "Hata";
                islemGecmisiLabel.Text = ex.Message;
            }
        }

        public void Hesapla()
        {
            if (string.IsNullOrEmpty(islem))
                return;

            guncelSayi = ParseSayi(sonucEkrani.Text); // ParseSayi metodunu kullanıyoruz
            double sonuc = 0;

            switch (islem)
            {
                case "+":
                    sonuc = oncekiSayi + guncelSayi;
                    break;
                case "-":
                    sonuc = oncekiSayi - guncelSayi;
                    break;
                case "×":
                    sonuc = oncekiSayi * guncelSayi;
                    break;
                case "÷":
                    if (guncelSayi == 0)
                    {
                        sonucEkrani.Text = "Sıfıra bölünemez";
                        islemGecmisiLabel.Text = "";
                        return;
                    }
                    sonuc = oncekiSayi / guncelSayi;
                    break;
            }

            string islemMetni = FormatSayi(oncekiSayi) + " " + islem + " " + FormatSayi(guncelSayi) + " = " + FormatSayi(sonuc);
            islemGecmisiLabel.Text = islemMetni;
            sonucEkrani.Text = FormatSayi(sonuc);

            // Hesaplama geçmişine ekle
            hesaplamaGecmisi.Add(islemMetni);
            gecmisListesi.Items.Add(islemMetni);

            // Son eklenen öğeyi görünür yap
            if (gecmisListesi.Items.Count > 0)
            {
                int lastIndex = gecmisListesi.Items.Count - 1;
                gecmisListesi.EnsureVisible(lastIndex);
            }

            oncekiSayi = sonuc;
        }

        public string FormatSayi(double sayi)
        {
            // Bölgesel ayarları kullanarak sayıyı formatla
            System.Globalization.CultureInfo culture = System.Globalization.CultureInfo.CurrentCulture;

            // Büyük sayılar için bilimsel gösterimi önle
            if (Math.Abs(sayi) >= 1e10 || (Math.Abs(sayi) < 0.0001 && sayi != 0))
            {
                return sayi.ToString("0.###e+0", culture);
            }

            // Ondalık sıfırları kaldır ve bölgesel ondalık ayracını kullan
            string sonuc = sayi.ToString("N10", culture).TrimEnd('0');

            // Bölgesel ayarlardaki ondalık ayracını kontrol et ve son ayracı kaldır
            char decimalSeparator = culture.NumberFormat.NumberDecimalSeparator[0];
            if (sonuc.EndsWith(decimalSeparator.ToString()))
                sonuc = sonuc.Substring(0, sonuc.Length - 1);

            return sonuc;
        }

        public void kapatButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Geçmiş paneli olayları
        public void gecmisToggle_CheckedChanged(object sender, EventArgs e)
        {
            gecmisPaneliAcik = gecmisToggle.Checked;
            gecmisPanel.Visible = gecmisPaneliAcik;

            // Formun genişliğini ayarla
            if (gecmisPaneliAcik)
            {
                this.Width = NORMAL_GENISLIK + GECMIS_PANEL_GENISLIK;
            }
            else
            {
                this.Width = NORMAL_GENISLIK;
            }
        }

        // Geçmişi temizle butonu olayı
        public void gecmisiTemizleButton_Click(object sender, EventArgs e)
        {
            hesaplamaGecmisi.Clear();
            gecmisListesi.Items.Clear();
        }

        // Geçmiş listesindeki öğeye çift tıklama olayı
        public void gecmisListesi_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (gecmisListesi.SelectedItems.Count > 0)
            {
                string seciliIslem = gecmisListesi.SelectedItems[0].Text;
                // Eşittir işaretinden sonraki sonucu al
                int esittirIndex = seciliIslem.LastIndexOf('=');
                if (esittirIndex != -1 && esittirIndex < seciliIslem.Length - 1)
                {
                    string sonuc = seciliIslem.Substring(esittirIndex + 1).Trim();
                    sonucEkrani.Text = sonuc;
                    islemSonrasiTemizle = true;
                }
            }
        }

        // Kopyala butonu olayı
        public void kopyalaButton_Click(object sender, EventArgs e)
        {
            // Sonuç ekranındaki değeri panoya kopyala
            if (!string.IsNullOrEmpty(sonucEkrani.Text))
            {
                Clipboard.SetText(sonucEkrani.Text);
                MessageBox.Show("Değer panoya kopyalandı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Yapıştır butonu olayı
        public void yapistirButton_Click(object sender, EventArgs e)
        {
            // Panodan yapıştır ve hesap makinesinde kullan
            if (Clipboard.ContainsText())
            {
                string pastedText = Clipboard.GetText();
                try
                {
                    // ParseSayi metodunu kullanarak dönüşüm yapın
                    double result = ParseSayi(pastedText);
                    sonucEkrani.Text = FormatSayi(result);
                    islemSonrasiTemizle = true;
                }
                catch
                {
                    MessageBox.Show("Yapıştırılan metin sayısal bir değer değil.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }

    // Özel Yüzde Giriş Formu
    public class OzelYuzdeForm : Form
    {
        public Guna2TextBox yuzdeTextBox;
        public double _yuzde = 10;
        public double Yuzde => _yuzde;

        public OzelYuzdeForm(string baslik, double varsayilanYuzde)
        {
            this.Text = baslik;
            this.Size = new Size(300, 200);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(240, 240, 240);
            _yuzde = varsayilanYuzde;
            InitializeUI();
        }

        public void InitializeUI()
        {
            Guna2HtmlLabel label = new Guna2HtmlLabel
            {
                Text = "Yüzde oran giriniz:",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(20, 20),
                Size = new Size(200, 20)
            };
            this.Controls.Add(label);

            yuzdeTextBox = new Guna2TextBox
            {
                Text = _yuzde.ToString(),
                Font = new Font("Segoe UI", 12, FontStyle.Regular),
                Location = new Point(20, 45),
                Size = new Size(250, 40),
                BorderRadius = 8,
                TextAlign = HorizontalAlignment.Right
            };
            this.Controls.Add(yuzdeTextBox);

            Guna2Button tamam = new Guna2Button
            {
                Text = "Tamam",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(140, 95),
                Size = new Size(130, 35),
                BorderRadius = 8,
                FillColor = Color.FromArgb(3, 125, 221)
            };
            tamam.Click += (s, e) =>
            {
                if (double.TryParse(yuzdeTextBox.Text.Replace(',', '.'), out double yuzde))
                {
                    _yuzde = yuzde;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Lütfen geçerli bir sayı giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };
            this.Controls.Add(tamam);

            Guna2Button iptal = new Guna2Button
            {
                Text = "İptal",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(20, 95),
                Size = new Size(110, 35),
                BorderRadius = 8,
                FillColor = Color.Gray
            };
            iptal.Click += (s, e) =>
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            };
            this.Controls.Add(iptal);

            this.AcceptButton = tamam;
            this.CancelButton = iptal;
        }
    }
}