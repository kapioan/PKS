using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateSQL.PostgreSQL
{
    public class EstateContext : DbContext
    {
        public DbSet<TypeContext> Types { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<EstateObject> EstateObjects { get; set; }
        public DbSet<Criteria> Criterias { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Realtor> Realtors { get; set; }
        public DbSet<Sale> Sales { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql($"Host=localhost:5432;Database=postgres;Username=postgres;Password={File.ReadAllText("pw.txt")}");
    }

    [Table("Type")]
    public class TypeContext
    {
        [Column("id")]
        public long Id { get; set; }
        [Column("name")]
        public char? Name { get; set; }
    }

    [Table("Area")]
    public class Area
    {
        [Column("id")]
        public long Id { get; set; }
        [Column("name")]
        public string? Name { get; set; }
    }

    [Table("Material")]
    public class Material
    {
        [Column("id")]
        public long Id { get; set; }
        [Column("name")]
        public string? Name { get; set; }
    }

    [Table("EstateObject")]
    public class EstateObject
    {
        [Column("id")]
        public long Id { get; set; }
        [Column("area")]
        public long AreaId { get; set; }
        [Column("address")]
        public string? Address { get; set; }
        [Column("floor")]
        public int Floor { get; set; }
        [Column("rooms")]
        public int Rooms { get; set; }
        [Column("type")]
        public long TypeId { get; set; }
        [Column("status")]
        public bool Status { get; set; }
        [Column("cost")]
        public float Cost { get; set; }
        [Column("description")]
        public string? Description { get; set; }
        [Column("material")]
        public long MaterialId { get; set; }
        [Column("square")]
        public float Square { get; set; }
        [Column("announcement_date")]
        public DateTime AnnouncementDate { get; set; }
    }

    [Table("Criteria")]
    public class Criteria
    {
        [Column("id")]
        public long Id { get; set; }
        [Column("name")]
        public string? Name { get; set; }
    }

    [Table("Grade")]
    public class Grade
    {
        [Column("id")]
        public long Id { get; set; }
        [Column("object")]
        public long EstateObjectId { get; set; }
        [Column("grade_date")]
        public DateTime GradeDate { get; set; }
        [Column("criteria")]
        public long CriteriaId { get; set; }
        [Column("grade")]
        public short GradeValue { get; set; }
    }

    [Table("Realtor")]
    public class Realtor
    {
        [Column("id")]
        public long Id { get; set; }
        [Column("surname")]
        public string? Surname { get; set; }
        [Column("name")]
        public string? Name { get; set; }
        [Column("patronim")]
        public string? Patronim { get; set; }
        [Column("phone_number")]
        public string? PhoneNumber { get; set; }
    }

    [Table("Sale")]
    public class Sale
    {
        [Column("id")]
        public long Id { get; set; }
        [Column("object")]
        public long EstateObjectId { get; set; }
        [Column("sale_date")]
        public DateTime SaleDate { get; set; }
        [Column("realtor")]
        public long RealtorId { get; set; }
        [Column("cost")]
        public float Cost { get; set; }
    }
}
