using System;
using AutoMapper;
using CrowdfindingApp.Common.DataTransfers;
using CrowdfindingApp.Common.Extensions;
using CrowdfindingApp.Common.Data.Models;

namespace CrowdfindingApp.Common.Mappings
{
    public class CommonProfile : Profile
    {
        public CommonProfile()
        {
            CreateMap<string, Guid>()
                .ConstructUsing(x => x.IsNullOrEmpty() ? Guid.Empty : new Guid(x))
                .ReverseMap()
                .ConstructUsing(x => x == Guid.Empty ? null : x.ToString());

            CreateMap<PagingInfo, Paging>().ReverseMap();
        }
    }
}
