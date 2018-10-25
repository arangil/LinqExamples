using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqExamples
{
    public static class Utils
    {
        public static void AddLineBreak(string message)
        {
            Console.WriteLine();
            Console.WriteLine(message);
            Console.WriteLine("----------------------------------------------------");
        }
    }
}
