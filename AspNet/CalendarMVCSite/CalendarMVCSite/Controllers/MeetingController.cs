using BusinessLogic;
using BusinessLogic.Interfaces;
using CalendarMVCSite.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Models;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting.Json;

namespace CalendarMVCSite.Controllers
{
    public class MeetingController : Controller
    {
        private readonly ILogger<MeetingController> _logger;
        private readonly IMeetingsService _meetingService;
        private readonly Serilog.ILogger _serilogLogger;
        private readonly IValidator<CreateMeetingModel> _validator;

        public MeetingController(ILogger<MeetingController> logger, CalendarDbContext calendar, IMeetingsService meetingService, IValidator<CreateMeetingModel> validator)
        {
            _logger = logger;
            _meetingService = meetingService;
            //_serilogLogger = serilogLogger;
            _validator = validator;
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
            var validationResult = _validator.Validate(model);
            if (validationResult.IsValid)
            {
                try
                {
                    _meetingService.Create(new Meeting
                    {
                        Id = Guid.NewGuid(),
                        StartDate = model.StartDate.Value,
                        EndDate = model.EndDate.Value,
                        Name = model.Name
                    });
                }
                catch (Exception e)
                {
                    //_logger.Log("Request failed.");
                    _logger.LogError(e, "Request failed. Request details: {@model}", model);
                }

                return Redirect("Index");
            }
            else
            {
                return View();
            }
        }
    }
}