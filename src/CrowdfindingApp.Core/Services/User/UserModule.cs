using System.Collections.Generic;
using System.Reflection;
using Autofac;
using CrowdfindingApp.Common.Localization;
using CrowdfindingApp.Core.Services.User.Handlers;

namespace CrowdfindingApp.Core.Services.User
{
    public class UserModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            RegisterHandlers(builder);

            // Repository registration in startup extensions.

            builder.RegisterType<UserProfile>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<ResourceProvider>()
                .WithParameter((x, _) => x.Name == "stringResources", (_, __) => new List<(string, Assembly)>()
                {
                    ("CrowdfindingApp.Core.Services.User.Resources.ActionMessages", Assembly.GetExecutingAssembly())
                })
                .As<IResourceProvider>()
                .SingleInstance();
        }

        private void RegisterHandlers(ContainerBuilder builder)
        {
            builder.RegisterType<RegisterRequestHandler>().AsSelf().SingleInstance();
            builder.RegisterType<GetTokenRequestHandler>().AsSelf().SingleInstance();
            builder.RegisterType<ForgotPasswordRequestHandler>().AsSelf().SingleInstance();
            builder.RegisterType<ResetPasswordRequestHandler>().AsSelf().SingleInstance();
        }
    }
}
