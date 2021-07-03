using System;
using CrowdfundingApp.Common.Extensions;
using CrowdfundingApp.Common.Core.Maintainers.CryptoProvider;

namespace CrowdfundingApp.Common.Core.Maintainers.TokenManager
{
    public sealed class TokenManager : ITokenManager
    {
        private const char _splitter = ';';
        private readonly ICryptoProvider _cryptoProvider;

        private const string ResetPasswordToken = nameof(ResetPasswordToken);
        private const string ConfirmEmailToken = nameof(ConfirmEmailToken);

        public TokenManager(ICryptoProvider cryptoProvider)
        {
            _cryptoProvider = cryptoProvider ?? throw new ArgumentNullException(nameof(cryptoProvider));
        }

        private bool TryGetValuesFromToken(string token, out Guid userId, out DateTime tokenCreatedDate, string tokenName)
        {
            userId = Guid.Empty;
            tokenCreatedDate = new DateTime();

            var data = _cryptoProvider.Decrypt(token);

            if(data.IsPresent() && data.Contains(_splitter))
            {
                var values = data.Split(_splitter);

                if(values[2].Equals(tokenName, StringComparison.InvariantCulture))
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

        private bool ValidateToken(string token, out Guid userId, string purpose)
        {
            var success = TryGetValuesFromToken(token, out userId, out DateTime createdDateTime, purpose);
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

        #region Reset password methods

        public string GetResetPasswordToken(Guid guid)
        {
            return _cryptoProvider.Encrypt($"{guid}{_splitter}{DateTime.UtcNow}{_splitter}{ResetPasswordToken}");
        }

        public bool ValidateResetPasswordToken(string token, out Guid userId)
        {
            return ValidateToken(token, out userId, ResetPasswordToken);
        }

        #endregion

        #region Confirm email methods

        public string GetConfirmEmailToken(Guid guid)
        {
            return _cryptoProvider.Encrypt($"{guid}{_splitter}{DateTime.UtcNow}{_splitter}{ConfirmEmailToken}");
        }

        public bool ValidateConfirmEmailToken(string token, out Guid userId)
        {
            return ValidateToken(token, out userId, ConfirmEmailToken);
        }

        #endregion

    }
}
