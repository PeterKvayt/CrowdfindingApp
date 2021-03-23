using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CrowdfindingApp.Common.DataTransfers.Rewards;
using CrowdfindingApp.Common.Handlers;
using CrowdfindingApp.Common.Messages;
using CrowdfindingApp.Data.Common.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;

namespace CrowdfindingApp.Core.Services.Rewards.Handlers
{
    public abstract class UnsafeRewardsSearchRequestHandler<TRequest> : NullOperationContextRequestHandler<TRequest, ReplyMessage<List<RewardInfo>>>
        where TRequest : MessageBase, new()
    {
        protected readonly IMapper Mapper;
        protected readonly IRewardRepository RewardRepository;
        protected readonly IRewardGeographyRepository RewardGeographyRepository;
        protected readonly IOrderRepository OrderRepository;
        protected readonly IConfiguration Configuration;

        public UnsafeRewardsSearchRequestHandler(IMapper mapper, 
            IRewardRepository rewardRepository, 
            IRewardGeographyRepository rewardGeographyRepository,
            IConfiguration configuration,
            IOrderRepository orderRepository
            )
        {
            Mapper = mapper ?? throw new NullReferenceException(nameof(mapper));
            RewardRepository = rewardRepository ?? throw new NullReferenceException(nameof(rewardRepository));
            OrderRepository = orderRepository ?? throw new NullReferenceException(nameof(orderRepository));
            Configuration = configuration ?? throw new NullReferenceException(nameof(configuration));
            RewardGeographyRepository = rewardGeographyRepository ?? throw new NullReferenceException(nameof(rewardGeographyRepository));
        }

        protected async Task<List<RewardInfo>> SearchAsync()
        {
            return null;
        }
    }
}
