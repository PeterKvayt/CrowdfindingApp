﻿
namespace CrowdfundingApp.Common.Core.Messages.Users
{
    public class UpdateUserRequestMessage : MessageBase
    {
        /// <example>nickName</example>
        public string UserName { get; set; }
        /// <example>test@user.com</example>
        public string Email { get; set; }
        /// <example>Jonh</example>
        public string Name { get; set; }
        /// <example>Dorian</example>
        public string Surname { get; set; }
        /// <example>Middle name</example>
        public string MiddleName { get; set; }
        public string Image { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
