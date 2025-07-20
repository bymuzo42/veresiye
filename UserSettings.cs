using System;
using System.Data.SQLite;
using System.IO;

namespace Veresiye2025
{
    public class UserSettings
    {
        public readonly string _connectionString = "Data Source=veresiye.db;Version=3;";
        public static UserSettings _instance;

        public bool ShowStatisticsPopup { get; set; } = true;
        public decimal MonthlyTarget { get; set; } = 100000;

        public UserSettings()
        {
            LoadSettings();
        }

        public static UserSettings Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UserSettings();
                }
                return _instance;
            }
        }

        public void LoadSettings()
        {
            try
            {
                // Ayarlar tablosunu oluştur (yoksa)
                EnsureSettingsTableExists();

                using (SQLiteConnection conn = new SQLiteConnection(_connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM Ayarlar WHERE ID = 1";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                ShowStatisticsPopup = Convert.ToBoolean(reader["ShowStatisticsPopup"]);
                                MonthlyTarget = Convert.ToDecimal(reader["HedefTutar"]);
                            }
                            else
                            {
                                // İlk kullanımsa varsayılan ayarları ekle
                                SaveSettings();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ayarlar yüklenirken hata: " + ex.Message);
                // Varsayılan ayarları kullan
            }
        }

        public void SaveSettings()
        {
            try
            {
                EnsureSettingsTableExists();

                using (SQLiteConnection conn = new SQLiteConnection(_connectionString))
                {
                    conn.Open();

                    // Ayarlar tablosunda kayıt var mı kontrol et
                    string checkQuery = "SELECT COUNT(*) FROM Ayarlar WHERE ID = 1";
                    bool recordExists = false;

                    using (SQLiteCommand checkCmd = new SQLiteCommand(checkQuery, conn))
                    {
                        recordExists = Convert.ToInt32(checkCmd.ExecuteScalar()) > 0;
                    }

                    string query;
                    if (recordExists)
                    {
                        // Kayıt varsa güncelle
                        query = "UPDATE Ayarlar SET ShowStatisticsPopup = @ShowPopup, HedefTutar = @Hedef, Ay = @CurrentMonth WHERE ID = 1";
                    }
                    else
                    {
                        // Kayıt yoksa ekle
                        query = "INSERT INTO Ayarlar (ID, ShowStatisticsPopup, HedefTutar, Ay) VALUES (1, @ShowPopup, @Hedef, @CurrentMonth)";
                    }

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ShowPopup", ShowStatisticsPopup);
                        cmd.Parameters.AddWithValue("@Hedef", MonthlyTarget);
                        cmd.Parameters.AddWithValue("@CurrentMonth", DateTime.Now.ToString("yyyyMM"));
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ayarlar kaydedilirken hata: " + ex.Message);
            }
        }

        public void EnsureSettingsTableExists()
        {
            using (SQLiteConnection conn = new SQLiteConnection(_connectionString))
            {
                conn.Open();
                string createTableQuery = @"
                    CREATE TABLE IF NOT EXISTS Ayarlar (
                        ID INTEGER PRIMARY KEY,
                        ShowStatisticsPopup BOOLEAN NOT NULL DEFAULT 1,
                        HedefTutar REAL NOT NULL DEFAULT 100000,
                        Ay TEXT
                    )";
                using (SQLiteCommand cmd = new SQLiteCommand(createTableQuery, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}