
namespace CrowdfindingApp.Common.Localization
{
    public interface IResourceProvider
    {
        string GetString(string key, params object[] args);
    }
}
