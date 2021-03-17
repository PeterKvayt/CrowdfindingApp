using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CrowdfindingApp.Data.Common.BusinessModels;
using CrowdfindingApp.Data.Common.Filters;
using CrowdfindingApp.Data.Common.Models;

namespace CrowdfindingApp.Data.Common.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmailAsync(string userName);
        Task<List<User>> GetUsersAsync(UserFilter filter);
        Task InsertUserAsync(User user);
        Task<User> GetUserByIdAsync(Guid id);
        Task UpdatePasswordAsync(Guid id, string passwordHash, string salt);

        /// <summary>
        /// Update user info. Search user by id.
        /// </summary>
        /// <param name="user">Already updated user. </param>
        /// <returns></returns>
        Task UpdateUserAsync(User user);
    }
}
