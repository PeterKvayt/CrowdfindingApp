
namespace CrowdfindingApp.Common.Maintainers.FileStorageProvider
{
    public class FileStorageConfiguration
    {
        public string Root { get; set; }
        public string TempFolderName { get; set; }
        public string PermanentFolderName { get; set; }
        public int TempStorageFileExpirationHours { get; set; }
    }
}
