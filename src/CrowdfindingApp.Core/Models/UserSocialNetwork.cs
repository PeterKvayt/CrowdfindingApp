using System;

namespace CrowdfindingApp.Core.Models
{
    public sealed class UserSocialNetwork : BaseModel
    {
        public Guid UserId { get; set; }
        public int Network { get; set; }
        public string Url { get; set; }
    }
}
