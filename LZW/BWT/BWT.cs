namespace BWT;

/// <summary>
/// Class of BWT transform and reverse transform.
/// </summary>
public static class BWTTransformer
{
    /// <summary>
    /// Make direct bwt transform.
    /// </summary>
    /// <param name="bytes">Byte array for transform.</param>
    /// <returns>New byte array and number of original byte array.</returns>
    public static (byte[], int) Transform(byte[] bytes)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(bytes));

        var BWTArray = new List<byte>();
        var sortedShifts = SuffixArrayBuild(bytes);

        var originalStringIndex = 1;

        for (var i = 0; i < bytes.Length; ++i)
        {
            if (sortedShifts[i] == 0)
            {
                originalStringIndex = i + 1;
                BWTArray.Add(bytes[^1]);
            }
            else
            {
                BWTArray.Add(bytes[sortedShifts[i] - 1]);
            }
        }

        return (BWTArray.ToArray(), originalStringIndex);
    }

    /// <summary>
    /// Function of reverse bwt transform.
    /// </summary>
    /// <param name="BWTArray">Array after direct bwt.</param>
    /// <param name="originalArrayIndex">index of original array.</param>
    /// <returns>Original array.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Index should be more than 0 and less than length of array + 1.</exception>
    public static byte[] ReverseTransform(byte[] BWTArray, int originalArrayIndex)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(BWTArray));

        if (originalArrayIndex <= 0 || originalArrayIndex > BWTArray.Length)
        {
            throw new ArgumentOutOfRangeException(paramName: nameof(originalArrayIndex));
        }

        var shifts = Enumerable.Range(0, BWTArray.Length).ToArray();
        Array.Sort(shifts, (int a, int b) =>
        {
            if (BWTArray[a] < BWTArray[b])
            {
                return -1;
            }

            return BWTArray[a] > BWTArray[b] ? 1 : a.CompareTo(b);
        });

        var originalString = new List<byte>();
        var currentSymbolIndex = shifts[originalArrayIndex - 1];
        for (var i = 0; i < BWTArray.Length; ++i)
        {
            Console.WriteLine(currentSymbolIndex);
            originalString.Add(BWTArray[currentSymbolIndex]);
            currentSymbolIndex = shifts[currentSymbolIndex];
        }

        return originalString.ToArray();
    }

    private static int[] SuffixArrayBuild(byte[] str)
    {
        var countingSortHelper = new int[str.Length];
        var shifts = Enumerable.Range(0, str.Length).ToArray();
        Array.Sort(shifts, (int a, int b) => str[a].CompareTo(str[b]));

        for (var i = 1; i < str.Length; ++i)
        {
            countingSortHelper[shifts[i]] = countingSortHelper[shifts[i - 1]];
            if (str[shifts[i]] != str[shifts[i - 1]])
            {
                ++countingSortHelper[shifts[i]];
            }
        }

        for (var k = 1; k < str.Length; k *= 2)
        {
            var countingSortArray = new List<(int, int)>[str.Length];
            for (var h = 0; h < str.Length; ++h)
            {
                countingSortArray[h] = new List<(int, int)>();
            }

            foreach (var secondHalf in shifts)
            {
                var firstHalf = secondHalf - k;
                if (firstHalf < 0)
                {
                    firstHalf += str.Length;
                }

                countingSortArray[countingSortHelper[firstHalf]].Add((countingSortHelper[secondHalf], firstHalf));
            }

            var countNewShift = 0;
            var shiftIndex = 0;
            foreach (var element in countingSortArray)
            {
                var lastShiftNumber = -1;
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
}
