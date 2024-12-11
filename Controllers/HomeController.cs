using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Moot.Models;
using Microsoft.EntityFrameworkCore;
using Moot.Data;
using Moot.Models.LibraryViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Moot.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly LibraryContext _context;

        public HomeController(LibraryContext context, ILogger<HomeController> logger)
        {
            _context = context;
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<ActionResult> Statistics()
        {
            IQueryable<OrderGroup> data =
            from offer in _context.Offer
            group offer by offer.OfferDate into dateGroup
            select new OrderGroup()
            {
                OfferDate = dateGroup.Key,
                PropertyCount = dateGroup.Count()
            };

            return View(await data.AsNoTracking().ToListAsync());
        }

        public IActionResult Chat()
        {
            return View();
        }

        public IActionResult Notification()
        {
            return View();
        }
    }
}

