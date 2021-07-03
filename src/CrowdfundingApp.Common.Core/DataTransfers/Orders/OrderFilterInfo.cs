using System.Collections.Generic;

namespace CrowdfundingApp.Common.Core.DataTransfers.Orders
{
    public class OrderFilterInfo
    {
        public List<string> Id { get; set; }
        public List<string> ProjectId { get; set; }
        public List<string> UserId { get; set; }

    }
}
