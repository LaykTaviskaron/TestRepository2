using BusinessLogic;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Controllers.Tests
{
    public class InitHelpers
    {
        public static Mock<CalendarDbContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<CalendarDbContext>()
                .UseInMemoryDatabase(databaseName: "FakeConnectionString")
                .Options;

            var mockContext = new Mock<CalendarDbContext>(options);

            return mockContext;
        }
    }
}
