using System;
using System.Threading.Tasks;
using CrowdfindingApp.Core.Interfaces.Data;
using CrowdfindingApp.Core.Interfaces.Data.Repositories;
using CrowdfindingApp.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CrowdfindingApp.Data.Repositories
{
    public class RoleRepository : RepositoryBase, IRoleRepository
    {
        public RoleRepository(IDataProvider storage) : base(storage)
        {

        }

        public async Task<Role> GetRoleByIdOrNullAsync(Guid guid)
        {
            return await Storage.Roles.FirstOrDefaultAsync(_ => _.Id == guid);
        }
    }
}
