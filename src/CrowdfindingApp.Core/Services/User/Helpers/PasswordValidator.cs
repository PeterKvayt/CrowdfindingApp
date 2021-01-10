
using CrowdfindingApp.Common.Extensions;

namespace CrowdfindingApp.Core.Services.User.Helpers
{
    public class PasswordValidator
    {
        public bool Confirm(string password, string confirmPassword)
        {
            return password == confirmPassword;
        }

        private const int MinLength = 2;

        private bool ValidLength(string password)
        {
            return password.Length >= MinLength;
        }

        private string[] SpecialSymbols => new string[] { "" };

        private bool HasSpecialSymbol(string password)
        {
            // ToDo: Implement method
            return true;
        }

        public bool Validate(string password)
        {
            return password.IsPresent() 
                && HasSpecialSymbol(password) 
                && ValidLength(password);
        }
    }
}
