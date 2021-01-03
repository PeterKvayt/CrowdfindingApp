using System.ComponentModel.DataAnnotations;

namespace CrowdfindingApp.Common.Messages.User
{
    public class GetTokenRequestMessage : MessageBase
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
