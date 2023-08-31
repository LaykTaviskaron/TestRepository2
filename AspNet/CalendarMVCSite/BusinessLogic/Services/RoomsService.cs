using BusinessLogic.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;

namespace BusinessLogic.Services
{
    public class RoomsService : IRoomsService
    {
        private readonly CalendarDbContext _calendar;

        public RoomsService(CalendarDbContext calendar) 
        {
            _calendar = calendar;
        }

        public Guid Create(Room room)
        {
            _calendar.Rooms.Add(room);

            var result = _calendar.SaveChanges();

            return room.Id;
        }

        public IEnumerable<Room> GetAll()
        {
            return _calendar.Rooms.ToList();
        }

        public Room GetById(Guid id)
        {
            var result = _calendar.Rooms.FirstOrDefault(x => x.Id == id);
            if (result == null)
            {
                throw new ArgumentException("No such id exists");
            }

            return result;
        }
    }
}
