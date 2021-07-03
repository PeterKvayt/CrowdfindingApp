using Microsoft.AspNetCore.Http;

namespace CrowdfundingApp.Common.Core.Messages.Files
{
    public abstract class SaveFileRequestMessageBase : MessageBase
    {
        public IFormFile File { get; set; }
    }
}
