using System;
using System.Threading.Tasks;
using AutoMapper;
using CrowdfindingApp.Common.Extensions;
using CrowdfindingApp.Common.Handlers;
using CrowdfindingApp.Common.Immutable;
using CrowdfindingApp.Common.Messages;
using CrowdfindingApp.Common.Messages.Projects;
using CrowdfindingApp.Data.Common.BusinessModels;
using CrowdfindingApp.Data.Common.Interfaces.Repositories;

namespace CrowdfindingApp.Core.Services.Projects.Handlers
{
    public class SaveDraftProjectRequestHandler : NullOperationContextRequestHandler<SaveDraftProjectRequestMessage, ReplyMessageBase>
    {
        private readonly IMapper _mapper;
        private readonly IProjectRepository _repository;

        public SaveDraftProjectRequestHandler(IMapper mapper, IProjectRepository repository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
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
            if(request.Data == null)
            {
                return new ReplyMessageBase();
            }

            var isExistedDraft = request.Data.Id.IsPresent();
            var model = new Project();
            if(isExistedDraft)
            {
                model = await _repository.GetById(new Guid(request.Data.Id));
            }

            _mapper.Map(request.Data, model);

            model.LastModifiedDateTime = DateTime.UtcNow;
            model.IsSketch = true;
            model.OwnerId = User.GetUserId();

            if(isExistedDraft)
            {
                await _repository.Update(model);
            }
            else
            {
                await _repository.Add(model);
            }

            return new ReplyMessageBase();
        }
    }
}
