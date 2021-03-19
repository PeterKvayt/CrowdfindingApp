using System.IO;
using System.Threading.Tasks;

namespace CrowdfindingApp.Common.Maintainers.FileStorageProvider
{
    public interface IFileStorage
    {
        Task<string> SaveToTempAsync(Stream stream);

        Task MoveTempToPermanentStorageAsync(string tempFileName, string fileName);

        void ClearExpiredTemporaryStorageFiles();
    }
}
