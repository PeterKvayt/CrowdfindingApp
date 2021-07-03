using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CrowdfundingApp.Common.Core.DataTransfers.Rewards;
using CrowdfundingApp.Common.Enums;
using CrowdfundingApp.Common.Extensions;
using CrowdfundingApp.Common.Core.Handlers;
using CrowdfundingApp.Common.Core.Messages;
using CrowdfundingApp.Common.Core.Messages.Rewards;
using CrowdfundingApp.Common.Core.Validators;
using CrowdfundingApp.Common.Data.BusinessModels;
using CrowdfundingApp.Common.Data.Filters;
using CrowdfundingApp.Common.Data.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using CrowdfundingApp.Common.Core.Extensions;
using CrowdfundingApp.Common.Core.DataTransfers;

namespace CrowdfundingApp.Core.Services.Rewards.Handlers
{
    public class GetPublicRewardByIdRequestHandler : RequestHandlerBase<GetPublicRewardByIdRequestMessage, ReplyMessage<RewardInfo>, Reward>
    {
        private readonly IMapper _mapper;
        private readonly IRewardRepository _rewardRepository;
        private readonly IRewardGeographyRepository _rewardGeographyRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IConfiguration _configuration;

        public GetPublicRewardByIdRequestHandler(IMapper mapper,
            IRewardRepository rewardRepository,
            IRewardGeographyRepository rewardGeographyRepository,
            IConfiguration configuration,
            IProjectRepository projectRepository,
            IOrderRepository orderRepository)
        {
            _mapper = mapper ?? throw new NullReferenceException(nameof(mapper));
            _rewardRepository = rewardRepository ?? throw new NullReferenceException(nameof(rewardRepository));
            _orderRepository = orderRepository ?? throw new NullReferenceException(nameof(orderRepository));
            _configuration = configuration ?? throw new NullReferenceException(nameof(configuration));
            _rewardGeographyRepository = rewardGeographyRepository ?? throw new NullReferenceException(nameof(rewardGeographyRepository));
            _projectRepository = projectRepository ?? throw new NullReferenceException(nameof(projectRepository));
        }

        protected override async Task<(ReplyMessageBase, Reward)> ValidateRequestMessageAsync(GetPublicRewardByIdRequestMessage requestMessage)
        {
            var (reply, reward) = await base.ValidateRequestMessageAsync(requestMessage);
            var validator = new IdValidator();
            await reply.MergeAsync(await validator.ValidateAsync(requestMessage.Id));
            if(reply.Errors?.Any() ?? false)
            {
                return (reply, reward);
            }

            reward = await _rewardRepository.GetByIdAsync(new Guid(requestMessage.Id));
            if(reward == null)
            {
                reply.AddObjectNotFoundError();
                return (reply, reward);
            }

            var project = await _projectRepository.GetByIdAsync(reward.ProjectId);
            if(project == null || (project.Status != (int)ProjectStatus.Active && project.Status != (int)ProjectStatus.Complited))
            {
                reply.AddObjectNotFoundError();
                return (reply, reward);
            }

            return (reply, reward);
        }

        protected override async Task<ReplyMessage<RewardInfo>> ExecuteAsync(GetPublicRewardByIdRequestMessage request, Reward reward)
        {
            var info = MapToRewardInfo(reward);

            var orders = await _orderRepository.GetOrdersAsync(new OrderFilter { RewardId = new List<Guid> { reward.Id } });
            if(orders != null)
            {
                info.Limit -= orders.Sum(x => x.Count);
            }

            var deliveryCountries = await _rewardGeographyRepository.GetByRewardIdAsync(reward.Id);
            info.DeliveryCountries = deliveryCountries.Select(x => new KeyValue<string, decimal?>(x.CountryId.ToString(), x.Price)).ToList();

            return new ReplyMessage<RewardInfo> { Value = info };
        }

        private RewardInfo MapToRewardInfo(Reward reward)
        {
            var info = _mapper.Map<RewardInfo>(reward);
            PrepareProjectImage(info);
            return info;
        }

        private void PrepareProjectImage(RewardInfo reward)
        {
            if(reward.Image.IsNullOrWhiteSpace())
            {
                return;
            }
            reward.Image = $"{_configuration["FileStorageConfiguration:PermanentFolderName"]}/Projects/{reward.ProjectId}/{reward.Image}";
        }
    }
}
