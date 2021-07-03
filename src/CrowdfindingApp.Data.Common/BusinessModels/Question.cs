using System;
using CrowdfindingApp.Common.Data.Models;

namespace CrowdfindingApp.Common.Data.BusinessModels
{
    public sealed class Question : BaseModel
    {
        public Guid ProjectId { get; set; }
        public string Description { get; set; }
        public string Answer { get; set; }
    }
}
