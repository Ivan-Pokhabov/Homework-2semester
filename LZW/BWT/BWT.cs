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

        var bWTList = new List<byte>();
        var sortedShifts = SuffixArrayBuild(bytes);

        var originalStringIndex = 1;

        for (var i = 0; i < bytes.Length; ++i)
        {
            if (sortedShifts[i] == 0)
            {
                originalStringIndex = i + 1;
                bWTList.Add(bytes[^1]);
            }
            else
            {
                bWTList.Add(bytes[sortedShifts[i] - 1]);
            }
        }

        return (bWTList.ToArray(), originalStringIndex);
    }

    /// <summary>
    /// Function of reverse bwt transform.
    /// </summary>
    /// <param name="bWTArray">Array after direct bwt.</param>
    /// <param name="originalArrayIndex">index of original array.</param>
    /// <returns>Original array.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Index should be more than 0 and less than length of array + 1.</exception>
    public static byte[] ReverseTransform(byte[] bWTArray, int originalArrayIndex)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(bWTArray));

        if (originalArrayIndex <= 0 || originalArrayIndex > bWTArray.Length)
        {
            throw new ArgumentOutOfRangeException(paramName: nameof(originalArrayIndex));
        }

        var shifts = Enumerable.Range(0, bWTArray.Length).ToArray();
        Array.Sort(shifts, (a, b) => bWTArray[a] == bWTArray[b] ? a.CompareTo(b) : bWTArray[a] - bWTArray[b]);

        var originalString = new List<byte>();
        var currentSymbolIndex = shifts[originalArrayIndex - 1];
        for (var i = 0; i < bWTArray.Length; ++i)
        {
            originalString.Add(bWTArray[currentSymbolIndex]);
            currentSymbolIndex = shifts[currentSymbolIndex];
        }

        return [.. originalString];
    }

    private static int[] SuffixArrayBuild(byte[] byteArray)
    {
        var shiftsDifferenceArray = new int[byteArray.Length];
        var shifts = Enumerable.Range(0, byteArray.Length).ToArray();
        Array.Sort(shifts, (a, b) => byteArray[a].CompareTo(byteArray[b]));

        for (var i = 1; i < byteArray.Length; ++i)
        {
            shiftsDifferenceArray[shifts[i]] = shiftsDifferenceArray[shifts[i - 1]];
            if (byteArray[shifts[i]] != byteArray[shifts[i - 1]])
            {
                ++shiftsDifferenceArray[shifts[i]];
            }
        }

        for (var k = 1; k < byteArray.Length; k *= 2)
        {
            var countingSortArray = Enumerable.Range(0, byteArray.Length)
            .Select(_ => new List<(int, int)>())
            .ToArray();

            foreach (var secondHalf in shifts)
            {
                var firstHalf = secondHalf - k;
                if (firstHalf < 0)
                {
                    firstHalf += byteArray.Length;
                }

                countingSortArray[shiftsDifferenceArray[firstHalf]].Add((shiftsDifferenceArray[secondHalf], firstHalf));
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

                    shiftsDifferenceArray[currentShiftPosition] = countNewShift - 1;
                }
            }

            if (countNewShift == byteArray.Length)
            {
                break;
            }
        }

        return shifts;
    }
}
