using Jobinator.Models;
using Microsoft.AspNetCore.Mvc;
using Jobinator.Data;
using Jobinator.Helpers;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.EntityFrameworkCore;


namespace Jobinator.Controllers
{
    public class UserController : Controller
    {
        private DataContext? _Data;

        public UserController(DataContext Data)
        {
            _Data = Data;
        }
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registration(string Username, string Name, string Surname, string Password, string PasswordCheck)
        {
            //making sure that none of the fields are left empty
            if (Name == null) return View();
            if (Username == null) return View();
            if (Surname == null) return View();
            if (Password == null) return View();

            if (PasswordCheck != Password) return View(); //checking if the passwords match

            //checking to see if the username isnt already taken
            User? user = _Data.Users
                .Where(u => u.Username == Username)
                .FirstOrDefault();
            if (user != null) return View();

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
            _Data.SaveChanges();

            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string Username, string Password)
        {
            //checking if the fields arent empty
            if (Username == null) return View();
            if (Password == null) return View();


            //checking if the user exists
            User? User = _Data.Users
                .Where(u => u.Username == Username)
                .FirstOrDefault();
            if (User == null) return View();

            //verifying if the stored password and the entered password match
            if (!BCrypt.Net.BCrypt.Verify(Password, User.PasswordHash)) return View();

            //saving the username for display on profile page 
            HttpContext.Session.SetString("LoggedIn", Username);

            return RedirectToAction("Profile");
        }

        public IActionResult Profile()
        {

            AuthHelper authHelper = new();

            User? LoggedUser = authHelper.GetLoggedInUser(_Data, HttpContext);

            if (LoggedUser == null) return RedirectToAction("Login");


            // Query all posts from the database
            _Data.Entry(LoggedUser)
            .Collection(u => u.Posts)
            .Load();

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
        public IActionResult DeleteAccount(string Password)
        {
            AuthHelper authHelper = new();

            User? LoggedUser = authHelper.GetLoggedInUser(_Data, HttpContext);

            if (LoggedUser == null) return RedirectToAction("Index", "Home");

            if (!BCrypt.Net.BCrypt.Verify(Password, LoggedUser.PasswordHash)) return View();
            HttpContext.Session.Clear();

            _Data.Users.Remove(LoggedUser);
            _Data.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [Route("UserProfile/{username}")]
        public async Task<IActionResult> UserProfile(string username)
        {
            var user = await _Data.Users
                .Include(u => u.Posts)
                .FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var viewModel = new User
            {
                Username = user.Username,
                Posts = user.Posts
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Search(string query)
        {
            // Make sure user is logged in
            AuthHelper authHelper = new();
            User? LoggedUser = authHelper.GetLoggedInUser(_Data, HttpContext);

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
    }
}
