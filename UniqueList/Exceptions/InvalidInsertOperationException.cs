namespace Exceptions;

public class InvalidInsertOperationException(string? message) : SystemException(message)
{
}
