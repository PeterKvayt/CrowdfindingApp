using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Autofac;
using AutoMapper;
using CrowdfindingApp.Common;
using CrowdfindingApp.Common.Immutable;
using CrowdfindingApp.Common.Localization;
using CrowdfindingApp.Core.Services.Projects;
using CrowdfindingApp.Core.Services.Roles;
using CrowdfindingApp.Core.Services.Users;
using CrowdfindingApp.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

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

        public static ContainerBuilder RegisterRepositories(this ContainerBuilder builder)
        {
            builder.RegisterType<RoleRepository>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<UserRepository>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<ProjectRepository>().AsImplementedInterfaces().SingleInstance();

            return builder;
        }

        public static ContainerBuilder RegisterModules(this ContainerBuilder builder)
        {
            builder.RegisterModule<RoleModule>();
            builder.RegisterModule<UserModule>();
            builder.RegisterModule<ProjectModule>();
            builder.RegisterModule<CommonModule>();

            return builder;
        }

        public static ContainerBuilder RegisterResourceProviders(this ContainerBuilder builder)
        {
            // ToDo: implement resource providers in modules.
            var providers = new List<(string, Assembly)>
            {
                ("CrowdfindingApp.Core.Services.Users.Resources.ActionMessages", typeof(UserModule).Assembly),
                ("CrowdfindingApp.Core.Services.Projects.Resources.ErrorMessages", typeof(ProjectModule).Assembly),
                ("CrowdfindingApp.Common.Resources.EmailResources", typeof(CommonModule).Assembly),
                ("CrowdfindingApp.Common.Resources.CommonErrorMessages", typeof(CommonModule).Assembly),
            };

            builder.Register(ctx => new ResourceProvider(providers)).AsImplementedInterfaces().SingleInstance();
            return builder;
        }

        /// <summary>
        /// Register all mapping profiles. Should be called after all profiles registrations.
        /// </summary>
        /// <param name="builder">Container builder.</param>
        public static ContainerBuilder RegisterAutoMapper(this ContainerBuilder builder)
        {
            builder.RegisterType<CommonProfile>().As<Profile>().SingleInstance();
            builder.Register(ctx => new MapperConfiguration(cfg =>
            {
                foreach(var profile in ctx.Resolve<IList<Profile>>())
                {
                    cfg.AddProfile(profile);
                }
            }));

            builder.Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper()).As<IMapper>().SingleInstance();

            return builder;
        }

        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            return services.AddSwaggerGen(config =>
            {
                var authenticationTypeId = "Authentication";
                config.AddSecurityDefinition(authenticationTypeId, new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                });
                config.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = authenticationTypeId
                            },
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });

                var apiDocs = Path.Combine(AppContext.BaseDirectory, $"{typeof(Startup).Assembly.GetName().Name}.xml");
                config.IncludeXmlComments(apiDocs);
                var commonDocs = Path.Combine(AppContext.BaseDirectory, $"{typeof(CommonModule).Assembly.GetName().Name}.xml");
                config.IncludeXmlComments(commonDocs);
            });
        }
    }
}
