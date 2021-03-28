using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CrowdfindingApp.Common.DataTransfers.Project;
using CrowdfindingApp.Common.DataTransfers.Projects;
using CrowdfindingApp.Common.Extensions;
using CrowdfindingApp.Common.Messages;
using CrowdfindingApp.Common.Messages.Projects;
using CrowdfindingApp.Data.Common.Filters;
using CrowdfindingApp.Data.Common.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;

namespace CrowdfindingApp.Core.Services.Projects.Handlers
{
    public class GetSupportedProjectCardsRequestHandler : ProjectCardSearchRequestHandlerBase<GetSupportedProjectCardsRequestMessage>
    {
        public GetSupportedProjectCardsRequestHandler(IProjectRepository projectRepository, IMapper mapper, IRewardRepository rewardRepository, IOrderRepository orderRepository,
            IConfiguration configuration) : base(projectRepository, mapper, rewardRepository, orderRepository, configuration)
        {

        }

        protected override async Task<PagedReplyMessage<List<ProjectCard>>> ExecuteAsync(GetSupportedProjectCardsRequestMessage request)
        {
            var orders = await OrderRepository.GetOrdersAsync(new OrderFilter { UserId = new List<Guid> { User.GetUserId() } });
            if(orders == null)
            {
                return new PagedReplyMessage<List<ProjectCard>> { Value = new List<ProjectCard>(), Paging = request.Paging };
            }

            var rewards = await RewardRepository.GetByIdsAsync(orders.Select(x => x.RewardId).Distinct());
            return await SearchAsync(new ProjectFilterInfo 
            { 
                Id = rewards.Select(x => x.ProjectId.ToString())
                .Distinct()
                .ToList() 
            }, request.Paging);
        }
    }
}
