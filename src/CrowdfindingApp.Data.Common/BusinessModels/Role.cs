
using CrowdfindingApp.Common.Data.Models;

namespace CrowdfindingApp.Common.Data.BusinessModels
{
    public sealed class Role : BaseModel
    {
        public string Name { get; set; }
        public string Permissions { get; set; }
    }
}
