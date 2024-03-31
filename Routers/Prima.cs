namespace Routers;

/// <summary>
/// Class of making minimal spanning tree from graph.
/// </summary>
public static class MinSpanningTreeMaker
{
    private static IGraph? graph;

    private static bool[]? visited;

    private static int[]? bestNeighbour;

    private static int[]? minEdge;

    private static int visitedCount = 0;

    private static IGraph? minSpanningTree;

    /// <summary>
    /// Algorithm Prima.
    /// </summary>
    /// <param name="initialGraph">IGraph graph.</param>
    /// <returns>(isSucessedAlgoritm, minSpaninngTee).</returns>
    public static (bool, IGraph) MakeAlgorithmPrima(IGraph initialGraph)
    {
        InitializeParameters(initialGraph);

        for (var i = 0; i < graph!.Size; ++i)
        {
            var currentVertex = FindVertexWithMinimalEnteringEdge();

            UpdateMinSpammingTree(currentVertex);

            UpdateNeighbours(currentVertex);
        }

        return (visitedCount == graph.Size, minSpanningTree!);
    }

    private static void UpdateNeighbours(int currentVertex)
    {
        foreach (var (neighbour, length) in graph!.GetNeighbours(currentVertex))
        {
            if (length < minEdge![neighbour])
            {
                minEdge[neighbour] = length;
                bestNeighbour![neighbour] = currentVertex;
            }
        }
    }

    private static void UpdateMinSpammingTree(int currentVertex)
    {
        if (currentVertex != 0)
        {
            minSpanningTree!.AddEdge(bestNeighbour![currentVertex], currentVertex, minEdge![currentVertex]);
        }
    }

    private static int FindVertexWithMinimalEnteringEdge()
    {
        var currentVertex = -1;
        for (var vertex = 0; vertex < graph!.Size; ++vertex)
        {
            if (!visited![vertex] && (currentVertex == -1 || minEdge![vertex] < minEdge![currentVertex]))
            {
                currentVertex = vertex;
            }
        }

        visited![currentVertex] = true;
        ++visitedCount;

        return currentVertex;
    }

    private static void InitializeParameters(IGraph initialGraph)
    {
        visited = new bool[initialGraph.Size];
        bestNeighbour = new int[initialGraph.Size];
        minEdge = new int[initialGraph.Size];
        minSpanningTree = new MyGraph(initialGraph.Size);
        graph = initialGraph;
    }
}