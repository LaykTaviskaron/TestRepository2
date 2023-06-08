// See https://aka.ms/new-console-template for more information
using ConsoleApp1;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

Console.WriteLine("Hello, World!");

string[] fruits = { "apple", "mango", "orange", "passionfruit", "grape" };

// Determine whether any string in the array is longer than "banana".
string longestName =
    fruits.Aggregate("banana",
                    (longest, next) =>
                        next.Length > longest.Length ? next : longest,
                    // Return the final result as an upper case string.
                    fruit => fruit.ToUpper());

Console.WriteLine(
    "The fruit with the longest name is {0}.",
    longestName);

// 
fruits.Count();
var areEnyElements = fruits.Any(x => x == "APPLE".ToLower());
var allElements = fruits.All(x => x.Length > 3);

var exists = fruits.Contains("banana");

if (fruits.Any())
{
    var firstEl = fruits.First(x => x.Length > 4);
}

var firstLongElement = fruits.FirstOrDefault(x => x.Length > 19);
if (firstLongElement != null)
{
    Console.WriteLine(firstLongElement);
}

var last = fruits.Last();
var last1 = fruits.LastOrDefault();

//fruits.Single();
var singleWith5Lenght = fruits.SingleOrDefault(x => x.Length == 19);

var sortedFruits = fruits.Where(x => x.Length > 5 
                    && (x.StartsWith("a") || x.StartsWith("g") || x.StartsWith("o")) 
                    && x.EndsWith("e"))
    .OrderBy(x => x)
    .ToList();



//var users = fruits.Select(x => new User { FavouriteFruit = x }).ToList();
// close connection

var users = new List<User> 
{
    new User { Id = 1, Name = "Alex", Age = 17 },
    new User { Id = 2, Name = "Alex", Age = 21 },
    new User { Id = 3, Name = "Mary", Age = 21 },
    new User { Id = 4, Name = "Mary", Age = 16 },

    new User { Id = 4, Name = "Alex", Age = 18 },
    new User { Id = 4, Name = "Mary", Age = 19 },
};

var groupedByAge = users.GroupBy(x => x.Age);


var userNames = users
    .Where(x => x.Age > 18)
    .Select(x => new { x.Name, x.Age });
//    .ToList();

var filteredUsers = users.AsEnumerable();
var offset = 2;
var pageSize = 2;

var uniqueAge = users.DistinctBy(x => x.Age);

if (true)
{
    filteredUsers = filteredUsers.Where(x => x.Age > 15);
}

if (true)
{
    filteredUsers = filteredUsers.Where(x => x.Name == "Mary" || x.Name == "Alex");
}

filteredUsers = filteredUsers.Skip(offset);

filteredUsers = filteredUsers.Take(pageSize);

filteredUsers = filteredUsers.SkipWhile(x => x.Age < 20);

var usersDict = users.Select(x => x.Id).ToHashSet();

var usersAge = users.Average(x => x.Age);

var usersByAge = ApplyAgeFilter(users, 18);
var usersByName = ApplyNameFilter(usersByAge, "Mary");


foreach (var item in userNames)
{
    Console.WriteLine(item.Name);
}


var usersByAge1 = users.MaxBy(x => x.Age);


Console.ReadLine();

IEnumerable<User> ApplyAgeFilter(IEnumerable<User> input, int age)
{
    return input.Where(x => x.Age > age);
}

IEnumerable<User> ApplyNameFilter(IEnumerable<User> input, string name)
{
    return input.Where(x => x.Name == name);
}