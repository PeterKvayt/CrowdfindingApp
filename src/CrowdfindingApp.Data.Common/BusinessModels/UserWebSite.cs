using System;
using CrowdfindingApp.Data.Common.Models;

namespace CrowdfindingApp.Data.Common.BusinessModels
{
    public sealed class UserWebSite : BaseModel
    {
        public Guid UserId { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
    }
}
