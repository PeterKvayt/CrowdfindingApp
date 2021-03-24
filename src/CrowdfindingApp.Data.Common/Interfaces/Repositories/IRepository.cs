using System;
using System.Threading.Tasks;
using AutoMapper;
using CrowdfindingApp.Data.Common.Models;

namespace CrowdfindingApp.Data.Common.Interfaces.Repositories
{
    public interface IRepository<TModel> where TModel : BaseModel
    {
        Task<Guid> AddAsync(TModel model);

        Task UpdateAsync(TModel changes, IMapper mapper, TModel target = null);

        Task<TModel> GetByIdAsync(Guid id);
    }
}
