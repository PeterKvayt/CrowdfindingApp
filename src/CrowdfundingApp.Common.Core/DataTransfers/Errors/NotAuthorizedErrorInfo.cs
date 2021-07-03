
namespace CrowdfundingApp.Common.Core.DataTransfers.Errors
{
    public class NotAuthorizedErrorInfo : ErrorInfo
    {
        public const string NotAuthorizedMessageKey = nameof(NotAuthorizedMessageKey);

        public NotAuthorizedErrorInfo(string key, string message)
            : base(key ?? NotAuthorizedMessageKey, message)
        {
        }
    }
}
