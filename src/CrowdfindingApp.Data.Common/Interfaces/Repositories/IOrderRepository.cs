using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CrowdfindingApp.Data.Common.BusinessModels;
using CrowdfindingApp.Data.Common.Filters;
using CrowdfindingApp.Data.Common.Models;

namespace CrowdfindingApp.Data.Common.Interfaces.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<List<Order>> GetOrdersAsync(OrderFilter filter, Paging paging = null);

        Task<List<Order>> GetByRewardIdAsync(Guid rewardId);
    }
}
