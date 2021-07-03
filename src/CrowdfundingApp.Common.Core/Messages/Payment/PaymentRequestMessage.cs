
namespace CrowdfundingApp.Common.Core.Messages.Payment
{
    public class PaymentRequestMessage : MessageBase
    {
        public decimal OutSum { get; set; }

        public decimal Fee { get; set; }

        public string InvId { get; set; }

        public string EMail { get; set; }

        public string SignatureValue { get; set; }
    }
}
