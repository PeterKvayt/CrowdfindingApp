
namespace CrowdfindingApp.Data.Common.Models
{
    public class Paging
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public long TotalCount { get; set; }

        //public bool SkipPaging { get; set; }
        //public string OrderBy { get; set; }
        //public bool Ascending { get; set; }
    }
}
