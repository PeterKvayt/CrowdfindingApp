﻿
namespace CrowdfindingApp.Data.Common.Models
{
    public sealed class Role : BaseModel
    {
        public string Name { get; set; }
        public string Permissions { get; set; }
    }
}