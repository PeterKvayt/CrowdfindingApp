using Autofac;
using CrowdfindingApp.Common;
using CrowdfindingApp.Common.Immutable;
using CrowdfindingApp.Core.Services.Role;
using CrowdfindingApp.Core.Services.User;
using CrowdfindingApp.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace CrowdfindingApp.Api
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddAuthentication(this IServiceCollection services, IWebHostEnvironment environment)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = environment.IsProduction();
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

        public static void RegisterRepositories(this ContainerBuilder builder)
        {
            builder.RegisterType<RoleRepository>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<UserRepository>().AsImplementedInterfaces().SingleInstance();
        }

        public static void RegisterModules(this ContainerBuilder builder)
        {
            builder.RegisterModule<UserModule>();
            builder.RegisterModule<RoleModule>();
            builder.RegisterModule<CommonModule>();
        }
    }
}
