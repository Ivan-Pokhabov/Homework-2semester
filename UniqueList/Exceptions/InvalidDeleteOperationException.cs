namespace Exceptions;

public class InvalidDeleteOperationException(string? message) : SystemException(message)
{
}
