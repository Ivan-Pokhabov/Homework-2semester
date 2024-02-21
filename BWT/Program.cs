using System.Linq.Expressions;
using System.Numerics;
using System.Reflection.Metadata;

int[] SuffixArrayBuild(string str)
{
    int[] shifts = new int[str.Length];
    int[] countingSortHelper = new int[str.Length];
    for (int i = 0; i < str.Length; i++)
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
        foreach(var element in countingSortArray)
        {
            int lastShiftNumber = -1;
            foreach(var (currentShiftNumber, currentShiftPosition) in element)
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
    int originalStringIndex = -1;
    for(int i = 0; i < str.Length; ++i)
    {
        if (sortedShifts[i] == 0)
        {
            originalStringIndex = i + 1;
            BWTString.Append(str[str.Length - 1].ToString());
        }
        else
        {
            BWTString.Append(str[sortedShifts[i] - 1].ToString());
        }
    }

    return (BWTString.ToString(), originalStringIndex);
}

var (str, i) = BWT("ABACABA");
Console.WriteLine($"{str}, {i}");