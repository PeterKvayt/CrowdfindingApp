using System.Threading.Tasks;
using CrowdfindingApp.Common.Maintainers.FileStorageProvider;
using CrowdfindingApp.Common.Messages;
using CrowdfindingApp.Common.Messages.Files;

namespace CrowdfindingApp.Core.Services.FileService.Handlers
{
    public class SaveImageRequestHandler : SaveFileRequestHandlerBase<SaveImageRequestMessage, ReplyMessage<string>>
    {
        public SaveImageRequestHandler(IFileStorage fileStorage) : base(fileStorage)
        {

        }

        protected override Task<ReplyMessageBase> ValidateRequestMessageAsync(SaveImageRequestMessage requestMessage)
        {
            return base.ValidateRequestMessageAsync(requestMessage);
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
