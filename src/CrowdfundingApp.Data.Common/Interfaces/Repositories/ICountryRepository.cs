using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CrowdfundingApp.Common.Data.BusinessModels;

namespace CrowdfundingApp.Common.Data.Interfaces.Repositories
{
    public interface ICountryRepository : IRepository<Country>
    {
        public Task<List<Country>> GetByIdsAsync(IEnumerable<Guid> guids);
    }
}
