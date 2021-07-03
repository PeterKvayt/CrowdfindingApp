using System;
using System.IO;
using System.Threading.Tasks;

namespace CrowdfindingApp.Common.Core.Maintainers.FileStorageProvider
{
    public interface IFileStorage
    {
        Task<string> SaveToTempAsync(Stream stream, string extension);

        Task SaveProjectImageAsync(string tempFileName, Guid projectId);

        Task SaveUserImageAsync(string tempFileName, Guid userId);

        void ClearExpiredTemporaryStorageFiles();
    }
}
