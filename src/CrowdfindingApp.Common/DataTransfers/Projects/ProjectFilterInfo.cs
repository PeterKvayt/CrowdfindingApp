
using System.Collections.Generic;
using CrowdfindingApp.Common.Enums;

namespace CrowdfindingApp.Common.DataTransfers.Project
{
    public class ProjectFilterInfo
    {
        public List<string> Id { get; set; }
        public List<string> Title { get; set; }
        public List<string> CategoryId { get; set; }
        public List<ProjectStatus> Status { get; set; }
    }
}
