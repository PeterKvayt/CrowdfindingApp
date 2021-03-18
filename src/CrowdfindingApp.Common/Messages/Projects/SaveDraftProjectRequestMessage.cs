using CrowdfindingApp.Common.DataTransfers.Projects;

namespace CrowdfindingApp.Common.Messages.Projects
{
    public class SaveDraftProjectRequestMessage : MessageBase
    {
        public ProjectInfo Data { get; set; }
    }
}
