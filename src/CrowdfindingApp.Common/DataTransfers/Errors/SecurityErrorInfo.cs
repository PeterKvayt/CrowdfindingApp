
namespace CrowdfindingApp.Common.DataTransfers.Errors
{
    class SecurityErrorInfo : ErrorInfo
    {
        public SecurityErrorInfo()
            : this(string.Empty, string.Empty)
        {
        }

        public SecurityErrorInfo(string message)
            : base(message)
        {
        }

        public SecurityErrorInfo(string key, string message)
            : base(key, message)
        {
        }
    }
}
