using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CrowdfundingApp.Common.Core.DataTransfers.Rewards;
using CrowdfundingApp.Common.Core.Handlers;
using CrowdfundingApp.Common.Core.Messages;
using CrowdfundingApp.Common.Data.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;

namespace CrowdfundingApp.Core.Services.Rewards.Handlers
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
