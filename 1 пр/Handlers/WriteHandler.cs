using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporatePractice1.Handlers
{
    public static class WriteHandler
    {
        public static void CW(string msg, ConsoleColor foreground = ConsoleColor.Gray, ConsoleColor background = ConsoleColor.Black)
        {
            Console.ForegroundColor = foreground;
            Console.BackgroundColor = background;

            Console.Write(msg);

            Console.ResetColor();
        }

        public static void CWL(string msg, ConsoleColor foreground = ConsoleColor.Gray, ConsoleColor background = ConsoleColor.Black)
        {
            CW(msg + "\n", foreground, background);
        }
    }
}
