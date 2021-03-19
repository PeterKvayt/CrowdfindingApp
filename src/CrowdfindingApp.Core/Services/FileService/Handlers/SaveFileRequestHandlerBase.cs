using System;
using System.Threading.Tasks;
using CrowdfindingApp.Common.Handlers;
using CrowdfindingApp.Common.Maintainers.FileStorageProvider;
using CrowdfindingApp.Common.Messages;
using CrowdfindingApp.Common.Messages.Files;

namespace CrowdfindingApp.Core.Services.FileService.Handlers
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
            return await FileStorage.SaveToTempAsync(request.File.OpenReadStream());
        }
    }
}
