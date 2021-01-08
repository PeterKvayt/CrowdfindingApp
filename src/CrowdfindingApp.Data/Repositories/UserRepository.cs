using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrowdfindingApp.Common.Extensions;
using CrowdfindingApp.Core.Interfaces.Data;
using CrowdfindingApp.Core.Interfaces.Data.Repositories;
using CrowdfindingApp.Core.Models;
using CrowdfindingApp.Core.Services.User.Filters;
using Microsoft.EntityFrameworkCore;

namespace CrowdfindingApp.Data.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(IDataProvider storage) : base(storage)
        {
            
        }

        public async Task<User> GetUserByUserNameOrNullAsync(string userName)
        {
            return await Storage.Users.FirstOrDefaultAsync(_ => _.UserName == userName);
        }

        public Task<List<User>> GetUsersAsync(UserFilter filter)
        {
            return GetQuery(filter).ToListAsync();
        }

        public async Task InsertUserAsync(User user)
        {
            await Storage.Users.AddAsync(user);
            await Storage.SaveChangesAsync();
        }

        private IQueryable<User> GetQuery(UserFilter filter)
        {
            var query = GetQuery();

            if(filter.Active.HasValue)
            {
                query = query.Where(x => x.Active == filter.Active.Value);
            }

            if(filter.CreatedDateTimeFrom.HasValue)
            {
                query = query.Where(x => x.CreatedDateTime >= filter.CreatedDateTimeFrom.Value);
            }

            if(filter.CreatedDateTimeTo.HasValue)
            {
                query = query.Where(x => x.CreatedDateTime <= filter.CreatedDateTimeTo.Value);
            }

            if(filter.Email?.AnyNonEmptyOrWhitespace() ?? false)
            {
                var emails = filter.Email.Select(_ => _.ToUpperInvariant()).ToList();
                query = query.Where(x => emails.Contains(x.Email));
            }

            if(filter.RoleId?.AnyNonEmpty() ?? false)
            {
                query = query.Where(x => filter.RoleId.Contains(x.RoleId));
            }

            if(filter.Id?.AnyNonEmpty() ?? false)
            {
                query = query.Where(x => filter.Id.Contains(x.Id));
            }

            if(filter.EmailConfirmed.HasValue)
            {
                query = query.Where(x => x.EmailConfirmed == filter.EmailConfirmed.Value);
            }

            return query;
        }
    }
}
