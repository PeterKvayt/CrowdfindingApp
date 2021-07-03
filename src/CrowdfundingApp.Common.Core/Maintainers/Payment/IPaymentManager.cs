
using System;
using CrowdfundingApp.Common.Core.Messages.Payment;

namespace CrowdfundingApp.Common.Core.Maintainers.Payment
{
    public interface IPaymentManager
    {
        string GetPaymentUri(decimal sum, string description, Guid orderId);

        bool ValidateResponse(PaymentRequestMessage message, string id);
    }
}
