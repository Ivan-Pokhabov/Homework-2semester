using LZW;

if (args.Length < 2 || args.Length > 3)
{
    Console.WriteLine("Incorrect input, please try again.");
    return;
}

if (!File.Exists(args[0]))
{
    Console.WriteLine("File with this pass doesn't exist.");
    return;
}

var withBWT = args is [_, _, "--bwt"];

if (args[1] == "--c")
{
    double encodeResult;
    double anotherEncodingVariantresult;

    try
    {
        anotherEncodingVariantresult = LZWTransformer.Encode(args[0], !withBWT);
        encodeResult = LZWTransformer.Encode(args[0], withBWT);
    }
    catch (ArgumentException e)
    {
        Console.WriteLine($"Encoding failed because of {e.Message}.");
        return;
    }

    var bwtInfluence = withBWT ? encodeResult / anotherEncodingVariantresult : anotherEncodingVariantresult / encodeResult;

    Console.WriteLine($"""
        Encoding was successfuly.
        Compression ratio is {encodeResult}.
        BWT influence is {bwtInfluence} (with bwt / without bwt).
        """);
}
else if (args[1] == "--u")
{
    try
    {
        LZWTransformer.Decode(args[0], withBWT);
    }
    catch (ArgumentException e)
    {
        Console.WriteLine($"Decoding failed because of {e.Message}.");
        return;
    }

    Console.WriteLine($"Decoding was successfuly.");
}
else
{
    Console.WriteLine("Incorrect input, please try again.");
}