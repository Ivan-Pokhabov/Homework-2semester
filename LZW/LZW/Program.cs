using LZW;

if (args.Length != 2)
{
    Console.WriteLine("Incorrect input, please try again");
    return;
}

if (!File.Exists(args[0]))
{
    Console.WriteLine("File with this pass doesn't exist");
    return;
}

if (args[1] == "--c")
{
    double result;

    try
    {
        result = LZWTransformer.Encode(args[0]);
    }
    catch
    {
        Console.WriteLine("Encoding failed");
        return;
    }

    Console.WriteLine($"Encoding was successfuly. Compression ratio is {result}");
}
else if (args[1] == "--u")
{
    try
    {
        LZWTransformer.Decode(args[0]);
    }
    catch
    {
        Console.WriteLine("Decoding failed");
        return;
    }

    Console.WriteLine($"Decoding was successfuly.");
}