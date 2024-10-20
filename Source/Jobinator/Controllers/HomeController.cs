using Jobinator.Data;
using Jobinator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public IActionResult Index()
        {
            var posts = _Data.Posts.ToList(); // loads all posts into a list that is then pasted onto the view

            return View(posts);
        }

        [HttpPost]
        public IActionResult Filter()
        {
            var posts = _Data.Posts.ToList();
            var filteredPosts = new List<Post>(); // a list that will contain all the posts that pass through the filter
            string filteredCategory = Request.Form["filter"].ToString(); // saves the selected value in the filter 

            if (!string.IsNullOrEmpty(filteredCategory)) // checks if the filter isnt empty
            { 
                foreach (var post in posts)
                {
                    if (Convert.ToString(post.Category).Equals(filteredCategory)) filteredPosts.Add(post); // checks if the post category is the same as the filtered one
                }
            }
            else return View("Index", posts); // if no filter is selected it returns all posts

        return View("Index", filteredPosts); // returns the filtered posts
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
