
namespace CrowdfundingApp.Common.Core.DataTransfers.Errors
{
    class SecurityErrorInfo : ErrorInfo
    {
        public const string SecurityErrorMessageKey = nameof(SecurityErrorMessageKey);

        public SecurityErrorInfo(string key, string message)
            : base(key ?? SecurityErrorMessageKey, message)
        {
        }
    }
}
