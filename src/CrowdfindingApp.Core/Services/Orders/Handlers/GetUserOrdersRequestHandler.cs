using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CrowdfindingApp.Common.Core.DataTransfers.Orders;
using CrowdfindingApp.Common.Extensions;
using CrowdfindingApp.Common.Core.Messages;
using CrowdfindingApp.Common.Core.Messages.Orders;
using CrowdfindingApp.Common.Data.Filters;
using CrowdfindingApp.Common.Data.Interfaces.Repositories;

namespace CrowdfindingApp.Core.Services.Orders.Handlers
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
