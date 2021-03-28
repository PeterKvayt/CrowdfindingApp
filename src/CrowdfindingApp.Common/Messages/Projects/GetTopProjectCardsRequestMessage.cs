using CrowdfindingApp.Common.DataTransfers;

namespace CrowdfindingApp.Common.Messages.Projects
{
    public class GetTopProjectCardsRequestMessage :MessageBase
    {
        public PagingInfo Paging { get; set; }
    }
}
