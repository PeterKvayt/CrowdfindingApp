
using System;
using System.Linq;
using CrowdfindingApp.Core.Interfaces.Data;

namespace CrowdfindingApp.Data.Repositories
{
    public abstract class RepositoryBase
    {
        protected IDataProvider Storage { get; }

        public RepositoryBase(IDataProvider storage)
        {
            Storage = storage ?? throw new ArgumentException(nameof(storage));
        }
    }
}
