using Task.App.Entities;
using Task.Service.Metadata;
namespace Task.App.ViewModel;

public class HomeViewModel
{
    public Exception? Error { get; set; }
    public Employee? EditableEmployee { get; set; }
    public string? Message { get; set; }
    public List<Employee>? Data { get; set; }
    public List<string> Columns { get; set; }
    public SearchingMetadata? Searching { get; set; }
    public PaginationMetada? Pagination { get; set; }
    public SortingMetadata<Employee>? Sorting { get; set; }
    public HomeViewModel(Type type)
    {
        this.Columns = type.GetProperties().Select(info => info.Name).ToList();
    }

    public string? nextUrl { get; set; }
}