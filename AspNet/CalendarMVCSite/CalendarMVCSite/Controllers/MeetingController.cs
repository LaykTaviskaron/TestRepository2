using BusinessLogic;
using BusinessLogic.Interfaces;
using CalendarMVCSite.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        private readonly IRoomsService _roomService;
        private readonly Serilog.ILogger _serilogLogger;
        private readonly IValidator<CreateMeetingModel> _createMeetingValidator;
        private readonly IValidator<EditMeetingModel> _editMeetingValidator;
        private readonly IValidator<CreateRecurrentMeetingModel> _createRecurrentMeetingValidator;

        public MeetingController(
            ILogger<MeetingController> logger, 
            CalendarDbContext calendar,
            IMeetingsService meetingService,
            IRoomsService roomService,
            IValidator<CreateMeetingModel> validator,
            IValidator<CreateRecurrentMeetingModel> createRecurrentMeetingValidator,
            IValidator<EditMeetingModel> editMeetingValidator)
        {
            _logger = logger;
            _meetingService = meetingService;
            _roomService = roomService;
            //_serilogLogger = serilogLogger;
            _createMeetingValidator = validator;
            _editMeetingValidator = editMeetingValidator;
            _createRecurrentMeetingValidator = createRecurrentMeetingValidator;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            var model = new IndexViewModel();

            var meetings = _meetingService.GetAll().OrderBy(x => x.StartDate);

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
                Name = meeting.Name,
                IsOnlineMeeting = meeting.IsOnlineMeeting,
                RoomId = meeting.Room?.Id.ToString()
            };

            PopulateRoomsInViewBag();

            return View(meetingModel);
        }

        private Room GetRoomById(string roomId)
        {
            if (Guid.TryParse(roomId, out var parsedRoomId))
            {
                return _roomService.GetById(parsedRoomId);
            }

            return null;
        }

        [HttpPost("edit/{id}")]
        public IActionResult Edit(Guid id, [FromForm] EditMeetingModel model)
        {
            var validationResult = _editMeetingValidator.Validate(model);
            if (validationResult.IsValid)
            {
                try
                {
                    var room = GetRoomById(model.RoomId);

                    _meetingService.Edit(new Meeting
                    {
                        Id = model.Id,
                        IsOnlineMeeting = model.IsOnlineMeeting,
                        StartDate = model.StartDate.Value,
                        EndDate = model.EndDate.Value,
                        Name = model.Name,
                        Room = room
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
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, $"{error.PropertyName}: {error.ErrorMessage}");
                }

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
            PopulateRoomsInViewBag();

            return View();
        }

        [HttpGet("createRecurrentMeeting")]
        public IActionResult CreateRecurrentMeeting()
        {
            PopulateRoomsInViewBag();

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
                    var room = GetRoomById(model.RoomId);

                    _meetingService.Create(new Meeting
                    {
                        Id = Guid.NewGuid(),
                        StartDate = model.StartDate.Value,
                        EndDate = model.EndDate.Value,
                        IsOnlineMeeting = model.IsOnlineMeeting,
                        Name = model.Name,
                        CreatedAt = DateTime.UtcNow,
                        Room = room
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
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, $"{error.PropertyName}: {error.ErrorMessage}");
                }

                PopulateRoomsInViewBag();

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
                    var room = GetRoomById(model.RoomId);

                    _meetingService.Create(new RecurrencySetting
                    {
                        Id = Guid.NewGuid(),
                        StartDate = model.StartDate.Value,
                        EndDate = model.EndDate.Value,
                        Name = model.Name,
                        IsOnlineMeeting = model.IsOnlineMeeting,
                        RepeatInterval = model.RepeatInterval,
                        RepeatUntil = model.RepeatUntil,
                        Room = room
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
                    var room = GetRoomById(model.RoomId);

                    _meetingService.Edit(new RecurrencySetting
                    {
                        Id = model.Id,
                        StartDate = model.StartDate.Value,
                        EndDate = model.EndDate.Value,
                        Name = model.Name,
                        RepeatInterval = model.RepeatInterval,
                        RepeatUntil = model.RepeatUntil,
                        IsOnlineMeeting = model.IsOnlineMeeting,
                        Room = room
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

        private void PopulateRoomsInViewBag()
        {
            var allRooms = _roomService.GetAll();
            var optionsList = allRooms
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();

            ViewBag.Rooms = (IEnumerable<SelectListItem>)optionsList;
        }
    }
}