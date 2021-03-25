using System;
using System.Threading.Tasks;
using CrowdfindingApp.Common.DataTransfers.Projects;
using CrowdfindingApp.Common.Handlers;
using CrowdfindingApp.Common.Messages;
using CrowdfindingApp.Common.Messages.Projects;
using CrowdfindingApp.Common.Extensions;
using AutoMapper;
using CrowdfindingApp.Data.Common.Interfaces.Repositories;
using CrowdfindingApp.Data.Common.BusinessModels;
using System.Linq;
using CrowdfindingApp.Common.DataTransfers.Rewards;
using CrowdfindingApp.Common.DataTransfers.Questions;
using CrowdfindingApp.Common.Maintainers.FileStorageProvider;
using System.Collections.Generic;
using CrowdfindingApp.Data.Common.Filters;
using CrowdfindingApp.Common.Immutable;

namespace CrowdfindingApp.Core.Services.Projects.Handlers
{
    public abstract class SaveProjectRequestHandlerBase<TRequest, TReply> : NullOperationContextRequestHandler<TRequest, TReply>
        where TRequest : MessageBase, new()
        where TReply : ReplyMessageBase, new()
    {
        protected readonly IMapper Mapper;
        protected readonly IProjectRepository ProjectRepository;
        protected readonly IRewardRepository RewardRepository;
        protected readonly IRewardGeographyRepository RewardGeographyRepository;
        protected readonly IQuestionRepository QuestionRepository;
        protected readonly IFileStorage FileProvider;

        public SaveProjectRequestHandlerBase(IMapper mapper,
            IProjectRepository projectRepository,
            IRewardRepository rewardRepository,
            IRewardGeographyRepository rewardGeographyRepository,
            IFileStorage fileProvider,
            IQuestionRepository questionRepository)
        {
            Mapper = mapper ?? throw new NullReferenceException(nameof(mapper));
            ProjectRepository = projectRepository ?? throw new NullReferenceException(nameof(projectRepository));
            RewardRepository = rewardRepository ?? throw new NullReferenceException(nameof(rewardRepository));
            RewardGeographyRepository = rewardGeographyRepository ?? throw new NullReferenceException(nameof(rewardGeographyRepository));
            QuestionRepository = questionRepository ?? throw new NullReferenceException(nameof(questionRepository));
            FileProvider = fileProvider ?? throw new NullReferenceException(nameof(fileProvider));
        }

        protected async Task<ReplyMessage<string>> ProcessAsync(SaveDraftProjectRequestMessage request)
        {
            var reply = new ReplyMessage<string>();
            
            if(request.Data == null)
            {
                return reply;
            }

            if(request.Data.Id.NonNullOrWhiteSpace())
            {
                var projectFromDb = await ProjectRepository.GetByIdAsync(new Guid(request.Data.Id));
                if(projectFromDb == null)
                {
                    reply.AddObjectNotFoundError();
                    return null;
                }

                if(projectFromDb.OwnerId != User.GetUserId() && !User.HasRole(Common.Immutable.Roles.Admin))
                {
                    reply.AddSecurityError();
                    return null;
                }
            }

            var projectId = await SaveProjectAsync(request.Data);
            await SaveQuestionsAsync(request.Data, projectId);
            await SaveRewardsAsync(request.Data, projectId);
            reply.Value = projectId.ToString();

            return reply;
        }

        private async Task<Guid> SaveProjectAsync(ProjectInfo projectInfo)
        {
            var isNew = projectInfo.Id.IsNullOrWhiteSpace();
            var project = Mapper.Map<Project>(projectInfo);
            PrepareValues(project, isNew);
            var projectId = project.Id;
            if(isNew)
            {
                projectId = await ProjectRepository.AddAsync(project);
            }
            else
            {
                await ProjectRepository.UpdateAsync(project, Mapper);
            }
            await SaveImageAsync(projectId, project.Image);
            return projectId;
        }

        private async Task<string> SaveImageAsync(Guid projectId, string image)
        {
            if(image.IsNullOrWhiteSpace())
            {
                return null;
            }
            image = image.Split('/').Last();
            await FileProvider.SaveProjectImageAsync(image, projectId);
            return image;
        }

        protected virtual void PrepareValues(Project project, bool isNew)
        {
            project.OwnerId = User.GetUserId();
            project.Image = project.Image.Split('/').Last();
        }

        private async Task SaveQuestionsAsync(ProjectInfo projectInfo, Guid projectId)
        {
            if(projectInfo.Questions is null || !projectInfo.Questions.Any())
            {
                return;
            }

            var questions = projectInfo.Questions.Select(x => MapToQuestion(x, projectId));
            await QuestionRepository.SubstituteRangeAsync(questions.Where(x => x.Id != Guid.Empty).ToList(), projectId);
            foreach(var question in questions.Where(x => x.Id == Guid.Empty))
            {
                await QuestionRepository.AddAsync(question);
            }
        }

        private Question MapToQuestion(QuestionInfo info, Guid projectId)
        {
            var question = Mapper.Map<Question>(info);
            question.ProjectId = projectId;
            return question;
        }

        private async Task SaveRewardsAsync(ProjectInfo projectInfo, Guid projectId)
        {
            await RewardRepository.RemoveByProjectAsync(projectId);
            if(projectInfo.Rewards is null || !projectInfo.Rewards.Any())
            {
                return;
            }

            foreach(var rewardInfo in projectInfo.Rewards)
            {
                var reward = MapToReward(rewardInfo, projectId);
                reward.Image = await SaveImageAsync(projectId, reward.Image);
                var rewardId = await RewardRepository.AddAsync(reward);

                var deliveryCountries = rewardInfo.DeliveryCountries.Select(x => new RewardGeography(rewardId, new Guid(x.Key), x.Value.Value)).ToList();
                await RewardGeographyRepository.SubstituteRangeAsync(deliveryCountries, rewardId);
            }
        }

        private Reward MapToReward(RewardInfo info, Guid projectId)
        {
            var reward = Mapper.Map<Reward>(info);
            reward.ProjectId = projectId;
            reward.IsLimited = reward.Limit.HasValue;
            return reward;
        }
    }
}
