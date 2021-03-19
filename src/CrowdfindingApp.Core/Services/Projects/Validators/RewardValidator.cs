using System;
using System.Linq;
using CrowdfindingApp.Common.DataTransfers.Rewards;
using CrowdfindingApp.Common.Enums;
using CrowdfindingApp.Core.Services.Projects.ValidationErrorKeys;
using FluentValidation;
using CrowdfindingApp.Common.Extensions;

namespace CrowdfindingApp.Core.Services.Projects.Validators
{
    public class RewardValidator : AbstractValidator<RewardInfo>
    {
        public RewardValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithErrorCode(RewardValidationErrorKeys.MissingTitle);
            RuleFor(x => x.Price).NotEmpty().WithErrorCode(RewardValidationErrorKeys.MissingPrice);
            RuleFor(x => x.Description).NotEmpty().WithErrorCode(RewardValidationErrorKeys.MissingDescription);
            RuleFor(x => x.DeliveryDate).NotEmpty().WithErrorCode(RewardValidationErrorKeys.MissingDeliveryDate);

            RuleFor(x => x.DeliveryCountries)
                .NotNull().Must(x => x.Any())
                .When(x => x.DeliveryType == DeliveryType.SomeCountries || x.DeliveryType == DeliveryType.WholeWorld)
                .WithErrorCode(RewardValidationErrorKeys.MissingDeliveryCountries);

            RuleFor(x => x.DeliveryCountries)
                .Must(countries => countries.All(country => country.Key.NonNullOrWhiteSpace() && country.Value.HasValue))
                .When(x => (x.DeliveryType == DeliveryType.SomeCountries || x.DeliveryType == DeliveryType.WholeWorld) && x.DeliveryCountries.Any())
                .WithErrorCode(RewardValidationErrorKeys.EmptyDeliveryCountries);

            RuleFor(x => x.DeliveryCountries)
                .Must(countries => countries.All(country => Guid.TryParse(country.Key, out var _)))
                .When(x => (x.DeliveryType == DeliveryType.SomeCountries || x.DeliveryType == DeliveryType.WholeWorld) && x.DeliveryCountries.Any())
                .WithErrorCode(RewardValidationErrorKeys.EmptyDeliveryCountries);

            RuleFor(x => x.Limit).Must(x => x.Value > 0)
                .When(x => x.Limit.HasValue)
                .WithErrorCode(RewardValidationErrorKeys.WrongLimitValue);
        }
    }
}
