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
        public IActionResult Index()
        {
            var posts = _Data.Posts.ToList(); // loads all posts into a list that is then pasted onto the view

            return View(posts);
        }

        [HttpPost]
        public IActionResult Filter()
        {
            var posts = _Data.Posts.ToList(); // a list that will contain all the posts that pass through the filter
            var removedPosts = new List<Post>(); // list where all the posts that failed the filter will be stored
            string filteredCategory = Request.Form["filterCategory"].ToString(); // saves the selected value in the category filter 
            string filteredType = Request.Form["filterType"].ToString();  // saves the selected value in the type filter 

            foreach (var post in posts) {
                if (!string.IsNullOrEmpty(filteredCategory))  // checks if a filter is selected
                {
                    if (!Convert.ToString(post.Category).Equals(filteredCategory)) // checks if the post category is the same as the filtered one
                    {
                        removedPosts.Add(post); //adds the posts that didnt pass the filter
                    }
                }
                if (!string.IsNullOrEmpty(filteredType)) // checks if a filter is selected
                {
                    if (!Convert.ToString(post.Type).Equals(filteredType)) // checks if the post category is the same as the filtered one
                    {
                        removedPosts.Add(post); //adds the posts that didnt pass the filter
                    }
                }
            }
            foreach (var post in removedPosts) {
                posts.Remove(post); //removes the posts from the main list
            }

        return View("Index", posts); // returns the filtered posts
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
