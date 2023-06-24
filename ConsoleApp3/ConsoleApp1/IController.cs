using System;

namespace ConsoleApp1
{
    public interface IController
    {
        public IController ExecuteAction(ConsoleHelper helper = null);
    }
}
