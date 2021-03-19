using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CrowdfindingApp.Data.Common.BusinessModels;

namespace CrowdfindingApp.Data.Common.Interfaces.Repositories
{
    public interface IRewardRepository : IRepository<Reward>
    {
        Task<List<Reward>> GetRewardsByProjectIdAsync(Guid guid);
        Task RemoveByProjectAsync(Guid projectId);
    }
}
