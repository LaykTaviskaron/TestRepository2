using ConsoleApp1;
using ConsoleApp1.Contracts;
using ConsoleApp1.Domain;
using Moq;

namespace TestProject
{
    public class AddMeetingControllerTests
    {
        [Fact]
        public void AddMeetingController_ExecuteAction_ShouldReturnMenuItemController()
        {
            // Arrange (setup)
            var sut = new AddMeetingController();


            // act
            var nextController = sut.ExecuteAction();


            // assert
            Assert.NotNull(nextController);
            Assert.IsType<MenuItemController>(nextController);
        }


        [Theory]
        [InlineData("2022/25/25")]
        [InlineData("123123123")]
        [InlineData("John Doe")]
        public void AddMeetingController_ExecuteActionWithInvalidDate_ShouldThrowArgumentException(string dateTime)
        {
            // Arrange (setup)
            var sut = new AddMeetingController();
            var consoleHelper = new Mock<ConsoleHelper>();
            consoleHelper.Setup(x => x.GetValueFromConsole()).Returns(dateTime);


            // assert
            var ex = Assert.Throws<ArgumentException>(() => sut.ExecuteAction(consoleHelper.Object));
            Assert.Equal("Error! Invalid Start date", ex.Message);
        }

        [Theory]
        [InlineData("2022/12/12")]
        [InlineData("2022/01/01")]
        [InlineData("2023/07/07")]
        public void AddMeetingController_ExecuteActionWithValidDate_ShouldReturnController(string dateTime) // duration, name
        {
            // Arrange (setup)
            var allMeetings = new List<Meeting>()
            {
                new Meeting
                {
                    Name = "Meeting1"
                },
                new Meeting
                {
                    Name = "Meeting2"
                },
            };

            var consoleHelper = new Mock<ConsoleHelper>();
            consoleHelper.Setup(x => x.GetValueFromConsole()).Returns(dateTime);


            var mockRepository = new Mock<IRepository>();
            mockRepository.Setup(x => x.GetAllMeetings()).Returns(allMeetings.ToArray());

            var sut = new AddMeetingController(mockRepository.Object);


            // act
            var nextController = sut.ExecuteAction(consoleHelper.Object);


            // assert
            Assert.NotNull(nextController);
            Assert.IsAssignableFrom<IController>(nextController);
        }
    }
}