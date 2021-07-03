
using System;
using CrowdfindingApp.Common.Core.Messages.Payment;

namespace CrowdfindingApp.Common.Core.Maintainers.Payment
{
    public interface IPaymentManager
    {
        string GetPaymentUri(decimal sum, string description, Guid orderId);

        bool ValidateResponse(PaymentRequestMessage message, string id);
    }
}
