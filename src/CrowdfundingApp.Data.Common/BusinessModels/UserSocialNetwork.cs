using System;
using CrowdfundingApp.Common.Data.Models;

namespace CrowdfundingApp.Common.Data.BusinessModels
{
    public sealed class UserSocialNetwork : BaseModel
    {
        public Guid UserId { get; set; }
        public int Network { get; set; }
        public string Url { get; set; }
    }
}
