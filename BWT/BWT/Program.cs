Console.WriteLine("Program of BWT:\n1 - Direct BWT\n2 - Reverse BWT");

switch(Console.ReadLine())
{
    case "1":
    {
        Console.WriteLine("Enter string: ");

        var inputString = Console.ReadLine();
        
        var (resultString, originalStringIndex) = BWT.Transform(inputString);

        Console.WriteLine($"Result of direct BWT: {resultString}, {originalStringIndex}");

        break;
    }

    case "2":
    {
        Console.WriteLine("Enter result of direct BWT(separated by space):");

        var input = Console.ReadLine();

        var BWTResult = input.Split();
        if (BWTResult.Length != 2)
        {
            Console.WriteLine("You can't add not 2 arguments");
            return;
        }

        if (!int.TryParse(BWTResult[1], out int index))
        {
            Console.WriteLine("Second variable should be number");
            return;
        }

        Console.WriteLine($"Result of reverse transformation: {BWT.ReverseTransform(BWTResult[0], index)}");
        break;
    }

    default:
    {
        Console.WriteLine("Invalid input");
        break;
    }
}