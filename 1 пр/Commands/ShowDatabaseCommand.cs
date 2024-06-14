using CorporatePractice1.Handlers;

namespace CorporatePractice1.Commands
{
    public class ShowDatabaseCommand
    {
        public static void Execute(string args)
        {
            var json = FileHandler.Deserialize();

            Console.ForegroundColor = ConsoleColor.DarkGray;

            Console.WriteLine(json);

            Console.ResetColor();
        }
    }
}