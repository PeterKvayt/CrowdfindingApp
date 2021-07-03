using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CrowdfundingApp.Common.Data.BusinessModels;

namespace CrowdfundingApp.Common.Data.Interfaces.Repositories
{
    public interface IRewardRepository : IRepository<Reward>
    {
        Task<List<Reward>> GetRewardsByProjectIdAsync(Guid guid);
        Task<List<Reward>> GetByIdsAsync(IEnumerable<Guid> guids);
        Task RemoveByProjectAsync(Guid projectId);
    }
}
