
namespace CrowdfundingApp.Common.Configs
{
    public class AppConfiguration
    {
        public string AllowedHosts { get; set; }
        public string ClientHost { get; set; }
        public ConnectionStrings ConnectionStrings { get; set; }
        public EmailConfig EmailConfig { get; set; }
        public FileStorageConfiguration FileStorageConfiguration { get; set; }
        public PaymentConfig PaymentSettings { get; set; }
    }
}
