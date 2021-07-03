using CrowdfindingApp.Common.Core.DataTransfers.Orders;
using CrowdfindingApp.Common.Data.Filters;
using CrowdfindingApp.Common.Data.BusinessModels;
using CrowdfindingApp.Common.Core.Mappings;
using CrowdfindingApp.Common.Core.Messages.Orders;

namespace CrowdfindingApp.Core.Services.Orders.Mappings
{
    public class OrderProfile : ProfileBase<Order>
    {
        public OrderProfile()
        {
            CreateMap<OrderFilter, OrderFilterInfo>();

            CreateMap<Order, OrderInfo>().ReverseMap();

            CreateMap<AcceptOrderRequestMessage, Order>();
        }
    }
}
