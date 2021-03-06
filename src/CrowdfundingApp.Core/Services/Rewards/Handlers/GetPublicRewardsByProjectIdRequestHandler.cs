﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CrowdfundingApp.Common.Core.DataTransfers.Rewards;
using CrowdfundingApp.Common.Enums;
using CrowdfundingApp.Common.Extensions;
using CrowdfundingApp.Common.Core.Messages;
using CrowdfundingApp.Common.Core.Messages.Rewards;
using CrowdfundingApp.Common.Core.Validators;
using CrowdfundingApp.Common.Data.BusinessModels;
using CrowdfundingApp.Common.Data.Filters;
using CrowdfundingApp.Common.Data.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Schema;
using CrowdfundingApp.Common.Core.Extensions;

namespace CrowdfundingApp.Core.Services.Rewards.Handlers
{
    public class GetPublicRewardsByProjectIdRequestHandler : UnsafeRewardsSearchRequestHandler<GetPublicRewardsByProjectIdRequestMessage>
    {
        private readonly IProjectRepository _projectRepository;

        public GetPublicRewardsByProjectIdRequestHandler(IMapper mapper,
            IRewardRepository rewardRepository,
            IRewardGeographyRepository rewardGeographyRepository,
            IConfiguration configuration,
            IOrderRepository orderRepository,
            IProjectRepository projectRepository
            ) : base(mapper, rewardRepository, rewardGeographyRepository, configuration, orderRepository)
        {
            _projectRepository = projectRepository ?? throw new NullReferenceException(nameof(projectRepository));

        }

        protected override async Task<ReplyMessageBase> ValidateRequestMessageAsync(GetPublicRewardsByProjectIdRequestMessage requestMessage)
        {
            var reply = await base.ValidateRequestMessageAsync(requestMessage);
            var validator = new IdValidator();
            await reply.MergeAsync(await validator.ValidateAsync(requestMessage.ProjectId));
            if(reply.Errors?.Any() ?? false)
            {
                return reply;
            }
            var project = await _projectRepository.GetByIdAsync(new Guid(requestMessage.ProjectId));
            if(project == null || (project.Status != (int)ProjectStatus.Active && project.Status != (int)ProjectStatus.Complited))
            {
                reply.AddObjectNotFoundError();
                return reply;
            }

            return reply;
        }

        protected override async Task<ReplyMessage<List<RewardInfo>>> ExecuteAsync(GetPublicRewardsByProjectIdRequestMessage request)
        {
            var rewards = await RewardRepository.GetRewardsByProjectIdAsync(new Guid(request.ProjectId));
            var rewardInfos = new List<RewardInfo>();
            foreach(var reward in rewards)
            {
                var info = MapToRewardInfo(reward);
                var orders = await OrderRepository.GetOrdersAsync(new OrderFilter { RewardId = new List<Guid> { reward.Id } });
                if(orders != null)
                {
                    info.Limit -= orders.Sum(x => x.Count);
                }
                rewardInfos.Add(info);
            }

            return new ReplyMessage<List<RewardInfo>> { Value = rewardInfos };
        }

        private RewardInfo MapToRewardInfo(Reward reward)
        {
            var info = Mapper.Map<RewardInfo>(reward);
            PrepareProjectImage(info);
            return info;
        }

        private void PrepareProjectImage(RewardInfo reward)
        {
            if(reward.Image.IsNullOrWhiteSpace())
            {
                return;
            }
            reward.Image = $"{Configuration["FileStorageConfiguration:PermanentFolderName"]}/Projects/{reward.ProjectId}/{reward.Image}";
        }
    }
}
