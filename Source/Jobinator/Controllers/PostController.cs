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
        public IActionResult Create(Post submitedPost)
        {
            // AuthHelper instance
            AuthHelper authHelper = new();

            // Check if user is logged in
            User? LoggedUser = authHelper.GetLoggedInUser(_Data, HttpContext);

            if (LoggedUser == null) return RedirectToAction("Login", "User");

            // Add user id to the post
            submitedPost.UserId = LoggedUser.Id;

            // Add post to database
            _Data.Posts.Add(submitedPost);

            // Save changes
            _Data.SaveChanges();

            // Redirect to user profile
            return RedirectToAction("Profile", "User");
        }
    }
}
