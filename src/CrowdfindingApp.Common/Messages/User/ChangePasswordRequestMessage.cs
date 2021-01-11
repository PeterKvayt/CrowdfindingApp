
namespace CrowdfindingApp.Common.Messages.User
{
    public class ChangePasswordRequestMessage : MessageBase
    {
        public string Id { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
