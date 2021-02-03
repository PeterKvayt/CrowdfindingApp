using System;
using AutoMapper;
using CrowdfindingApp.Common.Extensions;

namespace CrowdfindingApp.Common
{
    public class CommonProfile : Profile
    {
        public CommonProfile()
        {
            CreateMap<string, Guid>()
                .ConstructUsing(x => x.IsNullOrEmpty() ? Guid.Empty : new Guid(x))
                .ReverseMap()
                .ConstructUsing(x => x.ToString());
        }
    }
}
