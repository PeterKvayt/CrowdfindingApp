using System;
using System.Threading.Tasks;
using CrowdfindingApp.Common.DataTransfers.Projects;
using CrowdfindingApp.Common.Handlers;
using CrowdfindingApp.Common.Messages;
using CrowdfindingApp.Common.Messages.Projects;
using CrowdfindingApp.Core.Services.Projects.Validators;
using CrowdfindingApp.Common.Extensions;
using AutoMapper;
using CrowdfindingApp.Data.Common.Interfaces.Repositories;
using CrowdfindingApp.Data.Common.BusinessModels;
using CrowdfindingApp.Common.Enums;
using CrowdfindingApp.Data.Common.Filters;
using System.Collections.Generic;
using System.Linq;
using CrowdfindingApp.Common.DataTransfers.Rewards;
using CrowdfindingApp.Common.DataTransfers.Questions;

namespace CrowdfindingApp.Core.Services.Projects.Handlers
{
    public class ProjectModerationRequestHandler : SaveProjectRequestHandlerBase<ProjectModerationRequestMessage, ReplyMessageBase>
    {
        //private IMapper _mapper;
        //private IProjectRepository _projectRepository;
        //private IRewardRepository _rewardRepository;
        //private IRewardGeographyRepository _rewardGeographyRepository;
        //private IQuestionRepository _questionRepository;

        public ProjectModerationRequestHandler(IMapper mapper, 
            IProjectRepository projectRepository, 
            IRewardRepository rewardRepository, 
            IRewardGeographyRepository rewardGeographyRepository,
            IQuestionRepository questionRepository) : base(mapper, projectRepository, rewardRepository, rewardGeographyRepository, questionRepository)
        {
            //_mapper = mapper ?? throw new NullReferenceException(nameof(mapper));
            //_projectRepository = projectRepository ?? throw new NullReferenceException(nameof(projectRepository));
            //_rewardRepository = rewardRepository ?? throw new NullReferenceException(nameof(rewardRepository));
            //_rewardGeographyRepository = rewardGeographyRepository ?? throw new NullReferenceException(nameof(rewardGeographyRepository));
            //_questionRepository = questionRepository ?? throw new NullReferenceException(nameof(questionRepository));
        }

        protected override async Task<ReplyMessageBase> ValidateRequestMessageAsync(ProjectModerationRequestMessage requestMessage)
        {
            var reply = await base.ValidateRequestMessageAsync(requestMessage);
            var validator = new ProjectModerationValidator();
            var validationResult = await validator.ValidateAsync(requestMessage.Data);
            await reply.MergeAsync(validationResult);

            if(validationResult.IsValid && requestMessage.Data.Id.NonNullOrWhiteSpace())
            {
                var projectFromDb = ProjectRepository.GetProjects(new ProjectFilter
                {
                    Id = new List<Guid> { new Guid(requestMessage.Data.Id) },
                    OwnerId = new List<Guid> { User.GetUserId() }
                }, null);
                
                if(projectFromDb == null)
                {
                    reply.AddObjectNotFoundError();
                }
            }

            return reply;
        }

        protected override async Task<ReplyMessageBase> ExecuteAsync(ProjectModerationRequestMessage request)
        {
            return await ProcessAsync(request);
        }

        //private async Task<Guid> SaveProjectAsync(ProjectInfo projectInfo)
        //{
        //    var isNew = projectInfo.Id.IsNullOrWhiteSpace();
        //    var project = _mapper.Map<Project>(projectInfo);
        //    SetDefaultValues(project, isNew);

        //    var projectId = project.Id;
        //    if(isNew)
        //    {
        //        projectId = await _projectRepository.AddAsync(project);
        //    }
        //    else
        //    {
        //        await _projectRepository.UpdateAsync(project);
        //    }
        //    return projectId;
        //}

        protected override void SetDefaultValues(Project project, bool isNew)
        {
            base.SetDefaultValues(project, isNew);
            project.Status = (int)ProjectStatus.Moderation;
        }

        //private async Task SaveQuestionsAsync(ProjectInfo projectInfo, Guid projectId)
        //{
        //    var questions = projectInfo.Questions.Select(x => MapToQuestion(x, projectId));
        //    foreach(var question in questions)
        //    {
        //        if(question.Id == Guid.Empty)
        //        {
        //            await _questionRepository.AddAsync(question);
        //        }
        //        else
        //        {
        //            await _questionRepository.UpdateAsync(question);
        //        }
        //    }
        //}

        //private Question MapToQuestion(QuestionInfo info, Guid projectId)
        //{
        //    var question = _mapper.Map<Question>(info);
        //    question.ProjectId = projectId;
        //    return question;
        //}

        //private async Task SaveRewardsAsync(ProjectInfo projectInfo, Guid projectId)
        //{
        //    foreach(var rewardinfo in projectInfo.Rewards)
        //    {
        //        var reward = MapToReward(rewardinfo, projectId);
        //        var rewardId = reward.Id;
        //        if(reward.Id == Guid.Empty)
        //        {
        //            rewardId = await _rewardRepository.AddAsync(reward);
        //        }
        //        else
        //        {
        //            await _rewardRepository.UpdateAsync(reward);
        //        }

        //        var deliveryCountries = rewardinfo.DeliveryCountries.Select(x => new RewardGeography(rewardId, new Guid(x.Key), x.Value.Value)).ToList();
        //        await _rewardGeographyRepository.SubstituteRangeAsync(deliveryCountries, rewardId);
        //    }
        //}

        //private Reward MapToReward(RewardInfo info, Guid projectId)
        //{
        //    var reward = _mapper.Map<Reward>(info);
        //    reward.ProjectId = projectId;
        //    return reward;
        //}


    }
}
