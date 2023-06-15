using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    [System.AttributeUsage(System.AttributeTargets.All)]
    public class DefaultValue : Attribute 
    {
        public DefaultValue(int value) 
        {
            Value = value;
        }

        public int Value { get; set; }
    }

    [DefaultValue(10)]
    public class Test
    {
        [DefaultValue(20)]
        public int AnotherValue { get; set; }

        [DefaultValue(10)]
        public int Value { get; set;  }

        public string Name { get; }

        private string PrivateName { get; set; }

        public void Print()
        {
            Console.WriteLine(Value);
        }

        private bool Print([DefaultValue(20)] int a)
        {
            Console.WriteLine(a);
            return true;
        }
    }
}
