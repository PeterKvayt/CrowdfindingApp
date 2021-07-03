using System.ComponentModel.DataAnnotations;

namespace CrowdfindingApp.Common.Core.Messages.Users
{
    public class GetUserInfoByIdRequestMessage : MessageBase
    {
        public GetUserInfoByIdRequestMessage()
        {

        }

        public GetUserInfoByIdRequestMessage(string id) : base()
        {
            Id = id;
        }

        [Required]
        public string Id { get; set; }
    }
}
