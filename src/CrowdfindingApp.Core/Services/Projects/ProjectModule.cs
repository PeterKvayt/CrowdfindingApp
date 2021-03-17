using Autofac;
using AutoMapper;
using CrowdfindingApp.Core.Services.Projects.Handlers;

namespace CrowdfindingApp.Core.Services.Projects
{
    public class ProjectModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            RegisterHandlers(builder);

            builder.RegisterType<ProjectProfile>().As<Profile>().SingleInstance();
            // Repository registration in startup extensions.
        }

        private void RegisterHandlers(ContainerBuilder builder)
        {
            builder.RegisterType<SaveDraftProjectRequestHandler>().AsSelf().SingleInstance();
            builder.RegisterType<ProjectSearchRequestHandler>().AsSelf().SingleInstance();
            builder.RegisterType<GetCitiesRequestHandler>().AsSelf().SingleInstance();
            builder.RegisterType<GetCountriesRequestHandler>().AsSelf().SingleInstance();
        }
    }
}
