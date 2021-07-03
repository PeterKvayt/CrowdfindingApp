using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CrowdfindingApp.Common.Data.BusinessModels;

namespace CrowdfindingApp.Common.Data.Interfaces.Repositories
{
    public interface ICountryRepository : IRepository<Country>
    {
        public Task<List<Country>> GetByIdsAsync(IEnumerable<Guid> guids);
    }
}
