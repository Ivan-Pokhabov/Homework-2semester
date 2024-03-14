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
    public void Contains_WithAddNewWord_ShouldReturnCorrectResult(string[] words)
    {
        foreach (var word in words)
        {
            trie.Add(word);

            Assert.That(trie.Contains(word));
        }
    }

    [TestCaseSource(nameof(TestCasesWithDifferentWords))]
    public void Size_WithAddAndRemoveDifferentWords_ShouldReturnCorrectResult(string[] words)
    {
        var expectedSize = 0;

        foreach (var word in words)
        {
            Assert.That(trie.Size == expectedSize);

            trie.Add(word);
            trie.Add(word);
            ++expectedSize;
        }

        foreach (var word in words)
        {
            Assert.That(trie.Size == expectedSize);

            trie.Remove(word);
            trie.Remove(word);
            --expectedSize;
        }

        Assert.Pass();
    }
    
    [TestCaseSource(nameof(TestCasesWithDifferentWords))]
    public void Contains_WithRemoveDifferentWords_ShouldReturnCorrectResult(string[] words)
    {
        foreach (var word in words)
        {
            trie.Add(word);
        }

        foreach (var word in words)
        {
            trie.Remove(word);

            Assert.That(!trie.Contains(word));
        }
    }

    [TestCaseSource(nameof(TestCasesWithDifferentWords))]
    public void AddAndRemove_WithDifferentWords_ShouldReturnCorrectResult(string[] words)
    {
        foreach (var word in words)
        {
            Assert.That(!trie.Remove(word) && trie.Add(word));
        }

        foreach (var word in words)
        {
            Assert.That(!trie.Add(word) && trie.Remove(word));
        }

        Assert.Pass();
    }

    [Test]
    public void HowManyStartsWithPrefix_ShouldReturnCorrectResult()
    {
        string[] words = new string[] {"Matmeh", "mama", "mat", "aaa", "aa"};

        foreach (var word in words)
        {
            trie.Add(word);
        }

        Assert.That(trie.HowManyStartsWithPrefix("Matm") == 1 && trie.HowManyStartsWithPrefix("ma") == 2 && 
            trie.HowManyStartsWithPrefix("a") == 2 && trie.HowManyStartsWithPrefix("aaa") == 1);
    }

    [Test]
    public void HowManyStartsWithPrefix_WithRemove_ShouldReturnCorrectResult()
    {
        string[] words = new string[] {"Matmeh", "mama", "mat", "aaa", "aa"};

        foreach (var word in words)
        {
            trie.Add(word);
        }

        trie.Remove("aa");
        trie.Remove("mama");
        trie.Remove("Matmeh");

        Assert.That(trie.HowManyStartsWithPrefix("Matm") == 0 && trie.HowManyStartsWithPrefix("ma") == 1 && 
            trie.HowManyStartsWithPrefix("a") == 1 && trie.HowManyStartsWithPrefix("aaa") == 1);
    }

    [Test]
    public void Add_WithNull_ShouldThrowException()
    {
        Assert.Throws<ArgumentNullException>(() => trie.Add(null));
    }

    [Test]
    public void Remove_WithNull_ShouldThrowException()
    {
        Assert.Throws<ArgumentNullException>(() => trie.Remove(null));
    }

    [Test]
    public void Contains_WithNull_ShouldThrowException()
    {
        Assert.Throws<ArgumentNullException>(() => trie.Contains(null));
    }

    [Test]
    public void HowManyStartsWithPrefix_WithNull_ShouldThrowException()
    {
        Assert.Throws<ArgumentNullException>(() => trie.HowManyStartsWithPrefix(null));
    }
}