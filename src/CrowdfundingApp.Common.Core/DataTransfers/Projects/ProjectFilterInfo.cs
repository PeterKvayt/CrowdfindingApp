
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CrowdfundingApp.Common.Core.DataTransfers.Projects;
using CrowdfundingApp.Common.Enums;

namespace CrowdfundingApp.Common.Core.DataTransfers.Project
{
    public class ProjectFilterInfo
    {
        public List<string> Id { get; set; }
        public List<string> Title { get; set; }
        public List<string> CategoryId { get; set; }
        public List<string> OwnerId { get; set; }
        public List<ProjectStatus> Status { get; set; }
        public Expression<Func<ProjectInfo, object>> OrderBy { get; set; }
        public bool DescendingOrder { get; set; }
    }
}
