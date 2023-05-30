// See https://aka.ms/new-console-template for more information
using ConsoleApp1;

//Console.WriteLine("Hello, World!");


//var person = new Person("Artem", "Sokolov", "Odesa", 27, "789");
//var anotherPerson = new Person("John", "Doe", "Chicago", 42, "789");
//var copyPerson = new Person(anotherPerson);

//var newPerson = new Person
//{
//    PhoneNumber= "12345",
//    Address = " ",
//    Age = 30,
//    FirstName = "",
//    LastName =  " "
//};


//var phoneBook = new PhoneBook();

//phoneBook.AddNewRecord(person);
//phoneBook.AddNewRecord(anotherPerson);
//phoneBook.AddNewRecord(copyPerson);

//phoneBook.ShowAll();

//Console.ReadLine();

var employee = new Employee("John Doe", Positions.SoftwareDeveloper.ToString(), 1000);

//employee.Position = "Not existing position";
Console.WriteLine(employee.Position);