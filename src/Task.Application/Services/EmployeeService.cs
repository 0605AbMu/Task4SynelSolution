using Task.App.Entities;
using Task.App.Repositories;

namespace Task.Service.Services;

public class EmployeeService: IEmployeeService
{
    private readonly EmployeeRepository employeeRepository;
    public EmployeeService(EmployeeRepository employeeRepository)
    {
        this.employeeRepository = employeeRepository;
    }

    public async ValueTask<IQueryable<Employee>> RetrieveAll()
    {
        return await this.employeeRepository
            .FindAllAsync(null);
    }

    public async ValueTask<Employee> RetrieveById(Guid id)
    {
        return await this.employeeRepository
            .FindByExpressionAsync(expression: (employee => employee.Id.Equals(id)));
    }

    public async ValueTask<Employee> Create(Employee employee)
    {
        return await this.employeeRepository
            .CreateAsync(employee);
    }

    public async ValueTask<Employee> Modify(Guid id, Employee employee)
    {
        return await this.employeeRepository
            .CreateAsync(employee);
    }

    public async ValueTask<Employee> Remove(Guid id)
    {
        Employee employee = await this.RetrieveById(id);
        if (employee is null)
            throw new Exception("User not found");

        return await this.employeeRepository
            .DeleteAsync(employee);
    }
}