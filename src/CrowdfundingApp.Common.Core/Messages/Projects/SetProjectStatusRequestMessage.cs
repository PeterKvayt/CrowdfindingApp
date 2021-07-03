using System.ComponentModel.DataAnnotations;
using CrowdfundingApp.Common.Enums;

namespace CrowdfundingApp.Common.Core.Messages.Projects
{
    public class SetProjectStatusRequestMessage : MessageBase
    {
        [Required]
        public ProjectStatus Status { get; set; }
        [Required]
        public string ProjectId { get; set; }
    }
}
