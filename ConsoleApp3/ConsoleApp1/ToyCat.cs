// See https://aka.ms/new-console-template for more information

public class ToyCat : Cat
{
    public ToyCat(string name, string color, decimal weight, bool isLazy) : base(name, color, weight, isLazy)
    {
        IsLazy = false;
    }

    public new void SaySmth()
    {
        Console.WriteLine("Toy cat!");
    }

    //public override string ToString()
    //{
    //    var isLazy = IsLazy ? "lazy" : "not lazy";

    //    return $"I'm a toy cat! My name is {Name}";
    //}
}

