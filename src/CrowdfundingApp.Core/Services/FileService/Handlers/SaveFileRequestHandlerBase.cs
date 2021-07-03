using System;
using System.Linq;
using System.Threading.Tasks;
using CrowdfundingApp.Common.Core.Handlers;
using CrowdfundingApp.Common.Core.Maintainers.FileStorageProvider;
using CrowdfundingApp.Common.Core.Messages;
using CrowdfundingApp.Common.Core.Messages.Files;

namespace CrowdfundingApp.Core.Services.FileService.Handlers
{
    public abstract class SaveFileRequestHandlerBase<TRequest, TReply> : NullOperationContextRequestHandler<TRequest, TReply>
        where TRequest : SaveFileRequestMessageBase, new()
        where TReply : ReplyMessageBase, new()
    {
        protected IFileStorage FileStorage;

        public SaveFileRequestHandlerBase(IFileStorage fileStorage)
        {
            FileStorage = fileStorage ?? throw new NullReferenceException(nameof(fileStorage));
        }

        protected virtual async Task<string> SaveFileAsync(TRequest request)
        {
            return await FileStorage.SaveToTempAsync(request.File.OpenReadStream(), request.File.FileName.Split('.').Last());
        }
    }
}
