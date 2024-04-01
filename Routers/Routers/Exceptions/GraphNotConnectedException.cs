namespace Routers;

/// <summary>
/// Exception inform that graph is not connected.
/// </summary>
/// <param name="message">Message.</param>
public class GraphNotConnectedException(string message): Exception(message)
{
}