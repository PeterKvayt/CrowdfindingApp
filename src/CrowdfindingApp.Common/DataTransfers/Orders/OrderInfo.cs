using System;
using CrowdfindingApp.Common.Enums;

namespace CrowdfindingApp.Common.DataTransfers.Orders
{
    public class OrderInfo
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string RewardId { get; set; }
        public bool IsPrivate { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public bool PaymentStatus { get; set; }
        public DateTime PaymentDateTime { get; set; }
    }
}
