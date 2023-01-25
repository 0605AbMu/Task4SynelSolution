using Task.App.Entities;

namespace Task.Service.Services;

public interface IEmployeeService
{
    ValueTask<IQueryable<Employee>> RetrieveAll();
    ValueTask<Employee> RetrieveById(Guid id);
    ValueTask<Employee> Create(Employee employee);
    ValueTask<Employee> Modify(Employee employee);
    ValueTask<Employee> Remove(Guid id);
}