namespace Routers;

public class MyGraph : IGraph
{
    private readonly List<List<(int, int)>>? graph;

    private int size = 0;

    public int Size => size;

    public void AddEdge(int a, int b, int length)
    {
        graph[a].Add((b, length));
        ++size;
    }

    public (int, int)[] GetNeighbours(int a)
    {
        return [.. graph[a]];
    }
}