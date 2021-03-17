using System;
using CrowdfindingApp.Data.Common.Models;

namespace CrowdfindingApp.Data.Common.BusinessModels
{
    public sealed class RewardGeography : BaseModel
    {
        public Guid RewardId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
