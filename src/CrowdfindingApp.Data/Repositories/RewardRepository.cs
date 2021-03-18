using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrowdfindingApp.Data.Common.BusinessModels;
using CrowdfindingApp.Data.Common.Interfaces;

namespace CrowdfindingApp.Data.Repositories
{
    public class RewardRepository : RepositoryBase<Reward>
    {
        public RewardRepository(IDataProvider storage) : base(storage)
        {

        }
    }
}
