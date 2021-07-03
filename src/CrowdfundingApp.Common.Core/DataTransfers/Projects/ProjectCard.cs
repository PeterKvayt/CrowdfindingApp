
using CrowdfindingApp.Common.Enums;

namespace CrowdfindingApp.Common.Core.DataTransfers.Projects
{
    public class ProjectCard
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string CategoryId { get; set; }
        public string ImgPath { get; set; }
        public decimal? Purpose { get; set; }
        public decimal? CurrentResult { get; set; }
        public ProjectStatus Status { get; set; }
        public string RestTimeToEnd { get; set; }
    }
}
