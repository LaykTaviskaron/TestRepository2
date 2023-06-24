using ConsoleApp1.Contracts;

namespace ConsoleApp1.Domain
{
    public interface IRepository
    {
        public Meeting[] GetAllMeetings();
        public void AddMeeting(Meeting meeting);
    }
}
