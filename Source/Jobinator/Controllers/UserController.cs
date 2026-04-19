using Jobinator.Models;
using Microsoft.AspNetCore.Mvc;
using Jobinator.Data;
using Jobinator.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;


namespace Jobinator.Controllers
{
    public class UserController : Controller
    {
        private readonly DataContext _Data;
        private readonly IAuthHelper _authHelper;

        public UserController(DataContext Data, IAuthHelper authHelper)
        {
            _Data = Data;
            _authHelper = authHelper;
        }
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(string Username, string Name, string Surname, string Password, string PasswordCheck)
        {
            //making sure that none of the fields are left empty
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Surname) || string.IsNullOrEmpty(Password)) 
                return View();

            if (PasswordCheck != Password) return View(); //checking if the passwords match

            //checking to see if the username isnt already taken
            bool userExists = await _Data.Users.AnyAsync(u => u.Username == Username);
            if (userExists) return View();

            User NewUser = new User()
            {
                Username = Username,
                Name = Name,
                Surname = Surname,
                // Hashing the password using BCrypt
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(Password),
            };

            // adding user to the database
            _Data.Users.Add(NewUser);
            await _Data.SaveChangesAsync();

            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string Username, string Password)
        {
            //checking if the fields arent empty
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password)) return View();


            //checking if the user exists
            User? user = await _Data.Users
                .FirstOrDefaultAsync(u => u.Username == Username);
            
            if (user == null) return View();

            //verifying if the stored password and the entered password match
            if (!BCrypt.Net.BCrypt.Verify(Password, user.PasswordHash)) return View();

            // Clear http session, just in case he was admin
            HttpContext.Session.Clear();

            //saving the username for display on profile page
            HttpContext.Session.SetString("LoggedIn", Username);

            return RedirectToAction("Profile");
        }

        public async Task<IActionResult> Profile()
        {
            User? LoggedUser = _authHelper.GetLoggedInUser();

            if (LoggedUser == null) return RedirectToAction("Login");


            // Query all posts from the database
            await _Data.Entry(LoggedUser)
                .Collection(u => u.Posts)
                .LoadAsync();

            return View(LoggedUser);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult DeleteAccount()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> DeleteAccount(string Password)
        {
            User? LoggedUser = _authHelper.GetLoggedInUser();

            if (LoggedUser == null) return RedirectToAction("Index", "Home");

            if (!BCrypt.Net.BCrypt.Verify(Password, LoggedUser.PasswordHash)) return View();
            HttpContext.Session.Clear();

            _Data.Users.Remove(LoggedUser);
            await _Data.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> UserProfile(string username)
        {
            Debug.WriteLine(username);
            var user = await _Data.Users
                .Include(u => u.Posts)
                .FirstOrDefaultAsync(u => u.Username == username);
            
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            // Show amount of likes for user
            int likesCount = await _Data.Likes.CountAsync(l => l.LikedUserId == user.Id);
            ViewBag.LikesCount = likesCount;

            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> Search(string query)
        {
            // Make sure user is logged in
            User? LoggedUser = _authHelper.GetLoggedInUser();

            // Redirect to homepage if not logged in
            if (LoggedUser == null)
            {
                return RedirectToAction("Index", "Home");
            }

            // Show the Search page if there's no query
            if (string.IsNullOrWhiteSpace(query))
            {
                return View();
            }

            var user = await _Data.Users
                .FirstOrDefaultAsync(u => u.Username.Contains(query));

            // If user not found
            if (user == null)
            {
                return View();
            }

            // Redirect to user's profile
            return RedirectToAction("UserProfile", "User", new { username = user.Username });
        }

        [HttpPost]
        public async Task<IActionResult> LikeProfile(string username)
        {
            // Make sure user is logged in
            User? loggedInUser = _authHelper.GetLoggedInUser();

            // Redirect to homepage if not logged in
            if (loggedInUser == null)
            {
                return RedirectToAction("Login");
            }

            // Getting liked
            var likedUser = await _Data.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (likedUser == null || likedUser.Id == loggedInUser.Id)
            {
                // No self-liking
                return RedirectToAction("UserProfile", new { username });
            }

            // Check if wasn't liked before
            var existingLike = await _Data.Likes
                .FirstOrDefaultAsync(l => l.LikerId == loggedInUser.Id && l.LikedUserId == likedUser.Id);

            if (existingLike == null)
            {
                // Create new like
                var like = new Like
                {
                    LikerId = loggedInUser.Id,
                    LikedUserId = likedUser.Id
                };
                _Data.Likes.Add(like);
                await _Data.SaveChangesAsync();
            }

            return RedirectToAction("UserProfile", new { username });
        }
    }
}
