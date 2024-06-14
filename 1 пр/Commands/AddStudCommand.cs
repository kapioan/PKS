using CorporatePractice1.Handlers;

namespace CorporatePractice1.Commands
{
    public static class AddStudCommand
    {
        public static void Execute(string args)
        {
            // Student ID, Surname, Name, Patronim
            var _args = ArgumentHandler.ParseArgs(args);

            if (_args.Length < 3) throw new ArgumentOutOfRangeException(nameof(args), "Недостаточно аргументов для выполнения!");

            long id = ArgumentHandler.Parse<long>(_args[0]);

            string surname = _args[1],
                   name = _args[2],
                   patron = _args[3];

            var db = FileHandler.Deserialize();

            if(db.Students.Select(x => x.StudentID).Contains(id))
            {
                WriteHandler.CWL("Студент с таким ID уже есть в БД!", ConsoleColor.DarkRed);
            }
            else
            {
                db.Students.Add(new Data.Subtypes.Student(id, name, surname, patron));

                FileHandler.Serialize(db);
            }
        }
    }
}