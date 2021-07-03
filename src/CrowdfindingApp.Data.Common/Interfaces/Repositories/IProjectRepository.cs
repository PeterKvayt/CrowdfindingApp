using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CrowdfindingApp.Common.Data.BusinessModels;
using CrowdfindingApp.Common.Data.Filters;
using CrowdfindingApp.Common.Data.Models;

namespace CrowdfindingApp.Common.Data.Interfaces.Repositories
{
    public interface IProjectRepository : IRepository<Project>
    {
        Task<List<Project>> GetProjectsAsync(ProjectFilter filter, Paging paging);
        Task<List<Country>> GetCountriesAsync();
        Task<List<City>> GetCitiesAsync();
        Task<List<Category>> GetCategoriesAsync();
        Task<List<Category>> GetCategoriesByIdsAsync(List<Guid> ids);
        Task<Project> GetByIdAsync(Guid projectId, Guid ownerId);
        Task SetStatusAsync(int status, IEnumerable<Guid> projectId);
        Task<decimal> GetProgressAsync(Guid projectId);
        Task RemoveAsync(Guid id);
    }
}
