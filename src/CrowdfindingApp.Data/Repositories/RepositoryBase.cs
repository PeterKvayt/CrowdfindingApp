
using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CrowdfindingApp.Data.Common.Interfaces;
using CrowdfindingApp.Data.Common.Interfaces.Repositories;
using CrowdfindingApp.Data.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace CrowdfindingApp.Data.Repositories
{
    public abstract class RepositoryBase<TModel> : IRepository<TModel> where TModel : BaseModel
    {
        protected abstract DbSet<TModel> Repository { get; }

        protected IDataProvider Storage { get; }

        public RepositoryBase(IDataProvider storage)
        {
            Storage = storage ?? throw new ArgumentException(nameof(storage));
        }

        public virtual async Task<Guid> AddAsync(TModel model)
        {
            model.Id = new Guid();
            await Repository.AddAsync(model);
            await Storage.SaveChangesAsync();
            return model.Id;
        }

        public virtual async Task<TModel> GetByIdAsync(Guid id)
        {
            return await Repository.FirstOrDefaultAsync(x => x.Id == id);
        }

        protected IQueryable<TModel> GetQuery()
        {
            return Repository;
        }

        public virtual async Task UpdateAsync(TModel changes, IMapper mapper)
        {
            var modelFromDb = await GetByIdAsync(changes.Id);
            mapper.Map(changes, modelFromDb);
            Repository.Update(modelFromDb);
            await Storage.SaveChangesAsync();
        }

    }
}
