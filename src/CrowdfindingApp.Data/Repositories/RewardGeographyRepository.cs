using System;
using System.Collections.Generic;
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

        public async Task AddRange(List<RewardGeography> geographies)
        {
            foreach(var geo in geographies)
            {
                geo.Id = new Guid();
            }
            await Repository.AddRangeAsync(geographies);
            await Storage.SaveChangesAsync();
        }
    }
}
