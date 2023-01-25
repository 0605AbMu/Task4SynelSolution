using Microsoft.EntityFrameworkCore;

namespace Task.App.DataBaseContext;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
}