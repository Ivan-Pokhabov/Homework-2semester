namespace Routers;

public interface IGraph
{
    void AddEdge(int a, int b, int length);

    (int, int)[] GetNeighbours(int a);

    int Size { get; }
}