using System;
using System.Threading.Tasks;
using CrowdfundingApp.Common.Extensions;
using CrowdfundingApp.Common.Core.Handlers;
using CrowdfundingApp.Common.Core.Messages;
using CrowdfundingApp.Common.Core.Messages.Projects;
using CrowdfundingApp.Common.Core.Validators;
using CrowdfundingApp.Common.Data.Interfaces.Repositories;
using CrowdfundingApp.Common.Core.Extensions;

namespace CrowdfundingApp.Core.Services.Projects.Handlers
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
            await _repository.SetStatusAsync((int)request.Status, new Guid[] { projectId });
            return new ReplyMessageBase();
        }
    }
}
