using Models;

namespace BusinessLogic.Interfaces
{
    public interface IRoomsService
    {
        IEnumerable<Room> GetAll();
        Guid Create(Room meeting);
        Room GetById(Guid id);
    }
}
