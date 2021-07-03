using System.Threading.Tasks;
using AutoMapper;
using CrowdfundingApp.Common.Core.DataTransfers.Projects;
using CrowdfundingApp.Common.Extensions;
using CrowdfundingApp.Common.Core.Messages;
using CrowdfundingApp.Common.Core.Messages.Projects;
using CrowdfundingApp.Common.Data.BusinessModels;
using CrowdfundingApp.Common.Data.Interfaces.Repositories;


namespace CrowdfundingApp.Core.Services.Projects.Handlers
{
    public class GetByIdRequestHandler : GetProjectInfoByIdRequestHandlerBase<ProjectInfo>
    {

        public GetByIdRequestHandler(IMapper mapper, 
            IProjectRepository projectRepository,
            IRewardRepository rewardRepository,
            IRewardGeographyRepository rewardGeographyRepository,
            Microsoft.Extensions.Configuration.IConfiguration configuration,
            IQuestionRepository questionRepository, IOrderRepository orderRepository) : base(mapper, projectRepository, rewardRepository, rewardGeographyRepository, configuration, questionRepository, orderRepository)
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
