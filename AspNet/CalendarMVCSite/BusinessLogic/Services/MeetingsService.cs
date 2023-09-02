using BusinessLogic.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;
using NuGet.Configuration;
using System.Collections.ObjectModel;

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
            var meetingsWithRooms = _calendar.Meetings.Include(x => x.Room);

            return meetingsWithRooms;
        }

        public Meeting GetById(Guid id)
        {
            var result = _calendar.Meetings
                .Include(x => x.Room)
                .FirstOrDefault(x => x.Id == id);

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
            existingMeeting.Room = meeting.Room;
            existingMeeting.IsOnlineMeeting = meeting.IsOnlineMeeting;

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

            var firstMeetingId = CreateMeetings(setting);

            var result = _calendar.SaveChanges();

            return firstMeetingId;
        }

        public RecurrencySetting Edit(RecurrencySetting setting)
        {
            var existingRecurrencySettings = _calendar.RecurrencySettings.FirstOrDefault(x => x.Id == setting.Id);
            if (existingRecurrencySettings == null)
            {
                throw new ArgumentException("Such id is not found");
            }

            existingRecurrencySettings.StartDate = setting.StartDate;
            existingRecurrencySettings.EndDate = setting.EndDate;
            existingRecurrencySettings.Name = setting.Name;
            existingRecurrencySettings.RepeatInterval = setting.RepeatInterval;
            existingRecurrencySettings.RepeatUntil = setting.RepeatUntil;
            existingRecurrencySettings.Room = setting.Room;
            existingRecurrencySettings.IsOnlineMeeting = setting.IsOnlineMeeting;

            var existingMeetingSeries = _calendar.Meetings.Where(x => x.RecurrencySetting.Id == setting.Id).ToList();
            foreach (var item in existingMeetingSeries)
            {
                _calendar.Meetings.Remove(item);
            }

            var firstMeetingId = CreateMeetings(existingRecurrencySettings);

            var result = _calendar.SaveChanges();

            return existingRecurrencySettings;
        }

        private void ThrowIfConflictedMeetings(Meeting meeting)
        {
            if (!meeting.IsOnlineMeeting && meeting.Room != null)
            {
                var conflictedMeetings = _calendar.Meetings
                    .Include(x => x.Room)
                    .Where(x => x.Room != null && x.Room.Id == meeting.Room.Id && !x.IsOnlineMeeting)
                    .Where(x => x.StartDate <= meeting.StartDate && x.EndDate >= meeting.StartDate
                        || x.StartDate <= meeting.EndDate && x.EndDate >= meeting.EndDate
                        || meeting.StartDate <= x.StartDate && meeting.EndDate >= x.StartDate
                        || meeting.StartDate <= x.EndDate && meeting.EndDate >= x.EndDate);

                if (conflictedMeetings.Any())
                {
                    throw new ArgumentException($"Meeting conflic found for room {meeting.Room?.Name}");
                }
            }
        }

        public Guid Create(Meeting meeting) 
        {
            ThrowIfConflictedMeetings(meeting);

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
            while (startDate.Date <= repeatUntil.Date)
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

        private Guid CreateMeetings(RecurrencySetting setting)
        {
            var meeting = new Meeting
            {
                Id = Guid.NewGuid(),
                Name = setting.Name,
                StartDate = setting.StartDate,
                EndDate = setting.EndDate,
                CreatedAt = DateTime.UtcNow,
                RecurrencySetting = setting,
                Room = setting.Room,
                IsOnlineMeeting = setting.IsOnlineMeeting
            };

            ThrowIfConflictedMeetings(meeting);

            _calendar.Meetings.Add(meeting);

            var calculatedDate = GetNextOccurence(setting.StartDate, setting.RepeatUntil, setting.RepeatInterval);
            if (setting.StartDate == calculatedDate)
            {
                //var firstMeetingCreationResult = _calendar.SaveChanges();
                return meeting.Id;
            }

            var meetingDuration = setting.EndDate - setting.StartDate;

            while (calculatedDate.Date <= setting.RepeatUntil)
            {
                var nextMeetingOccurence = new Meeting
                {
                    Id = Guid.NewGuid(),
                    Name = setting.Name,
                    StartDate = calculatedDate,
                    EndDate = calculatedDate.Add(meetingDuration),
                    CreatedAt = DateTime.UtcNow,
                    RecurrencySetting = setting,
                    Room = setting.Room,
                    IsOnlineMeeting = setting.IsOnlineMeeting
                };

                ThrowIfConflictedMeetings(nextMeetingOccurence);

                _calendar.Meetings.Add(nextMeetingOccurence);

                calculatedDate = GetNextOccurence(calculatedDate, setting.RepeatUntil, setting.RepeatInterval);
            }

            return meeting.Id;
        }
    }
}
