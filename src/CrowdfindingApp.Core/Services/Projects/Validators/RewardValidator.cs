using System;
using System.Linq;
using System.Threading.Tasks;
using CrowdfindingApp.Common.Core.DataTransfers.Rewards;
using CrowdfindingApp.Common.Enums;
using CrowdfindingApp.Common.Extensions;
using CrowdfindingApp.Core.Services.Projects.ValidationErrorKeys;
using FluentValidation;

namespace CrowdfindingApp.Core.Services.Projects.Validators
{
    public class RewardValidator : AbstractValidator<RewardInfo>
    {
        public RewardValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithErrorCode(RewardValidationErrorKeys.MissingTitle);

            RuleFor(x => x.Price).NotEmpty().WithErrorCode(RewardValidationErrorKeys.MissingPrice);
            RuleFor(x => x.Price).GreaterThanOrEqualTo(1)
                .When(x => x.Price.HasValue)
                .WithErrorCode(RewardValidationErrorKeys.PriceLessThanOne);

            RuleFor(x => x.Description).NotEmpty().WithErrorCode(RewardValidationErrorKeys.MissingDescription);

            RuleFor(x => x.DeliveryDate).NotEmpty().WithErrorCode(RewardValidationErrorKeys.MissingDeliveryDate);
            RuleFor(x => x.DeliveryDate).InclusiveBetween(DateTime.UtcNow, DateTime.UtcNow.AddYears(3))
                .When(x => x.DeliveryDate.HasValue)
                .WithErrorCode(RewardValidationErrorKeys.DeliveryDateOutOfRange)
                .WithCustomMessageParameters(_ => 
                    Task.FromResult(new[] { string.Format("{0:dd.MM.yyyy}", DateTime.UtcNow), string.Format("{0:dd.MM.yyyy}", DateTime.UtcNow.AddYears(3)) }));

            RuleFor(x => x.DeliveryCountries).NotNull().Must(x => x.Any())
                .When(x => x.DeliveryType == DeliveryType.SomeCountries || x.DeliveryType == DeliveryType.WholeWorld)
                .WithErrorCode(RewardValidationErrorKeys.MissingDeliveryCountries);
            RuleForEach(x => x.DeliveryCountries).SetValidator(new DeliveryCountryValidator())
                .When(x => (x.DeliveryType == DeliveryType.SomeCountries || x.DeliveryType == DeliveryType.WholeWorld) && (x.DeliveryCountries?.Any() ?? false));

            RuleFor(x => x.Limit).GreaterThanOrEqualTo(1)
                .When(x => x.Limit.HasValue)
                .WithErrorCode(RewardValidationErrorKeys.WrongLimitValue);
        }
    }
}
