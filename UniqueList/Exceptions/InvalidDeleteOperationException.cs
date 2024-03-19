namespace Exceptions;

public class InvalidDeleteOperationException : SystemException
{
    public InvalidDeleteOperationException(string? message) : base(message)
    {
        
    }
}
