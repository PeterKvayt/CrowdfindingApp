using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CrowdfindingApp.Common.DataTransfers.Projects;
using CrowdfindingApp.Common.Messages;
using CrowdfindingApp.Common.Messages.Projects;
using CrowdfindingApp.Common.Data.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;

namespace CrowdfindingApp.Core.Services.Projects.Handlers
{
    public class UnsafeProjectCardSearchRequestHandler : ProjectCardSearchRequestHandlerBase<ProjectSearchRequestMessage>
    {
        public UnsafeProjectCardSearchRequestHandler(IProjectRepository projectRepository, IMapper mapper, IRewardRepository rewardRepository, IOrderRepository orderRepository,
            IConfiguration configuration) : base(projectRepository, mapper, rewardRepository, orderRepository, configuration)
        {
        }

        protected override async Task<PagedReplyMessage<List<ProjectCard>>> ExecuteAsync(ProjectSearchRequestMessage request)
        {
            return await SearchAsync(request.Filter, request.Paging);
        }
    }
}
