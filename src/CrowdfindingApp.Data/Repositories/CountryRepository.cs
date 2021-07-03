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
