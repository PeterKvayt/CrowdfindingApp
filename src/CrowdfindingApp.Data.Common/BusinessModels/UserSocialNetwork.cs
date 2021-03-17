using System;
using CrowdfindingApp.Data.Common.Models;

namespace CrowdfindingApp.Data.Common.BusinessModels
{
    public sealed class UserSocialNetwork : BaseModel
    {
        public Guid UserId { get; set; }
        public int Network { get; set; }
        public string Url { get; set; }
    }
}
