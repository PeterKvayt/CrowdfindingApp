﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CrowdfundingApp.Common.Core.DataTransfers.Projects;
using CrowdfundingApp.Common.Core.DataTransfers.Questions;
using CrowdfundingApp.Common.Core.DataTransfers.Rewards;
using CrowdfundingApp.Common.Extensions;
using CrowdfundingApp.Common.Core.Handlers;
using CrowdfundingApp.Common.Core.Messages;
using CrowdfundingApp.Common.Core.Messages.Projects;
using CrowdfundingApp.Core.Services.Projects.Validators;
using CrowdfundingApp.Common.Data.BusinessModels;
using CrowdfundingApp.Common.Data.Interfaces.Repositories;
using CrowdfundingApp.Common.Core.Extensions;
using CrowdfundingApp.Common.Core.DataTransfers;

namespace CrowdfundingApp.Core.Services.Projects.Handlers
{
    public abstract class GetProjectInfoByIdRequestHandlerBase<TModel> : RequestHandlerBase<GetProjectByIdRequestMessage, ReplyMessage<TModel>, Project>
    {
        protected readonly IProjectRepository ProjectRepository;
        protected readonly IMapper Mapper;
        protected readonly IRewardRepository RewardRepository;
        protected readonly IRewardGeographyRepository RewardGeographyRepository;
        protected readonly IQuestionRepository QuestionRepository;
        protected readonly Microsoft.Extensions.Configuration.IConfiguration Configuration;
        protected readonly IOrderRepository OrderRepository;

        public GetProjectInfoByIdRequestHandlerBase(IMapper mapper,
            IProjectRepository projectRepository,
            IRewardRepository rewardRepository,
            IRewardGeographyRepository rewardGeographyRepository,
            Microsoft.Extensions.Configuration.IConfiguration configuration,
            IQuestionRepository questionRepository,
            IOrderRepository orderRepository)
        {
            ProjectRepository = projectRepository ?? throw new NullReferenceException(nameof(projectRepository));
            Mapper = mapper ?? throw new NullReferenceException(nameof(mapper));
            RewardRepository = rewardRepository ?? throw new NullReferenceException(nameof(rewardRepository));
            RewardGeographyRepository = rewardGeographyRepository ?? throw new NullReferenceException(nameof(rewardGeographyRepository));
            QuestionRepository = questionRepository ?? throw new NullReferenceException(nameof(questionRepository));
            Configuration = configuration ?? throw new NullReferenceException(nameof(configuration));
            OrderRepository = orderRepository ?? throw new NullReferenceException(nameof(orderRepository));
        }

        protected override async Task<(ReplyMessageBase, Project)> ValidateRequestMessageAsync(GetProjectByIdRequestMessage requestMessage)
        {
            var reply = new ReplyMessageBase();
            var validator = new GetByIdProjectValidator();
            var result = await validator.ValidateAsync(requestMessage);
            await reply.MergeAsync(result);
            var project = await ProjectRepository.GetByIdAsync(new Guid(requestMessage.ProjectId));
            if(project == null)
            {
                reply.AddObjectNotFoundError();
            }

            return (reply, project);
        }

        protected async Task<ProjectInfo> GetProjectInfoAsync(Project project)
        {
            var projectInfo = Mapper.Map<ProjectInfo>(project);
            projectInfo.Image = GetImageUrl(project.Id, project.Image);

            var questions = await QuestionRepository.GetByProjectIdAsync(project.Id);
            projectInfo.Questions = questions.Select(Mapper.Map<QuestionInfo>).ToList();

            projectInfo.Rewards = new List<RewardInfo>();
            var rewards = await RewardRepository.GetRewardsByProjectIdAsync(project.Id);
            foreach(var reward in rewards)
            {
                var info = Mapper.Map<RewardInfo>(reward);

                var orders = await OrderRepository.GetByRewardIdAsync(reward.Id);
                if(orders?.Any() ?? false)
                {
                    info.Limit -= orders.Sum(x => x.Count);
                }

                var deliveryCountries = await RewardGeographyRepository.GetByRewardIdAsync(reward.Id);
                info.DeliveryCountries = deliveryCountries.Select(x => new KeyValue<string, decimal?>(x.CountryId.ToString(), x.Price)).ToList();
                
                info.Image = GetImageUrl(project.Id, info.Image);
                
                projectInfo.Rewards.Add(info);
            }

            return projectInfo;
        }

        private string GetImageUrl(Guid projectId, string image)
        {
            if(image.IsNullOrWhiteSpace())
            {
                return null;
            }
            return $"{Configuration["FileStorageConfiguration:PermanentFolderName"]}/Projects/{projectId}/{image}";
        }
    }
}
