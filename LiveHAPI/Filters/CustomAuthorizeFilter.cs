using Hangfire.Dashboard;

namespace LiveHAPI.Filters
{
    public class CustomAuthorizeFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            return true;
        }
    }
}
