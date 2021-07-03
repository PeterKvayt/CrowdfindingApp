using System;
using CrowdfundingApp.Common.Data.Models;

namespace CrowdfundingApp.Common.Data.BusinessModels
{
    public sealed class Question : BaseModel
    {
        public Guid ProjectId { get; set; }
        public string Description { get; set; }
        public string Answer { get; set; }
    }
}
