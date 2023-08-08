using BusinessLogic.Services;
using Models;

namespace BusinessLogic.Tests
{
    public class MeetingServiceTests
    {
        [Fact]
        public void GetAll_WhenResultIsNotEmpty()
        {
            // Arrange
            var meetings = new List<Meeting>
            {
                new Meeting()
                {
                    Id = Guid.NewGuid(),
                    EndDate = DateTime.Now,
                    Name = "First",
                    StartDate = DateTime.Now,
                },
                new Meeting()
                {
                    Id = Guid.NewGuid(),
                    EndDate = DateTime.Now,
                    Name = "Second",
                    StartDate = DateTime.Now,
                }
            };

            var mockSet = InitHelpers.GetQueryableMockDbSet(meetings);
            var mockContext = InitHelpers.GetDbContext();
            mockContext.Setup(m => m.Meetings).Returns(mockSet);
            
            var service = new MeetingsService(mockContext.Object);

            // Act
            var result = service.GetAll();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(meetings.Count(), result.Count());
        }

        [Fact]
        public void GetAll_WhenResultIsEmpty()
        {
            // Arrange
            var mockSet = InitHelpers.GetQueryableMockDbSet(new List<Meeting>());
            var mockContext = InitHelpers.GetDbContext();
            mockContext.Setup(m => m.Meetings).Returns(mockSet);

            var service = new MeetingsService(mockContext.Object);

            // Act
            var result = service.GetAll();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.Count());
        }

        [Fact]
        public void Create_WhenModelIsValid()
        {
            // Arrange
            var mockSet = InitHelpers.GetQueryableMockDbSet(new List<Meeting>());
            var mockContext = InitHelpers.GetDbContext();
            mockContext.Setup(m => m.Meetings).Returns(mockSet);

            var service = new MeetingsService(mockContext.Object);

            // Act
            var result = service.Create(new Meeting
            {
                Id = Guid.NewGuid(),
                EndDate = DateTime.Now,
                StartDate = DateTime.Now,
                Name = "First"
            });

            //Assert
            Assert.NotEqual(Guid.Empty, result);
            Assert.Equal(1, mockContext.Object.Meetings.Count());
        }

        [Fact]
        public void Edit_WhenModelIsValid_ReturnsUpdatedMeeting()
        {
            var meetingId = Guid.NewGuid();

            var meetings = new List<Meeting>
            {
                new Meeting()
                {
                    Id = meetingId,
                    EndDate = DateTime.Now,
                    Name = "First",
                    StartDate = DateTime.Now,
                },
                new Meeting()
                {
                    Id = Guid.NewGuid(),
                    EndDate = DateTime.Now,
                    Name = "Second",
                    StartDate = DateTime.Now,
                }
            };

            var mockSet = InitHelpers.GetQueryableMockDbSet(meetings);
            var mockContext = InitHelpers.GetDbContext();
            mockContext.Setup(m => m.Meetings).Returns(mockSet);

            var service = new MeetingsService(mockContext.Object);

            // Act
            var updatedMeeting = new Meeting
            {
                Id = meetingId,
                EndDate = DateTime.Now.AddDays(1),
                StartDate = DateTime.Now.AddDays(1),
                Name = "First meeting updated"
            };
            
            var result = service.Edit(updatedMeeting);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.Id, updatedMeeting.Id);
            Assert.Equal(result.Name, updatedMeeting.Name);
            Assert.Equal(result.StartDate, updatedMeeting.StartDate);
            Assert.Equal(result.EndDate, updatedMeeting.EndDate);
            Assert.Equal(result.Room, updatedMeeting.Room);

            Assert.Equal(2, mockContext.Object.Meetings.Count());
        }

        [Fact]
        public void Create_WhenModelIsValidAndListIsNotEmpty()
        {
            // Arrange
            var meetings = new List<Meeting>
            {
                new Meeting()
                {
                    Id = Guid.NewGuid(),
                    EndDate = DateTime.Now,
                    Name = "First",
                    StartDate = DateTime.Now,
                },
                new Meeting()
                {
                    Id = Guid.NewGuid(),
                    EndDate = DateTime.Now,
                    Name = "Second",
                    StartDate = DateTime.Now,
                }
            };

            var mockSet = InitHelpers.GetQueryableMockDbSet(meetings);
            var mockContext = InitHelpers.GetDbContext();
            mockContext.Setup(m => m.Meetings).Returns(mockSet);

            var service = new MeetingsService(mockContext.Object);

            // Act
            var result = service.Create(new Meeting
            {
                Id = Guid.NewGuid(),
                EndDate = DateTime.Now,
                StartDate = DateTime.Now,
                Name = "First"
            });

            //Assert
            Assert.NotEqual(Guid.Empty, result);
            Assert.Equal(3, mockContext.Object.Meetings.Count());
        }

        [Fact]
        public void Edit_WhenModelIsValidAndListIsNotEmpty_CreatesMultipleMeetings()
        {
            // Arrange
            var startDate = DateTime.Now;
            var recurrencySetting = new RecurrencySetting 
            { 
                Id = Guid.NewGuid(),
                StartDate = startDate,
                EndDate = startDate.AddMinutes(30),
                Name = "Meeting series",
                RepeatInterval = RepeatInterval.Daily,
                RepeatUntil = startDate.AddDays(2)
            };

            var settings = new List<RecurrencySetting> { recurrencySetting };

            var meetings = new List<Meeting>
            {
                new Meeting()
                {
                    Id = Guid.NewGuid(),
                    Name = "Meeting series",
                    EndDate = startDate.AddDays(1).AddMinutes(30),
                    StartDate = startDate.AddDays(1),
                    RecurrencySetting = recurrencySetting
                },
                new Meeting()
                {
                    Id = Guid.NewGuid(),
                    Name = "Meeting series",
                    EndDate = startDate.AddDays(2).AddMinutes(30),
                    StartDate = startDate.AddDays(2),
                    RecurrencySetting = recurrencySetting
                }
            };

            var meetingsMockSet = InitHelpers.GetQueryableMockDbSet(meetings);
            var settingsMockSet = InitHelpers.GetQueryableMockDbSet(settings);
            var mockContext = InitHelpers.GetDbContext();
            mockContext.Setup(m => m.Meetings).Returns(meetingsMockSet);
            mockContext.Setup(m => m.RecurrencySettings).Returns(settingsMockSet);

            var service = new MeetingsService(mockContext.Object);

            // Act
            var result = service.Edit(new RecurrencySetting
            {
                Id = recurrencySetting.Id,
                EndDate = startDate.AddDays(-2).AddMinutes(30),
                StartDate = startDate.AddDays(-2),
                Name = "Updated meeting series",
                RepeatInterval = RepeatInterval.Daily,
                RepeatUntil = startDate
            });

            //Assert
            //Assert.NotEqual(Guid.Empty, result);
            Assert.Single(mockContext.Object.RecurrencySettings);
            var recurrencySettingsFromDB = mockContext.Object.RecurrencySettings.First();

            Assert.Equal(recurrencySetting.Id, recurrencySettingsFromDB.Id);
            Assert.Equal(recurrencySetting.Name, recurrencySettingsFromDB.Name);
            Assert.Equal(recurrencySetting.StartDate, recurrencySettingsFromDB.StartDate);
            Assert.Equal(recurrencySetting.EndDate, recurrencySettingsFromDB.EndDate);
            Assert.Equal(recurrencySetting.RepeatInterval, recurrencySettingsFromDB.RepeatInterval);
            Assert.Equal(recurrencySetting.RepeatUntil, recurrencySettingsFromDB.RepeatUntil);
            Assert.Equal(recurrencySetting.Room, recurrencySettingsFromDB.Room);

            Assert.Equal(3, mockContext.Object.Meetings.Count());
            foreach (var meeting in mockContext.Object.Meetings)
            {
                Assert.Equal(meeting.Name, recurrencySettingsFromDB.Name);
                Assert.Equal(meeting.Room, recurrencySettingsFromDB.Room);
                Assert.Equal(meeting.RecurrencySetting, recurrencySettingsFromDB);
            }
        }

        [Fact]
        public void Edit_WhenModelIsValidAndListIsNotEmpty_CreatesOnlyOneMeeting()
        {
            // Arrange
            var startDate = DateTime.Now;
            var recurrencySetting = new RecurrencySetting
            {
                Id = Guid.NewGuid(),
                StartDate = startDate,
                EndDate = startDate.AddMinutes(30),
                Name = "Meeting series",
                RepeatInterval = RepeatInterval.Daily,
                RepeatUntil = startDate.AddDays(2)
            };

            var settings = new List<RecurrencySetting> { recurrencySetting };

            var meetings = new List<Meeting>
            {
                new Meeting()
                {
                    Id = Guid.NewGuid(),
                    Name = "Meeting series",
                    EndDate = startDate.AddDays(1).AddMinutes(30),
                    StartDate = startDate.AddDays(1),
                    RecurrencySetting = recurrencySetting
                },
                new Meeting()
                {
                    Id = Guid.NewGuid(),
                    Name = "Meeting series",
                    EndDate = startDate.AddDays(2).AddMinutes(30),
                    StartDate = startDate.AddDays(2),
                    RecurrencySetting = recurrencySetting
                }
            };

            var meetingsMockSet = InitHelpers.GetQueryableMockDbSet(meetings);
            var settingsMockSet = InitHelpers.GetQueryableMockDbSet(settings);
            var mockContext = InitHelpers.GetDbContext();
            mockContext.Setup(m => m.Meetings).Returns(meetingsMockSet);
            mockContext.Setup(m => m.RecurrencySettings).Returns(settingsMockSet);

            var service = new MeetingsService(mockContext.Object);

            // Act
            var result = service.Edit(new RecurrencySetting
            {
                Id = recurrencySetting.Id,
                EndDate = startDate.AddMinutes(30),
                StartDate = startDate,
                Name = "Updated meeting series",
                RepeatInterval = RepeatInterval.Daily,
                RepeatUntil = startDate
            });

            //Assert
            //Assert.NotEqual(Guid.Empty, result);
            Assert.Single(mockContext.Object.RecurrencySettings);
            var recurrencySettingsFromDB = mockContext.Object.RecurrencySettings.First();

            Assert.Equal(recurrencySetting.Id, recurrencySettingsFromDB.Id);
            Assert.Equal(recurrencySetting.Name, recurrencySettingsFromDB.Name);
            Assert.Equal(recurrencySetting.StartDate, recurrencySettingsFromDB.StartDate);
            Assert.Equal(recurrencySetting.EndDate, recurrencySettingsFromDB.EndDate);
            Assert.Equal(recurrencySetting.RepeatInterval, recurrencySettingsFromDB.RepeatInterval);
            Assert.Equal(recurrencySetting.RepeatUntil, recurrencySettingsFromDB.RepeatUntil);
            Assert.Equal(recurrencySetting.Room, recurrencySettingsFromDB.Room);

            Assert.Equal(1, mockContext.Object.Meetings.Count());
            foreach (var meeting in mockContext.Object.Meetings)
            {
                Assert.Equal(meeting.Name, recurrencySettingsFromDB.Name);
                Assert.Equal(meeting.Room, recurrencySettingsFromDB.Room);
                Assert.Equal(meeting.RecurrencySetting, recurrencySettingsFromDB);
            }
        }

        [Fact]
        public void Edit_WhenSettingIsNotFound_ThrowsException()
        {
            // Arrange
            var startDate = DateTime.Now;
            var recurrencySetting = new RecurrencySetting
            {
                Id = Guid.NewGuid(),
                StartDate = startDate,
                EndDate = startDate.AddMinutes(30),
                Name = "Meeting series",
                RepeatInterval = RepeatInterval.Daily,
                RepeatUntil = startDate.AddDays(2)
            };

            var settings = new List<RecurrencySetting> { recurrencySetting };

            var meetings = new List<Meeting>
            {
                new Meeting()
                {
                    Id = Guid.NewGuid(),
                    Name = "Meeting series",
                    EndDate = startDate.AddDays(1).AddMinutes(30),
                    StartDate = startDate.AddDays(1),
                    RecurrencySetting = recurrencySetting
                },
                new Meeting()
                {
                    Id = Guid.NewGuid(),
                    Name = "Meeting series",
                    EndDate = startDate.AddDays(2).AddMinutes(30),
                    StartDate = startDate.AddDays(2),
                    RecurrencySetting = recurrencySetting
                }
            };

            var meetingsMockSet = InitHelpers.GetQueryableMockDbSet(meetings);
            var settingsMockSet = InitHelpers.GetQueryableMockDbSet(settings);
            var mockContext = InitHelpers.GetDbContext();
            mockContext.Setup(m => m.Meetings).Returns(meetingsMockSet);
            mockContext.Setup(m => m.RecurrencySettings).Returns(settingsMockSet);

            var service = new MeetingsService(mockContext.Object);

            // Act
            Assert.Throws<ArgumentException>(() => service.Edit(new RecurrencySetting
            {
                Id = Guid.NewGuid() // invalid id
            }));            
        }

        [Fact]
        public void Edit_WhenMeetingIsNotFound_ThrowsException()
        {
            // Arrange
            var startDate = DateTime.Now;

            var meetings = new List<Meeting>
            {
                new Meeting()
                {
                    Id = Guid.NewGuid(),
                    Name = "Meeting series",
                    EndDate = startDate.AddDays(1).AddMinutes(30),
                    StartDate = startDate.AddDays(1),
                },
                new Meeting()
                {
                    Id = Guid.NewGuid(),
                    Name = "Meeting series",
                    EndDate = startDate.AddDays(2).AddMinutes(30),
                    StartDate = startDate.AddDays(2),
                }
            };

            var meetingsMockSet = InitHelpers.GetQueryableMockDbSet(meetings);
            var mockContext = InitHelpers.GetDbContext();
            mockContext.Setup(m => m.Meetings).Returns(meetingsMockSet);

            var service = new MeetingsService(mockContext.Object);

            // Act
            Assert.Throws<ArgumentException>(() => service.Edit(new Meeting
            {
                Id = Guid.NewGuid() // invalid id
            }));
        }

        [Fact]
        public void Delete_WhenMeetingIsNotFound_ThrowsException()
        {
            // Arrange
            var startDate = DateTime.Now;

            var meetings = new List<Meeting>
            {
                new Meeting()
                {
                    Id = Guid.NewGuid(),
                    Name = "Meeting series",
                    EndDate = startDate.AddDays(1).AddMinutes(30),
                    StartDate = startDate.AddDays(1),
                },
                new Meeting()
                {
                    Id = Guid.NewGuid(),
                    Name = "Meeting series",
                    EndDate = startDate.AddDays(2).AddMinutes(30),
                    StartDate = startDate.AddDays(2),
                }
            };

            var meetingsMockSet = InitHelpers.GetQueryableMockDbSet(meetings);
            var mockContext = InitHelpers.GetDbContext();
            mockContext.Setup(m => m.Meetings).Returns(meetingsMockSet);

            var service = new MeetingsService(mockContext.Object);

            // Act
            Assert.Throws<ArgumentException>(() => service.DeleteById(Guid.NewGuid())); // invalid id
        }

        [Fact]
        public void Delete_WhenModelIsValid_RemovesMeeting()
        {
            var meetingId = Guid.NewGuid();

            var meetings = new List<Meeting>
            {
                new Meeting()
                {
                    Id = meetingId,
                    EndDate = DateTime.Now,
                    Name = "First",
                    StartDate = DateTime.Now,
                },
                new Meeting()
                {
                    Id = Guid.NewGuid(),
                    EndDate = DateTime.Now,
                    Name = "Second",
                    StartDate = DateTime.Now,
                }
            };

            var mockSet = InitHelpers.GetQueryableMockDbSet(meetings);
            var mockContext = InitHelpers.GetDbContext();
            mockContext.Setup(m => m.Meetings).Returns(mockSet);

            var service = new MeetingsService(mockContext.Object);

            // Act
            service.DeleteById(meetingId);

            //Assert
            var existingMeetings = mockContext.Object.Meetings.ToList();
            Assert.NotEmpty(existingMeetings);
            Assert.Equal(1, existingMeetings.Count());
            
            Assert.NotEqual(meetingId, existingMeetings.First().Id);
        }


        [Theory, MemberData(nameof(StartAndEndDate), MemberType = typeof(MeetingServiceTests))]
        public void GetByDateRange_ReturnsDataWhenBothParametersPassed(DateTime? startDate, DateTime? endDate)
        {
            // Arrange

            var outOfRangeDate = DateTime.Now.AddDays(1);

            var meetings = new List<Meeting>
            {
                new Meeting()
                {
                    Id = Guid.NewGuid(),
                    EndDate = endDate.HasValue ? endDate.Value : DateTime.Now,
                    Name = "First",
                    StartDate = startDate.HasValue ? startDate.Value : DateTime.Now,
                },
                new Meeting()
                {
                    Id = Guid.NewGuid(),
                    EndDate = outOfRangeDate,
                    Name = "Second",
                    StartDate = outOfRangeDate,
                }
            };
            var mockSet = InitHelpers.GetQueryableMockDbSet(meetings);
            var mockContext = InitHelpers.GetDbContext();
            mockContext.Setup(m => m.Meetings).Returns(mockSet);

            var service = new MeetingsService(mockContext.Object);

            // Act
            var result = service.GetByDateRange(startDate, endDate);

            //Assert
            if (endDate == null)
            {
                Assert.Equal(meetings.Count(), result.Count());
            }
            else
            {
                Assert.Single(result);
            }

            Assert.Equal(meetings.First().Id, result.First().Id);
        }

        public static readonly object[][] StartAndEndDate =
        {
            new object[] { new DateTime(2017,3,1), new DateTime(2018,12,31) },
            new object[] { new DateTime(2018,3,1), new DateTime(2019,12,31) },
            new object[] { new DateTime(2017,3,1), null },
            new object[] { null, new DateTime(2019,12,31) },
        };
    }
}