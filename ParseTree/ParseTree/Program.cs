// Copyright (c) Ivan-Pokhabov. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

if (args.Length != 1)
{
    Console.Write("Argument error. Use \"dotnet run help\".");
    return;
}

if (args[0] == "help")
{
    Console.WriteLine("<dotnet run --project=projectPath FilePath> will calculate infix expression");
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
catch (ArgumentException e)
{
    Console.Write(e.Message);
    return;
}

double result;
try
{
    result = parseTree.CalculateTree();
}
catch (Exception e) when (e is ArgumentException || e is InvalidOperationException)
{
    Console.Write(e.Message);
    return;
}

Console.Write($"{parseTree.Print()}= {result}\n");
