using System;
using System.Threading.Tasks;
using CrowdfundingApp.Common.Core.Messages;
using CrowdfundingApp.Common.Core.Messages.Projects;
using CrowdfundingApp.Core.Services.Projects.Validators;
using CrowdfundingApp.Common.Extensions;
using AutoMapper;
using CrowdfundingApp.Common.Data.Interfaces.Repositories;
using CrowdfundingApp.Common.Data.BusinessModels;
using CrowdfundingApp.Common.Enums;
using CrowdfundingApp.Common.Data.Filters;
using System.Collections.Generic;
using CrowdfundingApp.Common.Core.Maintainers.FileStorageProvider;
using CrowdfundingApp.Common.Core.Extensions;

namespace CrowdfundingApp.Core.Services.Projects.Handlers
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
