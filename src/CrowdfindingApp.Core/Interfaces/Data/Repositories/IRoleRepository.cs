using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CrowdfindingApp.Core.Models;

namespace CrowdfindingApp.Core.Interfaces.Data.Repositories
{
    public interface IRoleRepository
    {
        Task<Role> GetRoleByIdOrNullAsync(Guid guid);

        Task<Dictionary<Guid, string>> GetNamesAsync();
    }
}
