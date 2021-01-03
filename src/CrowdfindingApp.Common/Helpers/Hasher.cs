using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace CrowdfindingApp.Common.Helpers
{
    public class Hasher : IHasher
    {
        public string GetHash(string value)
        {
            var keyDerivation = KeyDerivation.Pbkdf2(
                password: value,
                salt: GetSalt(),
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 32);

            return Convert.ToBase64String(keyDerivation);
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
