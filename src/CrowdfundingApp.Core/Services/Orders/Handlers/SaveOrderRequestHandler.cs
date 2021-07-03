using System;
using System.Threading.Tasks;
using AutoMapper;
using CrowdfundingApp.Common.Enums;
using CrowdfundingApp.Common.Extensions;
using CrowdfundingApp.Common.Core.Handlers;
using CrowdfundingApp.Common.Core.Messages;
using CrowdfundingApp.Common.Core.Messages.Orders;
using CrowdfundingApp.Common.Core.Validators;
using CrowdfundingApp.Core.Services.Orders.Validator;
using CrowdfundingApp.Common.Data.BusinessModels;
using CrowdfundingApp.Common.Data.Interfaces.Repositories;
using CrowdfundingApp.Common.Core.Maintainers.Payment;
using CrowdfundingApp.Common.Core.Extensions;

namespace CrowdfundingApp.Core.Services.Orders.Handlers
{
    public class SaveOrderRequestHandler : RequestHandlerBase<AcceptOrderRequestMessage, ReplyMessage<string>, Project>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IRewardRepository _rewardRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IPaymentManager _paymentManager;

        public SaveOrderRequestHandler(IOrderRepository orderRepository, IMapper mapper, IRewardRepository rewardRepository, IProjectRepository projectRepository,
            IPaymentManager paymentManager)
        {
            _orderRepository = orderRepository ?? throw new NullReferenceException(nameof(orderRepository));
            _mapper = mapper ?? throw new NullReferenceException(nameof(mapper));
            _rewardRepository = rewardRepository ?? throw new NullReferenceException(nameof(rewardRepository));
            _projectRepository = projectRepository ?? throw new NullReferenceException(nameof(projectRepository));
            _paymentManager = paymentManager ?? throw new NullReferenceException(nameof(paymentManager));
        }

        protected override async Task<(ReplyMessageBase, Project)> ValidateRequestMessageAsync(AcceptOrderRequestMessage requestMessage)
        {
            var (reply, project) = await base.ValidateRequestMessageAsync(requestMessage);
            var rewardIdvalidator = new IdValidator();
            var idValidationresult = await rewardIdvalidator.ValidateAsync(requestMessage.RewardId);
            await reply.MergeAsync(idValidationresult);

            var reward = await _rewardRepository.GetByIdAsync(new Guid(requestMessage.RewardId));
            if(reward == null)
            {
                reply.AddObjectNotFoundError();
                return (reply, project);
            }

            project = await _projectRepository.GetByIdAsync(reward.ProjectId);
            if(project == null)
            {
                reply.AddObjectNotFoundError();
                return (reply, project);
            }

            if(project.Status != (int)ProjectStatus.Active && project.Status != (int)ProjectStatus.Complited)
            {
                reply.AddValidationError(OrderErrorMessageKeys.DisallowToSupportProject);
                return (reply, project);
            }

            var orderedCount = await _orderRepository.GetOrdersCountByRewardIdAsync(reward.Id);
            var validator = new AcceptOrderMessageValidator(reward, orderedCount, requestMessage.Count);
            var validationResult = await validator.ValidateAsync(requestMessage);
            await reply.MergeAsync(validationResult);

            return (reply, project);
        }

        protected override async Task<ReplyMessage<string>> ExecuteAsync(AcceptOrderRequestMessage request, Project project)
        {
            var order = _mapper.Map<Order>(request);

            order.Status = (int)OrderStatus.Approved;
            order.UserId = User.GetUserId();
            order.PaymentDateTime = DateTime.UtcNow;

            var orderId = await _orderRepository.AddAsync(order);

            var progress = await _projectRepository.GetProgressAsync(project.Id);
            if(progress >= project.Budget)
            {
                await _projectRepository.SetStatusAsync((int)ProjectStatus.Complited, new Guid[] { project.Id });
            }

            var reward = await _rewardRepository.GetByIdAsync(new Guid(request.RewardId));
            return new ReplyMessage<string> { Value = _paymentManager.GetPaymentUri(reward.Price.Value * request.Count, reward.Title, orderId) };
        }
    }
}
