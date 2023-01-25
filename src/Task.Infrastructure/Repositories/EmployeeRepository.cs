using Task.App.DataBaseContext;
using Task.App.Entities;

namespace Task.App.Repositories;

public class EmployeeRepository: RepositoryBase<Employee>
{
    public EmployeeRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}