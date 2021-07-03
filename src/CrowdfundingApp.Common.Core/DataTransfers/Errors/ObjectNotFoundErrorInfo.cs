
namespace CrowdfindingApp.Common.Core.DataTransfers.Errors
{
    public class ObjectNotFoundErrorInfo : ErrorInfo
    {
        public const string ObjectNotFoundMessageKey = nameof(ObjectNotFoundMessageKey);

        public ObjectNotFoundErrorInfo(string key, string message)
            : base(key ?? ObjectNotFoundMessageKey, message)
        {
        }
    }
}
