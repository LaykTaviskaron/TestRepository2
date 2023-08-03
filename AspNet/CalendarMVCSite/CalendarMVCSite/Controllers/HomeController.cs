using BusinessLogic;
using CalendarMVCSite.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CalendarMVCSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CalendarDbContext _calendar;

        public HomeController(ILogger<HomeController> logger, CalendarDbContext calendar)
        {
            _logger = logger;
            _calendar = calendar;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Index", "Meeting");
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
    }
}