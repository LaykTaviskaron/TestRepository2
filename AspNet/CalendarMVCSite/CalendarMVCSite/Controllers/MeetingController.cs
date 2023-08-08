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
    [Route("meeting")]
    public class MeetingController : Controller
    {
        private readonly ILogger<MeetingController> _logger;
        private readonly IMeetingsService _meetingService;
        private readonly Serilog.ILogger _serilogLogger;
        private readonly IValidator<CreateMeetingModel> _createMeetingValidator;
        private readonly IValidator<EditMeetingModel> _editMeetingValidator;
        private readonly IValidator<CreateRecurrentMeetingModel> _createRecurrentMeetingValidator;

        public MeetingController(
            ILogger<MeetingController> logger, 
            CalendarDbContext calendar, 
            IMeetingsService meetingService, 
            IValidator<CreateMeetingModel> validator, 
            IValidator<EditMeetingModel> editMeetingValidator)
        {
            _logger = logger;
            _meetingService = meetingService;
            //_serilogLogger = serilogLogger;
            _createMeetingValidator = validator;
            _editMeetingValidator = editMeetingValidator;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            var model = new IndexViewModel();

            var meetings = _meetingService.GetAll();

            model.Meetings = meetings;

            return View(model);
        }

        [HttpGet("day/{day}")]
        public IActionResult Daily(DateTime? day)
        {
            var model = new DailyViewModel();

            if (day == null || !day.HasValue)
            {
                model.Meetings = _meetingService.GetAll();
            }
            else
            {
                var start = day.Value.Date;
                var end = start.AddDays(1).AddTicks(-1);
                model.Meetings = _meetingService.GetByDateRange(start, end);
            }

            return View(model);
        }

        [HttpGet("week/{startDay}")]
        public IActionResult Weekly(DateTime? startDay)
        {
            var model = new DailyViewModel();

            if (startDay == null || !startDay.HasValue)
            {
                model.Meetings = _meetingService.GetAll();
            }
            else
            {
                var start = startDay.Value.Date;
                var end = start.AddDays(7).AddTicks(-1);
                model.Meetings = _meetingService.GetByDateRange(start, end);
            }

            return View(model);
        }

        [HttpGet("month/{startDay}")]
        public IActionResult Monthly(DateTime? startDay)
        {
            var model = new DailyViewModel();

            if (startDay == null || !startDay.HasValue)
            {
                model.Meetings = _meetingService.GetAll();
            }
            else
            {
                var startDayValue = startDay.Value;
                var firstDayOfMonth = new DateTime(startDayValue.Year, startDayValue.Month, 1);
                var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddTicks(-1);
                model.Meetings = _meetingService.GetByDateRange(firstDayOfMonth, lastDayOfMonth);
            }

            return View(model);
        }


        [HttpGet("edit/{id}")]
        public IActionResult Edit(Guid id)
        {
            var meeting = _meetingService.GetById(id);

            var meetingModel = new EditMeetingModel
            {
                Id = id,
                EndDate = meeting.EndDate,
                StartDate = meeting.StartDate,
                Name = meeting.Name
            };

            return View(meetingModel);
        }

        [HttpPost("edit/{id}")]
        public IActionResult Edit(Guid id, [FromForm] EditMeetingModel model)
        {
            var validationResult = _editMeetingValidator.Validate(model);
            if (validationResult.IsValid)
            {
                try
                {
                    _meetingService.Edit(new Meeting
                    {
                        Id = model.Id,
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

                return RedirectToAction("Index", "Meeting");
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet("delete/{id}")]
        public IActionResult Delete(Guid id)
        {
            var meeting = _meetingService.GetById(id);

            return View();
        }

        [HttpPost("Delete/{id}")]
        public IActionResult ConfirmDeletion(Guid id)
        {
            try
            {
                _meetingService.DeleteById(id);
            }
            catch (Exception e)
            {
                //_logger.Log("Request failed.");
                _logger.LogError(e, "Request failed. Request details: {id}", id);
            }

            return RedirectToAction("Index", "Meeting");
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("create")]
        public IActionResult Create([FromForm] CreateMeetingModel model)
        {
            var validationResult = _createMeetingValidator.Validate(model);
            if (validationResult.IsValid)
            {
                try
                {
                    _meetingService.Create(new Meeting
                    {
                        Id = Guid.NewGuid(),
                        StartDate = model.StartDate.Value,
                        EndDate = model.EndDate.Value,
                        Name = model.Name,
                        CreatedAt = DateTime.UtcNow
                    });
                }
                catch (Exception e)
                {
                    //_logger.Log("Request failed.");
                    _logger.LogError(e, "Request failed. Request details: {@model}", model);
                }

                return RedirectToAction("Index", "Meeting");
            }
            else
            {
                return View(model);
            }
        }

        [HttpPost("createRecurrentMeeting")]
        public IActionResult CreateRecurrentMeeting([FromForm] CreateRecurrentMeetingModel model)
        {
            var validationResult = _createRecurrentMeetingValidator.Validate(model);
            if (validationResult.IsValid)
            {
                try
                {
                    _meetingService.Create(new RecurrencySetting
                    {
                        Id = Guid.NewGuid(),
                        StartDate = model.StartDate.Value,
                        EndDate = model.EndDate.Value,
                        Name = model.Name,
                        RepeatInterval = model.RepeatInterval,
                        RepeatUntil = model.RepeatUntil
                    });
                }
                catch (Exception e)
                {
                    //_logger.Log("Request failed.");
                    _logger.LogError(e, "Request failed. Request details: {@model}", model);
                }

                return RedirectToAction("Index", "Meeting");
            }
            else
            {
                return View(model);
            }
        }

        [HttpPost("editRecurrentMeeting")]
        public IActionResult EditRecurrentMeeting([FromForm] CreateRecurrentMeetingModel model)
        {
            var validationResult = _createRecurrentMeetingValidator.Validate(model);
            if (validationResult.IsValid)
            {
                try
                {
                    _meetingService.Edit(new RecurrencySetting
                    {
                        Id = model.Id,
                        StartDate = model.StartDate.Value,
                        EndDate = model.EndDate.Value,
                        Name = model.Name,
                        RepeatInterval = model.RepeatInterval,
                        RepeatUntil = model.RepeatUntil
                    });
                }
                catch (Exception e)
                {
                    //_logger.Log("Request failed.");
                    _logger.LogError(e, "Request failed. Request details: {@model}", model);
                }

                return RedirectToAction("Index", "Meeting");
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Details(Guid id)
        {
            var meeting = _meetingService.GetById(id);

            var meetingModel = new MeetingViewModel
            {
                Id = id,
                EndDate = meeting.EndDate,
                StartDate = meeting.StartDate,
                Name = meeting.Name
            };

            return View(meetingModel);
        }
    }
}