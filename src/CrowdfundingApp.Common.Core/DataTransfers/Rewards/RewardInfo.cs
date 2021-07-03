using System;
using System.Collections.Generic;
using CrowdfindingApp.Common.Enums;

namespace CrowdfindingApp.Common.Core.DataTransfers.Rewards
{
    public class RewardInfo
    {
        public string Id { get; set; }
        public string ProjectId { get; set; }
        public string Title { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public bool IsLimited { get; set; }
        public int? Limit { get; set; }
        public string Image { get; set; }
        public DeliveryType? DeliveryType { get; set; }
        public List<KeyValue<string, decimal?>> DeliveryCountries { get; set; }
    }
}
