
namespace CrowdfindingApp.Common.Messages.User
{
    public class ResetPasswordRequestMessage : MessageBase
    {
        public string Token { get; set; }
        /// <example>test</example>
        public string Password { get; set; }
        /// <example>test</example>
        public string ConfirmPassword { get; set; }
    }
}
