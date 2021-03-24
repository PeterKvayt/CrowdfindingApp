using System;
using CrowdfindingApp.Common.Enums;

namespace CrowdfindingApp.Common.DataTransfers.Orders
{
    public class OrderInfo
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string RewardId { get; set; }
        public int Count { get; set; }
        public bool IsPrivate { get; set; }
        public int PaymentMethod { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime PaymentDateTime { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string CountryId { get; set; }
        public string FullAddress { get; set; }
        public string PostCode { get; set; }
    }
}
