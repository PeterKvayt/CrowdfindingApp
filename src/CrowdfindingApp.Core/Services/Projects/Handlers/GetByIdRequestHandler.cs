using System.Threading.Tasks;
using AutoMapper;
using CrowdfindingApp.Common.DataTransfers.Projects;
using CrowdfindingApp.Common.Extensions;
using CrowdfindingApp.Common.Messages;
using CrowdfindingApp.Common.Messages.Projects;
using CrowdfindingApp.Data.Common.BusinessModels;
using CrowdfindingApp.Data.Common.Interfaces.Repositories;


namespace CrowdfindingApp.Core.Services.Projects.Handlers
{
    public class GetByIdRequestHandler : GetProjectInfoByIdRequestHandlerBase<ProjectInfo>
    {

        public GetByIdRequestHandler(IMapper mapper, 
            IProjectRepository projectRepository,
            IRewardRepository rewardRepository,
            IRewardGeographyRepository rewardGeographyRepository,
            Microsoft.Extensions.Configuration.IConfiguration configuration,
            IQuestionRepository questionRepository) : base(mapper, projectRepository, rewardRepository, rewardGeographyRepository, configuration, questionRepository)
        {
            
        }

        protected override async Task<(ReplyMessageBase, Project)> ValidateRequestMessageAsync(GetProjectByIdRequestMessage requestMessage)
        {
            var (reply, project) = await base.ValidateRequestMessageAsync(requestMessage);
            if(reply.Errors != null)
            {
                return (reply, project);
            }

            if(project.OwnerId != User.GetUserId() && !User.HasRole(nameof(Common.Immutable.Roles.Admin)))
            {
                reply.AddSecurityError();
            }

            return (reply, project);
        }

        protected override async Task<ReplyMessage<ProjectInfo>> ExecuteAsync(GetProjectByIdRequestMessage request, Project project)
        {
            return new ReplyMessage<ProjectInfo> { Value = await GetProjectInfoAsync(project) };
        }
    }
}
