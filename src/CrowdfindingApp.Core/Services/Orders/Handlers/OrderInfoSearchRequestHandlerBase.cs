using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CrowdfindingApp.Common.DataTransfers.Orders;
using CrowdfindingApp.Common.Enums;
using CrowdfindingApp.Common.Handlers;
using CrowdfindingApp.Common.Messages;
using CrowdfindingApp.Common.Data.BusinessModels;
using CrowdfindingApp.Common.Data.Filters;
using CrowdfindingApp.Common.Data.Interfaces.Repositories;

namespace CrowdfindingApp.Core.Services.Orders.Handlers
{
    public abstract class OrderInfoSearchRequestHandlerBase<TRequest> : NullOperationContextRequestHandler<TRequest, ReplyMessage<List<OrderInfo>>>
        where TRequest: MessageBase, new()
    {
        protected readonly IOrderRepository OrderRepository;
        protected readonly IMapper Mapper;
        protected readonly IRewardRepository RewardRepository;
        protected readonly IProjectRepository ProjectRepository;
        protected readonly ICountryRepository CountryRepository;
        protected readonly IRewardGeographyRepository RewardGeographyRepository;
        protected readonly IUserRepository UserRepository;

        public OrderInfoSearchRequestHandlerBase(IMapper mapper, IOrderRepository orderRepository, IRewardRepository rewardRepository,
            IProjectRepository projectRepository, ICountryRepository countryRepository, IRewardGeographyRepository rewardGeographyRepository,
            IUserRepository userRepository)
        {
            OrderRepository = orderRepository ?? throw new NullReferenceException(nameof(orderRepository));
            Mapper = mapper ?? throw new NullReferenceException(nameof(mapper));
            RewardRepository = rewardRepository ?? throw new NullReferenceException(nameof(rewardRepository));
            ProjectRepository = projectRepository ?? throw new NullReferenceException(nameof(projectRepository));
            CountryRepository = countryRepository ?? throw new NullReferenceException(nameof(countryRepository));
            RewardGeographyRepository = rewardGeographyRepository ?? throw new NullReferenceException(nameof(rewardGeographyRepository));
            UserRepository = userRepository ?? throw new NullReferenceException(nameof(userRepository));
        }

        protected async Task<ReplyMessage<List<OrderInfo>>> SearchAsync(OrderFilter filter)
        {
            var orders = await OrderRepository.GetOrdersAsync(filter);
            if(orders == null)
            {
                return new ReplyMessage<List<OrderInfo>> { Value = new List<OrderInfo>() };
            }

            var rewards = await RewardRepository.GetByIdsAsync(orders.Select(x => x.RewardId).Distinct());
            var users = await UserRepository.GetUsersAsync(new UserFilter { Id = orders.Select(x => x.UserId).Distinct().ToList() });
            var countries = await CountryRepository.GetByIdsAsync(orders.Where(_ => _.CountryId.HasValue).Select(x => x.CountryId.Value).Distinct());
            var projects = await ProjectRepository.GetProjectsAsync(new ProjectFilter { Id = rewards.Select(x => x.ProjectId).Distinct().ToList() }, null);

            var reply = new ReplyMessage<List<OrderInfo>> { Value = new List<OrderInfo>() };
            foreach(var order in orders) reply.Value.Add(await MapToOrderInfo(order, rewards, countries, projects, users));
            reply.Value = reply.Value.OrderByDescending(x => x.PaymentDateTime).ToList();
            return reply;
        }

        private async Task<OrderInfo> MapToOrderInfo(Order order, List<Reward> rewards, List<Country> countries, List<Project> projects, List<User> users)
        {
            var info = Mapper.Map<OrderInfo>(order);
            var reward = rewards.First(x => x.Id == order.RewardId);
            info.RewardName = reward.Title;
            info.UserEmail = users.First(x => x.Id == order.UserId).Email;

            var project = projects.FirstOrDefault(x => x.Id == reward.ProjectId);
            info.ProjectId = project.Id.ToString();
            info.ProjectName = project.Title;

            if(order.CountryId.HasValue)
            {
                info.CountryName = countries.First(x => x.Id == order.CountryId.Value).Name;
                var deliveries = await RewardGeographyRepository.GetListAsync(new List<Guid> { reward.Id }, new List<Guid> { order.CountryId.Value, Common.Immutable.Data.WholeWorldDelivery });
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
