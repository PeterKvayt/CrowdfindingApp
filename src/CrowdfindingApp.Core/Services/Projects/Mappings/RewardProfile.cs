using AutoMapper;
using CrowdfindingApp.Common.DataTransfers.Rewards;
using CrowdfindingApp.Data.Common.BusinessModels;

namespace CrowdfindingApp.Core.Services.Projects.Mappings
{
    public class RewardProfile : Profile
    {
        public RewardProfile()
        {
            CreateMap<RewardInfo, Reward>().ReverseMap();
        }
    }
}
