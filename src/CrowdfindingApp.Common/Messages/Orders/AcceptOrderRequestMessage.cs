
using System;

namespace CrowdfindingApp.Common.Messages.Orders
{
    public class AcceptOrderRequestMessage : MessageBase
    {
        public string RewardId { get; set; }
        public int Count { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string CountryId { get; set; }
        public string FullAddress { get; set; }
        public string PostCode { get; set; }
        public string PayCardNumber { get; set; }
        public string PayCardOwnerName { get; set; }
        public string PayCardCvv { get; set; }
        public DateTime? PayCardExpirationDate { get; set; }
    }
}
