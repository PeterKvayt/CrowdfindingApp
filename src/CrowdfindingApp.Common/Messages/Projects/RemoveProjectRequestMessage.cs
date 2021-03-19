
namespace CrowdfindingApp.Common.Messages.Projects
{
    public class RemoveProjectRequestMessage : MessageBase
    {
        public RemoveProjectRequestMessage()
        {

        }

        public RemoveProjectRequestMessage(string id) : base()
        {
            ProjectId = id;
        }

        public string ProjectId { get; set; }
    }
}
