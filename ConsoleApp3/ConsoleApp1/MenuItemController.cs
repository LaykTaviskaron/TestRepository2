using System;

namespace ConsoleApp1
{
    public class MenuItemController : IController
    {
        public IController ExecuteAction(ConsoleHelper helper = null)
        {
            Menu();

            var keyInfo = Console.ReadKey();
            try
            {
                switch (keyInfo.Key)
                {
                    case ConsoleKey.D0:
                        return null;
                    case ConsoleKey.D1:
                        return new AddMeetingController();
                    case ConsoleKey.D2:
                        return new ShowAllMeetingsController();
                    default:
                        return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        private void Menu()
        {
            Console.Clear();
            Console.WriteLine("2. Show all meetings");
            Console.WriteLine("1. Add meeting");
            Console.WriteLine("0. Exit calendar");
        }
    }
}
