using System;

namespace CrowdfindingApp.Core.Models
{
    public sealed class Order : BaseModel
    {
        public Guid UserId { get; set; }
        public Guid RewardId { get; set; }
        public bool IsPrivate { get; set; }
        public int PaymentMethod { get; set; }
        public bool PaymentStatus { get; set; }
        public DateTime PaymentDateTime { get; set; }
    }
}
