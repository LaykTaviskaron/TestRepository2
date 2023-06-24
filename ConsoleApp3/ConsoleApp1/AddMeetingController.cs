using ConsoleApp1.Contracts;
using ConsoleApp1.Domain;
using System.Xml.Linq;

namespace ConsoleApp1
{
    public class AddMeetingController : IController
    {
        const int MaximumRoomLenght = 25;
        const int MaximumNameLenght = 50;

        private IRepository _repository;
        public AddMeetingController(IRepository repository = null)
        {
            if (repository != null)
            {
                _repository = repository;
            }
            else
            {
                _repository = Factory.GetRepository();
            }
        }

        public IController ExecuteAction(ConsoleHelper helper = null)
        {
            var nextController = new MenuItemController();

            Console.WriteLine("Start date:");
            var dateParsingResult = DateTime.TryParse(helper.GetValueFromConsole(), out var startTime);
            if (!dateParsingResult)
            {
                RaiseError("Error! Invalid Start date");
                return nextController;
            }

            //Console.WriteLine("Duration in minutes: ");
            //var durationParsingResult = int.TryParse(GetValueFromConsole(), out var parsedDuration);
            //if (!durationParsingResult)
            //{
            //    RaiseError("Error! Invalid meeting duration");
            //    return nextController;
            //}

            //Console.WriteLine("Room: ");
            //var parsedRoom = GetValueFromConsole();
            //if (string.IsNullOrEmpty(parsedRoom))
            //{
            //    RaiseError("Error! Empty room");
            //    return nextController;
            //}

            //if (parsedRoom.Length > MaximumRoomLenght)
            //{
            //    RaiseError($"Error! Room should not be longer than {MaximumRoomLenght} symbols");
            //    return nextController;
            //}

            //Console.WriteLine("Name: ");
            //var parsedName = GetValueFromConsole();
            //if (string.IsNullOrEmpty(parsedName))
            //{
            //    RaiseError("Error! Empty name");
            //    return nextController;
            //}

            //if (parsedName.Length > MaximumNameLenght)
            //{
            //    RaiseError($"Error! Room should not be longer than {MaximumNameLenght} symbols");
            //    return nextController;
            //}

            var meeting = new Meeting
            {
                StartDate = startTime,
                Duration = 50,
                Name = "Meeting",
                Room = new Room { Name = "Room" }
            };

            _repository.AddMeeting(meeting);

            // check all meeting
            var allMeetings = _repository.GetAllMeetings();


            return nextController;
        }

        void RaiseError(string error)
        {
            //Console.WriteLine(error);
            throw new ArgumentException(error);
        }
    }
}
