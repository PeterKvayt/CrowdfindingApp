
namespace CrowdfindingApp.Common.Messages.User
{
    public class ResetPasswordRequestMessage : MessageBase
    {
        public string Token { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
