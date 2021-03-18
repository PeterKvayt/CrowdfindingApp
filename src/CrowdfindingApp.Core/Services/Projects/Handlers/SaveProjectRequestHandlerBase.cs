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

namespace CrowdfindingApp.Core.Services.Projects.Handlers
{
    public abstract class SaveProjectRequestHandlerBase<TRequest, TReply> : NullOperationContextRequestHandler<TRequest, TReply>
        where TRequest : MessageBase, new()
        where TReply : ReplyMessageBase, new()
    {
        protected IMapper Mapper;
        protected IProjectRepository ProjectRepository;
        protected IRewardRepository RewardRepository;
        protected IRewardGeographyRepository RewardGeographyRepository;
        protected IQuestionRepository QuestionRepository;

        public SaveProjectRequestHandlerBase(IMapper mapper,
            IProjectRepository projectRepository,
            IRewardRepository rewardRepository,
            IRewardGeographyRepository rewardGeographyRepository,
            IQuestionRepository questionRepository)
        {
            Mapper = mapper ?? throw new NullReferenceException(nameof(mapper));
            ProjectRepository = projectRepository ?? throw new NullReferenceException(nameof(projectRepository));
            RewardRepository = rewardRepository ?? throw new NullReferenceException(nameof(rewardRepository));
            RewardGeographyRepository = rewardGeographyRepository ?? throw new NullReferenceException(nameof(rewardGeographyRepository));
            QuestionRepository = questionRepository ?? throw new NullReferenceException(nameof(questionRepository));
        }

        protected async Task<ReplyMessageBase> ProcessAsync(SaveDraftProjectRequestMessage request)
        {
            if(request.Data == null)
            {
                return new ReplyMessageBase();
            }

            var projectId = await SaveProjectAsync(request.Data);
            await SaveQuestionsAsync(request.Data, projectId);
            await SaveRewardsAsync(request.Data, projectId);

            return new ReplyMessageBase();
        }

        private async Task<Guid> SaveProjectAsync(ProjectInfo projectInfo)
        {
            var isNew = projectInfo.Id.IsNullOrWhiteSpace();
            var project = Mapper.Map<Project>(projectInfo);
            SetDefaultValues(project, isNew);

            var projectId = project.Id;
            if(isNew)
            {
                projectId = await ProjectRepository.AddAsync(project);
            }
            else
            {
                await ProjectRepository.UpdateAsync(project);
            }
            return projectId;
        }

        protected virtual void SetDefaultValues(Project project, bool isNew)
        {
            project.OwnerId = User.GetUserId();
       }

        private async Task SaveQuestionsAsync(ProjectInfo projectInfo, Guid projectId)
        {
            var questions = projectInfo.Questions.Select(x => MapToQuestion(x, projectId));
            foreach(var question in questions)
            {
                if(question.Id == Guid.Empty)
                {
                    await QuestionRepository.AddAsync(question);
                }
                else
                {
                    await QuestionRepository.UpdateAsync(question);
                }
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
            foreach(var rewardinfo in projectInfo.Rewards)
            {
                var reward = MapToReward(rewardinfo, projectId);
                var rewardId = reward.Id;
                if(reward.Id == Guid.Empty)
                {
                    rewardId = await RewardRepository.AddAsync(reward);
                }
                else
                {
                    await RewardRepository.UpdateAsync(reward);
                }

                var deliveryCountries = rewardinfo.DeliveryCountries.Select(x => new RewardGeography(rewardId, new Guid(x.Key), x.Value.Value)).ToList();
                await RewardGeographyRepository.SubstituteRangeAsync(deliveryCountries, rewardId);
            }
        }

        private Reward MapToReward(RewardInfo info, Guid projectId)
        {
            var reward = Mapper.Map<Reward>(info);
            reward.ProjectId = projectId;
            return reward;
        }
    }
}
