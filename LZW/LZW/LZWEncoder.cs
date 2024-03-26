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

        for (var i = 0; i < 256; ++i)
        {
            dictionary.Add(new List<byte> { (byte)i }, i);
        }

        var currentNumber = 256;
        var code = new EncodeByteContainer();
        var currentEncodeSequense = new List<byte>();

        foreach (var bytes in file)
        {
            currentEncodeSequense.Add(bytes);

            if (dictionary.GetValue(currentEncodeSequense) == -1)
            {
                if (code.MaxSymbols == dictionary.Size)
                {
                    ++code.SymbolBitSize;
                    code.MaxSymbols <<= 1;
                }

                dictionary.Add(currentEncodeSequense, currentNumber);
                ++currentNumber;

                currentEncodeSequense.RemoveAt(currentEncodeSequense.Count - 1);

                code.Add(dictionary.GetValue(currentEncodeSequense));

                currentEncodeSequense.Clear();
                currentEncodeSequense.Add(bytes);
            }
        }

        code.Add(dictionary.GetValue(currentEncodeSequense));

        return code.GetByteArray();
    }
}