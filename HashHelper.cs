using System;
using System.Security.Cryptography;
using System.Text;

namespace NetMail
{
    // generate salt và hash, lưu dưới dạng "salt:hash"
    public static class HashHelper
    {
        // Generate a secure random salt (as base64)
        public static string GenerateSalt(int size = 16)
        {
            var rng = RandomNumberGenerator.Create();
            var bytes = new byte[size];
            rng.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }

        // Hash (salt + password) using SHA256, returns hash as hex string
        public static string ComputeSha256Hash(string salt, string password)
        {
            using (var sha = SHA256.Create())
            {
                // combine salt + password
                var plain = salt + password;
                var bytes = Encoding.UTF8.GetBytes(plain);
                var hash = sha.ComputeHash(bytes);
                var sb = new StringBuilder();
                foreach (var b in hash)
                    sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }

        // Convenience: create stored value "salt:hash"
        public static string CreateStoredPassword(string password)
        {
            var salt = GenerateSalt();
            var hash = ComputeSha256Hash(salt, password);
            return $"{salt}:{hash}";
        }

        // Validate password against stored "salt:hash"
        public static bool ValidatePassword(string password, string storedSaltHash)
        {
            if (string.IsNullOrEmpty(storedSaltHash)) return false;
            var parts = storedSaltHash.Split(':');
            if (parts.Length != 2) return false;
            var salt = parts[0];
            var expectedHash = parts[1];
            var actualHash = ComputeSha256Hash(salt, password);
            // Timing-attack safe compare
            return SecureEquals(expectedHash, actualHash);
        }

        private static bool SecureEquals(string a, string b)
        {
            if (a.Length != b.Length) return false;
            int diff = 0;
            for (int i = 0; i < a.Length; i++)
                diff |= a[i] ^ b[i];
            return diff == 0;
        }
    }
}
