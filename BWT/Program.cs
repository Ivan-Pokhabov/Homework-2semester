using System.Linq.Expressions;
using System.Numerics;
using System.Reflection.Metadata;

int[] SuffixArrayBuild(string str)
{
    int[] shifts = new int[str.Length];
    int[] countingSortHelper = new int[str.Length];
    for (int i = 0; i < str.Length; ++i)
    {
        shifts[i] = i;
    }
    Array.Sort(shifts, (int a, int b) => { if (str[a] < str[b]) return -1; if (str[a] > str[b]) return 1; return 0; });
    for (int i = 1; i < str.Length; ++i)
    {
        countingSortHelper[shifts[i]] = countingSortHelper[shifts[i - 1]];
        if ((str[shifts[i]] != str[shifts[i - 1]]))
        {
            ++countingSortHelper[shifts[i]];
        }
    }
    for (int k = 1; k < str.Length; k *= 2)
    {
        List<(int, int)>[] countingSortArray = new List<(int, int)>[str.Length];
        for(int h = 0; h < str.Length; ++h)
        {
            countingSortArray[h] = new List<(int, int)>();
        }
        foreach(var secondHalf in shifts)
        {
            int firstHalf = secondHalf - k;
            if (firstHalf < 0)
            {
                firstHalf += str.Length;
            }
            countingSortArray[countingSortHelper[firstHalf]].Add((countingSortHelper[secondHalf], firstHalf));
        }
        int countNewShift = 0;
        int shiftIndex = 0;
        foreach (var element in countingSortArray)
        {
            int lastShiftNumber = -1;
            foreach (var (currentShiftNumber, currentShiftPosition) in element)
            {
                shifts[shiftIndex++] = currentShiftPosition;
                if (currentShiftNumber != lastShiftNumber)
                {
                    ++countNewShift;
                    lastShiftNumber = currentShiftNumber;
                }
                countingSortHelper[currentShiftPosition] = countNewShift - 1;
            }
        }
        if (countNewShift == str.Length)
        {
            break;
        }
    }
    return shifts;
}

(string, int) BWT(string str)
{
    var BWTString = new System.Text.StringBuilder();
    int[] sortedShifts = SuffixArrayBuild(str);
    int originalStringIndex = 1;
    for (int i = 0; i < str.Length; ++i)
    {
        if (sortedShifts[i] == 0)
        {
            originalStringIndex = i + 1;
            BWTString.Append(str[str.Length - 1]);
        }
        else
        {
            BWTString.Append(str[sortedShifts[i] - 1]);
        }
    }
    return (BWTString.ToString(), originalStringIndex);
}

string ReverseBWT(string BWTString, int originalStringIndex)
{
    if (BWTString.Length == 0)
    {
        return "";
    }
    int[] shifts = new int[BWTString.Length];
    for (int i = 0; i < BWTString.Length; ++i)
    {
        shifts[i] = i;
    }
    Array.Sort(shifts, (int a, int b) => { if (BWTString[a] < BWTString[b]) return -1; if (BWTString[a] > BWTString[b]) return 1; return 0; });
    var originalString = new System.Text.StringBuilder();
    int currentSymbolIndex = shifts[originalStringIndex - 1];
    for (int i = 0; i < BWTString.Length; ++i)
    {
        originalString.Append(BWTString[currentSymbolIndex]);
        currentSymbolIndex = shifts[currentSymbolIndex];
    }
    return originalString.ToString();
}

bool Test1()
{
    string str = "abcd";
    var (BWTString, originalStringIndex) = BWT(str);
    if (!String.Equals("dabc", BWTString) || originalStringIndex != 1 || !String.Equals(str, ReverseBWT(BWTString, originalStringIndex)))
    {
        return false;
    }
    return true;
}

bool Test2()
{
    string str = "ABACABA";
    var (BWTString, originalStringIndex) = BWT(str);
    if (!String.Equals("BCABAAA", BWTString) || originalStringIndex != 3 || !String.Equals(str, ReverseBWT(BWTString, originalStringIndex)))
    {
        return false;
    }
    return true;
}

bool Test3()
{
    string str = "";
    var (BWTString, originalStringIndex) = BWT(str);
    if (!String.Equals("", BWTString) || originalStringIndex != 1 || !String.Equals(str, ReverseBWT(BWTString, originalStringIndex)))
    {
        return false;
    }
    return true;
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
    Environment.Exit(-1);
}

var (BWTString, originalStringIndex) = BWT("ABACABA");
Console.WriteLine($"{BWTString}, {originalStringIndex}");
Console.WriteLine(ReverseBWT(BWTString, originalStringIndex));