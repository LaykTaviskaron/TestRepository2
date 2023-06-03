// See https://aka.ms/new-console-template for more information
using ConsoleApp1;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Timers;
using Timer = System.Timers.Timer;

Console.WriteLine("Hello, World!");

void MyTimerCallback(Object source, ElapsedEventArgs e)
{
    Console.WriteLine("Timer has passed! Time: " + e.SignalTime);
}

Timer timer = new Timer(3000);
timer.Elapsed += MyTimerCallback;

timer.Start();
Thread.Sleep(5 * 1000);


//timer.Stop();

//+= MyTimerCallback;

//Action<int> integerAction = (int a) => 
//{ 
//    Console.WriteLine(a); 
//};

//Func<int, bool> integerFunc = (int a) =>
//{
//    return a % 2 == 0;
//};

//Predicate<int> predicateFunc = (int a) => a > 0; 

////var counter = new Counter();
////counter.Increment(-5, predicateFunc);

////counter.PrintValue(integerFunc, 9, 1);

//List<int> ints = new List<int>() { 1, 2, 3, 4, 5, 6 };
//var onlyEven = ints.First(x => x == 3);

//Console.ReadLine();

//void ChangeValue(object sender, SampleEventArgs e)
//{
//    Console.WriteLine($"Changed value: {e.Text}");
//}

//void ChangeValue2(object sender, SampleEventArgs e)
//{
//    Console.WriteLine($"Changed value2: {e.Text}");
//}

//void Increment(int a)
//{
//    Console.WriteLine($"Incremented number by ");
//    Console.WriteLine(a); 
//}

//void Decrement () => Console.WriteLine("Decremented number");


//var counter = new Counter();
////counter.IncrementAction += Increment;
////counter.DecrementAction += Decrement;

//counter.ChangeValueEvent += ChangeValue;
//counter.ChangeValueEvent += ChangeValue2;

//counter.Increment(1);

//counter.ChangeValueEvent -= ChangeValue2;

//counter.Increment(1);


//counter.Increment(1);
//counter.Increment(1);

//counter.Decrement();
//counter.Decrement();
//counter.Decrement();

//int AddTwoNumbers(int a, int b)
//{
//    return a + b;
//}

//int AddTwoNumbersAndAdd10(int a, int b)
//{
//    return a + b + 10;
//}

//var test = new Test();
//test.SumDelegate = AddTwoNumbersAndAdd10;

//var result = test.SumDelegate(10, 20);

Console.ReadLine();