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
            // Kontrola, zda je uživatel již přihlášen jako administrátor
            if (HttpContext.Session.GetString("Admin") == "true")
            {
                return RedirectToAction("PostDashboard");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // Načtení administrátorských údajů z konfiguračního souboru
            string AdminUsername = _config["AdminCredentials:Username"];
            string AdminPassword = _config["AdminCredentials:Password"];

            // Porovnání zadaných údajů s konfigurací
            if (username == AdminUsername && password == AdminPassword)
            {
                Debug.WriteLine("Admin Login successful");
                // Vymazání session pro případ, že byl uživatel přihlášen jako běžný uživatel
                HttpContext.Session.Clear();
                // Nastavení admin příznaku do session
                HttpContext.Session.SetString("Admin", "true");
            }
            else
            {
                Debug.WriteLine("Admin Login failed");
            }

            return RedirectToAction("PostDashboard");
        }

        // Dashboard pro správu všech příspěvků
        public async Task<IActionResult> PostDashboard()
        {
            // Ověření admin přístupu
            if (HttpContext.Session.GetString("Admin") != "true")
            {
                return RedirectToAction("Index");
            }
            // Načtení všech příspěvků včetně informací o autorech
            List<Post> posts = await _Data.Posts.Include(p => p.User).ToListAsync() ?? new List<Post>();
            return View(posts); 
        }

        // Dashboard pro správu uživatelských účtů
        public async Task<IActionResult> UserDashboard()
        {
            if (HttpContext.Session.GetString("Admin") != "true")
            {
                return RedirectToAction("Index");
            }
            // Načtení všech uživatelů i s jejich příspěvky
            List<User> users = await _Data.Users.Include(u => u.Posts).ToListAsync() ?? new List<User>();
            return View(users); 
        }

        // Odstranění příspěvku administrátorem
        [HttpPost]
        public async Task<IActionResult> DeletePost(int postId)
        {
            if (HttpContext.Session.GetString("Admin") != "true")
            {
                return RedirectToAction("Index");
            }
            // Vyhledání a smazání příspěvku
            Post? post = await _Data.Posts.FindAsync(postId);
            if (post != null)
            {
                _Data.Posts.Remove(post);
                await _Data.SaveChangesAsync();
            }
            return RedirectToAction("PostDashboard");
        }

        // Odstranění uživatelského účtu administrátorem
        [HttpPost]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            if (HttpContext.Session.GetString("Admin") != "true")
            {
                return RedirectToAction("Index");
            }
            // Vyhledání a smazání uživatele
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
