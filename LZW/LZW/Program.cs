using LZW;

if (args.Length < 2 || args.Length > 3)
{
    Console.WriteLine("Incorrect input, please try again");
    return;
}

if (!File.Exists(args[0]))
{
    Console.WriteLine("File with this pass doesn't exist");
    return;
}

bool withBWT = args.Length == 3 && args[2] == "--bwt";

if (args[1] == "--c")
{
    double result;

    try
    {
        result = LZWTransformer.Encode(args[0], withBWT);
    }
    catch (ArgumentException)
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
        LZWTransformer.Decode(args[0], withBWT);
    }
    catch (ArgumentException)
    {
        Console.WriteLine("Decoding failed");
        return;
    }

    Console.WriteLine($"Decoding was successfuly");
}
else
{
    Console.WriteLine("Incorrect input, please try again");
}