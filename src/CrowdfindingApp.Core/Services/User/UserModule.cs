using Autofac;
using CrowdfindingApp.Core.Services.User.Handlers;

namespace CrowdfindingApp.Core.Services.User
{
    public class UserModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            RegisterHandlers(builder);
        }

        private void RegisterHandlers(ContainerBuilder builder)
        {
            builder.RegisterType<GetTokenRequestHandler>().AsSelf().SingleInstance();
            builder.RegisterType<RegisterRequestHandler>().AsSelf().SingleInstance();
        }
    }
}
