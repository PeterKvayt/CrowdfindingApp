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

namespace CrowdfindingApp.Core.Services.Projects.Handlers
{
    public class ProjectCardSearchRequestHandler : NullOperationContextRequestHandler<ProjectCardSearchRequestMessage, ReplyMessage<List<ProjectCard>>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public ProjectCardSearchRequestHandler(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository ?? throw new NullReferenceException(nameof(projectRepository));
            _mapper = mapper ?? throw new NullReferenceException(nameof(mapper));
        }

        protected override async Task<ReplyMessage<List<ProjectCard>>> ExecuteAsync(ProjectCardSearchRequestMessage request)
        {
            var filter = _mapper.Map<ProjectFilter>(request.Filter);
            filter.OwnerId = new List<Guid> { User.GetUserId() };
            var paging = _mapper.Map<Paging>(request.Paging);

            var projects = await _projectRepository.GetProjects(filter, paging);
            var categories = await _projectRepository.GetCategoriesByIdsAsync(projects.Select(x => x.CategoryId).Distinct().ToList());

            var cards = projects.Select(x => MapToCard(x, categories)).ToList();

            return new ReplyMessage<List<ProjectCard>>
            {
                Value = cards
            };
        }

        private ProjectCard MapToCard(Project project, List<Category> categories)
        {
            var card = _mapper.Map<ProjectCard>(project);
            if(card.CategoryId.NonNullOrWhiteSpace())
            {
                card.CategoryName = categories.FirstOrDefault(_ => _.Id.ToString() == card.CategoryId)?.Name;
            }
            return card;
        }
    }
}
