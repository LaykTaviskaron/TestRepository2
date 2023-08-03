using Models;

namespace CalendarMVCSite.Models
{
    public class DailyViewModel
    {
        public IEnumerable<Meeting> Meetings { get; set; }
    }
}