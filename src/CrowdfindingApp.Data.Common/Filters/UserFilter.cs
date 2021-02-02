using System;
using System.Collections.Generic;

namespace CrowdfindingApp.Data.Common.Filters
{
    public class UserFilter
    {
        public IList<Guid> Id { get; set; }
        public IList<Guid> RoleId { get; set; }
        public IList<string> UserName { get; set; }
        public IList<string> Email { get; set; }
        public IList<string> PasswordHash { get; set; }
        public bool? EmailConfirmed { get; set; }
        public bool? Active { get; set; }
        public DateTime? CreatedDateTimeFrom { get; set; }
        public DateTime? CreatedDateTimeTo { get; set; }
    }
}
