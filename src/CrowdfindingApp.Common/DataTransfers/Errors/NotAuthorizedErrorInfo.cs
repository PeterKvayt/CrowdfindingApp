
namespace CrowdfindingApp.Common.DataTransfers.Errors
{
    public class NotAuthorizedErrorInfo : ErrorInfo
    {
        public NotAuthorizedErrorInfo()
            : this(string.Empty, string.Empty)
        {
        }

        public NotAuthorizedErrorInfo(string message)
            : base(message)
        {
        }

        public NotAuthorizedErrorInfo(string key, string message)
            : base(key, message)
        {
        }
    }
}
