
using CrowdfundingApp.Common.Data.Models;

namespace CrowdfundingApp.Common.Data.BusinessModels
{
    public sealed class OrderAddress : BaseModel
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string FullAddress { get; set; }
        public string PostCode { get; set; }
    }
}
