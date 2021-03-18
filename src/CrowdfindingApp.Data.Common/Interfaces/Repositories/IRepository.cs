using System;
using System.Threading.Tasks;
using CrowdfindingApp.Data.Common.Models;

namespace CrowdfindingApp.Data.Common.Interfaces.Repositories
{
    public interface IRepository<TModel> where TModel : BaseModel
    {
        Task<Guid> AddAsync(TModel model);

        Task UpdateAsync(TModel model);

        Task<TModel> GetByIdAsync(Guid id);
    }
}
