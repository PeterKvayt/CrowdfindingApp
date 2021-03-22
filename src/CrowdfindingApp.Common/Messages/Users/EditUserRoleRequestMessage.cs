using System.ComponentModel.DataAnnotations;

namespace CrowdfindingApp.Common.Messages.Users
{
    public class EditUserRoleRequestMessage : MessageBase
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string RoleName { get; set; }
    }
}
