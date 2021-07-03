
using CrowdfundingApp.Common.Data.Models;

namespace CrowdfundingApp.Common.Data.BusinessModels
{
    public sealed class Role : BaseModel
    {
        public string Name { get; set; }
        public string Permissions { get; set; }
    }
}
