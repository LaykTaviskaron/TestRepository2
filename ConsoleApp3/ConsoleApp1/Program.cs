// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using ConsoleApp1;

void MakeNoise(ISpeakable animalWhichCanSpeak)
{
    animalWhichCanSpeak.SaySmth();
}


//var dog = new Dog("Jack", "white", 15);
////var cat = new Cat("Sally", "black", 10);

//var cat1 = new Cat("Mary", "white", 12, false);
//var cat2 = new Cat("Mary", "white", 10, true);


//var catEquals = cat2.Equals(cat1);
//var dogEquals = cat2.Equals(dog);
//var stringEquals = cat2.Equals("cat");

//var code = cat2.GetHashCode();

//Console.WriteLine("Cat equals: " + catEquals);
//Console.WriteLine("Dog equals: " + dogEquals);
//Console.WriteLine("String equals: " + stringEquals);

//cat.SaySmth();
//dog.SaySmth();


//MakeNoise(cat);
//MakeNoise(dog);

//Console.WriteLine(cat.ToString());

//var toyCat = new ToyCat("Sally", "black", 10, false);
var cat = new Cat("Sally", "black", 10, false);
//var animal = new Animal("Sally", "black", 10);

//Cat cat1 = toyCat;
//cat1.ToString();

//Animal animal1 = cat;
//Animal animal2 = toyCat;

//toyCat.SaySmth();
//cat.SaySmth();
//animal.SaySmth();

void CallAllAction(Animal animal)
{
    if (animal is ISpeakable speakableCat)
    {
        speakableCat.SaySmth();
    }

    if (animal is ICanSwim catWhichSwims)
    {
        catWhichSwims.DoSwim();
    }
}

CallAllAction(cat);

//cat1.SaySmth();

//animal1.SaySmth();
//animal2.SaySmth();

//((ToyCat)animal2).SaySmth();

//if (animal1 is ToyCat anotherToy)
//{
//    anotherToy.SaySmth();
//} 

var catFromBuidler = new CatBuilder().WithName("Sally").WithWeigh(10).Build();


Console.ReadLine();

