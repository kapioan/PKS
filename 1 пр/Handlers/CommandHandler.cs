using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorporatePractice1.Commands;

namespace CorporatePractice1.Handlers
{
    public static class CommandHandler
    {
        public static void Handle()
        {
            Console.ResetColor();

            Console.WriteLine("Введите команду \"/help\" для помощи по командам консоли:");

            string input = "0";

            while (input != "/exit")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;

                input = Console.ReadLine() ?? string.Empty;

                Console.ForegroundColor = ConsoleColor.Gray;

                if (!input.StartsWith('/'))
                    continue;

                input = input[1..];

                var cmd = input.Split(' ')[0]!;

                switch (cmd)
                {
                    case "help":
                        HelpCommand.Execute(input["help".Length..]);
                        break;
                    case "addgrade":
                        AddGradeCommand.Execute(input["addgrade".Length..]);
                        break;
                    case "viewstud":
                        ViewStudentPlantCommand.Execute(input["viewstud".Length..]);
                        break;
                    case "addstud":
                        AddStudCommand.Execute(input["addstud".Length..]);
                        break;
                    case "addsubj":
                        AddSubjCommand.Execute(input["addsubj".Length..]);
                        break;
                    case "showdb":
                        ShowDatabaseCommand.Execute(input["showdb".Length..]);
                        break;
                    case "exit":
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
}
