using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CrowdfundingApp.Common.Core.DataTransfers.Projects;
using CrowdfundingApp.Common.Core.Messages;
using CrowdfundingApp.Common.Core.Messages.Projects;
using CrowdfundingApp.Common.Data.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;

namespace CrowdfundingApp.Core.Services.Projects.Handlers
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
