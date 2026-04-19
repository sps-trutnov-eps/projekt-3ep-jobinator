using Jobinator.Data;
using Jobinator.Helpers;
using Jobinator.Models;
using Jobinator.Models.ViewModels;
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
        public async Task<IActionResult> Create(PostCreateViewModel model)
        {
            // Check if user is logged in
            User? LoggedUser = _authHelper.GetLoggedInUser();
            if (LoggedUser == null) return RedirectToAction("Login", "User");

            if (!ModelState.IsValid)
            {
                // If validation fails, we return to the view that called it.
                // Depending on your UI, you might want to return to Offer or Demand views.
                return RedirectToAction(model.Type == Post.PostType.Offer ? "Offer" : "Demand", "Home");
            }

            // Map ViewModel to Database Model
            Post newPost = new Post
            {
                Title = model.Title,
                Content = model.Content,
                Category = model.Category,
                Type = model.Type,
                UserId = LoggedUser.Id
            };

            // Add post to database
            _Data.Posts.Add(newPost);

            // Save changes
            await _Data.SaveChangesAsync();

            // Redirect to user profile
            return RedirectToAction("Profile", "User");
        }
    }
}
