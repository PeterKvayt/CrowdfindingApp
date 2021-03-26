using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrowdfindingApp.Common.Enums;
using CrowdfindingApp.Data.Common.Filters;
using CrowdfindingApp.Data.Common.Interfaces.Repositories;

namespace CrowdfindingApp.Core.Services.BackgroundTasks.Jobs
{
    public class UpdateStatusOfExpiriedProjectsJob
    {
        private readonly IProjectRepository _projectRepository;

        public UpdateStatusOfExpiriedProjectsJob(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository ?? throw new NullReferenceException(nameof(projectRepository));
        }

        public async Task Execute()
        {
            var activeProjects = await _projectRepository.GetProjectsAsync(new ProjectFilter
            {
                Status = new List<int> { (int)ProjectStatus.Active, (int)ProjectStatus.Complited }
            }, null);

            if(!activeProjects?.Any() ?? true)
            {
                return;
            }

            var expiriedProjects = activeProjects.Where(x => x.StartDateTime.Value + new TimeSpan(x.Duration.Value, 0, 1, 0, 0) <= DateTime.UtcNow).Select(x => x.Id).ToList();
            await _projectRepository.SetStatusAsync((int)ProjectStatus.Finalized, expiriedProjects);
        }
    }
}
