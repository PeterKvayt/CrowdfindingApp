using Autofac;
using CrowdfindingApp.Data.Repositories;

namespace CrowdfindingApp.Api.Modules
{
    public class RoleModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RoleRepository>().AsImplementedInterfaces().SingleInstance();
        }
    }
}
