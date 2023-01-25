namespace Task.App.Exceptions;

public class FileInvalidFormatException: Exception
{
    public FileInvalidFormatException(): base("File format invalid. Please give .csv file")
    {
        
    }
}