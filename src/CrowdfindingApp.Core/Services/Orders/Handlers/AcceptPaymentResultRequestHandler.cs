using CrowdfindingApp.Common.Handlers;
using CrowdfindingApp.Common.Messages;
using System;
using System.Threading.Tasks;
using AutoMapper;
using CrowdfindingApp.Common.Enums;
using CrowdfindingApp.Data.Common.Interfaces.Repositories;
using CrowdfindingApp.Common.Maintainers.Payment;
using CrowdfindingApp.Common.Messages.Payment;

namespace CrowdfindingApp.Core.Services.Orders.Handlers
{
    public class AcceptPaymentResultRequestHandler : NullOperationContextRequestHandler<PaymentRequestMessage, ReplyMessage<string>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IPaymentManager _paymentManager;

        public AcceptPaymentResultRequestHandler(IOrderRepository orderRepository, IMapper mapper, IRewardRepository rewardRepository, IProjectRepository projectRepository,
            IPaymentManager paymentManager)
        {
            _orderRepository = orderRepository ?? throw new NullReferenceException(nameof(orderRepository));
            _mapper = mapper ?? throw new NullReferenceException(nameof(mapper));
            _paymentManager = paymentManager ?? throw new NullReferenceException(nameof(paymentManager));
        }

        protected override async Task<ReplyMessage<string>> ExecuteAsync(PaymentRequestMessage request)
        {
            if(!Guid.TryParse(request.InvId, out var id))
            {
                return new ReplyMessage<string> { Value = "fail" };
            }

            var order = await _orderRepository.GetByIdAsync(id);
            if(order is null)
            {
                return new ReplyMessage<string> { Value = "fail" };
            }

            var isSuccess = _paymentManager.ValidateResponse(request, order.Id.ToString());
            if(isSuccess)
            {
                order.Status = (int)OrderStatus.Approved;
                await _orderRepository.UpdateAsync(order, _mapper);
                return new ReplyMessage<string> { Value = $"OK{order.Id}" };
            }
            else
            {
                order.Status = (int)OrderStatus.Failed;
                await _orderRepository.UpdateAsync(order, _mapper);
                return new ReplyMessage<string> { Value = "fail" };
            }
        }
    }
}
