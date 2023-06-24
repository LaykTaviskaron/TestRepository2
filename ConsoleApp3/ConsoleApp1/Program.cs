// See https://aka.ms/new-console-template for more information
using ConsoleApp1;
using ConsoleApp1.Domain;

IController controller = new MenuItemController();

while (controller != null)
{
    controller = controller.ExecuteAction();
}

Console.WriteLine("Execution has ended");
Console.ReadLine();