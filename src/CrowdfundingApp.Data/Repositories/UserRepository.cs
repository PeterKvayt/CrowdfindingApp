﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrowdfundingApp.Common.Extensions;
using CrowdfundingApp.Common.Data.BusinessModels;
using CrowdfundingApp.Common.Data.Filters;
using CrowdfundingApp.Common.Data.Interfaces;
using CrowdfundingApp.Common.Data.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CrowdfundingApp.Data.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        protected override DbSet<User> Repository => Storage.Users;

        public UserRepository(IDataProvider storage) : base(storage)
        {
            
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await Storage.Users.FirstOrDefaultAsync(_ => _.Email == email);
        }

        public async Task UpdatePasswordAsync(Guid id, string passwordHash, string salt)
        {
            var user = await GetByIdAsync(id);
            if(user == null)
            {
                throw new ArgumentException($"User with id: {id} not exists.");
            }

            user.PasswordHash = passwordHash;
            user.Salt = salt;

            Storage.Users.Update(user);
            await Storage.SaveChangesAsync();
        }

        public Task<List<User>> GetUsersAsync(UserFilter filter)
        {
            return GetQuery(filter).ToListAsync();
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
