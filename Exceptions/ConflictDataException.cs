namespace CostMSWebAPI;

public class ConflictDataException : Exception
{
    public ConflictDataException()
    {
    }

    public ConflictDataException(string message) : base(message)
    {
    }

    public ConflictDataException(string message, Exception inner) : base(message, inner)
    {
    }
}
