using System;
using System.Threading.Tasks;
using CrowdfindingApp.Common.DataTransfers.Orders;
using CrowdfindingApp.Common.Handlers;
using CrowdfindingApp.Common.Messages;
using CrowdfindingApp.Common.Messages.Orders;
using CrowdfindingApp.Data.Common.Interfaces.Repositories;

namespace CrowdfindingApp.Core.Services.Orders.Handlers
{
    public class OrderSearchRequestHandler : NullOperationContextRequestHandler<OrderSearchRequestMessage, ReplyMessage<OrderInfo>>
    {
        private readonly IOrderRepository _repository;

        public OrderSearchRequestHandler(IOrderRepository repository)
        {
            _repository = repository ?? throw new NullReferenceException(nameof(repository));
        }

        protected override Task<ReplyMessage<OrderInfo>> ExecuteAsync(OrderSearchRequestMessage request)
        {
            throw new NotImplementedException();
        }
    }
}
