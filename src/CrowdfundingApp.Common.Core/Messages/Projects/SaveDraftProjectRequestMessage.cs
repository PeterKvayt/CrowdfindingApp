using CrowdfundingApp.Common.Core.DataTransfers.Projects;

namespace CrowdfundingApp.Common.Core.Messages.Projects
{
    public class SaveDraftProjectRequestMessage : MessageBase
    {
        public ProjectInfo Data { get; set; }
    }
}
