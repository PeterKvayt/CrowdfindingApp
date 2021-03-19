using Microsoft.AspNetCore.Http;

namespace CrowdfindingApp.Common.Messages.Files
{
    public abstract class SaveFileRequestMessageBase : MessageBase
    {
        public IFormFile File { get; set; }
    }
}
