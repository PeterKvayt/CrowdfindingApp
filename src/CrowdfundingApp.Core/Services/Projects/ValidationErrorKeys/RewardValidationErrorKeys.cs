namespace CrowdfundingApp.Core.Services.Projects.ValidationErrorKeys
{
    public static class RewardValidationErrorKeys
    {
        public static string MissingTitle => $"{nameof(RewardValidationErrorKeys)}_{nameof(MissingTitle)}";
        public static string MissingPrice => $"{nameof(RewardValidationErrorKeys)}_{nameof(MissingPrice)}";
        public static string MissingDescription => $"{nameof(RewardValidationErrorKeys)}_{nameof(MissingDescription)}";
        public static string MissingDeliveryDate => $"{nameof(RewardValidationErrorKeys)}_{nameof(MissingDeliveryDate)}";
        public static string MissingDeliveryCountries => $"{nameof(RewardValidationErrorKeys)}_{nameof(MissingDeliveryCountries)}";
        public static string EmptyDeliveryCountries => $"{nameof(RewardValidationErrorKeys)}_{nameof(EmptyDeliveryCountries)}";
        public static string WrongDeliveryCountriesIds => $"{nameof(RewardValidationErrorKeys)}_{nameof(WrongDeliveryCountriesIds)}";
        public static string WrongLimitValue => $"{nameof(RewardValidationErrorKeys)}_{nameof(WrongLimitValue)}";
        public static string PriceLessThanOne => $"{nameof(RewardValidationErrorKeys)}_{nameof(PriceLessThanOne)}";
        public static string DeliveryDateOutOfRange => $"{nameof(RewardValidationErrorKeys)}_{nameof(DeliveryDateOutOfRange)}";
        public static string DeliveryPriceMissing => $"{nameof(RewardValidationErrorKeys)}_{nameof(DeliveryPriceMissing)}";
        public static string DeliveryPriceLessThanOne => $"{nameof(RewardValidationErrorKeys)}_{nameof(DeliveryPriceLessThanOne)}";
    }
}
