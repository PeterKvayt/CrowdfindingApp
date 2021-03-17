using System;
using CrowdfindingApp.Data.Common.Models;

namespace CrowdfindingApp.Data.Common.BusinessModels
{
    public sealed class Question : BaseModel
    {
        public Guid ProjectId { get; set; }
        public string Description { get; set; }
        public string Answer { get; set; }
    }
}
