using Microsoft.EntityFrameworkCore;
using Task.App.DataBaseContext;
using Task.App.Repositories;

namespace Task.App.Extensions;

public static class ConfigureServices
{
    public static void ConfigureDataBase(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddDbContext<AppDbContext>(builder =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionString);
        });
    }

    public static void ConfigureRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<EmployeeRepository>();
    }
}