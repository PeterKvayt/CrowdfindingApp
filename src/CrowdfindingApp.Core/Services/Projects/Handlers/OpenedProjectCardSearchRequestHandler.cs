using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CrowdfindingApp.Common.DataTransfers.Project;
using CrowdfindingApp.Common.DataTransfers.Projects;
using CrowdfindingApp.Common.Enums;
using CrowdfindingApp.Common.Messages;
using CrowdfindingApp.Common.Messages.Projects;
using CrowdfindingApp.Common.Data.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;

namespace CrowdfindingApp.Core.Services.Projects.Handlers
{
    public class OpenedProjectCardSearchRequestHandler : ProjectCardSearchRequestHandlerBase<ProjectSearchRequestMessage>
    {
        public OpenedProjectCardSearchRequestHandler(IProjectRepository projectRepository, IMapper mapper, IRewardRepository rewardRepository, IOrderRepository orderRepository,
            IConfiguration configuration) : base(projectRepository, mapper, rewardRepository, orderRepository, configuration)
        {
        }

        public List<ProjectStatus> _allowedStatuses = new List<ProjectStatus> { ProjectStatus.Active, ProjectStatus.Complited, ProjectStatus.Finalized };

        protected override async Task<PagedReplyMessage<List<ProjectCard>>> ExecuteAsync(ProjectSearchRequestMessage request)
        {
            return await SearchAsync(GetFilterWithRestrictions(request.Filter), request.Paging);
        }

        private ProjectFilterInfo GetFilterWithRestrictions(ProjectFilterInfo filter)
        {
            filter = filter ?? new ProjectFilterInfo();
            filter.Status = filter.Status?.Where(x => _allowedStatuses.Contains(x)).ToList();
            if(!filter.Status?.Any() ?? true)
            {
                filter.Status = _allowedStatuses;
            }
            return filter;
        }
    }
}
