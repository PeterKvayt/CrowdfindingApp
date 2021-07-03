
namespace CrowdfundingApp.Common.Core.Messages.Rewards
{
    public class GetPublicRewardByIdRequestMessage : GetByIdRequestMessageBase
    {
        public GetPublicRewardByIdRequestMessage()
        {

        }

        public GetPublicRewardByIdRequestMessage(string id) : base()
        {
            Id = id;
        }
    }
}
