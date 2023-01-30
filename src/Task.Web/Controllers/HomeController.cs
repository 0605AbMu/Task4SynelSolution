using Microsoft.AspNetCore.Mvc;
using Task.App.Entities;
using Task.App.Repositories;
using Task.App.ViewModel;
using Task.Service.Extensions;
using Task.Service.Metadata;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Filters;
using Task.Service.Util;

namespace Task.App.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> logger;
    private readonly EmployeeRepository employeeRepository;
    private readonly ICsvFileStreamParser csvFileStreamParser;

    public HomeController(ILogger<HomeController> logger, EmployeeRepository employeeRepository,
        ICsvFileStreamParser csvFileStreamParser)
    {
        this.logger = logger;
        this.employeeRepository = employeeRepository;
        this.csvFileStreamParser = csvFileStreamParser;
    }

    [HttpGet]
    public async ValueTask<ViewResult> Index(
        string? sorting,
        string? pagination,
        string? searching,
        string? message)
    {
        ViewData["Error"] = TempData["Error"] ??  null;
        ViewData["Message"] = TempData["Message"] ?? null;
        return View(await MakeHomeViewModel(
            message, sorting, pagination, searching));
    }

    [HttpGet("/edit/{id:guid}")]
    public async ValueTask<ViewResult> Index(
        Guid id,
        string? next)
    {
        Employee employee = await employeeRepository
            .FindByExpressionAsync(employee => employee.Id == id);
        if (employee is null)
        {
            ViewData["Error"] = "User not found";
            return View(
                "Index",
                await MakeHomeViewModel());
        }

        return View(await MakeHomeViewModel(
            null,
            null,
            null,
            null,
            editableData: employee,
            nextUrl: next));
    }

    [HttpPost("edit/{id:guid}")]
    public async ValueTask<IActionResult> UpdateEmployee(Employee employee, string? next)
    {
        var updatedEmployee = await employeeRepository.UpdateAsync(employee);
        await employeeRepository.SaveChangesAsync();
        ViewData["Message"] = "Successfully updated " + $"{updatedEmployee.Forename} {updatedEmployee.Surname}";
        return View("Index", await MakeHomeViewModel());
    }

    [HttpGet("delete/{id:guid}")]
    public async ValueTask<IActionResult> DeleteEmployee(Guid id, string? next)
    {
        Employee employee = await employeeRepository
            .FindByExpressionAsync(employee => employee.Id == id);
        if (employee is null)
        {
            TempData["Error"] = "User not found";
            return View(
                "Index",
                await MakeHomeViewModel());
        }

        var deletedEmployee = await employeeRepository
            .DeleteAsync(employee);
        TempData["Message"] = "Successfully deleted " + $"{deletedEmployee.Forename} {deletedEmployee.Surname}";
        await employeeRepository.SaveChangesAsync();
        return RedirectToActionPermanent("Index");
    }

    [HttpPost]
    public async ValueTask<IActionResult> Index([FromForm] IFormFile file)
    {
        if (file.ContentType != "text/csv")
        {
            TempData["Error"] = "invalid file mimetype";
            return RedirectToAction("Index");
        }

        var entities = await csvFileStreamParser.Parse<Employee>(file.OpenReadStream());

        await employeeRepository.CreateRangeAsync(entities.AsQueryable());
        await employeeRepository.SaveChangesAsync();
        TempData["Message"] = $"Successfully imported data count: {entities.Count}";
        return RedirectToAction("Index");
    }

    public override void OnActionExecuted(ActionExecutedContext context)
    {
        base.OnActionExecuted(context);
    }
    
    private T QueryDeserialize<T>(string query)
    {
        return JsonSerializer.Deserialize<T>(query, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
    }

    private async ValueTask<HomeViewModel> MakeHomeViewModel(
        string? message = null,
        string? sorting = null,
        string? pagination = null,
        string? searching = null,
        Employee? editableData = null,
        string? nextUrl = null)
    {
        SortingMetadata<Employee> sortingMetadata = default;
        if (sorting != null)
            sortingMetadata = QueryDeserialize<SortingMetadata<Employee>>(sorting);
        else
        {
            sortingMetadata = new SortingMetadata<Employee>();
            sortingMetadata.Toggle(employee => employee.Forename);
        }

        PaginationMetada paginationMetada = null;

        if (pagination is not null)
            paginationMetada = QueryDeserialize<PaginationMetada>(pagination);

        SearchingMetadata searchingMetadata = null;
        if (searching is not null)
            searchingMetadata = QueryDeserialize<SearchingMetadata>(searching);

        var SearchedEmployees = (await this.employeeRepository
                .FindAllAsync())
            .ApplySearching(searchingMetadata);

        if (pagination is null)
        {
            paginationMetada = new PaginationMetada()
            {
                CountOfAll = SearchedEmployees.Count()
            };
        }

        var employees = SearchedEmployees
            .ApplyPagination(paginationMetada)
            .ApplySorting(sortingMetadata);

        return (new HomeViewModel(typeof(Employee))
        {
            Message = message,
            Data = employees,
            Searching = searchingMetadata,
            Pagination = paginationMetada,
            Sorting = sortingMetadata,
            EditableEmployee = editableData,
            nextUrl = nextUrl
        });
    }
}