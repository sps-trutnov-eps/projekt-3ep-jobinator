using Microsoft.AspNetCore.Mvc;

namespace Jobinator.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Profile()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
    }
}
