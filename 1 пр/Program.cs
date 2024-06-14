using CorporatePractice1.Data;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

using CorporatePractice1.Commands;

namespace CorporatePractice1
{
    public class Program
    {
        public static readonly string Fullpath = "Resources/students.json";

        public static string Dir => Path.GetDirectoryName(Fullpath) ?? Fullpath;
        public static string Filename => Path.GetFileName(Fullpath);

        public static void Main(string[] args)
        {
            Handlers.FileHandler.Setup();

            Handlers.CommandHandler.Handle();
        }
    }
}