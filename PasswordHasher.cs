using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace Veresiye2025
{
    public static class PasswordHasher
    {
        public static void HashAllPasswords()
        {
            string connectionString = "Data Source=" + Application.StartupPath + "\\veresiye.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Şifreleri Yonetim tablosundan almak için
                string selectQuery = "SELECT ID, Sifre FROM Yonetim";
                using (SQLiteCommand selectCmd = new SQLiteCommand(selectQuery, connection))
                {
                    using (SQLiteDataReader reader = selectCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int userId = reader.GetInt32(0);
                            string currentPassword = reader.GetString(1);

                            // Şifre zaten hashlenmişse atla
                            if (currentPassword.Length == 64 && IsHex(currentPassword))
                            {
                                Console.WriteLine($"User ID {userId} already has a hashed password. Skipping...");
                                continue;
                            }

                            // Şifreyi hashle
                            string hashedPassword = Helpers.HashPassword(currentPassword);

                            // Hashlenmiş şifreyi güncelle
                            string updateQuery = "UPDATE Yonetim SET Sifre = @HashedSifre WHERE ID = @UserID";
                            using (SQLiteCommand updateCmd = new SQLiteCommand(updateQuery, connection))
                            {
                                updateCmd.Parameters.AddWithValue("@HashedSifre", hashedPassword);
                                updateCmd.Parameters.AddWithValue("@UserID", userId);
                                updateCmd.ExecuteNonQuery();
                                Console.WriteLine($"User ID {userId} için şifre başarıyla hashlenip güncellendi.");
                            }
                        }
                    }
                }
            }
        }

        // Şifrenin hex formatında olup olmadığını kontrol eder
        public static bool IsHex(string input)
        {
            foreach (char c in input)
            {
                if (!Uri.IsHexDigit(c)) return false;
            }
            return true;
        }
    }
}
