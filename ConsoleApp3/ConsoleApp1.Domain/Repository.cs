using ConsoleApp1.Contracts;
using System.Xml.Linq;

namespace ConsoleApp1.Domain
{
    internal class Repository : IRepository
    {
        const string FileName = "meetings.csv";

        public Meeting[] GetAllMeetings()
        {
            var fileContent = File.ReadAllLines(FileName);

            List<Meeting> meetings = new List<Meeting>();
            foreach (var line in fileContent)
            {
                var meetingContent = line.Split(",");

                meetings.Add(new Meeting
                {
                    StartDate = DateTime.TryParse(meetingContent[0], out var date) ? date : DateTime.MinValue,
                    Duration = int.TryParse(meetingContent[1], out var duration) ? duration : 0,
                    Room = new Room { Name = meetingContent[2] },
                    Name = meetingContent[3],
                });
            }

            return meetings.ToArray();
        }

        public void AddMeeting(Meeting meeting)
        {
            File.AppendAllText(FileName, $"{meeting.StartDate},{meeting.Duration},{meeting.Room?.Name},{meeting.Name}" + Environment.NewLine);
        }
    }
}
