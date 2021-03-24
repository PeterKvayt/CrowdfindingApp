
namespace CrowdfindingApp.Core.Services.Orders
{
    public static class OrderErrorMessageKeys
    {
        public static string RewardCountLessThanOne => $"{nameof(OrderErrorMessageKeys)}_{nameof(RewardCountLessThanOne)}";
        public static string EmptyFullAddress => $"{nameof(OrderErrorMessageKeys)}_{nameof(EmptyFullAddress)}";
        public static string EmptyMiddleName => $"{nameof(OrderErrorMessageKeys)}_{nameof(EmptyMiddleName)}";
        public static string EmptySurname => $"{nameof(OrderErrorMessageKeys)}_{nameof(EmptySurname)}";
        public static string EmptyName => $"{nameof(OrderErrorMessageKeys)}_{nameof(EmptyName)}";
        public static string EmptyPostCode => $"{nameof(OrderErrorMessageKeys)}_{nameof(EmptyPostCode)}";
    }
}
