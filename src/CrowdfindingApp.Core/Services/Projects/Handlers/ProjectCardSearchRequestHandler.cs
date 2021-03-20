using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
    public class ProjectCardSearchRequestHandler : NullOperationContextRequestHandler<ProjectCardSearchRequestMessage, ReplyMessage<List<ProjectCard>>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        private readonly IRewardRepository _rewardRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IConfiguration _configuration;

        public ProjectCardSearchRequestHandler(IProjectRepository projectRepository, IMapper mapper, IRewardRepository rewardRepository, IOrderRepository orderRepository,
            IConfiguration configuration)
        {
            _projectRepository = projectRepository ?? throw new NullReferenceException(nameof(projectRepository));
            _mapper = mapper ?? throw new NullReferenceException(nameof(mapper));
            _rewardRepository = rewardRepository ?? throw new NullReferenceException(nameof(rewardRepository));
            _orderRepository = orderRepository ?? throw new NullReferenceException(nameof(orderRepository));
            _configuration = configuration ?? throw new NullReferenceException(nameof(configuration));
        }

        protected override async Task<ReplyMessage<List<ProjectCard>>> ExecuteAsync(ProjectCardSearchRequestMessage request)
        {
            var filter = _mapper.Map<ProjectFilter>(request.Filter);
            filter.OwnerId = new List<Guid> { User.GetUserId() };
            var paging = _mapper.Map<Paging>(request.Paging);

            var projects = await _projectRepository.GetProjects(filter, paging);
            var categories = await _projectRepository.GetCategoriesByIdsAsync(projects.Select(x => x.CategoryId)
                .Distinct()
                .ToList());

            var cards = new List<ProjectCard>();
            foreach(var project in projects)
            {
                cards.Add(await MapToCardAsync(project, categories));
            }

            return new ReplyMessage<List<ProjectCard>>
            {
                Value = cards
            };
        }

        private async Task<ProjectCard> MapToCardAsync(Project project, List<Category> categories)
        {
            PrepareProjectImage(project);
            var card = _mapper.Map<ProjectCard>(project);
            card.CurrentResult = await GetProjectProgressAsync(project.Id);
            if(card.CategoryId.NonNullOrWhiteSpace())
            {
                card.CategoryName = categories.FirstOrDefault(_ => _.Id.ToString() == card.CategoryId)?.Name;
            }
            return card;
        }

        private async Task<decimal> GetProjectProgressAsync(Guid projectId)
        {
            var rewards = await _rewardRepository.GetRewardsByProjectIdAsync(projectId);
            var orders = await _orderRepository.GetOrdersAsync(new OrderFilter
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
            project.Image = $"{_configuration["FileStorageConfiguration:PermanentFolderName"]}/Projects/{project.Id}/{project.Image}";
        }
    }
}
