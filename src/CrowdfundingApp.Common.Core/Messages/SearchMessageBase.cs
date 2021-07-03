using CrowdfundingApp.Common.Core.DataTransfers;

namespace CrowdfundingApp.Common.Core.Messages
{
    public abstract class SearchMessageBase<TFilter> : MessageBase
    {
        public TFilter Filter { get; set; }

        public PagingInfo Paging { get; set; }
    }
}
