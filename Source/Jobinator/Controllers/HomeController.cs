using Jobinator.Data;
using Jobinator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using static Jobinator.Models.Post;

namespace Jobinator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private DataContext? _Data;

        public HomeController(ILogger<HomeController> logger, DataContext Data)
        {
            _logger = logger;
            _Data = Data;
        }


        [HttpGet]
        public IActionResult Index()
        {
            var posts = _Data.Posts.ToList();

            return View(posts);
        }

        public IActionResult Offer()
        {
            return View();
        }
        public IActionResult Demand()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new User { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
