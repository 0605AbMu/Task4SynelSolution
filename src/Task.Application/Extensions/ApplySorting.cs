using Task.Service.Metadata;

namespace Task.Service.Extensions;

public static class Sorting
{
    public static List<T> ApplySorting<T>(this List<T> entities, SortingMetadata<T>? sortingMetadata)
        where T : class
    {
        if (sortingMetadata is null)
            return entities;
        var asc = sortingMetadata.Asc;
        var desc = sortingMetadata.Desc;

        if (sortingMetadata.Asc.Count == 0 && sortingMetadata.Desc.Count == 0)
            return entities;

        IOrderedEnumerable<T> result = null;

        if (asc?.Count > 0)
        {
            result = entities
                .OrderBy(entity =>
                    entity.GetType().GetProperty(asc[0])?.GetValue(entity, null));
            for (int i = 1; i < asc.Count; i++)
            {
                int index = i;
                result = result
                    .ThenBy(entity =>
                        entity.GetType().GetProperty(asc[index])?.GetValue(entity, null));
            }
        }


        if (desc?.Count > 0)
        {
            result = (result ?? entities.AsEnumerable())
                .OrderByDescending(entity =>
                    entity.GetType().GetProperty(desc[0])?.GetValue(entity, null));
            for (int i = 1; i < desc.Count; i++)
            {
                int index = i;
                result = result
                    .ThenByDescending(entity =>
                        entity.GetType().GetProperty(desc[index])?.GetValue(entity, null));
            }
        }

        return result?.ToList() ?? entities;
    }
}