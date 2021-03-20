using System;
using System.IO;
using System.Threading.Tasks;

namespace CrowdfindingApp.Common.Maintainers.FileStorageProvider
{
    public interface IFileStorage
    {
        Task<string> SaveToTempAsync(Stream stream, string extension);

        Task SaveProjectImageAsync(string tempFileName, Guid projectId);

        void ClearExpiredTemporaryStorageFiles();
    }
}
