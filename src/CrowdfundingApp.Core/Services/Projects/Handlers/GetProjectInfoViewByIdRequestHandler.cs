﻿using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CrowdfundingApp.Common.Core.DataTransfers.Projects;
using CrowdfundingApp.Common.Enums;
using CrowdfundingApp.Common.Extensions;
using CrowdfundingApp.Common.Immutable;
using CrowdfundingApp.Common.Core.Messages;
using CrowdfundingApp.Common.Core.Messages.Projects;
using CrowdfundingApp.Common.Data.BusinessModels;
using CrowdfundingApp.Common.Data.Interfaces.Repositories;
using CrowdfundingApp.Core.Services.Projects.Extensions;

namespace CrowdfundingApp.Core.Services.Projects.Handlers
{
    public class GetProjectInfoViewByIdRequestHandler : GetProjectInfoByIdRequestHandlerBase<ProjectInfoView>
    {
        public GetProjectInfoViewByIdRequestHandler(IMapper mapper,
            IProjectRepository projectRepository,
            IRewardRepository rewardRepository,
            IRewardGeographyRepository rewardGeographyRepository,
            Microsoft.Extensions.Configuration.IConfiguration configuration,
            IQuestionRepository questionRepository, IOrderRepository orderRepository) : base(mapper, projectRepository, rewardRepository, rewardGeographyRepository, configuration, questionRepository, orderRepository)
        {

        }

        private int[] _broadcastStatuses = new int[] { (int)ProjectStatus.Active, (int)ProjectStatus.Complited, (int)ProjectStatus.Finalized };

        protected override async Task<(ReplyMessageBase, Project)> ValidateRequestMessageAsync(GetProjectByIdRequestMessage requestMessage)
        {
            var (reply, project) = await base.ValidateRequestMessageAsync(requestMessage);
            if(reply.Errors?.Any() ?? false)
            {
                return (reply, project);
            }

            if(project.OwnerId != User.GetUserId()
                && !User.HasRole(nameof(Common.Immutable.Roles.Admin))
                && !_broadcastStatuses.Contains(project.Status))
            {
                reply.AddSecurityError();
            }

            return (reply, project);
        }

        protected override async Task<ReplyMessage<ProjectInfoView>> ExecuteAsync(GetProjectByIdRequestMessage request, Project project)
        {
            var view = Mapper.Map<ProjectInfoView>(await GetProjectInfoAsync(project));
            SetRestProjectDays(view);

            var categories = await ProjectRepository.GetCategoriesAsync();
            view.CategoryName = categories.FirstOrDefault(x => x.Id.ToString().Equals(view.CategoryId, StringComparison.InvariantCultureIgnoreCase))?.Name;

            var cities = await ProjectRepository.GetCitiesAsync();
            view.LocationName = cities.FirstOrDefault(x => x.Id.ToString().Equals(view.Location, StringComparison.InvariantCultureIgnoreCase))?.Name;

            view.Progress = await ProjectRepository.GetProgressAsync(new Guid(view.Id));
            return new ReplyMessage<ProjectInfoView> { Value = view };
        }

        private void SetRestProjectDays(ProjectInfoView view)
        {
            if(view.StartDateTime.HasValue && view.Duration.HasValue)
            {
                view.RestProjectTime = view.GetRestTime();
            }
            else if(view.Duration.HasValue)
            {
                view.RestProjectTime = $"{view.Duration.Value} д.";
            }
            else
            {
                view.RestProjectTime = "0 д.";
            }
        }
    }
}
