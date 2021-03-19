using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrowdfindingApp.Data.Common.BusinessModels;
using CrowdfindingApp.Data.Common.Interfaces;
using CrowdfindingApp.Data.Common.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CrowdfindingApp.Data.Repositories
{
    public class RewardRepository : RepositoryBase<Reward>, IRewardRepository
    {
        protected override DbSet<Reward> Repository => Storage.Rewards;
        public RewardRepository(IDataProvider storage) : base(storage)
        {

        }

        public async Task<List<Reward>> GetRewardsByProjectIdAsync(Guid guid)
        {
            return await GetQuery().Where(x => x.ProjectId == guid).ToListAsync();
        }
    }
}
