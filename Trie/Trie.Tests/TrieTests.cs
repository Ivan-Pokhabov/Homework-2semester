using System.Data;
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

    [TestCase("xuy"), TestCase(""), TestCase("aaa")]
    public void TrieAddNewWordAndCheckContainsWorkedCorrectly(string word)
    {
        trie.Add(word);
        Assert.IsTrue(trie.Contains(word));
    }
}