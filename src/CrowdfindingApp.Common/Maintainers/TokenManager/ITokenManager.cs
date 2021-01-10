using System;

namespace CrowdfindingApp.Common.Maintainers.TokenManager
{
    public interface ITokenManager
    {
        string GetResetPasswordToken(Guid guid);
        bool ValidateResetPasswordToken(string token, out Guid userId);
    }
}