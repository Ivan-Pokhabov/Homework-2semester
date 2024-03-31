using System.Text;
using Routers;

var graph = new MyGraph(3);

graph.AddEdge(0, 1, -5);
graph.AddEdge(0, 1, -10);
graph.AddEdge(0, 2, -16);
graph.AddEdge(1, 2, -20);

var (isConnected, tree) = MinSpanningTreeMaker.MakeAlgorithmPrima(graph);

for (var i = 0; i < 3; ++i)
{
    var stringOutput = new StringBuilder($"{i + 1} : ");
    foreach (var (a, b) in tree.GetNeighbours(i))
    {
        if (a >= i)
        {
            stringOutput.Append($"{a + 1}({-b}) ");
        }
    }

    if (stringOutput.Length > $"{i + 1} : ".Length)
    {
        Console.WriteLine(stringOutput);
    }
}