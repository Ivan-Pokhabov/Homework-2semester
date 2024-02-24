public static class BWT
{
    private static int[] SuffixArrayBuild(string str)
    {
        var countingSortHelper = new int[str.Length];
        var shifts = Enumerable.Range(0, str.Length).ToArray();
        Array.Sort(shifts, (int a, int b) => str[a].CompareTo(str[b]));

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

    public static (string, int) Transform(string str)
    {
        var BWTString = new System.Text.StringBuilder();
        int[] sortedShifts = SuffixArrayBuild(str);
        int originalStringIndex = 1;
        for (int i = 0; i < str.Length; ++i)
        {
            if (sortedShifts[i] == 0)
            {
                originalStringIndex = i + 1;
                BWTString.Append(str[^1]);
            }
            else
            {
                BWTString.Append(str[sortedShifts[i] - 1]);
            }
        }
        return (BWTString.ToString(), originalStringIndex);
    }

    public static string ReverseTransform(string BWTString, int originalStringIndex)
    {   
        if (BWTString.Length == 0 && originalStringIndex == 1)
        {
            return string.Empty;
        }
        if (originalStringIndex <= 0 || originalStringIndex > BWTString.Length)
        {
            Console.WriteLine("Incorrect input");
            return string.Empty;
        }
        
        var shifts = Enumerable.Range(0, BWTString.Length).ToArray();
        Array.Sort(shifts, (int a, int b) => BWTString[a].CompareTo(BWTString[b]));

        var originalString = new System.Text.StringBuilder();
        int currentSymbolIndex = shifts[originalStringIndex - 1];
        for (int i = 0; i < BWTString.Length; ++i)
        {
            originalString.Append(BWTString[currentSymbolIndex]);
            currentSymbolIndex = shifts[currentSymbolIndex];
        }
        return originalString.ToString();
    }
}