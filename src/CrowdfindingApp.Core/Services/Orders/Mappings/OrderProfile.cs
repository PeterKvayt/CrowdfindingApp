using CrowdfindingApp.Common.DataTransfers.Orders;
using CrowdfindingApp.Data.Common.Filters;
using CrowdfindingApp.Data.Common.BusinessModels;
using CrowdfindingApp.Common.Mappings;

namespace CrowdfindingApp.Core.Services.Orders.Mappings
{
    public class OrderProfile : ProfileBase<Order>
    {
        public OrderProfile()
        {
            CreateMap<OrderFilter, OrderFilterInfo>();

            CreateMap<Order, OrderInfo>().ReverseMap();
        }
    }
}
