using Models;

namespace CalendarMVCSite.Models
{
    public class IndexViewModel
    {
        public string? RequestId { get; set; }

        public IEnumerable<Meeting> Meetings { get; set; }
    }
}