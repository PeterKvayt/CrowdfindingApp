using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CrowdfindingApp.Data.Common.BusinessModels;

namespace CrowdfindingApp.Data.Common.Interfaces.Repositories
{
    public interface IRewardRepository
    {
        Task AddRewards(List<Reward> rewards);

        Task<List<Reward>> GetRewardsByProjectId(Guid guid);

        Task UpdateRewards(List<Reward> rewards);
    }
}
