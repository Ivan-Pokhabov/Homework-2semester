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

IGraph maxSpanningTree;
try
{
    maxSpanningTree = MaxSpanningTreeMaker.MakeAlgorithmPrima(graph);
}
catch (GraphNotConnectedException e)
{
    Console.WriteLine(e.Message);
    return -2;
}

GraphWriter.WriteGraph(maxSpanningTree, args[1]);
return 0;