
namespace CrowdfindingApp.Common.Messages.User
{
    public class ChangePasswordRequestMessage : MessageBase
    {
        /// <example>test</example>
        public string OldPassword { get; set; }
        /// <example>test</example>
        public string NewPassword { get; set; }
        /// <example>test</example>
        public string ConfirmPassword { get; set; }
    }
}
