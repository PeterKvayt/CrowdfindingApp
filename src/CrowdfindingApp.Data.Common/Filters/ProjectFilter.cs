using System;
using System.Collections.Generic;

namespace CrowdfindingApp.Data.Common.Filters
{
    public class ProjectFilter
    {
        public List<Guid> Id { get; set; }
        public List<Guid> OwnerId { get; set; }
        public List<string> Title { get; set; }
        public List<Guid> CategoryId { get; set; }
    }
}
