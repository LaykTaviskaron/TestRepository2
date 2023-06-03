using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class SampleEventArgs
    {
        public SampleEventArgs(string text) { Text = text; }
        public string Text { get; } // readonly
    }

    public delegate void ChangeValue(object sender, SampleEventArgs e);
    public delegate void Increment(int input);

    public delegate void Decrement();

    public class Counter
    {
        private int _number; 

        public Increment IncrementAction { get; set; }
        public Decrement DecrementAction { get; set; }
        public ChangeValue ChangeValueAction { get; set; }

        public event ChangeValue ChangeValueEvent;


        public void Increment(int input, Predicate<int> predicateFunc)
        {            
            if (predicateFunc(input))
            {
                _number += input;
                ChangeValue();
            }
        }

        public void Decrement()
        {
            ChangeValue();

            _number--;
        }

        public void ChangeValue()
        {
            ChangeValueEvent?.Invoke(this, new SampleEventArgs("Hello"));
        }

        public void PrintValue(Func<int, int, string> integerFunc, int a, int b)
        {
            integerFunc(a, b);
        }
    }
}
