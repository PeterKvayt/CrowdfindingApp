
namespace CrowdfindingApp.Common.Immutable
{
    public static class Configuration
    {
        public const string Connection = "DefaultConnection";
        public const string ClientHost = nameof(ClientHost);
    }

    public static class EmailConfig
    {
        public const string Section = nameof(EmailConfig);
        public const string Host = nameof(Host);
        public const string Port = nameof(Port);
        public const string Mail = nameof(Mail);
        public const string Password = nameof(Password);
    }
}
