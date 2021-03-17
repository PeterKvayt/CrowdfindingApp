using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CrowdfindingApp.Common.DataTransfers.Users;
using CrowdfindingApp.Common.Messages.Users;
using CrowdfindingApp.Data.Common.BusinessModels;

namespace CrowdfindingApp.Core.Services.Users
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserInfo>()
                .ForMember(dest => dest.Photo, opt => opt.MapFrom(source => source.Image))
                .ForMember(dest => dest.Role, opt => opt.MapFrom((src, dest, destMember, ctx) => GetRoleName(src.RoleId, ctx.Items.ToDictionary(_ => _.Key, _ => _.Value.ToString()))));

            CreateMap<UpdateUserRequestMessage, User>();

        }

        private string GetRoleName(Guid guid, IDictionary<string, string> roles)
        {
            return roles.FirstOrDefault(_ => _.Key == guid.ToString()).Value
                ?? throw new KeyNotFoundException($"There is no role with id {guid}");
        }
    }
}
