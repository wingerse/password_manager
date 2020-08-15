using System;
using System.Security.Cryptography;
using System.Text;

namespace PasswordManagerV3.Other
{
    static class Sha256
    {
        public static string Encode(string input, string salt)
        {
            byte[] hash = SHA256.Create().ComputeHash(Encoding.ASCII.GetBytes(input + salt));
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
    }
}
