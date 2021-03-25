using System;
using System.Threading.Tasks;
using AutoMapper;
using CrowdfindingApp.Common.Enums;
using CrowdfindingApp.Common.Extensions;
using CrowdfindingApp.Common.Handlers;
using CrowdfindingApp.Common.Messages;
using CrowdfindingApp.Common.Messages.Orders;
using CrowdfindingApp.Common.Validators;
using CrowdfindingApp.Core.Services.Orders.Validator;
using CrowdfindingApp.Data.Common.BusinessModels;
using CrowdfindingApp.Data.Common.Interfaces.Repositories;

namespace CrowdfindingApp.Core.Services.Orders.Handlers
{
    public class AcceptOrderRequestHandler : NullOperationContextRequestHandler<AcceptOrderRequestMessage, ReplyMessageBase>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IRewardRepository _rewardRepository;

        public AcceptOrderRequestHandler(IOrderRepository orderRepository, IMapper mapper, IRewardRepository rewardRepository)
        {
            _orderRepository = orderRepository ?? throw new NullReferenceException(nameof(orderRepository));
            _mapper = mapper ?? throw new NullReferenceException(nameof(mapper));
            _rewardRepository = rewardRepository ?? throw new NullReferenceException(nameof(rewardRepository));
        }

        protected override async Task<ReplyMessageBase> ValidateRequestMessageAsync(AcceptOrderRequestMessage requestMessage)
        {
            var reply = await base.ValidateRequestMessageAsync(requestMessage);
            var rewardIdvalidator = new IdValidator();
            var idValidationresult = await rewardIdvalidator.ValidateAsync(requestMessage.RewardId);
            await reply.MergeAsync(idValidationresult);

            var reward = await _rewardRepository.GetByIdAsync(new Guid(requestMessage.RewardId));
            if(reward == null)
            {
                reply.AddObjectNotFoundError();
                return reply;
            }

            var orderedCount = await _orderRepository.GetOrdersCountByRewardIdAsync(reward.Id);
            var validator = new AcceptOrderMessageValidator(reward, orderedCount, requestMessage.Count);
            var validationResult = await validator.ValidateAsync(requestMessage);
            await reply.MergeAsync(validationResult);

            return reply;
        }

        protected override async Task<ReplyMessageBase> ExecuteAsync(AcceptOrderRequestMessage request)
        {
            var order = _mapper.Map<Order>(request);

            order.Status = (int)OrderStatus.Approved;
            order.UserId = User.GetUserId();
            order.PaymentDateTime = DateTime.UtcNow;

            await _orderRepository.AddAsync(order);

            return new ReplyMessageBase();
        }
    }
}
