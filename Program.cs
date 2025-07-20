using System;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Veresiye2025;

namespace Veresiye2025
{
    static class Program
    {
        internal static readonly string yedekKlasoru = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Yedekler");

        /// <summary>
        /// Uygulamanın ana giriş noktası.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // 📌 .NET 4.8 yüklü mü kontrol et
            if (!IsDotNetInstalled("4.8"))
            {
                DialogResult result = MessageBox.Show(
                    "Bu programı çalıştırmak için .NET Framework 4.8 gereklidir.\n\nŞimdi yüklemek ister misiniz?",
                    ".NET Eksik!",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {
                    Process.Start("https://dotnet.microsoft.com/en-us/download/dotnet-framework/net48");
                }
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                // 📌 Veritabanı bağlantısını kontrol et
                if (!CheckDatabaseConnection())
                {
                    MessageBox.Show("Veritabanı bağlantısı kurulamadı. Uygulama kapatılacak.",
                        "Kritik Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 📌 Açılışta otomatik yedek al
                AutoBackup();

                // 📌 Program kapanırken yedekleme yap
                Application.ApplicationExit += (sender, e) => AcilYedekAl();

                // 📌 Şifrelerin hashlenmesini sağlamak için bir defaya mahsus çalıştırılır
                PasswordHasher.HashAllPasswords();

                // 🚀 GEÇİCİ: FormFirstSetup devre dışı - direkt Form1 aç
                /*
                if (IsFirstTimeSetup())
                {
                    FormFirstSetup firstSetup = new FormFirstSetup();
                    if (firstSetup.ShowDialog() == DialogResult.OK)
                    {
                        Application.Run(new Form1());
                    }
                }
                else
                {
                */
                Application.Run(new Form1()); // Direkt Form1 aç
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Uygulama başlatılırken hata oluştu:\n{ex.Message}",
                    "Başlatma Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);

                LogError(ex);
            }
        }

        /// <summary>
        /// 🚀 İlk kurulum gerekip gerekmediğini kontrol eder - YENİ ÖZELLİK
        /// </summary>
        static bool IsFirstTimeSetup()
        {
            try
            {
                string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM Yonetim";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                    {
                        int userCount = Convert.ToInt32(cmd.ExecuteScalar());
                        return userCount == 0;
                    }
                }
            }
            catch (Exception)
            {
                return true;
            }
        }

        /// <summary>
        /// 🚀 Veritabanı bağlantısını kontrol eder - YENİ ÖZELLİK
        /// </summary>
        static bool CheckDatabaseConnection()
        {
            try
            {
                string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Bilgisayarda belirli bir .NET Framework sürümünün yüklü olup olmadığını kontrol eder.
        /// </summary>
        static bool IsDotNetInstalled(string version)
        {
            return Microsoft.Win32.Registry.LocalMachine
                .OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full")?
                .GetValue("Version")?.ToString().StartsWith(version) ?? false;
        }

        /// <summary>
        /// 📌 Program açılırken otomatik yedekleme yapar.
        /// </summary>
        public static void AutoBackup()
        {
            try
            {
                if (!Directory.Exists(yedekKlasoru))
                {
                    Directory.CreateDirectory(yedekKlasoru);
                }

                string yedekDosyaAdi = Path.Combine(yedekKlasoru, $"Yedek_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.bak");
                File.Copy("veresiye.db", yedekDosyaAdi, true);

                Console.WriteLine($"✅ Otomatik yedek alındı: {yedekDosyaAdi}");

#if DEBUG
                MessageBox.Show($"✅ **Açılışta Otomatik Yedek Alındı!**\n📂 Yedek Konumu: {yedekDosyaAdi}",
                    "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
#endif
            }
            catch (Exception ex)
            {
                LogError(ex, "Açılışta yedekleme hatası");

                if (ex is UnauthorizedAccessException)
                {
                    MessageBox.Show($"❌ Yedekleme için yetki gerekli: {ex.Message}",
                        "Yetki Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        /// <summary>
        /// 📌 Program kapanırken acil yedekleme yapar.
        /// </summary>
        public static void AcilYedekAl()
        {
            try
            {
                if (!Directory.Exists(yedekKlasoru))
                {
                    Directory.CreateDirectory(yedekKlasoru);
                }

                string yedekDosyaAdi = Path.Combine(yedekKlasoru, $"Acil_Yedek_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.bak");
                File.Copy("veresiye.db", yedekDosyaAdi, true);

                Console.WriteLine($"✅ Acil yedek alındı: {yedekDosyaAdi}");
            }
            catch (Exception ex)
            {
                LogError(ex, "Acil yedekleme hatası");
            }
        }

        /// <summary>
        /// 🚀 Hata loglaması için yardımcı metod - YENİ ÖZELLİK
        /// </summary>
        private static void LogError(Exception ex, string context = "Genel Hata")
        {
            try
            {
                if (!Directory.Exists(yedekKlasoru))
                {
                    Directory.CreateDirectory(yedekKlasoru);
                }

                string logDosyasi = Path.Combine(yedekKlasoru, "ErrorLog.txt");
                string logMesaji = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {context}: {ex.Message}\n" +
                                  $"Stack Trace: {ex.StackTrace}\n" +
                                  $"{"".PadLeft(50, '-')}\n";

                File.AppendAllText(logDosyasi, logMesaji);
            }
            catch
            {
                // Log yazılamıyorsa sessizce geç
            }
        }
    }
}