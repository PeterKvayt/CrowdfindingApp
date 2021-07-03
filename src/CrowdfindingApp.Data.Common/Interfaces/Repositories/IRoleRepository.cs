using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CrowdfindingApp.Common.Data.BusinessModels;

namespace CrowdfindingApp.Common.Data.Interfaces.Repositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task<Dictionary<Guid, string>> GetNamesAsync();
    }
}
