
using CrowdfindingApp.Data.Common.Models;

namespace CrowdfindingApp.Data.Common.BusinessModels
{
    public sealed class Role : BaseModel
    {
        public string Name { get; set; }
        public string Permissions { get; set; }
    }
}
