using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CrowdfindingApp.Common.DataTransfers.Project;
using CrowdfindingApp.Common.DataTransfers.Projects;
using CrowdfindingApp.Common.Enums;
using CrowdfindingApp.Common.Extensions;
using CrowdfindingApp.Common.Messages;
using CrowdfindingApp.Common.Messages.Projects;
using CrowdfindingApp.Data.Common.Filters;
using CrowdfindingApp.Data.Common.Interfaces.Repositories;
using CrowdfindingApp.Data.Common.Models;
using Microsoft.Extensions.Configuration;

namespace CrowdfindingApp.Core.Services.Projects.Handlers
{
    public class OpenedProjectCardSearchRequestHandler : ProjectCardSearchRequestHandlerBase<ProjectSearchRequestMessage>
    {
        public OpenedProjectCardSearchRequestHandler(IProjectRepository projectRepository, IMapper mapper, IRewardRepository rewardRepository, IOrderRepository orderRepository,
            IConfiguration configuration) : base(projectRepository, mapper, rewardRepository, orderRepository, configuration)
        {
        }

        public List<ProjectStatus> _allowedStatuses = new List<ProjectStatus> { ProjectStatus.Active, ProjectStatus.Complited };

        protected override async Task<PagedReplyMessage<List<ProjectCard>>> ExecuteAsync(ProjectSearchRequestMessage request)
        {
            SetRestrictions(request.Filter);
            var filter = Mapper.Map<ProjectFilter>(request.Filter);
            var paging = Mapper.Map<Paging>(request.Paging);

            return await SearchAsync(filter, paging);
        }

        private void SetRestrictions(ProjectFilterInfo filter)
        {
            filter = filter ?? new ProjectFilterInfo();
            filter.Status = filter.Status?.Where(x => _allowedStatuses.Contains(x)).ToList();
            if(!filter.Status?.Any() ?? true)
            {
                filter.Status = _allowedStatuses;
            }
        }
    }
}
