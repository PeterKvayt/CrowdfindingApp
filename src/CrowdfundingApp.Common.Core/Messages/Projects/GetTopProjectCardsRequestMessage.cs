using CrowdfundingApp.Common.Core.DataTransfers;

namespace CrowdfundingApp.Common.Core.Messages.Projects
{
    public class GetTopProjectCardsRequestMessage :MessageBase
    {
        public PagingInfo Paging { get; set; }
    }
}
