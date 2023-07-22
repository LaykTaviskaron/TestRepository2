using Models;

namespace BusinessLogic.Interfaces
{
    public interface IMeetingsService
    {
        IEnumerable<Meeting> GetAll();
        Guid Create(Meeting meeting);
        IEnumerable<Meeting> GetByDateRange(DateTime? startDate, DateTime? endDate);
    }
}
