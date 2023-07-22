using BusinessLogic.Services;
using Models;

namespace BusinessLogic.Tests
{
    public class MeetingServiceTests
    {
        [Fact]
        public void MeetingService_GetAll_WhenResultIsNotEmpty()
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
        public void MeetingService_GetAll_WhenResultIsEmpty()
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
        public void MeetingService_Create_WhenModelIsValid()
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
        public void MeetingService_Create_WhenModelIsValidAndListIsNotEmpty()
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

        [Theory, MemberData(nameof(StartAndEndDate), MemberType = typeof(MeetingServiceTests))]
        public void MeetingService_GetByDateRange_ReturnsDataWhenBothParametersPassed(DateTime? startDate, DateTime? endDate)
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
                Assert.Equal(1, result.Count());
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