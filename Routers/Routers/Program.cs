using System.Text;
using Routers;

if (args.Length != 2)
{
    Console.Error.WriteLine("Invalid number of arguments");
    return -1;
}

IGraph graph;
try
{
    graph = GraphReader.ReadGraph(args[0]);
}
catch (FileNotFoundException e)
{
    Console.Error.WriteLine(e.Message);
    return -1;
}

var (isCorrect, maxSpanningTree) = MaxSpanningTreeMaker.MakeAlgorithmPrima(graph);

if (!isCorrect)
{
    Console.WriteLine("Graph wasn't connected");
    return -2;
}

GraphWriter.WriteGraph(maxSpanningTree, args[1]);
return 0;