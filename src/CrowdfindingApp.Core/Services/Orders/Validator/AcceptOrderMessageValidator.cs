
using System;
using System.Threading.Tasks;
using CrowdfindingApp.Common.Enums;
using CrowdfindingApp.Common.Extensions;
using CrowdfindingApp.Common.Core.Messages.Orders;
using CrowdfindingApp.Common.Core.Validators;
using CrowdfindingApp.Common.Data.BusinessModels;
using FluentValidation;

namespace CrowdfindingApp.Core.Services.Orders.Validator
{
    public class AcceptOrderMessageValidator : AbstractValidator<AcceptOrderRequestMessage>
    {
        private readonly IdValidator _idValidator = new IdValidator();

        public AcceptOrderMessageValidator(Reward reward, int orderedCount, int orderCount)
        {
            RuleFor(x => x.Count).GreaterThanOrEqualTo(1)
                .WithErrorCode(OrderErrorMessageKeys.RewardCountLessThanOne);

            RuleFor(x => x.CountryId)
                .SetValidator(_idValidator)
                .When(x => reward.DeliveryType == (int)DeliveryType.SomeCountries || reward.DeliveryType == (int)DeliveryType.WholeWorld);

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

            RuleFor(x => reward.Limit.Value).GreaterThanOrEqualTo(orderedCount + orderCount)
                .WithErrorCode(OrderErrorMessageKeys.GreaterThanLimit)
                .WithCustomMessageParameters(x => Task.FromResult((reward.Limit - orderedCount).ToString()))
                .When(_ => reward.IsLimited);

            RuleFor(x => x.PayCardCvv).NotEmpty()
                .WithErrorCode(OrderErrorMessageKeys.EmptyCvv);

            RuleFor(x => x.PayCardCvv).Matches("^[0-9]{3}$")
                .WithErrorCode(OrderErrorMessageKeys.WrongCvvValue)
                .When(x => x.PayCardCvv.IsPresent());

            RuleFor(x => x.PayCardExpirationDate).NotNull()
                .WithErrorCode(OrderErrorMessageKeys.EmptyPayCardExpirationDate);

            RuleFor(x => x.PayCardExpirationDate).GreaterThan(DateTime.UtcNow)
                .WithErrorCode(OrderErrorMessageKeys.WrongPayCardExpirationDate)
                .When(x => x.PayCardExpirationDate.HasValue);

            RuleFor(x => x.PayCardNumber).NotEmpty()
                .WithErrorCode(OrderErrorMessageKeys.EmptyPayCardNumber);

            RuleFor(x => x.PayCardNumber).Matches("^[0-9]{16}$")
                .WithErrorCode(OrderErrorMessageKeys.WrongPayCardNumber)
                .When(x => x.PayCardNumber.IsPresent());

            RuleFor(x => x.PayCardOwnerName).NotEmpty()
                .WithErrorCode(OrderErrorMessageKeys.EmptyPayCardOwnerName);

            RuleFor(x => x.PayCardOwnerName).Matches("^[a-zA-Z ]{1,20}$")
                .WithErrorCode(OrderErrorMessageKeys.WrongPayCardOwnerName)
                .When(x => x.PayCardOwnerName.IsPresent());
        }
    }
}
