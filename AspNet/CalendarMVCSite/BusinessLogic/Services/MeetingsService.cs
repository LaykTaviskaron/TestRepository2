using BusinessLogic.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;

namespace BusinessLogic.Services
{
    public class MeetingsService : IMeetingsService
    {
        private readonly CalendarDbContext _calendar;

        public MeetingsService(CalendarDbContext calendar) 
        {
            _calendar = calendar;
        }

        public IEnumerable<Meeting> GetAll()
        {
            return _calendar.Meetings.ToList();
        }

        public Guid Create(Meeting meeting) 
        { 
            _calendar.Meetings.Add(meeting);

            var result = _calendar.SaveChanges();

            return meeting.Id;
        }

        public IEnumerable<Meeting> GetByDateRange(DateTime? startDate, DateTime? endDate)
        {
            var baseQuery = _calendar.Meetings.AsNoTracking();

            if (startDate != null) 
            {
                baseQuery = baseQuery.Where(x => x.StartDate >= startDate);
            }

            if (endDate != null)
            {
                baseQuery = baseQuery.Where(x => x.EndDate <= endDate);
            }

            return baseQuery.ToList();
        }
    }
}
