using Trie;
using Containers;

namespace LZW;

/// <summary>
/// Class of encoding array of bytes by LZW.
/// </summary>
public static class LZWEncoder
{
    /// <summary>
    /// Encode array of bytes.
    /// </summary>
    /// <param name="file">Array of original bytes.</param>
    /// <returns>Array of transformed bytes.</returns>
    public static byte[] Encode(byte[] file)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(file));

        var dictionary = new Trie.Trie();

        const int ByteSize = 256;

        for (var i = 0; i < ByteSize; ++i)
        {
            dictionary.Add([(byte)i], i);
        }

        var currentCodeSequenceNumber = ByteSize;
        var code = new EncodeByteContainer();
        var currentEncodeSequence = new List<byte>();

        foreach (var bytes in file)
        {
            currentEncodeSequence.Add(bytes);

            if (dictionary.GetValue(currentEncodeSequence) == -1)
            {
                if (code.MaxSymbols == dictionary.Size)
                {
                    ++code.SymbolBitSize;
                    code.MaxSymbols <<= 1;
                }

                dictionary.Add(currentEncodeSequence, currentCodeSequenceNumber);
                ++currentCodeSequenceNumber;

                currentEncodeSequence.RemoveAt(currentEncodeSequence.Count - 1);

                code.Add(dictionary.GetValue(currentEncodeSequence));

                currentEncodeSequence.Clear();
                currentEncodeSequence.Add(bytes);
            }
        }

        code.Add(dictionary.GetValue(currentEncodeSequence));

        return code.GetByteArray();
    }
}