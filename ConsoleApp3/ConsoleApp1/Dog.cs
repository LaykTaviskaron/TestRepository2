// See https://aka.ms/new-console-template for more information

using System;

public class Dog : Animal, ISpeakable
{
    public Dog(string name, string color, decimal weight) : base(name, color, weight)
    {

    }

    public void SaySmth()
    {
        Console.WriteLine("Bark!");
    }

    public override string ToString()
    {
        return $"I'm a dog! My name is {Name}";
    }
}

