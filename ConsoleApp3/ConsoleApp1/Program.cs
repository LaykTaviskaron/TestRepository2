// See https://aka.ms/new-console-template for more information
using System.Text;

Console.WriteLine("Hello, World!");


char c = 'A';

void GetCharInfo(char c)
{
    Console.WriteLine($"Value: {c}");
    Console.WriteLine($"IsDigit: {char.IsDigit(c)}");
    Console.WriteLine($"IsLetter: {char.IsLetter(c)}");
    Console.WriteLine($"IsLower: {char.IsLower(c)}");
    Console.WriteLine($"IsUpper: {char.IsUpper(c)}");
    Console.WriteLine($"IsSymbol: {char.IsSymbol(c)}");
    Console.WriteLine($"IsAscii: {char.IsAscii(c)}");
}

string s1 = "BBB1";
var address = "Chreshatik street, Kyiv, Ukraine"; //"artem sokolov" // "Artem Sokolov"
string s2 = "bbb"; // "" ''

string[] cities = { "Kyiv", "Lviv", "Kharkiv", "Dnipro", "Odesa" };

//var allCities = new StringBuilder();
//foreach (var item in cities)
//{
//    allCities.AppendLine(item);
//}

//Console.WriteLine(allCities);

//Error 502
//Error 1005

//string newString = address.Replace("chreshatik", "Lesyi Ukrainki", StringComparison.InvariantCultureIgnoreCase);
//// replace

//Console.WriteLine(newString);

//Console.WriteLine(newString);

//var splitResult = address.Split(", ");

//foreach(var item in splitResult)
//{
//    Console.WriteLine(item);
//}

//foreach (var item in address)
//{
//    GetCharInfo(item);
//}

// AAA  BBB  AAABBB        


//GetCharInfo(c);

var inputString = Console.ReadLine(); // 12    15.87 18 19 22 fgjkhfgk 0 -5

var splittedInput = inputString.Trim().Split(" ");
int[] resultArray = new int[splittedInput.Length]; // triling spaces
int i = 0;

foreach (var item in splittedInput)
{
    if (int.TryParse(item, out var parsedResult))
    {
        resultArray[i++] = parsedResult;
    }

    // 1.Split for white spaces
    // 2. We receive array of strings
    // 3. Create int array size of splitted string array length
    // 4. New cycle for each string item in string array
    // 5. Convert string to int32
}

Console.WriteLine("end");
// return int[] array