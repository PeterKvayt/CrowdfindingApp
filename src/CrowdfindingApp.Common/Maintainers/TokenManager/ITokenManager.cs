using System;

namespace CrowdfindingApp.Common.Maintainers.TokenManager
{
    public interface ITokenManager
    {
        string GetResetPasswordToken(Guid guid);
        bool ValidateResetPasswordToken(string token, out Guid userId);

        string GetConfirmEmailToken(Guid guid);
        bool ValidateConfirmEmailToken(string token, out Guid userId);
    }
}