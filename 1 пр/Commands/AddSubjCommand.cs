using CorporatePractice1.Handlers;

namespace CorporatePractice1.Commands
{
    public static class AddSubjCommand
    {
        public static void Execute(string args)
        {
            // Subject ID, Name, Lectures, Practices
            var _args = ArgumentHandler.ParseArgs(args);

            if (_args.Length < 3) throw new ArgumentOutOfRangeException(nameof(args), "Недостаточно аргументов для выполнения!");

            long id = ArgumentHandler.Parse<long>(_args[0]),
                 lects = ArgumentHandler.Parse<long>(_args[2]),
                 pract = ArgumentHandler.Parse<long>(_args[3]);

            string name = _args[1];

            var db = FileHandler.Deserialize();

            if (db.Subjects.Select(x => x.SubjectID).Contains(id))
            {
                WriteHandler.CWL("Предмет с таким ID уже есть в БД!", ConsoleColor.DarkRed);
            }
            else
            {
                db.Subjects.Add(new Data.Subtypes.Subject(id, name, (ulong)lects, (ulong)pract));

                FileHandler.Serialize(db);
            }
        }
    }
}