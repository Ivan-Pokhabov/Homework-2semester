using System.Data.SqlTypes;
using Trie;

namespace LZW;

static class LZWDecoder
{
    public static void Decode()
    {
        var code = File.ReadAllText("test.txt");
        var codes = code.Split(" ");

        var dictionary = new Dictionary<int, List<byte>> ();

        for (var i = 0; i < 256; ++i)
        {
            dictionary.Add(i, new List<byte> {(byte)i});
        }

        var currentEncodeNumber = 256;
        var encode = new List<byte> ();

        for (var i = 0; i < codes.Length - 1; ++i)
        {
            encode.AddRange(dictionary[int.Parse(codes[i])]);

            var newCodeSequence = new List<byte> ();
            newCodeSequence.AddRange(dictionary[int.Parse(codes[i])]);

            newCodeSequence.Add(dictionary[int.Parse(codes[i + 1])][0]);

            dictionary.Add(currentEncodeNumber, newCodeSequence);
            
            ++currentEncodeNumber;
        }

        encode.AddRange(dictionary[int.Parse(codes[codes.Length - 1])]);

        File.WriteAllBytes("testoutput.txt", encode.ToArray());
    }
}