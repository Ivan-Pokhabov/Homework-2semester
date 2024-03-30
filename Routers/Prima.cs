using System.Dynamic;

namespace Routers;

public static class MinSpanningTreeMaker
{
    public static IGraph MakeAlgorithmPrima(IGraph graph)
    {
        var visited = new bool[graph.Size];
        var bestEdge= new int[graph.Size];

        var minEdge = new int[graph.Size];
        for (var i = 1; i < graph.Size; ++i)
        {
            minEdge[i] = (int)1e9;
        }

        var minSpanningTree = new MyGraph(graph.Size);
        
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

            if (currentVertex != 0)
            {
                minSpanningTree.AddEdge(bestEdge[currentVertex], currentVertex, minEdge[currentVertex]);
            }


            foreach (var (neighbour, length) in graph.GetNeighbours(currentVertex))
            {
                if (length < minEdge[neighbour])
                {
                    minEdge[neighbour] = length;
                    bestEdge[neighbour] = currentVertex;
                }
            }
        }

        return minSpanningTree;
    }
}