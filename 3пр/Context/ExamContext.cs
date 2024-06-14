using ExamSQL.Models;
using ExamSQL.Resources;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace ExamSQL.Context;

public class ExamContext : DbContext
{
    public DbSet<Achievement> Achievements { get; set; }
    public DbSet<Enrollee> Enrollees { get; set; }
    public DbSet<EnrolleeAchievement> EnrolleeAchievements { get; set; }
    public DbSet<EnrolleeSubject> EnrolleeSubjects { get; set; }
    public DbSet<Program> Programs { get; set; }
    public DbSet<ProgramEnrollee> ProgramEnrollees { get; set; }
    public DbSet<ProgramSubject> ProgramSubjects { get; set; }
    public DbSet<Subject> Subjects { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string password = GetPasswordFromFile();
        optionsBuilder.UseNpgsql($"Host=localhost;Database=postgres;Username=postgres;Password={password}");
    }

    private static string GetPasswordFromFile()
    {
        // Load the resource file
        var resourceManager = new ResourceManager("ExamSQL.Resources.Access", typeof(Access).Assembly);

        // Get the password from the resource file
        string? password = resourceManager.GetString("Password");

        if (string.IsNullOrEmpty(password))
        {
            throw new InvalidOperationException("Database password not found in resource file.");
        }

        return password;
    }
}
