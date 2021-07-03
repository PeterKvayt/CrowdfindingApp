using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrowdfundingApp.Common.Extensions;
using CrowdfundingApp.Common.Data.BusinessModels;
using CrowdfundingApp.Common.Data.Extensions;
using CrowdfundingApp.Common.Data.Filters;
using CrowdfundingApp.Common.Data.Interfaces;
using CrowdfundingApp.Common.Data.Interfaces.Repositories;
using CrowdfundingApp.Common.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CrowdfundingApp.Data.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        protected override DbSet<Order> Repository => Storage.Orders;

        public OrderRepository(IDataProvider storage) : base(storage)
        {

        }

        private IQueryable<Order> GetQuery(OrderFilter filter)
        {
            var query = GetQuery();

            if(filter.Id.AnyNonEmpty())
            {
                query = query.Where(x => filter.Id.Contains(x.Id));
            }

            if(filter.RewardId.AnyNonEmpty())
            {
                query = query.Where(x => filter.RewardId.Contains(x.RewardId));
            }

            if(filter.UserId.AnyNonEmpty())
            {
                query = query.Where(x => filter.UserId.Contains(x.UserId));
            }

            return query;
        }

        public async Task<List<Order>> GetOrdersAsync(OrderFilter filter, Paging paging = null)
        {
            return await GetQuery(filter).ToPagedListAsync(paging);
        }

        public async Task<List<Order>> GetByRewardIdAsync(Guid rewardId)
        {
            return await GetQuery().Where(x => x.RewardId == rewardId).ToListAsync();
        }

        public async Task<int> GetOrdersCountByRewardIdAsync(Guid rewardId)
        {
            return await Task.Run(() => GetQuery().Where(x => x.RewardId == rewardId).Sum(x => x.Count));
        }
    }
}
