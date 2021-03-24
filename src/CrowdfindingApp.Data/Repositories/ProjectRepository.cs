using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        protected override DbSet<Project> Repository => Storage.Projects;

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

            if(filter.Status?.Any() ?? false)
            {
                query = query.Where(_ => filter.Status.Contains(_.Status));
            }

            return query;
        }

        public async Task<List<Project>> GetProjects(ProjectFilter filter, Paging paging)
        {
            return await GetQuery(filter).ToPagedListAsync(paging);
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

        public async Task<List<Category>> GetCategoriesByIdsAsync(List<Guid> ids)
        {
            return await Storage.Categories.Where(x => ids.Contains(x.Id))
                .ToListAsync();
        }

        public override Task UpdateAsync(Project model, IMapper mapper)
        {
            model.LastModifiedDateTime = DateTime.UtcNow;
            return base.UpdateAsync(model, mapper);
        }

        public async Task<Project> GetByIdAsync(Guid projectId, Guid ownerId)
        {
            return await GetQuery().FirstOrDefaultAsync(x => x.Id == projectId && x.OwnerId == ownerId);
        }

        public async Task SetStatus(int status, Guid projectId)
        {
            var project = await GetQuery().FirstAsync(x => x.Id == projectId);
            project.Status = status;
            Repository.Update(project);
            await Storage.SaveChangesAsync();
        }

        public async Task<decimal> GetProgressAsync(Guid projectId)
        {
            var rewards = await Storage.Rewards.Where(x => x.ProjectId == projectId).ToListAsync();
            if(!rewards?.Any() ?? true)
            {
                return 0;
            }

            var orders = await Storage.Orders.Where(x => rewards.Select(r => r.Id).Contains(x.RewardId)).ToListAsync();
            if(!orders?.Any() ?? true)
            {
                return 0;
            }

            var groupedOrders = orders.GroupBy(x => x.RewardId);
            decimal progress = 0;
            foreach(var group in groupedOrders)
            {
                progress += group.Count() * rewards.First(x => x.Id == group.Key).Price.Value;
            }

            return progress;
        }
    }
}
