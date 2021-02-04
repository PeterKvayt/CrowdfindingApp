using CrowdfindingApp.Common.DataTransfers;

namespace CrowdfindingApp.Common.Messages
{
    public abstract class SearchMessageBase<TFilter> : MessageBase
    {
        public TFilter Filter { get; set; }

        public PagingInfo Paging { get; set; }
    }
}
