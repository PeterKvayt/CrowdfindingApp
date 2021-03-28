using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CrowdfindingApp.Common.DataTransfers.Orders;
using CrowdfindingApp.Common.Extensions;
using CrowdfindingApp.Common.Messages;
using CrowdfindingApp.Common.Messages.Orders;
using CrowdfindingApp.Common.Validators;
using CrowdfindingApp.Data.Common.Filters;
using CrowdfindingApp.Data.Common.Interfaces.Repositories;
namespace CrowdfindingApp.Core.Services.Orders.Handlers
{
    public class GetProjectOrdersRequestHandler : OrderInfoSearchRequestHandlerBase<GetProjectOrdersRequestMessage>
    {
        public GetProjectOrdersRequestHandler(IMapper mapper, IOrderRepository orderRepository, IRewardRepository rewardRepository,
            IProjectRepository projectRepository, ICountryRepository countryRepository, IRewardGeographyRepository rewardGeographyRepository, IUserRepository userRepository)
            : base(mapper, orderRepository, rewardRepository, projectRepository, countryRepository, rewardGeographyRepository, userRepository)
        {

        }

        protected override async Task<ReplyMessageBase> ValidateRequestMessageAsync(GetProjectOrdersRequestMessage requestMessage)
        {
            var reply = await base.ValidateRequestMessageAsync(requestMessage);
            var validator = new IdValidator();
            var result = await validator.ValidateAsync(requestMessage.Id);
            await reply.MergeAsync(result);

            return reply;
        }

        protected override async Task<ReplyMessage<List<OrderInfo>>> ExecuteAsync(GetProjectOrdersRequestMessage request)
        {
            var reply = new ReplyMessage<List<OrderInfo>> { Value = new List<OrderInfo>() };
            var project = await ProjectRepository.GetByIdAsync(new Guid(request.Id));
            if(project == null)
            {
                reply.AddObjectNotFoundError();
                return reply;
            }

            var rewards = await RewardRepository.GetRewardsByProjectIdAsync(project.Id);
            if(!rewards?.Any() ?? true)
            {
                reply.AddObjectNotFoundError();
                return reply;
            }

            return await SearchAsync(new OrderFilter { RewardId = rewards.Select(x => x.Id).ToList() });
        }
    }
}
