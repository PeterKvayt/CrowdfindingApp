using CrowdfindingApp.Common.DataTransfers;

namespace CrowdfindingApp.Common.Messages
{
    public sealed class PagedReplyMessage<T> : ReplyMessage<T>
    {
        public PagingInfo Paging { get; set; }
    }
}
