using System;
using System.Threading.Tasks;
using CrowdfindingApp.Common.Extensions;
using CrowdfindingApp.Common.Handlers;
using CrowdfindingApp.Common.Messages;
using CrowdfindingApp.Common.Messages.Projects;
using CrowdfindingApp.Core.Services.Projects.Validators;
using CrowdfindingApp.Data.Common.Interfaces.Repositories;

namespace CrowdfindingApp.Core.Services.Projects.Handlers
{
    public class SetProjectStatusRequestHandler : RequestHandlerBase<SetProjectStatusRequestMessage, ReplyMessageBase, Guid>
    {
        private readonly IProjectRepository _repository;

        public SetProjectStatusRequestHandler(IProjectRepository repository)
        {
            _repository = repository ?? throw new NullReferenceException(nameof(repository));
        }

        protected override async Task<(ReplyMessageBase, Guid)> ValidateRequestMessageAsync(SetProjectStatusRequestMessage requestMessage)
        {
            var (reply, projectId) = await base.ValidateRequestMessageAsync(requestMessage);
            var validator = new IdValidator();
            var idValidationResult = await validator.ValidateAsync(requestMessage.ProjectId);
            await reply.MergeAsync(idValidationResult);

            if(!idValidationResult.IsValid)
            {
                return (reply, projectId);
            }

            projectId = new Guid(requestMessage.ProjectId);
            var project = await _repository.GetByIdAsync(projectId);
            if(project == null)
            {
                reply.AddObjectNotFoundError();
            }

            return (reply, projectId);
        }

        protected override async Task<ReplyMessageBase> ExecuteAsync(SetProjectStatusRequestMessage request, Guid projectId)
        {
            await _repository.SetStatus((int)request.Status, projectId);
            return new ReplyMessageBase();
        }
    }
}
