using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CrowdfindingApp.Core.Interfaces.Data;
using CrowdfindingApp.Core.Interfaces.Data.Repositories;
using CrowdfindingApp.Core.Models;
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
