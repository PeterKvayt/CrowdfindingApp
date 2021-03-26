using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CrowdfindingApp.Common.DataTransfers.Orders;
using CrowdfindingApp.Common.Enums;
using CrowdfindingApp.Common.Extensions;
using CrowdfindingApp.Common.Handlers;
using CrowdfindingApp.Common.Immutable;
using CrowdfindingApp.Common.Messages;
using CrowdfindingApp.Common.Messages.Orders;
using CrowdfindingApp.Data.Common.BusinessModels;
using CrowdfindingApp.Data.Common.Filters;
using CrowdfindingApp.Data.Common.Interfaces.Repositories;

namespace CrowdfindingApp.Core.Services.Orders.Handlers
{
    public class GetUserOrdersRequestHandler : NullOperationContextRequestHandler<GetUserOrdersRequestMessage, ReplyMessage<List<OrderInfo>>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IRewardRepository _rewardRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IRewardGeographyRepository _rewardGeographyRepository;

        public GetUserOrdersRequestHandler(IMapper mapper, IOrderRepository orderRepository, IRewardRepository rewardRepository, 
            IProjectRepository projectRepository, ICountryRepository countryRepository, IRewardGeographyRepository rewardGeographyRepository)
        {
            _orderRepository = orderRepository ?? throw new NullReferenceException(nameof(orderRepository));
            _mapper = mapper ?? throw new NullReferenceException(nameof(mapper));
            _rewardRepository = rewardRepository ?? throw new NullReferenceException(nameof(rewardRepository));
            _projectRepository = projectRepository ?? throw new NullReferenceException(nameof(projectRepository));
            _countryRepository = countryRepository ?? throw new NullReferenceException(nameof(countryRepository));
            _rewardGeographyRepository = rewardGeographyRepository ?? throw new NullReferenceException(nameof(rewardGeographyRepository));
        }

        protected override async Task<ReplyMessage<List<OrderInfo>>> ExecuteAsync(GetUserOrdersRequestMessage request)
        {
            var orders = await _orderRepository.GetOrdersAsync(new OrderFilter { UserId = new List<Guid> { User.GetUserId() } });
            if(orders == null)
            {
                return new ReplyMessage<List<OrderInfo>> { Value = new List<OrderInfo>() };
            }

            var rewards = await _rewardRepository.GetByIdsAsync(orders.Select(x => x.RewardId).Distinct());
            var countries = await _countryRepository.GetByIdsAsync(orders.Where(_ => _.CountryId.HasValue).Select(x => x.CountryId.Value).Distinct());
            var projects = await _projectRepository.GetProjectsAsync(new ProjectFilter { Id = rewards.Select(x => x.ProjectId).Distinct().ToList() }, null);
            
            var reply = new ReplyMessage<List<OrderInfo>> { Value = new List<OrderInfo>() };
            foreach(var order in orders) reply.Value.Add(await MapToOrderInfo(order, rewards, countries, projects));
            reply.Value = reply.Value.OrderByDescending(x => x.PaymentDateTime).ToList();
            return reply;
        }

        private async Task<OrderInfo> MapToOrderInfo(Order order, List<Reward> rewards, List<Country> countries, List<Project> projects)
        {
            var info = _mapper.Map<OrderInfo>(order);
            var reward = rewards.First(x => x.Id == order.RewardId);
            info.RewardName = reward.Title;

            var project = projects.FirstOrDefault(x => x.Id == reward.ProjectId);
            info.ProjectId = project.Id.ToString();
            info.ProjectName = project.Title;

            if(order.CountryId.HasValue)
            {
                info.CountryName = countries.First(x => x.Id == order.CountryId.Value).Name;
                var deliveries = await _rewardGeographyRepository.GetListAsync(new List<Guid> { reward.Id }, new List<Guid> { order.CountryId.Value, Common.Immutable.Data.WholeWorldDelivery });
                if(reward.DeliveryType == (int)DeliveryType.WholeWorld)
                {
                    info.DeliveryCost = deliveries.First(x => x.CountryId == Common.Immutable.Data.WholeWorldDelivery).Price;
                }
                else
                {
                    info.DeliveryCost = deliveries.First(x => x.CountryId == order.CountryId.Value).Price;
                }
            }

            info.Total = order.Count * reward.Price.Value + info.DeliveryCost;

            return info;
        }
    }
}
