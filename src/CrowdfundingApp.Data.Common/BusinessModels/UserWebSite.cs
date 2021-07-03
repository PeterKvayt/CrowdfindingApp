using System;
using CrowdfundingApp.Common.Data.Models;

namespace CrowdfundingApp.Common.Data.BusinessModels
{
    public sealed class UserWebSite : BaseModel
    {
        public Guid UserId { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
    }
}
