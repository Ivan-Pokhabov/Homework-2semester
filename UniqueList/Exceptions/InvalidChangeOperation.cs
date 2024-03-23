namespace Exceptions;

public class InvalidChangeOperationException(string? message) : SystemException(message)
{
}
