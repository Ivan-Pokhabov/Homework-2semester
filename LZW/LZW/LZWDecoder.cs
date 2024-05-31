using Containers;

namespace LZW;

/// <summary>
/// Class of decoding lzw transformed array of bytes.
/// </summary>
public static class LZWDecoder
{
    /// <summary>
    /// Decode transformed bytes.
    /// </summary>
    /// <param name="code">Array of transformed bytes.</param>
    /// <returns>Array of origignal bytes.</returns>
    public static byte[] Decode(byte[] code)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(code));

        var dictionary = new Dictionary<int, List<byte>>();

        const int ByteSize = 256;

        for (var i = 0; i < ByteSize; ++i)
        {
            dictionary.Add(i, [(byte)i]);
        }

        var currentEncodeNumber = 256;
        var encode = new DecodeIntContainer();

        foreach (var newByte in code)
        {
            if (currentEncodeNumber == encode.MaxInt)
            {
                ++encode.IntBitSize;
                encode.MaxInt <<= 1;
            }

            if (encode.Add(newByte))
            {
                ++currentEncodeNumber;
            }
        }

        var encodeInts = encode.GetIntArray();

        var decodeBytes = new List<byte>();
        currentEncodeNumber = 256;

        for (var i = 0; i < encodeInts.Length - 1; ++i)
        {
            decodeBytes.AddRange(dictionary[encodeInts[i]]);

            var newCodeSequence = new List<byte>();
            newCodeSequence.AddRange(dictionary[encodeInts[i]]);

            newCodeSequence.Add(
                !dictionary.ContainsKey(encodeInts[i + 1])
                ? newCodeSequence[0]
                : dictionary[encodeInts[i + 1]][0]);
            dictionary.Add(currentEncodeNumber, newCodeSequence);

            ++currentEncodeNumber;
        }

        return [.. decodeBytes];
    }
}