using System;
using System.Threading.Tasks;
using AutoMapper;
using CrowdfindingApp.Common.Data.Models;

namespace CrowdfindingApp.Common.Data.Interfaces.Repositories
{
    public interface IRepository<TModel> where TModel : BaseModel
    {
        Task<Guid> AddAsync(TModel model);

        Task UpdateAsync(TModel changes, IMapper mapper, TModel target = null);

        Task<TModel> GetByIdAsync(Guid id);
    }
}
