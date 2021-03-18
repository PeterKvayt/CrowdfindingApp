using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CrowdfindingApp.Data.Common.BusinessModels;
using CrowdfindingApp.Data.Common.Filters;

namespace CrowdfindingApp.Data.Common.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserByEmailAsync(string userName);
        Task<List<User>> GetUsersAsync(UserFilter filter);
        Task UpdatePasswordAsync(Guid id, string passwordHash, string salt);
    }
}
