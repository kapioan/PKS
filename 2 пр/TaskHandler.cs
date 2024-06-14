using EstateSQL.PostgreSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateSQL;

public static class TaskHandler
{
    public static string[] ReadInput()
    {
        return ReadLine()!.Split(',').Select(x => x.Trim()).ToArray();
    }

    public static void Print(object obj, ConsoleColor foreground_color = ConsoleColor.Gray)
    {
        ForegroundColor = foreground_color;

        WriteLine(obj.ToString());

        ResetColor();
    }

    public static void T1()
    {
        Print("Task 1:", ConsoleColor.Yellow);

        Print("Type input data for task (cost1, cost2, area)", ConsoleColor.DarkYellow);

        var input = ReadInput();

        using (EstateContext ctx = new EstateContext())
        {
            var res = ctx.EstateObjects.Where(x => x.Cost > float.Parse(input[0])
                                                && x.Cost < float.Parse(input[1])
                                                && ctx.Areas.Single(y => y.Id == x.AreaId)!.Name == input[2])
                                       //&& Wrapper.SelectValue<Area>(dbContext, x.AreaId).Name == input[2])
                                       .Select(x => $"Address: {x.Address}, Square: {x.Square}, Floor: {x.Floor}");

            Print(string.Join("\n", res), ConsoleColor.Cyan);
        }
    }

    public static void T2()
    {
        Print("Task 2:", ConsoleColor.Yellow);

        using (EstateContext ctx = new EstateContext())
        {
            var res = ctx.EstateObjects.Where(x => x.Rooms == 2)
                                       .Select(x => new
                                       {
                                           x.Id,
                                           Realtor = ctx.Realtors.Single(z => z.Id == ctx.Sales.Single(y => y.EstateObjectId == x.Id).RealtorId)
                                       })
                                       .Select(x => $"{x.Id}, Surname: {x.Realtor.Surname}, Name: {x.Realtor.Name}, Patronim: {x.Realtor.Patronim}");

            Print(string.Join("\n", res), ConsoleColor.Cyan);
        }
    }

    public static void T3()
    {
        Print("Task 3:", ConsoleColor.Yellow);
        Print("Type name of the Area: ", ConsoleColor.DarkYellow);
        var sinput = ReadLine()!.Trim();

        using (EstateContext ctx = new EstateContext())
        {
            var res = ctx.EstateObjects.Where(x => x.Rooms == 2
                                        && ctx.Areas.Single(y => y.Id == x.AreaId).Name == sinput)
                                       .Select(x => x.Cost);

            Print(res.Sum(), ConsoleColor.Cyan);
        }
    }

    public static void T4()
    {
        Print("Task 4:", ConsoleColor.Yellow);
        Print("Type surname of realtor: ", ConsoleColor.DarkYellow);

        var sinput = ReadLine()!.Trim();

        using (EstateContext ctx = new())
        {
            var res = ctx.Sales.Where(x => x.RealtorId == ctx.Realtors.Single(y => y.Surname == sinput).Id)
                               .Select(x => x.Cost);

            Print($"Max: {res.Max()}, Min: {res.Min()}", ConsoleColor.Cyan);
        }
    }

    public static void T5()
    {
        Print("Task 5:", ConsoleColor.Yellow);
        Print("Type estate's type, realtor's surname, criteria:", ConsoleColor.DarkYellow);

        var input = ReadInput();

        using (EstateContext ctx = new())
        {
            var realtor = ctx.Realtors.Single(x => x.Surname == input[1]);
            var criteria = ctx.Criterias.Single(x => x.Name == input[2]);
            var estate_type = ctx.Types.Single(x => x.Name == input[0][0]);

            var estatesTotalIds = ctx.Sales
                                    .Where(x => x.RealtorId == realtor.Id)
                                    .Select(x => x.EstateObjectId)
                                    .Intersect(ctx.Grades.Where(x => x.CriteriaId == criteria.Id).Select(x => x.EstateObjectId))
                                    .Intersect(ctx.EstateObjects.Where(x => x.TypeId == estate_type.Id).Select(x => x.Id))
                                    .ToList(); // Client-side evaluation, WTF is this working and not at the same moment??

            var res = ctx.Grades
                .Where(x => estatesTotalIds.Contains(x.EstateObjectId)) 
                .Select(x => x.GradeValue);

            Print($"Average Grade value: {(res.Any() ? res.Sum(x => x) / res.Count() : "No data.")}", ConsoleColor.Cyan);
        }
    }

    public static void T6()
    {
        Print("Task 6 (Floor=2):", ConsoleColor.Yellow);

        using (EstateContext ctx = new())
        {
            var res = ctx.EstateObjects.Where(x => x.Floor == 2)
                                       .GroupBy(x => x.AreaId)
                                       .Select(x => new
                                       {
                                           AreaName = ctx.Areas.Single(y => y.Id == x.Key).Name,
                                           ObjectsCount = x.Select(x => x).Count()
                                       })
                                       .Select(x => $"{x.AreaName}: {x.ObjectsCount};");

            Print(string.Join("\n", res));
        }
    }

    public static void T7()
    {
        Print("Task 7:", ConsoleColor.Yellow);

        using(EstateContext ctx = new())
        {
            //var res = ctx.Sales
            //            .GroupBy(x => x.RealtorId)
            //            .Select(x => new
            //            {
            //                Realtor = ctx.Realtors.Single(y => y.Id == x.Key),
            //                Flats = ctx.EstateObjects
            //                    .Where(y => x.Select(s => s.EstateObjectId).Contains(y.Id) && y.TypeId == 0)
            //                    .ToList() // Force client-side evaluation
            //            })
            //            .Select(x => $"Realtor: {x.Realtor.Surname}:{x.Realtor.Name}:{x.Realtor.Patronim}, Flats (Count)={x.Flats.Count}")
            //            .ToList();

            // fix of past code:
            var res = ctx.Sales
                        .GroupBy(x => x.RealtorId)
                        .Select(group => new
                        {
                            Realtor = ctx.Realtors.FirstOrDefault(r => r.Id == group.Key),
                            FlatsCount = group.Join(
                                ctx.EstateObjects.Where(e => e.TypeId == 0),
                                sale => sale.EstateObjectId,
                                estateObject => estateObject.Id,
                                (sale, estateObject) => estateObject)
                                .Count()
                        })
                        .ToList();

            foreach (var item in res)
            {
                Print($"Realtor: {item.Realtor?.Surname}:{item.Realtor?.Name}:{item.Realtor?.Patronim}, Flats (Count)={item.FlatsCount}");
            }
        }
    }

    public static void T8()
    {
        Print("Task 8:", ConsoleColor.Yellow);

        using(EstateContext ctx = new())
        {
            var res = ctx.EstateObjects.GroupBy(x => x.AreaId)
                                       .Select(group => new
                                       {
                                           AreaObj = ctx.Areas.Single(x => x.Id == group.Key),
                                           Flats = group.OrderByDescending(x => x.Cost).Take(3)
                                       });

            foreach(var item in res)
            {
                var sb = new StringBuilder($"Area: {item.AreaObj.Name}, Flats (by cost):");

                foreach (var x in item.Flats)
                    sb.Append($"{x.Address}, {x.Cost}, {x.Floor}");

                Print(sb);
            }
        }
    }

    public static void T9()
    {
        Print("Task 9:", ConsoleColor.Yellow);
        Print("Type Surname and Name for realtor:");

        var input = ReadInput();

        using(EstateContext ctx = new())
        {
            var realtor = ctx.Realtors.Single(x => x.Surname == input[0] &&  x.Name == input[1]);

            var res = ctx.Sales.Where(x => x.RealtorId == realtor.Id)
                               .GroupBy(x => x.SaleDate.Year)
                               .Select(x => new
                               {
                                   x.Key,
                                   SalesCount = x.Select(x => x.EstateObjectId).Count(),
                               })
                               .Where(x => x.SalesCount > 2);

            Print(string.Join("\n", res.Select(x => $"Year: {x.Key}, Count: {x.SalesCount}")));
        }
    }

    public static void T10()
    {
        Print("Task 10:", ConsoleColor.Yellow);

        using(EstateContext ctx = new())
        {
            var res = ctx.EstateObjects.GroupBy(x => x.AnnouncementDate.Year)
                                       .Select(x => new
                                       {
                                           x.Key,
                                           AnnCount = x.Select(x => x.Id).Count(),
                                       })
                                       .Where(x => x.AnnCount >= 2 && x.AnnCount <= 3)
                                       .Select(x => $"Year: {x.Key}");

            Print(res.Count() != 0 ? string.Join("\n", res) : "No data.");
        }
    }

    public static void T11()
    {
        Print("Task 11:", ConsoleColor.Yellow);

        using (EstateContext ctx = new())
        {
            var res = ctx.EstateObjects.Select(x => new
                                        {
                                            x.Id,
                                            x.Cost,
                                            x.Address,
                                            AreaName = ctx.Areas.Single(y => y.Id == x.AreaId).Name
                                        })
                                        .Join(ctx.Sales, estate => estate.Id, sale => sale.EstateObjectId,
                                        (estate, sale) => new
                                        {
                                            estate.Id,
                                            estate.Cost,
                                            SaleCost = sale.Cost,
                                            estate.Address,
                                            estate.AreaName
                                        })
                                        .Where(x => (x.SaleCost - x.Cost) <= (int)(x.SaleCost*0.2))
                                        .Select(x => $"Address: {x.Address}, Area: {x.AreaName}, ID: {x.Id}");

            Print(string.Join("\n", res));
        }
    }

    public static void T12()
    {
        Print("Task 12:", ConsoleColor.Yellow);
        
        using(EstateContext ctx = new())
        {
            var mid_cost = ctx.EstateObjects.Select(x => new
            {
                x.Square,
                x.Cost
            });

            var objects_midCost = Math.Round(mid_cost.Select(x => x.Cost).Sum() / mid_cost.Select(x => x.Square).Sum());

            var res = ctx.EstateObjects.Select(x => new
            {
                MidCost = MathF.Round(x.Cost / x.Square),
                x.Address
            })
                .Where(x => x.MidCost < objects_midCost)
                .Select(x => $"{x.Address}, Cost: {x.MidCost}");

            Print(string.Join("\n", res));
        }
    }

    public static void T13()
    {
        Print("Task 13:", ConsoleColor.Yellow);

        using(EstateContext ctx = new())
        {
            var res = ctx.Sales.GroupBy(x => x.RealtorId)
                               .Select(x => new
                               {
                                   x.Key,
                                   SalesCount = x.Select(x => x.EstateObjectId).Count()
                               })
                               .Where(x => x.SalesCount == 0)
                               .Select(x => new
                               {
                                   RealtorObj = ctx.Realtors.Single(y => y.Id == x.Key)
                               })
                               .Select(x => $"YOU ARE FIRED!: {x.RealtorObj.Surname}::{x.RealtorObj.Name}::{x.RealtorObj.Patronim}")
                               .ToList();

            Print(res.Count == 0 ? "No one is fired!" : string.Join("\n", res));
        }
    }

    public static void T14()
    {
        Print("Task 14:", ConsoleColor.Yellow);

        using (EstateContext ctx = new())
        {
            int currentYear = DateTime.Now.Year,
                previousYear = currentYear - 1;

            var salesPreviousYear = ctx.Sales
                .Where(s => s.SaleDate.Year == previousYear)
                .GroupBy(s => ctx.EstateObjects.Single(x => x.Id == s.EstateObjectId).AreaId)
                .Select(g => new
                {
                    AreaId = g.Key,
                    TotalSales = g.Count()
                })
                .ToDictionary(x => x.AreaId, x => x.TotalSales);

            var salesCurrentYear = ctx.Sales
                .Where(s => s.SaleDate.Year == currentYear)
                .GroupBy(s => ctx.EstateObjects.Single(x => x.Id == s.EstateObjectId).AreaId)
                .Select(g => new
                {
                    AreaId = g.Key,
                    TotalSales = g.Count()
                })
                .ToDictionary(x => x.AreaId, x => x.TotalSales);

            Print($"Area\t2023\t2024\tDiff in %", ConsoleColor.DarkYellow);
            foreach (var areaId in salesPreviousYear.Keys.Union(salesCurrentYear.Keys))
            {
                var areaName = ctx.Areas.FirstOrDefault(a => a.Id == areaId)?.Name ?? "Unknown";
                var salesPrev = salesPreviousYear.GetValueOrDefault(areaId, 0);
                var salesCurr = salesCurrentYear.GetValueOrDefault(areaId, 0);
                var differencePercent = salesPrev != 0 ? (salesCurr - salesPrev) * 100.0 / salesPrev : 0;

                Print($"{areaName}\t\t{salesPrev}\t{salesCurr}\t{differencePercent:F2}%", ConsoleColor.Cyan);
            }
        }
    }

    public static void T15()
    {
        Print("Task 15:", ConsoleColor.Yellow);

        Print("Type ID of estate:");
        long specifiedEstateObjectId = long.Parse(Console.ReadLine()!.Trim());

        using (EstateContext ctx = new())
        {
            var averageGrades = ctx.Grades
                .Where(g => g.EstateObjectId == specifiedEstateObjectId)
                .GroupBy(g => g.CriteriaId)
                .Select(g => new
                {
                    CriteriaId = g.Key,
                    AverageGrade = g.Average(x => x.GradeValue)
                })
                .ToList();

            Console.WriteLine("Criteria\tAverage Grade\tText");
            foreach (var averageGrade in averageGrades)
            {
                var criterionName = ctx.Criterias.FirstOrDefault(c => c.Id == averageGrade.CriteriaId)?.Name ?? "Unknown";
                var equivalentText = GetEquivalentText(averageGrade.AverageGrade);

                Console.WriteLine($"{criterionName}\t\t{averageGrade.AverageGrade:F1} из 5\t\t{equivalentText}");
            }
        }
    }

    static string GetEquivalentText(double averageGrade)
    {
        if (averageGrade >= 90)
            return "превосходно";
        else if (averageGrade >= 80)
            return "очень хорошо";
        else if (averageGrade >= 70)
            return "хорошо";
        else if (averageGrade >= 60)
            return "удовлетворительно";
        else
            return "неудовлетворительно";
    }
}
