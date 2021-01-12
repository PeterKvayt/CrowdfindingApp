
namespace CrowdfindingApp.Common.DataTransfers.Errors
{
    public class SystemErrorInfo : ErrorInfo
    {
        public const string SystemErrorMessageKey = nameof(SystemErrorMessageKey);

        public SystemErrorInfo(string key, string message)
            : base(key ?? SystemErrorMessageKey, message)
        {
        }
    }
}
