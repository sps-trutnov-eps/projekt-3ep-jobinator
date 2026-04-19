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
            //Check if user is already logged in as admin
            if (HttpContext.Session.GetString("Admin") == "true")
            {
                return RedirectToAction("PostDashboard");
            }
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
                Debug.WriteLine("Admin Login successful");
                //Clear the session, in case user was logged in as a user
                HttpContext.Session.Clear();
                //Use the session to store that user is logged in as admin
                HttpContext.Session.SetString("Admin", "true");
            }
            else
            {
                Debug.WriteLine("Admin Login failed");
            }

            return RedirectToAction("PostDashboard");
        }

        public async Task<IActionResult> PostDashboard()
        {
            // Verify user is admin
            if (HttpContext.Session.GetString("Admin") != "true")
            {
                return RedirectToAction("Index");
            }
            //Load all posts
            List<Post> posts = await _Data.Posts.Include(p => p.User).ToListAsync() ?? new List<Post>();
            return View(posts); // Pass the 'posts' list into the view
        }
        //User accounts dashboard
        public async Task<IActionResult> UserDashboard()
        {
            // Verify user is admin
            if (HttpContext.Session.GetString("Admin") != "true")
            {
                return RedirectToAction("Index");
            }
            // Load all users with their posts
            List<User> users = await _Data.Users.Include(u => u.Posts).ToListAsync() ?? new List<User>();
            return View(users); // Pass the 'users' list into the view
        }

        //Delete post
        [HttpPost]
        public async Task<IActionResult> DeletePost(int postId)
        {
            // Verify user is admin
            if (HttpContext.Session.GetString("Admin") != "true")
            {
                return RedirectToAction("Index");
            }
            // Find the post by id
            Post? post = await _Data.Posts.FindAsync(postId);
            if (post != null)
            {
                _Data.Posts.Remove(post);
                await _Data.SaveChangesAsync();
            }
            return RedirectToAction("PostDashboard");
        }

        //Delete user
        [HttpPost]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            // Verify user is admin
            if (HttpContext.Session.GetString("Admin") != "true")
            {
                return RedirectToAction("Index");
            }
            // Find the user by id
            User? user = await _Data.Users.FindAsync(userId);
            if (user != null)
            {
                _Data.Users.Remove(user);
                await _Data.SaveChangesAsync();
            }
            return RedirectToAction("UserDashboard");
        }
    }
}
