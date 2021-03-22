using AutoMapper;

namespace CrowdfindingApp.Common.Mappings
{
    public abstract class ProfileBase<TDbModel> : Profile
    {
        public ProfileBase()
        {
            CreateMap<TDbModel, TDbModel>();
        }
    }
}
