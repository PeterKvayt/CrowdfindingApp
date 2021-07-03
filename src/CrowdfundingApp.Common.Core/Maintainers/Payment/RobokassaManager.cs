using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using CrowdfindingApp.Common.Configs;
using CrowdfindingApp.Common.Enums;
using CrowdfindingApp.Common.Core.Messages.Payment;

namespace CrowdfindingApp.Common.Core.Maintainers.Payment
{
    public class RobokassaManager : IPaymentManager
    {
        private readonly AppConfiguration _config;

        private readonly decimal RybCurrency = 29.36M;

        #region Robokassa settings 

        private const string Login = "MyCrowdAppStoretempId";// login
        private const string TestPassword1 = "qEvgBLaNU4Z66Rfj7gg7";// test password #1
        private const string TestPassword2 = "jvMEmaO15V21sECB6Iki";// test password #2
        private const string Password1 = "NdSa0OT4cUHvkWx88E3t";// password #1
        private const string Password2 = "FepmuMe4KXhg6a8ce6M2";// password #2

        private const int IsTest = 1; // 1 - true

        #endregion

        public RobokassaManager(AppConfiguration configuration)
        {
            _config = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public string GetPaymentUri(decimal sum, string description, Guid orderId)
        {
            // order properties
            var formattedSum = (sum * RybCurrency).ToString("0.00", CultureInfo.InvariantCulture);
            var sCrcBase = string.Format("{0}:{1}:{2}:{3}", Login, formattedSum, orderId, TestPassword1);

            // build CRC value
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] bSignature = md5.ComputeHash(Encoding.ASCII.GetBytes(sCrcBase));

            var sbSignature = new StringBuilder();
            foreach(byte b in bSignature)
            {
                sbSignature.AppendFormat("{0:x2}", b);
            }

            var sCrc = sbSignature.ToString();

            var host = "https://auth.robokassa.ru/Merchant/Index.aspx";
            var loginParam = $"MerchantLogin={Login}";
            var sumParam = $"OutSum={formattedSum}";
            var idParam = $"InvId={orderId}";
            var descriptionParam = $"Description={description}";
            var signatureParam = $"SignatureValue={sCrc}";
            var isTestParam = $"IsTest={IsTest}";

            return $"{host}?{loginParam}&{sumParam}&{idParam}&{descriptionParam}&{signatureParam}&{isTestParam}";
        }

        public bool ValidateResponse(PaymentRequestMessage message, string id)
        {
            var sCrcBase = string.Format("{0}:{1}:{2}", message.OutSum, id, TestPassword2);
            // build own CRC
            var md5 = new MD5CryptoServiceProvider();
            var bSignature = md5.ComputeHash(Encoding.ASCII.GetBytes(sCrcBase));

            var sbSignature = new StringBuilder();
            foreach(byte b in bSignature)
            {
                sbSignature.AppendFormat("{0:x2}", b);
            }

            var sMyCrc = sbSignature.ToString();

            return sMyCrc.Equals(message.SignatureValue, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
