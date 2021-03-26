﻿using System;
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
using CrowdfindingApp.Common.Handlers;

namespace CrowdfindingApp.Core.Services.Orders.Handlers
{
    public class AcceptOrderRequestHandler : RequestHandlerBase<AcceptOrderRequestMessage, ReplyMessageBase, Project>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IRewardRepository _rewardRepository;
        private readonly IProjectRepository _projectRepository;

        public AcceptOrderRequestHandler(IOrderRepository orderRepository, IMapper mapper, IRewardRepository rewardRepository, IProjectRepository projectRepository)
        {
            _orderRepository = orderRepository ?? throw new NullReferenceException(nameof(orderRepository));
            _mapper = mapper ?? throw new NullReferenceException(nameof(mapper));
            _rewardRepository = rewardRepository ?? throw new NullReferenceException(nameof(rewardRepository));
            _projectRepository = projectRepository ?? throw new NullReferenceException(nameof(projectRepository));
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

        protected override async Task<ReplyMessageBase> ExecuteAsync(AcceptOrderRequestMessage request, Project project)
        {
            var order = _mapper.Map<Order>(request);

            order.Status = (int)OrderStatus.Approved;
            order.UserId = User.GetUserId();
            order.PaymentDateTime = DateTime.UtcNow;

            await _orderRepository.AddAsync(order);

            var progress = await _projectRepository.GetProgressAsync(project.Id);
            if(progress >= project.Budget)
            {
                await _projectRepository.SetStatusAsync((int)ProjectStatus.Complited, new Guid[] { project.Id });
            }

            return new ReplyMessageBase();
        }
    }
}
