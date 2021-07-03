using System;
using System.Threading.Tasks;
using AutoMapper;
using CrowdfundingApp.Common.Data.Models;

namespace CrowdfundingApp.Common.Data.Interfaces.Repositories
{
    public interface IRepository<TModel> where TModel : BaseModel
    {
        Task<Guid> AddAsync(TModel model);

        Task UpdateAsync(TModel changes, IMapper mapper, TModel target = null);

        Task<TModel> GetByIdAsync(Guid id);
    }
}
