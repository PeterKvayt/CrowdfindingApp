using AutoMapper;

namespace CrowdfindingApp.Common.Core.Mappings
{
    public abstract class ProfileBase<TDbModel> : Profile
    {
        public ProfileBase()
        {
            CreateMap<TDbModel, TDbModel>();
        }
    }
}
