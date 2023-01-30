using System.Reflection;
using System.Text.Json;

namespace Task.Service.Util;

public class CsvFileStreamParser: ICsvFileStreamParser
{
    public async Task<List<T>> Parse<T>(Stream stream, bool ignoreFirstRow = true) where T: class
    {
        using StreamReader streamReader = new StreamReader(stream);
        if (ignoreFirstRow)
            streamReader.ReadLine();
        PropertyInfo[] propertyInfo = typeof(T).GetProperties();
        //Exclude 0 index of item for Id. Due to Id gives by ef core 
        propertyInfo = propertyInfo.TakeLast(propertyInfo.Length - 1).ToArray();
        List<T> result = new List<T>();
        while (!streamReader.EndOfStream)
        {
            string? rowContent = streamReader.ReadLine();
            
            if (rowContent is null)
                continue;
            
            T entity = Activator.CreateInstance<T>();
            
            var splittedContent = rowContent.Split(',');
            
            for (int i = 0; i <= propertyInfo.Count() && i < splittedContent.Length; i++)
                propertyInfo[i].SetValue(entity, splittedContent[i]);
           
            result.Add(entity);
        }
        return result;
    }
}