using System.Security.Cryptography;
using System.Text;

namespace Authentication.Helper
{
    public static class PasswordHelper
    {
        // Generate a random salt
        public static string GenerateSalt()
        {
            return Guid.NewGuid().ToString();
        }

        // Hash the password with salt
        public static string HashPassword(string password, string salt)
        {
            var combined = password + salt;
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(combined);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        // Verify password
        public static bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        {
            var enteredHash = HashPassword(enteredPassword, storedSalt);
            return enteredHash == storedHash;
        }
    }
}
