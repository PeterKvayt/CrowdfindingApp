using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CrowdfundingApp.Common.Core.DataTransfers.Orders;
using CrowdfundingApp.Common.Core.Handlers;
using CrowdfundingApp.Common.Core.Messages;
using CrowdfundingApp.Common.Core.Messages.Orders;
using CrowdfundingApp.Common.Data.Filters;
using CrowdfundingApp.Common.Data.Interfaces.Repositories;
using CrowdfundingApp.Common.Data.Models;

namespace CrowdfundingApp.Core.Services.Orders.Handlers
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
