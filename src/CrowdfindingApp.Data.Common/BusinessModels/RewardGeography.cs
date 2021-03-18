using System;
using CrowdfindingApp.Data.Common.Models;

namespace CrowdfindingApp.Data.Common.BusinessModels
{
    public sealed class RewardGeography : BaseModel
    {
        public RewardGeography()
        {

        }

        public RewardGeography(Guid rewardId, Guid countryId, decimal? price) : base()
        {
            RewardId = rewardId;
            CountryId = countryId;
            Price = price.Value;
        }

        public Guid RewardId { get; set; }
        public Guid CountryId { get; set; }
        public decimal Price { get; set; }
    }
}
