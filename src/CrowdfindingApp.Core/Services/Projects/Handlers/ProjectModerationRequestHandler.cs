using System;
using System.Threading.Tasks;
using CrowdfindingApp.Common.Messages;
using CrowdfindingApp.Common.Messages.Projects;
using CrowdfindingApp.Core.Services.Projects.Validators;
using CrowdfindingApp.Common.Extensions;
using AutoMapper;
using CrowdfindingApp.Common.Data.Interfaces.Repositories;
using CrowdfindingApp.Common.Data.BusinessModels;
using CrowdfindingApp.Common.Enums;
using CrowdfindingApp.Common.Data.Filters;
using System.Collections.Generic;
using CrowdfindingApp.Common.Maintainers.FileStorageProvider;

namespace CrowdfindingApp.Core.Services.Projects.Handlers
{
    public class ProjectModerationRequestHandler : SaveProjectRequestHandlerBase<ProjectModerationRequestMessage, ReplyMessage<string>>
    {
        public ProjectModerationRequestHandler(IMapper mapper, 
            IProjectRepository projectRepository, 
            IRewardRepository rewardRepository, 
            IRewardGeographyRepository rewardGeographyRepository,
            IFileStorage fileStorage,
            IQuestionRepository questionRepository) : base(mapper, projectRepository, rewardRepository, rewardGeographyRepository, fileStorage, questionRepository)
        {

        }

        protected override async Task<ReplyMessageBase> ValidateRequestMessageAsync(ProjectModerationRequestMessage requestMessage)
        {
            var reply = await base.ValidateRequestMessageAsync(requestMessage);
            var validator = new ProjectModerationValidator();
            var validationResult = await validator.ValidateAsync(requestMessage.Data);
            await reply.MergeAsync(validationResult);

            return reply;
        }

        protected override async Task<ReplyMessage<string>> ExecuteAsync(ProjectModerationRequestMessage request)
        {
            return await ProcessAsync(request);
        }

        protected override void PrepareValues(Project project, bool isNew)
        {
            base.PrepareValues(project, isNew);
            project.Status = (int)ProjectStatus.Moderation;
        }
     
    }
}
