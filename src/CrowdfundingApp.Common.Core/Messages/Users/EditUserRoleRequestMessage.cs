using System.ComponentModel.DataAnnotations;

namespace CrowdfundingApp.Common.Core.Messages.Users
{
    public class EditUserRoleRequestMessage : MessageBase
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string RoleName { get; set; }
    }
}
