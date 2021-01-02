using CrowdfindingApp.Common.Immutable;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CrowdfindingApp.Api
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false; // ToDo: add possibility to make this prop dependent of environment
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = AuthenticationOptions.ValidateIssuer,
                            ValidIssuer = AuthenticationOptions.ValidIssuer,
                            ValidateAudience = AuthenticationOptions.ValidateAudience,
                            ValidAudience = AuthenticationOptions.ValidAudience,
                            ValidateLifetime = AuthenticationOptions.ValidateLifetime,
                            IssuerSigningKey = AuthenticationOptions.IssuerSigningKey,
                            ValidateIssuerSigningKey = AuthenticationOptions.ValidateIssuerSigningKey,
                        };
                    });

            return services;
        }
    }
}
