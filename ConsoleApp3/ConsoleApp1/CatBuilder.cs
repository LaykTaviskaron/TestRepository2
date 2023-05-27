using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class CatBuilder
    {
        private Cat cat = new Cat();

        public CatBuilder WithName(string name)
        {
            cat.Name = name;
            return this;
        }

        public CatBuilder WithColor(string color)
        {
            cat.Color = color;
            return this;
        }

        public CatBuilder WithWeigh(decimal weight)
        {
            cat.Weight = weight;
            return this;
        }

        public Cat Build()
        {
            return cat;
        }
    }
}
