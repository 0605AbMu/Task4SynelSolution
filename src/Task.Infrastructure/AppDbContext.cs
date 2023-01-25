using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Task.App.Entities;

namespace Task.App.DataBaseContext;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Employee>()
            .ToTable("Employees");
    }
}