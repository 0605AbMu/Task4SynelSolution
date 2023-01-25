using System.Text;
using Task.App.Exceptions;

namespace Task.App.Util;

public class CSVFileParser
{
    private readonly int countOfColumns = 1;
    private const string separator = ",";

    public CSVFileParser(int countOfColumns)
    {
        this.countOfColumns = countOfColumns;
    }
    private string ParseStringFromStream(Stream stream)
    {
        if (int.MaxValue < stream.Length)
            throw new Exception("File size very large");
        byte[] buffer = new byte[stream.Length];
        stream
            .ReadExactly(buffer, 0, (int)stream.Length);
        return Encoding
            .UTF8
            .GetString(buffer);
    }
    public async Task<IEnumerable<IEnumerable<string>>> ParseFromStream(Stream stream)
    {
        string csvFileContent = ParseStringFromStream(stream: stream);

        return csvFileContent
            .Split(separator)
            .Chunk(countOfColumns);
    }
}