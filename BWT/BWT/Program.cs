Console.WriteLine("""
    1 - Encode string by BWT
    2 - Decode string after BWT
    """);

switch (Console.ReadLine())
{
    case "1":
    {
            Console.WriteLine("Enter your string: ");
            var inputString = Console.ReadLine();

            var (BWTString, originalStringIndex) = BWT.Transform(inputString);
            Console.WriteLine($"Encoded string: {BWTString}\nIndex of the original string: {originalStringIndex}");
            break;
    }
    case "2":
    {
            Console.WriteLine("Enter string after BWT and its index: ");
            var parameters = Console.ReadLine();

            var stringArray = parameters.Split(' ');
            if (stringArray.Length != 2)
            {
                return -2;
            }

            if (!Int32.TryParse(stringArray[1], out var lastIndex))
            {
                Console.WriteLine("Invalid number input");
                return -2;
            }

            Console.WriteLine($"Decoded string: {BWT.ReverseTransform(stringArray[0], lastIndex)}");

            break;
    }
    default:
    {
            Console.WriteLine("Incorrect number option");
            return -2;
    }
}
return 0;