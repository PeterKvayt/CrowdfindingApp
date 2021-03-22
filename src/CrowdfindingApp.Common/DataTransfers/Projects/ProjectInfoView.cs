
namespace CrowdfindingApp.Common.DataTransfers.Projects
{
    public class ProjectInfoView : ProjectInfo
    {
        public int RestProjectDays { get; set; }
        public string CategoryName { get; set; }
        public string LocationName { get; set; }
        public decimal Progress { get; set; }
    }
}
