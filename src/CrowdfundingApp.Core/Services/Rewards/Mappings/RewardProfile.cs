using CrowdfundingApp.Common.Core.DataTransfers.Rewards;
using CrowdfundingApp.Common.Core.Mappings;
using CrowdfundingApp.Common.Data.BusinessModels;

namespace CrowdfundingApp.Core.Services.Rewards.Mappings
{
    public class RewardProfile : ProfileBase<Reward>
    {
        public RewardProfile()
        {
            CreateMap<RewardInfo, Reward>().ReverseMap();
        }
    }
}
