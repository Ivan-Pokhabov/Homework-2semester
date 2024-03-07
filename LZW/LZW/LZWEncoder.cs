using System.Text;
using Trie;
namespace LZW;

static class LZWEncoder
{
    static public void Encode()
    {
        var text = File.ReadAllBytes("LZW.sln");

        File.WriteAllBytes("test.output", text);

        var dictionary = new Trie.Trie();

        for (var i = 0; i < 256; ++i)
        {
            dictionary.Add(new List<byte> {(byte)i}, i);
        }

        var currentNumber = 256;
        var code = new StringBuilder ();
        var currentEncodeSequense = new List<byte> ();

        foreach (var bytes in text)
        {
            currentEncodeSequense.Add(bytes);

            if (dictionary.GetValue(currentEncodeSequense) == -1)
            {
                
                dictionary.Add(currentEncodeSequense, currentNumber);
                ++currentNumber;

                currentEncodeSequense.RemoveAt(currentEncodeSequense.Count - 1);

                code.Append(dictionary.GetValue(currentEncodeSequense).ToString());
                code.Append(" ");

                currentEncodeSequense.Clear();
                currentEncodeSequense.Add(bytes);
            }
        }

        code.Append(dictionary.GetValue(currentEncodeSequense).ToString());

        File.WriteAllText("test.txt", code.ToString());
    }
}