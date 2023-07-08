// See https://aka.ms/new-console-template for more information
using ConsoleApp1;
using ConsoleApp1.Models;


using (var context = new ShopDbContext())
{
    //var userId = Guid.NewGuid();
    //context.Users.Add(new User
    //{
    //    FirstName = "John",
    //    LastName = "Smith",
    //    Id = userId,
    //    Phone = "+38063000000"
    //});
    //context.SaveChanges();


    var users = context.Users.Include("Orders").Where(x => x.Orders.Any(x => x.OrderDetails == "Give me some clothes and motorcycle")).ToList();


    //var allOrders = context.Orders.Where(x => x.OrderDetails == "Give me some clothes and motorcycle").ToList();

    //var selectAll = context.Database.SqlQuery<Order>("select * from orders").ToList();



    //context.Orders.Add(new Order
    //{
    //    Id = Guid.NewGuid(),
    //    Address = "Kyiv, Khreshatik st.",
    //    OrderDetails = "Give me some clothes and motorcycle",
    //    UserId = userId
    //});

    //context.Orders.Add(new Order
    //{
    //    Id = Guid.NewGuid(),
    //    Address = "Kyiv, Lesi Ukrainki blvd.",
    //    OrderDetails = "Give me some ice cream",
    //    UserId = userId
    //});

    //context.SaveChanges();
}

Console.ReadLine();