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
        private readonly IValidator<CreateMeetingModel> _validator;
        private readonly IValidator<EditMeetingModel> _editMeetingValidator;

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
            _validator = validator;
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

                return RedirectToAction("Index", "Meeting");
            }
            else
            {
                return View(model);
            }
        }
    }
}