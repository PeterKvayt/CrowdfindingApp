
using System.ComponentModel.DataAnnotations;

namespace CrowdfindingApp.Common.Messages.User
{
    public class RegisterRequestMessage : MessageBase
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
