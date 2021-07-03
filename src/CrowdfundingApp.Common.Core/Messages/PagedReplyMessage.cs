using CrowdfindingApp.Common.Core.DataTransfers;

namespace CrowdfindingApp.Common.Core.Messages
{
    public sealed class PagedReplyMessage<T> : ReplyMessage<T>
    {
        public PagingInfo Paging { get; set; }
    }
}
