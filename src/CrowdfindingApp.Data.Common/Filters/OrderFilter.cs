using System;
using System.Collections.Generic;

namespace CrowdfindingApp.Data.Common.Filters
{
    public class OrderFilter
    {
        public List<Guid> Id { get; set; }
        public List<Guid> RewardId { get; set; }
    }
}
