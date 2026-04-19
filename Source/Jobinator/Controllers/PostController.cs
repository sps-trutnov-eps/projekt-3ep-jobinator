using Jobinator.Data;
using Jobinator.Helpers;
using Jobinator.Models;
using Microsoft.AspNetCore.Mvc;

namespace Jobinator.Controllers
{
    public class PostController : Controller
    {
        private readonly DataContext _Data;
        private readonly IAuthHelper _authHelper;

        public PostController(DataContext Data, IAuthHelper authHelper)
        {
            _Data = Data;
            _authHelper = authHelper;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Post submitedPost)
        {
            // Check if user is logged in using injected AuthHelper
            User? LoggedUser = _authHelper.GetLoggedInUser();

            if (LoggedUser == null) return RedirectToAction("Login", "User");

            // Add user id to the post
            submitedPost.UserId = LoggedUser.Id;

            // Add post to database
            _Data.Posts.Add(submitedPost);

            // Save changes
            await _Data.SaveChangesAsync();

            // Redirect to user profile
            return RedirectToAction("Profile", "User");
        }
    }
}
