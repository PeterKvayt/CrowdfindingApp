﻿
namespace CrowdfindingApp.Common.Maintainers.CryptoProvider
{
    public interface ICryptoProvider
    {
        string Encrypt(object data);
        string Decrypt(string data);
    }
}
