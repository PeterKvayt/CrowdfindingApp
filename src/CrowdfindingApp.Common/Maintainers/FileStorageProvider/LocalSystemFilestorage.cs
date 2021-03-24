using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace CrowdfindingApp.Common.Maintainers.FileStorageProvider
{
    public class LocalSystemFilestorage : IFileStorage
    {
        private readonly string _root;
        private readonly FileStorageConfiguration _fileConfig;

        private const int BufferSize = 524288;

        public LocalSystemFilestorage(string rootPath, IConfiguration configuration)
        {
            _fileConfig = new FileStorageConfiguration
            {
                Root = configuration["FileStorageConfiguration:Root"],
                PermanentFolderName = configuration["FileStorageConfiguration:PermanentFolderName"],
                TempFolderName = configuration["FileStorageConfiguration:TempFolderName"],
                TempStorageFileExpirationHours = int.Parse(configuration["FileStorageConfiguration:TempStorageFileExpirationHours"]),
            };
            _root = Path.Combine(rootPath, _fileConfig.Root);
        }

        public void ClearExpiredTemporaryStorageFiles()
        {
            var expirationPeriod = new TimeSpan(_fileConfig.TempStorageFileExpirationHours, 0, 0);
            ClearTemporaryStorage(expirationPeriod);
        }

        private void ClearTemporaryStorage(TimeSpan expirationPeriod)
        {
            var tempStoragePath = Path.Combine(_root, _fileConfig.TempFolderName);
            CreateDirectoryIfNotExists(tempStoragePath);

            var expirationDate = DateTime.UtcNow - expirationPeriod;

            var tempStorageDirectory = new DirectoryInfo(tempStoragePath);

            foreach(var file in tempStorageDirectory.EnumerateFiles())
            {
                if(file.CreationTimeUtc <= expirationDate)
                {
                    file.Delete();
                }
            }
        }

        private void CreateDirectoryIfNotExists(string path)
        {
            if(Directory.Exists(path))
                return;

            Directory.CreateDirectory(path);
        }

        public async Task<string> SaveToTempAsync(Stream stream, string extension)
        {
            var uniqueFileName = Guid.NewGuid();
            var fileName = $"{uniqueFileName}.{extension}";
            await SaveToFileSystemAsync(GetFullTempPath(fileName), stream);
            return $"{_fileConfig.TempFolderName}/{fileName}";
        }

        private string GetFullTempPath(string fileName)
        {
            CreateDirectoryIfNotExists(Path.Combine(_root, _fileConfig.TempFolderName));
            return Path.Combine(_root, _fileConfig.TempFolderName, fileName);
        }

        private async Task SaveToFileSystemAsync(string filePath, Stream stream)
        {
            if(string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            if(stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if(stream.CanSeek)
            {
                stream.Seek(0, SeekOrigin.Begin);
            }

            using(var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                if(stream is MemoryStream memoryStream)
                {
                    memoryStream.WriteTo(fileStream);
                }
                else
                {
                    await CopyAndWriteStreamAsync(stream, fileStream);
                }
            }
        }

        private static async Task CopyAndWriteStreamAsync(Stream source, Stream destination)
        {
            byte[] buffer = new byte[BufferSize];
            int bytesRead;
            while((bytesRead = await source.ReadAsync(buffer, 0, buffer.Length)) != 0)
            {
                await destination.WriteAsync(buffer, 0, bytesRead);
            }
        }

        private string GetFullPermanentPath(string fileName, params string[] subFolders)
        {
            var subDrectories = subFolders.Prepend( _fileConfig.PermanentFolderName);
            subDrectories = subDrectories.Prepend(_root);
            CreateDirectoryIfNotExists(Path.Combine(subDrectories.ToArray()));
            subDrectories = subDrectories.Append(fileName);
            return Path.Combine(subDrectories.ToArray());
        }

        public async Task SaveProjectImageAsync(string tempFileName, Guid projectId)
        {
            await SaveImageAsync(tempFileName, projectId, "Projects");
        }

        public async Task SaveUserImageAsync(string tempFileName, Guid userId)
        {
            await SaveImageAsync(tempFileName, userId, "Users");
         }

        private Task SaveImageAsync(string tempFileName, Guid userId, string subFolderName)
        {
            var tempFile = GetFullTempPath(tempFileName);
            if(!File.Exists(tempFile))
            {
                return Task.CompletedTask;
            }

            var file = GetFullPermanentPath(tempFileName, subFolderName, userId.ToString());

            File.Move(tempFile, file);

            return Task.CompletedTask;
        }
    }
}
