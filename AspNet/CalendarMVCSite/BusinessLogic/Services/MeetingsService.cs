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

        public Meeting GetById(Guid id)
        {
            var result = _calendar.Meetings.FirstOrDefault(x => x.Id == id);
            if (result == null)
            {
                throw new ArgumentException("No such id exists");
            }

            return result;
        }

        public void DeleteById(Guid id)
        {
            var existingItem = _calendar.Meetings.FirstOrDefault(x => x.Id == id);
            if (existingItem == null)
            {
                throw new ArgumentException("No such id exists");
            }

            _calendar.Meetings.Remove(existingItem);
            _calendar.SaveChanges();
        }

        public Meeting Edit(Meeting meeting)
        {
            var existingMeeting = _calendar.Meetings.FirstOrDefault(x => x.Id == meeting.Id);
            if (existingMeeting == null)
            {
                throw new ArgumentException("No such id exists");
            }

            existingMeeting.Name = meeting.Name;
            existingMeeting.StartDate = meeting.StartDate;
            existingMeeting.EndDate = meeting.EndDate;

            _calendar.SaveChanges();

            return existingMeeting;
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
