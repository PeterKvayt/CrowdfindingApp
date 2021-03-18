using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CrowdfindingApp.Data.Common.BusinessModels;

namespace CrowdfindingApp.Data.Common.Interfaces.Repositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task<Dictionary<Guid, string>> GetNamesAsync();
    }
}
