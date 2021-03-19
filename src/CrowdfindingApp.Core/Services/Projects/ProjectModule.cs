using Autofac;
using AutoMapper;
using CrowdfindingApp.Core.Services.Projects.Handlers;
using CrowdfindingApp.Core.Services.Projects.Mappings;

namespace CrowdfindingApp.Core.Services.Projects
{
    public class ProjectModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Repository registration in startup extensions.
            RegisterHandlers(builder);

            builder.RegisterType<ProjectProfile>().As<Profile>();
            builder.RegisterType<RewardProfile>().As<Profile>();
            builder.RegisterType<QuestionProfile>().As<Profile>();
        }

        private void RegisterHandlers(ContainerBuilder builder)
        {
            builder.RegisterType<SaveDraftProjectRequestHandler>().AsSelf();
            builder.RegisterType<ProjectSearchRequestHandler>().AsSelf();
            builder.RegisterType<GetCitiesRequestHandler>().AsSelf();
            builder.RegisterType<GetCountriesRequestHandler>().AsSelf();
            builder.RegisterType<GetCategoriesRequestHandler>().AsSelf();
            builder.RegisterType<ProjectModerationRequestHandler>().AsSelf();
            builder.RegisterType<ProjectCardSearchRequestHandler>().AsSelf();
            builder.RegisterType<RemoveProjectRequestHandler>().AsSelf();
        }
    }
}
