// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using System.Collections;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

int[] array = new int[11] { 1, 12, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

int[,] table = new int[10,10];

int[] array2 = new int[3] { 1, 2, 3 };

string[] names = new string[3] { "Name", "Surname", "FamilyName" };


var first = array[0];
var second = array[1];
var third = array[2];
var fourth = array[3];

void PrintAll(int[] array)
{
    if (array.Length == 1000)
    {
        Console.WriteLine("Array is too big");
        return;
    }

    foreach (int element in array)
    {
        Console.WriteLine(element);
    }
}

void MultBy2(int[] array)
{
    for (int i = 0; i < array.Length; i++)
    {
        array[i] *= 2;
    }

    //PrintAll(array);
}

void InitTwoDimensionalArray(int[,] array)
{
    var random = new Random();
    for (int i = 0; i < array.GetLength(0); i++)
    {
        for (int j = 0; j < array.GetLength(1); j++)
        {
            array[i, j] = random.Next(100);
        }
    }
}

void PrintTwoDimensionalArray(int[,] array)
{
    for (int i = 0; i < array.GetLength(0); i++)
    {
        for (int j = 0; j < array.GetLength(1); j++)
        {
            Console.Write(array[i, j] + " ");
        }

        Console.WriteLine();
    }
}

void InitArray(int[] array, int start, int end)
{
    for (int i = 0; i < array.Length; i++)
    {
        array[i] = start++;
        if (start > end) 
        {
            array[i] = -1;
        }
    }
}

int[] Copy(int[] array)
{
    var newArray = new int[array.Length]; 
    var i = 0;

    foreach (var element in array)
    {
        newArray[i++] = element;
    }

    return newArray;
}

int[] Reverse(int[] array)
{
    int[] newArray = new int[array.Length];
    int i = array.Length - 1;
    foreach (var item in array)
    {
        newArray[i--] = item;
    }
    return newArray;
}

int Sum(int[] array)
{
    int sum = 0;

    foreach (var item in array)
    {
        sum += item;
    }

    return sum;
}

int SumTwoDimensional(int[,] array)
{
    int sum = 0;

    foreach (var item in array)
    {
        sum += item;
    }

    return sum;
}

//int[] copiedArray = array;//Copy(array);
//MultBy2(copiedArray);
//int[] newArray = new int[20];
//InitArray(newArray, 1, 12);
//PrintAll(newArray);

void SortArray(string[] array)
{
    for (int j = 0; j < array.Length - 1; j++)
    {
        bool isSorted = true;

        for (int i = 0; i < array.Length - 1; i++) // 1,2,3,4,6,5,10
        {            
            if (array[i] > array[i + 1])
            {
                isSorted = false;
                var temp = array[i];
                array[i] = array[i + 1];
                array[i + 1] = temp;
            }
        }

        if (isSorted)
        {
            break;
        }
    }    
}

//InitTwoDimensionalArray(table);
//PrintTwoDimensionalArray(table);

//Console.WriteLine($"Sum: {SumTwoDimensional(table)}");

//var reversedArray = Reverse(array);
SortArray(array);
PrintAll(array);

//PrintAll(copiedArray);