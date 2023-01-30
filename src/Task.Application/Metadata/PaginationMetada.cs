namespace Task.Service.Metadata;

public class PaginationMetada
{
    public int Size { get; set; } = 10;
    public int Current { get; set; } = 1;
    public int CountOfAll { get; set; }
    public int NumberOfPages { get => (int)Math.Ceiling((CountOfAll * 1.0) / (Size * 1.0)); }
    public bool HasNext
    {
        get => Current < NumberOfPages;
    }
    
    public bool HasPrevious
    {
        get => Current > 1;
    }
}