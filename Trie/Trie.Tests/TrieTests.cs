using System.Data;
using System.Runtime.CompilerServices;
using Trie;

namespace Trie.Tests;

public class TrieTests
{
    private Trie trie;

    [SetUp]
    public void SetUp()
    {
        trie = new ();
    }

    public static IEnumerable<string[]> TestCasesWithRepeatedWords
    {
        get
        {
            yield return new string[] {"aaa", "", "heh", "", "he"};
            yield return new string[] {"matmeh", "ahaha", "ahaha", "matmeh", "m", "ah", "люблюОРГ"};
        }
    }

    public static IEnumerable<string[]> TestCasesWithDifferentWords
    {
        get
        {
            yield return new string[] {"aaa", "", "heh", "he", "слово на русском, да еще и со знаками препинания???"};
            yield return new string[] {"matmeh", "ahaha", "matmeh:)", "m", "ah"};
        }
    }
    
    [TestCaseSource(nameof(TestCasesWithRepeatedWords)), TestCaseSource(nameof(TestCasesWithDifferentWords))]
    public void TrieAddNewWordAndContainsWorkedCorrectly(string[] words)
    {
        foreach (var word in words)
        {
            trie.Add(word);

            if (!trie.Contains(word))
            {
                Assert.Fail();
            }
        }
        Assert.Pass();
    }

    [TestCaseSource(nameof(TestCasesWithDifferentWords))]
    public void TrieAddAndRemovesWithDifferentWordsKeepSizeCorrectly(string[] words)
    {
        var expectedSize = 0;

        foreach (var word in words)
        {
            if (trie.Size != expectedSize)
            {
                Assert.Fail();
            }

            trie.Add(word);
            ++expectedSize;
        }

        foreach (var word in words)
        {
            if (trie.Size != expectedSize)
            {
                Assert.Fail();
            }

            trie.Remove(word);
            --expectedSize;
        }

        Assert.Pass();
    }
}