namespace Models
{
    public class Room
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ICollection<Meeting> Meetings { get; set; }
    }
}