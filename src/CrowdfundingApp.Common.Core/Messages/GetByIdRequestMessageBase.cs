using System.ComponentModel.DataAnnotations;

namespace CrowdfindingApp.Common.Core.Messages
{
    public abstract class GetByIdRequestMessageBase : MessageBase
    {

        [Required]
        public string Id { get; set; }
    }
}
