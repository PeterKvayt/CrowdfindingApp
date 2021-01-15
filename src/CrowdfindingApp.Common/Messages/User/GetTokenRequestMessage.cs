
namespace CrowdfindingApp.Common.Messages.User
{
    public class GetTokenRequestMessage : MessageBase
    {
        /// <example>test@user.com</example>
        public string Email { get; set; }
        /// <example>test</example>
        public string Password { get; set; }
    }
}
