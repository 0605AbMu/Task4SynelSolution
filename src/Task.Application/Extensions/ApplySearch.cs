using System.Reflection;
using Task.Service.Metadata;

namespace Task.Service.Extensions;

public static class ApplySearch
{
    public static IQueryable<T> ApplySearching<T>(
        this IQueryable<T> entities,
        SearchingMetadata? searchingMetadata)
        where T : class
    {
        if (searchingMetadata is null)
            return entities;

        var property = typeof(T).GetProperty(searchingMetadata.ParamName ?? "");

        if (property is null)
            return entities;
        
        return entities
            .Where((entity =>
                    property
                    .GetValue(entity)
                    .ToString()
                    .ToLowerInvariant()
                    .Contains(
                        searchingMetadata.Search
                            .ToLowerInvariant())));
    }
}