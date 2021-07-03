using System;
using CrowdfindingApp.Common.Data.Models;

namespace CrowdfindingApp.Common.Data.BusinessModels
{
    public sealed class Order : BaseModel
    {
        public Guid UserId { get; set; }
        public Guid RewardId { get; set; }
        public int Count { get; set; }
        public bool IsPrivate { get; set; }
        public int PaymentMethod { get; set; }
        public int Status { get; set; }
        public DateTime PaymentDateTime { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public Guid? CountryId { get; set; }
        public string FullAddress { get; set; }
        public string PostCode { get; set; }
    }
}
