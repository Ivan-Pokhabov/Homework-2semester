namespace Trie;

/// <summary>
/// Trie, type of k-ary search tree used for storing and searching a specific key from a set.
/// </summary>
public class Trie
{
    private readonly TrieVertex root = new (-1);

    /// <summary>
    /// Gets number of words in trie.
    /// </summary>
    public int Size => root.PrefixCount;

    /// <summary>
    /// Add byte list to trie.
    /// </summary>
    /// <param name="element">List of bytes.</param>
    /// <param name="value">Number of element.</param>
    /// <returns>True if word wasn't in list else false.</returns>
    /// <exception cref="ArgumentNullException">Can't be null.</exception>
    public bool Add(List<byte> element, int value)
    {
        ArgumentNullException.ThrowIfNull(element);

        if (Contains(element))
        {
            return false;
        }

        TrieVertex current = root;
        ++current.PrefixCount;

        foreach (var symbol in element)
        {
            if (!current.Childrens.ContainsKey(symbol))
            {
                current.Childrens.Add(symbol, new TrieVertex(value));
            }

            current = current.Childrens[symbol];
            ++current.PrefixCount;
        }

        current.IsTerminal = true;

        return true;
    }

    /// <summary>
    /// Function that check is word in trie or not.
    /// </summary>
    /// <param name="element">Word that we check.</param>
    /// <returns>True if word in trie else false.</returns>
    /// <exception cref="ArgumentNullException">Word can't be null.</exception>
    public bool Contains(List<byte> element)
    {
        ArgumentNullException.ThrowIfNull(element);

        TrieVertex current = root;

        foreach (var symbol in element)
        {
            if (!current.Childrens.ContainsKey(symbol))
            {
                return false;
            }

            current = current.Childrens[symbol];
        }

        return current.IsTerminal;
    }

    /// <summary>
    /// Function of deleting word from trie.
    /// </summary>
    /// <param name="element">Word that we should delete.</param>
    /// <returns>True if word was in trie else false.</returns>
    /// <exception cref="ArgumentNullException">Word can't be null.</exception>
    public bool Remove(List<byte> element)
    {
        ArgumentNullException.ThrowIfNull(element);

        if (!Contains(element))
        {
            return false;
        }

        TrieVertex current = root;
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
    /// Get value of list of bytes.
    /// </summary>
    /// <param name="word">List of bytes.</param>
    /// <returns>Value of word.</returns>
    /// <exception cref="ArgumentNullException">word can't be null.</exception>
    public int GetValue(List<byte> word)
    {
        ArgumentNullException.ThrowIfNull(word);

        TrieVertex current = root;

        foreach (var symbol in word)
        {
            if (!current.Childrens.ContainsKey(symbol))
            {
                return -1;
            }

            current = current.Childrens[symbol];
        }

        return current.Value;
    }

    private class TrieVertex
    {
        /// <summary>
        /// Innitialize new instance of TrieVertex.
        /// </summary>
        /// <param name="value">Vertex value</param>
        public TrieVertex(int value)
        {
            Value = value;
        }

        /// <summary>
        /// Value of vertex.
        /// </summary>
        public int Value {get; set; }

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
        public readonly Dictionary<byte, TrieVertex> Childrens = new ();
    }
}