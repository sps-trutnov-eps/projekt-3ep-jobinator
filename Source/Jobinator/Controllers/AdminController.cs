using Jobinator.Data;
using Jobinator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Jobinator.Controllers
{
    public class AdminController : Controller
    {
        private readonly IConfiguration _config;
        private DataContext? _Data;
        public AdminController(IConfiguration config, DataContext Data)
        {
            _config = config;
            _Data = Data;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            //Load creds from config
            string AdminUsername = _config["AdminCredentials:Username"];
            string AdminPassword = _config["AdminCredentials:Password"];

            // Compare username and password
            if (username == AdminUsername && password == AdminPassword)
            {
                Debug.WriteLine("Login successful");
                //Use the session to store that user is logged in as admin
                HttpContext.Session.SetString("Admin", "true");
            }
            else
            {
                Debug.WriteLine("Login failed");
            }

            return RedirectToAction("PostDashboard");
        }

        public IActionResult PostDashboard()
        {
            // Verify user is admin
            if (HttpContext.Session.GetString("Admin") != "true")
            {
                return RedirectToAction("Index");
            }
            //Load all posts
            List<Post> posts = _Data.Posts.Include(p => p.User).ToList() ?? new List<Post>();
            return View(posts); // Pass the 'posts' list into the view
        }
        //User accounts dashboard
        public IActionResult UserDashboard()
        {
            // Verify user is admin
            if (HttpContext.Session.GetString("Admin") != "true")
            {
                return RedirectToAction("Index");
            }
            // Load all users with their posts
            List<User> users = _Data.Users.Include(u => u.Posts).ToList() ?? new List<User>();
            return View(users); // Pass the 'users' list into the view
        }
    }
}
