using CrowdfundingApp.Common.Core.DataTransfers;
using CrowdfundingApp.Common.Core.Validators;
using CrowdfundingApp.Core.Services.Projects.ValidationErrorKeys;
using FluentValidation;

namespace CrowdfundingApp.Core.Services.Projects.Validators
{
    public class DeliveryCountryValidator : AbstractValidator<KeyValue<string, decimal?>>
    {
        public DeliveryCountryValidator()
        {
            RuleFor(x => x.Key).SetValidator(new IdValidator());

            RuleFor(x => x.Value).NotEmpty().WithErrorCode(RewardValidationErrorKeys.DeliveryPriceMissing);
            RuleFor(x => x.Value).GreaterThanOrEqualTo(1)
                .When(x => x.Value.HasValue)
                .WithErrorCode(RewardValidationErrorKeys.DeliveryPriceLessThanOne);
        }
    }
}
