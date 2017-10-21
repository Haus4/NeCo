using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neco.BeaCon
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "NeCo BeaCon";
            string annotation = "(c) 2017 NeCo. All rights reserved.";
            int versionMajor = 0;
            int versionMinor = 1;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("--------NECO BeaCon v" + versionMajor + "." + versionMinor + "--------" + "\n" + "\n");
        }
    }
}
