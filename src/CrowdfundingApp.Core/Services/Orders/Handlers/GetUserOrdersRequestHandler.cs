using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CrowdfundingApp.Common.Core.DataTransfers.Orders;
using CrowdfundingApp.Common.Extensions;
using CrowdfundingApp.Common.Core.Messages;
using CrowdfundingApp.Common.Core.Messages.Orders;
using CrowdfundingApp.Common.Data.Filters;
using CrowdfundingApp.Common.Data.Interfaces.Repositories;

namespace CrowdfundingApp.Core.Services.Orders.Handlers
{
    public class GetUserOrdersRequestHandler : OrderInfoSearchRequestHandlerBase<GetUserOrdersRequestMessage>
    {
        public GetUserOrdersRequestHandler(IMapper mapper, IOrderRepository orderRepository, IRewardRepository rewardRepository,
            IProjectRepository projectRepository, ICountryRepository countryRepository, IRewardGeographyRepository rewardGeographyRepository, IUserRepository userRepository)
            : base(mapper, orderRepository, rewardRepository, projectRepository, countryRepository, rewardGeographyRepository, userRepository)
        {

        }

        protected override async Task<ReplyMessage<List<OrderInfo>>> ExecuteAsync(GetUserOrdersRequestMessage request)
        {
            return await SearchAsync(new OrderFilter { UserId = new List<Guid> { User.GetUserId() } });
        }
    }
}
