using Jobinator.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Jobinator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Offer()
        {
            return View();
        }
        public IActionResult Demand()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new User { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
