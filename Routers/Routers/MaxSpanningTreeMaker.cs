namespace Routers;

/// <summary>
/// Class of making minimal spanning tree from graph.
/// </summary>
public class MaxSpanningTreeMaker
{
    private IGraph graph;

    private bool[] visited;

    private int[] bestNeighbour;

    private int[] maxEdge;

    private IGraph maxSpanningTree;

    /// <summary>
    /// Initializes a new instance of the <see cref="MaxSpanningTreeMaker"/> class.
    /// </summary>
    /// <param name="initialGraph">Graph that we will transfromed into a tree.</param>
    public MaxSpanningTreeMaker(IGraph initialGraph)
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

    /// <summary>
    /// Algorithm Prima.
    /// </summary>
    /// <returns>(isSucessedAlgoritm, minSpaninngTee).</returns>
    public IGraph MakePrimsAlgorithm()
    {
        ArgumentNullException.ThrowIfNull(graph);

        for (var i = 0; i < graph.Size; ++i)
        {
            var currentVertex = FindVertexWithMaxEnteringEdge();

            UpdateMinSpammingTree(currentVertex);

            UpdateNeighbours(currentVertex);
        }

        return maxSpanningTree!;
    }

    private void UpdateNeighbours(int currentVertex)
    {
        foreach (var (neighbour, length) in graph.GetNeighbours(currentVertex))
        {
            if (length > maxEdge[neighbour])
            {
                maxEdge[neighbour] = length;
                bestNeighbour[neighbour] = currentVertex;
            }
        }
    }

    private void UpdateMinSpammingTree(int currentVertex)
    {
        if (currentVertex != 0)
        {
            if (maxEdge[currentVertex] == -1)
            {
                throw new GraphNotConnectedException("Graph is incorrect");
            }

            maxSpanningTree.AddEdge(bestNeighbour![currentVertex], currentVertex, maxEdge![currentVertex]);
        }
    }

    private int FindVertexWithMaxEnteringEdge()
    {
        var currentVertex = -1;
        for (var vertex = 0; vertex < graph.Size; ++vertex)
        {
            if (!visited[vertex] && (currentVertex == -1 || maxEdge[vertex] > maxEdge[currentVertex]))
            {
                currentVertex = vertex;
            }
        }

        visited[currentVertex] = true;

        return currentVertex;
    }
}