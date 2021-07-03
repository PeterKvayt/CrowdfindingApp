
namespace CrowdfundingApp.Common.Core.DataTransfers.Projects
{
    public class ProjectInfoView : ProjectInfo
    {
        public string RestProjectTime { get; set; }
        public string CategoryName { get; set; }
        public string LocationName { get; set; }
        public decimal Progress { get; set; }
    }
}
