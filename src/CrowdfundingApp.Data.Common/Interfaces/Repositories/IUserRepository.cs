using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CrowdfundingApp.Common.Data.BusinessModels;
using CrowdfundingApp.Common.Data.Filters;

namespace CrowdfundingApp.Common.Data.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserByEmailAsync(string userName);
        Task<List<User>> GetUsersAsync(UserFilter filter);
        Task UpdatePasswordAsync(Guid id, string passwordHash, string salt);
    }
}
