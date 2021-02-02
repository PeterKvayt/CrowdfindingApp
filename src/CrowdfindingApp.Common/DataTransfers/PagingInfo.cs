
namespace CrowdfindingApp.Common.DataTransfers
{
    public class PagingInfo
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public long TotalCount { get; set; }
    }
}
