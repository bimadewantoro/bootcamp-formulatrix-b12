using DotnetApiPostgres.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetApiPostgres.Api;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    public DbSet<Person> People { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>()
            .Property(p => p.Gender)
            .HasConversion<string>();
    }
}
