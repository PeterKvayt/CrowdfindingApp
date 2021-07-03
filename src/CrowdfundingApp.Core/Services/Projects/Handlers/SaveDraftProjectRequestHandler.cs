using System;
using System.Threading.Tasks;
using AutoMapper;
using CrowdfundingApp.Common.Enums;
using CrowdfundingApp.Common.Extensions;
using CrowdfundingApp.Common.Immutable;
using CrowdfundingApp.Common.Core.Maintainers.FileStorageProvider;
using CrowdfundingApp.Common.Core.Messages;
using CrowdfundingApp.Common.Core.Messages.Projects;
using CrowdfundingApp.Common.Data.BusinessModels;
using CrowdfundingApp.Common.Data.Interfaces.Repositories;

namespace CrowdfundingApp.Core.Services.Projects.Handlers
{
    public class SaveDraftProjectRequestHandler : SaveProjectRequestHandlerBase<SaveDraftProjectRequestMessage, ReplyMessage<string>>
    {
        public SaveDraftProjectRequestHandler(IMapper mapper,
            IProjectRepository projectRepository,
            IRewardRepository rewardRepository,
            IRewardGeographyRepository rewardGeographyRepository,
            IFileStorage fileStorage,
            IQuestionRepository questionRepository) : base(mapper, projectRepository, rewardRepository, rewardGeographyRepository, fileStorage, questionRepository)
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

        protected override async Task<ReplyMessage<string>> ExecuteAsync(SaveDraftProjectRequestMessage request)
        {
            return await ProcessAsync(request);
        }

        protected override void PrepareValues(Project project, bool isNew)
        {
            base.PrepareValues(project, isNew);
            project.Status = (int)ProjectStatus.Draft;
        }
    }
}
