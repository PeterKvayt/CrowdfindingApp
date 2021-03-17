using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrowdfindingApp.Common.Extensions;
using CrowdfindingApp.Data.Common.BusinessModels;
using CrowdfindingApp.Data.Common.Extensions;
using CrowdfindingApp.Data.Common.Filters;
using CrowdfindingApp.Data.Common.Interfaces;
using CrowdfindingApp.Data.Common.Interfaces.Repositories;
using CrowdfindingApp.Data.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace CrowdfindingApp.Data.Repositories
{
    public class ProjectRepository : RepositoryBase<Project>, IProjectRepository
    {
        public ProjectRepository(IDataProvider storage) : base(storage)
        {

        }

        private IQueryable<Project> GetQuery(ProjectFilter filter)
        {
            if(filter == null)
            {
                throw new ArgumentNullException("ProjectFilter was null");
            }

            var query = GetQuery();

            if(filter.Id.AnyNonEmpty())
            {
                query = query.Where(_ => filter.Id.Contains(_.Id));
            }

            if(filter.OwnerId.AnyNonEmpty())
            {
                query = query.Where(_ => filter.OwnerId.Contains(_.OwnerId));
            }

            if(filter.Title.AnyNonEmptyOrWhitespace())
            {
                query = query.Where(_ => filter.Title.Contains(_.Title));
            }

            if(filter.CategoryId.AnyNonEmpty())
            {
                query = query.Where(_ => filter.CategoryId.Contains(_.CategoryId));
            }

            return query;
        }

        public async Task<List<Project>> GetProjects(ProjectFilter filter, Paging paging)
        {
            return await GetQuery(filter).ToPagedListAsync(paging);
        }

        public async Task<Project> GetById(Guid id)
        {
            return await Storage.Projects.FirstOrDefaultAsync(_ => _.Id == id);
        }

        public async Task<Guid> InsertDraftProject(Project project)
        {
            project.Id = new Guid();
            await Storage.Projects.AddAsync(project);
            await Storage.SaveChangesAsync();

            return project.Id;
        }

        public async Task UpdateDraftProject(Project project)
        {
            Storage.Projects.Update(project);
            await Storage.SaveChangesAsync();
        }

        public async Task<List<Country>> GetCountriesAsync()
        {
            return await Storage.Countries.ToListAsync();
        }

        public async Task<List<City>> GetCitiesAsync()
        {
            return await Storage.Cities.ToListAsync();
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await Storage.Categories.ToListAsync();
        }
    }
}
