using System.ComponentModel.DataAnnotations;

namespace CrowdfindingApp.Common.Core.Messages.Rewards
{
    public class GetPublicRewardsByProjectIdRequestMessage : MessageBase
    {
        public GetPublicRewardsByProjectIdRequestMessage()
        {

        }

        public GetPublicRewardsByProjectIdRequestMessage(string id) : base()
        {
            ProjectId = id;
        }

        [Required]
        public string ProjectId { get; set; }
    }
}
