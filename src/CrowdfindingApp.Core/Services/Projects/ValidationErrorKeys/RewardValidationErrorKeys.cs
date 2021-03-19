using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrowdfindingApp.Core.Services.Projects.ValidationErrorKeys
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
    }
}
