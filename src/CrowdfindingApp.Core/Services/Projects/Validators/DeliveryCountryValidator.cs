using CrowdfindingApp.Common.DataTransfers;
using CrowdfindingApp.Common.Validators;
using CrowdfindingApp.Core.Services.Projects.ValidationErrorKeys;
using FluentValidation;

namespace CrowdfindingApp.Core.Services.Projects.Validators
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
