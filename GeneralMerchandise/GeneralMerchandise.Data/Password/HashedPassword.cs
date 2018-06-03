using System;
using System.Text;
using System.Security.Cryptography;
using System.Collections;
using System.Linq;

namespace GeneralMerchandise.Data.Password
{
    internal class PasswordHash : ISecuredPassword
    {
        private static readonly int SALT_SIZE = 16;

        public byte[] SecurePassword(string password)
        {
            HashAlgorithm hash = new SHA1CryptoServiceProvider();
            RandomNumberGenerator rng = new RNGCryptoServiceProvider();
            byte[] passwordBytes = Encoding.ASCII.GetBytes(password);
            byte[] salt = CreateSalt(rng, SALT_SIZE);
            return hash.ComputeHash(PreppendBytes(salt, passwordBytes));
        }

        public bool VerifyPassword(string input, string storedPassword)
        {
            return storedPassword.Equals(SecurePassword(input));
        }

        private static byte[] CreateSalt(RandomNumberGenerator randomNumberGenerator, int size)
        {
            byte[] bytes = new Byte[size];
            randomNumberGenerator.GetBytes(bytes);
            return bytes;
        }

        private byte[] PreppendBytes(byte[] bytes, byte[] toPreppend)
        {
            return toPreppend.Concat(bytes).ToArray();
        }
    }
}
