using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CrowdfundingApp.Common.Data.BusinessModels;
using CrowdfundingApp.Common.Data.Filters;
using CrowdfundingApp.Common.Data.Models;

namespace CrowdfundingApp.Common.Data.Interfaces.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<List<Order>> GetOrdersAsync(OrderFilter filter, Paging paging = null);

        Task<List<Order>> GetByRewardIdAsync(Guid rewardId);

        Task<int> GetOrdersCountByRewardIdAsync(Guid rewardId);
    }
}
