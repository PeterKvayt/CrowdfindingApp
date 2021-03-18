using System;
using System.Threading.Tasks;
using CrowdfindingApp.Data.Common.Models;

namespace CrowdfindingApp.Data.Common.Interfaces.Repositories
{
    public interface IRepository<TModel> where TModel : BaseModel
    {
        Task<Guid> Add(TModel model);

        Task Update(TModel model);

        Task<TModel> GetById(Guid id);
    }
}
