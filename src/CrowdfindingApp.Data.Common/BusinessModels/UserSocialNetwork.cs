using System;
using CrowdfindingApp.Common.Data.Models;

namespace CrowdfindingApp.Common.Data.BusinessModels
{
    public sealed class UserSocialNetwork : BaseModel
    {
        public Guid UserId { get; set; }
        public int Network { get; set; }
        public string Url { get; set; }
    }
}
