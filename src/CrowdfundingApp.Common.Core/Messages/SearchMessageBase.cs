using CrowdfindingApp.Common.Core.DataTransfers;

namespace CrowdfindingApp.Common.Core.Messages
{
    public abstract class SearchMessageBase<TFilter> : MessageBase
    {
        public TFilter Filter { get; set; }

        public PagingInfo Paging { get; set; }
    }
}
