using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CrowdfindingApp.Common.Data.BusinessModels;

namespace CrowdfindingApp.Common.Data.Interfaces.Repositories
{
    public interface IRewardGeographyRepository : IRepository<RewardGeography>
    {
        Task SubstituteRangeAsync(List<RewardGeography> geographies, Guid rewardId);
        Task<List<RewardGeography>> GetByRewardIdAsync(Guid id);
        Task<List<RewardGeography>> GetListAsync(IEnumerable<Guid> rewardIds, IEnumerable<Guid> countryIds);
    }
}
