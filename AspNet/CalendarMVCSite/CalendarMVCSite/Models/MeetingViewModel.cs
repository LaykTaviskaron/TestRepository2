using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace CalendarMVCSite.Models
{
    public class MeetingViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
