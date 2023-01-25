using Microsoft.EntityFrameworkCore;
using Task.App.DataBaseContext;

namespace Task.App.Extensions;

public static class ConfigureApplication
{
    public static void ApplyMigration(this WebApplication webApplication)
    {
        Console.WriteLine("Applying migrations to db....");
        var scope = webApplication.Services.CreateScope();
        AppDbContext? dbContext = scope.ServiceProvider.GetService<AppDbContext>();
        if (dbContext is null)
        {
            throw new Exception("DbContext not found");
            Environment.Exit(1);
        }

        try
        {
            dbContext.Database.Migrate();
        }
        catch (Exception e)
        {
            throw new Exception("Migration error: " + e.Message);
            Environment.Exit(1);
        }
    }
}