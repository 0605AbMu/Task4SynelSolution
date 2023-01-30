using Task.App.Entities;
using Task.Service.Metadata;

namespace Task.App.Model;

public class HomeControllerFromQueryModel
{
    public SortingMetadata<Employee>? sorting { get; set; } = new SortingMetadata<Employee>();
    public SearchingMetadata? searching {get; set;}
    public PaginationMetada? pagination {get; set;}
}