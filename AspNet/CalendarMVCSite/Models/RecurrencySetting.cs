namespace Models
{
    public class RecurrencySetting
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsOnlineMeeting { get; set; }

        public Room Room { get; set; }

        public RepeatInterval RepeatInterval { get; set; }

        public DateTime RepeatUntil { get; set; }
    }
}