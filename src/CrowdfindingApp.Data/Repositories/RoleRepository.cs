using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CrowdfindingApp.Data.Common.Interfaces;
using CrowdfindingApp.Data.Common.Interfaces.Repositories;
using CrowdfindingApp.Data.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace CrowdfindingApp.Data.Repositories
{
    public class RoleRepository : RepositoryBase<Role>, IRoleRepository
    {
        public RoleRepository(IDataProvider storage) : base(storage)
        {

        }

        public Task<Dictionary<Guid, string>> GetNamesAsync()
        {
            return GetQuery()
                .ToDictionaryAsync(x => x.Id, x => x.Name);
        }

        public Task<Role> GetRoleByIdOrNullAsync(Guid guid)
        {
            return Storage.Roles.FirstOrDefaultAsync(_ => _.Id == guid);
        }
    }
}
