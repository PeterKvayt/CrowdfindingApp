using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrowdfundingApp.Common.Data.BusinessModels;
using CrowdfundingApp.Common.Data.Interfaces;
using CrowdfundingApp.Common.Data.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CrowdfundingApp.Data.Repositories
{
    public class CountryRepository : RepositoryBase<Country>, ICountryRepository
    {
        protected override DbSet<Country> Repository => Storage.Countries;

        public CountryRepository(IDataProvider storage) : base(storage)
        {

        }

        public async Task<List<Country>> GetByIdsAsync(IEnumerable<Guid> guids)
        {
            return await Repository.Where(x => guids.Contains(x.Id)).ToListAsync();
        }
    }
}
