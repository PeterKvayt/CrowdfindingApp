using System.ComponentModel.DataAnnotations;

namespace CrowdfindingApp.Common.Messages.User
{
    public class GetTokenRequestMessage : MessageBase
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
