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

        public async Task<List<RewardGeography>> GetByRewardIdAsync(Guid id)
        {
            return await GetQuery().Where(x => x.RewardId == id).ToListAsync();
        }

        public async Task<List<RewardGeography>> GetListAsync(IEnumerable<Guid> rewardIds, IEnumerable<Guid> countryIds)
        {
            var query = GetQuery();

            if(rewardIds?.Any() ?? false)
            {
                query = query.Where(x => rewardIds.Contains(x.RewardId));
            }

            if(countryIds?.Any() ?? false)
            {
                query = query.Where(x => countryIds.Contains(x.CountryId));
            }

            return await query.ToListAsync();
        }
    }
}
