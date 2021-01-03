using System.Collections.Generic;
using System.Threading.Tasks;
using CrowdfindingApp.Core.Models;
using CrowdfindingApp.Core.Services.User.Filters;

namespace CrowdfindingApp.Core.Interfaces.Data.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByUserNameOrNullAsync(string userName);

        Task<List<User>> GetUsersAsync(UserFilter filter);

        Task InsertUserAsync(User user);
    }
}
