if (args.Length != 1)
{
    Console.Write("Argument error. Use \"dotnet run help\".");
    return;
}

if (args[0] == "help")
{
    Console.WriteLine("dotnet run FilePath will calculate infix expression");
    return;
}

var parseTree = new ParseTree.ParseTree();

if (!File.Exists(args[0]))
{
    Console.Write("File not found.");
    return;
}

var expression = File.ReadAllText(args[0]);
try
{
    parseTree.BuildTree(expression);
}
catch (Exception e) when (e is ArgumentException || e is ArgumentNullException)
{
    Console.Write(e.Message);
    return;
}

double result;
try
{
    result = parseTree.CalclulateExpression();
}
catch (Exception e) when (e is ArgumentException || e is InvalidOperationException)
{
    Console.Write(e.Message);
    return;
}

try
{
    parseTree.Print();
    Console.Write($"= {result}\n");
}
catch (InvalidOperationException e)
{
    Console.Write(e.Message);
    return;
}
