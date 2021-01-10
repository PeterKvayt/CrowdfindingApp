using System;
using CrowdfindingApp.Common.Extensions;
using CrowdfindingApp.Common.Maintainers.CryptoProvider;

namespace CrowdfindingApp.Common.Maintainers.TokenManager
{
    public sealed class TokenManager : ITokenManager
    {
        private const char _splitter = ';';
        private readonly ICryptoProvider _cryptoProvider;

        private const string ResetPasswordPurpose = "ResetPasswordToken";

        public TokenManager(ICryptoProvider cryptoProvider)
        {
            _cryptoProvider = cryptoProvider ?? throw new ArgumentNullException(nameof(cryptoProvider));
        }

        private bool TryGetValuesFromResetPasswordToken(string token, out Guid userId, out DateTime tokenCreatedDate)
        {
            userId = Guid.Empty;
            tokenCreatedDate = new DateTime();

            var data = _cryptoProvider.Decrypt(token);

            if(data.IsPresent() && data.Contains(_splitter))
            {
                var values = data.Split(_splitter);

                if(values[2] != ResetPasswordPurpose)
                {
                    return false;
                }

                if(values[0].IsNullOrEmpty())
                {
                    return false;
                }

                if(!Guid.TryParse(values[0], out userId))
                {
                    return false;
                }

                return DateTime.TryParse(values[1], out tokenCreatedDate);
            }
            else
            {
                return false;
            }
        }

        public string GetResetPasswordToken(Guid guid)
        {
            return _cryptoProvider.Encrypt($"{guid}{_splitter}{DateTime.UtcNow}{_splitter}{ResetPasswordPurpose}");
        }

        public bool ValidateResetPasswordToken(string token, out Guid userId)
        {
            var success = TryGetValuesFromResetPasswordToken(token, out userId, out DateTime createdDateTime);
            if(success)
            {
                return !IsExpired(createdDateTime);
            }
            else
            {
                return success;
            }
        }

        private bool IsExpired(DateTime tokenCreatedDate, int expirDaysPeriod = 0, int expirHoursPeriod = 0, int expirMinutesPeriod = 10, int expirSecondsPeriod = 0, int expirMilliSecondsPeriod = 0)
        {
            var expirationPeriod = new TimeSpan(expirDaysPeriod, expirHoursPeriod, expirMinutesPeriod, expirSecondsPeriod, expirMilliSecondsPeriod);
            var timeDifference = (DateTime.Now - tokenCreatedDate).TotalMinutes;

            return timeDifference > expirationPeriod.TotalMinutes;
        }
    }
}
