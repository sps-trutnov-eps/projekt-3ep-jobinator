using Jobinator.Data;
using Jobinator.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Jobinator.Helpers
{
    public interface IAuthHelper
    {
        User? GetLoggedInUser();
    }

    public class AuthHelper : IAuthHelper
    {
        private readonly DataContext _data;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthHelper(DataContext data, IHttpContextAccessor httpContextAccessor)
        {
            _data = data;
            _httpContextAccessor = httpContextAccessor;
        }

        // Metoda pro získání aktuálně přihlášeného uživatele ze session
        public User? GetLoggedInUser()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null) return null;

            // Získání uživatelského jména uloženého v session pod klíčem "LoggedIn"
            string? loggedInUsername = httpContext.Session.GetString("LoggedIn");
            if (string.IsNullOrEmpty(loggedInUsername)) return null;

            // Vyhledání uživatele v databázi podle jména ze session
            return _data.Users
                .FirstOrDefault(u => u.Username == loggedInUsername);
        }
    }
}
