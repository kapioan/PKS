using CorporatePractice1.Handlers;
using System.Text;

namespace CorporatePractice1.Commands
{
    public static class ViewStudentPlantCommand
    {
        public static void Execute(string args)
        {
            // Student ID
            var _args = ArgumentHandler.ParseArgs(args);

            if (_args.Length < 1) throw new ArgumentOutOfRangeException(nameof(args), "Недостаточно аргументов для выполнения!");

            long stud_id = ArgumentHandler.Parse<long>(_args[0]);

            var db = FileHandler.Deserialize();

            if(db.Students.Select(x => x.StudentID).Contains(stud_id))
            {
                var plan = db.StudentPlan.Where(x => x.StudentID == stud_id)
                                         .GroupBy(x => x.SubjectID)
                                         .Select(x => new
                                         {
                                             x.Key,
                                             Grades = x.Where(y => y.SubjectID == x.Key).Select(y => y.Grade)
                                         });

                var stud = db.Students.FirstOrDefault(x => x.StudentID == stud_id)!;

                WriteHandler.CWL($"| План студента {stud.StudentSurname} {stud.StudentName} {stud.StudentPatronimic} | ", ConsoleColor.Cyan);
                
                foreach(var plan_item in plan)
                {
                    var subject = db.Subjects.FirstOrDefault(x => x.SubjectID == plan_item.Key)!;

                    WriteHandler.CWL($"| Оценки по [{subject.SubjectName}]: {string.Join(", ", plan_item.Grades)}", ConsoleColor.Yellow);

                    var grades_dic = new byte[] { 1, 2, 3, 4, 5 };
                    var grades_count = plan_item.Grades.Count();

                    StringBuilder init_string = new($"| Процентный состав оценок по [{subject.SubjectName}]:");

                    foreach (var grade in grades_dic)
                    {
                        var freq = ((double)plan_item.Grades.Count(x => x == grade) / (double)grades_count) * 100;

                        init_string.Append($"({grade})= {Math.Round(freq, 2)}%;");
                    }

                    WriteHandler.CWL(init_string.ToString(), ConsoleColor.DarkYellow);
                }
            }
            else
            {
                WriteHandler.CWL("Такого студента нет в БД!", ConsoleColor.DarkRed);
            }
        }
    }
}