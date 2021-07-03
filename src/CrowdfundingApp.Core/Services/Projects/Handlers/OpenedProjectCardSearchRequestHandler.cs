using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CrowdfundingApp.Common.Core.DataTransfers.Project;
using CrowdfundingApp.Common.Core.DataTransfers.Projects;
using CrowdfundingApp.Common.Enums;
using CrowdfundingApp.Common.Core.Messages;
using CrowdfundingApp.Common.Core.Messages.Projects;
using CrowdfundingApp.Common.Data.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;

namespace CrowdfundingApp.Core.Services.Projects.Handlers
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
