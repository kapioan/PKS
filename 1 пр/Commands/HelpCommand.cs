using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporatePractice1.Commands
{
    public static class HelpCommand
    {
        public static void Execute(string args)
        {
            Console.WriteLine("Квадратные скобки обозначают обязательность параметра ввода.\n");
            Console.WriteLine("Команда: \"help\" - показывает данное меню;\nКоманда: \"addgrade ::[ID студента] ::[ID предмета] ::[Оценка]\" - добавляет оценку студенту по ID студента, ID предмета и значению оценки (не больше 5 включ.)\nКоманда: \"viewstud ::[ID студента]\" - показывает учебный план студента с оценками");

            Console.WriteLine("Команда: \"addsubj ::[ID] ::[Название] ::[Объём лекций] ::[Объём практик]\" - техническая команда для создания предмета из бытия\n");
            Console.WriteLine("Команда: \"addstud ::[ID] ::[Ф] ::[И] ::[О]\" - техническая команда для создания студента из бытия\nКоманда: \"showdb\" - показывает JSON");
        }
    }
}
