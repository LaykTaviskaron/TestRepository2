using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Domain
{
    public static class Factory
    {
        public static IRepository GetRepository()
        {
            return new Repository();
        }
    }
}
