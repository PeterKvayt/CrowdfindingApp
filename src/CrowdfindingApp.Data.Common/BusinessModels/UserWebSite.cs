using System;
using CrowdfindingApp.Common.Data.Models;

namespace CrowdfindingApp.Common.Data.BusinessModels
{
    public sealed class UserWebSite : BaseModel
    {
        public Guid UserId { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
    }
}
