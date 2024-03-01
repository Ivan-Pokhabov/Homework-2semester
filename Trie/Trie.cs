using System.Dynamic;

namespace Trie;

/// <summary>
/// Trie, type of k-ary search tree used for storing and searching a specific key from a set.
/// </summary>
public class Trie
{
    private readonly TrieVertex root = new ();

    /// <summary>
    /// Gets number of words in trie.
    /// </summary>
    public int Size { get; private set; }

    /// <summary>
    /// Function that adds word to trie.
    /// </summary>
    /// <param name="element">Word that we adds.</param>
    /// <returns>True if word wasn't in trie else false.</returns>
    /// <exception cref="ArgumentNullException">Word can't be null.</exception>
    public bool Add(string element)
    {
        if (element == null)
        {
            throw new ArgumentNullException(nameof(element), "Can't be null");
        }

        TrieVertex current = this.root;
        ++current.PrefixCount;

        foreach (var symbol in element)
        {
            if (!current.Childrens.ContainsKey(symbol))
            {
                current.Childrens.Add(symbol, new TrieVertex());
            }

            current = current.Childrens[symbol];
            ++current.PrefixCount;
        }

        if (current.IsTerminal)
        {
            return false;
        }

        ++this.Size;
        current.IsTerminal = true;
        return true;
    }

    /// <summary>
    /// Function that check is word in trie or not.
    /// </summary>
    /// <param name="element">Word that we check.</param>
    /// <returns>True if word in trie else false.</returns>
    /// <exception cref="ArgumentNullException">Word can't be null.</exception>
    public bool Contains(string element)
    {
        if (element == null)
        {
            throw new ArgumentNullException(nameof(element), "Can't be null");
        }

        TrieVertex current = this.root;

        foreach (var symbol in element)
        {
            if (!current.Childrens.ContainsKey(symbol))
            {
                return false;
            }
        }

        return current.IsTerminal;
    }

    /// <summary>
    /// Function of deleting word from trie.
    /// </summary>
    /// <param name="element">Word that we should delete.</param>
    /// <returns>True if word was in trie else false.</returns>
    /// <exception cref="ArgumentNullException">Word can't be null.</exception>
    public bool Remove(string element)
    {
        if (element == null)
        {
            throw new ArgumentNullException(nameof(element), "Can't be null");
        }

        if (!this.Contains(element))
        {
            return false;
        }

        --this.Size;

        TrieVertex current = this.root;
        --current.PrefixCount;

        foreach (var symbol in element)
        {
            if (current.Childrens[symbol].PrefixCount == 1)
            {
                current.Childrens.Remove(symbol);
                return true;
            }

            current = current.Childrens[symbol];
            --current.PrefixCount;
        }

        current.IsTerminal = false;
        return true;
    }

    /// <summary>
    /// Function of checking how many words in trie starts with some prefix.
    /// </summary>
    /// <param name="prefix">Prefix that we check.</param>
    /// <returns>Number of words in trie that starts with this prefix.</returns>
    /// <exception cref="ArgumentNullException">Prefix can't be null.</exception>
    public int HowManyStartsWithPrefix(string prefix)
    {
        if (prefix == null)
        {
            throw new ArgumentNullException(nameof(prefix), "Can't be null");
        }

        TrieVertex current = this.root;

        foreach (var symbol in prefix)
        {
            if (!current.Childrens.ContainsKey(symbol))
            {
                return 0;
            }
        }

        return current.PrefixCount;
    }

    private class TrieVertex
    {
        /// <summary>
        /// Gets or sets a value indicating whether true if path to this vertex is a word in Trie else false.
        /// </summary>
        public bool IsTerminal { get; set; }

        /// <summary>
        /// Gets or sets number of words in Trie that starts with that prefix.
        /// </summary>
        public int PrefixCount { get; set; }

        /// <summary>
        /// Container of edges to childrens.
        /// </summary>
        public readonly Dictionary<char, TrieVertex> Childrens = new ();
    }
}