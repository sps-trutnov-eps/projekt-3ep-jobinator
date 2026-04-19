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

        // Vytvoření nového příspěvku (nabídka nebo poptávka)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostCreateViewModel model)
        {
            // Kontrola, zda je uživatel přihlášen pomocí injektovaného pomocníka
            User? LoggedUser = _authHelper.GetLoggedInUser();
            if (LoggedUser == null) return RedirectToAction("Login", "User");

            // Validace vstupních dat
            if (!ModelState.IsValid)
            {
                // Při neúspěšné validaci přesměruje zpět na příslušnou stránku
                return RedirectToAction(model.Type == Post.PostType.Offer ? "Offer" : "Demand", "Home");
            }

            // Mapování ViewModelu na databázový model Post
            Post newPost = new Post
            {
                Title = model.Title,
                Content = model.Content,
                Category = model.Category,
                Type = model.Type,
                UserId = LoggedUser.Id
            };

            // Přidání do DB a asynchronní uložení změn
            _Data.Posts.Add(newPost);
            await _Data.SaveChangesAsync();

            // Po úspěšném vytvoření přesměruje na profil uživatele
            return RedirectToAction("Profile", "User");
        }
    }
}
