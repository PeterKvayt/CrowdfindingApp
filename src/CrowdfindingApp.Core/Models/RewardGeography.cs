using System;

namespace CrowdfindingApp.Core.Models
{
    public sealed class RewardGeography : BaseModel
    {
        public Guid RewardId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
