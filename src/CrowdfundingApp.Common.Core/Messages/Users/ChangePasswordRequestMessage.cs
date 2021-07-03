
namespace CrowdfindingApp.Common.Core.Messages.Users
{
    public class ChangePasswordRequestMessage : MessageBase
    {
        public ChangePasswordRequestMessage()
        {

        }

        public ChangePasswordRequestMessage(string currentPassword, string newPassword, string confirmPassword) : base()
        {
            OldPassword = currentPassword;
            NewPassword = newPassword;
            ConfirmPassword = confirmPassword;
        }

        /// <example>test</example>
        public string OldPassword { get; set; }
        /// <example>test</example>
        public string NewPassword { get; set; }
        /// <example>test</example>
        public string ConfirmPassword { get; set; }
    }
}
