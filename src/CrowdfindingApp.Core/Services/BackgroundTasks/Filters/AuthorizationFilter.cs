using CrowdfindingApp.Common.Extensions;
using Hangfire.Annotations;
using Hangfire.Dashboard;

namespace CrowdfindingApp.Core.Services.BackgroundTasks.Filters
{
    public class AuthorizationFilter : IDashboardAuthorizationFilter
    {
        private readonly string _roleName;

        public AuthorizationFilter(string roleName = nameof(Common.Immutable.Roles.Admin))
        {
            _roleName = roleName;
        }

        public bool Authorize([NotNull] DashboardContext context)
        {
            var user = context.GetHttpContext().User;
            return user.Identity.IsAuthenticated && user.HasRole(_roleName);
        }
    }
}
