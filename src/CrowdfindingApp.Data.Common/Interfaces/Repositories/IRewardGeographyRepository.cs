using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CrowdfindingApp.Data.Common.BusinessModels;

namespace CrowdfindingApp.Data.Common.Interfaces.Repositories
{
    public interface IRewardGeographyRepository : IRepository<RewardGeography>
    {
        Task SubstituteRangeAsync(List<RewardGeography> geographies, Guid rewardId);
        Task<List<RewardGeography>> GetByRewardIdAsync(Guid id);
        Task<List<RewardGeography>> GetListAsync(IEnumerable<Guid> rewardIds, IEnumerable<Guid> countryIds);
    }
}
