﻿
namespace CrowdfindingApp.Common.Messages.User
{
    public class RegisterRequestMessage : MessageBase
    {
        /// <example>test@user.com</example>
        public string Email { get; set; }
        /// <example>test</example>
        public string Password { get; set; }
        /// <example>test</example>
        public string ConfirmPassword { get; set; }
    }
}
