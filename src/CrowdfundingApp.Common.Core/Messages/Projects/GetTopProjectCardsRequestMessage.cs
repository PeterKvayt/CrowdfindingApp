using CrowdfindingApp.Common.Core.DataTransfers;

namespace CrowdfindingApp.Common.Core.Messages.Projects
{
    public class GetTopProjectCardsRequestMessage :MessageBase
    {
        public PagingInfo Paging { get; set; }
    }
}
