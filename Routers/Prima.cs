namespace Routers;

public static class MinSpanningTreeMaker
{
    public static IGraph MakeAlgorithmPrima(IGraph graph)
    {
        var visited = new bool[graph.Size];

        var minEdge = new int[graph.Size];
        for (var i = 0; i < graph.Size; ++i)
        {
            minEdge[i] = (int)1e9;
        }

        minEdge[0] = 0;

        var bestEdge= new int[graph.Size];
        

        for (var i = 0; i < graph.Size; ++i)
        {
            var currentVertex = -1;
            for (var vertex = 0; vertex < graph.Size; ++vertex)
            {
                if (!visited[vertex] && (currentVertex == -1 || minEdge[vertex] < minEdge[currentVertex]))
                {
                    currentVertex = vertex;
                }
            }
                
            visited[currentVertex] = true;

            foreach (var (neighbour, length) in graph.GetNeighbours(currentVertex))
            {
                if (length < minEdge[neighbour])
                {
                    minEdge[neighbour] = length;
                    bestEdge[neighbour] = currentVertex;
                }
            }
        }

        var minSpanningTree = new MyGraph();

        for (int i = 0; i < graph.Size; ++i)
        {
            minSpanningTree.AddEdge(i, bestEdge[i], minEdge[i]);
        }

        return minSpanningTree;
    }
}