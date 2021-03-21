using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CrowdfindingApp.Common.DataTransfers;
using CrowdfindingApp.Common.DataTransfers.Projects;
using CrowdfindingApp.Common.Extensions;
using CrowdfindingApp.Common.Handlers;
using CrowdfindingApp.Common.Messages;
using CrowdfindingApp.Common.Messages.Projects;
using CrowdfindingApp.Data.Common.BusinessModels;
using CrowdfindingApp.Data.Common.Filters;
using CrowdfindingApp.Data.Common.Interfaces.Repositories;
using CrowdfindingApp.Data.Common.Models;
using Microsoft.Extensions.Configuration;

namespace CrowdfindingApp.Core.Services.Projects.Handlers
{
    public abstract class ProjectCardSearchRequestHandlerBase : NullOperationContextRequestHandler<ProjectSearchRequestMessage, PagedReplyMessage<List<ProjectCard>>>
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

        protected async Task<PagedReplyMessage<List<ProjectCard>>> SearchAsync(ProjectFilter filter, Paging paging)
        {
            var projects = await ProjectRepository.GetProjects(filter, paging);
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
            card.CurrentResult = await GetProjectProgressAsync(project.Id);
            if(card.CategoryId.NonNullOrWhiteSpace())
            {
                card.CategoryName = categories.FirstOrDefault(_ => _.Id.ToString() == card.CategoryId)?.Name;
            }
            return card;
        }

        private async Task<decimal> GetProjectProgressAsync(Guid projectId)
        {
            var rewards = await RewardRepository.GetRewardsByProjectIdAsync(projectId);
            var orders = await OrderRepository.GetOrdersAsync(new OrderFilter
            {
                RewardId = rewards.Select(x => x.Id).ToList()
            });
            var groupedOrders = orders.GroupBy(x => x.RewardId);
            decimal progress = 0;
            foreach(var group in groupedOrders)
            {
                progress += group.Count() * rewards.First(x => x.Id == group.Key).Price.Value;
            }

            return progress;
        }

        private void PrepareProjectImage(Project project)
        {
            if(project.Image.IsNullOrWhiteSpace())
            {
                return;
            }
            project.Image = $"{Configuration["FileStorageConfiguration:PermanentFolderName"]}/Projects/{project.Id}/{project.Image}";
        }
    }
}
