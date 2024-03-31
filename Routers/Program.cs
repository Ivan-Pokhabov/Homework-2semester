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

var output = new StringBuilder();
for (var i = 0; i < maxSpanningTree.Size; ++i)
{
    var stringOutput = new StringBuilder($"{i + 1} : ");
    foreach (var (neighbour, edgeWeight) in maxSpanningTree.GetNeighbours(i))
    {
        if (neighbour >= i)
        {
            stringOutput.Append($"{neighbour + 1} ({edgeWeight}) ");
        }
    }

    if (stringOutput.Length > $"{i + 1} : ".Length)
    {
        output.AppendLine(stringOutput.ToString());
    }
}

File.WriteAllText(args[1], output.ToString());
return 0;