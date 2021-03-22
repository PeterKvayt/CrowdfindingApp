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
    public abstract class GetProjectInfoByIdRequestHandlerBase<TModel> : RequestHandlerBase<GetProjectByIdRequestMessage, ReplyMessage<TModel>, Project>
    {
        protected readonly IProjectRepository ProjectRepository;
        protected readonly IMapper Mapper;
        protected readonly IRewardRepository RewardRepository;
        protected readonly IRewardGeographyRepository RewardGeographyRepository;
        protected readonly IQuestionRepository QuestionRepository;
        protected readonly Microsoft.Extensions.Configuration.IConfiguration Configuration;

        public GetProjectInfoByIdRequestHandlerBase(IMapper mapper,
            IProjectRepository projectRepository,
            IRewardRepository rewardRepository,
            IRewardGeographyRepository rewardGeographyRepository,
            Microsoft.Extensions.Configuration.IConfiguration configuration,
            IQuestionRepository questionRepository)
        {
            ProjectRepository = projectRepository ?? throw new NullReferenceException(nameof(projectRepository));
            Mapper = mapper ?? throw new NullReferenceException(nameof(mapper));
            RewardRepository = rewardRepository ?? throw new NullReferenceException(nameof(rewardRepository));
            RewardGeographyRepository = rewardGeographyRepository ?? throw new NullReferenceException(nameof(rewardGeographyRepository));
            QuestionRepository = questionRepository ?? throw new NullReferenceException(nameof(questionRepository));
            Configuration = configuration ?? throw new NullReferenceException(nameof(configuration));
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
                var deliveryCountries = await RewardGeographyRepository.GetByRewardIdAsync(reward.Id);
                var info = Mapper.Map<RewardInfo>(reward);
                info.Image = GetImageUrl(project.Id, info.Image);
                info.DeliveryCountries = deliveryCountries.Select(x => new Common.DataTransfers.KeyValue<string, decimal?>(x.CountryId.ToString(), x.Price)).ToList();
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
