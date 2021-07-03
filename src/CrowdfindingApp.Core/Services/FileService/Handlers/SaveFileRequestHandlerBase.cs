using System;
using System.Linq;
using System.Threading.Tasks;
using CrowdfindingApp.Common.Core.Handlers;
using CrowdfindingApp.Common.Core.Maintainers.FileStorageProvider;
using CrowdfindingApp.Common.Core.Messages;
using CrowdfindingApp.Common.Core.Messages.Files;

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
            return await FileStorage.SaveToTempAsync(request.File.OpenReadStream(), request.File.FileName.Split('.').Last());
        }
    }
}
