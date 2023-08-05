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

        public Guid Create(RecurrencySetting setting)
        {
            var existingRecurrencySettings = _calendar.RecurrencySettings.FirstOrDefault(x => x.Id == setting.Id);
            if (existingRecurrencySettings != null)
            {
                throw new ArgumentException("Such id already exists");
            }

            _calendar.RecurrencySettings.Add(setting);

            var meeting = new Meeting
            {
                Id = Guid.NewGuid(),
                Name = setting.Name,
                StartDate = setting.StartDate,
                EndDate = setting.EndDate,
                CreatedAt = DateTime.UtcNow,
                RecurrencySetting = setting,
                Room = setting.Room
            };

            _calendar.Meetings.Add(meeting);

            var calculatedDate = GetNextOccurence(setting.StartDate, setting.RepeatUntil, setting.RepeatInterval);
            if (setting.StartDate == calculatedDate)
            {
                var firstMeetingCreationResult = _calendar.SaveChanges();
                return meeting.Id;
            }

            var meetingDuration = setting.EndDate - setting.StartDate;
            var currentDate = setting.StartDate;

            while (calculatedDate >= currentDate)
            {
                var nextMeetingOccurence = new Meeting
                {
                    Id = Guid.NewGuid(),
                    Name = setting.Name,
                    StartDate = calculatedDate,
                    EndDate = calculatedDate.AddMinutes(meetingDuration.Minutes),
                    CreatedAt = DateTime.UtcNow,
                    RecurrencySetting = setting,
                    Room = setting.Room
                };

                _calendar.Meetings.Add(nextMeetingOccurence);

                currentDate = calculatedDate;
                calculatedDate = GetNextOccurence(currentDate, setting.RepeatUntil, setting.RepeatInterval);
            }

            var result = _calendar.SaveChanges();

            return meeting.Id;
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

        private DateTime GetNextOccurence(DateTime startDate, DateTime repeatUntil, RepeatInterval repeatInterval)
        {
            while (startDate <= repeatUntil)
            {
                switch (repeatInterval)
                {
                    case RepeatInterval.Daily:
                        return startDate.AddDays(1);
                    case RepeatInterval.Weekly:
                        return startDate.AddDays(7);
                    case RepeatInterval.Monthly:
                        return startDate.AddMonths(1);
                    case RepeatInterval.Undefined:
                    default:
                        break;
                }
            }

            return startDate;
        }
    }
}
