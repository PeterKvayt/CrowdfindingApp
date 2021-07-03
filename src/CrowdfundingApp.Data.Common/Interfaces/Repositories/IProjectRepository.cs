using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CrowdfundingApp.Common.Data.BusinessModels;
using CrowdfundingApp.Common.Data.Filters;
using CrowdfundingApp.Common.Data.Models;

namespace CrowdfundingApp.Common.Data.Interfaces.Repositories
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
