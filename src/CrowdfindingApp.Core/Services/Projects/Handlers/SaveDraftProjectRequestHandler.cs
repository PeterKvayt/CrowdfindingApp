﻿using System;
using System.Threading.Tasks;
using AutoMapper;
using CrowdfindingApp.Common.Enums;
using CrowdfindingApp.Common.Extensions;
using CrowdfindingApp.Common.Immutable;
using CrowdfindingApp.Common.Messages;
using CrowdfindingApp.Common.Messages.Projects;
using CrowdfindingApp.Data.Common.BusinessModels;
using CrowdfindingApp.Data.Common.Interfaces.Repositories;

namespace CrowdfindingApp.Core.Services.Projects.Handlers
{
    public class SaveDraftProjectRequestHandler : SaveProjectRequestHandlerBase<SaveDraftProjectRequestMessage, ReplyMessageBase>
    {
        public SaveDraftProjectRequestHandler(IMapper mapper,
            IProjectRepository projectRepository,
            IRewardRepository rewardRepository,
            IRewardGeographyRepository rewardGeographyRepository,
            IQuestionRepository questionRepository) : base(mapper, projectRepository, rewardRepository, rewardGeographyRepository, questionRepository)
        {

        }

        protected override async Task<ReplyMessageBase> ValidateRequestMessageAsync(SaveDraftProjectRequestMessage requestMessage)
        {
            var reply = await base.ValidateRequestMessageAsync(requestMessage);

            if((requestMessage.Data?.Id?.IsPresent() ?? false) && !Guid.TryParse(requestMessage.Data.Id, out Guid _))
            {
                return reply.AddObjectNotFoundError();
            }

            if((requestMessage.Data?.CategoryId?.IsPresent() ?? false) && !Guid.TryParse(requestMessage.Data.CategoryId, out Guid _))
            {
                return reply.AddValidationError(CommonErrorMessageKeys.InvalidCategoryId, parameters: requestMessage.Data.CategoryId);
            }

            return reply;
        }

        protected override async Task<ReplyMessageBase> ExecuteAsync(SaveDraftProjectRequestMessage request)
        {
            return await ProcessAsync(request);
        }

        protected override void PrefillValues(Project project, bool isNew)
        {
            base.PrefillValues(project, isNew);
            project.Status = (int)ProjectStatus.Draft;
        }
    }
}
