
namespace CrowdfundingApp.Common.Core.Localization
{
    public interface IResourceProvider
    {
        string GetString(string key, params object[] args);
    }
}
