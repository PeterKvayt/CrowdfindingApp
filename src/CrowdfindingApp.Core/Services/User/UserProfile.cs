using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CrowdfindingApp.Common.DataTransfers.User;
using CrowdfindingApp.Core.Interfaces.Data.Repositories;

namespace CrowdfindingApp.Core.Services.User
{
    public class UserProfile : Profile
    {
        private IDictionary<string, string> _cachedRoles;

        public UserProfile()
        {
            CreateMap<Models.User, UserInfo>()
                .BeforeMap((src, dest, opt) => _cachedRoles = opt.Items.ToDictionary(_ => _.Key, _ => _.Value.ToString()))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(x => GetRoleName(x.RoleId)));
        }

        private string GetRoleName(Guid guid)
        {
            return _cachedRoles.FirstOrDefault(_ => _.Key == guid.ToString()).Value
                ?? throw new KeyNotFoundException($"There is no role with id {guid}");
        }
    }
}
