using BusinessLogic;
using BusinessLogic.Services;
using CalendarMVCSite.Models;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace CalendarMVCSite.Controllers
{
    public class MeetingController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MeetingsService _meetingService;

        public MeetingController(ILogger<HomeController> logger, CalendarDbContext calendar)
        {
            _logger = logger;
            _meetingService = new MeetingsService(calendar);
        }

        public IActionResult Index()
        {
            var model = new IndexViewModel();

            var meetings = _meetingService.GetAll();

            model.Meetings = meetings;

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([FromForm] CreateMeetingModel model)
        {
            if (model != null)
            {
                _meetingService.Create(new Meeting
                {
                    Id = Guid.NewGuid(),
                    StartDate = model.StartDate.Value,
                    EndDate = model.EndDate.Value,
                    Name = model.Name
                });
            }
            else
            {
                throw new ArgumentException("Invalid input");
            }

            return Redirect("Index");
        }
    }
}