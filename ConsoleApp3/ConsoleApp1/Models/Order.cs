using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Models
{
    public class Order
    {
        public Guid Id { get; set; }

        public string Address { get; set; }

        public Guid UserId { get; set; }

        public Guid? EmployeeId { get; set; }

        public string OrderDetails { get; set; }
    }
}
