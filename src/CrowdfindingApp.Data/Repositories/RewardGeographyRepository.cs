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
    public class RewardGeographyRepository : RepositoryBase<RewardGeography>, IRewardGeographyRepository
    {
        protected override DbSet<RewardGeography> Repository => Storage.RewardGeographies;

        public RewardGeographyRepository(IDataProvider storage) : base(storage)
        {

        }

        public async Task SubstituteRangeAsync(List<RewardGeography> geographies, Guid rewardId)
        {
            if(geographies.Any())
            {
                var legacyCollection = await GetQuery().Where(x => x.RewardId == rewardId).ToListAsync();
                Repository.RemoveRange(legacyCollection);

                foreach(var geo in geographies)
                {
                    geo.Id = new Guid();
                }
                await Repository.AddRangeAsync(geographies);
                await Storage.SaveChangesAsync();
            }
        }
    }
}
