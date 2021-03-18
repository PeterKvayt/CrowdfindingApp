using System;
using CrowdfindingApp.Data.Common.Models;

namespace CrowdfindingApp.Data.Common.BusinessModels
{
    public sealed class Reward : BaseModel
    {
        public Guid ProjectId { get; set; }
        public string Title { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public bool IsLimited { get; set; }
        public int? Limit { get; set; }
        public string Image { get; set; }
        public int DeliveryType { get; set; }
    }
}
