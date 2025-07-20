using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Globalization;
using System.Linq;

namespace Veresiye2025
{
    public class StatisticsManager
    {
        public readonly string _connectionString = "Data Source=veresiye.db;Version=3;";

        public StatisticsData CalculateStatistics()
        {
            StatisticsData stats = new StatisticsData();

            using (SQLiteConnection conn = new SQLiteConnection(_connectionString))
            {
                try
                {
                    conn.Open();

                    // Genel İstatistikler
                    CalculateGeneralStatistics(conn, stats);

                    // Banka İstatistikleri
                    CalculateBankStatistics(conn, stats);

                    // Cihaz İstatistikleri
                    CalculateDeviceStatistics(conn, stats);

                    // Trend Bilgileri
                    CalculateTrendStatistics(conn, stats);

                    // Hedef İstatistikleri
                    CalculateTargetStatistics(conn, stats);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("İstatistik hesaplanırken hata: " + ex.Message);
                    // Boş istatistikler döndür
                }
            }

            return stats;
        }

        public void CalculateGeneralStatistics(SQLiteConnection conn, StatisticsData stats)
        {
            string currentMonth = DateTime.Now.ToString("yyyyMM");

            // Bu ayki toplam ciro
            string query = "SELECT SUM(Tutar) FROM Gunsonu WHERE strftime('%Y%m', IslemTarihi) = @CurrentMonth";
            using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@CurrentMonth", currentMonth);
                object result = cmd.ExecuteScalar();
                stats.MonthlyTotalAmount = (result != DBNull.Value && result != null) ? Convert.ToDecimal(result) : 0;
            }

            // Bugün hesaba geçecek tutar - Banka tablosunda KesintiOrani yoksa düzeltme gerekebilir
            try
            {
                query = "SELECT SUM(G.Tutar) FROM Gunsonu G WHERE DATE(G.GececegiTarih) = DATE('now')";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    object result = cmd.ExecuteScalar();
                    stats.TodayIncomingAmount = (result != DBNull.Value && result != null) ? Convert.ToDecimal(result) : 0;
                }
            }
            catch
            {
                // Kesinti oranı yoksa direkt tutarı alırız
                query = "SELECT SUM(Tutar) FROM Gunsonu WHERE DATE(GececegiTarih) = DATE('now')";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    object result = cmd.ExecuteScalar();
                    stats.TodayIncomingAmount = (result != DBNull.Value && result != null) ? Convert.ToDecimal(result) : 0;
                }
            }

            // Toplam banka blokesi
            query = "SELECT SUM(Tutar) FROM Gunsonu WHERE DATE(GececegiTarih) >= DATE('now')";
            using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
            {
                object result = cmd.ExecuteScalar();
                stats.TotalBlockedAmount = (result != DBNull.Value && result != null) ? Convert.ToDecimal(result) : 0;
            }

            // Ortalama valör süresi
            query = "SELECT AVG(Valör) FROM Gunsonu WHERE strftime('%Y%m', IslemTarihi) = @CurrentMonth";
            using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@CurrentMonth", currentMonth);
                object result = cmd.ExecuteScalar();
                stats.AverageValorPeriod = (result != DBNull.Value && result != null) ? Convert.ToDecimal(result) : 0;
            }
        }

        public void CalculateBankStatistics(SQLiteConnection conn, StatisticsData stats)
        {
            string currentMonth = DateTime.Now.ToString("yyyyMM");

            // En çok kullanılan banka
            string query = "SELECT BankaAdi, SUM(Tutar) as ToplamTutar FROM Gunsonu WHERE strftime('%Y%m', IslemTarihi) = @CurrentMonth GROUP BY BankaAdi ORDER BY ToplamTutar DESC LIMIT 1";
            using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@CurrentMonth", currentMonth);
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        stats.MostUsedBank = reader["BankaAdi"].ToString();
                        stats.MostUsedBankAmount = Convert.ToDecimal(reader["ToplamTutar"]);
                    }
                }
            }

            // Banka dağılım oranları
            query = "SELECT BankaAdi, SUM(Tutar) as ToplamTutar FROM Gunsonu WHERE strftime('%Y%m', IslemTarihi) = @CurrentMonth GROUP BY BankaAdi ORDER BY ToplamTutar DESC";
            using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@CurrentMonth", currentMonth);
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string bankaAdi = reader["BankaAdi"].ToString();
                        decimal tutar = Convert.ToDecimal(reader["ToplamTutar"]);
                        stats.BankDistribution.Add(bankaAdi, tutar);
                    }
                }
            }

            // Bankaların işlem sayılarını alma
            query = "SELECT BankaAdi, COUNT(*) as IslemSayisi FROM Gunsonu WHERE strftime('%Y%m', IslemTarihi) = @CurrentMonth GROUP BY BankaAdi";
            using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@CurrentMonth", currentMonth);
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string bankaAdi = reader["BankaAdi"].ToString();
                        int islemSayisi = Convert.ToInt32(reader["IslemSayisi"]);
                        stats.BankTransactionCounts[bankaAdi] = islemSayisi;
                    }
                }
            }

            // En az kesinti yapan banka - Banka tablosunda KesintiOrani var mı kontrol et
            try
            {
                query = "SELECT BankaAdi, MIN(KesintiOrani) as EnDusukKesinti FROM Banka GROUP BY BankaAdi ORDER BY EnDusukKesinti ASC LIMIT 1";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                stats.LeastCommissionBank = reader["BankaAdi"].ToString();
                                stats.LeastCommissionValue = Convert.ToDecimal(reader["EnDusukKesinti"]);
                            }
                        }
                    }
                    else
                    {
                        stats.LeastCommissionBank = "Bilinmiyor";
                        stats.LeastCommissionValue = 0;
                    }
                }
            }
            catch
            {
                // KesintiOrani alanı yoksa veya sorgu başarısız olursa
                stats.LeastCommissionBank = "Bilinmiyor";
                stats.LeastCommissionValue = 0;
            }
        }

        public void CalculateDeviceStatistics(SQLiteConnection conn, StatisticsData stats)
        {
            string currentMonth = DateTime.Now.ToString("yyyyMM");
            int daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            int currentDay = DateTime.Now.Day;

            // En aktif cihaz
            string query = "SELECT CihazAdi, SUM(Tutar) as ToplamTutar FROM Gunsonu WHERE strftime('%Y%m', IslemTarihi) = @CurrentMonth GROUP BY CihazAdi ORDER BY ToplamTutar DESC LIMIT 1";
            using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@CurrentMonth", currentMonth);
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        stats.MostActiveDevice = reader["CihazAdi"].ToString();
                        stats.MostActiveDeviceAmount = Convert.ToDecimal(reader["ToplamTutar"]);
                    }
                }
            }

            // Cihaz performansları
            query = "SELECT CihazAdi, SUM(Tutar) as ToplamTutar, COUNT(*) as IslemSayisi FROM Gunsonu WHERE strftime('%Y%m', IslemTarihi) = @CurrentMonth GROUP BY CihazAdi ORDER BY ToplamTutar DESC";
            using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@CurrentMonth", currentMonth);
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DeviceStats deviceStat = new DeviceStats
                        {
                            DeviceName = reader["CihazAdi"].ToString(),
                            TotalAmount = Convert.ToDecimal(reader["ToplamTutar"]),
                            TransactionCount = Convert.ToInt32(reader["IslemSayisi"]),
                            DailyAverage = Convert.ToDecimal(reader["ToplamTutar"]) / currentDay
                        };
                        stats.DevicePerformance.Add(deviceStat);
                    }
                }
            }

            // Pasif cihazlar - önce tüm cihazları al
            List<string> allDevices = new List<string>();
            query = "SELECT DISTINCT CihazAdi FROM Banka";
            using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
            {
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        allDevices.Add(reader["CihazAdi"].ToString());
                    }
                }
            }

            // Sonra bu ay işlem yapılan cihazları al
            List<string> activeDevices = new List<string>();
            query = "SELECT DISTINCT CihazAdi FROM Gunsonu WHERE strftime('%Y%m', IslemTarihi) = @CurrentMonth";
            using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@CurrentMonth", currentMonth);
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        activeDevices.Add(reader["CihazAdi"].ToString());
                    }
                }
            }

            // Farkı bul (pasif cihazlar)
            foreach (string device in allDevices)
            {
                if (!activeDevices.Contains(device))
                {
                    stats.InactiveDevices.Add(device);
                }
            }
        }

        public void CalculateTrendStatistics(SQLiteConnection conn, StatisticsData stats)
        {
            DateTime now = DateTime.Now;
            string currentMonth = now.ToString("yyyyMM");

            // Önceki ay
            DateTime prevMonth = now.AddMonths(-1);
            string previousMonth = prevMonth.ToString("yyyyMM");

            // Önceki aydaki toplam
            string query = "SELECT SUM(Tutar) FROM Gunsonu WHERE strftime('%Y%m', IslemTarihi) = @PreviousMonth";
            using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@PreviousMonth", previousMonth);
                object result = cmd.ExecuteScalar();
                stats.PreviousMonthAmount = (result != DBNull.Value && result != null) ? Convert.ToDecimal(result) : 0;
            }

            // Değişim yüzdesi hesaplama
            if (stats.PreviousMonthAmount > 0)
            {
                stats.ChangePercentage = ((stats.MonthlyTotalAmount - stats.PreviousMonthAmount) / stats.PreviousMonthAmount) * 100;
            }

            // Günlük ortalama
            int currentDay = now.Day;
            if (currentDay > 0)
            {
                stats.AverageDailyAmount = stats.MonthlyTotalAmount / currentDay;
            }

            // Ay sonu tahmini
            int daysInMonth = DateTime.DaysInMonth(now.Year, now.Month);
            stats.MonthEndEstimate = stats.AverageDailyAmount * daysInMonth;
        }

        public void CalculateTargetStatistics(SQLiteConnection conn, StatisticsData stats)
        {
            // Hedef değeri veritabanından çekilecek (Bunu kullanıcının ayarladığı bir değer olarak düşünüyoruz)
            // Bu örnek için varsayılan bir değer kullanıyoruz, gerçek uygulamada ayarlar tablosundan alınabilir

            try
            {
                string query = "SELECT HedefTutar FROM Ayarlar WHERE ID = 1";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    object result = cmd.ExecuteScalar();
                    stats.MonthlyTarget = (result != DBNull.Value && result != null) ? Convert.ToDecimal(result) : 100000; // Varsayılan 100,000 TL
                }
            }
            catch
            {
                // Hedef tablosu henüz oluşturulmamış olabilir
                stats.MonthlyTarget = 100000; // Varsayılan 100,000 TL
            }

            // Hedef gerçekleşme oranı
            if (stats.MonthlyTarget > 0)
            {
                stats.TargetCompletionRate = (stats.MonthlyTotalAmount / stats.MonthlyTarget) * 100;
            }
        }

        public List<MonthlyComparisonData> GetMonthlyComparisonData(int monthCount = 6)
        {
            List<MonthlyComparisonData> comparisonData = new List<MonthlyComparisonData>();

            using (SQLiteConnection conn = new SQLiteConnection(_connectionString))
            {
                conn.Open();

                DateTime currentDate = DateTime.Now;
                for (int i = 0; i < monthCount; i++)
                {
                    DateTime targetMonth = currentDate.AddMonths(-i);
                    string monthYearStr = targetMonth.ToString("yyyyMM");
                    string monthName = targetMonth.ToString("MMM yyyy", new CultureInfo("tr-TR"));

                    string query = "SELECT SUM(Tutar) FROM Gunsonu WHERE strftime('%Y%m', IslemTarihi) = @Month";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Month", monthYearStr);
                        object result = cmd.ExecuteScalar();
                        decimal amount = (result != DBNull.Value && result != null) ? Convert.ToDecimal(result) : 0;

                        comparisonData.Add(new MonthlyComparisonData
                        {
                            Month = monthName,
                            TotalAmount = amount
                        });
                    }
                }
            }

            return comparisonData;
        }
    }
}