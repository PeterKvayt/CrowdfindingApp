using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CrowdfundingApp.Common.Core.DataTransfers;
using CrowdfundingApp.Common.Core.DataTransfers.Project;
using CrowdfundingApp.Common.Core.DataTransfers.Projects;
using CrowdfundingApp.Common.Enums;
using CrowdfundingApp.Common.Extensions;
using CrowdfundingApp.Common.Core.Handlers;
using CrowdfundingApp.Common.Core.Messages;
using CrowdfundingApp.Common.Data.BusinessModels;
using CrowdfundingApp.Common.Data.Filters;
using CrowdfundingApp.Common.Data.Interfaces.Repositories;
using CrowdfundingApp.Common.Data.Models;
using Microsoft.Extensions.Configuration;
using CrowdfundingApp.Core.Services.Projects.Extensions;

namespace CrowdfundingApp.Core.Services.Projects.Handlers
{
    public abstract class ProjectCardSearchRequestHandlerBase<TRequest> : NullOperationContextRequestHandler<TRequest, PagedReplyMessage<List<ProjectCard>>>
        where TRequest : MessageBase, new()
    {
        protected readonly IProjectRepository ProjectRepository;
        protected readonly IMapper Mapper;
        protected readonly IRewardRepository RewardRepository;
        protected readonly IOrderRepository OrderRepository;
        protected readonly IConfiguration Configuration;

        public ProjectCardSearchRequestHandlerBase(IProjectRepository projectRepository, IMapper mapper, IRewardRepository rewardRepository, IOrderRepository orderRepository,
            IConfiguration configuration)
        {
            ProjectRepository = projectRepository ?? throw new NullReferenceException(nameof(projectRepository));
            Mapper = mapper ?? throw new NullReferenceException(nameof(mapper));
            RewardRepository = rewardRepository ?? throw new NullReferenceException(nameof(rewardRepository));
            OrderRepository = orderRepository ?? throw new NullReferenceException(nameof(orderRepository));
            Configuration = configuration ?? throw new NullReferenceException(nameof(configuration));
        }

        protected async Task<PagedReplyMessage<List<ProjectCard>>> SearchAsync(ProjectFilterInfo filter, PagingInfo paging)
        {
            return await SearchAsync(Mapper.Map<ProjectFilter>(filter), Mapper.Map<Paging>(paging));
        }

        protected async Task<PagedReplyMessage<List<ProjectCard>>> SearchAsync(ProjectFilter filter, Paging paging)
        {
            var projects = await ProjectRepository.GetProjectsAsync(filter, paging);
            var categories = await ProjectRepository.GetCategoriesByIdsAsync(projects.Select(x => x.CategoryId)
                .Distinct()
                .ToList());

            var cards = new List<ProjectCard>();
            foreach(var project in projects)
            {
                cards.Add(await MapToCardAsync(project, categories));
            }

            return new PagedReplyMessage<List<ProjectCard>>
            {
                Value = cards,
                Paging = Mapper.Map<PagingInfo>(paging)
            };
        }

        private async Task<ProjectCard> MapToCardAsync(Project project, List<Category> categories)
        {
            PrepareProjectImage(project);
            var card = Mapper.Map<ProjectCard>(project);
            SetRestTimeToEnd(card, project);
            card.CurrentResult = await ProjectRepository.GetProgressAsync(project.Id);
            if(card.CategoryId.NonNullOrWhiteSpace())
            {
                card.CategoryName = categories.FirstOrDefault(_ => _.Id.ToString() == card.CategoryId)?.Name;
            }
            return card;
        }

        private void PrepareProjectImage(Project project)
        {
            if(project.Image.IsNullOrWhiteSpace())
            {
                return;
            }
            project.Image = $"{Configuration["FileStorageConfiguration:PermanentFolderName"]}/Projects/{project.Id}/{project.Image}";
        }

        private void SetRestTimeToEnd(ProjectCard card, Project project)
        {
            if(card.Status == ProjectStatus.Active || card.Status == ProjectStatus.Complited)
            {
                card.RestTimeToEnd = project.GetRestTime();
            }
            if(card.Status == ProjectStatus.Finalized)
            {
                card.RestTimeToEnd = "Завершен";
                return;
            }
            if(card.Status == ProjectStatus.Stopped)
            {
                card.RestTimeToEnd = "Остановлен";
                return;
            }
        }
    }
}
