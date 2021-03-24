
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
    }
}
