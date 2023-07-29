using BusinessLogic;
using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using CalendarMVCSite.Controllers;
using CalendarMVCSite.Models;
using FluentValidation;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Controllers.Tests
{
    public class MeetingControllerTests
    {
        [Theory]
        [InlineData("Meeting title")]
        [InlineData("Meeting title1")]
        [InlineData("Meeting title2")]
        [InlineData("Meeting title3")]
        [InlineData("Meeting title4")]
        [InlineData("Meeting title5")]
        public void MeetingController_Create_WithValidName_ReturnsNoError(string meetingTitle)
        {
            // Arrange
            var logger = new Mock<Microsoft.Extensions.Logging.ILogger<MeetingController>>();
            var dbContext = InitHelpers.GetDbContext();
            var meetingService = new Mock<IMeetingsService>();
            var validator = new CreateMeetingModelValidator();

            var service = new MeetingController(logger.Object, dbContext.Object, meetingService.Object, validator);

            // Act
            var result = service.Create(new CreateMeetingModel 
            { 
                Name = meetingTitle,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now
            });

            //Assert
            Assert.NotNull(result);
            //Assert.Equal(meetings.Count(), result.Count());

        }

        [Theory]
        [InlineData("1")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("123456789012345678901234567890 invalid")]
        public void MeetingController_Create_WithInvalidName_ReturnsError(string meetingTitle)
        {
            // Arrange
            var model = new CreateMeetingModel
            {
                Name = meetingTitle,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now
            };

            var logger = new Mock<Microsoft.Extensions.Logging.ILogger<MeetingController>>();
            var dbContext = InitHelpers.GetDbContext();
            var meetingService = new Mock<IMeetingsService>();
            //var validatorMock = new Mock<CreateMeetingModelValidator>();
            //validatorMock.Setup(c => c.Validate(model)).Verifiable();

            var validator = new CreateMeetingModelValidator();

            var service = new MeetingController(logger.Object, dbContext.Object, meetingService.Object, validator);
            // Act
            var result = service.Create(model);

            //Assert
            Assert.NotNull(result);

            var validationResult = validator.Validate(model);

            Assert.NotNull(validationResult);
            Assert.False(validationResult.IsValid);

            //validatorMock.Verify(d => d.Validate(model), Times.Once());
        }
    }
}