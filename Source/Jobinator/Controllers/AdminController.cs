using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Jobinator.Controllers
{
    public class AdminController : Controller
    {
        private readonly IConfiguration _config;
        public AdminController(IConfiguration config)
        {
            _config = config;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            //Load creds from config
            string adminUsername = _config["AdminCredentials:Username"];
            string adminPassword = _config["AdminCredentials:Password"];

            // Compare username and password
            if (username == adminUsername && password == adminPassword)
            {
                Debug.WriteLine("Login successful");
            }
            else
            {
                Debug.WriteLine("Login failed");
            }

            return RedirectToAction("Index");
        }
    }
}
