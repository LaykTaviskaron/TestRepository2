// See https://aka.ms/new-console-template for more information

// 2. Show all meeting
// 1. Add meeting - without validation
// 0. Exit calendar

using static System.Net.Mime.MediaTypeNames;

// meeting start time, duration, room, name

const string FileName = "meetings.csv";
const int MaximumRoomLenght = 25;
const int MaximumNameLenght = 50;

void ShowAll()
{
    Console.WriteLine($"{"Start time",20}" 
        + $"{"Duration",20}" 
        + $"{"Room",20}"+
        $"{"Name",20}");

    var fileContent = File.ReadAllLines(FileName);

    foreach (var line in fileContent)
    {
        var meetingContent = line.Split(",");

        Console.WriteLine($"{meetingContent[0],20}" +
              $"{meetingContent[1],20}" +
              $"{meetingContent[2],20}" +
              $"{meetingContent[3],20}");
    }

    //Console.WriteLine("Press any key to return to menu...");
    Console.ReadLine();
}

void RaiseError(string error)
{
    //Console.WriteLine(error);
    throw new Exception(error);
}

void AddMeeting() // meeting start time, duration, room, name
{
    Console.WriteLine("Start date:");
    var dateParsingResult = DateTime.TryParse(Console.ReadLine(), out var startTime);
    if (!dateParsingResult)
    {
        RaiseError("Error! Invalid Start date");
        return;
    }

    Console.WriteLine("Duration in minutes: ");
    var durationParsingResult = int.TryParse(Console.ReadLine(), out var duration);
    if (!durationParsingResult)
    {
        RaiseError("Error! Invalid meeting duration");
        return;
    }

    Console.WriteLine("Room: ");
    var room = Console.ReadLine();
    if (string.IsNullOrEmpty(room))
    {
        RaiseError("Error! Empty room");
        return;
    }

    if (room.Length > MaximumRoomLenght)
    {
        RaiseError($"Error! Room should not be longer than {MaximumRoomLenght} symbols");
        return;
    }
    
    Console.WriteLine("Name: ");
    var name = Console.ReadLine();
    if (string.IsNullOrEmpty(name))
    {
        RaiseError("Error! Empty name");
        return;
    }

    if (name.Length > MaximumNameLenght)
    {
        RaiseError($"Error! Room should not be longer than {MaximumNameLenght} symbols");
        return;
    }

    File.AppendAllText(FileName, $"{startTime},{duration},{room},{name}" + Environment.NewLine);
}

void Exit()
{
    Environment.Exit(0);
}

void Menu()
{
    Console.Clear();
    Console.WriteLine("2. Show all meetings");
    Console.WriteLine("1. Add meeting");
    Console.WriteLine("0. Exit calendar");
}

while (true)
{
    Menu();
    var keyInfo = Console.ReadKey();
    try
    {
        switch (keyInfo.Key)
        {
            case ConsoleKey.D0:
                Exit();
                break;
            case ConsoleKey.D1:
                AddMeeting();
                break;
            case ConsoleKey.D2:
                ShowAll();
                break;
            default: break;
        }
    }
    catch (Exception e)
    {
        try
        {
            // log message
        }
        catch 
        {
            // 
        }
        Console.WriteLine(e.Message);
        Console.ReadLine();
    }
    finally
    {

    }
}

//try
//{
//    var zero = 0;
//    var i = 10 / zero;
//}
//catch (DivideByZeroException e)
//{
//    Console.WriteLine($"Divide by zero found at: {e.StackTrace}");
//    throw new Exception("Divide by zero happened", e); // custom exception
//}
//catch (Exception e)
//{
//    Console.WriteLine($"Error happened: {e.Message} and Stack trace: {e.StackTrace}");
//    // add message log
//    throw new Exception("Error happened", e); // custom exception
//}
//finally
//{
//    Console.WriteLine("Try block is finished, with or without errors");
//}


//Console.WriteLine("Finished without errors");

Guid guid = Guid.NewGuid();

Console.WriteLine(guid);