using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CrowdfindingApp.Common.DataTransfers.Rewards;
using CrowdfindingApp.Common.Enums;
using CrowdfindingApp.Common.Extensions;
using CrowdfindingApp.Common.Messages;
using CrowdfindingApp.Common.Messages.Rewards;
using CrowdfindingApp.Common.Validators;
using CrowdfindingApp.Data.Common.BusinessModels;
using CrowdfindingApp.Data.Common.Filters;
using CrowdfindingApp.Data.Common.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;

namespace CrowdfindingApp.Core.Services.Rewards.Handlers
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
            if(project == null || project.Status != (int)ProjectStatus.Active)
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
