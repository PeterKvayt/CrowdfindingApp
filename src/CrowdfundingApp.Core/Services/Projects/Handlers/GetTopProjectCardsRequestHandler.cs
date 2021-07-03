using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CrowdfundingApp.Common.Core.DataTransfers.Projects;
using CrowdfundingApp.Common.Enums;
using CrowdfundingApp.Common.Core.Messages;
using CrowdfundingApp.Common.Core.Messages.Projects;
using CrowdfundingApp.Common.Data.Filters;
using CrowdfundingApp.Common.Data.Interfaces.Repositories;
using CrowdfundingApp.Common.Data.Models;
using Microsoft.Extensions.Configuration;

namespace CrowdfundingApp.Core.Services.Projects.Handlers
{
    public class GetTopProjectCardsRequestHandler : ProjectCardSearchRequestHandlerBase<GetTopProjectCardsRequestMessage>
    {
        public GetTopProjectCardsRequestHandler(IProjectRepository projectRepository, IMapper mapper, IRewardRepository rewardRepository, IOrderRepository orderRepository,
            IConfiguration configuration) : base(projectRepository, mapper, rewardRepository, orderRepository, configuration)
        {
        }

        protected override async Task<PagedReplyMessage<List<ProjectCard>>> ExecuteAsync(GetTopProjectCardsRequestMessage request)
        {
            var cards = await SearchAsync(new ProjectFilter
            {
                Status = new List<int> { (int)ProjectStatus.Active, (int)ProjectStatus.Complited },
                OrderBy = x => x.StartDateTime,
                DescendingOrder = false
            }, Mapper.Map<Paging>(request.Paging));

            return new PagedReplyMessage<List<ProjectCard>>
            {
                Value = cards.Value,
                Paging = request.Paging
            };
        }
    }
}
