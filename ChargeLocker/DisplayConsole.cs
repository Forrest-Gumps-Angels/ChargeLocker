using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargeLocker
{
    public class DisplayConsole : IDisplay
    {
        public void Display(string message)
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine(message);
            Console.ResetColor();

        }
        public void DisplayStatus(string message)
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();

        }
    }
}
