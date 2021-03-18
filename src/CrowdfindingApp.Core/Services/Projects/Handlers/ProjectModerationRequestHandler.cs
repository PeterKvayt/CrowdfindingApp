using System;
using System.Threading.Tasks;
using CrowdfindingApp.Common.Messages;
using CrowdfindingApp.Common.Messages.Projects;
using CrowdfindingApp.Core.Services.Projects.Validators;
using CrowdfindingApp.Common.Extensions;
using AutoMapper;
using CrowdfindingApp.Data.Common.Interfaces.Repositories;
using CrowdfindingApp.Data.Common.BusinessModels;
using CrowdfindingApp.Common.Enums;
using CrowdfindingApp.Data.Common.Filters;
using System.Collections.Generic;

namespace CrowdfindingApp.Core.Services.Projects.Handlers
{
    public class ProjectModerationRequestHandler : SaveProjectRequestHandlerBase<ProjectModerationRequestMessage, ReplyMessageBase>
    {
        public ProjectModerationRequestHandler(IMapper mapper, 
            IProjectRepository projectRepository, 
            IRewardRepository rewardRepository, 
            IRewardGeographyRepository rewardGeographyRepository,
            IQuestionRepository questionRepository) : base(mapper, projectRepository, rewardRepository, rewardGeographyRepository, questionRepository)
        {

        }

        protected override async Task<ReplyMessageBase> ValidateRequestMessageAsync(ProjectModerationRequestMessage requestMessage)
        {
            var reply = await base.ValidateRequestMessageAsync(requestMessage);
            var validator = new ProjectModerationValidator();
            var validationResult = await validator.ValidateAsync(requestMessage.Data);
            await reply.MergeAsync(validationResult);

            if(validationResult.IsValid && requestMessage.Data.Id.NonNullOrWhiteSpace())
            {
                var projectFromDb = ProjectRepository.GetProjects(new ProjectFilter
                {
                    Id = new List<Guid> { new Guid(requestMessage.Data.Id) },
                    OwnerId = new List<Guid> { User.GetUserId() }
                }, null);
                
                if(projectFromDb == null)
                {
                    reply.AddObjectNotFoundError();
                }
            }

            return reply;
        }

        protected override async Task<ReplyMessageBase> ExecuteAsync(ProjectModerationRequestMessage request)
        {
            return await ProcessAsync(request);
        }

        protected override void SetDefaultValues(Project project, bool isNew)
        {
            base.SetDefaultValues(project, isNew);
            project.Status = (int)ProjectStatus.Moderation;
        }
     
    }
}
