namespace CostMSWebAPI.Exceptions;

public class IncorrectDataException : Exception
{
    public IncorrectDataException()
    {
    }

    public IncorrectDataException(string message) : base(message)
    {
    }

    public IncorrectDataException(string message, Exception inner) : base(message, inner)
    {
    }
}
