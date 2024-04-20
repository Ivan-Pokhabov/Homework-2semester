using Routers;

if (args.Length != 2)
{
    Console.Error.WriteLine("Invalid number of arguments");
    return -1;
}

try
{
    MakeTopology.Build(args[0], args[1]);
}
catch (Exception e) when (e is FileNotFoundException || e is ArgumentException || e is GraphNotConnectedException)
{
    Console.Error.WriteLine(e.Message);
    return -2;
}

return 0;