
namespace CrowdfindingApp.Common.Immutable
{
    public static class RegexPatterns
    {
        public const string PassportNo = "^[A-Za-z]{2}[0-9]{7}$";
        public const string IdentificationPassportNo = "^[0-9]{7}[A-Za-z]{1}[0-9]{3}[A-Za-z]{2}[0-9]{1}$";
        public const string PhoneNumber = "^[0-9]{12}$";
    }
}
