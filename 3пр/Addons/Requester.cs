using ExamSQL.Context;
using ExamSQL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSQL.Addons;
public static class Requester
{
    public static IEnumerable<Enrollee> SendRequest1(params string[] args)
    {
        using (ExamContext ctx = new())
        {
            Program program = ctx.Programs.Single(x => x.Name == args[0]);

            var data = ctx.ProgramEnrollees
                .Where(x => x.ProgramId == program.ProgramId)
                .Select(x => x.Enrollee)
                .ToList();

            return data;
        }
    }

    public static IEnumerable<Program> SendRequest2(params string[] args)
    {
        using (ExamContext ctx = new())
        {
            Subject exam = ctx.Subjects.Single(x => x.Name == args[0]);

            var data = ctx.ProgramSubjects
                .Where(x => x.SubjectId ==  exam.SubjectId)
                .Include(x => x.Program.Department)
                .Select(x => x.Program)
                .ToList();

            return data;
        }
    }

    public static IEnumerable<object> SendRequest3()
    {
        using(ExamContext ctx = new())
        {
            var data = ctx.EnrolleeSubjects
                .GroupBy(x => x.Subject)
                .Select(x => new
                {
                    x.Key,
                    MaxScore = x.Select(y => y.Result).Max(),
                    MinScore = x.Select(y => y.Result).Min()
                })
                .Select(x => $"Subject: {x.Key.Name}, Max: {x.MaxScore}, Min: {x.MinScore}")
                .ToList();

            return data;
        }
    }

    public static IEnumerable<object> SendRequest4(params string[] args)
    {
        using(ExamContext ctx = new())
        {
            // количество баллов
            int ballsSize = int.Parse(args[0]);

            var data = ctx.ProgramSubjects
                .Where(x => x.MinResult > ballsSize)
                .OrderByDescending(x => x.MinResult)
                .Select(x => $"Program's name: {x.Program.Name}, Min. balls: {x.MinResult}, Subj. ID: {x.SubjectId}")
                .ToList();
            
            return data;
        }
    }

    public static IEnumerable<object> SendRequest5(params string[] args)
    {
        using(ExamContext ctx = new())
        {
            int filter = int.Parse(args[0]);

            var data = ctx.ProgramEnrollees
                .GroupBy(x => x.Program)
                .Select(x => new
                {
                    x.Key,
                    EnrolleesCount = x.Select(x => x.Enrollee).Count()
                })
                .Where(x => x.EnrolleesCount >= filter)
                .OrderByDescending(x => x.EnrolleesCount)
                .Select(x => $"Program: {x.Key.Name}, Count: {x.EnrolleesCount}")
                .ToList();

            return data;
        }
    }

    public static IEnumerable<object> SendRequest6()
    {
        using (ExamContext ctx = new())
        {
            var data = ctx.EnrolleeAchievements
                .GroupBy(x => x.Enrollee)
                .Select(x => new
                {
                    x.Key,
                    Bonuses = x.Select(y => y.Achievement.Bonus).Sum(y => y)
                })
                .OrderByDescending(x => x.Bonuses)
                .Select(x => $"Enrollee: {x.Key.Name}, Bonuses: {x.Bonuses}")
                .ToList();
            
            return data;
        }
    }

    public static IEnumerable<object> SendRequest7()
    {
        using (ExamContext ctx = new())
        {
            var data = ctx.EnrolleeSubjects
                .GroupBy(x => x.Enrollee)
                .Select(x => new
                {
                    x.Key,
                    BallsArray = string.Join("::", x.Select(y => y.Result)),
                    TotalSum = x.Select(y => y.Result).Sum()
                })
                .OrderByDescending(x => x.TotalSum)
                .Select(x => $"Enrollee: {x.Key.Name}, TotalSum: {x.TotalSum}, BallsArray: {x.BallsArray}")
                .ToList();

            return data;
        }
    }

    public static IEnumerable<object> SendRequest8(params string[] args)
    {
        using (ExamContext ctx = new())
        {
            List<long> subjects = new List<long>()
            {
                ctx.Subjects.Single(x => x.Name == args[0]).SubjectId,
                ctx.Subjects.Single(y => y.Name == args[1]).SubjectId
            };

            var data = ctx.ProgramSubjects
                .GroupBy(x => x.Program)
                .Select(x => new
                {
                    x.Key,
                    Subjects = x.Select(x => x.Subject.SubjectId).Intersect(subjects)
                })
                .Where(x => x.Subjects.Count() > 1)
                // etc. we are checking on subjects LENGTH > 0 (0..1) either Count > 1;
                .Select(x => $"Program: {x.Key.Name}, S1: {x.Subjects.ElementAt(0)}, S2: {x.Subjects.ElementAt(1)}")
                .ToList();

            return data;
        }
    }

    public static IEnumerable<object> SendRequest9(params string[] args)
    {
        using (ExamContext ctx = new())
        {
            var data = ctx.EnrolleeSubjects
                .Join(ctx.ProgramSubjects, enrolleeSubject => enrolleeSubject.SubjectId, programSubject => programSubject.SubjectId,
                    (enrolleeSubject, programSubject) => new
                    {
                        EnrolleeSubject = enrolleeSubject,
                        ProgramSubject = programSubject
                    })
                .Join(ctx.ProgramEnrollees, combined => combined.EnrolleeSubject.EnrolleeId, programEnrollee => programEnrollee.EnrolleeId,
                    (combined, programEnrollee) => new
                    {
                        combined.EnrolleeSubject,
                        combined.ProgramSubject,
                        ProgramEnrollee = programEnrollee
                    })
                .GroupBy(x => new
                {
                    x.ProgramEnrollee.ProgramId,
                    x.ProgramEnrollee.EnrolleeId
                })
                .Select(x => new
                {
                    ProgramName = ctx.Programs.Single(y => y.ProgramId == x.Key.ProgramId).Name,
                    EnrolleeName = ctx.Enrollees.Single(y => y.Id == x.Key.EnrolleeId).Name,
                    TotalScore = x.Sum(y => y.EnrolleeSubject.Result)
                })
                .Select(x => $"Program: {x.ProgramName}, Enrollee: {x.EnrolleeName}, TotalScore: {x.TotalScore}")
                .ToList();

            return data;
        }
    }

    public static IEnumerable<object> SendRequest10(params string[] args)
    {
        using (ExamContext ctx = new())
        {
            var data = ctx.EnrolleeSubjects
                .GroupBy(x => x.Enrollee)
                .Select(x => new
                {
                    Enrollee = x.Key,
                    TotalResults = x.Sum(y => y.Result)
                })
                .Where(x => x.TotalResults < ctx.ProgramSubjects.Min(y => y.MinResult))
                .Select(x => $"Enrollee: {x.Enrollee.Name}, Results: {x.TotalResults}")
                .ToList();

            return data;
        }
    }
}
