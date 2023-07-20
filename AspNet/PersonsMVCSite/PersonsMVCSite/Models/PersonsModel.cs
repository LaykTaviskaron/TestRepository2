namespace PersonsMVCSite.Models
{
    public class PersonsModel
    {
        public string Name { get; set; } = "Persons list";

        public List<Person> PersonsList { get; set; } = new List<Person>();
    }


    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Dob { get; set; }
        public bool Verified { get; set; }
        public string Email { get; set; }
        public byte Age { get; set; }
    }
}
