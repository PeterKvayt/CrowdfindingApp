using System;
using System.Threading.Tasks;
using CrowdfundingApp.Common.Core.Maintainers.FileStorageProvider;

namespace CrowdfundingApp.Core.Services.BackgroundTasks.Jobs
{
    public class ClearExpiredTempFilesJob : IBackgroundJob
    {
        private readonly IFileStorage _fileProvider;

        public ClearExpiredTempFilesJob(IFileStorage fileProvider)
        {
            _fileProvider = fileProvider ?? throw new NullReferenceException(nameof(fileProvider));
        }

        public async Task Execute()
        {
           await Task.Run(() => _fileProvider.ClearExpiredTemporaryStorageFiles());
        }
    }
}
