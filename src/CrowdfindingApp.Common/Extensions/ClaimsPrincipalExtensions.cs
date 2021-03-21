using System;
using System.Security.Claims;
using CrowdfindingApp.Common.Immutable;

namespace CrowdfindingApp.Common.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal claims)
        {
            var claim = claims.FindFirst(ClaimsKeys.UserId);
            if(claim != null)
            {
                return new Guid(claim.Value);
            }
            else
            {
                return new Guid();
            }
        }

        public static bool HasRole(this ClaimsPrincipal claims, string roleName)
        {
            if(roleName.IsNullOrWhiteSpace()) throw new ArgumentNullException(nameof(roleName));

            var claim = claims.FindFirst(ClaimsIdentity.DefaultRoleClaimType);
            if(claim == null)
            {
                return false;
            }
            
            return claim.Value.Equals(roleName, StringComparison.InvariantCultureIgnoreCase);
        }

        public static string GetUserRoleName(this ClaimsPrincipal claims)
        {
            return claims.FindFirst(ClaimsIdentity.DefaultRoleClaimType)?.Value ?? string.Empty;
        }
    }
}
