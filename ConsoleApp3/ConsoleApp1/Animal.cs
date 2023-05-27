// See https://aka.ms/new-console-template for more information

using System;


public abstract class Animal
{
    public decimal Weight { get; set; }
    public string Name { get; set; }
    public string Color { get; set; }
    public bool CanSwim { get; set; }

    public Animal()
    {

    }

    public Animal(string name, string color, decimal weight)
    {
        Name = name;
        Color = color;
        Weight = weight;
    }
}

