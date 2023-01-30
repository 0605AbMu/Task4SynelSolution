namespace Task.Service.Util;

public interface ICsvFileStreamParser
{
    Task<List<T>> Parse<T>(Stream stream, bool ignoreFirstRow = true) where T: class;
}