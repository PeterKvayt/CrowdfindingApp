﻿using Autofac;
using AutoMapper;
using CrowdfundingApp.Core.Services.Projects.Handlers;
using CrowdfundingApp.Core.Services.Projects.Mappings;

namespace CrowdfundingApp.Core.Services.Projects
{
    public class ProjectModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Repository registration in startup extensions.
            RegisterHandlers(builder);

            builder.RegisterType<ProjectProfile>().As<Profile>();
            builder.RegisterType<QuestionProfile>().As<Profile>();
        }

        private void RegisterHandlers(ContainerBuilder builder)
        {
            builder.RegisterType<SaveDraftProjectRequestHandler>().AsSelf();
            builder.RegisterType<GetCitiesRequestHandler>().AsSelf();
            builder.RegisterType<GetCountriesRequestHandler>().AsSelf();
            builder.RegisterType<GetCategoriesRequestHandler>().AsSelf();
            builder.RegisterType<ProjectModerationRequestHandler>().AsSelf();
            builder.RegisterType<OwnerProjectCardSearchRequestHandler>().AsSelf();
            builder.RegisterType<RemoveProjectRequestHandler>().AsSelf();
            builder.RegisterType<GetByIdRequestHandler>().AsSelf();
            builder.RegisterType<SetProjectStatusRequestHandler>().AsSelf();
            builder.RegisterType<UnsafeProjectCardSearchRequestHandler>().AsSelf();
            builder.RegisterType<OpenedProjectCardSearchRequestHandler>().AsSelf();
            builder.RegisterType<GetProjectInfoViewByIdRequestHandler>().AsSelf();
            builder.RegisterType<GetSupportedProjectCardsRequestHandler>().AsSelf();
            builder.RegisterType<GetTopProjectCardsRequestHandler>().AsSelf();
        }
    }
}
