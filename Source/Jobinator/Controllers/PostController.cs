using Jobinator.Data;
using Jobinator.Helpers;
using Jobinator.Models;
using Microsoft.AspNetCore.Mvc;

namespace Jobinator.Controllers
{
    public class PostController : Controller
    {
        private DataContext? _Data;
        public PostController(DataContext Data)
        {
            _Data = Data;
        }

        [HttpPost]
        public IActionResult Create()
        {
            // Create an instance of the AuthHelper manually
            AuthHelper authHelper = new();

            // Pass the DataContext and HttpContext to the helper method
            User? LoggedUser = authHelper.GetLoggedInUser(_Data, HttpContext);

            if (LoggedUser == null) return RedirectToAction("Login", "User");

            return RedirectToAction("Profile", "User");
        }
    }
}
