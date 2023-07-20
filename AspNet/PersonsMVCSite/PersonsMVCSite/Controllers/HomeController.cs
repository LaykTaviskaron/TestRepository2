using Microsoft.AspNetCore.Mvc;
using PersonsMVCSite.Context;
using PersonsMVCSite.Models;
using System.Diagnostics;

namespace PersonsMVCSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PersonDbContext _persons;

        public HomeController(ILogger<HomeController> logger, PersonDbContext persons)
        {
            _logger = logger;
            _persons = persons;
        }

        public IActionResult Index()
        {
            var model = new PersonsModel();

            model.PersonsList = _persons.People.ToList();

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(Person person)
        {
            _persons.People.Add(person);
            _persons.SaveChanges();

            return Redirect("Index");
        }

        [HttpGet]
        public IActionResult Create()
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
    }
}