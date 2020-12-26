using System;

namespace CrowdfindingApp.Core.Models
{
    public sealed class Question : BaseModel
    {
        public Guid ProjectId { get; set; }
        public string Description { get; set; }
        public string Answer { get; set; }
    }
}
