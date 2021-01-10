﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CrowdfindingApp.Core.Models;
using CrowdfindingApp.Core.Services.User.Filters;

namespace CrowdfindingApp.Core.Interfaces.Data.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmailAsync(string userName);
        Task<List<User>> GetUsersAsync(UserFilter filter);
        Task InsertUserAsync(User user);
        Task<User> GetUserByIdAsync(Guid id);
        Task UpdatePasswordAsync(Guid id, string passwordHash, string salt);
    }
}
