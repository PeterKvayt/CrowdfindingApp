﻿using System.ComponentModel.DataAnnotations;
using CrowdfindingApp.Common.Enums;

namespace CrowdfindingApp.Common.Messages.Projects
{
    public class SetProjectStatusRequestMessage : MessageBase
    {
        [Required]
        public ProjectStatus Status { get; set; }
        [Required]
        public string ProjectId { get; set; }
    }
}