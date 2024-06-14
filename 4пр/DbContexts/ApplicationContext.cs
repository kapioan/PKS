
using Microsoft.EntityFrameworkCore;

using PKS4.Models.Data;

namespace PKS4.DbContexts;

public class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Message> Messages { get; set; }

    public ApplicationContext() { }

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Username=postgres;Password=admin;Database=postgres;Pooling=true;SSL Mode=Prefer;Trust Server Certificate=true;");
}
