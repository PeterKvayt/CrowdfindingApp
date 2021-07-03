using System.Threading.Tasks;
using CrowdfindingApp.Common.Core.Maintainers.FileStorageProvider;
using CrowdfindingApp.Common.Core.Messages;
using CrowdfindingApp.Common.Core.Messages.Files;

namespace CrowdfindingApp.Core.Services.FileService.Handlers
{
    public class SaveImageRequestHandler : SaveFileRequestHandlerBase<SaveImageRequestMessage, ReplyMessage<string>>
    {
        public SaveImageRequestHandler(IFileStorage fileStorage) : base(fileStorage)
        {

        }

        protected override async Task<ReplyMessageBase> ValidateRequestMessageAsync(SaveImageRequestMessage requestMessage)
        {
            var reply = await base.ValidateRequestMessageAsync(requestMessage);
            if(requestMessage.File == null)
            {
                reply.AddObjectNotFoundError();
            }

            return reply;
        }

        protected override async Task<ReplyMessage<string>> ExecuteAsync(SaveImageRequestMessage request)
        {
            var fileName = await SaveFileAsync(request);
            return new ReplyMessage<string>
            {
                Value = fileName
            };
        }
    }
}
