using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CrowdfindingApp.Data.Common.BusinessModels;
using CrowdfindingApp.Data.Common.Models;

namespace CrowdfindingApp.Data.Common.Interfaces.Repositories
{
    public interface IRoleRepository
    {
        Task<Role> GetRoleByIdOrNullAsync(Guid guid);

        Task<Dictionary<Guid, string>> GetNamesAsync();
    }
}
