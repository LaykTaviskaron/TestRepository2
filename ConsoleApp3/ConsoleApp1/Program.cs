// See https://aka.ms/new-console-template for more information
using ConsoleApp1;
using static System.Console;

Console.WriteLine("Hello, World!");
Console.WriteLine("Hello, Team!");
Console.WriteLine("First line");
Console.WriteLine("Second line");

// students, teachers, rooms, lessons

var cathedra = new Cathedra
{
    Name = "Tech Cathedra",
    Teachers = new[]
    {
        new Teacher
        {
            Name = "John"
        },
        new Teacher
        {
            Name = "Mary"
        }
    },
    Groups = new[]
    {
        new Group
        {
            Name = "A1",
            Lessons = new[] 
            {
                new Lesson
                {
                    Name = "Math"
                }
            },
            Students = new[]
            {
                new Student
                {
                    Name = "Artem"
                }
            }
        },
        new Group
        {
            Name = "A2",
            Lessons = new[]
            {
                new Lesson
                {
                    Name = "Math"
                }
            },
            Students = new[]
            {
                new Student
                {
                    Name = "Artem"
                }
            }
        }
    }
};

foreach (var group in cathedra.Groups)
{
    Console.WriteLine($"{group.Name}: ");

    foreach (var student in group.Students)
    {
        Console.WriteLine($"{student.Name}");
    }
}

// initialize cathedra with default constructor
// cathedra.AddGroup();

// cathedra.AddTeacher();
// cathedra.AddTeacher();
// cathedra.AddTeacher();

// group.AddStudent();
// group.AddStudent();
// group.AddStudent();
// group.AddStudent();
