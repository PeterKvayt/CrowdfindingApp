
using CrowdfindingApp.Common.Enums;
using CrowdfindingApp.Common.Messages.Orders;
using CrowdfindingApp.Common.Validators;
using CrowdfindingApp.Data.Common.BusinessModels;
using FluentValidation;

namespace CrowdfindingApp.Core.Services.Orders.Validator
{
    public class AcceptOrderMessageValidator : AbstractValidator<AcceptOrderRequestMessage>
    {
        private readonly IdValidator _idValidator = new IdValidator();

        public AcceptOrderMessageValidator(Reward reward)
        {
            RuleFor(x => x.Count).GreaterThanOrEqualTo(1)
                .WithErrorCode(OrderErrorMessageKeys.RewardCountLessThanOne);

            RuleFor(x => x.CountryId)
                .SetValidator(_idValidator)
                .When(x =>  reward.DeliveryType == (int)DeliveryType.SomeCountries || reward.DeliveryType == (int)DeliveryType.WholeWorld);

            RuleFor(x => x.FullAddress).NotEmpty()
                .WithErrorCode(OrderErrorMessageKeys.EmptyFullAddress)
                .When(x => reward.DeliveryType == (int)DeliveryType.SomeCountries || reward.DeliveryType == (int)DeliveryType.WholeWorld);

            RuleFor(x => x.MiddleName).NotEmpty()
                .WithErrorCode(OrderErrorMessageKeys.EmptyMiddleName);

            RuleFor(x => x.Surname).NotEmpty()
                .WithErrorCode(OrderErrorMessageKeys.EmptySurname);

            RuleFor(x => x.Name).NotEmpty()
                .WithErrorCode(OrderErrorMessageKeys.EmptyName);

            RuleFor(x => x.PostCode).NotEmpty()
                .WithErrorCode(OrderErrorMessageKeys.EmptyPostCode)
                .When(x => reward.DeliveryType == (int)DeliveryType.SomeCountries || reward.DeliveryType == (int)DeliveryType.WholeWorld);
        }
    }
}
