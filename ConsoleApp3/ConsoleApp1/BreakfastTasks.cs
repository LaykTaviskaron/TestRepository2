// See https://aka.ms/new-console-template for more information

class BreakfastTasks
{
    public async Task Main()
    {
        var heatThePen = Task.Run(() => HeatUpAPen());
        var boilWater = Task.Run(() => BoilSomeWater());

        await heatThePen;
        var fryTwoEggs = Task.Run(() => FryTwoEggs());
        await fryTwoEggs;
        var fryThreeSlicesOfBacon = Task.Run(() => FryThreeSlicesOfBacon());

        await Task.WhenAll(boilWater);
        var coffee = Task.Run(() => MakeSomeCoffee());

        await Task.WhenAll(fryThreeSlicesOfBacon, coffee);

        Console.ReadLine();


        void HeatUpAPen()
        {
            Thread.Sleep(1000);
            Console.WriteLine("I'm firing pen...");
        }

        void BoilSomeWater()
        {
            Thread.Sleep(5000);
            Console.WriteLine("I'm boiling water...");
        }

        void FryTwoEggs()
        {
            Thread.Sleep(3000);
            Console.WriteLine("I'm frying eggs...");
        }

        void FryThreeSlicesOfBacon()
        {
            Thread.Sleep(2000);
            Console.WriteLine("I'm frying bacon...");
        }

        void MakeSomeCoffee()
        {
            Thread.Sleep(4000);
            Console.WriteLine("I'm making coffee...");
        }
    }
}