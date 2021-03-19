﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrowdfindingApp.Common.Extensions;
using CrowdfindingApp.Data.Common.BusinessModels;
using CrowdfindingApp.Data.Common.Extensions;
using CrowdfindingApp.Data.Common.Filters;
using CrowdfindingApp.Data.Common.Interfaces;
using CrowdfindingApp.Data.Common.Interfaces.Repositories;
using CrowdfindingApp.Data.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace CrowdfindingApp.Data.Repositories
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

            return query;
        }

        public async Task<List<Order>> GetOrdersAsync(OrderFilter filter, Paging paging = null)
        {
            return await GetQuery(filter).ToPagedListAsync(paging);
        }
    }
}