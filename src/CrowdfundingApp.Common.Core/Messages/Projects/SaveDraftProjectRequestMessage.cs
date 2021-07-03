using CrowdfindingApp.Common.Core.DataTransfers.Projects;

namespace CrowdfindingApp.Common.Core.Messages.Projects
{
    public class SaveDraftProjectRequestMessage : MessageBase
    {
        public ProjectInfo Data { get; set; }
    }
}
