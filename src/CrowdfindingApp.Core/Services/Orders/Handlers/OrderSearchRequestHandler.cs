using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CrowdfindingApp.Common.DataTransfers.Orders;
using CrowdfindingApp.Common.Handlers;
using CrowdfindingApp.Common.Messages;
using CrowdfindingApp.Common.Messages.Orders;
using CrowdfindingApp.Data.Common.Filters;
using CrowdfindingApp.Data.Common.Interfaces.Repositories;
using CrowdfindingApp.Data.Common.Models;

namespace CrowdfindingApp.Core.Services.Orders.Handlers
{
    public class OrderSearchRequestHandler : NullOperationContextRequestHandler<OrderSearchRequestMessage, ReplyMessage<List<OrderInfo>>>
    {
        private readonly IOrderRepository _repository;
        private readonly IMapper _mapper;

        public OrderSearchRequestHandler(IOrderRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new NullReferenceException(nameof(repository));
            _mapper = mapper ?? throw new NullReferenceException(nameof(mapper));
        }

        protected override async Task<ReplyMessage<List<OrderInfo>>> ExecuteAsync(OrderSearchRequestMessage request)
        {
            var filter = _mapper.Map<OrderFilter>(request.Filter);
            var paging = _mapper.Map<Paging>(request.Paging);

            var orders = await _repository.GetOrdersAsync(filter, paging);

            return new ReplyMessage<List<OrderInfo>>
            {
                Value = orders.Select(_mapper.Map<OrderInfo>).ToList()
            };
        }
    }
}
