namespace Exceptions;

public class InvalidChangeOperationException : SystemException
{
    public InvalidChangeOperationException(string? message) : base(message)
    {
        
    }
}
