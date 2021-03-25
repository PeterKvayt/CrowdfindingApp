using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CrowdfindingApp.Data.Common.BusinessModels;

namespace CrowdfindingApp.Data.Common.Interfaces.Repositories
{
    public interface ICountryRepository : IRepository<Country>
    {
        public Task<List<Country>> GetByIdsAsync(IEnumerable<Guid> guids);
    }
}
