using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorporatePractice1.Data.Subtypes;
using CorporatePractice1.Handlers;

namespace CorporatePractice1.Data
{
    public class Everything
    {
        public List<Student> Students { get; set; } = new();
        public List<Subject> Subjects { get; set; } = new();
        public List<PlanItem> StudentPlan { get; set; } = new();

        public Everything() { }
        public override string ToString()
        {
            return File.ReadAllText(Program.Fullpath);
        }
    }
}
