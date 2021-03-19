using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CrowdfindingApp.Common.DataTransfers.Projects;
using CrowdfindingApp.Common.DataTransfers.Questions;
using CrowdfindingApp.Common.DataTransfers.Rewards;
using CrowdfindingApp.Common.Extensions;
using CrowdfindingApp.Common.Handlers;
using CrowdfindingApp.Common.Messages;
using CrowdfindingApp.Common.Messages.Projects;
using CrowdfindingApp.Core.Services.Projects.Validators;
using CrowdfindingApp.Data.Common.BusinessModels;
using CrowdfindingApp.Data.Common.Interfaces.Repositories;

namespace CrowdfindingApp.Core.Services.Projects.Handlers
{
    public class GetByIdRequestHandler : RequestHandlerBase<GetProjectByIdRequestMessage, ReplyMessage<ProjectInfo>, Project>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        private readonly IRewardRepository _rewardRepository;
        private readonly IRewardGeographyRepository _rewardGeographyRepository;
        private readonly IQuestionRepository _questionRepository;

        public GetByIdRequestHandler(IMapper mapper, 
            IProjectRepository projectRepository,
            IRewardRepository rewardRepository,
            IRewardGeographyRepository rewardGeographyRepository,
            IQuestionRepository questionRepository)
        {
            _projectRepository = projectRepository ?? throw new NullReferenceException(nameof(projectRepository));
            _mapper = mapper ?? throw new NullReferenceException(nameof(mapper));
            _rewardRepository = rewardRepository ?? throw new NullReferenceException(nameof(rewardRepository));
            _rewardGeographyRepository = rewardGeographyRepository ?? throw new NullReferenceException(nameof(rewardGeographyRepository));
            _questionRepository = questionRepository ?? throw new NullReferenceException(nameof(questionRepository));
        }

        protected override async Task<(ReplyMessageBase, Project)> ValidateRequestMessageAsync(GetProjectByIdRequestMessage requestMessage)
        {
            var reply = new ReplyMessageBase();
            var validator = new GetByIdProjectValidator();
            var result = await validator.ValidateAsync(requestMessage);
            await reply.MergeAsync(result);
            var project = await _projectRepository.GetByIdAsync(new Guid(requestMessage.ProjectId));
            if(project == null)
            {
                reply.AddObjectNotFoundError();
            }
            if(project.OwnerId != User.GetUserId())
            {
                reply.AddSecurityError();
            }

            return (reply, project);
        }

        protected override async Task<ReplyMessage<ProjectInfo>> ExecuteAsync(GetProjectByIdRequestMessage request, Project project)
        {
            var projectInfo = _mapper.Map<ProjectInfo>(project);

            var questions = await _questionRepository.GetByProjectIdAsync(project.Id);
            projectInfo.Questions = questions.Select(_mapper.Map<QuestionInfo>).ToList();

            projectInfo.Rewards = new List<RewardInfo>();
            var rewards = await _rewardRepository.GetRewardsByProjectIdAsync(project.Id);
            foreach(var reward in rewards)
            {
                var deliveryCountries = await _rewardGeographyRepository.GetByRewardIdAsync(reward.Id);
                var info = _mapper.Map<RewardInfo>(reward);
                info.DeliveryCountries = deliveryCountries.Select(x => new Common.DataTransfers.KeyValue<string, decimal?>(x.CountryId.ToString(), x.Price)).ToList();
                projectInfo.Rewards.Add(info);
            }

            return new ReplyMessage<ProjectInfo> { Value = projectInfo };
        }
    }
}
