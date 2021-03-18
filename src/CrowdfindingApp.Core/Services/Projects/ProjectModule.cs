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

            builder.RegisterType<ProjectProfile>().As<Profile>();
            // Repository registration in startup extensions.
        }

        private void RegisterHandlers(ContainerBuilder builder)
        {
            builder.RegisterType<SaveDraftProjectRequestHandler>().AsSelf();
            builder.RegisterType<ProjectSearchRequestHandler>().AsSelf();
            builder.RegisterType<GetCitiesRequestHandler>().AsSelf();
            builder.RegisterType<GetCountriesRequestHandler>().AsSelf();
            builder.RegisterType<GetCategoriesRequestHandler>().AsSelf();
            builder.RegisterType<ProjectModerationRequestHandler>().AsSelf();
        }
    }
}
