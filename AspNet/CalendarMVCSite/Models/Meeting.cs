namespace Models
{
    public class Meeting
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsOnlineMeeting { get; set; }

        public Room? Room { get; set; }

        public RecurrencySetting? RecurrencySetting { get; set; }
    }
}