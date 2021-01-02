
namespace CrowdfindingApp.Common.DataTransfers.Errors
{
    class ObjectNotFoundErrorInfo : ErrorInfo
    {
        public ObjectNotFoundErrorInfo()
            : this(string.Empty, string.Empty)
        {
        }

        public ObjectNotFoundErrorInfo(string message)
            : base(message)
        {
        }

        public ObjectNotFoundErrorInfo(string key, string message)
            : base(key, message)
        {
        }
    }
}
