using AutoMapper;
using CrowdfindingApp.Common.DataTransfers.Orders;
using CrowdfindingApp.Data.Common.Filters;
using CrowdfindingApp.Data.Common.BusinessModels;

namespace CrowdfindingApp.Core.Services.Orders.Mappings
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderFilter, OrderFilterInfo>();

            CreateMap<Order, OrderInfo>().ReverseMap();
        }
    }
}
