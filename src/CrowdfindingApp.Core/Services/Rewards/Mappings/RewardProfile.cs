using CrowdfindingApp.Common.Core.DataTransfers.Rewards;
using CrowdfindingApp.Common.Core.Mappings;
using CrowdfindingApp.Common.Data.BusinessModels;

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
