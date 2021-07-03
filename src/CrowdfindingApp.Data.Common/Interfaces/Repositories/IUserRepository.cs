using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CrowdfindingApp.Common.Data.BusinessModels;
using CrowdfindingApp.Common.Data.Filters;

namespace CrowdfindingApp.Common.Data.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserByEmailAsync(string userName);
        Task<List<User>> GetUsersAsync(UserFilter filter);
        Task UpdatePasswordAsync(Guid id, string passwordHash, string salt);
    }
}
