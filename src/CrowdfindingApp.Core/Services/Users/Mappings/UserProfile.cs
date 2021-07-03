using System;
using System.Collections.Generic;
using System.Linq;
using CrowdfindingApp.Common.DataTransfers.Users;
using CrowdfindingApp.Common.Mappings;
using CrowdfindingApp.Common.Messages.Users;
using CrowdfindingApp.Common.Data.BusinessModels;

namespace CrowdfindingApp.Core.Services.Users.Mappings
{
    public class UserProfile : ProfileBase<User>
    {
        public UserProfile()
        {
            CreateMap<User, UserInfo>()
                .ForMember(dest => dest.Photo, opt => opt.MapFrom(source => source.Image))
                .ForMember(dest => dest.Role, opt => opt.MapFrom((src, dest, destMember, ctx) => GetRoleName(src.RoleId, ctx.Items.ToDictionary(_ => _.Key, _ => _.Value.ToString()))));

            CreateMap<UpdateUserRequestMessage, User>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        }

        private string GetRoleName(Guid guid, IDictionary<string, string> roles)
        {
            return roles.FirstOrDefault(_ => _.Key == guid.ToString()).Value
                ?? throw new KeyNotFoundException($"There is no role with id {guid}");
        }
    }
}
