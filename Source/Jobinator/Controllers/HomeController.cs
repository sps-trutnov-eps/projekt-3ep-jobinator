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
        private readonly DataContext _Data;

        public HomeController(ILogger<HomeController> logger, DataContext Data)
        {
            _logger = logger;
            _Data = Data;
        }

        // Hlavní stránka - asynchronně načte všechny příspěvky včetně informací o autorech
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var posts = await _Data.Posts.Include(p => p.User).ToListAsync(); 
            return View(posts);
        }

        // Filtrování příspěvků - probíhá přímo na straně databáze pro vyšší výkon (používá GET pro sdílitelné URL)
        [HttpGet]
        public async Task<IActionResult> Filter(string? filterCategory, string? filterType)
        {
            // Inicializace dotazu pro dynamické skládání SQL příkazu
            var query = _Data!.Posts.Include(p => p.User).AsQueryable();

            // Filtrování podle kategorie, pokud byla vybrána
            if (!string.IsNullOrEmpty(filterCategory) && Enum.TryParse<JobCategory>(filterCategory, out var categoryEnum))
            {
                query = query.Where(p => p.Category == categoryEnum);
            }

            // Filtrování podle typu (Nabídka/Poptávka), pokud bylo vybráno
            if (!string.IsNullOrEmpty(filterType) && Enum.TryParse<PostType>(filterType, out var typeEnum))
            {
                query = query.Where(p => p.Type == typeEnum);
            }

            // Spuštění dotazu v databázi a asynchronní načtení výsledků
            var posts = await query.ToListAsync();

            return View("Index", posts);
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
