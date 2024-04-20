namespace Routers;

/// <summary>
/// Interface of undirected graph.
/// </summary>
public interface IGraph
{
    /// <summary>
    /// Gets size of Graph.
    /// </summary>
    int Size { get; }

    /// <summary>
    /// Method of adding undirect edge to graph.
    /// </summary>
    /// <param name="firstVertex">Int number of first vertex.</param>
    /// <param name="secondVertex">Int number of second vertex.</param>
    /// <param name="length">Weight of edge.</param>
    void AddEdge(int firstVertex, int secondVertex, int length);

    /// <summary>
    /// Get array of neighbours and edges length.
    /// </summary>
    /// <param name="vertex">Int number of vertex.</param>
    /// <returns>Array of ints.</returns>
    (int, int)[] GetNeighbours(int vertex);
}