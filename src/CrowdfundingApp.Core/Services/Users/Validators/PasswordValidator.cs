
using System.Threading.Tasks;
using CrowdfundingApp.Common.Extensions;
using FluentValidation;

namespace CrowdfundingApp.Core.Services.Users.Validators
{
    public class PasswordValidator : AbstractValidator<string>
    {
        public PasswordValidator()
        {
            RuleFor(x => x).NotEmpty().WithErrorCode(UserErrorKeys.EmptyPassword);

            RuleFor(x => x.Length).GreaterThanOrEqualTo(MinLength)
                .When(x => !x.IsNullOrWhiteSpace())
                .WithErrorCode(UserErrorKeys.InvalidPasswordLength)
                .WithCustomMessageParameters(x => Task.FromResult(new[] { MinLength.ToString() }));
        }

        public bool Confirm(string password, string confirmPassword)
        {
            return password == confirmPassword;
        }

        private const int MinLength = 6;

    }
}
