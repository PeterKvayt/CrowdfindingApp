
using System;
using CrowdfindingApp.Common.Messages.Payment;

namespace CrowdfindingApp.Common.Maintainers.Payment
{
    public interface IPaymentManager
    {
        string GetPaymentUri(decimal sum, string description, Guid orderId);

        bool ValidateResponse(PaymentRequestMessage message, string id);
    }
}
