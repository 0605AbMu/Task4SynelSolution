using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Task.Service.Metadata;

public class SortingMetadata<T>
{
    [JsonPropertyName("asc")]
    public List<string> Asc { get; set; }
    [JsonPropertyName("desc")]
    public List<string> Desc { get; set; }

    public SortingMetadata()
    {
        this.Asc = new List<string>();
        this.Desc = new List<string>();
    }
    public void Toggle(Expression<Func<T, object>> expression)
    {
        PropertyInfo propertyInfo = expression.GetPropertyAccess();
        if (propertyInfo is null)
            return;
        
        if (this.Asc.Find(item => item == propertyInfo.Name) != null)
        {
            this.Asc.Remove(propertyInfo.Name);
            this.Desc.Add(propertyInfo.Name);
        }
        else if (this.Desc.Find(item => item == propertyInfo.Name) != null)
        {
            this.Desc.Remove(propertyInfo.Name);
            this.Asc.Add(propertyInfo.Name);
        }
        else
        {
            this.Asc.Add(propertyInfo.Name);
        }
        
    }
}
