using System;
using System.IO;
using System.Security.Cryptography;

namespace CrowdfindingApp.Common.Core.Maintainers.CryptoProvider
{
    public sealed class CryptoProvider : ICryptoProvider
    {
        /// <summary>
        /// Key array.
        /// </summary>
        private byte[] Key => Convert.FromBase64String("Nu/X+EAoEXgEP3DDm3I4gpDnEraFfdShjDG8bnsWD4E=");

        /// <summary>
        /// Input value.
        /// </summary>
        private byte[] Iv => Convert.FromBase64String("8BFnUsNOQdBCQBhKaMaOjQ==");

        /// <summary>
        /// Encrypts data and returns base64 ecrtypted string.
        /// </summary>
        /// <param name="data">data for encryption</param>
        /// <returns>base64 ecrtypted string</returns>
        public string Encrypt(object data)
        {
            if(data == null)
            {
                return string.Empty;
            }

            using(var ms = new MemoryStream())
            {
                using(var aes = Aes.Create())
                {
                    using(var encryptor = aes.CreateEncryptor(Key, Iv))
                    {
                        using(var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                        {
                            using(var sw = new StreamWriter(cs))
                            {
                                sw.Write(data);
                            }
                        }
                    }
                }
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        /// <summary>
        /// Decrypts data from base64 encrypted string.
        /// </summary>
        /// <param name="data">Encrypted string</param>
        /// <returns>Decrypted string or null in case of exception</returns>
        public string Decrypt(string data)
        {
            if(string.IsNullOrWhiteSpace(data))
            {
                return data;
            }

            try
            {
                using(var aes = Aes.Create())
                {
                    if(aes == null)
                    {
                        return string.Empty;
                    }
                    using(var ms = new MemoryStream(Convert.FromBase64String(data)))
                    {
                        using(var decryptor = aes.CreateDecryptor(Key, Iv))
                        {
                            using(var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                            {
                                using(var sr = new StreamReader(cs))
                                {
                                    return sr.ReadToEnd();
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
