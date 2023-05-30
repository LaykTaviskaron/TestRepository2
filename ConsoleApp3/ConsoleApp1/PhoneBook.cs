using System;

namespace ConsoleApp1
{
    public class PhoneBook
    {
        public Person[] persons = new Person[0];

        public void AddNewRecord(Person person)
        {
            Array.Resize(ref persons, persons.Length + 1);
            persons[persons.Length - 1] = person;
        }

        public void ShowAll()
        {
            Person.SayHello();
            foreach (Person person in persons) 
            {
                Console.WriteLine(person.GetInfo());
            }
        }
    }
}
