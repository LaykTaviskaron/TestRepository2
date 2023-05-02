// See https://aka.ms/new-console-template for more information

string name = "artem";
string surname = "Sokolov";

//if (name == "Artem")
//{
//    Console.WriteLine("Name is Artem");
//}
//else if (name == "artem")
//{
//    Console.WriteLine("Name is Artem");
//}
//else
//{
//    Console.WriteLine("Name is not Artem");
//}

string result = name == "Artem" || surname == "Sokolov"
    ? "Artem Sokolov"
    : "Unknown Person";

//if (name == "Artem" && surname == "sokolov")
//{
//}
//else if (name == "artem")
//{
//    Console.WriteLine("Name is Artem");
//}
//else
//{
//    Console.WriteLine("Name is not Artem");
//}

//string nameFromConsole = Console.ReadLine();

//if (nameFromConsole == null)
//{
//    Console.WriteLine("Name from console is null");
//}

//string switchResult = "";
//switch (nameFromConsole)
//{
//    default:
//        switchResult = "Unknown name";
//        break;
//    case "artem":
//        switchResult = "Name is artem";
//        break;
//    case "Artem":
//        switchResult = "Name is Artem";
//        break;
//}

//Console.WriteLine(switchResult);

//bool condition = false;
//while (condition)
//{
//    /// 1
//    /// 2
//    /// 3
//    break;
//}

//do
//{
//    // 1
//}
//while (condition);

//int number = 5;
//int sum = 0;
//for (int i = 0; i < 10; i++)
//{
//    Console.WriteLine("i = " + i);
//    if (i == 7)
//    {
//        Console.WriteLine("Found 7!");
//        break;
//    }

//    sum += number + i;
//}

//int number = 0;

//int sum = 0;
//while (number < 10)
//{
//    sum += number++ % 2 == 0 
//        ? number 
//        : 0;
//}

//Console.WriteLine("Sum: " + sum);

//do
//{
//    Console.WriteLine("Number: " + number++);
//    if (number == 7)
//    {
//        break;
//    }
//}
//while (true);

//Console.WriteLine("Found 7!");

//Console.WriteLine("Sum of 5 number 10 times:" + sum);

//string nameFromConsole = Console.ReadLine();
//int number = 0;
//bool conversionResult = int.TryParse(nameFromConsole, out number);

//if (!conversionResult)
//{
//    Console.WriteLine("Input is in incorrect format!");
//}
//else
//{
//    if (number == 0)
//    {
//        Console.WriteLine("It's a zero");
//    }
//    else if (number > 0)
//    {
//        Console.WriteLine("It's positive number");
//    }
//    else
//    {
//        Console.WriteLine("It's negative number");
//    }
//}

//Console.WriteLine(number);

Console.Write("Enter first number: ");
int number1 = int.Parse(Console.ReadLine());
Console.Write("Enter second number: ");
int number2 = int.Parse(Console.ReadLine());
int multiplicationResult = 0;

if (number1 > 0)
{
    multiplicationResult = number1 / number2;
}



Console.WriteLine("Multiplication result: " + multiplicationResult);
