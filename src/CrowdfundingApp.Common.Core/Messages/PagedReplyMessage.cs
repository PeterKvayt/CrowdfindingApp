using CrowdfundingApp.Common.Core.DataTransfers;

namespace CrowdfundingApp.Common.Core.Messages
{
    public sealed class PagedReplyMessage<T> : ReplyMessage<T>
    {
        public PagingInfo Paging { get; set; }
    }
}
