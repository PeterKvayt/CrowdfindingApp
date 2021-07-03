using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace CrowdfundingApp.Common.Immutable
{
    public static class AuthenticationOptions
    {
        public const bool ValidateIssuer = true;
        public const bool ValidateAudience = true;
        public const bool ValidateLifetime = true;
        public const bool ValidateIssuerSigningKey = true;
        public const string ValidIssuer = "CrowdfundingApp";
        public const string ValidAudience = "CrowdfundingAppClient";
        const string Key = "b9FIqypA)6fTN~nw1:G!u&?`i<3Nrs760{)r:iAh%KJ`w`<hiP$f4E>{-zlC1SU";
        public const int LifeTime = 300;

        public static SymmetricSecurityKey IssuerSigningKey => new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
    }
}
