﻿using CrowdfindingApp.Common.DataTransfers.Rewards;
using CrowdfindingApp.Common.Mappings;
using CrowdfindingApp.Data.Common.BusinessModels;

namespace CrowdfindingApp.Core.Services.Rewards.Mappings
{
    public class RewardProfile : ProfileBase<Reward>
    {
        public RewardProfile()
        {
            CreateMap<RewardInfo, Reward>().ReverseMap();
        }
    }
}