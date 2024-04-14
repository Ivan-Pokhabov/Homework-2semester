namespace Routers;

/// <summary>
/// Class of reliazation IGraph interface.
/// </summary>
public class MyGraph : IGraph
{
    private readonly List<List<(int, int)>> graph;

    /// <summary>
    /// Initializes a new instance of the <see cref="MyGraph"/> class.
    /// </summary>
    /// <param name="size">int number of vertexes in graph.</param>
    public MyGraph(int size)
    {
        graph = [];

        for (var i = 0; i < size; ++i)
        {
            graph.Add([]);
        }

        Size = size;
    }

    /// <inheritdoc/>
    public int Size { get; }

    /// <inheritdoc/>
    public void AddEdge(int firstVertex, int secondVertex, int length)
    {
        if (firstVertex < 0 || secondVertex < 0 || firstVertex >= Size || secondVertex >= Size)
        {
            throw new ArgumentException("Vertex should be non-negative and less than size of graph");
        }

        graph[firstVertex].Add((secondVertex, length));
        graph[secondVertex].Add((firstVertex, length));
    }

    /// <inheritdoc/>
    public (int, int)[] GetNeighbours(int vertex)
        => (vertex < 0 || vertex >= Size) ? throw new ArgumentException("Vertex should be non-negative and less than size of graph") : [.. graph[vertex]];
}