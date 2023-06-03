using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public delegate int Sum(int a, int b);

    public class Test
    {
        public Sum SumDelegate { get; set; }
    }
}
