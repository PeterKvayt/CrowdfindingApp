using System;
using AutoMapper;
using CrowdfundingApp.Common.Core.DataTransfers;
using CrowdfundingApp.Common.Extensions;
using CrowdfundingApp.Common.Data.Models;

namespace CrowdfundingApp.Core.Mapping
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
