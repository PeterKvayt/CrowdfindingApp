using CrowdfundingApp.Common.Core.DataTransfers.Orders;
using CrowdfundingApp.Common.Data.Filters;
using CrowdfundingApp.Common.Data.BusinessModels;
using CrowdfundingApp.Common.Core.Mappings;
using CrowdfundingApp.Common.Core.Messages.Orders;

namespace CrowdfundingApp.Core.Services.Orders.Mappings
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
