using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CrowdfindingApp.Common.Data.BusinessModels;
using CrowdfindingApp.Common.Data.Filters;
using CrowdfindingApp.Common.Data.Models;

namespace CrowdfindingApp.Common.Data.Interfaces.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<List<Order>> GetOrdersAsync(OrderFilter filter, Paging paging = null);

        Task<List<Order>> GetByRewardIdAsync(Guid rewardId);

        Task<int> GetOrdersCountByRewardIdAsync(Guid rewardId);
    }
}
