using System;
using CrowdfindingApp.Data.Common.Models;

namespace CrowdfindingApp.Data.Common.BusinessModels
{
    public sealed class User : BaseModel
    {
        public Guid RoleId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string Image { get; set; }
    }
}
