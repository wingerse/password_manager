using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace PasswordManagerV3.Other.Security
{
    internal class SimpleAes : ICipher
    {
        private readonly UTF8Encoding _encoder;
        private readonly byte[] _key;
        private readonly Random _random;
        private readonly RijndaelManaged _rm;

        public SimpleAes(string key)
        {
            _random = new Random();
            _rm = new RijndaelManaged();
            _encoder = new UTF8Encoding();
            _key = Encoding.UTF8.GetBytes(key).Take(16).ToArray();
        }

        public string Decrypt(string encrypted)
        {
            byte[] cryptogram = Convert.FromBase64String(encrypted);
            if (cryptogram.Length < 17)
                throw new ArgumentException("Not a valid encrypted string", nameof(encrypted));

            byte[] vector = cryptogram.Take(16).ToArray();
            byte[] buffer = cryptogram.Skip(16).ToArray();
            return _encoder.GetString(Decrypt(buffer, vector));
        }

        public string Encrypt(string unencrypted)
        {
            var vector = new byte[16];
            _random.NextBytes(vector);
            IEnumerable<byte> cryptogram = vector.Concat(Encrypt(_encoder.GetBytes(unencrypted), vector));
            return Convert.ToBase64String(cryptogram.ToArray());
        }

        private byte[] Decrypt(byte[] buffer, byte[] vector)
        {
            ICryptoTransform decryptor = _rm.CreateDecryptor(_key, vector);
            return Transform(buffer, decryptor);
        }

        private byte[] Encrypt(byte[] buffer, byte[] vector)
        {
            ICryptoTransform encryptor = _rm.CreateEncryptor(_key, vector);
            return Transform(buffer, encryptor);
        }

        private byte[] Transform(byte[] buffer, ICryptoTransform transform)
        {
            var stream = new MemoryStream();
            using (var cs = new CryptoStream(stream, transform, CryptoStreamMode.Write))
                cs.Write(buffer, 0, buffer.Length);

            return stream.ToArray();
        }
    }
}