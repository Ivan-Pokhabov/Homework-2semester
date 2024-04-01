namespace Routers;

/// <summary>
/// Class of making minimal spanning tree from graph.
/// </summary>
public static class MaxSpanningTreeMaker
{
    private static IGraph? graph;

    private static bool[]? visited;

    private static int[]? bestNeighbour;

    private static int[]? maxEdge;

    private static IGraph? maxSpanningTree;

    /// <summary>
    /// Algorithm Prima.
    /// </summary>
    /// <param name="initialGraph">IGraph graph.</param>
    /// <returns>(isSucessedAlgoritm, minSpaninngTee).</returns>
    public static IGraph MakeAlgorithmPrima(IGraph initialGraph)
    {
        ArgumentNullException.ThrowIfNull(initialGraph);

        InitializeParameters(initialGraph);

        for (var i = 0; i < graph!.Size; ++i)
        {
            var currentVertex = FindVertexWithMaxEnteringEdge();

            UpdateMinSpammingTree(currentVertex);

            UpdateNeighbours(currentVertex);
        }

        return maxSpanningTree!;
    }

    private static void UpdateNeighbours(int currentVertex)
    {
        foreach (var (neighbour, length) in graph!.GetNeighbours(currentVertex))
        {
            if (length > maxEdge![neighbour])
            {
                maxEdge[neighbour] = length;
                bestNeighbour![neighbour] = currentVertex;
            }
        }
    }

    private static void UpdateMinSpammingTree(int currentVertex)
    {
        if (currentVertex != 0)
        {
            if (maxEdge![currentVertex] == -1)
            {
                throw new GraphNotConnectedException("Graph is incorrect");
            }

            maxSpanningTree!.AddEdge(bestNeighbour![currentVertex], currentVertex, maxEdge![currentVertex]);
        }
    }

    private static int FindVertexWithMaxEnteringEdge()
    {
        var currentVertex = -1;
        for (var vertex = 0; vertex < graph!.Size; ++vertex)
        {
            if (!visited![vertex] && (currentVertex == -1 || maxEdge![vertex] > maxEdge![currentVertex]))
            {
                currentVertex = vertex;
            }
        }

        visited![currentVertex] = true;

        return currentVertex;
    }

    private static void InitializeParameters(IGraph initialGraph)
    {
        visited = new bool[initialGraph.Size];
        bestNeighbour = new int[initialGraph.Size];
        maxEdge = new int[initialGraph.Size];
        maxSpanningTree = new MyGraph(initialGraph.Size);
        for (var i = 1; i < initialGraph.Size; ++i)
        {
            maxEdge[i] = -1;
        }

        graph = initialGraph;
    }
}