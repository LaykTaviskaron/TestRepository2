using ConsoleApp1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class ShopDbContext : System.Data.Entity.DbContext
    {
        public System.Data.Entity.DbSet<Order> Orders { get; set; }

        public System.Data.Entity.DbSet<User> Users { get; set; }

        public System.Data.Entity.DbSet<Employee> Employees { get; set; }

        //public static DbContextOptions

        public ShopDbContext() : base("Server=localhost;Database=OrdersDB;Trusted_Connection=True;")
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //optionsBuilder.("Server=localhost;Database=OrdersDB;Trusted_Connection=True;");
        //    optionsBuilder.LogTo(Console.WriteLine);
        //}
    }

}
