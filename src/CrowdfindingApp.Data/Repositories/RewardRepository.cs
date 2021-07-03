using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrowdfindingApp.Common.Data.BusinessModels;
using CrowdfindingApp.Common.Data.Interfaces;
using CrowdfindingApp.Common.Data.Interfaces.Repositories;
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

        public async Task RemoveByProjectAsync(Guid projectId)
        {
            var rewards = await Repository.Where(x => x.ProjectId == projectId).ToListAsync();
            var deliveryCountries = await Storage.RewardGeographies.Where(x => rewards.Select(x => x.Id).Contains(x.RewardId)).ToListAsync();
            Storage.RewardGeographies.RemoveRange(deliveryCountries);
            Repository.RemoveRange(rewards);
            await Storage.SaveChangesAsync();
        }

        public async Task<List<Reward>> GetByIdsAsync(IEnumerable<Guid> guids)
        {
            return await GetQuery().Where(x => guids.Contains(x.Id)).ToListAsync();
        }
    }
}
