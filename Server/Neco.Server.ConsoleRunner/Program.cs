using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neco.Server.Infrastructure;

namespace Neco.Server.ConsoleRunner
{
    class Program
    {
        enum Commands { Help, Exit };
        static void Main(string[] args)
        {
            InitConsole();
            var container = Bootstrap.Run();
            string[] cmds = Enum.GetNames(typeof(Commands));
            string cmd = "";
            while (!cmd.Equals(Commands.Exit.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                if (cmd.Equals(Commands.Help.ToString(), StringComparison.InvariantCultureIgnoreCase))
                {
                    Console.WriteLine("Available commands: " + string.Join(", ", cmds));
                }
                else
                {
                    Console.WriteLine("Unrecognized command...");
                    Console.WriteLine("Enter 'help' to see all visible commands");
                }
                cmd = Console.ReadLine();
            }
        }

        private static void InitConsole()
        {
            Console.Title = "NeCo Server";
            string annotation = "(c) 2017 NeCo. All rights reserved.";
            string logo = "    |              |\n   .' `.          .' `.\n  : :   \\_..--.._/   : :\n  | . '            ` . |\n  '   ___        ___   `\n  '  `.  `.    .'  .'  `\n :     `-.|    |.-'     :\n .     .  `    '  .     ,\n /      `. \\  / .'      \\ \n`,'  . . .` `' '. . .  `.' \n `,'    .__.--.__.    `.'\n  `,'                `.'\n   `,'-`;::....::;' -`.'\n    `    ''::::``    '";
            int versionMajor = 0;
            int versionMinor = 1;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("--------NECO SERVER v" + versionMajor + "." + versionMinor + "--------");
            Console.WriteLine(annotation + "\n\n");
            Console.WriteLine(logo + "\n\n");
        }
    }
}
