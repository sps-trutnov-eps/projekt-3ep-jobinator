using Jobinator.Data;
using Jobinator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.Versioning;
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
        public async Task<IActionResult> Index()
        {
            var posts = await _Data.Posts.Include(p => p.User).ToListAsync(); // loads all posts with user information into a list that is then pasted onto the view

            return View(posts);
        }

        [HttpPost]
        public async Task<IActionResult> Filter(string filterCategory, string filterType)
        {
            // Start with a queryable object to build the SQL command dynamically
            var query = _Data.Posts.Include(p => p.User).AsQueryable();

            // Apply category filter if selected
            if (!string.IsNullOrEmpty(filterCategory) && Enum.TryParse<JobCategory>(filterCategory, out var categoryEnum))
            {
                query = query.Where(p => p.Category == categoryEnum);
            }

            // Apply type filter if selected
            if (!string.IsNullOrEmpty(filterType) && Enum.TryParse<PostType>(filterType, out var typeEnum))
            {
                query = query.Where(p => p.Type == typeEnum);
            }

            // Execute the query at the database level
            var posts = await query.ToListAsync();

            return View("Index", posts);
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
