using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace CrowdfindingApp.Common.Maintainers.Hasher
{
    public class Hasher : IHasher
    {
        public bool Equals(string hash, string valueToHash, string salt)
        {
            var saltBytes = Convert.FromBase64String(salt);
            var hashedValue = GetHash(valueToHash, saltBytes);
            return hash.Equals(hashedValue, StringComparison.InvariantCultureIgnoreCase);
        }

        private string GetHash(string value, byte[] salt)
        {
            var keyDerivation = KeyDerivation.Pbkdf2(
                password: value,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 32);

            return Convert.ToBase64String(keyDerivation);
        }

        public (string, string) GetHashWithSalt(string value)
        {
            var salt = GetSalt();
            var hash = GetHash(value, salt);
            return (hash, Convert.ToBase64String(salt));
        }

        private byte[] GetSalt()
        {
            var salt = new byte[16];
            using(var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return salt;
        }
    }
}
