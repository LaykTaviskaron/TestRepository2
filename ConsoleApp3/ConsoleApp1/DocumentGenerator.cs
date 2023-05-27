using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public abstract class DocumentGenerator
    {
        public abstract void Generate(string data);
    }

    public class PdfGenerator : DocumentGenerator
    {
        public override void Generate(string data)
        {
            throw new NotImplementedException();
        }
    }
}
