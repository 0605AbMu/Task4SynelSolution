using Task.App.Entities;

namespace Task.Application.Services;

public interface IEmployeeService
{
    ValueTask<IQueryable<Employee>> RetrieveAllAsync();
    ValueTask<Employee> RetrieveByIdAsync(Guid id);
    ValueTask<Employee> CreateAsync(Employee employee);
    ValueTask<Employee> ModifyAsync(Employee employee);
    ValueTask<Employee> RemoveAsync(Guid id);
}