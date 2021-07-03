
namespace CrowdfundingApp.Common.Core.DataTransfers.Errors
{
    class ValidationErrorInfo : ErrorInfo
    {
        public ValidationErrorInfo()
            : this(string.Empty, string.Empty)
        {
        }

        public ValidationErrorInfo(string message)
            : base(message)
        {
        }

        public ValidationErrorInfo(string key, string message)
            : base(key, message)
        {
        }
    }
}
