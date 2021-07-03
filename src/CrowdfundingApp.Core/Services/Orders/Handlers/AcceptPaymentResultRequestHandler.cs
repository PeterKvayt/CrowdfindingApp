using CrowdfundingApp.Common.Core.Handlers;
using CrowdfundingApp.Common.Core.Messages;
using System;
using System.Threading.Tasks;
using AutoMapper;
using CrowdfundingApp.Common.Enums;
using CrowdfundingApp.Common.Data.Interfaces.Repositories;
using CrowdfundingApp.Common.Core.Maintainers.Payment;
using CrowdfundingApp.Common.Core.Messages.Payment;

namespace CrowdfundingApp.Core.Services.Orders.Handlers
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
