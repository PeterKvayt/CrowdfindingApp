using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CrowdfindingApp.Data.Common.BusinessModels;
using CrowdfindingApp.Data.Common.Interfaces;
using CrowdfindingApp.Data.Common.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CrowdfindingApp.Data.Repositories
{
    public class RoleRepository : RepositoryBase<Role>, IRoleRepository
    {
        protected override DbSet<Role> Repository => Storage.Roles;

        public RoleRepository(IDataProvider storage) : base(storage)
        {

        }


        public Task<Dictionary<Guid, string>> GetNamesAsync()
        {
            return GetQuery()
                .ToDictionaryAsync(x => x.Id, x => x.Name);
        }
    }
}
