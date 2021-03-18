
using System;
using System.Linq;
using System.Threading.Tasks;
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

        public virtual async Task<Guid> Add(TModel model)
        {
            model.Id = new Guid();
            await Repository.AddAsync(model);
            await Storage.SaveChangesAsync();
            return model.Id;
        }

        public virtual async Task Update(TModel model)
        {
            Repository.Update(model);
            await Storage.SaveChangesAsync();
        }

        public virtual async Task<TModel> GetById(Guid id)
        {
            return await Repository.FirstOrDefaultAsync(x => x.Id == id);
        }

        protected IQueryable<TModel> GetQuery()
        {
            return Repository;
        }
    }
}
