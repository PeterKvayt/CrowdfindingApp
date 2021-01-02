using System.Threading.Tasks;
using CrowdfindingApp.Core.Interfaces.Data;
using CrowdfindingApp.Core.Interfaces.Data.Repositories;
using CrowdfindingApp.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CrowdfindingApp.Data.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public UserRepository(IDataProvider storage) : base(storage)
        {
            
        }

        public async Task<User> GetUserByUserNameOrNullAsync(string userName)
        {
            return await Storage.Users.FirstOrDefaultAsync(_ => _.UserName == userName);
        }
    }
}
