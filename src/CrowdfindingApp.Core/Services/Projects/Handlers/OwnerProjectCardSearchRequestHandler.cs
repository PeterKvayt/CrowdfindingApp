using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CrowdfindingApp.Common.DataTransfers.Project;
using CrowdfindingApp.Common.DataTransfers.Projects;
using CrowdfindingApp.Common.Extensions;
using CrowdfindingApp.Common.Messages;
using CrowdfindingApp.Common.Messages.Projects;
using CrowdfindingApp.Common.Data.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;

namespace CrowdfindingApp.Core.Services.Projects.Handlers
{
    public class OwnerProjectCardSearchRequestHandler : ProjectCardSearchRequestHandlerBase<ProjectSearchRequestMessage>
    {
        public OwnerProjectCardSearchRequestHandler(IProjectRepository projectRepository, IMapper mapper, IRewardRepository rewardRepository, IOrderRepository orderRepository,
            IConfiguration configuration) : base(projectRepository, mapper, rewardRepository, orderRepository, configuration)
        {
        }

        protected override async Task<PagedReplyMessage<List<ProjectCard>>> ExecuteAsync(ProjectSearchRequestMessage request)
        {
            return await SearchAsync(GetFilterWithRestrictions(request.Filter), request.Paging);
        }

        private ProjectFilterInfo GetFilterWithRestrictions(ProjectFilterInfo filter)
        {
            filter = filter ?? new ProjectFilterInfo();
            filter.OwnerId = new List<string> { User.GetUserId().ToString() };
            return filter;
        }
    }
}
