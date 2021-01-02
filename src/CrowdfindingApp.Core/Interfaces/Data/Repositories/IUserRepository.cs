using System.Threading.Tasks;
using CrowdfindingApp.Core.Models;

namespace CrowdfindingApp.Core.Interfaces.Data.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByUserNameOrNullAsync(string userName);
    }
}
