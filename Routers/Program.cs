using Routers;

var graph = new MyGraph(3);

graph.AddEdge(0, 1, -5);
graph.AddEdge(0, 1, -10);
graph.AddEdge(0, 2, -16);
graph.AddEdge(1, 2, -20);


var tree = MinSpanningTreeMaker.MakeAlgorithmPrima(graph);

for (var i = 0; i < 3; ++i)
{
    Console.Write($"{i + 1} : ");
    foreach (var (a, b) in tree.GetNeighbours(i))
    {
        if (a < i)
        {
            continue;
        }
        
        Console.Write($"{a + 1}({-b}) ");
    }
    Console.WriteLine();
}