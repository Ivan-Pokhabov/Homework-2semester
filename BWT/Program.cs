bool Test1()
{
    string str = "abcd";
    var (BWTString, originalStringIndex) = BWT.Transform(str);
    return String.Equals("dabc", BWTString) && originalStringIndex == 1 && String.Equals(str, BWT.ReverseTransfom(BWTString, originalStringIndex));
}

bool Test2()
{
    string str = "ABACABA";
    var (BWTString, originalStringIndex) = BWT.Transform(str);
    return String.Equals("BCABAAA", BWTString) && originalStringIndex == 3 && String.Equals(str, BWT.ReverseTransfom(BWTString, originalStringIndex));
}

bool Test3()
{
    string str = "";
    var (BWTString, originalStringIndex) = BWT.Transform(str);
    return String.Equals("", BWTString) && originalStringIndex == 1 && String.Equals(str, BWT.ReverseTransfom(BWTString, originalStringIndex));
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

Console.WriteLine("Enter your string for BWT:");
var (BWTString, originalStringIndex) = BWT.Transform(Console.ReadLine());
Console.WriteLine($"Result: {BWTString}, {originalStringIndex}");
Console.WriteLine($"Result of reverse transfomation: {BWT.ReverseTransfom(BWTString, originalStringIndex)}");
return 0;