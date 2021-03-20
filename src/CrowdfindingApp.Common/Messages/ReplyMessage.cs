
namespace CrowdfindingApp.Common.Messages
{
    public class ReplyMessage<T> : ReplyMessageBase
    {
        public T Value { get; set; }

        public ReplyMessage() : this(null)
        { }

        public ReplyMessage(T result) : this(null)
        {
            Value = result;
        }

        public ReplyMessage(ReplyMessageBase result)
            : base(result)
        {
            if(result is ReplyMessage<T> r)
            {
                Value = r.Value;
            }
        }
    }
}
