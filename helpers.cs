using System.Security.Cryptography;
using System.Text;

namespace Veresiye2025
{
    public static class Helpers
    {
        // Şifreyi SHA-256 ile hashlemek için kullanılan fonksiyon
        public static string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
