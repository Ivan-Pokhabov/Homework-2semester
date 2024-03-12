using Containers;

namespace LZW;

public static class LZWDecoder
{
    public static byte[] Decode(byte[] code)
    {
        var dictionary = new Dictionary<int, List<byte>> ();

        for (var i = 0; i < 256; ++i)
        {
            dictionary.Add(i, new List<byte> {(byte)i});
        }

        var currentEncodeNumber = 256;
        var encode = new DecodeIntContainer ();

        for (var i = 0; i < code.Length; ++i)
        {
            if (currentEncodeNumber == encode.MaxSymbols)
            {
                ++encode.SymbolBitSize;
                encode.MaxSymbols <<= 1;
            }
            
            if (encode.Add(code[i]))
            {
                ++currentEncodeNumber;
            }
        }

        encode.AddLastInt();

        var encodeInts = encode.GetIntArray();

        var decodeBytes = new List<byte> ();
        currentEncodeNumber = 256;

        for (var i = 0; i < encodeInts.Length - 1; ++i)
        {
            decodeBytes.AddRange(dictionary[encodeInts[i]]);

            var newCodeSequence = new List<byte> ();
            newCodeSequence.AddRange(dictionary[encodeInts[i]]);

            if (!dictionary.ContainsKey(encodeInts[i + 1]))
            {
                newCodeSequence.Add(newCodeSequence[0]);

                dictionary.Add(currentEncodeNumber, newCodeSequence);
            }
            else
            {
                newCodeSequence.Add(dictionary[encodeInts[i + 1]][0]);

                dictionary.Add(currentEncodeNumber, newCodeSequence);
            }
            
            ++currentEncodeNumber;
        }

        return decodeBytes.ToArray();
    }
}