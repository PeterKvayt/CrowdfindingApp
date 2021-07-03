using System;
using AutoMapper;
using CrowdfindingApp.Common.Core.DataTransfers;
using CrowdfindingApp.Common.Extensions;
using CrowdfindingApp.Common.Data.Models;

namespace CrowdfindingApp.Common.Core.Mappings
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
