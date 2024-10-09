using Jobinator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using static Jobinator.Models.Post;

namespace Jobinator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SubmitForm(string title, string content, OfferRequest offerRequest, JobCategory category)
        {
            var post = new Post
            {
                Title = title,
                Content = content,
                OfferRequest = offerRequest,
                JobCategory = category
            };

            // Add the new entry to the database
            _context.Entries.Add(post);
            _context.SaveChanges();

            // Use TempData to display a success message
            TempData["Message"] = $"Title: {title}, Content: {content}, Offer/Request: {offerRequest}, Category: {category}";

            // Refresh page after submission
            return RedirectToAction("Index");
        }
    }
}
