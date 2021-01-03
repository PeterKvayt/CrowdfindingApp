
using System.ComponentModel.DataAnnotations;

namespace CrowdfindingApp.Common.Messages.User
{
    public class RegisterRequestMessage : MessageBase
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
