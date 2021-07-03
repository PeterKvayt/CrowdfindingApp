using System;
using System.Threading.Tasks;
using CrowdfundingApp.Common.Extensions;
using CrowdfundingApp.Common.Immutable;
using FluentValidation;

namespace CrowdfundingApp.Common.Core.Validators
{
    public class IdValidator : AbstractValidator<string>
    {
        public IdValidator()
        {
            RuleFor(x => x).NotEmpty()
                .WithErrorCode(CommonErrorMessageKeys.EmptyId);
            RuleFor(x => x).Must(x => Guid.TryParse(x, out var _))
                .When(x => x.NonNullOrWhiteSpace())
                .WithErrorCode(CommonErrorMessageKeys.InvalidIdFormat)
                .WithCustomMessageParameters(x => Task.FromResult(x));
        }
    }
}
