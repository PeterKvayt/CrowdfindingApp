﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CrowdfindingApp.Common.DataTransfers.Projects;
using CrowdfindingApp.Common.Enums;
using CrowdfindingApp.Common.Extensions;
using CrowdfindingApp.Common.Messages;
using CrowdfindingApp.Common.Messages.Projects;
using CrowdfindingApp.Data.Common.Filters;
using CrowdfindingApp.Data.Common.Interfaces.Repositories;
using CrowdfindingApp.Data.Common.Models;
using Microsoft.Extensions.Configuration;

namespace CrowdfindingApp.Core.Services.Projects.Handlers
{
    public class OpenedProjectCardSearchRequestHandler : ProjectCardSearchRequestHandlerBase
    {
        public OpenedProjectCardSearchRequestHandler(IProjectRepository projectRepository, IMapper mapper, IRewardRepository rewardRepository, IOrderRepository orderRepository,
            IConfiguration configuration) : base(projectRepository, mapper, rewardRepository, orderRepository, configuration)
        {
        }

        protected override async Task<PagedReplyMessage<List<ProjectCard>>> ExecuteAsync(ProjectSearchRequestMessage request)
        {
            var filter = Mapper.Map<ProjectFilter>(request.Filter);
            SetRestrictions(filter);
            var paging = Mapper.Map<Paging>(request.Paging);

            return await SearchAsync(filter, paging);
        }

        private void SetRestrictions(ProjectFilter filter)
        {
            filter = filter ?? new ProjectFilter();
            filter.Status = new List<int> { (int)ProjectStatus.Active, (int)ProjectStatus.Complited };
        }
    }
}
