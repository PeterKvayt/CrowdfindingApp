
namespace CrowdfundingApp.Core.Services.Orders
{
    public static class OrderErrorMessageKeys
    {
        public static string RewardCountLessThanOne => $"{nameof(OrderErrorMessageKeys)}_{nameof(RewardCountLessThanOne)}";
        public static string EmptyFullAddress => $"{nameof(OrderErrorMessageKeys)}_{nameof(EmptyFullAddress)}";
        public static string EmptyMiddleName => $"{nameof(OrderErrorMessageKeys)}_{nameof(EmptyMiddleName)}";
        public static string EmptySurname => $"{nameof(OrderErrorMessageKeys)}_{nameof(EmptySurname)}";
        public static string EmptyName => $"{nameof(OrderErrorMessageKeys)}_{nameof(EmptyName)}";
        public static string EmptyPostCode => $"{nameof(OrderErrorMessageKeys)}_{nameof(EmptyPostCode)}";
        public static string GreaterThanLimit => $"{nameof(OrderErrorMessageKeys)}_{nameof(GreaterThanLimit)}";
        public static string DisallowToSupportProject => $"{nameof(OrderErrorMessageKeys)}_{nameof(DisallowToSupportProject)}";
        public static string EmptyCvv => $"{nameof(OrderErrorMessageKeys)}_{nameof(EmptyCvv)}";
        public static string WrongCvvValue => $"{nameof(OrderErrorMessageKeys)}_{nameof(WrongCvvValue)}";
        public static string EmptyPayCardExpirationDate => $"{nameof(OrderErrorMessageKeys)}_{nameof(EmptyPayCardExpirationDate)}";
        public static string WrongPayCardExpirationDate => $"{nameof(OrderErrorMessageKeys)}_{nameof(WrongPayCardExpirationDate)}";
        public static string EmptyPayCardNumber => $"{nameof(OrderErrorMessageKeys)}_{nameof(EmptyPayCardNumber)}";
        public static string WrongPayCardNumber => $"{nameof(OrderErrorMessageKeys)}_{nameof(WrongPayCardNumber)}";
        public static string EmptyPayCardOwnerName => $"{nameof(OrderErrorMessageKeys)}_{nameof(EmptyPayCardOwnerName)}";
        public static string WrongPayCardOwnerName => $"{nameof(OrderErrorMessageKeys)}_{nameof(WrongPayCardOwnerName)}";

    }
}
