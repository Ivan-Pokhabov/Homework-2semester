using LZW;

if (args.Length != 3)
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
        result = LZWTransformer.Encode(args[0], args[2] == "--bwt");
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
        LZWTransformer.Decode(args[0], args[2] == "--bwt");
    }
    catch
    {
        Console.WriteLine("Decoding failed");
        return;
    }

    Console.WriteLine($"Decoding was successfuly.");
}