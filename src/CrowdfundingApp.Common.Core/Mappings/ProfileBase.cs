using AutoMapper;

namespace CrowdfundingApp.Common.Core.Mappings
{
    public abstract class ProfileBase<TDbModel> : Profile
    {
        public ProfileBase()
        {
            CreateMap<TDbModel, TDbModel>();
        }
    }
}
