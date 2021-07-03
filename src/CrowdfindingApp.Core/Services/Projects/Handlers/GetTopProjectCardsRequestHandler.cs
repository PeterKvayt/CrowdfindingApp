using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CrowdfindingApp.Common.Core.DataTransfers.Projects;
using CrowdfindingApp.Common.Enums;
using CrowdfindingApp.Common.Core.Messages;
using CrowdfindingApp.Common.Core.Messages.Projects;
using CrowdfindingApp.Common.Data.Filters;
using CrowdfindingApp.Common.Data.Interfaces.Repositories;
using CrowdfindingApp.Common.Data.Models;
using Microsoft.Extensions.Configuration;

namespace CrowdfindingApp.Core.Services.Projects.Handlers
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
