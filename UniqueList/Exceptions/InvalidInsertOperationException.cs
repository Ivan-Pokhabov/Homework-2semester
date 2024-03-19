namespace Exceptions;

public class InvalidInsertOperationException : SystemException
{
    public InvalidInsertOperationException(string? message) : base(message)
    {
        
    }
}
