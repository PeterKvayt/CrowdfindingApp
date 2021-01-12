using System.Collections.Generic;
using System.Reflection;
using Autofac;
using AutoMapper;
using CrowdfindingApp.Common;
using CrowdfindingApp.Common.Immutable;
using CrowdfindingApp.Common.Localization;
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
            builder.RegisterModule<RoleModule>();
            builder.RegisterModule<UserModule>();
            builder.RegisterModule<CommonModule>();
        }

        public static void RegisterResourceProviders(this ContainerBuilder builder)
        {
            // ToDo: implement resource providers in modules.
            var providers = new List<(string, Assembly)>
            {
                ("CrowdfindingApp.Core.Services.User.Resources.ActionMessages", typeof(UserModule).Assembly),
                ("CrowdfindingApp.Common.Resources.EmailResources", typeof(CommonModule).Assembly),
            };

            builder.Register(ctx => new ResourceProvider(providers)).AsImplementedInterfaces().SingleInstance();
        }

        /// <summary>
        /// Register all mapping profiles. Should be called after all profiles registrations.
        /// </summary>
        /// <param name="builder">Container builder.</param>
        public static void RegisterAutoMapper(this ContainerBuilder builder)
        {
            builder.Register(ctx => new MapperConfiguration(cfg =>
            {
                foreach(var profile in ctx.Resolve<IList<Profile>>())
                {
                    cfg.AddProfile(profile);
                }
            }));

            builder.Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper()).As<IMapper>().SingleInstance();
        }
    }
}
