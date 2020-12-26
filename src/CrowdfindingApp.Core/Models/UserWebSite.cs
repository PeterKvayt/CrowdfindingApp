using System;

namespace CrowdfindingApp.Core.Models
{
    public sealed class UserWebSite : BaseModel
    {
        public Guid UserId { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
    }
}
