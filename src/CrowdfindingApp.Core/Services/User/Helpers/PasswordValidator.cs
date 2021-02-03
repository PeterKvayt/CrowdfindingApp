
using CrowdfindingApp.Common.Extensions;
using CrowdfindingApp.Common.Messages;

namespace CrowdfindingApp.Core.Services.User.Helpers
{
    public class PasswordValidator
    {
        public bool Confirm(string password, string confirmPassword)
        {
            return password == confirmPassword;
        }

        private const int MinLength = 2;

        private void ValidateLength(string password, ReplyMessageBase reply)
        {
            if(password.Length < MinLength)
            {
                reply.AddValidationError(key: UserErrorKeys.InvalidPasswordLength, parameters: MinLength);
            }
        }

        private string[] SpecialSymbols => new string[] { "" };

        private void ValidateOnSpecialSymbols(string password, ReplyMessageBase reply)
        {
            // ToDo: Implement method
        }

        public ReplyMessageBase Validate(string password)
        {
            var reply = new ReplyMessageBase();

            if(password.IsNullOrEmpty())
            {
                return reply.AddValidationError(UserErrorKeys.EmptyPassword);
            }

            ValidateLength(password, reply);
            if(!reply.Success)
            {
                return reply;
            }

            ValidateOnSpecialSymbols(password, reply);
            return reply;
        }
    }
}
