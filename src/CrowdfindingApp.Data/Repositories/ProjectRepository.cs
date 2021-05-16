using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using CrowdfindingApp.Common.Enums;
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

            if(filter.OrderBy != null)
            {
                if(filter.DescendingOrder)
                {
                    query = query.OrderByDescending(filter.OrderBy);
                }
                else
                {
                    query = query.OrderBy(filter.OrderBy);
                }
            }

            return query;
        }

        public async Task<List<Project>> GetProjectsAsync(ProjectFilter filter, Paging paging)
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

        public override Task UpdateAsync(Project model, IMapper mapper, Project project = null)
        {
            model.LastModifiedDateTime = DateTime.UtcNow;
            return base.UpdateAsync(model, mapper);
        }

        public async Task<Project> GetByIdAsync(Guid projectId, Guid ownerId)
        {
            return await GetQuery().FirstOrDefaultAsync(x => x.Id == projectId && x.OwnerId == ownerId);
        }

        public async Task SetStatusAsync(int status, IEnumerable<Guid> projectIds)
        {
            var projects = await GetQuery().Where(x => projectIds.Contains(x.Id)).ToListAsync();
            foreach(var project in projects)
            {
                project.Status = status;
                ApplyStatus(project);
            }
            Repository.UpdateRange(projects);
            await Storage.SaveChangesAsync();
        }

        private void ApplyStatus(Project project)
        {
            switch(project.Status)
            {
                case (int)ProjectStatus.Active: 
                    project.StartDateTime = DateTime.UtcNow;
                    break;
                default:
                    break;
            }
        }

        public async Task<decimal> GetProgressAsync(Guid projectId)
        {
            var rewards = await Storage.Rewards.Where(x => x.ProjectId == projectId).ToListAsync();
            if(!rewards?.Any() ?? true)
            {
                return 0;
            }

            var orders = await Storage.Orders
                .Where(x => rewards.Select(r => r.Id).Contains(x.RewardId))
                .Where(x => x.Status == (int)OrderStatus.Approved)
                .ToListAsync();
            if(!orders?.Any() ?? true)
            {
                return 0;
            }

            var groupedOrders = orders.GroupBy(x => x.RewardId);
            decimal progress = 0;
            foreach(var group in groupedOrders)
            {
                progress += group.Sum(x => x.Count) * rewards.First(x => x.Id == group.Key).Price.Value;
            }

            return progress;
        }

        public async Task RemoveAsync(Guid id)
        {
            var questions = await Storage.Questions.Where(x => x.ProjectId == id).ToListAsync();
            if(questions?.Any() ?? false)
            {
                Storage.Questions.RemoveRange(questions);
            }

            var rewards = await Storage.Rewards.Where(x => x.ProjectId == id).ToListAsync();
            if(rewards?.Any() ?? false)
            {
                var deliveries = await Storage.RewardGeographies.Where(x => rewards.Select(r => r.Id).Contains(x.RewardId)).ToListAsync();
                if(deliveries?.Any() ?? false)
                {
                    Storage.RewardGeographies.RemoveRange(deliveries);
                }
                Storage.Rewards.RemoveRange(rewards);
            }

            var project = await Repository.FirstOrDefaultAsync(x => x.Id == id);
            if(project != null)
            {
                Repository.Remove(project);
            }
        }
    }
}
