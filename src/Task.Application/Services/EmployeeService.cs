using Task.App.Entities;
using Task.App.Repositories;

namespace Task.Application.Services;

public class EmployeeService: IEmployeeService
{
    private readonly EmployeeRepository employeeRepository;
    public EmployeeService(EmployeeRepository employeeRepository)
    {
        this.employeeRepository = employeeRepository;
    }

    public async ValueTask<IQueryable<Employee>> RetrieveAllAsync()
    {
        return await this.employeeRepository
            .FindAllAsync(null);
    }

    public async ValueTask<Employee> RetrieveByIdAsync(Guid id)
    {
        return await this.employeeRepository
            .FindByExpressionAsync(expression: (employee => employee.Id.Equals(id)));
    }

    public async ValueTask<Employee> CreateAsync(Employee employee)
    {
        return await this.employeeRepository
            .CreateAsync(employee);
    }

    public async ValueTask<Employee> ModifyAsync(Employee employee)
    {
        return await this.employeeRepository
            .UpdateAsync(employee);
    }

    public async ValueTask<Employee> RemoveAsync(Guid id)
    {
        Employee employee = await this.RetrieveByIdAsync(id);
        if (employee is null)
            throw new Exception("User not found");

        return await this.employeeRepository
            .DeleteAsync(employee);
    }
    
    
}