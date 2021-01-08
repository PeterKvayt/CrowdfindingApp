using Autofac;
using CrowdfindingApp.Core.Services.User;
using CrowdfindingApp.Core.Services.User.Handlers;
using CrowdfindingApp.Data.Repositories;

namespace CrowdfindingApp.Api.Modules
{
    public class UserModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            RegisterHandlers(builder);

            builder.RegisterType<UserProfile>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<UserRepository>().AsImplementedInterfaces().SingleInstance();
        }

        private void RegisterHandlers(ContainerBuilder builder)
        {
            builder.RegisterType<RegisterRequestHandler>().AsSelf().SingleInstance();
            builder.RegisterType<GetTokenRequestHandler>().AsSelf().SingleInstance();
        }
    }
}
