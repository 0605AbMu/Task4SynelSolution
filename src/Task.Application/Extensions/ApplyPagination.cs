using Microsoft.AspNetCore.Http;
using Task.Service.Metadata;

namespace Task.Service.Extensions;

public static class ApplyingPagination
{
    public static List<T> ApplyPagination<T>(
        this IQueryable<T> entities,
        PaginationMetada? paginationMetada)
        where T : class
    {
        return entities
            .Skip((paginationMetada.Current - 1) * paginationMetada.Size)
            .Take(paginationMetada.Size)
            .ToList();
    }
}