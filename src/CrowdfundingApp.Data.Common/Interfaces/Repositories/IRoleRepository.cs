using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CrowdfundingApp.Common.Data.BusinessModels;

namespace CrowdfundingApp.Common.Data.Interfaces.Repositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task<Dictionary<Guid, string>> GetNamesAsync();
    }
}
