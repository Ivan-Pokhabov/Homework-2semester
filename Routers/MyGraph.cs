namespace Routers;

public class MyGraph : IGraph
{
    public MyGraph(int size)
    {
        graph = [];

        for (var i = 0; i < size; ++i)
        {
            graph.Add([]);
        }

        Size = size;
    }
    private readonly List<List<(int, int)>> graph;

    public int Size { get; }

    public void AddEdge(int a, int b, int length)
    {
        graph[a].Add((b, length));
        graph[b].Add((a, length));
    }

    public (int, int)[] GetNeighbours(int a)
        => [.. graph[a]];
}