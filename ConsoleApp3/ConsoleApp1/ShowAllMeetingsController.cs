using ConsoleApp1.Contracts;
using ConsoleApp1.Domain;

namespace ConsoleApp1
{
    public class ShowAllMeetingsController : IController
    {
        private IRepository repository;

        public ShowAllMeetingsController()
        {
            repository = Factory.GetRepository();
        }

        public IController ExecuteAction(ConsoleHelper helper = null)
        {
            var meetings = repository.GetAllMeetings();
            ShowAll(meetings);

            return new MenuItemController();
        }

        private void ShowAll(Meeting[] meetings)
        {
            Console.WriteLine($"{"Start time",20}"
                + $"{"Duration",20}"
                + $"{"Room",20}" +
                $"{"Name",20}");

            foreach (var meeting in meetings)
            {
                Console.WriteLine($"{meeting.StartDate,20}" +
                      $"{meeting.Duration,20}" +
                      $"{meeting.Room?.Name,20}" +
                      $"{meeting.Name,20}");
            }

            Console.WriteLine("Press any key to return to menu...");
            Console.ReadLine();
        }
    }
}
