using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Employee
    {        
        public string FullName { get; set; }
        public string Position { get; private set; }
        public decimal Salary { get; set; }

        public Department Department { get; set; }

        public Employee(string fullName, string position, decimal salary)
        {
            FullName = fullName;
            Position = position;
            Salary = salary;
        }

        public void UpgradePosition()
        {
            if (Position == Positions.JuniorSoftwareDeveloper)
            {
                Position = Positions.SoftwareDeveloper;
            }

            throw new ArgumentException("Cannot upgrade such position!");
        }
    }
}
