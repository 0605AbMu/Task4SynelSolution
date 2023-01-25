using System.Reflection;
using System.Runtime.InteropServices.JavaScript;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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
        ImportDataset(modelBuilder.Entity<Employee>());
    }
    private void ImportDataset(EntityTypeBuilder builder)
    {
        string filePath = Path.Join(Environment.CurrentDirectory, "asset", "dataset.csv");
        if (!File.Exists(filePath))
            return;
        using var stream = new StreamReader(File.OpenRead(filePath));
        stream.ReadLine();
        while (!stream.EndOfStream)
        {
            var data = stream
                .ReadLine()
                .Split(",").ToList();
            builder
                .HasData(new Employee()
                {
                    Id = Guid.NewGuid(),
                    Payroll = data[0],
                    Forename = data[1],
                    Surname = data[2],
                    DateOfBirth = data[3],
                    Telephone = data[4],
                    Mobile = data[5],
                    Address = data[6],
                    Address2 = data[7],
                    PostCode = data[8],
                    Email = data[9],
                    StartDate = data[10]
                });
        }
    }
}