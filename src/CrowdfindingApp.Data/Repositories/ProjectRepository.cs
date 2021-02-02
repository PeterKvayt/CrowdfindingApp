using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrowdfindingApp.Common.Extensions;
using CrowdfindingApp.Data.Common.Extensions;
using CrowdfindingApp.Data.Common.Filters;
using CrowdfindingApp.Data.Common.Interfaces;
using CrowdfindingApp.Data.Common.Interfaces.Repositories;
using CrowdfindingApp.Data.Common.Models;

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
    }
}
