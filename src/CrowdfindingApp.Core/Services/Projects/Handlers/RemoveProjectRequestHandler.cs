using System;
using System.Threading.Tasks;
using CrowdfindingApp.Common.Enums;
using CrowdfindingApp.Common.Extensions;
using CrowdfindingApp.Common.Handlers;
using CrowdfindingApp.Common.Messages;
using CrowdfindingApp.Common.Messages.Projects;
using CrowdfindingApp.Data.Common.Interfaces.Repositories;

namespace CrowdfindingApp.Core.Services.Projects.Handlers
{
    public class RemoveProjectRequestHandler : NullOperationContextRequestHandler<RemoveProjectRequestMessage, ReplyMessageBase>
    {
        private readonly IProjectRepository _projectRepository;

        public RemoveProjectRequestHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository ?? throw new NullReferenceException(nameof(projectRepository));
        }

        protected override async Task<ReplyMessageBase> ExecuteAsync(RemoveProjectRequestMessage request)
        {
            var isId = Guid.TryParse(request.ProjectId, out var projectId);
            if(request.ProjectId.IsNullOrWhiteSpace() || !isId)
            {
                return new ReplyMessageBase();
            }

            var project = await _projectRepository.GetByIdAsync(projectId);
            if(project is null || project.OwnerId != User.GetUserId())
            {
                return new ReplyMessageBase();
            }

            await _projectRepository.SetStatusAsync((int)ProjectStatus.Deleted, projectId);

            return new ReplyMessageBase();
        }
    }
}
