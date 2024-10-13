using Jobinator.Data;
using Jobinator.Models;

namespace Jobinator.Helpers
{
    public class AuthHelper
    {
        public User? GetLoggedInUser(DataContext data, HttpContext httpContext)
        {
            string? LoggedIn = httpContext.Session.GetString("LoggedIn");
            if (LoggedIn == null) return null;

            User? LoggedUser = data.Users
                .Where(u => u.Username == LoggedIn)
                .FirstOrDefault();
            return LoggedUser;
        }
    }
}