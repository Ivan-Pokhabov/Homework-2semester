bool Test1()
{
    string str = "abcd";
    var (BWTString, originalStringIndex) = BWT.Transform(str);
    return String.Equals("dabc", BWTString) && originalStringIndex == 1 && String.Equals(str, BWT.ReverseTransform(BWTString, originalStringIndex));
}

bool Test2()
{
    string str = "ABACABA";
    var (BWTString, originalStringIndex) = BWT.Transform(str);
    return String.Equals("BCABAAA", BWTString) && originalStringIndex == 3 && String.Equals(str, BWT.ReverseTransform(BWTString, originalStringIndex));
}

bool Test3()
{
    string str = "";
    var (BWTString, originalStringIndex) = BWT.Transform(str);
    return String.Equals("", BWTString) && originalStringIndex == 1 && String.Equals(str, BWT.ReverseTransform(BWTString, originalStringIndex));
}

bool Test()
{
    bool[] testCases = {Test1(), Test2(), Test3()};
    bool passed = true;
    for (int i = 0; i < 3; ++i)
    {
        if (!testCases[i])
        {
            Console.WriteLine($"Program did not pass test {i + 1}");
            passed = false;
        }
    }
    return passed;
}

if (!Test())
{
    return -1;
}

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

            var stringArray = parameters.Split();
            if (parameters.Length != 2)
            {
                Console.WriteLine("Incorrect input");
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