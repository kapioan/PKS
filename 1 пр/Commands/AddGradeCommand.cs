using CorporatePractice1.Handlers;

namespace CorporatePractice1.Commands
{
    public static class AddGradeCommand
    {
        public static void Execute(string args)
        {
            // Student ID, Subject ID, Grade
            var _args = ArgumentHandler.ParseArgs(args);

            if(_args.Length < 2) throw new ArgumentOutOfRangeException(nameof(args), "Недостаточно аргументов для выполнения!");

            long studentId = ArgumentHandler.Parse<long>(_args[0]),
                 subjectId = ArgumentHandler.Parse<long>(_args[1]);

            byte grade = ArgumentHandler.Parse<byte>(_args[2]);

            var db = FileHandler.Deserialize();

            if(db.Students.Select(x => x.StudentID).Contains(studentId))
            {
                db.StudentPlan.Add(new Data.Subtypes.PlanItem(studentId, subjectId, grade));

                FileHandler.Serialize(db);
            }
            else
            {
                WriteHandler.CWL("Ошибка! Нет такого студента в БД с таким ID!", ConsoleColor.DarkRed);
            }
        }
    }
}