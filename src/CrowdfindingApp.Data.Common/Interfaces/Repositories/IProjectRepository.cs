using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CrowdfindingApp.Data.Common.Filters;
using CrowdfindingApp.Data.Common.Models;

namespace CrowdfindingApp.Data.Common.Interfaces.Repositories
{
    public interface IProjectRepository
    {
        Task<List<Project>> GetProjects(ProjectFilter filter, Paging paging);
        Task<Project> GetById(Guid id);
        Task<Guid> InsertDraftProject(Project project);
        Task UpdateDraftProject(Project project);
    }
}
